using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common;
using Clean_Architecture.Common.Dto;
using Clean_Architecture.Common.UploadFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Users.Command.EditUser
{

    public class EditUserService : IEditUserService
    {
        private readonly IDataBaseContext _context;
        private readonly UploadFilee _uploadFilee;
        public EditUserService(IDataBaseContext context, UploadFilee uploadFilee)
        {
            _context = context;
            _uploadFilee = uploadFilee;
        }

        public ResultDto Execute(RequestUserEditDto request)
        {
            var user = _context.Users.Find(request.UserId);
            if (user == null)
            {
                return new ResultDto()
                {
                    IsSeccess = false,
                    Message = "کاربر یافت نشد"
                };
            }
            if (request.ImageSrc!=null)
            {
                var UploadResult = _uploadFilee.DeleteAndUploadFile(request.ImageSrc, @"Users\","ProfilePhoto");
                user.ImegeSrc = UploadResult.FileNameAddress;
            }

            //Conversion Password to Hashed Password
            var passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.HashPassword(request.Password);


            user.Password = hashedPassword;
            user.FullName = request.FullName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNubmer;

            _context.SaveChanges();

            return new ResultDto()
            {
                IsSeccess = true,
                Message = "اطلاعات  با موفقیت ویرایش شد"
            };
        }
    }
}
