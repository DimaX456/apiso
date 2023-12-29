using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    /// <summary>
    /// Реализация <see cref="IFilmWriteRepository"/>
    /// </summary>
    public class FilmWriteRepository : BaseWriteRepository<Film>, IFilmWriteRepository, IRepositoryAnchor
    {
        public FilmWriteRepository(IDbWriterContext writerContext) 
            : base(writerContext)
        {
            
        }
    }
}
