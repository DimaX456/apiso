using FluentValidation;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="HallModel"/>
    /// </summary>
    public class HallModelValidator : AbstractValidator<HallModel>
    {
        public HallModelValidator()
        {
            RuleFor(x => (int)x.Number)
              .InclusiveBetween(1, 999).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => (int)x.NumberOfSeats)
              .InclusiveBetween(15, 200).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
