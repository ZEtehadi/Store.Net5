using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Common.UploadFile;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Commands.EditCategory
{
    public interface IEditCategoryService
    {
        ResultDto Execute(RequestEditCategoryDto request);
    }

    public class EditCategoryService : IEditCategoryService
    {
        private readonly IDataBaseContext _context;
        private readonly UploadFilee _uploadFilee;


        public EditCategoryService(IDataBaseContext context, UploadFilee uploadFilee)
        {
            _context = context;
            _uploadFilee = uploadFilee;
        }


        public ResultDto Execute(RequestEditCategoryDto request)
        {
            var Category = _context.Categories.Find(request.Id);
            if (Category == null)
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = "دسته بندی یافت نشد"
                };
            }


            if (request.ImageSrc != null)
            {
                var UploadResult = _uploadFilee.UploadFileMethod(request.ImageSrc, @"Categories\");
                Category.Src = UploadResult.FileNameAddress;

            }

            Category.Name = request.Name;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSeccess = true,
                Message = "دسته بندی ویرایش شد"
            };
        }
    }

    public class RequestEditCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImageSrc { get; set; }
    }
}
