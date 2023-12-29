using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="TicketModel"/>
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Получить список всех <see cref="TicketModel"/>
        /// </summary>
        Task<IEnumerable<TicketModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="TicketModel"/> по идентификатору
        /// </summary>
        Task<TicketModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый билет
        /// </summary>
        Task<TicketModel> AddAsync(TicketRequestModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий билет
        /// </summary>
        Task<TicketModel> EditAsync(TicketRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий билет
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
