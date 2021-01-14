using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.Meals;
using FitLife.DB.Context;
using FitLife.DB.Models.Food;
using FitLife.Infrastructure.CommandHandlers.Meals;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FitLife.Tests.CommandHandler.Meals
{
    class EditMealCommandHandlerTests
    {
        private DbContextOptions<FoodContext> _options;
        private FoodContext _context;
        private Mock<ILogger<EditMealCommandHandler>> _logger;
        private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FoodContext>().UseInMemoryDatabase(databaseName: "FitLifeInMemory").Options;
            _context = new FoodContext(_options);
            _logger = new Mock<ILogger<EditMealCommandHandler>>();
            _config = new Mock<IConfiguration>();
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Execute_CorrectCommand_UpdatesMeal()
        {
            //Arrange
            Seed(_context);
            var command = new EditMealCommand {Name = "TestMeal", CategoryId = 1, Id = 1};
            var mealProducts = new[]
                {new EditMealProduct {Grams = 12, Id = 1}, new EditMealProduct {Grams = 22, Id = 2} , new EditMealProduct {Grams = 22, Id = 3}};
            command.MealProducts = mealProducts;
            var validator = new Mock<AbstractValidator<EditMealCommand>>();
            validator.Setup(x => x.Validate(It.IsAny<ValidationContext<EditMealCommand>>())).Returns(new ValidationResult());
            var handler = new EditMealCommandHandler(_context,_config.Object, _logger.Object, validator.Object);

            //Act
            await handler.Handle(command);

            //Assert
            var meal = _context.Meals.First();
            Assert.AreEqual(meal.MealProducts.Count, 3);
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


            var meals = new[]
            {
                new Meal { Name = "TestMeal", Category = categories.First() },
            };

            context.Products.AddRange(products);
            context.Categories.AddRange(categories);
            context.Meals.AddRange(meals);
            context.SaveChanges();
        }
    }
}
