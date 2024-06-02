
namespace Clean_Architecture.Application.Services.Products.Queries.GetProductForSite
{
    public class ProductForSiteDto
    {
        public long  Id { get; set; }
        public string Title { get; set; }
        public string ImagesSrc { get; set; }
        public int Price { get; set; }
        public int Star { get; set; }
    }
}
