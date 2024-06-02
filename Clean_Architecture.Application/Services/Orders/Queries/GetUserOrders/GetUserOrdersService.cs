using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Architecture.Application.Services.Orders.Queries.GetUserOrders
{
    public class GetUserOrdersService : IGetUserOrdersService
    {
        private readonly IDataBaseContext _context;
        public GetUserOrdersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<OrdersUserDto>> Execute(long UserId)
        {
            var order = _context.Orders.Include(p => p.OrderDetails)
                                     .ThenInclude(p => p.Product)
                                     .Include(p => p.RequestPay)
                                     .Where(p => p.UserId == UserId)
                                     .OrderByDescending(p => p.Id)
                                     .ToList()
                                     .Select(p => new OrdersUserDto()
                                     {
                                         OrderId = p.Id,
                                         RequestPayId = p.RequestPayId,
                                         OrderState = p.OrderState,
                                         OrderDate = p.DateTime,
                                         Amount = p.RequestPay.Amount,
                                     }).ToList();
                         
            return new ResultDto<List<OrdersUserDto>>()
            {
                Data=order,
                IsSeccess=true,
                Message="Send Users Order was Successfull"
            };
        }
    }
}
