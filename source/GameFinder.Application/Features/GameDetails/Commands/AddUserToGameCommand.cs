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

namespace GameFinder.Application.Features.GameDetails.Commands
{
    public record AddUserToGameCommand(NewGameDetailsDto newGameDetailsDto) : IRequest<int>;
    public class AddUserToGameCommandHandler : IRequestHandler<AddUserToGameCommand, int>
    {
        private readonly IGameDetailsRepository _gameDetailsRepository;

        public AddUserToGameCommandHandler(IGameDetailsRepository gameDetailsRepository)
        {
            _gameDetailsRepository = gameDetailsRepository;
        }

        public async Task<int> Handle(AddUserToGameCommand request, CancellationToken cancellationToken)
        {
            var newGameDetails = new Domain.Entities.GameDetails(
                request.newGameDetailsDto.GameId,
                request.newGameDetailsDto.UserId);
            if(await _gameDetailsRepository.GetGameDetails(newGameDetails.GameId,newGameDetails.UserId) is not null)           
                throw new Exception("User already in this game");
                       
            await _gameDetailsRepository.AddSingleAsync(newGameDetails);
            await _gameDetailsRepository.SaveChangesAsync();
            return newGameDetails.GameDetailsId;
        }
    }
}
