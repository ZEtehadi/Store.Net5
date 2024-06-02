namespace Clean_Architecture.Application.Services.Carts
{
    public class CartItemDto
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public string ImageSrc { get; set; }
    }
}
