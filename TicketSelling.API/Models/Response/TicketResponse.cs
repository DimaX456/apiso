namespace TicketSelling.API.Models.Response
{
    public class TicketResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Сущность кинотеатра
        /// </summary>
        public CinemaResponse? Cinema { get; set; }

        /// <summary>
        /// Сущность клиента
        /// </summary>
        public ClientResponse? Client { get; set; }

        /// <summary>
        /// Сущность фильма
        /// </summary>
        public FilmResponse? Film { get; set; }

        /// <summary>
        /// Сущность зала
        /// </summary>
        public HallResponse? Hall { get; set; }

        /// <summary>
        /// Сущность персонала
        /// </summary>
        public StaffResponse? Staff { get; set; }

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
        public string Date { get; set; } = string.Empty;
    }
}
