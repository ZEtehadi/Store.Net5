using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Application.Interface.Facadpatter.FinancesFacad;
using Clean_Architecture.Application.Interface.Facadpatter.OrdersFacad;
using Clean_Architecture.Application.Services.Carts;
using Clean_Architecture.Application.Services.Orders.Commands.AddNewOrder;
using Dto.Payment;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarinPal.Class;

namespace EndPoint.Site.Controllers
{
    [Authorize]//only those who have the role of Customer can enter
    public class PayController : Controller
    {
        private readonly IFinancesFacad _financesFacad;
        private readonly ICartService _cartService;
        private readonly IDataBaseContext _context;
        private readonly IOrderFacad _orderFacad;
        private readonly CookiesManager _cookiesManager = new CookiesManager();

        //ZarinPal
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        public PayController(IDataBaseContext context,IFinancesFacad financesFacad,ICartService cartService,IOrderFacad orderFacad)
        {
            _context = context;
            _cartService = cartService;
            _financesFacad = financesFacad;
            _orderFacad = orderFacad;

            //ZarinPal
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
        }

        public async Task<IActionResult> Index(string DisCountCode)
        {
            try
            {

                long? UserId = ClaimUtility.GetUserId(User);
                var Cart = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), UserId);
                if (Cart.Data.SumAmount > 0)
                {
                    var requestPay = _financesFacad.AddRequestPayService.Execute(Cart.Data.SumAmount, UserId.Value, DisCountCode);

                    //ZarinPal
                    var result = await _payment.Request(new DtoRequest()
                    {
                        Mobile = "09027268973",
                        CallbackUrl = $"https://localhost:44355/Pay/Verify?guid={requestPay.Data.guid}",//send guid because 
                                                                                                        //guid is UnSpesified and Id in Specified
                        Description = $"This Pay for Pay ID:{requestPay.Data.RequestPayId}",
                        Email = requestPay.Data.Email,
                        Amount = requestPay.Data.Amount,
                        MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                    }, ZarinPal.Class.Payment.Mode.sandbox);//SandBox-->ZarinPal Test
                    return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");


                }
                else
                {
                    return RedirectToAction("index", "Cart");
                }
            }
            catch(Exception ex)
            {
                //TempData["ErrorSentence"] = "ارتباط با درگاه پرداخت با خطا رو به رو شد _ وضعیت اینترنت خود را بررسی کنید";
                TempData["ErrorSentence"] = ex.ToString();
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> Verify(Guid guid,string authority)
        {
            var requestPay = _financesFacad.RequestPayService.Execute(guid);

            var Verification = await _payment.Verification(new DtoVerification()
            {
                Amount = requestPay.Data.Amount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority,
            }, Payment.Mode.sandbox);


            long? userId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), userId);
            if (Verification.Status == 100)//Pay is Seccessfull
            {
                _orderFacad.AddNewOrderService.Execute(new RequestAddNewOrderServiceDto()
                {
                    UserId = userId.Value,
                    CartId = cart.Data.CartId,
                    RequestPayId = requestPay.Data.Id,
                    Authority=authority,
                    RefId=Verification.RefId,
                });
                return RedirectToAction("index", "Order");
            }
            else
            {
                //pay is Faild
                TempData["ErrorSentence"] = "ارتباط با درگاه پرداخت با خطا رو به رو شد _ دوباره تلاش کنید";
                return RedirectToAction("index", "cart");
            }
        }
        public IActionResult Error()
        {
            return View();
        }
    }


   
}
