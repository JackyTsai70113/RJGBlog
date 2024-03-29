﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Core.Helpers
{
    /// <summary>
    /// Redis Helper
    /// </summary>
    /// <remarks>
    /// https://stackexchange.github.io/StackExchange.Redis/
    /// </remarks>
    public class RedisHelper
    {
        public readonly IConfiguration _configuration;

        /// <summary>
        /// 取得 IServer，用來處理 key 相關的功能
        /// </summary>
        private readonly IServer server;

        /// <summary>
        /// 取得 IDatabase，用來處理 Value 相關的功能
        /// </summary>
        private readonly IDatabase _db;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="connectionString">連接字串</param>
        public RedisHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetSection("Redis").GetSection("ConnectionString").Value;

            ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);
            ConnectionMultiplexer _connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options)).Value;
            server = _connection.GetServer(options.EndPoints.First());
            _db = _connection.GetDatabase();
        }

        public string GetValue(string key)
        {
            string value = _db.StringGet(key);
            return value;
        }

        public IEnumerable<string> GetAllKeyStrs()
        {
            IEnumerable<string> keys = GetAllKeys().Select(x => x.ToString());
            return keys;
        }

        public IEnumerable<RedisKey> GetAllKeys()
        {
            IEnumerable<RedisKey> redisKeys = GetKeysByPattern();
            return redisKeys;
        }

        /// <summary>
        /// 取得指定 <paramref name="pattern"/> 的 Redis 鍵值對，沒給 <paramref name="pattern"/> 則全取
        /// </summary>
        /// <param name="pattern">模式</param>
        /// <returns>鍵值對</returns>
        public Dictionary<string, string> GetKeyValuePairs(string pattern = "*")
        {
            IEnumerable<RedisKey> redisKeys = server.Keys(pattern: $"{pattern}");
            Dictionary<string, string> redisKVPs = new();
            foreach (RedisKey redisKey in redisKeys)
            {
                RedisValue redisValue = _db.StringGet(redisKey);
                redisKVPs.Add(redisKey.ToString(), redisValue.ToString());
            }
            return redisKVPs;
        }

        public IEnumerable<string> GetKeyStrsByPattern(string pattern = "*")
        {
            IEnumerable<string> keys = GetKeysByPattern(pattern).Select(k => k.ToString());
            return keys;
        }

        /// <summary>
        /// 設定 redis 鍵值
        /// </summary>
        /// <param name="key">鍵</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        public bool Set(string key, string value)
        {
            return _db.StringSet(key, value);
        }

        public bool Set(string key, string value, DateTime expires)
        {
            TimeSpan expiryTimeSpan = expires.Subtract(DateTime.UtcNow);

            return _db.StringSet(key, value, expiryTimeSpan);
        }

        /// <summary>
        /// 設定 redis 鍵值
        /// </summary>
        /// <param name="key">鍵</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        public bool Set(string key, string value, TimeSpan timeSpan)
        {
            return _db.StringSet(key, value, timeSpan);
        }

        public bool DeleteKey(string key)
        {
            return _db.KeyDelete(key);
        }

        private IEnumerable<RedisKey> GetKeysByPattern(string pattern = "*")
        {
            IEnumerable<RedisKey> keys = server.Keys(pattern: $"{pattern}");
            return keys;
        }

        // private RedisKey ToRedisKey(string keyStr)
        // {
        //     RedisKey redisKey = keyStr;
        //     return redisKey;
        // }

        // private IEnumerable<RedisKey> ToRedisKeys(IEnumerable<string> keyStrs)
        // {
        //     IEnumerable<RedisKey> redisKeys = keyStrs.Select(k => (RedisKey)k);
        //     return redisKeys;
        // }
    }
}