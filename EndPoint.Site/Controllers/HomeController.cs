using Clean_Architecture.Application.Interface.Facadpatter;
using Clean_Architecture.Application.Interface.Facadpatter.CommonFacad;
using Clean_Architecture.Application.Services.Products.Queries.GetProductForSite;
using EndPoint.Site.Models;
using EndPoint.Site.Models.ViewModels.HomePage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommonFacad _commonFacad;
        private readonly IProductFacadSite _productFacadSite;
        public HomeController(ILogger<HomeController> logger,ICommonFacad commonFacad, IProductFacadSite productFacadSite)
        {
            _logger = logger;
            _commonFacad = commonFacad;
            _productFacadSite = productFacadSite;
        }

        public IActionResult Index()
        {
            HomePageViewModel model = new HomePageViewModel()
            {
                Sliders = _commonFacad.GetSliderService.Execute().Data,

                HomePageImages=_commonFacad.GetHomePageImagesService.Execute().Data,

                Mobile=_productFacadSite.GetProductForSiteService.Execute(Ordering.TheNewest,6,1,2,null).Data.Products,
                
                LapTop = _productFacadSite.GetProductForSiteService.Execute(Ordering.TheNewest, 6, 1, 1, null).Data.Products,
                
                KeyBoard = _productFacadSite.GetProductForSiteService.Execute(Ordering.TheNewest, 6, 1, 19, null).Data.Products,
                
                Manitore = _productFacadSite.GetProductForSiteService.Execute(Ordering.TheNewest, 6, 1,3, null).Data.Products,
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
