using DapperASPNetCore.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Repository
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();

            return await db.StringGetAsync(key);
        }
        
        

        public async Task<string> SetCacheValueAsync(string key , string value)
        {
            var db = _connectionMultiplexer.GetDatabase();

            await db.StringSetAsync(key, value);
            return ("OK");
        }
    }
}
