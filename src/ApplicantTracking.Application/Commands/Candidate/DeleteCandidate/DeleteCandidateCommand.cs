using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.DeleteCandidate
{
    public record DeleteCandidateCommand(int IdCandidate)
    : IRequest;
}
