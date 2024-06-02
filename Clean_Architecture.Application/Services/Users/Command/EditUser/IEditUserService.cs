using Clean_Architecture.Common.Dto;
using Clean_Architecture.Common.UploadFile;

namespace Clean_Architecture.Application.Services.Users.Command.EditUser
{
    public interface IEditUserService
    {
        ResultDto Execute(RequestUserEditDto request);
    }
}
