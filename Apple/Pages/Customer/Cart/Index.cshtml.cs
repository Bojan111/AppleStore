using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Apple.Data.Interface;
using Apple.Core;
using Settings;
using Apple.Models;

namespace Apple.Pages.Customer.Cart
{ 
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public OrderDetailsCart OrderDetailsCartVM { get; set; }

            public void OnGet()
            {
                OrderDetailsCartVM = new OrderDetailsCart()
                {
                    OrderHeaders = new Apple.Core.OrderHeader(),
                    ShoppingCarts = new List<ShoppingCart>()
                };

                OrderDetailsCartVM.OrderHeaders.OrderTotal = 0;

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim =claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (claim != null)
                {
                    IEnumerable<ShoppingCart> cart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);

                    if (cart != null)
                    {
                        OrderDetailsCartVM.ShoppingCarts = cart.ToList();
                    }

                    foreach (var cartList in OrderDetailsCartVM.ShoppingCarts)
                    {
                        cartList.Catalog = _unitOfWork.Catalog.GetFirstOrDefault(m => m.Id == cartList.CatalogId);
                        OrderDetailsCartVM.OrderHeaders.OrderTotal += (cartList.Catalog.Price * cartList.Count);
                    }
                }
            }

            public IActionResult OnPostPlus(int cartId)
            {
                var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
                _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
                _unitOfWork.Save();
                return RedirectToPage("/Customer/Cart/Index");
            }

            public IActionResult OnPostMinus(int cartId)
            {
                var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
                if (cart.Count == 1)
                {
                    _unitOfWork.ShoppingCart.Remove(cart);
                    _unitOfWork.Save();

                    var cnt = _unitOfWork.ShoppingCart.
                                    GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
                    HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);
                }
                else
                {
                    _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
                    _unitOfWork.Save();

                }

           
                return RedirectToPage("/Customer/Cart/Index");
            }


        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();

            var cnt = _unitOfWork.ShoppingCart.
                               GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);
            return RedirectToPage("/Customer/Cart/Index");
        }  
    }
}