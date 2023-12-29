namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания фильма
    /// </summary>
    public class FilmRequest : CreateFilmRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
