using Clean_Architecture.Application.Interface.Facadpatter.CommonFacad;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class GetMenu:ViewComponent
    {
        private readonly ICommonFacad _getMenuFacad;
        public GetMenu(ICommonFacad getMenuFacad)
        {
            _getMenuFacad = getMenuFacad;
        }

        public IViewComponentResult Invoke()
        {
            var menuItem = _getMenuFacad.GetMenuItemService.Execute();
            return View(viewName:"GetMenu",menuItem.Data) ;
        }

    }
}
