using FluentMigrator.Runner;
using GameExchange.Infrastructe.DataAccess;
using GameExchange.Infrastructe.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Infrastructe
{
    public static class DependecyInjectionExtension
    {
        public static void AddInfrastructe(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddFluentMigrator(services, configuration);
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
    }
}
