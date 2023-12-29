namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания кинотеатра
    /// </summary>
    public class CreateCinemaRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
