using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProduct_sameCatForAdminService
{
    interface IGetProduct_sameCatForAdminService
    {
        ResultDto<ProductForAdminDto> Execute(long CatId, int Page = 1,int PageSize=20);
    }
    public class GetProduct_sameCatForAdminService : IGetProduct_sameCatForAdminService
    {
        private readonly IDataBaseContext _context;
        public GetProduct_sameCatForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductForAdminDto> Execute(long CatId, int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;
            var Products = _context.Products
                                 .Include(p => p.Category)
                                 .Where(p => p.Category.ParentCategoryId == CatId)
                                 .ToPaged(Page, PageSize, out rowCount)
                                 .ToList()
                                 .Select(p => new ProductForAdminList_Dto()
                                 {
                                     Id = p.Id,
                                     Name = p.Name,
                                     Category = p.Category.Name,
                                     Brand = p.Brand,
                                     Description = p.Description,
                                     Inventory = p.Inventory,
                                     Price = p.Price,
                                     Displayed = p.Displayed,
                                 }).ToList();

            return new ResultDto<ProductForAdminDto>()
            {
                Data=new ProductForAdminDto()
                {
                    RowCount=rowCount,
                    CurrentPage=Page,
                    PageSize=PageSize,
                    Products=Products
                },
                IsSeccess=true,
                Message="sending Product by same Category was Seccessfully"
            };
        }
    }
    public class ProductForAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ProductForAdminList_Dto> Products { get; set; }
    }
    public class ProductForAdminList_Dto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Inventory { get; set; }
        public int Price { get; set; }
        public bool Displayed { get; set; }
    }
   
  
}
