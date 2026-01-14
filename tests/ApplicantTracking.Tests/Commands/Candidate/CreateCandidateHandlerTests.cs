using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Application.Commands.Candidate.CreateCandidate;
using ApplicantTracking.Application.Commands.Candidate.UpdateCandidate;
using ApplicantTracking.Domain.DTO;
using ApplicantTracking.Infrastructure.IRepository;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApplicantTracking.Tests.Commands.Candidate
{
    public class CreateCandidateHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitMock;
        private readonly Mock<ICandidateRepository> _candidateRepoMock;
        private readonly Mock<ITimelineRepository> _timelineRepoMock;

        private readonly CreateCandidateHandler _handler;

        public CreateCandidateHandlerTests()
        {
            _candidateRepoMock = new Mock<ICandidateRepository>();
            _timelineRepoMock = new Mock<ITimelineRepository>();

            _unitMock = new Mock<IUnitOfWork>();
            _unitMock.SetupGet(x => x.Candidates).Returns(_candidateRepoMock.Object);
            _unitMock.SetupGet(x => x.Timelines).Returns(_timelineRepoMock.Object);

            _handler = new CreateCandidateHandler(_unitMock.Object);
        }

        [Fact]
        public async Task Create_Candidate_Timeline()
        {
            var command = new CreateCandidateCommand(
                "Felipe",
                "Rossi",
                new DateTime(1993, 9, 10),
                "phelipe.caffeu@gmail.com"
            );

            var id = await _handler.Handle(command, CancellationToken.None);

            _candidateRepoMock.Verify(x => x.AddAsync(It.IsAny<Domain.DTO.Candidate>()), Times.Once);
            _timelineRepoMock.Verify(x => x.AddAsync(It.IsAny<Timeline>()), Times.Once);
            _unitMock.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}
