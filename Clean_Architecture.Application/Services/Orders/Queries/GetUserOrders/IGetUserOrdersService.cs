using Clean_Architecture.Common.Dto;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Orders.Queries.GetUserOrders
{
    public interface IGetUserOrdersService
    {
        ResultDto<List<OrdersUserDto>> Execute(long UserId);
    }
}
