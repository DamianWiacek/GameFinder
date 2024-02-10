using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Repositories
{
    public interface ICourtRepository : IBaseRepository<Court>
    {
        Task<Court> GetCourtById(int id);
    }
}
