using System;
using System.Threading;
using System.Threading.Tasks;
using Millennium.Application.Repositories;
using Millennium.Domain;

namespace Millennium.Application.Users.Queries
{
    public class GetUserQuery
    {
        private readonly IInMemoryRepository _repository;

        public long UserId { get; }

        public GetUserQuery(long userId, IInMemoryRepository repo)
        {
            UserId = userId;
            _repository = repo;
        }

        public async Task<UserDto> Execute(CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync<User>(
                UserId,
                cancellationToken);

            if (user == null)
            {
                throw new ArgumentException($"User with id {UserId} was not found.");
            }

            return new UserDto(
                user.Name,
                user.Surname);
        }
    }
}