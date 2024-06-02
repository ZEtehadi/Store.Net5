using Clean_Architecture.Application.Services.Common.Queries.GetHomePageImages;
using Clean_Architecture.Application.Services.Common.Queries.GetSlider;
using Clean_Architecture.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Models.ViewModels.HomePage
{
    public class HomePageViewModel
    {
        public List<SliderDto> Sliders { get; set; }
        public List<HomePageImagesDto> HomePageImages { get; set; }
        public List<ProductForSiteDto> Mobile { get; set; }
        public List<ProductForSiteDto> KeyBoard { get; set; }
        public List<ProductForSiteDto> LapTop { get; set; }
        public List<ProductForSiteDto> Manitore { get; set; }
    }
}
