using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts;
using TicketSelling.Context.Contracts.Configuration.Configurations;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context
{
    /// <summary>
    /// Контекст работы с БД
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet-ef --version 6.0.0
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project TicketSelling.Context\TicketSelling.Context.csproj
    /// 4) dotnet ef database update --project TicketSelling.Context\TicketSelling.Context.csproj
    /// 5) dotnet ef database update [targetMigrationName] --TicketSelling.Context\TicketSelling.Context.csproj
    /// </remarks>
    public class TicketSellingContext : DbContext, ITicketSellingContext, IDbRead, IDbWriter, IUnitOfWork
    {
        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Hall> Halls { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public TicketSellingContext(DbContextOptions<TicketSellingContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaEntityTypeConfiguration).Assembly);
        }

        /// <summary>
        /// Сохранение изменений в БД
        /// </summary>
        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }

        /// <summary>
        /// Чтение сущностей из БД
        /// </summary>
        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        /// <summary>
        /// Запись сущности в БД
        /// </summary>
        void IDbWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        /// <summary>
        /// Обновление сущностей
        /// </summary>
        void IDbWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        /// <summary>
        /// Удаление сущности из БД
        /// </summary>
        void IDbWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;
    }
}
