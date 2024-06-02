using Clean_Architecture.Application.Interface.Facadpatter;
using Clean_Architecture.Application.Services.Products.Commands.AddNewProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class ProductsController : Controller
    {
        private readonly IProductFacadAdmin _productFacad;

        public ProductsController(IProductFacadAdmin productFacad)
        {
            _productFacad = productFacad;
        }

        public IActionResult Index(int Page=1,int PageSize=20)//Show Products
        {
            return View(_productFacad.GetProductForAdminService.Execute(Page,PageSize).Data);
        }


        public IActionResult Detail(long Id)//Show Product Detail
        {
            return View(_productFacad.GetProductDetailForAdminService.Execute(Id).Data);
        }

        [HttpPost]
        public IActionResult Delete(long Id)//Deleting Product
        {
            return Json(_productFacad.DeleteProductForAdminService.Execute(Id));
        }


        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacad.GetAllCategoriesService.Execute().Data, "Id", "Name");

            return View();
        }


        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProduct_Features> Features)
        {
            //Getting All Images to Images
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.Featurese = Features;
            return Json(_productFacad.AddNewProductService.Execute(request));
        }


        [HttpPost]
        public IActionResult ChangeDisplayed(long Id)
        {
            return Json(_productFacad.ChangeDisplayedService.Execute(Id));
        }

    }
}
