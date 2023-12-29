namespace TicketSelling.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности фильма
    /// </summary>
    public class FilmResponse
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
        /// Ограничение по возросту
        /// </summary>
        public short Limitation { get; set; }
    }
}
