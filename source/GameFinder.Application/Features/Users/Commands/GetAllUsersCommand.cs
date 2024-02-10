using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Users.Commands
{
    public record GetAllUsersCommand : IRequest<List<User>>;
    public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
        => (await _userRepository.GetAllAsync()).ToList();
        
    }
}
