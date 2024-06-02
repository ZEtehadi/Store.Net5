using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Finances;
using System;
using System.Linq;

namespace Clean_Architecture.Application.Services.Finances.Commands.AddRequestPay
{
    public class AddRequestPayService : IAddRequestPayService
    {
        private readonly IDataBaseContext _Context;
        public AddRequestPayService(IDataBaseContext Context)
        {
            _Context = Context;
        }

        public ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId, string DisCountCode)
        {
            float discountCost;
            int FinalAmount = Amount;
            long discountID =1;

            if (DisCountCode != null)
            {
                //var discount = _Context.DisCounts.FindAsync(DisCountCode);
                var discount= _Context.DisCounts
                                     .Where(d => d.DisCountCode == DisCountCode)
                                     .FirstOrDefault();

                if (discount == null)
                {
                    return new ResultDto<ResultRequestPayDto>()
                    {
                        Data = new ResultRequestPayDto(),
                        IsSeccess = false,
                        Message = "کد تخفیف یافت نشد"
                    };
                }
                ////Set DisCount Code


                 discountCost = (Amount * (discount.Percent)) / 100;
                 FinalAmount = Amount - (Convert.ToInt32(discountCost));
                discountID = discount.Id;
                /////Finish Calc DisCount
            }


            var User = _Context.Users.Find(UserId);
            RequestPay requestPay = new RequestPay()
            {
                User = User,
                Guid = Guid.NewGuid(),
                IsPay = false,
                Amount = FinalAmount,
                DisCountId =discountID,
            };
            _Context.RequestPays.Add(requestPay);
            _Context.SaveChanges();

            return new ResultDto<ResultRequestPayDto>()
            {
                Data = new ResultRequestPayDto()
                {
                    guid = requestPay.Guid,
                    Email = User.Email,
                    Amount = requestPay.Amount,
                    RequestPayId = requestPay.Id
                },
                IsSeccess = true,
                Message = "درخواست پرداخت با موفقیت ثبت شد"
            };
        }
    }
}
