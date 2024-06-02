using Clean_Architecture.Application.Interface.Facadpatter.DisCountsFacad;
using Clean_Architecture.Application.Services.DisCounts.Commands.AddNewDisCount;
using Clean_Architecture.Application.Services.DisCounts.Commands.EditDisCount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class DisCountController : Controller
    {
        private readonly IDisCountFacad _disCountFacad;
        public DisCountController(IDisCountFacad disCountFacad)
        {
            _disCountFacad = disCountFacad;
        }


        public IActionResult Index()
        {
            var discount = _disCountFacad.GetDisCountService.Execute().Data;
            return View(discount);
        }

       

        [HttpPost]
        public IActionResult Add(string DisCountCode, float Percent)
        {
            var result = _disCountFacad.AddNewDisCountService.Execute(new RequestDisCountDto()
            {
                DisCountCode = DisCountCode,
                Percent = Percent,
            });
            return Json(result);
        }


        [HttpPost]
        public IActionResult Edit(long Id, string DisCountCode, float Percent)
        {
            var result = _disCountFacad.EditDisCountService.Execute(new RequestDiscountDto()
            {
                Id = Id,
                DisCountCode = DisCountCode,
                Percent = Percent,
            });
            return Json(result);
        }  
        
        
        
        [HttpPost]
        public IActionResult StatusChange(long Id ,bool Status)
        {
            var result = _disCountFacad.ChangeStatusChange.Execute(Id, Status);
            return Json(result);
        }


        [HttpPost]
        public IActionResult Delete (long Id )
        {
            var result = _disCountFacad.DeleteDisCountService.Execute(Id);
            return Json(result);
        }
    }
}
