using Core.Models.DTO;
using Core.Utility.Redis;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web.Services;

namespace Web.Controllers
{

    public class RedisController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Redis/GetAllKVP")]
        public Dictionary<string, string> GetAllKeysAndValues()
        {
            RedisProvider redisProvider = new RedisProvider(ConfigService.Redis_ConnectionString);

            var dict = redisProvider.GetKeyValuePairs();
            return dict;
        }

        [HttpPost("Redis/Set")]
        public bool Set([FromBody] RedisKeyValueRequestModel request)
        {
            RedisProvider redisProvider = new RedisProvider(ConfigService.Redis_ConnectionString);

            bool result = redisProvider.Set(request.Key, request.Value);
            if (result == false)
            {
                throw new Exception($"設定鍵值失敗, key: {request.Key}, value: {request.Value}");
            }
            return result;
        }

        [HttpDelete("Redis/Key/{key}")]
        public bool DeleteKeyAndValue(string key)
        {
            RedisProvider redisProvider = new RedisProvider(ConfigService.Redis_ConnectionString);

            bool result = redisProvider.DeleteKey(key);
            if (result == false)
            {
                throw new Exception($"刪除鍵失敗, key: {key}");
            }
            return result;
        }

        public bool Test()
        {
            RedisProvider redisProvider = new RedisProvider(ConfigService.Redis_ConnectionString);

            bool result = redisProvider.Set("test", "testValue", new TimeSpan(0, 0, 0, 10));
            return result;
        }
    }
}