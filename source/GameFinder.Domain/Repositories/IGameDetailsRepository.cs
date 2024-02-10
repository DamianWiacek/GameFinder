using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Repositories
{
    public interface IGameDetailsRepository : IBaseRepository<GameDetails>
    {
        Task<bool> DeleteUserFromGame(GameDetails gameDetails);
        Task<GameDetails> GetGameDetails(int gameId, int userId);
        Task<IEnumerable<GameDetails>> GetAllUserGames(int userId);
        Task<IEnumerable<GameDetails>> GetAllGameUsers(int gameId);
    }
}
