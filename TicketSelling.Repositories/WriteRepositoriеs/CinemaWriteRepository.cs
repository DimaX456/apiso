using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    /// <summary>
    /// Реализация <see cref="ICinemaWriteRepository"/>
    /// </summary>
    public class CinemaWriteRepository : BaseWriteRepository<Cinema>, ICinemaWriteRepository, IRepositoryAnchor
    {
        public CinemaWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {

        }
    }
}
