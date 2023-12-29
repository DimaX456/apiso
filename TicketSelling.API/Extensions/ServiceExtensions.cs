using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using TicketSelling.API.AutoMappers;
using TicketSelling.Common.Entity;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context;
using TicketSelling.Repositories;
using TicketSelling.Services;
using TicketSelling.Services.AutoMappers;

namespace TicketSelling.API.Extensions
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Регистрирует все сервисы, репозитории и все что нужно для контекста
        /// </summary>
        public static void RegistrationSRC(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDbWriterContext, DbWriterContext>();
            services.RegistrationService();
            services.RegistrationRepository();
            services.RegistrationContext();
            services.AddAutoMapper(typeof(APIMappers), typeof(ServiceMapper));
        }

        /// <summary>
        /// Включает фильтры и ставит шрифт на перечесления
        /// </summary>
        /// <param name="services"></param>
        public static void RegistrationControllers(this IServiceCollection services)
        {
            services.AddControllers(x =>
            {
                x.Filters.Add<TicketSellingExceptionFilter>();
            })
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        CamelCaseText = false
                    });
                })
                .AddControllersAsServices();
        }

        /// <summary>
        /// Настройки свагера
        /// </summary>
        public static void RegistrationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Cinema", new OpenApiInfo { Title = "Кинотетры", Version = "v1" });
                c.SwaggerDoc("Client", new OpenApiInfo { Title = "Клиенты", Version = "v1" });
                c.SwaggerDoc("Film", new OpenApiInfo { Title = "Фильмы", Version = "v1" });
                c.SwaggerDoc("Hall", new OpenApiInfo { Title = "Залы", Version = "v1" });
                c.SwaggerDoc("Staff", new OpenApiInfo { Title = "Персонал", Version = "v1" });
                c.SwaggerDoc("Ticket", new OpenApiInfo { Title = "Билеты", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "TicketSelling.API.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// Настройки свагера
        /// </summary>
        public static void CustomizeSwaggerUI(this WebApplication web)
        {
            web.UseSwagger();
            web.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Cinema/swagger.json", "Кинотеатры");
                x.SwaggerEndpoint("Client/swagger.json", "Клиенты");
                x.SwaggerEndpoint("Film/swagger.json", "Фильмы");
                x.SwaggerEndpoint("Hall/swagger.json", "Залы");
                x.SwaggerEndpoint("Staff/swagger.json", "Работники");
                x.SwaggerEndpoint("Ticket/swagger.json", "Билеты");
            });
        }
    }
}
