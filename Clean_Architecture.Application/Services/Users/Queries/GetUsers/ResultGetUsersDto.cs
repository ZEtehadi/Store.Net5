using System.Collections.Generic;

namespace Clean_Architecture.Application.Services.Users.Queries.GetUsers
{
    public class ResultGetUsersDto
    {
        public List<GetUsersDto> Users { get; set; }
        public int Rows { get; set; }

    }
}
