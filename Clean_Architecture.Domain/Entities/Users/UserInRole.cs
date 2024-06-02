using Clean_Architecture.Domain.Entities.Commons;

namespace Clean_Architecture.Domain.Entities.Users
{
    public class UserInRole: BaseEntity
    {
        public long Id { get; set; }


        //Relation to User
        public virtual User User { get; set; }
        public long UserId { get; set; }


        //Relation to Role
        public virtual Role Role { get; set; }
        public long RoleId { get; set; }

    }
}
