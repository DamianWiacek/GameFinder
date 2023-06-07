using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<int> Register(User newUser);
        Task<User> Login(User user);
        Task<List<User>> GetAllUsers();
        Task<bool> FindUserByEmail(string email);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
