using FluentValidation;
using Microsoft.Extensions.Localization;
using PersonAPI.Core.Models;
using PersonAPI.Core.Resources.PhoneNumber;
using PersonAPI.Core.Resources.RelatedPerson;

namespace PersonAPI.Core.Validators
{
    public class PhoneNumberValidator : AbstractValidator<PhoneNumberModel>
    {
        public PhoneNumberValidator(IStringLocalizer<PhoneNumberMessage> localizer)
        {
            RuleFor(x => x.NumberType)
                .IsInEnum().WithMessage(PhoneNumberMessage.NumberTypeInvalid);

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage(PhoneNumberMessage.NumberRequired)
                .Length(4, 50).WithMessage(PhoneNumberMessage.NumberLength);
        }
    }
}
