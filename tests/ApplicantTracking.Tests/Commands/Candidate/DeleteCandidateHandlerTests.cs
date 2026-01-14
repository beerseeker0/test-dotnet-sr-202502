using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Application.Commands.Candidate.DeleteCandidate;
using ApplicantTracking.Domain.DTO;
using ApplicantTracking.Infrastructure.IRepository;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApplicantTracking.Tests.Commands.Candidate
{
    public class DeleteCandidateHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitMock;
        private readonly Mock<ICandidateRepository> _candidateRepoMock;
        private readonly Mock<ITimelineRepository> _timelineRepoMock;

        private readonly DeleteCandidateHandler _handler;

        public DeleteCandidateHandlerTests()
        {
            _candidateRepoMock = new Mock<ICandidateRepository>();
            _timelineRepoMock = new Mock<ITimelineRepository>();

            _unitMock = new Mock<IUnitOfWork>();
            _unitMock.SetupGet(x => x.Candidates).Returns(_candidateRepoMock.Object);
            _unitMock.SetupGet(x => x.Timelines).Returns(_timelineRepoMock.Object);

            _handler = new DeleteCandidateHandler(_unitMock.Object);
        }

        [Fact]
        public async Task Should_Delete_Candidate_And_Create_Timeline()
        {
            // Arrange
            var candidate = new Domain.DTO.Candidate
            {
                IdCandidate = 1,
                Name = "Felipe",
                Surname = "Rossi",
                Email = "feliperossi@gmail.com"
            };

            _candidateRepoMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(candidate);

            // Act
            await _handler.Handle(
                new DeleteCandidateCommand(1),
                CancellationToken.None
            );

            // Assert
            _candidateRepoMock.Verify(x => x.Delete(candidate), Times.Once);
            _timelineRepoMock.Verify(x => x.AddAsync(It.IsAny<Timeline>()), Times.Once);
            _unitMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_When_Candidate_Not_Found()
        {

            _candidateRepoMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Domain.DTO.Candidate?)null);

            Func<Task> act = async () =>
                await _handler.Handle(
                    new DeleteCandidateCommand(99),
                    CancellationToken.None
                );

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
