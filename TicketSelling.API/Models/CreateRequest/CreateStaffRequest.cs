using TicketSelling.API.Enums;

namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания сотрудника
    /// </summary>
    public class CreateStaffRequest
    {
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
        /// Возраст
        /// </summary>
        public short Age { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public PostResponse Post { get; set; }
    }
}
