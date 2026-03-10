using GameExchange.API.Filters;
using GameExchange.API.Middleware;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameExchange.API
{
    public static class DependecyInjectionExtension
    {

        public static void AddApi(this IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add<ExceptionFilter>());
            //services.AddScoped<ITokenProvider, HttpContextTokenValue>();
            //services.AddHostedService<DeleteUserService>();


        }

      
    }
}
