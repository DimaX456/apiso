using TicketSelling.Services.Contracts.Enums;

namespace TicketSelling.Services.Contracts.Models
{
    /// <summary>
    /// Модель персонала
    /// </summary>
    public class StaffModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Должность
        /// </summary>
        public PostModel Post { get; set; } 

        /// <summary>
        /// Возраст
        /// </summary>
        public short Age { get; set; }
    }
}
