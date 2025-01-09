using Clean_Architecture.Application.Services.Payments;
using Dto.Payment;
using EndPoint.Site.Models.ViewModels.Dto;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZarinPal.Class;
using ZarinpalSandbox;
using ZarinpalSandbox.Models;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class PayController : Controller
    {

        private readonly ZarinPal.Class.Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;

        private readonly ZarinpalSandbox.Payment __sandbox;

        private readonly IConfiguration configuration;
        private readonly IPaymentService paymentService;
        private readonly string merchendId;

        public PayController(IConfiguration configuration, IPaymentService paymentService)
        {
            this.configuration = configuration;
            this.paymentService = paymentService;
            merchendId = configuration["ZarinpalMerchendId"];


            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();

        }


        public async Task<IActionResult> Index(Guid PaymentId)
        {


            ////

            var payment = paymentService.GetPayment(PaymentId);
            if (payment == null)
            {
                return NotFound();
            }
            string userId = ClaimUtility.GetUserId(User).ToString();
            if (userId != payment.Userid)
            {
                return BadRequest();
            }
            string callbackUrl = Url.Action(nameof(Verify), "pay", new { payment.Id }, protocol: Request.Scheme);

            //
            //var x = await __sandbox.PaymentRequest(payment.Description, callbackUrl);
            
            PaymentRequestResponse result;
            using (HttpClient httpClient = new HttpClient())
            {
                string content = JsonConvert.SerializeObject(new
                {
                    MerchantID = "c632f574-bd37-15e7-99ca-000c295eb9d3",
                    Amount = payment.Amount,
                    Description = payment.Description,
                    Email = payment.Email,
                    Mobile = payment.PhoneNumber,
                    CallbackURL = callbackUrl
                });
                using HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("https://sandbox.zarinpal.com/pg/rest/WebGate/PaymentRequest.json", new StringContent(content, Encoding.UTF8, "application/json 'content-type: application/json'"));
                result = JsonConvert.DeserializeObject<PaymentRequestResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
            }
            //
            //var resultZarinpalRequest = await _payment.Request(new DtoRequest()
            //{
            //    Amount = payment.Amount,
            //    CallbackUrl = callbackUrl,
            //    Description = payment.Description,
            //    Email = payment.Email,
            //    MerchantId = merchendId,
            //    Mobile = payment.PhoneNumber,
            //}, ZarinPal.Class.Payment.Mode.sandbox
            //    );

            //return Redirect($"https://zarinpal.com/pg/StartPay/{resultZarinpalRequest.Authority}");
            Dictionary<string,string> valueResult= new Dictionary<string,string>();
            //valueResult = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(resultZarinpalRequest);
           //return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{resultZarinpalRequest.Authority}");
           return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");

        }

        public IActionResult Verify(Guid Id, string Authority)
        {

            string Status = HttpContext.Request.Query["Status"];

            if (Status != "" && Status.ToString().ToLower() == "ok"
                && Authority != "")
            {
                var payment = paymentService.GetPayment(Id);
                if (payment == null)
                {
                    return NotFound();
                }

                //var verification = _payment.Verification(new DtoVerification
                //{
                //    Amount = payment.Amount,
                //    Authority = Authority,
                //    MerchantId = merchendId,
                //}, Payment.Mode.zarinpal).Result;

                //var client = new RestClient("https://www.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
                var client = new RestClient("https://sandbox.zarinpal.com/pg/v4/payment/verify.json");
                // client.Timout = -1;
                var request = new RestRequest("Method.Post");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", $"{{\"MerchantID\" :\"{merchendId}\",\"Authority\":\"{Authority}\",\"Amount\":\"{payment.Amount}\"}}", ParameterType.RequestBody);
                var response = client.Execute(request);

                VerificationPayResultDto verification =
                    JsonConvert.DeserializeObject<VerificationPayResultDto>(response.Content);

                if (verification.Status == 100)
                {
                    bool verifyResult = paymentService.VerifyPayment(Id, Authority, verification.RefID);
                    if (verifyResult)
                    {
                        return Redirect("/customers/orders");
                    }
                    else
                    {
                        TempData["message"] = "پرداخت انجام شد اما ثبت نشد";
                        return RedirectToAction("checkout", "basket");
                    }
                }
                else
                {
                    TempData["message"] = "پرداخت شما ناموفق بوده است . لطفا مجددا تلاش نمایید یا در صورت بروز مشکل با مدیریت سایت تماس بگیرید .";
                    return RedirectToAction("checkout", "basket");
                }

            }
            TempData["message"] = "پرداخت شما ناموفق بوده است .";
            return RedirectToAction("checkout", "basket");
        }
    }


    public class VerificationPayResultDto
    {
        public int Status { get; set; }
        public long RefID { get; set; }
    }




    // [Authorize]//only those who have the role of Customer can enter
    //public class PayController : Controller
    //{
    //    private readonly IFinancesFacad _financesFacad;
    //    private readonly ICartService _cartService;
    //    private readonly IDataBaseContext _context;
    //    private readonly IOrderFacad _orderFacad;
    //    private readonly CookiesManager _cookiesManager = new CookiesManager();

    //    //ZarinPal
    //    private readonly Payment _payment;
    //    private readonly Authority _authority;
    //    private readonly Transactions _transactions;
    //    string authority;
    //    public PayController(IDataBaseContext context,IFinancesFacad financesFacad,ICartService cartService,IOrderFacad orderFacad)
    //    {
    //        _context = context;
    //        _cartService = cartService;
    //        _financesFacad = financesFacad;
    //        _orderFacad = orderFacad;

    //        //ZarinPal
    //        var expose = new Expose();
    //        _payment = expose.CreatePayment();
    //        _authority = expose.CreateAuthority();
    //        _transactions = expose.CreateTransactions();
    //    }

    //    public async Task<IActionResult> Index(string DisCountCode)
    //    {
    //        string PriceToPay = DisCountCode.Replace(",", "");

    //        try
    //        {

    //            long? UserId = ClaimUtility.GetUserId(User);
    //            var Cart = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), UserId);
    //            if (Cart.Data.SumAmount > 0)
    //            {
    //                var requestPay = _financesFacad.AddRequestPayService.Execute(Cart.Data.SumAmount, UserId.Value, PriceToPay);

    //              //var x=  new DtoRequest()
    //              //  {
    //              //      Mobile = "09027268973",
    //              //      CallbackUrl = $"https://localhost:44355/Pay/Verify?guid={requestPay.Data.guid}",//send guid because 
    //              //                                                                                      //guid is UnSpesified and Id in Specified
    //              //      Description = $"This Pay for Pay ID:{requestPay.Data.RequestPayId}",
    //              //      Email = requestPay.Data.Email,
    //              //      Amount = requestPay.Data.Amount,
    //              //      MerchantId = "cfa83c81-89b0-4993-9445-2c3fcd323455",//cfa83c81-89b0-4993-9445-2c3fcd323455  //XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX
    //              //  };
    //              //   var y=JsonConvert.SerializeObject(x);
    //              //  //ZarinPal
    //              //  var result = await _payment.Request(x, ZarinPal.Class.Payment.Mode.sandbox);//SandBox-->ZarinPal Test
    //              //  return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");

    //                //mhnds babie
    //                //var result = await _payment.Request(new DtoRequest()
    //                //{
    //                //    Mobile = "09121112222",
    //                //    CallbackUrl = $"https://localhost:44339/Pay/Verify?guid={requestPay.Data.guid}",
    //                //    Description = "پرداخت فاکتور شماره:" + requestPay.Data.RequestPayId,
    //                //    Email = requestPay.Data.Email,
    //                //    Amount = requestPay.Data.Amount,
    //                //    MerchantId = "cfa83c81-89b0-4993-9445-2c3fcd323455"
    //                //}, ZarinPal.Class.Payment.Mode.sandbox);
    //                //return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");


    //                ///
    //                /// 
    //                /////////////////////source
    //                var Parameters = new DtoRequest()
    //                {
    //                    Mobile = "09027268973",
    //                    CallbackUrl = $"https://localhost:44355/Pay/Verify?guid={requestPay.Data.guid}",//send guid because 
    //                                                                                                    //guid is UnSpesified and Id in Specified
    //                    Description = $"This Pay for Pay ID:{requestPay.Data.RequestPayId}",
    //                    Email = requestPay.Data.Email,
    //                    Amount = requestPay.Data.Amount,
    //                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
    //                };
    //                //RequestParameters Parameters = new RequestParameters(merchant, amount, description, callbackurl, "", "");

    //                //be dalil in ke metadata be sorate araye ast va do meghdare mobile va email dar metadata gharar mmigirad
    //                //shoma mitavanid in maghadir ra az kharidar begirid va set konid dar gheir in sorat khali ersal konid

    //                var client = new RestClient(URLs.requestUrl);
    //                Method method = Method.Post;
    //                var request = new RestRequest("", method);
    //                request.AddHeader("accept", "application/json");
    //                request.AddHeader("content-type", "application/json");
    //                request.AddJsonBody(Parameters);
    //                var requestresponse = client.ExecuteAsync(request);
    //                JObject jo = JObject.Parse(requestresponse.Result.Content);
    //                string errorscode = jo["errors"].ToString();
    //                JObject jodata = JObject.Parse(requestresponse.Result.Content);
    //                string dataauth = jodata["data"].ToString();

    //                if (dataauth != "[]")
    //                {
    //                    authority = jodata["data"]["authority"].ToString();
    //                    string gatewayUrl = URLs.gateWayUrl + authority;
    //                    return Redirect(gatewayUrl);
    //                }
    //                else
    //                {
    //                    return BadRequest("error " + errorscode);
    //                }

    //            }
    //            else
    //            {
    //                return RedirectToAction("index", "Cart");
    //            }
    //        }
    //        catch(Exception ex)
    //        {
    //            //TempData["ErrorSentence"] = "ارتباط با درگاه پرداخت با خطا رو به رو شد _ وضعیت اینترنت خود را بررسی کنید";
    //            TempData["ErrorSentence"] = ex.ToString();
    //            return RedirectToAction("Error");
    //        }
    //    }

    //    public async Task<IActionResult> Verify(Guid guid,string authority)
    //    {
    //        var requestPay = _financesFacad.RequestPayService.Execute(guid);

    //        var Verification = await _payment.Verification(new DtoVerification()
    //        {
    //            Amount = requestPay.Data.Amount,
    //            MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
    //            Authority = authority,
    //        }, Payment.Mode.sandbox);


    //        long? userId = ClaimUtility.GetUserId(User);
    //        var cart = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), userId);
    //        if (Verification.Status == 100)//Pay is Seccessfull
    //        {
    //            _orderFacad.AddNewOrderService.Execute(new RequestAddNewOrderServiceDto()
    //            {
    //                UserId = userId.Value,
    //                CartId = cart.Data.CartId,
    //                RequestPayId = requestPay.Data.Id,
    //                Authority=authority,
    //                RefId=Verification.RefId,
    //            });
    //            return RedirectToAction("index", "Order");
    //        }
    //        else
    //        {
    //            //pay is Faild
    //            TempData["ErrorSentence"] = "ارتباط با درگاه پرداخت با خطا رو به رو شد _ دوباره تلاش کنید";
    //            return RedirectToAction("index", "cart");
    //        }
    //    }
    //    public IActionResult Error()
    //    {
    //        return View();
    //    }
    //}



}
