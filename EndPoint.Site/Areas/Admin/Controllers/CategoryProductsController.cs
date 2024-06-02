using Clean_Architecture.Application.Interface.Facadpatter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class CategoryProductsController : Controller
    {
        private readonly IProductFacadAdmin _productFacadAdmin;
        public CategoryProductsController(IProductFacadAdmin productFacadAdmin)
        {
            _productFacadAdmin = productFacadAdmin;
        }

        public IActionResult Index()
        {
            var categories = _productFacadAdmin.GetCategoriesForAdmin.Execute().Data;
            return View(categories);
        }
    }
}
