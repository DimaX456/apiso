namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания зала
    /// </summary>
    public class CreateHallRequest
    {
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
