using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Common.UploadFile;
using Clean_Architecture.Domain.Entities.HomePage;

namespace Clean_Architecture.Application.Services.HomePage.Commands.AddNewHomeImage
{
    public class AddNewHomeImagesService : IAddNewHomeImagesService
    {
        private readonly IDataBaseContext _context;
        private readonly UploadFilee _uploadFile;
        public AddNewHomeImagesService(IDataBaseContext context,UploadFilee uploadFilee)
        {
            _context = context;
            _uploadFile = uploadFilee;
        }

        public ResultDto Execute(RequestAddNewHomeImagesDto request)
        {
            var resultUpload = _uploadFile.UploadFileMethod(request.File, @"HomePage\HomeImages\");
            HomePageImages homePageImages = new HomePageImages()
            {
                Link=request.Link,
                Src=resultUpload.FileNameAddress,
                ImageLocation=request.ImageLocation
            };

            _context.HomePageImages.Add(homePageImages);
            _context.SaveChanges();
             
            return new ResultDto()
            {
                IsSeccess=true,
                Message="اسلاید با موفقیت افزوده شد"
            };
        }
    }
}
