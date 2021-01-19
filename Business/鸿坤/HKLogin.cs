using EIAC.SSO.PSO;
using MobileSoft.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Business
{
    public class HKLogin
    {
        public static ConcurrentDictionary<string, HKUserModel> LoggedUsers = new ConcurrentDictionary<string, HKUserModel>();
        private HttpRequest _request;

        public HKLogin(HttpRequest request)
        {
            _request = request;
        }

        public string Login()
        {
            if (!AppSSOBLL.TOEACAuthenticat("01", AppSSOBLL.GetTimeStamp(), InterfaceUtility.GetNodeValueForConfig("BackUrl"), ""))
            {
                return JSONHelper.FromString(true, "连接成功");
            }

            return JSONHelper.FromString(true, "连接失败");
        }

        public string Valiadate()
        {
            //接受EAC发送回来的认证信息,如果通过定位到保护页面
            if (_request["IASID"] != null)
            {
                if (_request["Result"].ToString() == "0")
                {
                    var account = _request["Account"].ToString();
                    var eacToken = _request["IASID"].ToString();

                    if (AppSSOBLL.ValidateFromEAC(eacToken,
                            _request["TimeStamp"].ToString(),
                            _request["UserAccount"].ToString(),
                            _request["Result"].ToString(),
                            _request["ErrorDescription"].ToString(),
                            _request["Authenticator"].ToString())
                        )
                    {
                        HKLogin.LoggedUsers.AddOrUpdate(account, new HKUserModel()
                        {
                            Account = account,
                            EACToken = eacToken,
                            LastActivityTime = DateTime.Now
                        }, (key, value) =>
                        {
                            value.Account = account;
                            value.EACToken = eacToken;
                            value.LastActivityTime = DateTime.Now;
                            return value;
                        });

                        // 登录成功
                        return JSONHelper.FromString(true, eacToken);
                    }
                }
            }

            return JSONHelper.FromString(false, "账号或密码错误");
        }
    }
}
