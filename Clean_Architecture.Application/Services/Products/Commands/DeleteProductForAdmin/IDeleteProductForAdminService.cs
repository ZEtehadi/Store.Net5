using Clean_Architecture.Common.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Commands.DeleteProductForAdmin
{
    public interface IDeleteProductForAdminService
    {
        ResultDto Execute(long Id);

    }
}
