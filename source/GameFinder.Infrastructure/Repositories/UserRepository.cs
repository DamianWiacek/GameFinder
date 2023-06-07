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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<int> Register(User newUser)
        {
            var result = await _dbContext.User.AddAsync(newUser);
            return result.Entity.UserId;
        }
        public async Task<bool> FindUserByEmail(string email)
        {
            var existingUser = await _dbContext.User.FirstOrDefaultAsync(x => x.Email == email);
            return existingUser == null ? false : true;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.User.Include(user => user.RoleRole).FirstOrDefaultAsync(x => x.Email == email);          
        }
        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.User.Include(user => user.RoleRole).FirstOrDefaultAsync(x => x.UserId == id);
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<User> Login(User user)
        {
            var result = await _dbContext.User.FirstOrDefaultAsync(x =>x.Email==user.Email);
            return result;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var result = await _dbContext.User.ToListAsync();
            return result;
        }
    }
}
