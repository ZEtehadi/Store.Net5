using Clean_Architecture.Domain.Entities.Commons;

namespace Clean_Architecture.Domain.Entities.Products
{
    public class ProductFeatures: BaseEntity<long>
    {
        //has Relation to Product       
        //Must have one Product
        public  virtual Product Product { get; set; }
        public long ProductId { get; set; }


        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
