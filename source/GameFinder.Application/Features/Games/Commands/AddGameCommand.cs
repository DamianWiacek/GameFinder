using GameFinder.Application.Data;
using GameFinder.Application.Models;
using GameFinder.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Application.Features.GameService.Commands
{
    public record AddGameCommand(NewGameDto newGameDto) : IRequest<int>;
    public class AddGameCommandHandler : IRequestHandler<AddGameCommand,int>
    {
        private readonly IDbContext _dbContext;

        public AddGameCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddGameCommand request, CancellationToken cancellationToken)
        {
            var newGame = new Game(
                request.newGameDto.SportId,
                request.newGameDto.Start,
                request.newGameDto.PredictedEnd,
                request.newGameDto.CourtId);
            if(!(DateTime.Compare(newGame.PredictedEnd, newGame.Start) > 0)) throw new Exception("Wrong date input");
            await _dbContext.Game.AddAsync(newGame);
            await _dbContext.SaveChangesAsync();
            return newGame.GameId;           
        }      
    }
}
