using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductDetailFroSite
{
    public class GetProductDetailForSiteService : IGetProductDetailForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailForSiteService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto<ProductDetailForSiteDto> Execute(long Id)
        {
            var Product = _context.Products
                        .Include(p => p.ProductFeatures)
                        .Include(p => p.ProductImages)
                        .Include(p => p.Category)
                        .ThenInclude(p => p.ParentCategory)
                        .Where(p => p.Id == Id)
                        .FirstOrDefault();

            Product.ViewCount++;
            _context.SaveChanges();


            if (Product == null)
            {
                throw new Exception("Product Not Found ...");
            }

            return new ResultDto<ProductDetailForSiteDto>()
            {
                Data=new ProductDetailForSiteDto()
                {
                    Id=Product.Id,
                    Title=Product.Name,
                    Brand=Product.Brand,
                    Category =$"{Product.Category.ParentCategory.Name}-{Product.Category.Name}",
                    Description=Product.Description,
                   Price=Product.Price,
                   Images=Product.ProductImages.Select(p=>p.Src).ToList(),
                   Features=Product.ProductFeatures.Select(p=>new ProductDetailForSite_FeaturesDto()
                   {
                       DisplayName=p.DisplayName,
                       Value=p.Value
                   }).ToList(),
                },
                IsSeccess=true,
                Message=""
            };
        }


    }
}
