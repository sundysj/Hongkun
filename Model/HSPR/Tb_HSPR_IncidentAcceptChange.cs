using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HSPR
{
    [Serializable]
    public class Tb_HSPR_IncidentAcceptChange
    {
        public Tb_HSPR_IncidentAcceptChange() { }

        #region Model
        private int _IID;
        private int _IncidentID;
        private string _ChangeDate;
        private string _ChangeUserCode;

        public int IID
        {
            get { return _IID; }
            set { _IID = value; }
        }

        public int IncidentID
        {
            get
            {
                return _IncidentID;
            }

            set
            {
                _IncidentID = value;
            }
        }

        public string ChangeDate
        {
            get
            {
                return _ChangeDate;
            }

            set
            {
                _ChangeDate = value;
            }
        }

        public string ChangeUserCode
        {
            get
            {
                return _ChangeUserCode;
            }

            set
            {
                _ChangeUserCode = value;
            }
        }
        #endregion
    }
}
