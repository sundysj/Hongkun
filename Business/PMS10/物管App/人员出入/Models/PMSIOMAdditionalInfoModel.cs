using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.物管App.人员出入.Models
{
    public class PMSIOMAdditionalInfoModel
    {
        public string IID { get; set; }
        public string InfoTitle { get; set; }
        public int IsOption { get; set; }

        public List<PMSIOMAdditionalInfoOptionModel> Options { get; set; }
    }
}
