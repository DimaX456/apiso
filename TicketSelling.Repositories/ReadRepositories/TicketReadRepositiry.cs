using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Common.Entity.Repositories;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="ITicketReadRepository"/>
    /// </summary>
    public class TicketReadRepositiry : ITicketReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public TicketReadRepositiry(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Ticket>> ITicketReadRepository.GetAllAsync(CancellationToken cancellationToken)     
            =>reader.Read<Ticket>()
                .NotDeletedAt()
                .OrderBy(x => x.Date)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Ticket?> ITicketReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Ticket>()
                .ById(id)
                .NotDeletedAt()
                .FirstOrDefaultAsync(cancellationToken);

        Task<bool> ITicketReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Ticket>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
