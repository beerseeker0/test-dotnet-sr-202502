using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.QueryCandidate
{
    public class ListCandidateHandler
    : IRequestHandler<ListCandidateQuery, List<Domain.DTO.Candidate>>
    {
        private readonly IApplicationReadDbContext _context;

        public ListCandidateHandler(IApplicationReadDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.DTO.Candidate>> Handle(
            ListCandidateQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.Set<Domain.DTO.Candidate>()
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .Select(c => new Domain.DTO.Candidate
                {
                    IdCandidate = c.IdCandidate,
                    Name = c.Name,
                    Surname = c.Surname,
                    Email = c.Email,
                    Birthdate = c.Birthdate
                })
                .ToListAsync(cancellationToken);
        }
    }
}
