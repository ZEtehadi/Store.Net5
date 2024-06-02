using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.HomePage.Queries.GetAllSlider
{
   public interface IGetAllSliderService
    {
        ResultDto<List<ResultAllSliderDto>> Execute();
    }

    public class GetAllSliderService : IGetAllSliderService
    {
        private readonly IDataBaseContext _context;
        public GetAllSliderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultAllSliderDto>> Execute()
        {
            var Sliders = _context.Sliders
                        .ToList()
                        .Select(p => new ResultAllSliderDto()
                        {
                            Src = p.Src,
                            Link = p.Link
                        }).ToList();

            return new ResultDto<List<ResultAllSliderDto>>()
            {
                Data=Sliders,
                IsSeccess=true,
                Message=""
            };
        }
    }

    public class ResultAllSliderDto
    {
        public string Src { get; set; }
        public string Link { get; set; }
    }
}
