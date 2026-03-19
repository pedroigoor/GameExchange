using GameExchange.Application.Mappings;
using GameExchange.Application.UseCases.Login.LoginInterno;
using GameExchange.Application.UseCases.Token.RefreshToken;
using GameExchange.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameExchange.Application
{
    public static class DependecyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddUseCase(services);
            AddMapsters();
        }

        private static void AddUseCase(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IUseRefreshTokenUseCase, UseRefreshTokenUseCase>();
        }

        private static void AddMapsters()
        {
            MapConfiguration.Configure();
        }


    }
}
