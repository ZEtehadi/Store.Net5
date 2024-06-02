using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Services.Orders.Queries.GetUserOrders;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Orders.Queries.GetOrderDetails
{
   public interface IGetOrderDetailsService
    {
        ResultDto<List<OrderDetailUseDto>> Execute(long OrderId);
    }
    public class GetOrderDetailService : IGetOrderDetailsService
    {
        private readonly IDataBaseContext _Context;
        public GetOrderDetailService(IDataBaseContext context)
        {
            _Context = context;
        }

        public ResultDto<List<OrderDetailUseDto>> Execute(long OrderId)
        {
            var orderDetails = _Context.OrderDetails.Include(p => p.Product)
                                                    .Where(p => p.OrderId == OrderId)
                                                    .ToList()
                                                    .OrderByDescending(p => p.Id)
                                                    .Select(p => new OrderDetailUseDto()
                                                    {
                                                        OrderId = p.OrderId,
                                                        ProductId = p.ProductId,
                                                        ProductName = p.Product.Name,
                                                        Count = p.Count,
                                                        Price = p.Price
                                                    }).ToList();

            return new ResultDto<List<OrderDetailUseDto>>()
            {
                Data=orderDetails,
                IsSeccess=true,
                Message=$"Sending Order Detial by OrderId={OrderId} wat Successful"
            };
        }
    }
}
