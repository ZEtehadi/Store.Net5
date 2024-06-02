using Clean_Architecture.Domain.Entities.Commons;
using Clean_Architecture.Domain.Entities.Finances;
using Clean_Architecture.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Domain.Entities.DisCounts
{
    public class DisCount:BaseEntity
    {
        //تخفیف
        public string DisCountCode { get; set; }
        public float Percent { get; set; } = 0;

        // DateTime --> Start DateTime
        // RemovedTime --> Finish DateTime

        //public virtual ICollection<RequestPay> RequestPays { get; set; }

    }
}
