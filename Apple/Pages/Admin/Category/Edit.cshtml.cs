using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apple.Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Settings;

namespace Apple.Pages.Admin.Category
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
        public Apple.Core.Category Category { get; set; }
        public IActionResult OnGet(int? Id)
        {
            Category = new Apple.Core.Category();
            if(Id != null)
			{
                Category = unitOfWork.Category.GetFirstOrDefault(s => s.Id == Id);
                if(Category == null)
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
            if(Category.Id == 0)
			{
                unitOfWork.Category.Add(Category);
			}
            else
			{
                unitOfWork.Category.Update(Category);
			}
            unitOfWork.Save();
            return RedirectToPage("./Index");
		}
    }
}