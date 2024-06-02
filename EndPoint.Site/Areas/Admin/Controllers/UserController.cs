using Clean_Architecture.Application.Services.Users.Command.EditUserForAdmin;
using Clean_Architecture.Application.Services.Users.Command.RegisterUser;
using Clean_Architecture.Application.Services.Users.Command.RemoveUser;
using Clean_Architecture.Application.Services.Users.Command.UserStatusChange;
using Clean_Architecture.Application.Services.Users.Queries.GetRoles;
using Clean_Architecture.Application.Services.Users.Queries.GetUsers;
using Clean_Architecture.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin,Operator")]
    public class UserController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IRemoveUserService _removeUserService;
        private readonly IUserStatusChangeService _userStatusChangeService;
        private readonly IEditUserForAdminService _editUserForAdminService;

        public UserController(IGetUsersService getUsersService, IGetRolesService getRolesService, IRegisterUserService registerUserService, IRemoveUserService removeUserService,
            IUserStatusChangeService userStatusChangeService, IEditUserForAdminService editUserForAdminService)
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
            _removeUserService = removeUserService;
            _userStatusChangeService = userStatusChangeService;
            _editUserForAdminService=editUserForAdminService;
        }



        public IActionResult Index(string SearchKey, int Page = 1)
        {
            return View(_getUsersService.Execute(new RequestGetUserDto()
            {
                Page = Page,
                SearchKey = SearchKey
            }));
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data,"Id","Name");

            return View();
        }


        [HttpPost]
        public IActionResult Create(string email, string fullname, long RoleId, string Password, string RePassword,string PhoneNumber)
        {

            //Validation 
            if (string.IsNullOrWhiteSpace(fullname) ||
               string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(RePassword) ||
                string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return Json(new ResultDto { IsSeccess = false, Message = "لطفا فیلدها را پر کنید" });
            }

           
            if (Password != RePassword)
                return Json(new ResultDto { IsSeccess = false, Message = "رمز عبور و تکرار آن برابر نیست" });

            if (Password.Length < 8)
                return Json(new ResultDto { IsSeccess = false, Message = "رمز عبور باید حداقل 8 کاراکتر باشد" });


            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_'{1}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
            var emailmatch = Regex.Match(email, emailRegex, RegexOptions.IgnoreCase);
            if (!emailmatch.Success)//Email is Faild
            {
                return Json(new ResultDto { IsSeccess = false, Message = "ایمیل خود را به درستی وارد کنید" });
            }



            string phoneRegex = @"^(?:(?:(?:\\+?|00)(98))|(0))?((?:90|91|92|93|99)[0-9]{8})$";
            var phonematch = Regex.Match(PhoneNumber, phoneRegex, RegexOptions.IgnoreCase);
            if (!phonematch.Success)//Email is Faild
            {
                return Json(new ResultDto { IsSeccess = false, Message = "موبایل خود را به درستی وارد کنید" });
            }


            //Validation is done



            var result = _registerUserService.Execute(new RequestRegisterUserDto
            {
                FullName = fullname,
                Email = email,
                PhoneNumber= PhoneNumber,
                roles = new List<RolesInRegisterUserDto>()
                {
                    new RolesInRegisterUserDto
                    {
                        Id=RoleId
                    }
                },

                Password = Password,
                RePassword = RePassword,
            });

            return Json(result);
        }


        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_removeUserService.Execute(UserId));
        }


        [HttpPost]
        public IActionResult UserStatusChange(long UserId)
        {
            return Json(_userStatusChangeService.Execute(UserId));
        }


        [HttpPost]
        public IActionResult Edit(long UserId, string FullName, string Email,string PhoneNumber)
        {

            //Validation 
            if (string.IsNullOrWhiteSpace(FullName) ||
               string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(PhoneNumber))
            {
                return Json(new ResultDto { IsSeccess = false, Message = "لطفا فیلدها را پر کنید" });
            }


            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_'{1}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
            var emailmatch = Regex.Match(Email, emailRegex, RegexOptions.IgnoreCase);
            if (!emailmatch.Success)//Email is Faild
            {
                return Json(new ResultDto { IsSeccess = false, Message = "ایمیل را به درستی وارد کنید" });
            }



            string phoneRegex = @"^(?:(?:(?:\\+?|00)(98))|(0))?((?:90|91|92|93|99)[0-9]{8})$";
            var phonematch = Regex.Match(PhoneNumber, phoneRegex, RegexOptions.IgnoreCase);
            if (!phonematch.Success)//Email is Faild
            {
                return Json(new ResultDto { IsSeccess = false, Message = "موبایل را به درستی وارد کنید" });
            }


            //Validation is done



            ViewBag.test = "true";
            return Json(_editUserForAdminService.Execute(new RequestUserEditForAdminDto
            {
                UserId = UserId,
                FullName = FullName,
                Email = Email,
                PhoneNubmer= PhoneNumber,
            }));
        }
    }
}
