using GameExchange.Application.Mappings;
using GameExchange.Application.UseCases.Category.List;
using GameExchange.Application.UseCases.Category.Register;
using GameExchange.Application.UseCases.Category.Update;
using GameExchange.Application.UseCases.Game.List;
using GameExchange.Application.UseCases.Game.Register;
using GameExchange.Application.UseCases.Login.LoginInterno;
using GameExchange.Application.UseCases.Platform.List;
using GameExchange.Application.UseCases.Platform.Register;
using GameExchange.Application.UseCases.Platform.Update;
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

            services.AddScoped<ISavePlatformUseCase, SavePlatformUseCase>();
            services.AddScoped<IUpdatePlatformUseCase, UpdatePlatformUseCase>();
            services.AddScoped<IListPlatformUseCase, ListPlatformUseCase>();

            services.AddScoped<IListCategoryUseCase, ListCategoryUseCase>();
            services.AddScoped<ISaveCategoryUseCase, SaveCategoryUseCase>();
            services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();

            services.AddScoped<IListGameUseCase, ListGameUseCase>();
            services.AddScoped<ISaveGameUseCase, SaveGameUseCase>();
            //services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();

            services.AddScoped<IUseRefreshTokenUseCase, UseRefreshTokenUseCase>();
        }

        private static void AddMapsters()
        {
            MapConfiguration.Configure();
        }


    }
}
