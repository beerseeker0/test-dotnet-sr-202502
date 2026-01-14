using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.CreateCandidate
{
    public record CreateCandidateCommand(
    string Name,
    string Surname,
    DateTime Birthdate,
    string Email
    ) : IRequest<int>;
}
