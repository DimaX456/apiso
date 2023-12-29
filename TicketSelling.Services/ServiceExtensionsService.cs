using Microsoft.Extensions.DependencyInjection;
using TicketSelling.General;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.ServicesContracts;
using TicketSelling.Services.Services;

namespace TicketSelling.Services
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsService
    {
        /// <summary>
        /// Регистрация всех сервисов и валидатора
        /// </summary>
        public static void RegistrationService(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
            service.AddTransient<IServiceValidatorService, ServicesValidatorService>();
        }       
    }
}
