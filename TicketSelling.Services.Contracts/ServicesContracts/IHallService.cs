using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="HallModel"/>
    /// </summary>
    public interface IHallService
    {
        /// <summary>
        /// Получить список всех <see cref="HallModel"/>
        /// </summary>
        Task<IEnumerable<HallModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="HallModel"/> по идентификатору
        /// </summary>
        Task<HallModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый зал
        /// </summary>
        Task<HallModel> AddAsync(HallModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий зал
        /// </summary>
        Task<HallModel> EditAsync(HallModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий зал
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    }
}
