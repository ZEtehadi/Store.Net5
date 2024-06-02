using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Clean_Architecture.Application.Services.Products.Commands.DeleteCategory
{
    public class DeleteCategoryService : IDeleteCategoryService
    {
        private readonly IDataBaseContext _context;
        public DeleteCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public object ChildName { get; private set; }


        public ResultDto Execute(long Id)
        {
            var Category = _context.Categories.Find(Id);
            var ChildCat = _context.Categories
                         .Where(p => p.ParentCategoryId == Id)
                         .ToList();
            var ProductHasCat = _context.Products
                              .Include(p => p.Category)
                              .ToList()
                             .Where(p => p.CategoryId == Id || p.Category.ParentCategoryId == Id)
                             .ToList();

            if (Category == null)
            {
                return new ResultDto
                {
                    IsSeccess = false,
                    Message = "دسته بندی یافت نشد"
                };
            }
            if (ChildCat.Count==0 && ProductHasCat.Count==0)//یعنی دسته بندی هیچ محصول و زیرشاخه ای ندارد
            {//پس میتونه حذف بشه
                Category.IsRemoved = true;
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSeccess = true,
                    Message = $"دسته بندی -{Category.Name}- حذف شد"
                };
            }
            else//دسته بندی دارای فرزند است پس نمیشه حذف کرد
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = $"دسته بندی دارای -{ChildCat.Count}- زیرشاخه و -{ProductHasCat.Count}- محصول است "
                };
            }

        }

    }
}
