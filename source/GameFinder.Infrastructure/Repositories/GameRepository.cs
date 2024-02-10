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
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationDbContext dbContext) : base(dbContext)
        {  
        }

        public async Task<IEnumerable<Game>> GetAllPublicGames()
            => await GetAllAsync(g =>!g.IsPrivateMatch);

        public async Task<IEnumerable<Game>> GetAllPublicGamesFromCourt(int courtId)
            => await GetAllAsync(game => !game.IsPrivateMatch
                                       && game.CourtId == courtId);

        public async Task<IEnumerable<Game>> GetAllGamesQuery(string query)
            => await GetAllAsync(game =>
            game.Court.Address.City.Contains(query) ||
            game.Court.Address.PostalCode.Contains(query) ||
            game.Court.Address.Street.Contains(query));

        public async Task<Game> GetGameById(int id)       
           => await GetFirstOrDefaultAsync(game => game.GameId == id);
        
        
    }
}
