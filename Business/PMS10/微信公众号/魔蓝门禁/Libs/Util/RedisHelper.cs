using StackExchange.Redis;
using System;

namespace TianChengEntranceSyncService.Util
{
    public class RedisHelper
    {
        private static readonly object locker = new object();

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(RedisConnection);
        });

        public static ConnectionMultiplexer RedisClient
        {
            get
            {
                return lazyConnection.Value;
            }
        }
        /// <summary>
        /// 获取Redis链接
        /// </summary>
        private static string RedisConnection
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RedisConnection"];
            }
        }
    }
}
