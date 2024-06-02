using Clean_Architecture.Domain.Entities.Commons;
using Clean_Architecture.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Domain.Entities.Carts
{
    public class Cart : BaseEntity
    {
       public virtual User User { get; set; }
        public long? UserId { get; set; }
        public Guid BrowserId { get; set; }
        public bool Fininshed { get; set; }

        public  ICollection<CartItems> CartItems { get; set; }

    }
}
