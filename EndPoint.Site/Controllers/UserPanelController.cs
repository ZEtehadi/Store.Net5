using Clean_Architecture.Application.Services.Users.Command.EditUser;
using Clean_Architecture.Application.Services.Users.Queries.GetUseDetailById;
using Clean_Architecture.Common.Dto;
using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class UserPanelController : Controller
    {
        private readonly IGetUserDetailByIdService _getUserDetailByIdService;
        private readonly IEditUserService _editUserService;
        public UserPanelController(IGetUserDetailByIdService getUserDetailByIdService,
            IEditUserService editUserService)
        {
            _getUserDetailByIdService = getUserDetailByIdService;
            _editUserService = editUserService;
        }

        public IActionResult Index()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User).Value;
            var userDetail = _getUserDetailByIdService.Execute(userId).Data;
            return View(userDetail);
        }

        [HttpPost]
        public IActionResult Index(string Name,string Email, string Password, string RePassword, string PhoneNumber,IFormFile File)
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User).Value;

            //Validation 
            if (string.IsNullOrWhiteSpace(Name) ||
               string.IsNullOrWhiteSpace(Email) ||
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
            var emailmatch = Regex.Match(Email, emailRegex, RegexOptions.IgnoreCase);
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
            /////////////////////valid



            //Registration is done Successfully so add Claim

            var Claims = new List<Claim>()
               {
                   new Claim(ClaimTypes.NameIdentifier,userId.ToString()),
                   new Claim(ClaimTypes.Email,Email),
                   new Claim(ClaimTypes.Name,Name),
                   new Claim(ClaimTypes.MobilePhone,PhoneNumber),
                   new Claim(ClaimTypes.Role,"Customer"),
               };

            var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };

            HttpContext.SignInAsync(principal, properties);

            // Update is done outomatically


            /////

            var response = _editUserService.Execute(new RequestUserEditDto()
            {
                UserId = userId,
                FullName = Name,
                Email = Email,
                PhoneNubmer = PhoneNumber,
                ImageSrc = File,
                Password = Password,
            });

            return Json(response);
        }
    }
}
