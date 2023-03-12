using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Products;
using FitLife.DB.Context;
using FitLife.DB.Models.Food;
using FitLife.Infrastructure.CommandHandlers.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace FitLife.Tests.CommandHandler.Products
{
    public class AddProductCommandHandlerTests
    {
        private DbContextOptions<FoodContext> _options;
        private FoodContext _context;
        private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FoodContext>().UseInMemoryDatabase(databaseName: "FitLifeInMemory").Options;
            _context = new FoodContext(_options);
            _config = new Mock<IConfiguration>();
            _config.Setup(c => c.GetSection(It.IsAny<string>())).Returns(new Mock<IConfigurationSection>().Object);
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Execute_CorrectCommand_CreatesProducts()
        {
            //Arrange
            var command = new AddProductCommand { Name = "TestProduct", CarbsGrams = 12, FatsGrams = 22, ProteinsGrams = 31 };
            var handler = new AddProductCommandHandler(_config.Object, _context);

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
            var handler = new AddProductCommandHandler(_config.Object, _context);

            //Act
            var result = await handler.Handle(command);

            //Assert
            Assert.AreEqual(result.Success, true);
        }

        [Test]
        public async Task Execute_DuplicatedProductName_ReturnsError()
        {
            //Arrange
            Seed(_context);
            var command = new AddProductCommand { Name = "TestProduct2", CarbsGrams = 12, FatsGrams = 22, ProteinsGrams = 31 };
            var handler = new AddProductCommandHandler(_config.Object, _context);

            //Act
            var result = await handler.Handle(command);

            //Assert
            Assert.AreEqual(result.Success, false);
        }

        private void Seed(FoodContext context)
        {
            var products = new[]
            {
                new Product {Calories = 122, CarbsGrams = 11, FatsGrams = 22, ProteinsGrams = 33, Name = "TestProduct2"},
            };

            context.Products.AddRange(products);
           
            context.SaveChanges();
        }
    }
}
