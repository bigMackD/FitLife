using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitLife.Contracts.Request.Command.UserMeal;
using FitLife.DB.Context;
using FitLife.DB.Models.Food;
using FitLife.Infrastructure.CommandHandlers.UserMeals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace FitLife.Tests.CommandHandler.UserMeals
{ 
    class DeleteUserMealsCommandHandlerTests
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
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task Execute_CorrectCommand_RemovesUserMeals()
        {
            //Arrange
            Seed(_context);
            var handler = new DeleteUserMealsCommandHandler(_config.Object, _context);
            var command = new DeleteUserMealsCommand {Ids = new List<int> {1}};

            //Act
            await handler.Handle(command);

            //Assert
            Assert.AreEqual(1,_context.UserMeals.Count());
        }

        private void Seed(FoodContext context)
        {
            var userMeals = new[]
            {
                new UserMeal {CategoryId = 1,MealId = 2, UserMealId = 1},
                new UserMeal {CategoryId = 2,MealId = 3, UserMealId = 2},
            };

            context.UserMeals.AddRange(userMeals);
            context.SaveChanges();
        }
    }
}
