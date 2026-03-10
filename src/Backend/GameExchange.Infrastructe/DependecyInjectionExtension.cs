using FluentMigrator.Runner;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.User;
using GameExchange.Domain.Security.Cryptogaphy;
using GameExchange.Infrastructe.DataAccess;
using GameExchange.Infrastructe.DataAccess.Repositories;
using GameExchange.Infrastructe.Extensions;
using GameExchange.Infrastructe.Security.Cryptogaphy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameExchange.Infrastructe
{
    public static class DependecyInjectionExtension
    {
        public static void AddInfrastructe(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddFluentMigrator(services, configuration);
            AddRepositories(services);
            AddPasswordEncripter(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameExchangeDbContext>(options =>
            {
                options.UseSqlServer(configuration.ConnectionString());
            });
        }
        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(DependecyInjectionExtension).Assembly).For.All())
                .AddLogging(lb => lb.AddFluentMigratorConsole());


        }

        private static void AddPasswordEncripter(IServiceCollection services) {
            services.AddScoped<IPasswordEncripter, BCryptNet>();
        }

    }
}
