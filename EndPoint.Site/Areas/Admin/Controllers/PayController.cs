using Clean_Architecture.Application.Interface.Facadpatter.FinancesFacad;
using Clean_Architecture.Application.Services.Finances.Queries.GetRequestPayForAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class PayController : Controller
    {

        private readonly IFinancesFacad _financesFacad;
        public PayController(IFinancesFacad financesFacad)
        {
            _financesFacad = financesFacad;
        }

        public IActionResult Index(string SearchKey, int page = 1)
        {
            var requestPay = _financesFacad.GetRequestPayForAdmin.Execute(new RequestDto()
            {
                SearchKey = SearchKey,
                Page = page,
            }).Data;

            return View(requestPay);
        }
    }
}
