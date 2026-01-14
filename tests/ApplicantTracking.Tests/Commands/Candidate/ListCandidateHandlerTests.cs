using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApplicantTracking.Application.Commands.Candidate.QueryCandidate;
using FluentAssertions;
using Xunit;

namespace ApplicantTracking.Tests.Commands.Candidate
{
    public class ListCandidateHandlerTests
    {
        [Fact]
        public async Task Should_Return_All_Candidates()
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
                },
                new()
                {
                    IdCandidate = 2,
                    Name = "Rossi",
                    Surname = "Felipe",
                    Email = "rossifelipe@gmail.com",
                    Birthdate = new DateTime(1990, 3, 20)
                }
            };

            var context = new FakeReadDbContext();
            context.AddSet(candidates);

            var handler = new ListCandidateHandler(context);

            var result = await handler.Handle(
                new ListCandidateQuery(),
                CancellationToken.None
            );

            result.Should().HaveCount(2);
            result.Select(x => x.IdCandidate)
                  .Should().Contain(new[] { 1, 2 });
        }

        [Fact]
        public async Task Should_Return_Empty_List_When_No_Data()
        {
            var context = new FakeReadDbContext();
            context.AddSet(new List<Domain.DTO.Candidate>());

            var handler = new ListCandidateHandler(context);

            var result = await handler.Handle(
                new ListCandidateQuery(),
                CancellationToken.None
            );

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
