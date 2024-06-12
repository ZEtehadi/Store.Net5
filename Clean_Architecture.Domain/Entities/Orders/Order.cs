using Clean_Architecture.Domain.Entities.Commons;
using Clean_Architecture.Domain.Entities.Finances;
using Clean_Architecture.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Domain.Entities.Orders
{
    public class Order:BaseEntity<long>
    {
        public virtual User User { get; set; }
        public long UserId { get; set; }


        public virtual RequestPay RequestPay{ get; set; }
        public long RequestPayId { get; set; }

        public OrderState OrderState { get; set; }
        public string Address{ get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }

    public enum OrderState
    {
        [Display(Name="در حال پردازش")]
        Processing=0,

        [Display(Name = "لغو شده")]
        Canceled = 1,

        [Display(Name = "تحویل شده")]
        Delivered = 2,
    }


}
