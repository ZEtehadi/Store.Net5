using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductForSite
{
    public class GetProductForSiteService : IGetProductForSiteService
    {
        private readonly IDataBaseContext _context;
        public GetProductForSiteService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto<ResultProductForSiteDto> Execute(Ordering ordering, int PageSize, int Paga, long? CatId, string SearchKey)
        {
            int TotalRow = 0;
            var ProductQuery = _context.Products
                 .Include(p => p.ProductImages).AsQueryable();


            if (CatId != null)
            {
                ProductQuery = ProductQuery.Where(p => p.CategoryId == CatId || p.Category.ParentCategoryId == CatId).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(SearchKey))
            {
                ProductQuery = ProductQuery.Where(p =>
                               p.Name.Contains(SearchKey) ||
                               p.Brand.Contains(SearchKey)).AsQueryable();
            }


            ///OrderByDescending => big to little
            ///OrderBy => little to big
            switch (ordering)
            {
                case Ordering.NotOrder:
                    ProductQuery = ProductQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostVisited:
                    ProductQuery = ProductQuery.OrderByDescending(p => p.ViewCount).AsQueryable();
                    break;
                case Ordering.BestSelling:///////We dont have it
                    ProductQuery = ProductQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostPopular://////
                    ProductQuery = ProductQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.TheNewest:
                    ProductQuery = ProductQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    ProductQuery = ProductQuery.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.TheMostExpensive:
                    ProductQuery = ProductQuery.OrderByDescending(p => p.Price).AsQueryable();
                    break;

            }

            var Product = ProductQuery.ToPaged(Paga, PageSize, out TotalRow);



            Random dr = new Random();
            return new ResultDto<ResultProductForSiteDto>()
            {
                Data = new ResultProductForSiteDto()
                {
                    TotolRow = TotalRow,
                    Products = Product.Select(p => new ProductForSiteDto()
                    {
                        Id = p.Id,
                        Title = p.Name,
                        Price = p.Price,
                        Star = dr.Next(1, 5),
                        ImagesSrc = p.ProductImages.FirstOrDefault().Src
                    }).ToList()
                },
                IsSeccess = true,
                Message = ""
            };
        }

    }
}
