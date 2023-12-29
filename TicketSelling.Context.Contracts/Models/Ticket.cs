using TicketSelling.Common.Entity.EntityInterface;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Билет
    /// </summary>
    public class Ticket : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор Зала
        /// </summary>
        public Guid HallId { get; set; }
        public Hall Hall { get; set; }

        /// <summary>
        /// Идентификатор Кинотеатра
        /// </summary>
        public Guid CinemaId { get; set; }
        public Cinema Cinema { get; set; }

        /// <summary>
        /// Идентификатор Фильма
        /// </summary>
        public Guid FilmId { get; set; }
        public Film Film { get; set; }

        /// <summary>
        /// Идентификатор Клиента
        /// </summary>
        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        /// <summary>
        /// Идентификатор завхоза
        /// </summary>
        public Guid? StaffId { get; set; }
        public Staff? Staff { get; set; }

        /// <summary>
        /// Ряд
        /// </summary>
        public short Row { get; set; }

        /// <summary>
        /// Место
        /// </summary>
        public short Place { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата и врремя проведения фильма
        /// </summary>
        public DateTimeOffset Date { get; set; }
    }
}
