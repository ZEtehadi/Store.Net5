using Clean_Architecture.Application.Services.Products.Queries.GetProductDetailFroSite;
using Clean_Architecture.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Interface.Facadpatter
{
    public interface IProductFacadSite
    {
        IGetProductForSiteService GetProductForSiteService { get; }
        IGetProductDetailForSiteService GetProductDetailForSiteService { get; }
        
    }
}
