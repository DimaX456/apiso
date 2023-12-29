using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.API.Tests.Infrastructures;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.API.Tests.Tests
{
    /// <summary>
    /// Переделаю
    /// </summary>
    public class FilmIntergrationTests : BaseIntegrationTest
    {
        public FilmIntergrationTests(TicketSellingApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var film = mapper.Map<CreateFilmRequest>(TestDataGenerator.FilmModel());

            // Act
            string data = JsonConvert.SerializeObject(film);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Film", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmResponse>(resultString);

            var filmFirst = await context.Films.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            filmFirst.Should()
                .BeEquivalentTo(film);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var film = TestDataGenerator.Film();
            await context.Films.AddAsync(film);
            await unitOfWork.SaveChangesAsync();

            var filmRequest = mapper.Map<FilmRequest>(TestDataGenerator.FilmModel(x => x.Id = film.Id));

            // Act
            string data = JsonConvert.SerializeObject(filmRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Film", contextdata);

            var filmFirst = await context.Films.FirstAsync(x => x.Id == filmRequest.Id);

            // Assert           
            filmFirst.Should()
                .BeEquivalentTo(filmRequest);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var film = TestDataGenerator.Film();
            await context.Films.AddAsync(film);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Film/{film.Id}");

            var filmFirst = await context.Films.FirstAsync(x => x.Id == film.Id);

            // Assert
            filmFirst.DeletedAt.Should()
                .NotBeNull();

            filmFirst.Should()
            .BeEquivalentTo(new
            {
                film.Title,
                film.Description
            });
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var film1 = TestDataGenerator.Film();
            var film2 = TestDataGenerator.Film();

            await context.Films.AddRangeAsync(film1, film2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Film/{film1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    film1.Id,
                    film1.Title
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var film1 = TestDataGenerator.Film();
            var film2 = TestDataGenerator.Film(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Films.AddRangeAsync(film1, film2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Film");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<FilmResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == film1.Id);

            result.Should()
                .NotBeNull()
                .And
                .NotContain(x => x.Id == film2.Id);
        }
    }
}
