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
    public class HallIntergrationTests : BaseIntegrationTest
    {
        public HallIntergrationTests(TicketSellingApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var hall = mapper.Map<CreateHallRequest>(TestDataGenerator.HallModel());

            // Act
            string data = JsonConvert.SerializeObject(hall);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Hall", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<HallResponse>(resultString);

            var hallFirst = await context.Halls.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            hallFirst.Should()
                .BeEquivalentTo(hall);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var hall = TestDataGenerator.Hall();
            await context.Halls.AddAsync(hall);
            await unitOfWork.SaveChangesAsync();

            var hallRequest = mapper.Map<HallRequest>(TestDataGenerator.HallModel(x => x.Id = hall.Id));

            // Act
            string data = JsonConvert.SerializeObject(hallRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Hall", contextdata);

            var hallFirst = await context.Halls.FirstAsync(x => x.Id == hallRequest.Id);

            // Assert           
            hallFirst.Should()
                .BeEquivalentTo(hallRequest);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var hall = TestDataGenerator.Hall();
            await context.Halls.AddAsync(hall);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Hall/{hall.Id}");

            var hallFirst = await context.Halls.FirstAsync(x => x.Id == hall.Id);

            // Assert
            hallFirst.DeletedAt.Should()
                .NotBeNull();

            hallFirst.Should()
            .BeEquivalentTo(new
            {
                hall.Number,
                hall.NumberOfSeats
            });
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var hall1 = TestDataGenerator.Hall();

            await context.Halls.AddRangeAsync(hall1);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Hall/{hall1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<HallResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    hall1.Id,
                    hall1.NumberOfSeats,
                    hall1.Number
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var hall1 = TestDataGenerator.Hall();
            var hall2 = TestDataGenerator.Hall(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Halls.AddRangeAsync(hall1, hall2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Hall");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<HallResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == hall1.Id);

            result.Should()
                .NotBeNull()
                .And
                .NotContain(x => x.Id == hall2.Id);
        }
    }
}
