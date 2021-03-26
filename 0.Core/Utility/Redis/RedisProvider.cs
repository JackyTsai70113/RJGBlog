using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Utility.Redis {

    /// <summary>
    /// Redis Provider
    /// </summary>
    /// <remarks>
    /// https://stackexchange.github.io/StackExchange.Redis/
    /// </remarks>
    public class RedisProvider {

        //private readonly string _connectionString;
        private static Lazy<ConnectionMultiplexer> _connection;

        private IDatabase db;
        private IServer redisServer;

        public RedisProvider(string connectionString) {
            //_connectionString = connectionString;
            ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);
            _connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
            redisServer = _connection.Value.GetServer(options.EndPoints.First());
            var dd = redisServer.Keys().ToList();
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
            db = redis.GetDatabase();
        }

        public string StringGet(string keyStr) {
            try {
                string valueStr = db.StringGet(keyStr);
                return valueStr;
            } catch (Exception ex) {
                Console.WriteLine($"ex: {ex}");
                throw;
            }
        }

        public List<string> GetAllKeys() {
            try {
                List<string> keyStrs = redisServer.Keys().Select(x => x.ToString()).ToList();
                return keyStrs;
            } catch (Exception ex) {
                Console.WriteLine($"ex: {ex}");
                throw;
            }
        }

        public List<string> GetKeysByPattern(string pattern = "*") {
            try {
                List<string> keyStrs =
                    redisServer.Keys(pattern: $"{pattern}").Select(x => x.ToString()).ToList();
                return keyStrs;
            } catch (Exception ex) {
                Console.WriteLine($"ex: {ex}");
                throw;
            }
        }

        public bool StringSet(string keyStr, string valueStr) {
            try {
                db.StringSet(keyStr, valueStr);
                return true;
            } catch (Exception ex) {
                // Log
                return false;
            }
        }
    }
}