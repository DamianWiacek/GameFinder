using GameFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Domain.Repositories
{
    public interface INotificationRepository
    {
        public Task<List<Notification>> GetAllUnsentNotifications();

        public Task RemoveSentNotifications(IEnumerable<Notification> notifications = null);

        public Task SaveChangesAsync();
    }
}
