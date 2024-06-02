using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Users.Command.UserStatusChange
{

    public class UserStatusChangeService:IUserStatusChangeService
    {
        private readonly IDataBaseContext _context;
        public UserStatusChangeService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long UserId)
        {
            var user = _context.Users.Find(UserId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSeccess = false,
                    Message="کاربر یافت نشد"
                };
            }

            user.IsActive = !user.IsActive;
            _context.SaveChanges();

            string Userstate = user.IsActive == true ? "فعال" : "غیر فعال";
            return new ResultDto
            {
                IsSeccess=true,
                Message=$"کاربر با موفقیت {Userstate} شد"
            };
        }

    }
}
