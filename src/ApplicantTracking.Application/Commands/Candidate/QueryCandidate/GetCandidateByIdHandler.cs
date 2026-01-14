using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Interfaces;
using ApplicantTracking.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using ApplicantTracking.Infrastructure;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.QueryCandidate
{
    public class GetCandidateByIdHandler
    : IRequestHandler<GetCandidateByIdQuery, Domain.DTO.Candidate>
    {
        private readonly IApplicationReadDbContext _context;

        public GetCandidateByIdHandler(IApplicationReadDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.DTO.Candidate> Handle(
            GetCandidateByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Set<Domain.DTO.Candidate>()
                .AsNoTracking()
                .Where(c => c.IdCandidate == request.IdCandidate)
                .Select(c => new Domain.DTO.Candidate
                {
                    IdCandidate = c.IdCandidate,
                    Name = c.Name,
                    Surname = c.Surname,
                    Email = c.Email,
                    Birthdate = c.Birthdate
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
