using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using GameFinder.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GameRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<List<Game>> GetAllGames(Expression<Func<Game,Boolean>> predicate = null)
        {
            if(predicate == null)
                return await _dbContext.Game.ToListAsync();
   
            return await _dbContext.Game.Where(predicate).ToListAsync();            
        }
        public async Task<List<Game>> GetAllPublicGames()
            => await GetAllGames();

        public async Task<List<Game>> GetAllPublicGamesFromCourt(int courtId)
            => await GetAllGames(game => !game.IsPrivateMatch
                                       && game.CourtId == courtId);

        public async Task<List<Game>> GetAllGamesQuery(string query)
            => await GetAllGames(game =>
            game.Court.Address.City.Contains(query) ||
            game.Court.Address.PostalCode.Contains(query) ||
            game.Court.Address.Street.Contains(query));

        public async Task<int> AddGame(Game newGame)
            => (await _dbContext.Game.AddAsync(newGame)).Entity.GameId;

        public async Task<bool> DeleteGame(Game gameToDelete)
        {
            _dbContext.Game.Remove(gameToDelete);
            return await Task.FromResult(true);
        }

        public async Task<Game> GetGameById(int id)       
           => await _dbContext.Game.FirstOrDefaultAsync(game => game.GameId == id);
        
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
           => await _dbContext.SaveChangesAsync(cancellationToken);
        
    }
}
