using Clean_Architecture.Common.Dto;

namespace Clean_Architecture.Application.Services.Users.Command.UserLogin
{
    public interface IUserLoginService
    {
        ResultDto<ResultUserLoginDto> Execute(string UserName, string Password);
    }
}
