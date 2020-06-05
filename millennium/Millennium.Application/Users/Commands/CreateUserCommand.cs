using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Millennium.Application.Repositories;
using Millennium.Domain;

namespace Millennium.Application.Users.Commands
{
    public class CreateUserCommand
    {
        private readonly ILogger _logger;
        private readonly IInMemoryRepository _repository;

        public UserDto User { get; }

        public CreateUserCommand(
            UserDto user,
            IInMemoryRepository repo,
            ILogger logger)
        {
            User = user;
            _repository = repo;
            _logger = logger;
        }

        public async Task<CreateUserCommandResult> Execute(CancellationToken cancellationToken)
        {
            if (User.Surname == "Dziarmaga")
            {
                _logger.LogWarning("Hey, you want to add my surname! (testing log)");
            }

            var user = new User(User.Name, User.Surname);

            await _repository.SaveAsync(
                user,
                cancellationToken);

            return new CreateUserCommandResult(user.Id);
        }
    }

    public class CreateUserCommandResult
    {
        public long Id { get; }

        public CreateUserCommandResult(long id)
        {
            Id = id;
        }
    }
}