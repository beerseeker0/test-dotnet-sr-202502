using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTracking.Application.Commands.Candidate.UpdateCandidate
{
    

    public record UpdateCandidateCommand(
        int IdCandidate,
        string Name,
        string Surname,
        DateTime Birthdate,
        string Email
    ) : IRequest;
}
