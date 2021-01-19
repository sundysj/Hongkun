using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianChengEntranceSyncService.Util
{
    public class DateHelper
    {
        public static long Get13TimeStamp(DateTime dateTime)
        {
            return (dateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        public static long Get10TimeStamp(DateTime dateTime)
        {
            return Get13TimeStamp(dateTime) / 1000;
        }
    }
}
