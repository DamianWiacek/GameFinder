using GameFinder.Application.Models;
using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Users.Commands
{
    
    public record LoginCommand(LoginUserDto user) : IRequest<LoggedUserDto>;
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedUserDto>
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public LoginCommandHandler(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<LoggedUserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User loggedUser = await _userRepository.GetUserByEmail(request.user.Email)
            ?? throw new Exception("No user with this email");
            if (!VerifyPasswordHash(request.user.Password, loggedUser.PasswordHash, loggedUser.PasswordSalt))
            {
                throw new Exception("Wrong Password");
            }      
            LoggedUserDto response = new LoggedUserDto
            {
                Token = Generate(loggedUser),
                UserId = loggedUser.UserId
            };
            return response;
        }

        private string Generate(User loggedUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedUser.Name),
                new Claim(ClaimTypes.Email, loggedUser.Email),
                new Claim("UserId", loggedUser.UserId.ToString()),
                new Claim(ClaimTypes.Surname, loggedUser.Surname),
                new Claim(ClaimTypes.Role, loggedUser.RoleRole.Name)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}