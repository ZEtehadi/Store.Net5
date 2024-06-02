using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Architecture.Application.Services.Common.Queries.GetSlider
{
    public class GetSliderService : IGetSliderService
    {
        private readonly IDataBaseContext _context;
        public GetSliderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<SliderDto>> Execute()
        {
            var Sliders = _context.Sliders.OrderByDescending(p => p.Id)
                        .ToList()
                        .Select(p => new SliderDto()
                        {
                            Src = p.Src,
                            Link = p.Link,
                            ClickCount = p.ClickCount
                        }).ToList();

            return new ResultDto<List<SliderDto>>()
            {
                Data=Sliders,
                IsSeccess=true,
                Message=""
            };
        }
             
    }
}
