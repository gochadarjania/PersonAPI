using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using PersonAPI.Core.Models;
using PersonAPI.Core.Resources;
using PersonAPI.Core.Resources.RelatedPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.Validators
{
    public class RelatedPersonValidator : AbstractValidator<RelatedPersonModel>
    {
        public RelatedPersonValidator()
        {
            RuleFor(x => x.RelationshipType)
                .IsInEnum().WithMessage(RelatedPersonMessage.RelationshipTypeInvalid);

            RuleFor(x => x.RelatedPersonId)
                .GreaterThan(0).WithMessage(RelatedPersonMessage.RelatedPersonIdInvalid);
        }
    }
}
