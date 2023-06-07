using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using GameFinder.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<Game>> GetAllGames()
        {
            var result = await _dbContext.Game.ToListAsync();
            return result;
        }
        public async Task<List<Game>> GetAllGamesFromCourt(int courtId)
        {
            var result = await _dbContext.Game.Where(x => x.CourtId == courtId).ToListAsync();
            return result;
        }
        public async Task<List<Game>> GetAllGamesQuery(string query)
        {
            var result = await _dbContext.Game
            .Where(g =>
            g.Court.Address.City.Contains(query) ||
            g.Court.Address.PostalCode.Contains(query) ||
            g.Court.Address.Street.Contains(query))
            .ToListAsync();
            return result;
        }
        public async Task<int> AddGame(Game newGame)
        {
            var result = await _dbContext.Game.AddAsync(newGame);
            return result.Entity.GameId;
        }

        public async Task<bool> DeleteGame(Game gameToDelete)
        {
            _dbContext.Game.Remove(gameToDelete);
            return await Task.FromResult(true);
        }

        public async Task<Game> GetGameById(int id)
        {
            return await _dbContext.Game.FirstOrDefaultAsync(game => game.GameId == id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
