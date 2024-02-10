using GameFinder.Domain.Exceptions;
using GameFinder.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.Games.Commands
{
    public record DeleteGameCommand(int gameId) : IRequest<bool>;

    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, bool>
    {
        private readonly IGameRepository _gameRepository;

        public DeleteGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var gameToDelete = await _gameRepository.GetGameById(request.gameId);
            if (gameToDelete == null)
                throw new ArgumentNullException("There is no game with given id!");

            var result = await _gameRepository.DeleteSingleAsync(gameToDelete);
            await _gameRepository.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
