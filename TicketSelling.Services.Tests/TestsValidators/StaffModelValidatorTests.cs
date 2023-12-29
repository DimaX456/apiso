using FluentValidation.TestHelper;
using TicketSelling.Services.Contracts.Enums;
using TicketSelling.Services.Validator.Validators;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.Services.Tests.TestsValidators
{
    public class StaffModelValidatorTests
    {
        private readonly StaffModelValidator validator;

        public StaffModelValidatorTests()
        {
            validator = new StaffModelValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.StaffModel(x => { x.FirstName = "1"; x.LastName = "1"; x.Patronymic = "1"; x.Age = -1; x.Post = (PostModel)33; });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.StaffModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
