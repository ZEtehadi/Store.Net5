using Clean_Architecture.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Domain.Entities.Products
{
    public class Product: BaseEntity<long>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
        public int ViewCount { get; set; } = 0;

        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductFeatures> ProductFeatures { get; set; }
    }
}
