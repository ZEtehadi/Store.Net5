using Clean_Architecture.Application.Services.Users.Command.RegisterUser;
using Clean_Architecture.Application.Services.Users.Command.UserLogin;
using Clean_Architecture.Common.Dto;
using EndPoint.Site.Models.ViewModels.AuthenticationViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly IUserLoginService _userLoginService;

        public AuthenticationController(IRegisterUserService registerUserService, IUserLoginService userLoginService)
        {
            _registerUserService = registerUserService;
            _userLoginService = userLoginService;
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SignUp(SignUpViewModel requst)
        {
            //Validation 
            if (string.IsNullOrWhiteSpace(requst.FullName) ||
               string.IsNullOrWhiteSpace(requst.Email) ||
                string.IsNullOrWhiteSpace(requst.Password) ||
                string.IsNullOrWhiteSpace(requst.RePassword) ||
                string.IsNullOrWhiteSpace(requst.PhoneNumber))
            {
                return Json(new ResultDto { IsSeccess = false, Message = "لطفا فیلدها را پر کنید" });
            }

            if (User.Identity.IsAuthenticated == true)
                return Json(new ResultDto { IsSeccess = false, Message = "شما به حساب کاربری خود وارد شده اید و در حال حاضر نمیتوانید ثبت نام کنید" });

            if (requst.Password != requst.RePassword)
                return Json(new ResultDto { IsSeccess = false, Message = "رمز عبور و تکرار آن برابر نیست" });

            if (requst.Password.Length < 8)
                return Json(new ResultDto { IsSeccess = false, Message = "رمز عبور باید حداقل 8 کاراکتر باشد" });


            string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_'{1}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
            var emailmatch = Regex.Match(requst.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!emailmatch.Success)//Email is Faild
            {
                return Json(new ResultDto { IsSeccess = false, Message = "ایمیل خود را به درستی وارد کنید" });
            }
            
            
            
            string phoneRegex = @"^(?:(?:(?:\\+?|00)(98))|(0))?((?:90|91|92|93|99)[0-9]{8})$";
            var phonematch = Regex.Match(requst.PhoneNumber, phoneRegex, RegexOptions.IgnoreCase);
            if (!phonematch.Success)//Email is Faild
            {
                return Json(new ResultDto { IsSeccess = false, Message = "موبایل خود را به درستی وارد کنید" });
            }


            //Validation is done


            var SignUpResult = _registerUserService.Execute(new RequestRegisterUserDto
            {
                FullName = requst.FullName,
                Email = requst.Email,
                Password = requst.Password,
                RePassword = requst.RePassword,
                PhoneNumber=requst.PhoneNumber,
                roles = new List<RolesInRegisterUserDto>()
                {
                    new RolesInRegisterUserDto{Id=2}//role=Customer
                }
            }); //at first , User Registration is done



            if (SignUpResult.IsSeccess == true) //Registration is done Successfully so add Claim
            {
                var Claims = new List<Claim>()
               {
                   new Claim(ClaimTypes.NameIdentifier,SignUpResult.Data.UserId.ToString()),
                   new Claim(ClaimTypes.Email,requst.Email),
                   new Claim(ClaimTypes.Name,requst.FullName),
                   new Claim(ClaimTypes.MobilePhone,requst.PhoneNumber),
                   new Claim(ClaimTypes.Role,"Customer"),
               };

                var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync(principal, properties);

            }// Login is done outomatically

            return Json(SignUpResult);

        }


        [HttpGet]
        public IActionResult SignIn(string Returnurl = "/")
        {
            ViewBag.url = Returnurl;
            return View();
        }


        [HttpPost]
        public IActionResult SignIn(string Email, string Password,string url = "/")
        {
            var SignUpResult = _userLoginService.Execute(Email, Password);

            if (SignUpResult.IsSeccess == true)
            {
                var Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,SignUpResult.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Email,Email),
                    new Claim(ClaimTypes.MobilePhone,SignUpResult.Data.PhoneNumber.ToString()),
                    new Claim(ClaimTypes.Name,SignUpResult.Data.Name),
                };

                foreach (var item in SignUpResult.Data.Roles)
                {
                    Claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5)
                };
                HttpContext.SignInAsync(principal, properties);
            }

            return Json(SignUpResult);
        }

        public IActionResult SignOut()
        {
            //Exite User in UserPanel
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index","Home");
        }
    }

}
