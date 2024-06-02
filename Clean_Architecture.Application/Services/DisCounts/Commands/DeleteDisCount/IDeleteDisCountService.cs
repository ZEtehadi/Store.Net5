using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.DisCounts.Commands.DeleteDisCount
{
   public interface IDeleteDisCountService
    {
        ResultDto Execute(long Id);
    }

    public class DeleteDisCountService : IDeleteDisCountService
    {
        private readonly IDataBaseContext _context;
        public DeleteDisCountService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long Id)
        {
            var disCount = _context.DisCounts.Where(P => P.Id == Id).FirstOrDefault();

            if (disCount == null)
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = "کد تخفیف یافت نشد"
                };
            }

            disCount.IsRemoved = true;
            disCount.RemovedTime = DateTime.Now;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSeccess = true,
                Message =$"کد تخفیف با  شماره {Id}  با موفقیت حذف شد."
            };
        }
    }
}
