using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Domain.DTO;
using ApplicantTracking.Domain.Enumerators;
using ApplicantTracking.Infrastructure.IRepository;
using MediatR;

namespace ApplicantTracking.Application.Commands.Candidate.CreateCandidate
{
    public class CreateCandidateHandler
        : IRequestHandler<CreateCandidateCommand, int>
    {
        private readonly IUnitOfWork _unit;

        public CreateCandidateHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<int> Handle(
            CreateCandidateCommand request,
            CancellationToken cancellationToken)
        {
            var candidate = new ApplicantTracking.Domain.DTO.Candidate
            {
                Name = request.Name,
                Surname = request.Surname,
                Birthdate = request.Birthdate,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            };

            await _unit.Candidates.AddAsync(candidate);

            await _unit.Timelines.AddAsync(new Timeline(
                candidate.IdCandidate,
                TimelineTypes.Create,
                null,
                JsonSerializer.Serialize(candidate)
            ));

            await _unit.CommitAsync();

            return candidate.IdCandidate;
        }
    }
}
