using Clean_Architecture.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Domain.Entities.Products
{
   public class Category :BaseEntity<long>
    {
        public string Name { get; set; }
        public string Src { get; set; } = @"images\Categories\Temp.jpg";


        //Parent
        public virtual Category ParentCategory { get; set; }
        public long? ParentCategoryId { get; set; }


        //Child
        public virtual ICollection<Category> SubCategories { get; set; }


        public virtual ICollection<Product> Products { get; set; }
    }
}
