using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Products;
using FitLife.DB.Context;
using FitLife.Infrastructure.CommandHandlers.Products;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FitLife.Tests.CommandHandler.Products
{
    public class AddProductCommandHandlerTests
    {
        private DbContextOptions<FoodContext> _options;
        private FoodContext _context;
        private Mock<ILogger<AddProductCommandHandler>> _logger;
        private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FoodContext>().UseInMemoryDatabase(databaseName: "FitLifeInMemory").Options;
            _context = new FoodContext(_options);
            _logger = new Mock<ILogger<AddProductCommandHandler>>();
            _config = new Mock<IConfiguration>();
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Execute_CorrectCommand_CreatesProducts()
        {
            //Arrange
            var command = new AddProductCommand { Name = "TestProduct", CarbsGrams = 12, FatsGrams = 22, ProteinsGrams = 31 };
            var validator = new Mock<AbstractValidator<AddProductCommand>>();
            validator.Setup(x => x.Validate(It.IsAny<ValidationContext<AddProductCommand>>())).Returns(new ValidationResult());
            var handler = new AddProductCommandHandler(_config.Object, _logger.Object, validator.Object, _context);

            //Act
            await handler.Handle(command);

            //Assert
            Assert.AreEqual(_context.Products.Count(), 1);
        }

        [Test]
        public async Task Execute_CorrectCommand_ReturnsSuccess()
        {
            //Arrange
            var command = new AddProductCommand { Name = "TestProduct", CarbsGrams = 12, FatsGrams = 22, ProteinsGrams = 31 };
            var validator = new Mock<AbstractValidator<AddProductCommand>>();
            validator.Setup(x => x.Validate(It.IsAny<ValidationContext<AddProductCommand>>())).Returns(new ValidationResult());
            var handler = new AddProductCommandHandler(_config.Object, _logger.Object, validator.Object, _context);

            //Act
            var result = await handler.Handle(command);

            //Assert
            Assert.AreEqual(result.Success, true);
        }

        [Test]
        public async Task Execute_IncorrectCommand_ReturnsFailure()
        {
            //Arrange
            var command = new AddProductCommand { Name = "TestProduct3", CarbsGrams = 12, FatsGrams = 22, ProteinsGrams = 31 };
            var validator = new Mock<AbstractValidator<AddProductCommand>>();
            var incorrectValidationResultStub = new ValidationResult()
                {Errors = {new ValidationFailure("Name", "IncorrectName")}};
            validator.Setup(x => x.Validate(It.IsAny<ValidationContext<AddProductCommand>>()))
                .Returns(incorrectValidationResultStub);
            var handler = new AddProductCommandHandler(_config.Object, _logger.Object, validator.Object, _context);

            //Act
            var result = await handler.Handle(command);

            //Assert
            Assert.AreEqual(result.Success, false);
        }
    }
}
