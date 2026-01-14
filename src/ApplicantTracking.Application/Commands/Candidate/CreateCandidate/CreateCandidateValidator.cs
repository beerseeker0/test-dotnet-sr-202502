using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ApplicantTracking.Application.Commands.Candidate.CreateCandidate
{
    public class CreateCandidateValidator
    : AbstractValidator<CreateCandidateCommand>
    {
        public CreateCandidateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(80);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(250);

            RuleFor(x => x.Birthdate)
                .LessThan(DateTime.Today)
                .WithMessage("Birthdate must be lower than today.");
        }
    }
}
