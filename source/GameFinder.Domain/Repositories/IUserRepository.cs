using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> Login(User user);
        Task<bool> UserWithEmailExist(string email);
        Task<User> GetUserWithRoleByEmail(string email);
        Task<User> GetUserWithRoleById(int id);
    }
}
