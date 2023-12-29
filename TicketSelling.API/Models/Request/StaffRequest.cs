namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания сотрудника
    /// </summary>
    public class StaffRequest : CreateStaffRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
