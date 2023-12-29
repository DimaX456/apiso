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
    /// <inheritdoc cref="ICinemaService"/>
    public class CinemaService : ICinemaService, IServiceAnchor
    {
        private readonly ICinemaReadRepository cinemaReadRepositiry;
        private readonly ICinemaWriteRepository cinemaWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public CinemaService(ICinemaReadRepository cinemaReadRepositiry, IMapper mapper, 
            ICinemaWriteRepository cinemaWriteRepository, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.cinemaReadRepositiry = cinemaReadRepositiry;
            this.mapper = mapper;
            this.cinemaWriteRepository = cinemaWriteRepository;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<CinemaModel> ICinemaService.AddAsync(CinemaModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();

            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Cinema>(model); 
            cinemaWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CinemaModel>(item);
        }

        async Task ICinemaService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetCinema = await cinemaReadRepositiry.GetByIdAsync(id, cancellationToken);

            if (targetCinema == null)
            {
                throw new TimeTableEntityNotFoundException<Cinema>(id);
            }
         
            cinemaWriteRepository.Delete(targetCinema);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<CinemaModel> ICinemaService.EditAsync(CinemaModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetCinema = await cinemaReadRepositiry.GetByIdAsync(source.Id, cancellationToken);

            if (targetCinema == null)
            {
                throw new TimeTableEntityNotFoundException<Cinema>(source.Id);
            }

            targetCinema = mapper.Map<Cinema>(source);
            cinemaWriteRepository.Update(targetCinema);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CinemaModel>(targetCinema);
        }

        async Task<IEnumerable<CinemaModel>> ICinemaService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await cinemaReadRepositiry.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<CinemaModel>(x));
        }

        async Task<CinemaModel?> ICinemaService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
           var item = await cinemaReadRepositiry.GetByIdAsync(id, cancellationToken);

           if(item == null) 
           {
                throw new TimeTableEntityNotFoundException<Cinema>(id);
           }

           return mapper.Map<CinemaModel>(item);
        } 
    }
}
