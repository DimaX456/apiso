namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания зала
    /// </summary>
    public class HallRequest : CreateHallRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
