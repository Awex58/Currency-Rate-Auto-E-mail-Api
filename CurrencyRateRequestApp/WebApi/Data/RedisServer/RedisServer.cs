using System;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace WebApi.Data.RedisServer
{
    public class RedisServer
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;

        public RedisServer(IConfiguration configuration)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect("localhost,allowAdmin=true");
            _database = _connectionMultiplexer.GetDatabase();
        }
        public IDatabase Database => _database;


    }
}