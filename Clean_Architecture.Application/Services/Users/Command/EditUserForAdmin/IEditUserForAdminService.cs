using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Users.Command.EditUserForAdmin
{
    public interface IEditUserForAdminService
    {
        ResultDto Execute(RequestUserEditForAdminDto request);

    }
    public class EditUserForAdminService : IEditUserForAdminService
    {

        private readonly IDataBaseContext _context;
        public EditUserForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestUserEditForAdminDto request)
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
    public class RequestUserEditForAdminDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNubmer { get; set; }

    }
}
