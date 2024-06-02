using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Users.Command.UserLogin
{

    public class UserLoginService:IUserLoginService
    {
        private readonly IDataBaseContext _context;

        public UserLoginService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto<ResultUserLoginDto> Execute(string UserName, string Password)
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return new ResultDto<ResultUserLoginDto>()
                {
                    Data=new ResultUserLoginDto()
                    {},
                    IsSeccess=false,
                    Message="نام کاربری و رمز عبور را وارد کنید"
                };
            }

            //Find Email(UserName)
            var user = _context.Users
                     .Include(p => p.UserInRoles)
                     .ThenInclude(p => p.Role)
                     .Where(p => p.Email.Equals(UserName)
                     && p.IsActive == true).FirstOrDefault();
            if (user == null)
            {
                return new ResultDto<ResultUserLoginDto>()
                {
                    Data = new ResultUserLoginDto()
                    { },
                    IsSeccess = false,
                    Message="کاربری با این ایمیل ثبت نام نکرده"
                };
            }

            //Checking Password
            var PasswordHasher = new PasswordHasher();
            bool resultVerifyPassword = PasswordHasher.VerifyPassword(user.Password, Password);

            if (resultVerifyPassword == false)
            {
                return new ResultDto<ResultUserLoginDto>()
                {
                    Data = new ResultUserLoginDto()
                    { },
                    IsSeccess = false,
                    Message = "رمز وارد شده اشتباه است"
                };
            }

            List<string> roles = new List<string>();
            foreach (var item in user.UserInRoles)
            {
                roles.Add(item.Role.Name);
            }

            return new ResultDto<ResultUserLoginDto>()
            {
                Data = new ResultUserLoginDto()
                { 
                    UserId=user.Id,
                    Roles=roles,
                    Name=user.FullName,
                    PhoneNumber=user.PhoneNumber,
                },
                IsSeccess = true,
                Message = "ورود با موفقیت انجام شد"
            };
        }
    }
}
