using Clean_Architecture.Common.Dto;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductForAdminDto> Execute(int Page = 1,int PageSize=20);
    }
}
