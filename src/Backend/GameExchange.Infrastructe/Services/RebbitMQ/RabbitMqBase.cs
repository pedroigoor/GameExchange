using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Infrastructe.Services.RebbitMQ
{
    public abstract class RabbitMqBase(RabbitMqConnection connection)
    {
        protected readonly RabbitMqConnection _connection = connection;

        protected async Task<(IConnection, IChannel)> CreateChannel()
        {
            var connection = await _connection.CreateConnection();
            var channel = await connection.CreateChannelAsync();

            return (connection, channel);
        }
    }
}
