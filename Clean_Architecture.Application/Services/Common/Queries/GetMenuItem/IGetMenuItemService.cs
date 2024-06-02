using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Common.Queries.GetMenuItem
{
    public interface IGetMenuItemService
    {
        ResultDto<List<MenuItemDto>> Execute();
    }

    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDataBaseContext _context;
        public GetMenuItemService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<MenuItemDto>> Execute()
        {
            var Menu = _context.Categories
                     .Include(p => p.SubCategories)
                     .Where(p => p.ParentCategoryId == null)
                     .ToList()
                     .Select(p => new MenuItemDto()
                     {
                         CatId = p.Id,
                         Name = p.Name,
                         Child = p.SubCategories
                         .ToList()
                         .Select(p => new MenuItemDto()
                         {
                             CatId = p.Id,
                             Name = p.Name
                         }).ToList(),
                     }).ToList();

            return new ResultDto<List<MenuItemDto>>()
            {
                Data=Menu,
                IsSeccess=true,
                Message=""
            };
        }


    }
}
