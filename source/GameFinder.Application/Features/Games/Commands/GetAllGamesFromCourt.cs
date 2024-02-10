using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Games.Commands
{
    public record GetAllGamesFromCourtCommand(int courtId) : IRequest<List<Game>>;
    public class GetAllGamesFromCourtCommandHandler : IRequestHandler<GetAllGamesFromCourtCommand, List<Game>>
    {
        private readonly IGameRepository _gameRepository;

        public GetAllGamesFromCourtCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<Game>> Handle(GetAllGamesFromCourtCommand request, CancellationToken cancellationToken)
        {
            var games = await _gameRepository.GetAllPublicGamesFromCourt(request.courtId);
            if (games.IsNullOrEmpty()) 
                throw new ArgumentNullException("There is no games on this court!");
            return games.ToList();
        }
    }
}
