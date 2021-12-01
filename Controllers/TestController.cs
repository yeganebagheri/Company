using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperASPNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
       
        public TestController(IDistributedCache distributedCache)
        {

            _distributedCache = distributedCache;
            
        }

        [HttpGet("allCached")]
        public async Task<IActionResult> GetCache()
        {
            var serializedCustomerList = JsonConvert.SerializeObject("testvalue");
            var cacheKeyTest = Encoding.UTF8.GetBytes(serializedCustomerList);
            var options = new DistributedCacheEntryOptions()
                       .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                       .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            await _distributedCache.SetAsync("testKey", cacheKeyTest, options);

            var cachedTest = await _distributedCache.GetAsync("testKey");
            Console.WriteLine("Value cached in redis server is: " + cachedTest);
            Console.ReadLine();
            return Ok();
        }
           
    }
}
