using GameFinder.Application.Data;
using GameFinder.Domain.Repositories;
using GameFinder.Infrastructure.Persistance;
using GameFinder.Infrastructure.Repositories;
using GameFinder.Infrastructure.Services.ScopedService;
using GameFinder.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.Infrastructure 
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddScoped<IDbContext,ApplicationDbContext>();
            services.AddScoped<ICourtRepository, CourtRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameDetailsRepository, GameDetailsRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddHostedService<MainBackgroundService>();
            services.AddScoped<IScopedProcessingService,ScopedNotificationService>();
            return services;
        }
    }
}
