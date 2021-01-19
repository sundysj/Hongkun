using System.Web.Configuration;
using System.Data;

namespace Common
{
    public class Global_Enum
    {
        private Global_Enum()
        {

        }
        #region 操作类型

        public enum OprState : int 
        {
            Insert=0,
            Update=1,
            Delete=2,
            Select=3
        }

        #endregion
    }
}