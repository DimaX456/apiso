namespace TicketSelling.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности клиента
    /// </summary>
    public class ClientResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Возраст
        /// </summary>
        public short Age { get; set; }
    }
}
