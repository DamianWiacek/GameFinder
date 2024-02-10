using GameFinder.Domain.Repositories;
using MediatR;

namespace GameFinder.Application.Features.GameDetails.Commands
{
    public record DeleteUserFromGameCommand(int gameId, int userId) : IRequest<bool>;

    public class DeleteUserFromGameCommandHandler : IRequestHandler<DeleteUserFromGameCommand, bool>
    {
        private readonly IGameDetailsRepository _gameDetailsRepository;

        public DeleteUserFromGameCommandHandler(IGameDetailsRepository gameDetailsRepositoryy)
        {
            _gameDetailsRepository = gameDetailsRepositoryy;
        }

        public async Task<bool> Handle(DeleteUserFromGameCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _gameDetailsRepository.GetGameDetails(request.gameId, request.userId);
            if (userToDelete == null)
                throw new ArgumentNullException("User is not in this game or game doesn't exists");
            var result = await _gameDetailsRepository.DeleteUserFromGame(userToDelete);
            await _gameDetailsRepository.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}