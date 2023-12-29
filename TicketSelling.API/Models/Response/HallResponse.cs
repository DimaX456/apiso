namespace TicketSelling.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности зала
    /// </summary>
    public class HallResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер зала
        /// </summary>
        public short Number { get; set; }

        /// <summary>
        /// Кол-во мест
        /// </summary>
        public short NumberOfSeats { get; set; }
    }
}
