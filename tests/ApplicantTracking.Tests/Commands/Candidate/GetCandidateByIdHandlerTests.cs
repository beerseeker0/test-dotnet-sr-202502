using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Application.Commands.Candidate.CreateCandidate;
using ApplicantTracking.Application.Commands.Candidate.QueryCandidate;
using ApplicantTracking.Application.Commands.Candidate.UpdateCandidate;
using ApplicantTracking.Domain.DTO;
using ApplicantTracking.Infrastructure.IRepository;
using FluentAssertions;
using Moq;
using Xunit;

namespace ApplicantTracking.Tests.Commands.Candidate
{
    public class GetCandidateByIdHandlerTests
    {
        [Fact]
        public async Task Should_Return_Candidate_When_Exists()
        {
            var candidates = new List<Domain.DTO.Candidate>
            {
                new()
                {
                    IdCandidate = 1,
                    Name = "Felipe",
                    Surname = "Rossi",
                    Email = "feliperossi@gmail.com",
                    Birthdate = new DateTime(1993, 9, 10)
                }
            };

            var context = new FakeReadDbContext();
            context.AddSet(candidates);

            var handler = new GetCandidateByIdHandler(context);

            var result = await handler.Handle(
                new GetCandidateByIdQuery(1),
                CancellationToken.None
            );

            result.Should().NotBeNull();
            result!.IdCandidate.Should().Be(1);
            result.Name.Should().Be("Felipe");
            result.Surname.Should().Be("Rossi");
            result.Email.Should().Be("feliperossi@gmail.com");
        }

        [Fact]
        public async Task Should_Return_Null_When_Candidate_Does_Not_Exist()
        {
            var candidates = new List<Domain.DTO.Candidate>();

            var context = new FakeReadDbContext();
            context.AddSet(candidates);

            var handler = new GetCandidateByIdHandler(context);

            var result = await handler.Handle(
                new GetCandidateByIdQuery(999),
                CancellationToken.None
            );

            result.Should().BeNull();
        }
    }
}
