using GameFinder.Application.Features.Courts.Commands;
using GameFinder.Application.Features.Games.Commands;
using GameFinder.Application.Features.GameService;
using GameFinder.Application.Features.GameService.Commands;
using GameFinder.Application.Models;
using GameFinder.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameFinder.Presentation.Controllers
{
    [Route("api/Game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("/GetAllGames")]
        public async Task<IActionResult> GetAllGames([FromQuery] GetAllGamesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("/GetAllGamesFromCourt")]
        public async Task<IActionResult> GetAllGamesFromCourt([FromQuery] GetAllGamesFromCourtCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("/GetAllGamesFromQuery")]
        public async Task<IActionResult> GetAllGamesFromQuery([FromQuery] GetAllGamesQueryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("/AddGame")]
        [Authorize]
        public async Task<IActionResult> AddGame([FromBody] AddGameCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("/DeleteGame")]
        public async Task<IActionResult> DeleteGame([FromBody] DeleteGameCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
