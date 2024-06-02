using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductForAdmin
{
    public class GetProductForAdminService : IGetProductForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProductForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductForAdminDto> Execute(int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;
            var Products = _context.Products
                         .Include(p => p.Category)
                         .ToPaged(Page, PageSize, out rowCount)
                         .Select(p => new ProductForAdminList_Dto()
                         {
                             Id = p.Id,
                             Name = p.Name,
                             Category = p.Category.Name,
                             Brand = p.Brand,
                             Description = p.Description,
                             Price = p.Price,
                             Inventory = p.Inventory,
                             Displayed = p.Displayed,
                         }).ToList();

            return new ResultDto<ProductForAdminDto>()
            {
                Data=new ProductForAdminDto()
                {
                    RowCount=rowCount,
                    CurrentPage=Page,
                    PageSize=PageSize,
                    Products=Products,
                },
                IsSeccess=true,
                Message=""
            };
        }

    }
}
