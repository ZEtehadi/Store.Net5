using System.Collections.Generic;

namespace Clean_Architecture.Application.Services.Carts
{
    public class CartDto
    {
        public long CartId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
        public int ProductCount { get; set; }
        public int SumAmount { get; set; }
    }
}
