using System.Collections.Generic;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Authentication;
using FitLife.DB.Context;
using FitLife.DB.Models.Authentication;
using FitLife.Infrastructure.CommandHandlers.Authentication;
using FitLife.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FitLife.Tests.CommandHandler.Users
{
    class DisableUserCommandHandlerTests
    {
        private DbContextOptions<AuthenticationContext> _options;
        private AuthenticationContext _context;
        private Mock<ILogger<DisableUserCommandHandler>> _logger;
        private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<AuthenticationContext>().UseInMemoryDatabase(databaseName: "FitLifeInMemory").Options;
            _logger = new Mock<ILogger<DisableUserCommandHandler>>();
            _config = new Mock<IConfiguration>();
            _context = new AuthenticationContext(_options);
        }

        [Test]
        public async Task Execute_CorrectCommand_EnablesUserAccount()
        {

            //Arrange
            var usersStub = new List<AppUser>
            {
                new AppUser {Id = "6bd969d6-cec7-4383-8aa0-d59b89f77602", Email = "test@mail.com", FullName = "Mr Test", IsDisabled = false}
            };
            var idStub = "6bd969d6-cec7-4383-8aa0-d59b89f77602";

            var userManager = MockUserManager.Build(usersStub, idStub).Object;
            var command = new DisableUserCommand { Id = idStub };
            var handler = new DisableUserCommandHandler(_config.Object, _logger.Object, _context, userManager);

            //Act
            await handler.Handle(command);

            //Assert
            var user = await userManager.FindByIdAsync(idStub);
            Assert.AreEqual(user.IsDisabled.Value, true);
        }
    }
}
