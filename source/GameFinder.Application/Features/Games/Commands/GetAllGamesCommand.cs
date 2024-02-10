using GameFinder.Application.Data;
using GameFinder.Application.Models;
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
    public record GetAllGamesCommand : IRequest<List<Game>>;
    public class GetAllGamesCommandHandler : IRequestHandler<GetAllGamesCommand, List<Game>>
    {
        private readonly IGameRepository _gameRepository;

        public GetAllGamesCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<Game>> Handle(GetAllGamesCommand request, CancellationToken cancellationToken)        
            => (await _gameRepository.GetAllAsync()).ToList();
        
    }
}
