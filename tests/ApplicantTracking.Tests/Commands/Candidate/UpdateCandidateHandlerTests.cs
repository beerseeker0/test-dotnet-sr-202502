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
    public class UpdateCandidateHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitMock;
        private readonly Mock<ICandidateRepository> _candidateRepoMock;
        private readonly Mock<ITimelineRepository> _timelineRepoMock;

        private readonly UpdateCandidateHandler _handler;

        public UpdateCandidateHandlerTests()
        {
            _candidateRepoMock = new Mock<ICandidateRepository>();
            _timelineRepoMock = new Mock<ITimelineRepository>();

            _unitMock = new Mock<IUnitOfWork>();
            _unitMock.SetupGet(x => x.Candidates).Returns(_candidateRepoMock.Object);
            _unitMock.SetupGet(x => x.Timelines).Returns(_timelineRepoMock.Object);

            _handler = new UpdateCandidateHandler(_unitMock.Object);
        }


        [Fact]
        public async Task Update_Return_Error()
        {
            _candidateRepoMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Domain.DTO.Candidate?)null);

            var command = new UpdateCandidateCommand(
                1, "Update", "Teste", DateTime.Today.AddYears(-20), "updateteste@gmail.com"
            );

            Func<Task> act = async () =>
                await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
