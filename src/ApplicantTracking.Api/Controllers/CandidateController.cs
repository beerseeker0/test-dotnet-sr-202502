using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicantTracking.Application.Commands.Candidate.CreateCandidate;
using ApplicantTracking.Application.Commands.Candidate.DeleteCandidate;
using ApplicantTracking.Application.Commands.Candidate.QueryCandidate;
using ApplicantTracking.Application.Commands.Candidate.UpdateCandidate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApplicantTracking.Api.Controllers
{
    [ApiController]
    [Route("candidates")]
    public sealed class CandidateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<Domain.DTO.Candidate>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new ListCandidateQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Domain.DTO.Candidate), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetCandidateByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCandidateCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateCandidateCommand command)
        {
            if (id != command.IdCandidate)
                return BadRequest();

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeleteCandidateCommand(id));
            return Ok();
        }
    }
}
