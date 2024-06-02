using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Commands.ChangeDisplayed
{
    public interface IChangeDisplayedService
    {
        ResultDto Execute(long Id);

    }
    public class ChangeDisplayedService : IChangeDisplayedService
    {
        private readonly IDataBaseContext _cotnext;
        public ChangeDisplayedService(IDataBaseContext context)
        {
            _cotnext = context;
        }

        public ResultDto Execute(long Id)
        {
            var Product = _cotnext.Products.Find(Id);
            if (Product == null)
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = "محصول یافت نشد"
                };
            }
            Product.Displayed = !Product.Displayed;
            _cotnext.SaveChanges();
            return new ResultDto()
            {
                IsSeccess = true,
                Message = "Displayed تغیر کرد"
            };
        }
    }
}
