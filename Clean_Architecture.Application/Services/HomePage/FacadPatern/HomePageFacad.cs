using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Interface.Facadpatter.HomePageFacad;
using Clean_Architecture.Application.Services.Common.Queries.GetSlider;
using Clean_Architecture.Application.Services.HomePage.Commands.AddNewHomeImage;
using Clean_Architecture.Application.Services.HomePage.Commands.AddNewSlider;
using Clean_Architecture.Application.Services.HomePage.Queries.GetAllSlider;
using Clean_Architecture.Common.UploadFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.HomePage.FacadPatern
{
    public class HomePageFacad:IHomePageFacad
    {
        private readonly IDataBaseContext _context;
        private readonly UploadFilee _uploadFilee;
        public HomePageFacad(IDataBaseContext context, UploadFilee uploadFilee)
        {
            _context = context;
            _uploadFilee = uploadFilee;
        }



        ////Adding AddNewSlider Service
        private AddNewSliderService _addNewSliderService;
        public AddNewSliderService AddNewSliderService
        {
            get
            {
                return _addNewSliderService = _addNewSliderService ?? new AddNewSliderService(_context ,_uploadFilee);
            }
        }



        ////Adding GetSlider Service
        private GetAllSliderService _getAllSliderService;
        public GetAllSliderService GetAllSliderService
        {
            get
            {
                return _getAllSliderService = _getAllSliderService ?? new GetAllSliderService(_context);
            }
        }



        ////Adding AddNewHomePage Service
        private AddNewHomeImagesService _addNewHomeImagesService;
        public AddNewHomeImagesService AddNewHomeImagesService
        {
            get
            {
                return _addNewHomeImagesService = _addNewHomeImagesService ?? new AddNewHomeImagesService(_context,_uploadFilee);
            }
        }
    }
}
