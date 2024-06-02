using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Common.UploadFile;
using Clean_Architecture.Domain.Entities.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.HomePage.Commands.AddNewSlider
{

    public class AddNewSliderService : IAddNewSliderService
    {

        private readonly IDataBaseContext _context;
        private readonly UploadFilee _uploadFilee;
        public AddNewSliderService(IDataBaseContext context, UploadFilee uploadFilee)
        {
            _context = context;
            _uploadFilee = uploadFilee;
        }

        public ResultDto Execute(RequestSliderDto request)
        {
            try
            {
                //foreach (var item in request.Images)
                //{
                //    var resultUpoad = _uploadFilee.UploadFileMethod(item, @"HomePage\Slider\");

                //}

                var resultUpoad = _uploadFilee.UploadFileMethod(request.Images.FirstOrDefault(), @"HomePage\Slider\");


                Slider slider = new Slider()
                {
                    Src = resultUpoad.FileNameAddress,
                    Link = request.Link,
                    ClickCount = 0
                };
                _context.Sliders.Add(slider);
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSeccess = true,
                    Message = "اسلاید با موفقیت ذخیره شد"
                };
            }
            catch
            {
                return new ResultDto()
                {
                    IsSeccess = true,
                    Message = "خطا در ذخیره اسلاید"
                };
            }
        }

    }
}
