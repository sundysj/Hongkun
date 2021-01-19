using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianChengEntranceSyncService.Attribute
{
    public class SyncWorkException: Exception
    {
        public SyncWorkException(string message) : base(message)
        {

        }
    }
}
