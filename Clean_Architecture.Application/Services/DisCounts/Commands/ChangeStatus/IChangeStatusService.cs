using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.DisCounts.Commands.ChangeStatus
{
  public  interface IChangeStatusService
    {
        ResultDto Execute(long Id, bool IsActive);
    }
    public class ChangeStatusService : IChangeStatusService
    {
        private readonly IDataBaseContext _context;
        public ChangeStatusService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long Id, bool Status)
        {
            var disCount = _context.DisCounts.Where(p => p.Id == Id).FirstOrDefault();

            if (disCount == null)
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = "کد تخفیف یافت نشد"
                };
            }

            disCount.IsActive = Status;
            _context.SaveChanges();

            string StatusName;
            if (Status == false)
                StatusName = "غیرفعال";
            else
                StatusName = "فعال";
              
            return new ResultDto()
            {
                IsSeccess = true,
                Message =$"کد تخفیف با شماره {Id} ، با موفقیت   {StatusName}  شد"
            };
        }
    }
}
