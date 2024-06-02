using Clean_Architecture.Common.Dto;
using Microsoft.AspNetCore.Http;

namespace Clean_Architecture.Application.Services.HomePage.Commands.AddNewSlider
{
    public interface IAddNewSliderService
    {
        ResultDto Execute(RequestSliderDto request);

    }
}
