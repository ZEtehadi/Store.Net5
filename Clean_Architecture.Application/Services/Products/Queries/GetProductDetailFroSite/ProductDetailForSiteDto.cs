using System.Collections.Generic;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductDetailFroSite
{
    public class ProductDetailForSiteDto
    {
        public long Id { get; set; }
        public string  Title { get; set; }
        public string Brand { get; set; }
        public string  Category { get; set; }
        public string Description { get; set; }
        public int  Price { get; set; }
        public List<string> Images { get; set; }
        public List<ProductDetailForSite_FeaturesDto> Features { get; set; }
    }
}
