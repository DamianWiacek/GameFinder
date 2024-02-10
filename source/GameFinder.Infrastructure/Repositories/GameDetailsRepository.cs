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
    public class GameDetailsRepository : BaseRepository<GameDetails>,IGameDetailsRepository
    {
        public GameDetailsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<GameDetails>> GetAllUserGames(int userId)
            => await GetAllAsync(x => x.UserId == userId);


        public async Task<IEnumerable<GameDetails>> GetAllGameUsers(int gameId)
            => await GetAllAsync(g => g.GameId == gameId);

        public async Task<GameDetails> GetGameDetails(int gameId, int userId)
            => await GetFirstOrDefaultAsync(x => x.GameId == gameId && x.UserId == userId);


        public async Task<bool> DeleteUserFromGame(GameDetails gameDetails)
            => await DeleteSingleAsync(gameDetails);

        public async Task<int> AddUserToGame(GameDetails gameDetails)
            => (await AddSingleAsync(gameDetails)).GameDetailsId;

    }
}
