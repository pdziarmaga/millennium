using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Millennium.Api.Controllers.Requests;
using Millennium.Api.Controllers.Responses;
using Millennium.Application.Repositories;
using Millennium.Application.Users.Commands;
using Millennium.Application.Users.Queries;

namespace Millennium.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action].{format}"), FormatFilter]
    public class UsersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IInMemoryRepository _repository;

        public UsersController(
            IInMemoryRepository repository,
            ILogger<UsersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<GetUserResponse> GetUser(
            [FromQuery] GetUserRequest request,
            CancellationToken cancellationToken)
        {
            var getUsersQuery = new GetUserQuery(
                request.UserId,
                _repository);

            var result = await getUsersQuery.Execute(cancellationToken);

            return result.ToApiResponse();
        }

        [HttpPost]
        public async Task<CreateUserResponse> CreateUser(
            [FromBody] CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var createUserCommand = new CreateUserCommand(
                request.ToCommand(),
                _repository,
                _logger);

            var result = await createUserCommand.Execute(cancellationToken);

            return result.ToApiResponse();
        }

        [HttpPut]
        public Task UpdateUser(
            [FromBody] UpdateUserRequest request,
            CancellationToken cancellationToken)
        {
            var updateUserCommand = new UpdateUserCommand(
                request.UserId,
                request.ToCommand(),
                _repository);

            return updateUserCommand.Execute(cancellationToken);
        }

        [HttpDelete]
        public Task DeleteUser(
            [FromQuery] DeleteUserRequest request,
            CancellationToken cancellationToken)
        {
            var deleteUserCommand = new DeleteUserCommand(
                request.UserId,
                _repository);

            return deleteUserCommand.Execute(cancellationToken);
        }
    }
}