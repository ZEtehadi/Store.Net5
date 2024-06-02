using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean_Architecture.Domain.Entities.DisCounts;

namespace Clean_Architecture.Application.Services.DisCounts.Commands.AddNewDisCount
{
   public interface IAddNewDisCountService
    {
        ResultDto Execute(RequestDisCountDto request);
    }
    public class AddNewDisCountService : IAddNewDisCountService
    {
        private readonly IDataBaseContext _context;
        public AddNewDisCountService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestDisCountDto request)
        {
            var discounts = _context.DisCounts
                                    .Where(p => p.DisCountCode == request.DisCountCode && 
                                     p.IsActive==true &&
                                     p.IsRemoved==false)
                                    .ToList();

            if (discounts.Count>0)
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = "این کد تخفیف قبلا ثبت شده"
                };
            }
            if (string.IsNullOrEmpty(request.DisCountCode))
            {
                return new ResultDto()
                {
                    IsSeccess=false,
                    Message="کد تخفیف را وارد کنید."
                };
            }
            if (request.Percent<= 0  )
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = "درصد تخفیف را وارد کنید"
                };
            }
        

            DisCount disCount = new DisCount();

            disCount.DisCountCode = request.DisCountCode;
            disCount.Percent = request.Percent;
            disCount.DateTime = DateTime.Now;

            _context.DisCounts.Add(disCount);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSeccess = true,
                Message = "تخفیف با موفقیت افزوده شد"
            };
        }
    }

    public class RequestDisCountDto
    {
        public string DisCountCode { get; set; }
        public float Percent { get; set; } = 0;

    }
}
