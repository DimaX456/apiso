using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context;
using TicketSelling.Context.Contracts;
using Xunit;

namespace TicketSelling.API.Tests.Infrastructures
{
    public class TicketSellingApiFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private TicketSellingContext? ticketSellingContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketSellingApiFixture"/>
        /// </summary>
        public TicketSellingApiFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => TicketSellingContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await TicketSellingContext.Database.EnsureDeletedAsync();
            await TicketSellingContext.Database.CloseConnectionAsync();
            await TicketSellingContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        public CustomWebApplicationFactory Factory => factory;

        public ITicketSellingContext Context => TicketSellingContext;

        public IUnitOfWork UnitOfWork => TicketSellingContext;

        internal TicketSellingContext TicketSellingContext
        {
            get
            {
                if (ticketSellingContext != null)
                {
                    return ticketSellingContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                ticketSellingContext = scope.ServiceProvider.GetRequiredService<TicketSellingContext>();
                return ticketSellingContext;
            }
        }
    }
}
