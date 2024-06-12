using Clean_Architecture.Domain.Entities.Commons;
using Clean_Architecture.Domain.Entities.Products;

namespace Clean_Architecture.Domain.Entities.Carts
{
    public class CartItems : BaseEntity<long>
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }

        public int Count  { get; set; }
        public int Price { get; set; }

        public  virtual Cart Cart { get; set; }
        public long CartId { get; set; }
    }
}
