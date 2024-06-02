using Clean_Architecture.Domain.Entities.Orders;
using System;

namespace Clean_Architecture.Application.Services.Orders.Queries.GetOrdersForAdmin
{
    public class OrdersDto
    {
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long RequstPayId { get; set; }
        public DateTime DateTime { get; set; }
        public OrderState OrderState{ get; set; }
        public int ProductCount { get; set; }
    }
}
