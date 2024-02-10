using GameFinder.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.GameDetails.Commands
{
    public record GetAllGameUsersCommand(int gameId) : IRequest<List<Domain.Entities.GameDetails>>;
    public class GetAllGameUsersCommandHandler : IRequestHandler<GetAllGameUsersCommand, List<Domain.Entities.GameDetails>>
    {
        private readonly IGameDetailsRepository _gameDetailsRepository;

        public GetAllGameUsersCommandHandler(IGameDetailsRepository gameDetailsRepository)
        {
            _gameDetailsRepository = gameDetailsRepository;
        }

        public async Task<List<Domain.Entities.GameDetails>> Handle(GetAllGameUsersCommand request, CancellationToken cancellationToken)
           => (await _gameDetailsRepository.GetAllGameUsers(request.gameId)).ToList();
                 
               
    }
}
