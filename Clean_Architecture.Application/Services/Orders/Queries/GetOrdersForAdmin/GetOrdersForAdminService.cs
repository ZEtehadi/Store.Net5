using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Architecture.Application.Services.Orders.Queries.GetOrdersForAdmin
{
    public class GetOrdersForAdminService : IGetOrdersForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetOrdersForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<OrdersDto>> Execute(OrderState orderState,string SearchKey)
        {
            var Orders = _context.Orders.Include(p => p.OrderDetails)
                                        .Include(p=>p.User)
                                      .Where(p => p.OrderState == orderState)
                                      .OrderByDescending(p => p.Id)
                                      .ToList()
                                      .Select(p => new OrdersDto()
                                      {
                                          OrderId = p.Id,
                                          UserId = p.UserId,
                                          UserName=p.User.FullName,
                                          RequstPayId = p.RequestPayId,
                                          OrderState = p.OrderState,
                                          DateTime = p.DateTime,
                                          ProductCount = p.OrderDetails.Count(),
                                      }).ToList();

            if (!string.IsNullOrEmpty(SearchKey))
            {
                Orders = Orders.Where(p => p.DateTime.ToString() == SearchKey ||
                                         p.OrderId.ToString() == SearchKey ||
                                         p.UserId.ToString() == SearchKey).ToList();
            }
                return new ResultDto<List<OrdersDto>>()
            {
                Data=Orders,
                IsSeccess=true,
                Message ="Send Orders was Seccessfull"
            };
        }
    }
}
