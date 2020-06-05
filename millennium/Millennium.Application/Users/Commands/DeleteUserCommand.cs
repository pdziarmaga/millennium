using System.Threading;
using System.Threading.Tasks;
using Millennium.Application.Repositories;
using Millennium.Domain;

namespace Millennium.Application.Users.Commands
{
    public class DeleteUserCommand
    {
        private readonly IInMemoryRepository _repository;

        public long UserId { get; }

        public DeleteUserCommand(
            long userId,
            IInMemoryRepository repo)
        {
            UserId = userId;
            _repository = repo;
        }

        public async Task<CreateUserCommandResult> Execute(CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetAsync<User>(
                UserId,
                cancellationToken);

            await _repository.DeleteAsync(
                existingUser,
                cancellationToken);

            return new CreateUserCommandResult(existingUser.Id);
        }
    }
}