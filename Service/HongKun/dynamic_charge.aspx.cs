using System;

namespace Service.HongKun
{
    public partial class dynamic_charge : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString.Get("organcode")) || string.IsNullOrEmpty(Request.QueryString.Get("logincode")))
            {
                throw new ArgumentNullException("组织机构代码或用户账号不能为空");
            }

            this.OrganCodeStr = Request.QueryString.Get("organcode");
            this.LoginCodeStr = Request.QueryString.Get("logincode");

            this.OrganCode.Value = this.OrganCodeStr;
            this.OrganName.Value = this.GetOrganName(this.OrganCodeStr);
            this.UserCode.Value = this.GetUserCode(this.LoginCodeStr);
        }
    }
}