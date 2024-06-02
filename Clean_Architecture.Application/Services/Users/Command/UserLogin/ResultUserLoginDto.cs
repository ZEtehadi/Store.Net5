using System.Collections.Generic;

namespace Clean_Architecture.Application.Services.Users.Command.UserLogin
{
    public class ResultUserLoginDto
    {
        public long UserId { get; set; }
        public List<string>  Roles { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
