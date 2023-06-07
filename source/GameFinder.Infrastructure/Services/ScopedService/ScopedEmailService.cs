using GameFinder.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Infrastructure.Services.ScopedService
{
    public sealed class ScopedEmailService : IScopedProcessingService
    {
        private readonly IEmailRepository _emailRepository;
        public ScopedEmailService(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }
        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var emails = await _emailRepository.GetAllUnsentEmails();
                foreach (var email in emails)
                {
                     email.Send();
                }

                await _emailRepository.RemoveSentEmails();
                await _emailRepository.SaveChangesAsync();
                await Task.Delay(25000).WaitAsync(stoppingToken);
            }
        }
    }
}
