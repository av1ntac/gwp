using FluentValidation;
using WebApi.DTO;

namespace WebApi.Validators
{
    public class CountryGwpRequestValidator: AbstractValidator<CountryGwpRequest>
    {
        public CountryGwpRequestValidator()
        {
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country should be not empty");
            RuleFor(x => x.Country).Length(2).WithMessage("Country should be two-letter code");
            RuleFor(x => x.Lob).NotEmpty().WithMessage("Lob should be not empty");
        }
    }
}
