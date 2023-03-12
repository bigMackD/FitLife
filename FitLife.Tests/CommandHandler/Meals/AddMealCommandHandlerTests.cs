using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.DB.Context;
using FitLife.DB.Models.Food;
using FitLife.Infrastructure.CommandHandlers.Meals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FitLife.Tests.CommandHandler.Meals
{
    class AddMealCommandHandlerTests
    {
        private DbContextOptions<FoodContext> _options;
        private FoodContext _context;
        private Mock<ILogger<AddMealCommandHandler>> _logger;
        private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FoodContext>().UseInMemoryDatabase(databaseName: "FitLifeInMemory").Options;
            _context = new FoodContext(_options);
            _logger = new Mock<ILogger<AddMealCommandHandler>>();
            _config = new Mock<IConfiguration>();
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Execute_CorrectCommand_CreatesMeal()
        {
            //Arrange
            Seed(_context);
            var command = new AddMealCommand { Name = "TestMeal", CategoryId = 1};
            var mealProduct = new AddMealProduct { Grams = 12, Id = 1};
            command.MealProducts = new List<AddMealProduct>(){mealProduct};
            var handler = new AddMealCommandHandler(_context, _config.Object);

            //Act
            await handler.Handle(command);

            //Assert
            Assert.AreEqual(_context.Meals.Count(), 1);
        }

        [Test]
        public async Task Execute_CategoryNotExists_ReturnsError()
        {
            //Arrange
            Seed(_context);
            var command = new AddMealCommand { Name = "TestMeal", CategoryId = 20 };
            var mealProduct = new AddMealProduct { Grams = 12, Id = 1 };
            command.MealProducts = new List<AddMealProduct> { mealProduct };
            _config.Setup(c => c.GetSection(It.IsAny<string>())).Returns(new Mock<IConfigurationSection>().Object);
            var handler = new AddMealCommandHandler(_context, _config.Object);

            //Act
            var response = await handler.Handle(command);

            //Assert
            Assert.AreEqual(response.Success, false);
        }

        private void Seed(FoodContext context)
        {
            var categories = new[]
            {
                new Category() {Name = "TestCategory", Id = 1},
            };

            var products = new[]
            {
                new Product {Calories = 122, CarbsGrams = 11, FatsGrams = 22, ProteinsGrams = 33, Name = "Cheese"},
                new Product {Calories = 112, CarbsGrams = 21, FatsGrams = 33, ProteinsGrams = 23, Name = "Ham"},
            };

            context.Products.AddRange(products);
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}
