using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Common.Queries.GetHomePageImages
{
   public interface IGetHomePageImagesService
    {
        ResultDto<List<HomePageImagesDto>> Execute();
    }

    public class GetHomePageImagesService : IGetHomePageImagesService
    {
        private readonly IDataBaseContext _context;
        public GetHomePageImagesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<HomePageImagesDto>> Execute()
        {
            var Images = _context.HomePageImages
                        .OrderByDescending(p => p.Id)
                        .Select(p => new HomePageImagesDto()
                        {
                            Id = p.Id,
                            Src = p.Src,
                            Link = p.Link,
                            ImageLocation = p.ImageLocation
                        }).ToList();

             return new ResultDto<List<HomePageImagesDto>>()
             {
                 Data=Images,
                 IsSeccess=true,
                 Message=""
             }; 
        }
    }

    public class HomePageImagesDto
    {
        public long Id { get; set; }
        public string Link { get; set; }
        public string Src { get; set; }
        public ImageLocation ImageLocation{ get; set; }

    }
}
