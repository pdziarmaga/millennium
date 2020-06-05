using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Millennium.Api.Controllers;
using Millennium.Api.Controllers.Requests;
using Millennium.Application.Repositories;
using Millennium.Domain;
using Moq;
using NUnit.Framework;

namespace Millennium.Api.Tests
{
    public class UsersApiTests
    {
        private readonly CancellationToken _noneCancellationToken = CancellationToken.None;
        private IInMemoryRepository _inMemoryRepository;

        [SetUp]
        public void Setup()
        {
            _inMemoryRepository = new InMemoryRepository();
        }

        [Test]
        public async Task when_queryForExistingUser_returns_user()
        {
            const int mockedUserId = 1;

            // Arrange
            var mockRepo = new Mock<IInMemoryRepository>();
            mockRepo.Setup(repo => repo.GetAsync<User>(mockedUserId, _noneCancellationToken)).Returns(Task.FromResult(new User("Pawel", "Dziarmaga")));
            var mockLogger = new Mock<ILogger<UsersController>>();
            var controller = new UsersController(mockRepo.Object, mockLogger.Object);

            // Act
            var result = await controller.GetUser(new GetUserRequest
            {
                UserId = mockedUserId
            }, _noneCancellationToken);

            //Assert
            Assert.AreEqual("Pawel", result.User.Name);
            Assert.AreEqual("Dziarmaga", result.User.Surname);
        }

        [Test]
        public void when_queryForNonExistingUser_throws_exception()
        {
            // Arrange
            var mockRepo = new Mock<IInMemoryRepository>();
            var mockLogger = new Mock<ILogger<UsersController>>();
            var controller = new UsersController(mockRepo.Object, mockLogger.Object);

            // Act

            //Assert
            Assert.ThrowsAsync<ArgumentException>(() => controller.GetUser(new GetUserRequest
            {
                UserId = 2
            }, _noneCancellationToken));
        }

        [Test]
        public async Task when_createValidUser_returns_userIdGreaterThanZero()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var controller = new UsersController(_inMemoryRepository, mockLogger.Object);

            // Act
            var result = await controller.CreateUser(new CreateUserRequest
            {
                Name = "Jan",
                Surname = "Kowalski"
            }, _noneCancellationToken);

            //Assert
            Assert.Greater(result.UserId, 0);
        }

        [Test]
        public async Task when_queryForDeletedUser_throws_exception()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var controller = new UsersController(_inMemoryRepository, mockLogger.Object);

            // Act
            var createUserResponse = await controller.CreateUser(new CreateUserRequest
            {
                Name = "Ala",
                Surname = "Kowalska"
            }, _noneCancellationToken);

            await controller.DeleteUser(new DeleteUserRequest
            {
                UserId = createUserResponse.UserId
            }, _noneCancellationToken);

            //Assert
            Assert.ThrowsAsync<ArgumentException>(() => controller.GetUser(new GetUserRequest
            {
                UserId = createUserResponse.UserId
            }, _noneCancellationToken));
        }

        [Test]
        public async Task when_updateUserData_then_userDataIsUpdated()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<UsersController>>();
            var controller = new UsersController(_inMemoryRepository, mockLogger.Object);

            // Act
            var createUserResponse = await controller.CreateUser(new CreateUserRequest
            {
                Name = "Ala",
                Surname = "Kowalska"
            }, _noneCancellationToken);

            const string updatedName = "Gosia";
            const string updatedSurname = "Kowalska";

            await controller.UpdateUser(new UpdateUserRequest
            {
                UserId = createUserResponse.UserId,
                Name = updatedName,
                Surname = updatedSurname
            }, _noneCancellationToken);

            var updatedUserResponse = await controller.GetUser(new GetUserRequest
            {
                UserId = createUserResponse.UserId
            }, _noneCancellationToken);

            //Assert
            Assert.AreEqual(updatedName, updatedUserResponse.User.Name);
            Assert.AreEqual(updatedSurname, updatedUserResponse.User.Surname);
        }
    }
}