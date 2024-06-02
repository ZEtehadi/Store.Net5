using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductDetailForAdmin
{
    public class GetProductDetailForAdminService : IGetProductDetailForAdminService
    {
        private readonly IDataBaseContext _context;

        public GetProductDetailForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductDetailForAdminDto> Execute(long Id)
        {
            var Product = _context.Products
                        .Include(p => p.Category)
                        .ThenInclude(p => p.ParentCategory)
                        .Include(p => p.ProductFeatures)
                        .Include(p => p.ProductImages)
                        .Where(p => p.Id == Id)
                        .FirstOrDefault();



            return new ResultDto<ProductDetailForAdminDto>()
            {
                Data = new ProductDetailForAdminDto()
                {
                    Id = Product.Id,
                    Name = Product.Name,
                    Category = GetCategory(Product.Category),
                    Brand=Product.Brand,
                    Description=Product.Description,
                    Price=Product.Price,
                    Inventory=Product.Inventory,
                    Displayed=Product.Displayed,
                    Features=Product.ProductFeatures.ToList().Select(P=>new ProductDetailFeatureDto()
                    {
                        Id=P.Id,
                        DisplayName=P.DisplayName,
                        Value=P.Value,
                    }).ToList(),
                    Images=Product.ProductImages.ToList().Select(p=>new ProductDetailImagesDto()
                    {
                        Id=p.Id,
                        Src=p.Src,
                    }).ToList(),
                },
                IsSeccess=true,
                Message=""
            };
        }

        private string GetCategory(Category category)
        {
            string result = category.ParentCategory != null ? $"{ category.ParentCategory.Name}-" : "";
            return result += category.Name;
        }

    }
}
