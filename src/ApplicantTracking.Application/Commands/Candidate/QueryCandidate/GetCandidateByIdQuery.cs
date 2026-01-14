using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.QueryCandidate
{
    public record GetCandidateByIdQuery(int IdCandidate)
        : IRequest<ApplicantTracking.Domain.DTO.Candidate>;
}
