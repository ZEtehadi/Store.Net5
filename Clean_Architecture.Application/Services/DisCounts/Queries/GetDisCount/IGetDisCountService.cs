using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.DisCounts.Queries.GetDisCount
{
    public interface IGetDisCountService
    {
        ResultDto<List<DiscountDto>> Execute();
    }

    public class GetDisCountService : IGetDisCountService
    {
        private readonly IDataBaseContext _context;
        public GetDisCountService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<DiscountDto>> Execute()
        {
            var discount = _context.DisCounts.OrderByDescending(p => p.Id)
                                            .Where(p=>p.Id!=1 && p.IsRemoved==false)
                                           .Select(p => new DiscountDto()
                                           {
                                               Id=p.Id,
                                               DisCountCode = p.DisCountCode,
                                               Percent = p.Percent,
                                               IsActive=p.IsActive
                                           }).ToList();

            return new ResultDto<List<DiscountDto>>()
            {
                Data=discount,
                IsSeccess=true,
                Message="لیست تخفیف ها با موفقیت ارسال شد"
            };
        }
    }

    public class DiscountDto
    {
        public long Id { get; set; }
        public string DisCountCode { get; set; }
        public float Percent { get; set; } = 0;
        public bool IsActive { get; set; }
    }
}
