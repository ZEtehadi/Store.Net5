using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Dto;
using System;

namespace Clean_Architecture.Application.Services.Users.Command.RemoveUser
{
    public class RemoveUserService : IRemoveUserService
    {
        private readonly IDataBaseContext _context;
        public RemoveUserService(IDataBaseContext context)
        {
            _context = context;
        }
        
        public ResultDto Execute(long UserId)
        {
            var User = _context.Users.Find(UserId);
            if (User == null)
            {
                return new ResultDto
                {
                    IsSeccess = false,
                    Message="کاربر یافت نشد"
                };
            }

            User.RemovedTime = DateTime.Now;
            User.IsRemoved = true;
            _context.SaveChanges();
            
            return new ResultDto
            {
                IsSeccess=true,
                Message="کاربر با موفقیت حذف شد"
            };
        }
    }
}
