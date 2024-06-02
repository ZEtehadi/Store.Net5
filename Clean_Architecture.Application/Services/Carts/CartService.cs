using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Clean_Architecture.Application.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly IDataBaseContext _context;

        public CartService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto AddToCart(long ProductId, Guid BrowserId,int Count)
        {
            var cart = _context.Carts.Where(p => p.BrowserId == BrowserId &&
                                            p.Fininshed == false).FirstOrDefault();
            if (cart == null)
            {
                Cart NewCart = new Cart()
                {
                    Fininshed = false,
                    BrowserId = BrowserId
                };
                _context.Carts.Add(NewCart);
                _context.SaveChanges();
                cart = NewCart;
            }

            var product = _context.Products.Find(ProductId);
            var cartItem = _context.CartItems
                           .Where(p => p.CartId == cart.Id && p.ProductId == ProductId)
                            .FirstOrDefault();

            if (cartItem != null)
            {
                cartItem.Count++;
            }
            else
            {
                var NewCartItem = new CartItems()
                {
                    Product = product,
                    Count = Count,
                    Price = product.Price,
                    Cart = cart,
                };
                _context.CartItems.Add(NewCartItem);
                _context.SaveChanges();
            }
            return new ResultDto()
            {
                IsSeccess = true,
                Message = "محصول با موفقیت اضافه شد"
            };
        }

        public ResultDto RemoveFromCart(long CartItemId, Guid BrowserId)
        {
            var cartItem = _context.CartItems
                         .Where(p => p.Cart.BrowserId == BrowserId &&
                                p.Id == CartItemId)
                         .FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.IsRemoved = true;
                cartItem.RemovedTime = DateTime.Now;

                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSeccess = true,
                    Message = "محصول با موفقیت حذف شد"
                };
            }

            return new ResultDto()
            {
                IsSeccess = false,
                Message = "محصول یافت نشد"
            };
        }

        public ResultDto<CartDto> GetMyCart(Guid BrowserId,long? UserId)
        {
            //string s = "3e869ecb-e507-469d-ab7c-de3647cd7b9c";
            //var cart = _context.Carts
            //                    .Include(p => p.CartItems)
            //                    .ThenInclude(p => p.Product)
            //                    .ThenInclude(p => p.ProductImages)
            //                    .Where(p => p.BrowserId ==Guid.Parse(s) && p.Fininshed == false)
            //                    .OrderByDescending(p => p.Id)
            //                    .FirstOrDefault();

            var cart = _context.Carts
                    .Include(p => p.CartItems)
                    .ThenInclude(p => p.Product)
                    .ThenInclude(p => p.ProductImages)
                    .Where(p => p.BrowserId == BrowserId && p.Fininshed == false)
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefault();

             if (cart == null)
            {
                return new ResultDto<CartDto>()
                {
                    Data =null,
                    IsSeccess = true,
                    Message = "محصولات با موفقیت نمایش داده شد"
                };
            }

            if (UserId != null)
            {
                var user = _context.Users.Find(UserId);
                cart.User = user;
                _context.SaveChanges();
            }

            return new ResultDto<CartDto>()
            {
                Data = new CartDto()
                {
                    CartId=cart.Id,
                    ProductCount = cart.CartItems.Count(),
                    SumAmount = cart.CartItems.Sum(p => p.Count * p.Price),
                    CartItems = cart.CartItems.Select(p => new CartItemDto()
                    {
                        ProductName = p.Product.Name,
                        ProductId=p.Product.Id,
                        Price = p.Price,
                        Count = p.Count,
                        Id = p.Id,
                        ImageSrc = p.Product?.ProductImages?.FirstOrDefault()?.Src ?? "",
                    }).ToList()
                },
                IsSeccess = true,
                Message = "محصولات با موفقیت نمایش داده شد"
            };
        }


        public ResultDto AddCount(long CartItemId, Guid BrowserId)
        {
            var cartItem = _context.CartItems
                          .Where(p => p.Cart.BrowserId == BrowserId &&
                                p.Cart.Fininshed == false &&
                                p.Id == CartItemId)
                          .FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Count++;
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSeccess = true,
                    Message = "تعداد محصول افزایش یافت"
                };
            }

            return new ResultDto()
            {
                IsSeccess = false,
                Message = "محصول یافت نشد"
            };
        }



        public ResultDto LowCount(long CartItemId, Guid BrowserId)
        {
            var cartItem = _context.CartItems
                          .Where(p => p.Cart.BrowserId == BrowserId &&
                                p.Cart.Fininshed == false &&
                                p.Id == CartItemId)
                          .FirstOrDefault();

            if (cartItem != null && cartItem.Count >= 2)
            {
                cartItem.Count--;
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSeccess = true,
                    Message = "تعداد محصول افزایش یافت"
                };
            }

            return new ResultDto()
            {
                IsSeccess = false,
                Message = "محصول یافت نشد یا تعداد برابر 1 است"
            };
        }
    }
}

