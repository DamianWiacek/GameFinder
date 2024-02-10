using GameFinder.Domain.Entities;
using GameFinder.Domain.Exceptions;
using GameFinder.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.GameDetails.Commands
{
    public record GetAllUsersInAllGamesCommand : IRequest<List<Domain.Entities.GameDetails>>;
    public class GetAllUsersInAllGamesCommandHandler : IRequestHandler<GetAllUsersInAllGamesCommand, List<Domain.Entities.GameDetails>>
    {
        private readonly IGameDetailsRepository _gameDetailsRepository;

        public GetAllUsersInAllGamesCommandHandler(IGameDetailsRepository gameDetailsRepository)
        {
            _gameDetailsRepository = gameDetailsRepository;
        }

        public async Task<List<Domain.Entities.GameDetails>> Handle(GetAllUsersInAllGamesCommand request, CancellationToken cancellationToken) 
            => (await _gameDetailsRepository.GetAllAsync()).ToList();
        
    }
}
