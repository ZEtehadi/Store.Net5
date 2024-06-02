using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Orders;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Orders.Queries.GetOrdersForAdmin
{
    public  interface IGetOrdersForAdminService
    {
        ResultDto<List<OrdersDto>> Execute(OrderState orderState, string SearchKey);
    }
}
