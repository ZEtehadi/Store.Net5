using System;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        ResultGetUsersDto Execute(RequestGetUserDto request);

    }

}
