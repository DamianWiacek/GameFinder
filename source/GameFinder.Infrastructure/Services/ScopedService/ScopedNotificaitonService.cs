using GameFinder.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Infrastructure.Services.ScopedService
{
    public sealed class ScopedNotificationService : IScopedProcessingService
    {
        private readonly INotificationRepository _notificationRepository;
        public ScopedNotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {

                var notifications = await _notificationRepository.GetAllUnsentNotifications();
                foreach (var notification in notifications)
                {
                     notification.SendThroughEmail();
                }
                

                await _notificationRepository.RemoveSentNotifications(notifications);
                await _notificationRepository.SaveChangesAsync();
                await Task.Delay(3000).WaitAsync(stoppingToken);
            }
        }
    }
}
