using PersonAPI.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using System;
using PersonAPI.Core.Resources;
using PersonAPI.Core.Resources.Person;


namespace PersonAPI.Core.Validators
{
    public class PersonValidator : AbstractValidator<PersonModel>
    {        public PersonValidator(
        PhoneNumberValidator phoneNumberValidator,
        RelatedPersonValidator relatedPersonValidator)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(PersonMessage.FirstNameRequired)
                .Length(2, 50).WithMessage(PersonMessage.FirstNameLength)
                .Matches("^[ა-ჰ]+$|^[a-zA-Z]+$").WithMessage(PersonMessage.FirstNameRegex);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(PersonMessage.LastNameRequired)
                .Length(2, 50).WithMessage(PersonMessage.LastNameLength)
                .Matches("^[ა-ჰ]+$|^[a-zA-Z]+$").WithMessage(PersonMessage.LastNameRegex);

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage(PersonMessage.GenderInvalid);

            RuleFor(x => x.PersonalNumber)
                .NotEmpty().WithMessage(PersonMessage.PersonalNumberRequired)
                .Length(11).WithMessage(PersonMessage.PersonalNumberLength)
                .Matches(@"^\d{11}$").WithMessage(PersonMessage.PersonalNumberRegex);

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage(PersonMessage.BirthDateRequired)
                .Must(BeAtLeast18).WithMessage(PersonMessage.AgeValidation);

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage(PersonMessage.CityIdRequired);

            RuleFor(x => x.PhoneNumbers)
                .NotEmpty().WithMessage(PersonMessage.PhoneNumbersMinLength)
                .ForEach(phoneNumber =>
                {
                    phoneNumber.SetValidator(phoneNumberValidator);
                });

            RuleForEach(x => x.RelatedPersons)
                .SetValidator(relatedPersonValidator);
        }

        private bool BeAtLeast18(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }

}
