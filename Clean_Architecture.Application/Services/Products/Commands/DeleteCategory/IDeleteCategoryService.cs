using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Commands.DeleteCategory
{
    public interface IDeleteCategoryService
    {
        ResultDto Execute(long Id);

    }
}
