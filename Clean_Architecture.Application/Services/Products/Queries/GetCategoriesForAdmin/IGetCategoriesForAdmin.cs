using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Queries.GetCategoriesForAdmin
{
    public interface IGetCategoriesForAdmin
    {
        ResultDto<List<ResultCategoriesDto>> Execute();
    }

    public class GetCategoriesForAdmin : IGetCategoriesForAdmin
    {
        private readonly IDataBaseContext _context;
        public GetCategoriesForAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultCategoriesDto>> Execute()
        {
            var categoreis = _context.Categories
                                    .Include(p => p.Products)
                                    .ThenInclude(p => p.ProductImages)
                                   .Where(p => p.ParentCategoryId == null)
                                   .ToList()
                                   .Select(p => new ResultCategoriesDto()
                                   {
                                       Id = p.Id,
                                       Name = p.Name,
                                       ImageSrc=p.Src,
                                   }).ToList();


            return new ResultDto<List<ResultCategoriesDto>>()
            {
                Data = categoreis,
                IsSeccess = true,
                Message = "sending Categories was Seccessfully"
            };
        }
    }

    public class ResultCategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
    }
}
