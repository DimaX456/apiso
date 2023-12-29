using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    public class TicketIEntityTypeConfiguration : IEntityTypeConfiguration<Ticket>
    {
        /// <summary>
        /// Конфигурация для <see cref="Ticket"/>
        /// </summary>
        void IEntityTypeConfiguration<Ticket>.Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Date).IsRequired();
            builder.HasIndex(x => x.Date).HasDatabaseName($"{nameof(Ticket)}_{nameof(Ticket.Date)}")
                .HasFilter($"{nameof(Ticket.DeletedAt)} is null");
            builder.Property(x => x.Place).IsRequired();
            builder.Property(x => x.Row).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.HallId).IsRequired();
            builder.Property(x => x.CinemaId).IsRequired();
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.FilmId).IsRequired();
        }
    }
}
