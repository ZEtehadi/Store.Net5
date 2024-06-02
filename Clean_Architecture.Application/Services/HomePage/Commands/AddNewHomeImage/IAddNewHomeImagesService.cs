using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.HomePage.Commands.AddNewHomeImage
{
    public interface IAddNewHomeImagesService
    {
        ResultDto Execute(RequestAddNewHomeImagesDto request);
    }
}
