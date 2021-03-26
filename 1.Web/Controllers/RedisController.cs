using Core.Utility.Redis;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace Web.Controllers {

    public class RedisController : Controller {

        public IActionResult Index() {
            RedisProvider redisProvider = new RedisProvider(ConfigService.Redis_ConnectionString);

            List<string> keys = redisProvider.GetAllKeys();
            IEnumerable<RedisKeyValuePair> pairs = keys.Select(x => {
                return new RedisKeyValuePair {
                    KeyStr = x,
                    ValueStr = redisProvider.StringGet(x)
                };
            });
            //List<RedisKeyValuePair> pairs = new List<RedisKeyValuePair>();
            return View(pairs);
        }
    }

    public class RedisKeyValuePair {
        public string KeyStr { get; set; }
        public string ValueStr { get; set; }
    }
}