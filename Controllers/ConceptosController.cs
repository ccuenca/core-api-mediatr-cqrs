using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestMediaTR.Domain.Commands;

namespace TestMediaTR.Controllers
{
    [Route("api/conceptos")]
    public class ConceptosController : Controller
    {
        private readonly IMediator _mediator;

        public ConceptosController(IMediator mediator)
        {
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([
            FromBody] CreateUpdateConceptosCommand command, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commandResult = await _mediator.Send(command, cancellationToken);

            if(commandResult.IsSuccess) {

                return CreatedAtRoute(
                    routeName: nameof(GetById),
                    routeValues: new { commandResult.Data.Id },
                    value: commandResult.Data
                );
            }
                
            return StatusCode(500, new {
                error = commandResult.FailureReason
            });
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery] QueryConceptosCommand command, CancellationToken cancellationToken)
        {
            var commandResult = await _mediator.Send(command, cancellationToken);

            if(commandResult.IsSuccess) {

                return Ok(
                    value: commandResult.Data
                );
            }
                
            return StatusCode(500, new {
                error = commandResult.FailureReason
            });
        }

        [HttpGet("{id:int}", Name="GetById")]
        public async Task<IActionResult> GetById(int id){

            var commandResult = await _mediator.Send(new QueryConceptosCommand { Id = id });

            if(commandResult.IsSuccess) {

                return Ok(
                    value: commandResult.Data
                );
            }
                
            return StatusCode(500, new {
                error = commandResult.FailureReason
            });
        }

    }
}