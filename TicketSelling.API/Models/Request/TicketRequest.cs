namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания билета
    /// </summary>
    public class TicketRequest : CreateTicketRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
