using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ERPMessageModel
    {
        public string IID { get; set; }

        public int Level { get; set; }

        public string Title { get; set; }
        
        public string IssueDate { get; set; }
    }
}
