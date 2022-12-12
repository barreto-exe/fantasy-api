using FantasyApi.Interfaces;
using FantasyApi.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(FantasyApi.Startup))]

namespace FantasyApi
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.Add(new ServiceDescriptor(typeof(IDatabaseService), new DatabaseService(connectionString)));

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IEventService, EventService>();
            builder.Services.AddTransient<IAzureStorageService, AzureStorage>();
            builder.Services.AddTransient<ITeamsService, TeamsService>();
            builder.Services.AddTransient<IAdService, AdService>();
            builder.Services.AddTransient<ISoccerPlayerService, SoccerPlayerService>();
            builder.Services.AddTransient<IStickerService, StickerService>();
            builder.Services.AddTransient<IMatchService, MatchService>();
        }
    }
}
