using Clean_Architecture.Application.Interface.Facadpatter.OrdersFacad;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderFacad _ordreFacad;
        public OrderController(IOrderFacad orderFacad)
        {
            _ordreFacad = orderFacad;
        }

        public IActionResult Index()
        {
            long UserId = ClaimUtility.GetUserId(User).Value;
            return View(_ordreFacad.GetUserOrdersService.Execute(UserId).Data);
        }


        public IActionResult OrderDetial(long OrderId)
        {
            var DetailOrders = _ordreFacad.GetOrderDetailService.Execute(OrderId).Data;
            return Json(DetailOrders);
        }

    }
}
