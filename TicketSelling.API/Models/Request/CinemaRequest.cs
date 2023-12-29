namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания кинотеатра
    /// </summary>
    public class CinemaRequest : CreateCinemaRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
