using Clean_Architecture.Common.Dto;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductForSite
{
    public interface IGetProductForSiteService
    {
        ResultDto<ResultProductForSiteDto> Execute(Ordering ordering,int PageSize,int Page,long? CatId,string SearchKey);
    }

    public enum Ordering
    {
        NotOrder=0,
        MostVisited=1,
        BestSelling=2,
        MostPopular=3,
        TheNewest=4,
        Cheapest=5,
        TheMostExpensive=6
    }


}
