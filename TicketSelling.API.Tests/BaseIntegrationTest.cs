using AutoMapper;
using TicketSelling.API.AutoMappers;
using TicketSelling.API.Tests.Infrastructures;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts;
using TicketSelling.Services.AutoMappers;
using Xunit;

namespace TicketSelling.API.Tests
{
    /// <summary>
    /// Базовый класс для тестов
    /// </summary>
    [Collection(nameof(TicketSellingApiTestCollection))]
    public class BaseIntegrationTest
    {
        protected readonly CustomWebApplicationFactory factory;
        protected readonly ITicketSellingContext context;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public BaseIntegrationTest(TicketSellingApiFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;

            Profile[] profiles = { new APIMappers(), new ServiceMapper() };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);
            });

            mapper = config.CreateMapper();
        }
    }
}
