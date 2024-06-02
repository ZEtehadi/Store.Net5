using Clean_Architecture.Application.Interface.Facadpatter.HomePageFacad;
using Clean_Architecture.Application.Services.HomePage.Commands.AddNewSlider;
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
    public class SliderController : Controller
    {
        private readonly IHomePageFacad _homePageFacad;
        public SliderController(IHomePageFacad homePageFacad)
        {
            _homePageFacad = homePageFacad;
        }

        public IActionResult Index()
        {
            return View(_homePageFacad.GetAllSliderService.Execute().Data);
        }

        ///////////////////////////////////////

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RequestSliderDto request,string Link,IFormFile Images)
        {
            
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            return Json(_homePageFacad.AddNewSliderService.Execute(request));
        }

        ///////////////////////////////////////

    }
}
