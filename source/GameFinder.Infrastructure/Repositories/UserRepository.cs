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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> UserWithEmailExist(string email)        
             => (await GetFirstOrDefaultAsync(u => u.EmailAddress == email)) == null 
                                                                              ? false 
                                                                              : true;
            
        
        public async Task<User> GetUserWithRoleByEmail(string email)        
             => await _dbContext.User.Include(user => user.RoleRole)
                                      .FirstOrDefaultAsync(x => x.EmailAddress == email);          
        
        public async Task<User> GetUserWithRoleById(int id)        
            => await _dbContext.User.Include(user => user.RoleRole)
                                        .FirstOrDefaultAsync(x => x.UserId == id);
        

        public async Task<User> Login(User user)        
            => await GetFirstOrDefaultAsync(u => u.EmailAddress == user.EmailAddress);
            
        

    }
}
