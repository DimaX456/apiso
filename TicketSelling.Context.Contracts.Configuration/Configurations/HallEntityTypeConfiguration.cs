using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Hall"/>
    /// </summary>
    public class HallEntityTypeConfiguration : IEntityTypeConfiguration<Hall>
    {
        void IEntityTypeConfiguration<Hall>.Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.ToTable("Halls");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Number).IsRequired();
            builder.HasIndex(x => x.Number)
                .IsUnique()
                .HasDatabaseName($"{nameof(Hall)}_{nameof(Hall.Number)}")
                .HasFilter($"{nameof(Hall.DeletedAt)} is null");
            builder.HasMany(x => x.Tickets).WithOne(x => x.Hall).HasForeignKey(x => x.HallId);
        }
    }
}
