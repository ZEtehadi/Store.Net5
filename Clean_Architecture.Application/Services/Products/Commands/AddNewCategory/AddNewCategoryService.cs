using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Common.UploadFile;
using Clean_Architecture.Domain.Entities.Products;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Clean_Architecture.Application.Services.Products.Commands.AddNewCategory
{
    public class AddNewCategoryService : IAddNewCategoryService
    {
        private readonly IDataBaseContext _context;
        private readonly UploadFilee _uploadFilee;


        public AddNewCategoryService(IDataBaseContext context, UploadFilee uploadFilee)
        {
            _context = context;
            _uploadFilee = uploadFilee;
        }

        public ResultDto Execute(long? parentId, string Name, IFormFile Src)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = "نام دسته بندی را وارد کنید"
                };
            }

            //Select => false true   Where=> Row
            var SameCategory = _context.Categories
                             .Where(p => p.Name == Name && p.ParentCategoryId == parentId)
                             .ToList();

            if (SameCategory.Count > 0)
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = $"-{SameCategory.Count}- دسته بندی با نام -{SameCategory.FirstOrDefault().Name}- قبلا ذخیره شده"
                };
            }

            string SrcImage;
            if (Src != null)
            {
               var UploadResult = _uploadFilee.UploadFileMethod(Src, @"Categories\");
                SrcImage = UploadResult.FileNameAddress;
            }
            else
            {
                SrcImage = @"mages\Categories\Temp.jpg";
            }
            Category category = new Category()
            {
                Src = SrcImage,
                Name = Name,
                ParentCategory = GetParent(parentId)
            };
            _context.Categories.Add(category);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSeccess = true,
                Message = "دسته بندی با موفقیت اضافه شد"
            };
        }

        private Category GetParent(long? ParentId)
        {
            return _context.Categories.Find(ParentId);
        }
    }


}
