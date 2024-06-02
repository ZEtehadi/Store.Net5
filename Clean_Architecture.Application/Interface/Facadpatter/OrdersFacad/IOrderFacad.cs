using Clean_Architecture.Application.Services.Orders.Commands.AddNewOrder;
using Clean_Architecture.Application.Services.Orders.Commands.ChangeOrderStateService;
using Clean_Architecture.Application.Services.Orders.Queries.GetOrderDetails;
using Clean_Architecture.Application.Services.Orders.Queries.GetOrdersForAdmin;
using Clean_Architecture.Application.Services.Orders.Queries.GetUserOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Interface.Facadpatter.OrdersFacad
{
    public interface IOrderFacad
    {
        AddNewOrderService AddNewOrderService { get; }
        GetUserOrdersService GetUserOrdersService { get; }
        GetOrderDetailService GetOrderDetailService { get; }
        GetOrdersForAdminService GetOrdersForAdminService { get; }
        ChangeOrderStateService ChangeOrderStateService { get; }
    }
}
