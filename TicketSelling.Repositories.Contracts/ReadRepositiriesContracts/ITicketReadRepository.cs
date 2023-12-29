using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Ticket"/>
    /// </summary>
    public interface ITicketReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Ticket"/>
        /// </summary>
        Task<IReadOnlyCollection<Ticket>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Ticket"/> по идентификатору
        /// </summary>
        Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Ticket"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
