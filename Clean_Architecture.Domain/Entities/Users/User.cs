using Clean_Architecture.Domain.Entities.Commons;
using Clean_Architecture.Domain.Entities.Finances;
using Clean_Architecture.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Domain.Entities.Users
{
    // We Inherit from "BaseEntity" so the Property of "BaseEntity" are applied to 
    //"User" as well
    //so We don't have Id in "User" because the Id is in "BaseEntity"
    public class User:BaseEntity<long>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string? ImegeSrc { get; set; }


        //Relation to UserInRole
        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<RequestPay> RequestPays { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}
