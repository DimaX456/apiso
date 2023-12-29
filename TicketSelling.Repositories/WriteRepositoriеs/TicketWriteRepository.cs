using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    /// <summary>
    /// Реализация <see cref="ITicketWriteRepository"/>
    /// </summary>
    public class TicketWriteRepository : BaseWriteRepository<Ticket>, ITicketWriteRepository, IRepositoryAnchor
    {
        public TicketWriteRepository(IDbWriterContext writerContext) 
            : base(writerContext)
        {

        }
    }
}
