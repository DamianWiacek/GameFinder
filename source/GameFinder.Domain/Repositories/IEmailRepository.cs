using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Repositories
{
    public interface IEmailRepository
    {
        public Task<List<EMail>> GetAllUnsentEmails();

        public Task RemoveSentEmails();

        public Task SaveChangesAsync();
    }
}
