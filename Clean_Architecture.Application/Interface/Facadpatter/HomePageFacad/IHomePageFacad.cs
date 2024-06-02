using Clean_Architecture.Application.Services.Common.Queries.GetSlider;
using Clean_Architecture.Application.Services.HomePage.Commands.AddNewHomeImage;
using Clean_Architecture.Application.Services.HomePage.Commands.AddNewSlider;
using Clean_Architecture.Application.Services.HomePage.Queries.GetAllSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Interface.Facadpatter.HomePageFacad
{
    public interface IHomePageFacad
    {
        AddNewSliderService AddNewSliderService { get; }
        GetAllSliderService GetAllSliderService { get; }
        AddNewHomeImagesService AddNewHomeImagesService { get; }
    }
}
