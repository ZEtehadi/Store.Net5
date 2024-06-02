using System.Collections.Generic;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductForSite
{
    public class ResultProductForSiteDto
    {
        public List<ProductForSiteDto> Products { get; set; }
        public int TotolRow { get; set; }
    }
}
