using GameFinder.Application.Data;
using GameFinder.Application.Models;
using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Users.Commands
{
    public record RegisterCommand(NewUserDto newUser) : IRequest<int>;
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.FindUserByEmail(request.newUser.Email) == true) 
            {
                throw new Exception("User already exists");
            }
            CreatePasswordHash(request.newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User()
            {
                Name = request.newUser.Name,
                Surname = request.newUser.Surname,
                Birthday = request.newUser.Birthday,
                Email = request.newUser.Email,
                PasswordHash = passwordHash,
                Phone = request.newUser.Phone,
                RoleId = request.newUser.RoleId,
                PasswordSalt = passwordSalt,
                Pesel  = request.newUser.Pesel
            };
            await _userRepository.Register(user);
            await _userRepository.SaveChangesAsync();
            return user.UserId;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
