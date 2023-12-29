using FluentValidation;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="TicketRequestModel"/>
    /// </summary>
    public class TicketRequestValidator : AbstractValidator<TicketRequestModel>
    {
        private readonly ICinemaReadRepository cinemaReadRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IFilmReadRepository filmReadRepository;
        private readonly IHallReadRepository hallReadRepository;

        public TicketRequestValidator(ICinemaReadRepository cinemaReadRepository, IClientReadRepository clientReadRepository,
            IFilmReadRepository filmReadRepository, IHallReadRepository hallReadRepository)
        {
            this.cinemaReadRepository = cinemaReadRepository;
            this.clientReadRepository = clientReadRepository;
            this.filmReadRepository = filmReadRepository;
            this.hallReadRepository = hallReadRepository;

            RuleFor(x => x.StaffId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .When(x => x.StaffId.HasValue);

            RuleFor(x => x.HallId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.hallReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.CinemaId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.cinemaReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.clientReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.FilmId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.filmReadRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .GreaterThan(DateTimeOffset.Now.AddMinutes(1)).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => (int)x.Place)
                .InclusiveBetween(1, 200).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => (int)x.Row)
                .InclusiveBetween(1, 50).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.Price)
                .InclusiveBetween(100, 5000).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
