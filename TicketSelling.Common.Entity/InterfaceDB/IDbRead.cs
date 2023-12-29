using TicketSelling.Common.Entity.EntityInterface;

namespace TicketSelling.Common.Entity.InterfaceDB
{
    /// <summary>
    /// Интерфейс чтения БД
    /// </summary>
    public interface IDbRead
    {
        /// <summary>
        /// Предоставляет функциональные возможности для выполнения запросов
        /// </summary> 
        IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity;
    }
}
