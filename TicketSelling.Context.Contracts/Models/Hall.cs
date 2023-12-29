using TicketSelling.Common.Entity.EntityInterface;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Зал
    /// </summary>
    public class Hall : BaseAuditEntity
    {
        /// <summary>
        /// Номер зала
        /// </summary>
        public short Number { get; set; }

        /// <summary>
        /// Кол-во мест
        /// </summary>
        public short NumberOfSeats { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
