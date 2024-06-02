using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Architecture.Application.Services.Products.Queries.GetCategories
{
    public class GetCategorisService : IGetCategoriesService
    {
        private readonly IDataBaseContext _context;
        public GetCategorisService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CategoriesDto>> Execute(long? ParentId)
        {
            //string ImageAddress = "";
            //if (Photo.startsWith("imagesCategories"))
            //{
            //    Photo = Photo.substring(15);
            //}

            var Categories = _context.Categories
                           .Include(p => p.ParentCategory)
                           .Include(p => p.SubCategories)
                           .Where(p => p.ParentCategoryId == ParentId).ToList()
                           .Select(p => new CategoriesDto()
                           {
                               Id = p.Id,
                               Name = p.Name,
                               IsActive=p.IsActive,
                               ImageSrc=(p.Src).Substring(18),
                               Parent = p.ParentCategory != null ?
                               new ParentCategoryDto()
                               {
                                   Id = p.ParentCategory.Id,
                                   Name = p.ParentCategory.Name
                               } : null,
                               HasChild = p.SubCategories.Count() > 0 ? true : false
                           }).ToList();

            return new ResultDto<List<CategoriesDto>>()
            {
                Data=Categories,
                IsSeccess=true,
                Message="لیست با موفقیت برگردانده شد"
            };
        }
    }
}
