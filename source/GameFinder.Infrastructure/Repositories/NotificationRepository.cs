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
    internal class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NotificationRepository(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<List<Notification>> GetAllUnsentNotifications()
        {
            return await _dbContext.Notification.Where(n=>!n.IsSent).ToListAsync();
        }

        public async Task RemoveSentNotifications(IEnumerable<Notification> notifications = null)
        {
            if (notifications == null) 
            { 
                _dbContext.Notification.RemoveRange(notifications);
                return;
            }
                
            notifications = await _dbContext.Notification.Where(n => n.IsSent).ToListAsync();
            _dbContext.Notification.RemoveRange(notifications);
         
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
