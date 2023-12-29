using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Client"/>
    /// </summary>
    public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
    {
        void IEntityTypeConfiguration<Client>.Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.FirstName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Patronymic).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasDatabaseName($"{nameof(Client)}_{nameof(Client.Email)}")
                .HasFilter($"{nameof(Client.DeletedAt)} is null");
            builder.Property(x => x.Age).IsRequired();
            builder.HasMany(x => x.Tickets).WithOne(x => x.Client).HasForeignKey(x => x.ClientId);
        }
    }
}
