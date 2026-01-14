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

namespace ApplicantTracking.Application.Commands.Candidate.DeleteCandidate
{
    public class DeleteCandidateHandler
    : IRequestHandler<DeleteCandidateCommand>
    {
        private readonly IUnitOfWork _unit;

        public DeleteCandidateHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task Handle(
            DeleteCandidateCommand request,
            CancellationToken cancellationToken)
        {
            var candidate = await _unit.Candidates.GetByIdAsync(request.IdCandidate);
            if (candidate is null)
                throw new KeyNotFoundException("Candidate not found");

            var oldData = JsonSerializer.Serialize(candidate);

            _unit.Candidates.Delete(candidate);

            await _unit.Timelines.AddAsync(new Timeline(
                candidate.IdCandidate,
                TimelineTypes.Delete,
                oldData,
                JsonSerializer.Serialize(candidate)
            ));

            await _unit.CommitAsync();
        }
    }
}
