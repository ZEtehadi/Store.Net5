using Clean_Architecture.Application.Services.Carts;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly CookiesManager _cookiewManeger;
        
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            _cookiewManeger = new CookiesManager();
        }


        public IActionResult Index()
        {
            var UserId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiewManeger.GetBrowserId(HttpContext),UserId).Data;

            return View(cart);
        }


        public IActionResult AddToCart(long ProductId,int Count)
        {
            var result = _cartService.AddToCart(ProductId, _cookiewManeger.GetBrowserId(HttpContext), Count);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(long CartItemId)
        {
            var result = _cartService.RemoveFromCart(CartItemId, _cookiewManeger.GetBrowserId(HttpContext));

            return RedirectToAction("Index");
        }

        public IActionResult AddCount(long CartItemId)
        {
            var result = _cartService.AddCount(CartItemId, _cookiewManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }

        public IActionResult LowCount(long CartItemId)
        {
            var result = _cartService.LowCount(CartItemId, _cookiewManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }
    }
}
