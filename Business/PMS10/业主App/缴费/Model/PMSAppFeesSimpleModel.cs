using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.业主App.缴费.Model
{
    public class PMSAppFeesSimpleModel
    {
        public long FeesID { get; set; }

        public decimal DueAmount { get; set; }

        public decimal DebtsAmount { get; set; }

        public decimal LateFeeAmount { get; set; }

        public string FeesDueDate { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal PrecAmount { get; set; }
    }
}
