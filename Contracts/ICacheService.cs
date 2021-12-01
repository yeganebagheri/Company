using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperASPNetCore.Contracts
{
    public interface ICacheService
    {
        public Task<string> GetCacheValueAsync(string key);
        public Task<string> SetCacheValueAsync(string key,string value);
    }
}

//using Microsoft.Extensions.Caching.Distributed;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DapperASPNetCore.Contracts
//{
//    public interface IDistributedCache
//    {
//        byte[] Get(string key);
//        Task<byte[]> GetAsync(string key);
//        void Refresh(string key);
//        Task RefreshAsync(string key);
//        void Remove(string key);
//        Task RemoveAsync(string key);
//        void Set(string key, byte[] value, DistributedCacheEntryOptions options);
//        Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options);
//    }

//}
