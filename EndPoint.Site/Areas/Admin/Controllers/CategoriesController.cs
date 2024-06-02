using Clean_Architecture.Application.Interface.Facadpatter;
using Clean_Architecture.Application.Services.Products.Commands.EditCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductFacadAdmin _productFacad;
        public CategoriesController(IProductFacadAdmin productFacad)
        {
            _productFacad = productFacad;
        }

        public IActionResult Index(long? ParentId)
        {
            if (ParentId.HasValue)
            {
                ViewBag.HasParentOrNot = "Yes";
                ViewBag.HasParentTrue_Name = ParentId;
            }
            else
            {
                ViewBag.HasParentOrNot = "No";
            }
            return View(_productFacad.GetCategoriesService.Execute(ParentId).Data);
        }

        [HttpGet]
        public IActionResult AddNewCategory(long? ParentId)
        {
            ViewBag.ParentId = ParentId;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(long? ParentId, string Name, IFormFile Src)
        {

            if (Request.Form.Files.Count > 0)
            {
                Src = Request.Form.Files[0];
            }

            var result = _productFacad.AddNewCategoryService.Execute(ParentId, Name, Src);
            return Json(result);
        }



        [HttpPost]
        public IActionResult CategoryStatusChange(long CatId)
        {
            return Json(_productFacad.CategoryStatusChangeService.Execute(CatId));
        }


        [HttpPost]
        public IActionResult Edit(long Id, string Name, IFormFile Src)
        {
            if (Request.Form.Files.Count > 0)
            {
                Src = Request.Form.Files[0];
            }

            return Json(_productFacad.EditCategoryService.Execute(new RequestEditCategoryDto()
            {
                Id = Id,
                Name = Name,
                ImageSrc=Src
            }));
        }


        [HttpPost]
        public IActionResult Delete(long Id)
        {
            return Json(_productFacad.DeleteCategoryService.Execute(Id));
        }
    }
}
