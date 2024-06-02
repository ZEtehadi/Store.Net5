using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Users.Queries.GetUseDetailById
{
    public interface IGetUserDetailByIdService
    {
        ResultDto<UserDetialDto> Execute(long UserId);
    }

    public class GetUserDetailByIdService : IGetUserDetailByIdService
    {
        private readonly IDataBaseContext _context;
        public GetUserDetailByIdService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<UserDetialDto> Execute(long UserId)
        {
            var userDetail = _context.Users.Include(p => p.UserInRoles)
                                         .ThenInclude(p => p.Role)
                                         .Where(p => p.Id == UserId)
                                         .ToList()
                                         .Select(p => new UserDetialDto()
                                         {
                                             UserId = p.Id,
                                             Name = p.FullName,
                                             Email = p.Email,
                                             PhoneNumber=p.PhoneNumber,
                                             LoginDateTime = p.DateTime,
                                             UserRoles =p.UserInRoles.Select(n=>n.Role.Name).ToList(),
                                             ImageSrc=p.ImegeSrc,
                                         }).FirstOrDefault();

            return new ResultDto<UserDetialDto>()
            {
                Data=userDetail,
                IsSeccess=true,
                Message="اطلاعات کاربر با شماره {"+userDetail.UserId+"} به درستی ارسال شد."

            };
        }
    }

    public class UserDetialDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> UserRoles { get; set; }
        public DateTime LoginDateTime { get; set; }
        public string? ImageSrc { get; set; }
        public string PhoneNumber { get; set; }

    }

}
