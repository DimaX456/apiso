using Xunit;

namespace TicketSelling.API.Tests.Infrastructures
{
    [CollectionDefinition(nameof(TicketSellingApiTestCollection))]
    public class TicketSellingApiTestCollection
        : ICollectionFixture<TicketSellingApiFixture>
    {
    }
}
