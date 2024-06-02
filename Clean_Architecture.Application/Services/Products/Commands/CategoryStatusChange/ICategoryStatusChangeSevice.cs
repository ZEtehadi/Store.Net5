using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Products.Commands.CategoryStatusChange
{
    public interface ICategoryStatusChangeService
    {
        ResultDto Execute(long CatId);

    }

    public class CategoryStatusChangeService : ICategoryStatusChangeService
    {
        private readonly IDataBaseContext _context;
        public CategoryStatusChangeService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long CatId)
        {
            var Category = _context.Categories.Find(CatId);
            if (Category == null)
            {
                return new ResultDto
                {
                    IsSeccess = false,
                    Message = "دسته بندی یافت نشد"
                };
            }

            Category.IsActive = !Category.IsActive;
            _context.SaveChanges();

            string catState = Category.IsActive == true ? " فعال" : " غیر فعال";
            return new ResultDto
            {
                IsSeccess = true,
                Message = $"دسته بندی با موفقیت {catState} شد"
            };
        }
    }
}
