using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Interface.Facadpatter.OrdersFacad;
using Clean_Architecture.Application.Services.Users.Queries.GetUseDetailById;
using Clean_Architecture.Domain.Entities.Orders;
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
    public class OrdersController : Controller
    {
        private readonly IDataBaseContext _context;
        private readonly IOrderFacad _orderFacad;
        private readonly IGetUserDetailByIdService _getUserDetailByIdService;
        public OrdersController(IDataBaseContext context,IOrderFacad orderFacad, IGetUserDetailByIdService getUserDetailByIdService)
        {
            _context = context;
            _orderFacad = orderFacad;
            _getUserDetailByIdService = getUserDetailByIdService;
        }


        public IActionResult Index(OrderState orderState, string SearchKey)
        {
            var orders = _orderFacad.GetOrdersForAdminService.Execute(orderState,  SearchKey).Data;
            return View(orders);
        }

        public IActionResult ShowOrderDetails(long OrderId)
        {
            var OrderDetail = _orderFacad.GetOrderDetailService.Execute(OrderId).Data;
            return Json(OrderDetail);
        }

        public IActionResult ChangeOrderState(long OrderId, OrderState orderState)
        {
            var ChangeResult = _orderFacad.ChangeOrderStateService.Execute(OrderId, orderState);
            return Json(ChangeResult);
        }

        public IActionResult ShowUserDetails(long UserId)
        {
            var userDetail = _getUserDetailByIdService.Execute(UserId).Data;
            return Json(userDetail);
        }

    }
}
