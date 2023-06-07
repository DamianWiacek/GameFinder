using GameFinder.Application.Models;
using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Users.Commands
{
    public record GetUserCommand() : IRequest<User>
    {
        public string JwtToken { get; init; }
    }
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand, User>
    {
        private IConfiguration _config;
        private readonly IUserRepository _userRepository;
        public GetUserCommandHandler(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<User> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var jwt = request.JwtToken;

            var token = Verify(jwt);

            var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                // Handle the case when the UserId claim is missing or invalid
                throw new InvalidOperationException("Invalid token. UserId claim is missing or invalid.");
            }

            return await _userRepository.GetUserById(userId);


        }
        private JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
