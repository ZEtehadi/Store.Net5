using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Architecture.Application.Services.Common.Queries.GetCategory
{
    public class GetCategoryService : IGetCategoryService
    {
       private readonly IDataBaseContext _context;
        public GetCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CategoryDto>> Execute()
        {
            var Query = _context.Categories
                      .Where(p => p.ParentCategoryId == null)
                      .ToList()
                      .Select(p => new CategoryDto()
                      {
                          CatId = p.Id,
                          CategoryName = p.Name
                      }).ToList();

            return new ResultDto<List<CategoryDto>>()
            {
                Data=Query,
                IsSeccess=true,
                Message=""
            };
        }

    }
}
