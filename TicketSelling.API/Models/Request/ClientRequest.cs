namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания клиента
    /// </summary>
    public class ClientRequest : CreateClientRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
