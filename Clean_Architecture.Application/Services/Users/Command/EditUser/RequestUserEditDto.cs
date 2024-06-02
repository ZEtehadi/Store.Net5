using Microsoft.AspNetCore.Http;

namespace Clean_Architecture.Application.Services.Users.Command.EditUser
{
    public class RequestUserEditDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public IFormFile? ImageSrc { get; set; }
        public string PhoneNubmer { get; set; }
        public string Password { get; set; }
        
    }
}
