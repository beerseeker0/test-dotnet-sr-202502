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

namespace ApplicantTracking.Application.Commands.Candidate.UpdateCandidate
{
    public class UpdateCandidateHandler: IRequestHandler<UpdateCandidateCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateCandidateHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task Handle(
            UpdateCandidateCommand request,
            CancellationToken cancellationToken)
        {
            var candidate = await _unit.Candidates.GetByIdAsync(request.IdCandidate);
            if (candidate is null)
                throw new KeyNotFoundException("Candidate not found.");

            var oldData = JsonSerializer.Serialize(candidate);

            candidate.Name = request.Name;
            candidate.Surname = request.Surname;
            candidate.Email = request.Email;
            candidate.Birthdate = request.Birthdate;
            candidate.LastUpdatedAt = DateTime.UtcNow;

            _unit.Candidates.Update(candidate);

            await _unit.Timelines.AddAsync(new Timeline(
                candidate.IdCandidate,
                TimelineTypes.Update,
                oldData,
                JsonSerializer.Serialize(candidate)
            ));

            await _unit.CommitAsync();
        }
    }
}
