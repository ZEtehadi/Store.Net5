using Clean_Architecture.Application.Interface.Contexts;
using System.Linq;
using Clean_Architecture.Common;

namespace Clean_Architecture.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {

        private readonly IDataBaseContext _context;

        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultGetUsersDto Execute(RequestGetUserDto request)
        {
            //این متد دارای کد تی_اسکیوال هست  ولی هنوز به سمت دیتابیس نرفته
            //صرفا دارای کوئری مورد نظر هست
            var Users = _context.Users.AsQueryable();//تمام کاربر ها در متغیر یوزر هستند

            if (!string.IsNullOrEmpty(request.SearchKey))//اگه "سرچ کی" نال نبود پس کاربر
            {//داره عملیات سرچ انجام میده
                Users = Users.Where(p => p.FullName.Contains(request.SearchKey) || p.Email.Contains(request.SearchKey));
            }

            int rowsCount = 0;
            var UsersList = Users.ToPaged(request.Page, 20, out rowsCount).Select(p => new GetUsersDto()
            {
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,
                PhoneNumber=p.PhoneNumber,
                IsActive=p.IsActive
            }).ToList();

            return new ResultGetUsersDto()
            {
                Rows=rowsCount,
                Users=UsersList,
            };
        }
    }
}
