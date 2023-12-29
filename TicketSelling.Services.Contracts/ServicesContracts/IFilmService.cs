using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="FilmModel"/>
    /// </summary>
    public interface IFilmService
    {
        /// <summary>
        /// Получить список всех <see cref="FilmModel"/>
        /// </summary>
        Task<IEnumerable<FilmModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="FilmModel"/> по идентификатору
        /// </summary>
        Task<FilmModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый фильм
        /// </summary>
        Task<FilmModel> AddAsync(FilmModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий фильм
        /// </summary>
        Task<FilmModel> EditAsync(FilmModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий фильм
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    }
}
