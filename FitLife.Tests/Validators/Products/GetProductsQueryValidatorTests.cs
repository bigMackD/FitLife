using FitLife.Contracts.Request.Query.Products;
using FitLife.Infrastructure.Validators.Products;
using NUnit.Framework;

namespace FitLife.Tests.Validators.Products
{
    public class GetProductsQueryValidatorTests
    {
        private GetProductsQueryValidator _validator;

        [SetUp]
        public void Setup()
        {
          _validator = new GetProductsQueryValidator();
        }


        [Test]
        public void Validate_PageSizeZero_ValidatesIncorrectQuery()
        {
            //Arrange
            var query = new GetProductsQuery {PageIndex = 2, PageSize = 0};

            //Act
            var validationResult = _validator.Validate(query);

            //Assert
            Assert.AreEqual(validationResult.IsValid, false);

        }

        [Test]
        public void Validate_PageIndexNegative_ValidatesIncorrectQuery()
        {
            //Arrange
            var query = new GetProductsQuery { PageIndex = -1, PageSize = 25 };

            //Act
            var validationResult = _validator.Validate(query);

            //Assert
            Assert.AreEqual(validationResult.IsValid, false);

        }

        [Test]
        public void Validate_PageIndexAndPageSizeIncorrect_ValidatesIncorrectQuery()
        {
            //Arrange
            var query = new GetProductsQuery { PageIndex = -1, PageSize = 0 };

            //Act
            var validationResult = _validator.Validate(query);

            //Assert
            Assert.AreEqual(validationResult.IsValid, false);

        }
    }
}
