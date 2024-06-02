using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Interface.Facadpatter.FinancesFacad;
using Clean_Architecture.Application.Services.Finances.Commands.AddRequestPay;
using Clean_Architecture.Application.Services.Finances.Queries.GetRequestPay;
using Clean_Architecture.Application.Services.Finances.Queries.GetRequestPayForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Finances.Facad.FinancesFacad
{
   public class FinancesFacad:IFinancesFacad
    {
        private readonly IDataBaseContext _context;

        public FinancesFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private AddRequestPayService _addRequestPayService;
        public AddRequestPayService AddRequestPayService
        {
            get
            {
                return _addRequestPayService = _addRequestPayService ?? new AddRequestPayService(_context);
            }
        }


        private RequestPayService _requestPayService;
        public RequestPayService RequestPayService
        {
            get
            {
                return _requestPayService=_requestPayService?? new RequestPayService(_context);
            }
        }
        
        
        private GetRequestPayForAdmin _getRequestPayForAdmin;
        public GetRequestPayForAdmin GetRequestPayForAdmin
        {
            get
            {
                return _getRequestPayForAdmin = _getRequestPayForAdmin?? new GetRequestPayForAdmin(_context);
            }
        }
    }
}
