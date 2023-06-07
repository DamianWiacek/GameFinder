using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Games.Commands
{
    
    public record GetAllGamesQueryCommand(string search) : IRequest<List<Game>>;
    public class GetAllGamesQueryCommandHandler : IRequestHandler<GetAllGamesQueryCommand, List<Game>>
    {
        private readonly IGameRepository _gameRepository;

        public GetAllGamesQueryCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<Game>> Handle(GetAllGamesQueryCommand request, CancellationToken cancellationToken)
        {
            var games = await _gameRepository.GetAllGamesQuery(request.search);
            if (games == null) throw new ArgumentNullException("No records matching to address");
            return games;
        }
    }

}
