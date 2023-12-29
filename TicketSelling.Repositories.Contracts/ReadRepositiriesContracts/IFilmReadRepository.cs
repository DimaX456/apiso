using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Film"/>
    /// </summary>
    public interface IFilmReadRepository
    {       
        /// <summary>
        /// Получить список всех <see cref="Film"/>
        /// </summary>
        Task<IReadOnlyCollection<Film>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Film"/> по идентификатору
        /// </summary>
        Task<Film?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Film"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Film>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Film"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
