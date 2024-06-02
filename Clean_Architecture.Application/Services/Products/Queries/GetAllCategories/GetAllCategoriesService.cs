using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Architecture.Application.Services.Products.Queries.GetAllCategories
{
    public class GetAllCategoriesService : IGetAllCategoriesService
    {
        private readonly IDataBaseContext _context;
        public GetAllCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<AllCategoriesDto>> Execute()
        {
            var Categories = _context.Categories
                           .Include(p => p.ParentCategory)
                           .Where(p => p.ParentCategoryId != null)//Has Parent
                           .ToList()
                           .Select(p => new AllCategoriesDto()
                           {
                               Id = p.Id,
                               Name = $" { p.ParentCategory.Name }-{ p.Name }"
                           }).ToList();

            return new ResultDto<List<AllCategoriesDto>>()
            {
                Data=Categories,
                IsSeccess=false,
                Message=""
            };
        }


    }
}
