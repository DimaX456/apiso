namespace TicketSelling.Services.Contracts.Models
{
    /// <summary>
    /// Модель фильма
    /// </summary>
    public class FilmModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Ограничение по возросту
        /// </summary>
        public short Limitation { get; set; }
    }
}
