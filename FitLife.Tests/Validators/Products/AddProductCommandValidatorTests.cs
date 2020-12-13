using FitLife.Contracts.Request.Command.Products;
using FitLife.Infrastructure.Validators.Products;
using NUnit.Framework;

namespace FitLife.Tests.Validators.Products
{
    public class AddProductCommandValidatorTests
    {
        private AddProductCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AddProductCommandValidator();
        }

        [Test]
        public void Validate_CorrectValues_ValidatesCommand()
        {
            //Arrange
            var query = new AddProductCommand { CarbsGrams = 12, FatsGrams = 10, ProteinsGrams = 22, Name = "TestProduct" };

            //Act
            var validationResult = _validator.Validate(query);

            //Assert
            Assert.AreEqual(validationResult.IsValid, true);
        }

        [Test]
        public void Validate_SumOfMacronutrientsGreaterThan100_ValidatesIncorrectCommand()
        {
            //Arrange
            var query = new AddProductCommand {CarbsGrams = 40, FatsGrams = 40, ProteinsGrams = 40, Name = "TestProduct"};

            //Act
            var validationResult = _validator.Validate(query);

            //Assert
            Assert.AreEqual(validationResult.IsValid, false);
        }

        [Test]
        public void Validate_MacronutrientGreaterThan100_ValidatesIncorrectCommand()
        {
            //Arrange
            var query = new AddProductCommand { CarbsGrams = 140, FatsGrams = 10, ProteinsGrams = 5, Name = "TestProduct" };

            //Act
            var validationResult = _validator.Validate(query);

            //Assert
            Assert.AreEqual(validationResult.IsValid, false);

        }

        [Test]
        public void Validate_NameWithNumbers_ValidatesIncorrectCommand()
        {
            //Arrange
            var query = new AddProductCommand { CarbsGrams = 40, FatsGrams = 40, ProteinsGrams = 40, Name = "TestProduct123" };

            //Act
            var validationResult = _validator.Validate(query);

            //Assert
            Assert.AreEqual(validationResult.IsValid, false);
        }
    }
}
