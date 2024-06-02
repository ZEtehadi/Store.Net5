using Clean_Architecture.Domain.Entities.HomePage;
using Microsoft.AspNetCore.Http;

namespace Clean_Architecture.Application.Services.HomePage.Commands.AddNewHomeImage
{
    public class RequestAddNewHomeImagesDto
    {
        public string Link { get; set; }
        public IFormFile File { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
