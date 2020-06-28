using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Apple.Data.Interface;
using Apple.Core;
using Settings;
using Apple.Models;

namespace Apple.Pages.Customer.Cart
{
    public class PayModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public PayModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public OrderDetailsCart detailCart { get; set; }

        public IActionResult OnGet()
        {
            detailCart = new OrderDetailsCart()
            {
                OrderHeaders = new Apple.Core.OrderHeader()
            };

            detailCart.OrderHeaders.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<ShoppingCart> cart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);

            if (cart != null)
            {
                detailCart.ShoppingCarts = cart.ToList();
            }

            foreach (var cartList in detailCart.ShoppingCarts)
            {
                cartList.Catalog = _unitOfWork.Catalog.GetFirstOrDefault(m => m.Id == cartList.CatalogId);
                detailCart.OrderHeaders.OrderTotal += (cartList.Catalog.Price * cartList.Count);
            }

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(c => c.Id == claim.Value);
            detailCart.OrderHeaders.PickupName = applicationUser.FullName;
            detailCart.OrderHeaders.PhoneNumber = applicationUser.PhoneNumber;
            return Page();

        }

        public IActionResult OnPost(string stripeToken)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            detailCart.ShoppingCarts = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToList();

            detailCart.OrderHeaders.PaymentStatus = SD.PaymentStatusPending;
            detailCart.OrderHeaders.OrderDate = DateTime.Now;
            detailCart.OrderHeaders.UserId = claim.Value;
            detailCart.OrderHeaders.Status = SD.PaymentStatusPending;

			List<OrderDetails> orderDetailsList = new List<OrderDetails>();
			_unitOfWork.OrderHeader.Add(detailCart.OrderHeaders);
			_unitOfWork.Save();

			foreach (var item in detailCart.ShoppingCarts)
			{
				item.Catalog = _unitOfWork.Catalog.GetFirstOrDefault(m => m.Id == item.CatalogId);
				OrderDetails orderDetails = new OrderDetails
				{
					CatalogId = item.CatalogId,
					OrderId = detailCart.OrderHeaders.Id,
					Description = item.Catalog.Specifications,
					Name = item.Catalog.Name,
					Price = item.Catalog.Price,
					Count = item.Count
				};
				detailCart.OrderHeaders.OrderTotal += (orderDetails.Count * orderDetails.Price);
				_unitOfWork.OrderDetails.Add(orderDetails);

			}
			detailCart.OrderHeaders.OrderTotal = Convert.ToDouble(String.Format("{0:.##}", detailCart.OrderHeaders.OrderTotal));
            _unitOfWork.ShoppingCart.RemoveRange(detailCart.ShoppingCarts);
            HttpContext.Session.SetInt32(SD.ShoppingCart, 0);
            _unitOfWork.Save();

            if (stripeToken != null)
            {

                var options = new ChargeCreateOptions
                {
                    //Amount is in cents
                    Amount = Convert.ToInt32(detailCart.OrderHeaders.OrderTotal * 100),
                    Currency = "eur",
                    Description = "Order ID : " + detailCart.OrderHeaders.Id,
                    Source = stripeToken
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);

                detailCart.OrderHeaders.TransactionId = charge.Id;

                if (charge.Status.ToLower() == "succeeded")
                {
                    //email 
                    detailCart.OrderHeaders.PaymentStatus = SD.PaymentStatusApproved;
                    detailCart.OrderHeaders.Status = SD.StatusSubmitted;
                }
                else
                {
                    detailCart.OrderHeaders.PaymentStatus = SD.PaymentStatusRejected;
                }
            }
            else
            {
                detailCart.OrderHeaders.PaymentStatus = SD.PaymentStatusRejected;
            }
            _unitOfWork.Save();

            return RedirectToPage("/Customer/Cart/OrderConfirmation", new { id = detailCart.OrderHeaders.Id });

        }



    }
}