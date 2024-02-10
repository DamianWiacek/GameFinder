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
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace GameFinder.Infrastructure.Repositories
{
    internal class NotificationRepository : BaseRepository<Notification> ,INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Notification>> GetAllUnsentNotifications()
            => await GetAllAsync(n => !n.IsSent);

        public async Task RemoveSentNotifications()
        {
            var notifications = await GetAllAsync(n => n.IsSent);
            await DeleteRangeAsync(notifications);         
        }

    }
}
