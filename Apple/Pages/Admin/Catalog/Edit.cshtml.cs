using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Apple.Data.Interface;
using Apple.Core;
using Settings;
using Apple.Models;

namespace Apple.Pages.Admin.Catalog
{
    [Authorize(Roles = SD.ManagerRole)]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public CatalogModel CatalogModel { get; set; }

        public IActionResult OnGet(int? id)
        {
            CatalogModel = new CatalogModel
            {
                CategoryList = _unitOfWork.Category.Categories(),
                ProductTypeList = _unitOfWork.ProductType.ProductTypes(),
                Catalogs = new Apple.Core.Catalog()
            };
            if (id != null)
            {
                CatalogModel.Catalogs = _unitOfWork.Catalog.GetFirstOrDefault(u => u.Id == id);
                if (CatalogModel.Catalogs == null)
                {
                    return NotFound();
                }
            }
            return Page();

        }


        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (CatalogModel.Catalogs.Id == 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\AppleImg");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                CatalogModel.Catalogs.Image = @"\images\AppleImg\" + fileName + extension;

                _unitOfWork.Catalog.Add(CatalogModel.Catalogs);
            }
            else
            {
                //Edit Menu Item
                var objFromDb = _unitOfWork.Catalog.Get(CatalogModel.Catalogs.Id);
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\AppleImg");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }


                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    CatalogModel.Catalogs.Image = @"\images\AppleImg\" + fileName + extension;
                }
                else
                {
                    CatalogModel.Catalogs.Image = objFromDb.Image;
                }


                _unitOfWork.Catalog.Update(CatalogModel.Catalogs);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}