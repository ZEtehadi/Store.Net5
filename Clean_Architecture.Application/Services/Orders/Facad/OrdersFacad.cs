using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Interface.Facadpatter.OrdersFacad;
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

namespace Clean_Architecture.Application.Services.Orders.Facad
{
    public class OrdersFacad : IOrderFacad
    {
        private readonly IDataBaseContext _context;
        public OrdersFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private AddNewOrderService _addNewOrderService;
        public AddNewOrderService AddNewOrderService
        {
            get
            {
                return _addNewOrderService = _addNewOrderService ?? new AddNewOrderService(_context);
            }
        }


        private GetUserOrdersService _getUserOrdersService;
        public GetUserOrdersService GetUserOrdersService
        {
            get
            {
                return _getUserOrdersService = _getUserOrdersService ?? new GetUserOrdersService(_context);
            }
        }


        private GetOrderDetailService _getOrderDetailService;
        public GetOrderDetailService GetOrderDetailService
        {
            get
            {
                return _getOrderDetailService = _getOrderDetailService ?? new GetOrderDetailService(_context);
            }
        }

        private GetOrdersForAdminService _getOrdersForAdminService;
        public GetOrdersForAdminService GetOrdersForAdminService
        {
            get
            {
                return _getOrdersForAdminService = _getOrdersForAdminService ?? new GetOrdersForAdminService(_context);
            }
        }

        private ChangeOrderStateService _changeOrderStateService;
        public ChangeOrderStateService ChangeOrderStateService
        {
            get
            {
                return _changeOrderStateService = _changeOrderStateService ?? new ChangeOrderStateService(_context);
            }
        }
    }
}
