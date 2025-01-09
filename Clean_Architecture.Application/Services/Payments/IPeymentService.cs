using Clean_Architecture.Application.Interface.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Payments
{

    public interface IPaymentService
    {
       // PaymentOfOrderDto PayForOrder(int OrderId);
        PaymentDto GetPayment(Guid Id);
        bool VerifyPayment(Guid Id, string Authority, long RefId);

    }

    public class PaymentService : IPaymentService
    {
        private readonly IDataBaseContext context;
        //private readonly IIdentityDatabaseContext identityContext;
        public PaymentService(IDataBaseContext context)//, IIdentityDatabaseContext identityDatabaseContext)
        {
            this.context = context;
            //identityContext = identityDatabaseContext;
        }

        public PaymentDto GetPayment(Guid Id)
        {
            //var payment = context.Payments
            //     .Include(p => p.Order)
            //     .ThenInclude(p => p.OrderItems)
            //     .SingleOrDefault(p => p.Id == Id);

           // var user = identityContext.Users.SingleOrDefault(p => p.Id == payment.Order.UserId);

           // string description = $"پرداخت سفارش شماره {payment.OrderId} " + Environment.NewLine;
            //string description = $"پرداخت سفارش شماره {10001} " + Environment.NewLine;
            //description += "محصولات" + Environment.NewLine;
            //foreach (var item in payment.Order.OrderItems.Select(p => p.ProductName))
            //{
            //    description += $" -{item}";
            //}

            PaymentDto paymentDto = new PaymentDto
            {
                Amount = 1200300,
                Email ="zetehadi822@gamail.com",
                PhoneNumber = "09925306265",
                Userid = "1",
                Id = new Guid(),
                Description = "این درگاه پرداخت ازمایشی توسط زهرا است.",
            };
            return paymentDto;
        }

        //public PaymentOfOrderDto PayForOrder(int OrderId)
        //{
        //    //var order = context.Orders
        //    //        .Include(p => p.OrderItems)
        //    //        .SingleOrDefault(p => p.Id == OrderId);
        //    //if (order == null)
        //    //    throw new Exception("");
        //    //var payment = context.Payments.SingleOrDefault(p => p.OrderId == order.Id);

        //    //if (payment == null)
        //    //{
        //    //    payment = new Payment(order.TotalPrice(), order.Id);
        //    //    context.Payments.Add(payment);
        //    //    context.SaveChanges();
        //    //}

        //    return new PaymentOfOrderDto()
        //    {
        //        Amount =120,// payment.Amount,
        //        PaymentId =new Guid(),// payment.Id,
        //        PaymentMethod = order.PaymentMethod,
        //    };


        //}

        public bool VerifyPayment(Guid Id, string Authority, long RefId)
        {
            var payment = context.RequestPays
       .Include(p => p.Orders)
       .SingleOrDefault(p => p.Id.ToString() == Id.ToString());

            if (payment == null)
                throw new Exception("payment not found");

            // payment.Orders.PaymentDone();
            // payment.PaymentIsDone(Authority, RefId);
            payment.RefId = RefId;
            payment.Authority = Authority;

            context.SaveChanges();
            return true;
        }
    }



    public class PaymentOfOrderDto
    {
        public Guid PaymentId { get; set; }
        public int Amount { get; set; }
       // public PaymentMethod PaymentMethod { get; set; }
    }


    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public string Userid { get; set; }
    }
}
