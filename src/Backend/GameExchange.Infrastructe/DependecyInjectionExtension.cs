using FluentMigrator.Runner;
using GameExchange.Domain.Repositories;
using GameExchange.Domain.Repositories.Category;
using GameExchange.Domain.Repositories.Game;
using GameExchange.Domain.Repositories.Listing;
using GameExchange.Domain.Repositories.Order;
using GameExchange.Domain.Repositories.Platform;
using GameExchange.Domain.Repositories.Token;
using GameExchange.Domain.Repositories.User;
using GameExchange.Domain.Security.Cryptogaphy;
using GameExchange.Domain.Security.Tokens;
using GameExchange.Domain.Services;
using GameExchange.Infrastructe.DataAccess;
using GameExchange.Infrastructe.DataAccess.Repositories;
using GameExchange.Infrastructe.Extensions;
using GameExchange.Infrastructe.Security.Cryptogaphy;
using GameExchange.Infrastructe.Security.Tokens.Access.Generator;
using GameExchange.Infrastructe.Security.Tokens.Access.Validator;
using GameExchange.Infrastructe.Security.Tokens.Refresh;
using GameExchange.Infrastructe.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameExchange.Infrastructe
{
    public static class DependecyInjectionExtension
    {
        public static void AddInfrastructe(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddPasswordEncripter(services);
            AddToken(services, configuration);
            AddLoggedUser(services);

            if (configuration.IsUnitTestEnviroment())
            {
                return;
            }

            AddDbContext(services, configuration);
            AddFluentMigrator(services, configuration);
          
        }

        private static void AddRepositories(IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();

            services.AddScoped<IPlatformWriteOnlyRepository, PlatformRepository>();
            services.AddScoped<IPlatformUpdateOnlyRepository, PlatformRepository>();
            services.AddScoped<IPlatformReadOnlyRepository, PlatformRepository>();


            services.AddScoped<ICategoryWriteOnlyRepository, CategoryRepository>();
            services.AddScoped<ICategoryUpdateOnlyRepository, CategoryRepository>();
            services.AddScoped<ICategoryReadOnlyRepository, CategoryRepository>();

            services.AddScoped<IGameWriteOnlyRepository, GameRepository>();
            services.AddScoped<IGameUpdateOnlyRepository, GameRepository>();
            services.AddScoped<IGameReadOnlyRepository, GameRepository>();


            services.AddScoped<IListingWriteOnlyRepository, ListingRepository>();
            services.AddScoped<IListingReadOnlyRepository, ListingRepository>();
            services.AddScoped<IListingUpdateOnlyRepository, ListingRepository>();


            services.AddScoped<IOrderWriteOnlyRepository, OrderRepositoy>();


            

            services.AddScoped<ITokenRepository, TokenRepository>();
        }
        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameExchangeDbContext>(options =>
            {
                options.UseSqlServer(configuration.ConnectionString());
            });
        }

        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAccessTokenGenerator>(option => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
            services.AddScoped<IAccessTokenValidator>(option => new JwtTokenValidator(signingKey!));

            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
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

        private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

    }
}
