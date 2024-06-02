using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Interface.Facadpatter.CommonFacad;
using Clean_Architecture.Application.Services.Common.Queries.GetCategory;
using Clean_Architecture.Application.Services.Common.Queries.GetHomePageImages;
using Clean_Architecture.Application.Services.Common.Queries.GetMenuItem;
using Clean_Architecture.Application.Services.Common.Queries.GetSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Common.FacadPatern
{
    public class CommonFacad : ICommonFacad
    {
        private readonly IDataBaseContext _context;
        public CommonFacad(IDataBaseContext context)
        {
            _context = context;
        }



        private IGetMenuItemService _getMenuItemService;
        public IGetMenuItemService GetMenuItemService
        {
            get
            {
                return _getMenuItemService = _getMenuItemService ?? new GetMenuItemService(_context);
            }
        }




        private IGetCategoryService _getCategoryService;
       public IGetCategoryService GetCategoryService
        {
            get
            {
                return _getCategoryService = _getCategoryService ?? new GetCategoryService(_context);
            }
        }



        private GetSliderService _getSliderService;
       public GetSliderService GetSliderService
        {
            get
            {
                return _getSliderService = _getSliderService ?? new GetSliderService(_context);
            }
        }



        private GetHomePageImagesService _getHomePageImagesService;
        public GetHomePageImagesService GetHomePageImagesService
        {
            get
            {
                return _getHomePageImagesService = _getHomePageImagesService ?? new GetHomePageImagesService(_context);
            }
        }
    }
}
