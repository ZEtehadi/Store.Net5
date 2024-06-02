using Clean_Architecture.Common.Dto;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductDetailFroSite
{
    public interface IGetProductDetailForSiteService
    {
        ResultDto<ProductDetailForSiteDto> Execute(long Id);
    }
}
