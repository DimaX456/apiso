using AutoMapper;
using FluentAssertions;
using FluentAssertions.Extensions;
using TicketSelling.API.AutoMappers;
using Xunit;

namespace TicketSelling.API.Tests.Tests
{
    public class MapperTests
    {
        [Fact]
        public void TestMapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<APIMappers>());
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void TestValidate()
        {
            var item = 1.March(2022).At(20, 30).AsLocal();
            var item2 = 2.March(2022).At(20, 30).AsLocal();
            item.Should().NotBe(item2);
        }
    }
}