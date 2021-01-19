using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianChengEntranceSyncService.Util;

namespace TianChengEntranceSyncService.Redis
{
    public class Redis : IRedis
    {
        protected static readonly StackExchange.Redis.IDatabase mRedis = RedisHelper.RedisClient.GetDatabase();

    }
}
