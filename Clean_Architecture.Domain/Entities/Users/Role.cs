using Clean_Architecture.Domain.Entities.Commons;
using System.Collections.Generic;

namespace Clean_Architecture.Domain.Entities.Users
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }


        //Relation to UserInRole
        public virtual ICollection<UserInRole> UserInRoles { get; set; }

    }
}
