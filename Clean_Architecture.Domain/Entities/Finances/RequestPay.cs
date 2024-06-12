using Clean_Architecture.Domain.Entities.Commons;
using Clean_Architecture.Domain.Entities.DisCounts;
using Clean_Architecture.Domain.Entities.Orders;
using Clean_Architecture.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Domain.Entities.Finances
{
    public class RequestPay : BaseEntity<long>
    {
        public Guid Guid { get; set; }
        public virtual User User { get; set; }
        public long? Userid{ get; set; }
        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;

        public virtual ICollection<Order> Orders { get; set; }

        public virtual DisCount DisCount { get; set; }
        public long? DisCountId { get; set; }

    }
}
