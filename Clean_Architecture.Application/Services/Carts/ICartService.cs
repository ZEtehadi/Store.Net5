using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Products;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Carts
{
    public interface ICartService
    {
        public ResultDto AddToCart(long ProductId, Guid BrowserId,int Count);
        public ResultDto RemoveFromCart(long CartItemId, Guid BrowserId);
        public ResultDto<CartDto> GetMyCart(Guid BrowserId,long? UserId);
        public ResultDto AddCount(long CartItemId, Guid BrowserId);
        public ResultDto LowCount(long CartItemId, Guid BrowserId);
    }
}

