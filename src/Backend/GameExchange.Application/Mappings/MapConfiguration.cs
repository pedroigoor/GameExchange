using GameExchange.Communication.Request;
using Mapster;

namespace GameExchange.Application.Mappings
{
    public static class MapConfiguration
    {
        public static void Configure()
        {
            TypeAdapterConfig<RequestRegisterUserJson, Domain.Entities.User>
                .NewConfig()
                .Ignore(dest => dest.Password);
        }

    }
}
