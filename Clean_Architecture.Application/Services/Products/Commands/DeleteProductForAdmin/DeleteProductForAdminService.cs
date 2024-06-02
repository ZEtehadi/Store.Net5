using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;

namespace Clean_Architecture.Application.Services.Products.Commands.DeleteProductForAdmin
{
    public class DeleteProductForAdminService : IDeleteProductForAdminService
    {
        private readonly IDataBaseContext _context;
        public DeleteProductForAdminService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto Execute(long Id)
        {
            var Product = _context.Products.Find(Id);
            if (Product == null)
            {
                return new ResultDto()
                {
                    IsSeccess=false,
                    Message="محصول یافت نشد"
                };
            }

            Product.IsRemoved = true;
            Product.RemovedTime = DateTime.Now;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSeccess=true,
                Message="محصول با موفقیت حذف شد"
            };
        }



    }
}
