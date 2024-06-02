namespace Clean_Architecture.Application.Services.Orders.Queries.GetUserOrders
{
    public class OrderDetailUseDto
    {
        public long OrderId { get; set; }
        public long OrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
