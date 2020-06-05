using System.Threading;
using System.Threading.Tasks;
using Millennium.Application.Repositories;
using Millennium.Domain;

namespace Millennium.Application.Users.Commands
{
    public class UpdateUserCommand
    {
        private readonly IInMemoryRepository _repository;

        public long UserId { get; }
        public UserDto User { get; }

        public UpdateUserCommand(
            long userId,
            UserDto user,
            IInMemoryRepository repo)
        {
            UserId = userId;
            User = user;
            _repository = repo;
        }

        public async Task<CreateUserCommandResult> Execute(CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetAsync<User>(
                UserId,
                cancellationToken);

            existingUser.Update(
                User.Name,
                User.Surname);

            await _repository.SaveAsync(
                existingUser,
                cancellationToken);

            return new CreateUserCommandResult(existingUser.Id);
        }
    }
}