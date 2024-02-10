using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using GameFinder.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameFinder.Infrastructure.Repositories
{
    public class CourtRepository : BaseRepository<Court>, ICourtRepository
    {
        public CourtRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Court> GetCourtById(int id)        
            =>   await _dbContext.Court
                .Include(c => c.Address)
                .Include(c => c.Games)
                .FirstOrDefaultAsync(c => c.CourtId == id);
        

    }
}
