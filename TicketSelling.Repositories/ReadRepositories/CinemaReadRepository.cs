using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Common.Entity.Repositories;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="ICinemaReadRepository"/>
    /// </summary>
    public class CinemaReadRepository : ICinemaReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
        private IDbRead reader;

        public CinemaReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Cinema>> ICinemaReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Cinema>()
                .NotDeletedAt()
                .OrderBy(x=> x.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Cinema?> ICinemaReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Cinema>()
                .ById(id)
                .NotDeletedAt()
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Cinema>> ICinemaReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Cinema>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> ICinemaReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Cinema>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
