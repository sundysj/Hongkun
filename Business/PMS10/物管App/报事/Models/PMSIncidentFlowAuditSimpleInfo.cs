namespace Business.PMS10.物管App.报事.Models
{
    public partial class PMSIncidentPush
    {
        public class PMSIncidentFlowAuditSimpleInfo
        {
            public long IncidentID { get; set; }
            public string RoleCode { get; set; }
            public string AuditUser { get; set; }
            public string AuditUserName { get; set; }
            public string AuditState { get; set; }
            public int OrderId { get; set; }
            public int IsAudit { get; set; }
        }
    }


}
