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
    public class Products_sameCatController : Controller
    {
        private readonly IProductFacadAdmin _productFacadAdmin;
        public Products_sameCatController(IProductFacadAdmin productFacadAdmin)
        {
            _productFacadAdmin = productFacadAdmin;
        }

        public IActionResult Index(long CatId, int Page = 1,int PageSize=20)
        {
            var Products_sameCategory = _productFacadAdmin.GetProduct_SameCatForAdminService.Execute(CatId, Page, PageSize).Data;
            return View(Products_sameCategory);
        }
    }
}
