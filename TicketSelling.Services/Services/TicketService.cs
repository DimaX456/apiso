using AutoMapper;
using FluentValidation;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;
using TicketSelling.Services.Contracts.ServicesContracts;

namespace TicketSelling.Services.ReadServices
{
    /// <inheritdoc cref="ITicketService"/>
    public class TicketService : ITicketService, IServiceAnchor
    {
        private readonly ITicketWriteRepository ticketWriteRepository;
        private readonly ITicketReadRepository ticketReadRepository;
        private readonly ICinemaReadRepository cinemaReadRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IHallReadRepository hallReadRepository;
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public TicketService(ITicketWriteRepository ticketWriteRepository, ITicketReadRepository ticketReadRepository, ICinemaReadRepository cinemaReadRepository,
            IClientReadRepository clientReadRepository, IFilmReadRepository filmReadRepository,
            IHallReadRepository hallReadRepository, IStaffReadRepository staffReadRepository,
            IMapper mapper, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.ticketWriteRepository = ticketWriteRepository;
            this.ticketReadRepository = ticketReadRepository;
            this.clientReadRepository = clientReadRepository;
            this.cinemaReadRepository = cinemaReadRepository;
            this.filmReadRepository = filmReadRepository;
            this.hallReadRepository = hallReadRepository;
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<TicketModel> ITicketService.AddAsync(TicketRequestModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var ticket = mapper.Map<Ticket>(model);      
            ticketWriteRepository.Add(ticket);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetTicketModelOnMapping(ticket, cancellationToken);
        }

        async Task ITicketService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetTicket = await ticketReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetTicket == null)
            {
                throw new TimeTableEntityNotFoundException<Ticket>(id);
            }

            ticketWriteRepository.Delete(targetTicket);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<TicketModel> ITicketService.EditAsync(TicketRequestModel model, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(model, cancellationToken);

            var ticket = await ticketReadRepository.GetByIdAsync(model.Id, cancellationToken);

            if (ticket == null)
            {
                throw new TimeTableEntityNotFoundException<Ticket>(model.Id);
            }

            ticket = mapper.Map<Ticket>(model);       
            ticketWriteRepository.Update(ticket);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetTicketModelOnMapping(ticket, cancellationToken);
        }

        async Task<IEnumerable<TicketModel>> ITicketService.GetAllAsync(CancellationToken cancellationToken)
        {
            var tickets = await ticketReadRepository.GetAllAsync(cancellationToken);
            var cinemas = await cinemaReadRepository
                .GetByIdsAsync(tickets.Select(x => x.CinemaId).Distinct(), cancellationToken);

            var clients = await clientReadRepository
                .GetByIdsAsync(tickets.Select(x => x.ClientId).Distinct(), cancellationToken);

            var films = await filmReadRepository
                .GetByIdsAsync(tickets.Select(x => x.FilmId).Distinct(), cancellationToken);

            var halls = await hallReadRepository
                .GetByIdsAsync(tickets.Select(x => x.HallId).Distinct(), cancellationToken);

            var staffs = await staffReadRepository
                .GetByIdsAsync(tickets.Where(x => x.StaffId.HasValue).Select(x => x.StaffId!.Value).Distinct(), cancellationToken);

            var result = new List<TicketModel>();

            foreach (var ticket in tickets)
            {
                if (!cinemas.TryGetValue(ticket.CinemaId, out var cinema) ||
                !clients.TryGetValue(ticket.ClientId, out var client) ||
                !films.TryGetValue(ticket.FilmId, out var film) ||
                !halls.TryGetValue(ticket.HallId, out var hall))
                {
                    continue;
                }
                else
                {
                    var ticketModel = mapper.Map<TicketModel>(ticket);

                    ticketModel.Hall = mapper.Map<HallModel>(hall);
                    ticketModel.Film = mapper.Map<FilmModel>(film);
                    ticketModel.Staff = ticket.StaffId.HasValue &&
                                              staffs.TryGetValue(ticket.StaffId!.Value, out var staff)
                        ? mapper.Map<StaffModel>(staff)
                        : null;
                    ticketModel.Cinema = mapper.Map<CinemaModel>(cinema);
                    ticketModel.Client = mapper.Map<ClientModel>(client);

                    result.Add(ticketModel);
                }
            }

            return result;
        }

        async Task<TicketModel?> ITicketService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await ticketReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new TimeTableEntityNotFoundException<Ticket>(id);
            }

           return await GetTicketModelOnMapping(item, cancellationToken);
        }

        async private Task<TicketModel> GetTicketModelOnMapping(Ticket ticket, CancellationToken cancellationToken)
        {
            var ticketModel = mapper.Map<TicketModel>(ticket);
            ticketModel.Cinema = mapper.Map<CinemaModel>(await cinemaReadRepository.GetByIdAsync(ticket.CinemaId, cancellationToken));
            ticketModel.Hall = mapper.Map<HallModel>(await hallReadRepository.GetByIdAsync(ticket.HallId, cancellationToken));
            ticketModel.Film = mapper.Map<FilmModel>(await filmReadRepository.GetByIdAsync(ticket.FilmId, cancellationToken));
            ticketModel.Staff = mapper.Map<StaffModel>(ticket.StaffId.HasValue
                ? await staffReadRepository.GetByIdAsync(ticket.StaffId.Value, cancellationToken)
                : null);
            ticketModel.Client = mapper.Map<ClientModel>(await clientReadRepository.GetByIdAsync(ticket.ClientId, cancellationToken));

            return ticketModel;
        }
    }
}
