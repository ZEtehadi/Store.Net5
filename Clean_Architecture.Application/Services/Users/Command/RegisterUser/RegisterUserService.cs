using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Domain.Entities.Users;
using System.Collections.Generic;


namespace Clean_Architecture.Application.Services.Users.Command.RegisterUser
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDataBaseContext _context;

        public RegisterUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultRegisterUserDto> Execute(RequestRegisterUserDto request)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSeccess = false,
                        Message = "نام کاربری را وارد کنید."
                    };
                }
                if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSeccess = false,
                        Message = "شماره موبایل را وارد کنید."
                    };
                }
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSeccess = false,
                        Message= "پست الکترونیک را وارد کنید"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSeccess = false,
                        Message = "رمز عبور را وارد کنید"
                    };
                }
                if (request.Password != request.RePassword)
                {
                    return new ResultDto<ResultRegisterUserDto>()
                    {
                        Data = new ResultRegisterUserDto()
                        {
                            UserId = 0
                        },
                        IsSeccess = false,
                        Message = "رمز عبور و تکرار آن برابر نیستند"
                    };
                }

                //Conversion Password to Hashed Password
                var passwordHasher = new PasswordHasher();
                var hashedPassword = passwordHasher.HashPassword(request.Password);


                User user = new User()
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Password = hashedPassword,
                    PhoneNumber=request.PhoneNumber,
                   IsActive=true // at first all Users is Active
                };

                List<UserInRole> userInRoles = new List<UserInRole>();

                foreach (var item in request.roles)
                {
                    var roles = _context.Roles.Find(item.Id);

                    userInRoles.Add(new UserInRole()
                    {
                        Role = roles,
                        RoleId = roles.Id,
                        User = user,
                        UserId = user.Id
                    });
                };
                user.UserInRoles = userInRoles;

                _context.Users.Add(user);
                _context.SaveChanges();

                return new ResultDto<ResultRegisterUserDto>()
                {
                    Data = new ResultRegisterUserDto()
                    {
                        UserId = user.Id,
                    },
                    IsSeccess = true,
                    Message = "ثبت نام با موفقیت انجام شد"
                };


        }
            catch
            {
                return new ResultDto<ResultRegisterUserDto>()
                {
                    Data = new ResultRegisterUserDto()
        {
            UserId = 0
                    },
                    IsSeccess = false,
                    Message = "=!ثبت نام انجام نشد"
                };
}
        }

    }
}
