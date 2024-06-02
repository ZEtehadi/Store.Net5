using Clean_Architecture.Application.Services.Finances.Commands.AddRequestPay;
using Clean_Architecture.Application.Services.Finances.Queries.GetRequestPay;
using Clean_Architecture.Application.Services.Finances.Queries.GetRequestPayForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Interface.Facadpatter.FinancesFacad
{
   public interface IFinancesFacad
    {
        AddRequestPayService AddRequestPayService { get; }
        RequestPayService RequestPayService { get; }
        GetRequestPayForAdmin GetRequestPayForAdmin { get; }
    }
}
