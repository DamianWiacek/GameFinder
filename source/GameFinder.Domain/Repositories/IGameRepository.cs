using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Repositories
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Task<Game> GetGameById(int id);
        Task<IEnumerable<Game>> GetAllPublicGamesFromCourt(int courtId);
        Task<IEnumerable<Game>> GetAllGamesQuery(string query);
    }
}
