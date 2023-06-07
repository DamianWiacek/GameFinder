using GameFinder.Application.Features.Courts.Commands;
using GameFinder.Application.Features.Games.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Presentation.Controllers
{
    [Route("api/Court")]
    public class CourtController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourtController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("/GetAllCourts")]
        public async Task<IActionResult> GetAllCourts([FromQuery] GetAllCourtsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("/GetCourt")]
        public async Task<IActionResult> GetCourt([FromQuery] GetCourtCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("/GetAddress")]
        public async Task<IActionResult> GetAddress([FromQuery] GetAddressCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("/AddCourt")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddCourt([FromBody] CreateCourtCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
