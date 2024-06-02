using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Finances.Queries.GetRequestPay
{
   public interface IRequestPayService
    {
        ResultDto<RequestPayDto> Execute(Guid guid);
    }

    public class RequestPayService : IRequestPayService
    {
        private readonly IDataBaseContext _context;
        public RequestPayService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<RequestPayDto> Execute(Guid guid)
        {
            var requestPay = _context.RequestPays.Where(p => p.Guid == guid).FirstOrDefault();
            if (requestPay != null)
            {
                return new ResultDto<RequestPayDto>()
                {
                    Data=new RequestPayDto()
                    {
                        Id=requestPay.Id,
                        Amount=requestPay.Amount,
                    },
                    IsSeccess=true,
                    Message="send Amount Of Request Pay"
                };
            }
            else
            {
                throw new Exception("Request Pay not Found");
            }
        }
    }

    public class RequestPayDto
    {
        public long Id { get; set; }
        public int Amount { get; set; }

    }
}
