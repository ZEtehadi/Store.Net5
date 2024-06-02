using Clean_Architecture.Application.Interface.Facadpatter.CommonFacad;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class Search:ViewComponent
    {
        private readonly ICommonFacad _commonFacad;
        public Search(ICommonFacad commonFacad)
        {
            _commonFacad = commonFacad;
        }

        public IViewComponentResult Invoke()
        {
            return View(viewName: "Search", _commonFacad.GetCategoryService.Execute().Data);
        }


       
    }
}
