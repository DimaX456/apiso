using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.API.Tests.Infrastructures;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.API.Tests.Tests
{
    public class TicketIntergrationTests : BaseIntegrationTest
    {
        private readonly Hall hall;
        private readonly Client client;
        private readonly Cinema cinema;
        private readonly Film film;

        public TicketIntergrationTests(TicketSellingApiFixture fixture) : base(fixture)
        {
            hall = TestDataGenerator.Hall();
            client = TestDataGenerator.Client();
            cinema = TestDataGenerator.Cinema();
            film = TestDataGenerator.Film();

            context.Cinemas.Add(cinema);
            context.Films.Add(film);
            context.Halls.Add(hall);
            context.Clients.Add(client);
            unitOfWork.SaveChangesAsync();
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();

            var ticket = mapper.Map<CreateTicketRequest>(TestDataGenerator.TicketRequestModel());
            ticket.ClientId = client.Id;
            ticket.FilmId = film.Id;
            ticket.HallId = hall.Id;
            ticket.CinemaId = cinema.Id;

            // Act
            string data = JsonConvert.SerializeObject(ticket);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await clientFactory.PostAsync("/Ticket", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TicketResponse>(resultString);

            var cinemaFirst = await context.Tickets.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            cinemaFirst.Should()
                .BeEquivalentTo(ticket);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var ticket = TestDataGenerator.Ticket();

            SetDependenciesOrTicket(ticket);
            await context.Tickets.AddAsync(ticket);
            await unitOfWork.SaveChangesAsync();

            var ticketRequest = mapper.Map<TicketRequest>(TestDataGenerator.TicketRequestModel(x => x.Id = ticket.Id));
            SetDependenciesOrTicketRequestModelWithTicket(ticket, ticketRequest);

            // Act
            string data = JsonConvert.SerializeObject(ticketRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Ticket", contextdata);

            var ticketFirst = await context.Tickets.FirstAsync(x => x.Id == ticketRequest.Id);

            // Assert           
            ticketFirst.Should()
                .BeEquivalentTo(ticketRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var ticket1 = TestDataGenerator.Ticket();
            var ticket2 = TestDataGenerator.Ticket();

            SetDependenciesOrTicket(ticket1);
            SetDependenciesOrTicket(ticket2);

            await context.Tickets.AddRangeAsync(ticket1, ticket2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Ticket/{ticket1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TicketResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    ticket1.Id,
                    ticket1.Place,
                    ticket1.Price,
                    ticket1.Row
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var ticket1 = TestDataGenerator.Ticket();
            var ticket2 = TestDataGenerator.Ticket(x => x.DeletedAt = DateTimeOffset.Now);

            SetDependenciesOrTicket(ticket1);
            SetDependenciesOrTicket(ticket2);

            await context.Tickets.AddRangeAsync(ticket1, ticket2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Ticket");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<TicketResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == ticket1.Id)
                .And
                .NotContain(x => x.Id == ticket2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var ticket = TestDataGenerator.Ticket();

            SetDependenciesOrTicket(ticket);
            await context.Tickets.AddAsync(ticket);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Ticket/{ticket.Id}");

            var ticketFirst = await context.Tickets.FirstAsync(x => x.Id == ticket.Id);

            // Assert
            ticketFirst.DeletedAt.Should()
                .NotBeNull();

            ticketFirst.Should()
                .BeEquivalentTo(new
                {
                    ticket.Date,
                    ticket.Place,
                    ticket.Price,
                    ticket.Row,
                    ticket.ClientId,
                    ticket.FilmId,
                    ticket.HallId,
                    ticket.CinemaId
                });
        }

        private void SetDependenciesOrTicket(Ticket ticket)
        {
            ticket.ClientId = client.Id;
            ticket.FilmId = film.Id;
            ticket.HallId = hall.Id;
            ticket.CinemaId = cinema.Id;
        }

        private void SetDependenciesOrTicketRequestModelWithTicket(Ticket ticket, TicketRequest ticketRequest)
        {
            ticketRequest.CinemaId = ticket.CinemaId;
            ticketRequest.ClientId = ticket.ClientId;
            ticketRequest.FilmId = ticket.FilmId;
            ticketRequest.HallId = ticket.HallId;
        }
    }
}
