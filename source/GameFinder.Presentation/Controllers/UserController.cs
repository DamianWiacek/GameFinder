using GameFinder.Application.Features.Courts.Commands;
using GameFinder.Application.Features.Games.Commands;
using GameFinder.Application.Features.GameService.Commands;
using GameFinder.Application.Features.Users.Commands;
using GameFinder.Application.Models;
using GameFinder.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GameFinder.Presentation.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
            {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            Response.Cookies.Append("jwt", result.Token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true  // Make sure to set Secure to true when using HTTPS
            });
            return Ok(result);
        }
        [HttpGet("/GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var jwtToken = Request.Cookies["jwt"];
            var commandWithToken = new GetUserCommand()
            {
                JwtToken = jwtToken
            };
            var result = await _mediator.Send(commandWithToken);
            return Ok(result);
        }

        [HttpPost("/Logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("/GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
