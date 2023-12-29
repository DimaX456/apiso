using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Hall"/>
    /// </summary>
    public interface IHallReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Hall"/>
        /// </summary>
        Task<IReadOnlyCollection<Hall>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Hall"/> по идентификатору
        /// </summary>
        Task<Hall?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Hall"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Hall>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Hall"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);

    }
}
