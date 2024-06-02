using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Orders.Commands.AddNewOrder
{
    public interface IAddNewOrderService
    {
        ResultDto Execute(RequestAddNewOrderServiceDto request);
    }
    public class AddNewOrderService : IAddNewOrderService
    {
        private readonly IDataBaseContext _context;
        public AddNewOrderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewOrderServiceDto request)
        {
            var user = _context.Users.Find(request.UserId);
            var cart = _context.Carts.Include(p => p.CartItems)
                                   .ThenInclude(p => p.Product)
                                   .Where(p => p.Id == request.CartId).FirstOrDefault();
            var reqeustPay = _context.RequestPays.Find(request.RequestPayId);

            reqeustPay.IsPay = true;
            reqeustPay.PayDate = DateTime.Now;
            reqeustPay.RefId = request.RefId;
            reqeustPay.Authority = request.Authority;

            cart.Fininshed = true;

            Order order = new Order()
            {
                User=user,
                RequestPay=reqeustPay,
                OrderState=OrderState.Processing,
                Address="User Address Must be get"
            };

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach(var item in cart.CartItems)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Order=order,
                    Product=item.Product,
                    Price=item.Product.Price,
                    Count=item.Count
                };
                orderDetails.Add(orderDetail);
            }
            _context.OrderDetails.AddRange(orderDetails);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSeccess=true,
                Message="The Order has been Saved Successfully"
            };
        }
    }

    public class RequestAddNewOrderServiceDto
    {
        public long UserId { get; set; }
        public long RequestPayId { get; set; }
        public long CartId { get; set; }
        public long RefId { get; set; } = 0;
        public string Authority { get; set; }
    }
}
