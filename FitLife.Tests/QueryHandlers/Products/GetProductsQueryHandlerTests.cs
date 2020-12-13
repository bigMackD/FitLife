using System.Linq;
using FitLife.Contracts.Request.Query.Products;
using FitLife.DB.Context;
using FitLife.DB.Models.Food;
using FitLife.Infrastructure.QueryHandlers.Products;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace FitLife.Tests.QueryHandlers.Products
{
    public class GetProductsQueryHandlerTests
    {
        private DbContextOptions<FoodContext> _options;
        private FoodContext _context;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<FoodContext>().UseInMemoryDatabase(databaseName: "FitLifeInMemory").Options;
            _context = new FoodContext(_options);
        }

        [Test]
        public void Execute_CorrectPagination_ReturnsProducts()
        {
            //Arrange
            Seed(_context);
            var query = new GetProductsQuery { PageIndex = 0, PageSize = 25 };
            var logger = new Mock<ILogger<GetProductsQueryHandler>>();
            var config = new Mock<IConfiguration>();
            var validator = new Mock<AbstractValidator<GetProductsQuery>>();
            validator.Setup(x => x.Validate(It.IsAny<ValidationContext<GetProductsQuery>>())).Returns(new ValidationResult());
            var handler = new GetProductsQueryHandler(_context, config.Object, logger.Object, validator.Object);

            //Act
            var result = handler.Handle(query);
            var mockedDbCount = _context.Products.Count();

            //Assert
            Assert.AreEqual(mockedDbCount, result.Count);

        }

        private void Seed(FoodContext context)
        {
            var products = new[]
            {
                new Product {Calories = 122, CarbsGrams = 11, FatsGrams = 22, ProteinsGrams = 33, Name = "Cheese"},
                new Product {Calories = 112, CarbsGrams = 21, FatsGrams = 33, ProteinsGrams = 23, Name = "Ham"},
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}