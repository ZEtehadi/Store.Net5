using Clean_Architecture.Domain.Entities.Commons;
using Clean_Architecture.Domain.Entities.Products;

namespace Clean_Architecture.Domain.Entities.Orders
{
    public class OrderDetail : BaseEntity<long>
    {
        public virtual Order Order { get; set; }
        public long OrderId { get; set; }

        public virtual Product Product { get; set; }
        public long ProductId { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }
    }


}
