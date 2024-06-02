using Clean_Architecture.Application.Interface.Facadpatter.HomePageFacad;
using Clean_Architecture.Application.Services.HomePage.Commands.AddNewHomeImage;
using Clean_Architecture.Domain.Entities.HomePage;
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
    [Authorize(Roles = "Admin,Operator")]
    public class HomePageImagesController : Controller
    {
        private readonly IHomePageFacad _homePageFacad;
        public HomePageImagesController(IHomePageFacad homePageFacad)
        {
            _homePageFacad = homePageFacad;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile Images, string Link, ImageLocation ImageLocation)
        {
            return Json(_homePageFacad.AddNewHomeImagesService.Execute(new RequestAddNewHomeImagesDto()
            {
                Link = Link,
                File = Images,
                ImageLocation = ImageLocation,
            }));
        }

    }
}
