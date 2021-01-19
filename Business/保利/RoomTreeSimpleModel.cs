using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    internal class RoomTableSimpleModel
    {
        public long RoomID { get; set; }
        public string RoomSign { get; set; }
        public string RoomName { get; set; }
        public string BuildSNum { get; set; }
        public string BuildName { get; set; }
        public string UnitSNum { get; set; }
        public string UnitName { get; set; }

        public string FloorSNum { get; set; }
    }

    internal class CustRoomTableSimpleModel : RoomTableSimpleModel
    {
        public long CustID { get; set; }
        public string CustName { get; set; }
        public string MobilePhone { get; set; }
    }

    public class RoomSimpleModel
    {
        public long RoomID { get; set; }
        public string RoomSign { get; set; }
        public string RoomName { get; set; }
    }

    public class BuildTreeSimpleModel
    {
        public string BuildSNum { get; set; }
        public string BuildName { get; set; }
        public List<UnitTreeSimpleModel> Units { get; set; }
    }

    public class UnitTreeSimpleModel
    {
        public string UnitSNum { get; set; }
        public string UnitName { get; set; }
    }

    public class UnitTreeSimpleModel<T> : UnitTreeSimpleModel
    {
        public List<T> Rooms { get; set; }
    }

    public class BuildUnitTreeSimpleModel
    {
        public string BuildSNum { get; set; }
        public string BuildName { get; set; }
        public string UnitSNum { get; set; }
        public string UnitName { get; set; }
    }

    public class BuildUnitTreeSimpleModel<T> : BuildUnitTreeSimpleModel
    {
        public List<T> Rooms { get; set; }
    }
}
