using RabbitMQ.Client;

namespace GameExchange.Infrastructe.Services.RebbitMQ
{
    public class RabbitMqConnection(RabbitMqSettings settings)
    {
        private readonly RabbitMqSettings _settings = settings;

        public async Task<IConnection> CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.Username,
                Password = _settings.Password
            };

            return await factory.CreateConnectionAsync();
        }
    }
}
