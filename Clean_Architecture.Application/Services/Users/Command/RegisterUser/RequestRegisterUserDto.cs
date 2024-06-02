using System.Collections.Generic;

namespace Clean_Architecture.Application.Services.Users.Command.RegisterUser
{
    public class RequestRegisterUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<RolesInRegisterUserDto> roles { get; set; }
        public string  Password { get; set; }
        public string RePassword { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }

    }
}
