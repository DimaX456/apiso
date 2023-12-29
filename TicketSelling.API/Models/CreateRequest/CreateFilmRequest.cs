namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания фильма
    /// </summary>
    public class CreateFilmRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Ограничение по возросту
        /// </summary>
        public short Limitation { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
    }
}
