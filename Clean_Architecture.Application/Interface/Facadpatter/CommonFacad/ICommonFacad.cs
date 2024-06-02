using Clean_Architecture.Application.Services.Common.Queries.GetCategory;
using Clean_Architecture.Application.Services.Common.Queries.GetHomePageImages;
using Clean_Architecture.Application.Services.Common.Queries.GetMenuItem;
using Clean_Architecture.Application.Services.Common.Queries.GetSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Interface.Facadpatter.CommonFacad
{
    public interface ICommonFacad
    {
        IGetMenuItemService GetMenuItemService { get; }
        IGetCategoryService GetCategoryService { get; }
        GetSliderService GetSliderService { get; }
        GetHomePageImagesService GetHomePageImagesService { get; }

    }
}
