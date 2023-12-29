namespace TicketSelling.Services.Contracts.Models
{
    /// <summary>
    /// Модель кинотеатра
    /// </summary>
    public class CinemaModel
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
        /// Адрес
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
