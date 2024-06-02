using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Clean_Architecture.Application.Services.HomePage.Commands.AddNewSlider
{
    public class RequestSliderDto
    {
        public string Src { get; set; }
        public string Link { get; set; }
        public int ClickCount { get; set; }
        public List<IFormFile> Images { get; set; }

    }
}
