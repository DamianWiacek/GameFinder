using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.GameDetails.Commands
{
    public record GetAllUserGamesCommand(int userId) : IRequest<List<Domain.Entities.GameDetails>>;
    public class GetAllUserGamesCommandHandler : IRequestHandler<GetAllUserGamesCommand, List<Domain.Entities.GameDetails>>
    {
        private readonly IGameDetailsRepository _gameDetailsRepository;

        public GetAllUserGamesCommandHandler(IGameDetailsRepository gameDetailsRepository)
        {
            _gameDetailsRepository = gameDetailsRepository;
        }

        public async Task<List<Domain.Entities.GameDetails>> Handle(GetAllUserGamesCommand request, CancellationToken cancellationToken)
        {
            var games = await _gameDetailsRepository.GetAllUserGames(request.userId);
            if (games.IsNullOrEmpty()) 
                throw new ArgumentNullException("There is no games on this court!");
            return games.ToList();
        }
    }
}
