using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class RedisController : Controller
    {
        private readonly RedisHelper _redisHelper;

        public RedisController(RedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Redis/GetAllKVP")]
        public Dictionary<string, string> GetAllKeysAndValues()
        {
            var dict = _redisHelper.GetKeyValuePairs();
            return dict;
        }

        [HttpPost("Redis/Set")]
        public bool Set([FromBody] RedisKeyValueRequestModel request)
        {
            bool result = _redisHelper.Set(request.Key, request.Value);
            if (result == false)
            {
                throw new Exception($"設定鍵值失敗, key: {request.Key}, value: {request.Value}");
            }
            return result;
        }

        [HttpDelete("Redis/Key/{key}")]
        public bool DeleteKeyAndValue(string key)
        {
            bool result = _redisHelper.DeleteKey(key);
            if (result == false)
            {
                throw new Exception($"刪除鍵失敗, key: {key}");
            }
            return result;
        }

        public bool Test()
        {
            bool result = _redisHelper.Set("test", "testValue", new TimeSpan(0, 0, 0, 10));
            return result;
        }
    }
}