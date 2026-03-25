using GameExchange.API.BackgroundServices;
using GameExchange.API.Filters;
using GameExchange.API.Middleware;
using GameExchange.API.Token;
using GameExchange.Domain.Security.Tokens;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameExchange.API
{
    public static class DependecyInjectionExtension
    {

        public static void AddApi(this IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add<ExceptionFilter>());
            services.AddScoped<ITokenProvider, HttpContextTokenValue>();
            services.AddHostedService<OrderCreatedConsumer>();
            services.AddHostedService<PaymentRequestedConsumer>();
            services.AddHostedService<PaymentRejectedConsumer>();
            services.AddHostedService<PaymentAproveConsumer>();


        }

      
    }
}
