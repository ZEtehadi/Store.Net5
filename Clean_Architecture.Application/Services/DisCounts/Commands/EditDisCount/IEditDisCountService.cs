using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.DisCounts.Commands.EditDisCount
{
  public  interface IEditDisCountService
    {
        ResultDto Execute(RequestDiscountDto request);
    }
    public class EditDisCountService : IEditDisCountService
    {
        private readonly IDataBaseContext _context;
        public EditDisCountService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestDiscountDto request)
        {
            var disCount = _context.DisCounts.Where(p => p.Id == request.Id).FirstOrDefault();

            if (disCount == null)
            {
                return new ResultDto()
                {
                    IsSeccess=false,
                    Message="کد تخفیف یافت نشد"
                };
            }

            disCount.DisCountCode = request.DisCountCode;
            disCount.Percent = request.Percent;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSeccess = true,
                Message = $"کد تخفیف با شماره {disCount.Id} ،با موفقیت ویرایش شد"
            };
        }
    }

    public class RequestDiscountDto
    {
        public long Id { get; set; }
        public string DisCountCode { get; set; }
        public float Percent { get; set; }
    }
}
