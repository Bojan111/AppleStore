using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apple.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Settings;

namespace Apple.Pages.Admin.ProductType
{
    [Authorize(Roles = SD.ManagerRole)]
    public class EditModel : PageModel
    {
		private readonly IUnitOfWork unitOfWork;

		public EditModel(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

        [BindProperty]
        public Apple.Core.ProductType ProductType { get; set; } 
        public IActionResult OnGet(int? Id)
        {
            ProductType = new Apple.Core.ProductType();
            if(Id != null)
			{
                ProductType = unitOfWork.ProductType.GetFirstOrDefault(s => s.Id == Id);
                if(ProductType == null)
				{
                    return NotFound();
				}
			}
            return Page();
        }

        public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
                return Page();
			}
            if(ProductType.Id == 0)
			{
                unitOfWork.ProductType.Add(ProductType);
			}
            else
			{
                unitOfWork.ProductType.Update(ProductType);
			}
            unitOfWork.Save();
            return RedirectToPage("./Index");
		}
    }
}