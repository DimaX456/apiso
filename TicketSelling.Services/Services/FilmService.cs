using AutoMapper;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ServicesContracts;

namespace TicketSelling.Services.ReadServices
{
    /// <inheritdoc cref="IFilmService"/>
    public class FilmService : IFilmService, IServiceAnchor
    {
        private readonly IFilmWriteRepository filmWriteRepository;
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public FilmService(IFilmWriteRepository filmWriteRepository, IFilmReadRepository filmReadRepository, 
            IMapper mapper, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.filmWriteRepository = filmWriteRepository;
            this.filmReadRepository = filmReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<FilmModel> IFilmService.AddAsync(FilmModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Film>(model);

            filmWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<FilmModel>(item);
        }

        async Task IFilmService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetFilm = await filmReadRepository.GetByIdAsync(id, cancellationToken);

            if(targetFilm == null)
            {
                throw new TimeTableEntityNotFoundException<Film>(id);
            }

            filmWriteRepository.Delete(targetFilm);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<FilmModel> IFilmService.EditAsync(FilmModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetFilm = await filmReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetFilm == null)
            {
                throw new TimeTableEntityNotFoundException<Film>(source.Id);
            }

            targetFilm = mapper.Map<Film>(source);

            filmWriteRepository.Update(targetFilm);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<FilmModel>(targetFilm);
        }

        async Task<IEnumerable<FilmModel>> IFilmService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await filmReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<FilmModel>(x));
        }

        async Task<FilmModel?> IFilmService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await filmReadRepository.GetByIdAsync(id, cancellationToken);

            if(item == null) 
            {
                throw new TimeTableEntityNotFoundException<Film>(id);
            }

            return mapper.Map<FilmModel>(item);
        }
    }
}
