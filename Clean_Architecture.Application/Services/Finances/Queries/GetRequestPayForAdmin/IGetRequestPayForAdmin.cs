using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean_Architecture.Common;

namespace Clean_Architecture.Application.Services.Finances.Queries.GetRequestPayForAdmin
{
    public interface IGetRequestPayForAdmin
    {
        ResultDto<ResultRequstPayDto> Execute(RequestDto request);
    }

    public class GetRequestPayForAdmin : IGetRequestPayForAdmin
    {
        private readonly IDataBaseContext _context;
        public GetRequestPayForAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultRequstPayDto> Execute(RequestDto request)
        {
            var requestPay = _context.RequestPays.Include(p => p.User).ToList();
             
            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                requestPay = requestPay.Where(p => p.Userid.ToString() == request.SearchKey ||
                                              p.User.FullName.Contains(request.SearchKey) ||
                                              p.Id.ToString()== request.SearchKey ||
                                              p.RefId.ToString()== request.SearchKey ||
                                              p.DateTime.Date.ToString().Contains(request.SearchKey)).ToList();
            }

            int rowsCount = 0;
            var requestList = requestPay.ToPaged(request.Page, 20, out rowsCount).Select(p => new PayDetailDto()
            {
                Id = p.Id,
                Guid = p.Guid,
                UserName = p.User.FullName,
                Userid = p.Userid,
                Amount = p.Amount,
                IsPay = p.IsPay,
                Authority = p.Authority,
                RefId = p.RefId,
                PayDate=p.DateTime,
            }).ToList();
       
            return new ResultDto<ResultRequstPayDto>()
            {
               Data=new ResultRequstPayDto()
               {
                   PayDetialDto=requestList,
                   Rows=rowsCount,
               },
               IsSeccess=true,
               Message="send Request Pay was Seccessfully"
            };
        }
    }

    public class PayDetailDto
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long? Userid { get; set; }
        public string UserName { get; set; }
        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
    }

    public class ResultRequstPayDto
    {
        public List<PayDetailDto> PayDetialDto { get; set; }
        public int Rows { get; set; }
    }

    public class RequestDto
    {
        public string SearchKey { get; set; }
        public int Page { get; set; }
    }
}
