using Millennium.Api.Controllers.Requests;
using Millennium.Api.Controllers.Responses;
using Millennium.Application.Users;
using Millennium.Application.Users.Commands;

namespace Millennium.Api.Controllers
{
    public static class UsersExtensions
    {
        public static GetUserResponse ToApiResponse(this UserDto user)
        {
            return new GetUserResponse
            {
                User = new UserModel
                {
                    Name = user.Name,
                    Surname = user.Surname
                }
            };
        }

        public static CreateUserResponse ToApiResponse(this CreateUserCommandResult result)
        {
            return new CreateUserResponse
            {
                UserId = result.Id
            };
        }

        public static UserDto ToCommand(this CreateUserRequest request)
        {
            return new UserDto(request.Name, request.Surname);
        }

        public static UserDto ToCommand(this UpdateUserRequest request)
        {
            return new UserDto(request.Name, request.Surname);
        }
    }
}