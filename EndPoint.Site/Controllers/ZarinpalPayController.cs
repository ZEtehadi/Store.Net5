using Clean_Architecture.Application.Interface.Facadpatter.FinancesFacad;
using Clean_Architecture.Application.Interface.Facadpatter.OrdersFacad;
using Clean_Architecture.Application.Services.Carts;
using Clean_Architecture.Application.Services.Orders.Commands.AddNewOrder;
using Clean_Architecture.Application.Services.Payments;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class ZarinpalPayController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly ICartService _cartService;
        private readonly IOrderFacad _orderFacad;
        private readonly CookiesManager _cookiesManager = new CookiesManager();
        private readonly IFinancesFacad _financesFacad;
        public ZarinpalPayController(
            IPaymentService paymentService,
            HttpClient httpClient,
            ICartService cartService,
            IOrderFacad orderFacad,
            IFinancesFacad financesFacad)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _cartService = cartService;
            _financesFacad = financesFacad;
            _orderFacad = orderFacad;
        }
        [HttpPost]
        public async Task<IActionResult> Payment(int amount)
        {

            long? UserId = ClaimUtility.GetUserId(User);
            var Cart = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), UserId);
            if (Cart.Data.SumAmount > 0)
            {
                var requestPay = _financesFacad.AddRequestPayService.Execute(Cart.Data.SumAmount, UserId.Value, amount.ToString());


                var zarinpalUrl = "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
                var merchantId = "00000000-0000-0000-0000-000000000000";
                string callbackUrl = Url.Action(nameof(VerifyPayment), "ZarinpalPay", new { guid = requestPay.Data.guid }, Request.Scheme);
                //

                var paymentRequest = new ZarinpalRequest
                {
                    Merchant_id = merchantId,
                    Amount = amount,
                    Callback_url = callbackUrl,
                    Description =$"خرید محصولات فروشگاه اتحاد - {DateTime.Now}",
                    Email = requestPay.Data.Email,
                    Mobile = requestPay.Data.Mobile,
                };

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsJsonAsync(zarinpalUrl, paymentRequest);
                var responseContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonConvert.DeserializeObject<ZarinpalResponse>(responseContent);

                if (result.Data != null && result.Data.Code == 100)
                {
                    var gatewayUrl = $"https://sandbox.zarinpal.com/pg/StartPay/{result.Data.Authority}";
                    return Redirect(gatewayUrl);
                }
                return Content("خطا در اتصال به درگاه پرداخت");

            }
            else
            {
                ViewBag.ErrorToPay = "سبد خرید دارای هیچ محصولی نیست";
                return View();
            }

        }


        [HttpGet]
        public async Task<IActionResult> VerifyPayment(Guid guid, string authority)
        {// int amount

            var requestPay = _financesFacad.RequestPayService.Execute(guid);

            var merchantId = "00000000-0000-0000-0000-000000000000";
            var verifyRequest = new ZarinpalVerifyRequest
            {
                merchant_id = merchantId,
                amount = requestPay.Data.Amount,
                authority = authority
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(verifyRequest), Encoding.UTF8, "application/json");
            var jsonRequest = JsonConvert.SerializeObject(verifyRequest);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var zarinpalVerifyUrl = "https://sandbox.zarinpal.com/pg/v4/payment/verify.json";
            var response = await _httpClient.PostAsync(zarinpalVerifyUrl, jsonContent);



            //Get result
            try
            {
                var result = await response.Content.ReadFromJsonAsync<ZarinpalVerifyResponse>();

                if (result != null && result.data.code == 100)
                {

                    long? userId = ClaimUtility.GetUserId(User);
                    var cart = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), userId);
                    var data= new RequestAddNewOrderServiceDto()
                    {
                        UserId = userId.Value,
                        CartId = cart.Data.CartId,
                        RequestPayId = requestPay.Data.Id,
                        Authority = authority,
                        RefId = result.data.ref_id,
                    };

                    var resultPay = _orderFacad.AddNewOrderService.Execute(data);

                    return RedirectToAction("index", "Order");

                    return Content($"پرداخت موفق! شماره پیگیری: {result.data.ref_id}");
                }
            }
            catch
            {
                return Content("پرداخت ناموفق");

            }






            return Content("پرداخت ناموفق");


        }

    }
    public class ZarinpalRequest
    {
        public string Merchant_id { get; set; }
        public int Amount { get; set; }
        public string Callback_url { get; set; }
        public string Description { get; set; }
        public string Email { get; set; } = "";
        public string Mobile { get; set; } = "";
    }

    public class ZarinpalResponse
    {
        public DataResponse Data { get; set; }
        public List<string> Errors { get; set; }
    }

    public class DataResponse
    {
        public string Authority { get; set; }
        public int Code { get; set; }
        public int Fee { get; set; }
        public string Fee_Type { get; set; }
        public string Message { get; set; }
    }

    public class ZarinpalVerifyRequest
    {
        public string merchant_id { get; set; }
        public int amount { get; set; }
        public string authority { get; set; }
    }



    public class ZarinpalVerifyResponse
    {
        public ZarinpalVerifyData data { get; set; }

        public List<string> errors { get; set; }
    }

    public class ZarinpalVerifyData
    {
        public int code { get; set; }
        public string message { get; set; }
        public string card_hash { get; set; }
        public string card_pan { get; set; }
        public long ref_id { get; set; }
        public string fee_type { get; set; }
        public int fee { get; set; }
    }

}
