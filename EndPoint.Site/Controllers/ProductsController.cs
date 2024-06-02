using Clean_Architecture.Application.Interface.Facadpatter;
using Clean_Architecture.Application.Services.Products.Queries.GetProductForSite;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductFacadSite _productFacadSite;
        public ProductsController(IProductFacadSite productFacadSite)
        {
            _productFacadSite = productFacadSite;
        }


        public IActionResult Index(Ordering ordering,string SearchKey, long? CatId=null, int Page = 1, int PageSize=20)
        {
            return View(_productFacadSite.GetProductForSiteService.Execute(ordering,PageSize,Page,CatId,SearchKey).Data);
        }


        public IActionResult Detail(long Id)
        {
            return View(_productFacadSite.GetProductDetailForSiteService.Execute(Id).Data);
        }
    }
}
