using Clean_Architecture.Common.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Commands.EditProduct
{
 public   interface IEditProductService
    {
        ResultDto Execute();
    }

    public class RequestProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public long CategoryId { get; set; }

        public List<IFormFile> ProductImages { get; set; }
        public List<string> ProductFeatures { get; set; }
    }
}
