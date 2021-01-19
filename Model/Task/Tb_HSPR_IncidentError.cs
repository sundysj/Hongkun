using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Task
{
    public class Tb_HSPR_IncidentError
    {
        public Tb_HSPR_IncidentError()
        {

        }

        #region Model
        private string _state;
        private string _errorcontent;
        private string _method;
        private DateTime? _errordate;
        private string _parameter;


        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorContent
        {
            set { _errorcontent = value; }
            get { return _errorcontent; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Method
        {
            set { _method = value; }
            get { return _method; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ErrorDate
        {
            set { _errordate = value; }
            get { return _errordate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Parameter
        {
            set { _parameter = value; }
            get { return _parameter; }
        }


        #endregion Model
    }


}
