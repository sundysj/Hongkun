using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.物管App.报事.Models
{
    public class PMSIncidentSelUserModel
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string EmployeeProfession { get; set; }
        public string EmployeeStar { get; set; }
        public string EmployeeStatus { get; set; }
        public int CommID { get; set; }
        public string Tip { get; set; }
        public int IncidentCount { get; set; }

        public string ClockLocation { get; set; }

        public string ClockOnlineState { get; set; }

        public string LastLocation { get; set; }
    }
}
