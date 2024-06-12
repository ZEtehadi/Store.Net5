using Clean_Architecture.Domain.Entities.Commons;

namespace Clean_Architecture.Domain.Entities.Products
{
    public class ProductImage: BaseEntity<long>
    {
        //has Relation to Product
        //Must have One Product
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }

        public string Src { get; set; }
    }
}
