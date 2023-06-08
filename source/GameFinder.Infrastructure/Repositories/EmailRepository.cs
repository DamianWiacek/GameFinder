using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using GameFinder.Domain.Entities;
using GameFinder.Domain.Repositories;
using GameFinder.Infrastructure.Persistance;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GameFinder.Infrastructure.Repositories
{
    internal class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmailRepository(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<List<EMail>> GetAllUnsentEmails() 
        {  
            await _dbContext.SaveChangesAsync();
            return await _dbContext.EMail.Where(x=>x.IsSent).ToListAsync(); 
            
        }

        public async Task RemoveSentEmails()
        {
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC p_RemoveSentEmails");
         
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
