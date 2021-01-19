using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianChengEntranceSyncService.Model
{
    public class EntranceGroup
    {
        public int Id { get; set; }
        public int CommID { get; set; }
        public int BuildSNum { get; set; }
        public int UnitSNum { get; set; }
        public long GroupId { get; set; }
    }
}
