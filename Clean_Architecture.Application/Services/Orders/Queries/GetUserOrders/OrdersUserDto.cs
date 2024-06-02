using Clean_Architecture.Domain.Entities.Orders;
using System;
using System.Collections.Generic;

namespace Clean_Architecture.Application.Services.Orders.Queries.GetUserOrders
{
    public class OrdersUserDto
    {
        public long OrderId { get; set; }
        public long RequestPayId { get; set; }
        public OrderState OrderState { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }
        // public List<OrderDetailUseDto> UserOrderDetails { get; set; }
    }
}
