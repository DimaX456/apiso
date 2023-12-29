using FluentValidation.TestHelper;
using TicketSelling.Context.Tests;
using TicketSelling.Repositories.ReadRepositories;
using TicketSelling.Services.Validator.Validators;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.Services.Tests.TestsValidators
{
    public class TicketRequestValidatorTests : TicketSellingContextInMemory
    {
        private readonly TicketRequestValidator validator;

        public TicketRequestValidatorTests()
        {
            validator = new TicketRequestValidator(new CinemaReadRepository(Reader), new ClientReadRepository(Reader), 
                new FilmReadRepository(Reader), new HallReadRepository(Reader));
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.TicketRequestModel(x => { x.Row = -1; x.Date = DateTimeOffset.Now; x.Price = 0; x.Place = -1;});
            model.ClientId = Guid.NewGuid();
            model.CinemaId = Guid.NewGuid();
            model.FilmId = Guid.NewGuid();
            model.HallId = Guid.NewGuid();
            model.StaffId = Guid.Empty;

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        async public void ValidatorShouldSuccess()
        {
            //Arrange
            var cinema = TestDataGenerator.Cinema();
            var film = TestDataGenerator.Film();
            var hall = TestDataGenerator.Hall();
            var client = TestDataGenerator.Client();

            await Context.Cinemas.AddAsync(cinema);
            await Context.Films.AddAsync(film);
            await Context.Halls.AddAsync(hall);
            await Context.Clients.AddAsync(client);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.TicketRequestModel();
            model.ClientId = client.Id;
            model.FilmId = film.Id;
            model.HallId = hall.Id;
            model.CinemaId = cinema.Id;
            model.StaffId = Guid.Empty;

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
