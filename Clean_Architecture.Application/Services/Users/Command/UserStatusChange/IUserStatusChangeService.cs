using Clean_Architecture.Common.Dto;

namespace Clean_Architecture.Application.Services.Users.Command.UserStatusChange
{
    public interface IUserStatusChangeService
    {
        ResultDto Execute(long UserId);
    }
}
