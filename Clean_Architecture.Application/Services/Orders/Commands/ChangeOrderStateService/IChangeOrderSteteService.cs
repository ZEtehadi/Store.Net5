using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Orders.Commands.ChangeOrderStateService
{
   public interface IChangeOrderSteteService
    {
        ResultDto Execute(long OrderId,OrderState orderState);
    }

    public class ChangeOrderStateService : IChangeOrderSteteService
    {
        private readonly IDataBaseContext _context;
        public ChangeOrderStateService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long OrderId, OrderState orderState)
        {
            var order = _context.Orders.Find(OrderId);

            order.OrderState = orderState;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSeccess=true,
                Message=$"وضعیت سفارش به {orderState} تغیر یافت."
            };
        }
    }
}
