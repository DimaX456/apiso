namespace TicketSelling.Services.Contracts.Models
{
    /// <summary>
    /// Модель билета
    /// </summary>
    public class TicketModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор Зала
        /// </summary>
        public HallModel Hall { get; set; }

        /// <summary>
        /// Идентификатор Кинотеатра
        /// </summary>
        public CinemaModel Cinema { get; set; }

        /// <summary>
        /// Идентификатор Фильма
        /// </summary>
        public FilmModel Film{ get; set; }

        /// <summary>
        /// Идентификатор Клиента
        /// </summary>
        public ClientModel Client { get; set; }

        /// <summary>
        /// Идентификатор завхоза
        /// </summary>
        public StaffModel? Staff { get; set; }

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
