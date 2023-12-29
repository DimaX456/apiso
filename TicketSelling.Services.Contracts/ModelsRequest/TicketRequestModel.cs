namespace TicketSelling.Services.Contracts.ModelsRequest
{
    /// <summary>
    /// Модель запроса создания билета
    /// </summary>
    public class TicketRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор Зала
        /// </summary>
        public Guid HallId { get; set; }

        /// <summary>
        /// Идентификатор Кинотеатра
        /// </summary>
        public Guid CinemaId { get; set; }

        /// <summary>
        /// Идентификатор Фильма
        /// </summary>
        public Guid FilmId { get; set; }

        /// <summary>
        /// Идентификатор Клиента
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Идентификатор сотрудника, продавшего билет 
        /// </summary>
        public Guid? StaffId { get; set; }

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
