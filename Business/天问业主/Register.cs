using System;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using System.Data;
using System.Text;
using Common;
using MobileSoft.BLL.Common;
using MobileSoft.Model.Common;
using MobileSoft.Model.Unified;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using System.Linq;
using System.Collections.Generic;
using System.Web;

namespace Business
{
    /// <summary>
    /// 注册
    /// </summary>
    public class Register : PubInfo
    {
        public Register() //获取小区、项目信息
        {
            base.Token = "20160803Register";
        }
        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "RegisterUser":
                    Trans.Result = RegisterUser(Row);//注册用户
                    break;
                case "RegisterUser_WeiXin"://微信公共号注册
                    Trans.Result = RegisterUser_WeiXin(Row);
                    break;
                case "RegisterUser_WeiXin_hnc"://微信公共号注册,华南城
                    Trans.Result = RegisterUser_WeiXin_hnc(Row);
                    break;
                    
                case "GetVerifyCode"://获取验证码
                    Trans.Result = GetVerifyCode(Row, Trans);
                    break;
                case "UserLogin"://业主登录
                    Trans.Result = UserLogin(Row, Trans);
                    break;
                case "UserLoginLocationCommunity"://业主登录，自动定位到小区
                    Trans.Result = UserLoginLocationCommunity(Row, Trans);
                    break;
                case "UserLogin_WeiXin"://微信公共号登录
                    Trans.Result = UserLogin_WeiXin(Row);
                    break;
                case "CheckPayInfo":
                    Trans.Result = CheckPayInfo(Row, Trans);
                    break;
                case "CheckRedWuYeInfo":
                    Trans.Result = CheckRedWuYeInfo(Row, Trans);
                    break;
                case "GetVerifyCodeForPwd"://发送验证码---忘记密码
                    Trans.Result = GetVerifyCodeForPwd(Row, Trans);
                    break;
                case "CheckVerifyCode"://验证修改密码的验证码
                    Trans.Result = CheckVerifyCode(Row);
                    break;
                case "UpdateUserPwd"://修改密码
                    Trans.Result = UpdateUserPwd(Row);
                    break;
                case "RegisterUser_WeiXin_RH"://微信公共号注册_融汇
                    Trans.Result = RegisterUser_WeiXin_RH(Row);
                    break;
                default:
                    break;
            }
        }

        private string RegisterUser_WeiXin_RH(DataRow Row)
        {
            string backstr = "";
            string UserId = "";
            try
            {

                if (Row.Table.Columns.Contains("OpenId") && !string.IsNullOrEmpty(Row["OpenId"].ToString()))
                {
                    DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT WeChatToken FROM Tb_User_WeiXin  where WeChatToken='" + Row["OpenId"].ToString() + "'").Tables[0];


                    if (dTable.Rows.Count > 0)
                    {
                        return JSONHelper.FromString(false, "此用户已存在");
                    }
                    else
                    {
                        string Pwd = "";
                        if (Row.Table.Columns.Contains("Pwd"))
                        {
                            Pwd = Row["Pwd"].ToString();
                        }
                        if (string.IsNullOrEmpty(Pwd))
                        {
                            return JSONHelper.FromString(false, "密码不能为空");
                        }
                        Random ro = new Random();
                        string Name = "";
                        string Email = "";
                        string QQ = "";
                        string QQToken = "";
                        string WeChatToken = "";
                        string NickName = Row["Mobile"].ToString();//游客

                        string UserPic = "";
                        int Sex = 1;
                        if (Row.Table.Columns.Contains("Name"))
                        {
                            Name = Row["Name"].ToString();
                        }
                        if (Row.Table.Columns.Contains("Email"))
                        {
                            Email = Row["Email"].ToString();
                        }
                        if (Row.Table.Columns.Contains("QQ"))
                        {
                            QQ = Row["QQ"].ToString();
                        }
                        if (Row.Table.Columns.Contains("QQToken"))
                        {
                            QQToken = Row["QQToken"].ToString();
                        }
                        if (Row.Table.Columns.Contains("WeChatToken"))
                        {
                            WeChatToken = Row["WeChatToken"].ToString();
                        }
                        if (Row.Table.Columns.Contains("NickName") && !string.IsNullOrEmpty(Row["NickName"].ToString()))
                        {
                            NickName = Row["NickName"].ToString();
                        }

                        if (Row.Table.Columns.Contains("UserPic"))
                        {
                            UserPic = Row["UserPic"].ToString();
                        }
                        if (Row.Table.Columns.Contains("Sex"))
                        {
                            Sex = AppGlobal.StrToInt(Row["Sex"].ToString());
                        }
                        MobileSoft.Model.Unified.Tb_User User = new MobileSoft.Model.Unified.Tb_User();
                        User.Id = Guid.NewGuid().ToString();
                        User.Name = Name;
                        User.Mobile = Row["Mobile"].ToString();
                        User.QQ = QQ;
                        User.QQToken = QQToken;
                        User.WeChatNum = WeChatToken;
                        User.WeChatToken = WeChatToken;
                        User.Pwd = Pwd;
                        User.NickName = NickName;
                        User.Email = Email;
                        User.UserPic = UserPic;
                        User.Sex = Sex;
                        User.RegDate = DateTime.Now;
                        //新增用户
                        new MobileSoft.BLL.Unified.Bll_Tb_User().Add(User);
                        UserId = User.Id;
                        //新增openid
                        new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).ExecuteSql("insert into Tb_User_WeiXin(UserId,WeChatToken) values('" + User.Id + "','" + Row["OpenId"] + "')");

                    }
                }
                else
                {
                    return JSONHelper.FromString(false, "用户名不能为空!");
                }
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }
            if (backstr == "")
            {
                return JSONHelper.FromString(true, UserId);
            }
            else
            {
                return JSONHelper.FromString(false, backstr);
            }
        }

        /// <summary>
        /// 小区是否支持红色物业
        /// </summary>
        /// <param name="row"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private string CheckRedWuYeInfo(DataRow row, Transfer trans)
        {
            string result = "0";
            if (!row.Table.Columns.Contains("CommunityId") && String.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return result;
            }

            string CommunityId = row["CommunityId"].ToString();
            string sql = string.Format("select CommID from Tb_Community where Id='{0}'", row["CommunityId"].ToString());

            DataTable dTable = new DbHelperSQLP(PubConstant.GetConnectionString("UnifiedConnectionString")).Query(sql.ToString()).Tables[0];
            if (dTable.Rows.Count > 0)
            {
                string CommID = dTable.Rows[0]["CommID"].ToString();
                if (!string.IsNullOrEmpty(CommID))
                {
                    string haveRedWuYe = Global_Fun.AppWebSettings("HaveRedWuYeCommID").ToString();
                    if (!string.IsNullOrEmpty(haveRedWuYe))
                    {
                        if (haveRedWuYe.Contains(CommunityId))
                        {
                            result = "1";
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 小区开通支付通道查询
        /// </summary>
        /// <param name="row"></param>
        /// <param name="trans"></param>
        /// CommunityId    小区编号【必填】
        /// <returns></returns>
        private string CheckPayInfo(DataRow row, Transfer trans)
        {
            if (!row.Table.Columns.Contains("CommunityId") && String.IsNullOrEmpty(row["CommunityId"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            string CommunityId = row["CommunityId"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",\"data\":[{");
            IDbConnection con = new SqlConnection(PubConstant.UnifiedContionString.ToString());
            //微信
            string sql = "select * from Tb_WeiXinPayCertificate where  ISNULL(IsEnable,0)=0 and  CommunityId=@CommunityId";
            List<Tb_WeiXinPayCertificate> list_weixin = con.Query<Tb_WeiXinPayCertificate>(sql, new { CommunityId = CommunityId }).ToList<Tb_WeiXinPayCertificate>();
            if (list_weixin.Count > 0)
            {
                sb.Append("\"WChatPay\":\"true\",");
            }
            else
            {
                sb.Append("\"WChatPay\":\"false\",");
            }
            //支付宝
            sql = "select * from Tb_AlipayCertifiate where  ISNULL(IsEnable,0)=0 and  CommunityId=@CommunityId";
            List<Tb_AlipayCertifiate> list_Alipay = con.Query<Tb_AlipayCertifiate>(sql, new { CommunityId = CommunityId }).ToList<Tb_AlipayCertifiate>();
            if (list_Alipay.Count > 0)
            {
                sb.Append("\"AliPay\":\"true\",");
            }
            else
            {
                sb.Append("\"AliPay\":\"false\",");
            }
            //银联
            sql = "select * from Tb_UnionPayCertificate where  ISNULL(IsEnable,0)=0 and  CommunityId=@CommunityId";
            List<Tb_UnionPayCertificate> list_UnionPay = con.Query<Tb_UnionPayCertificate>(sql, new { CommunityId = CommunityId }).ToList<Tb_UnionPayCertificate>();
            if (list_UnionPay.Count > 0)
            {
                sb.Append("\"UnionPay\":\"true\"");
            }
            else
            {
                sb.Append("\"UnionPay\":\"false\"");
            }


            sb.Append("}]}");
            return sb.ToString();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="row"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private string UserLogin(DataRow row, Transfer trans)
        {
            string Mobile = "";
            string QQToken = "";
            string WeChatToken = "";
            string Pwd = "";


            if (row.Table.Columns.Contains("Mobile"))
            {
                Mobile = row["Mobile"].ToString();
            }
            if (row.Table.Columns.Contains("QQToken"))
            {
                QQToken = row["QQToken"].ToString();
            }
            if (row.Table.Columns.Contains("WeChatToken"))
            {
                WeChatToken = row["WeChatToken"].ToString();
            }
            if (row.Table.Columns.Contains("Pwd"))
            {
                Pwd = row["Pwd"].ToString();
            }
            if (!string.IsNullOrEmpty(Mobile) && string.IsNullOrEmpty(Pwd))
            {
                return JSONHelper.FromString(false, "密码不能为空");
            }
            if (string.IsNullOrEmpty(Mobile) && string.IsNullOrEmpty(WeChatToken) && string.IsNullOrEmpty(QQToken))
            {
                return JSONHelper.FromString(false, "登录名称不能为空");
            }
            if (!string.IsNullOrEmpty(Mobile) && !string.IsNullOrEmpty(Pwd) || !string.IsNullOrEmpty(WeChatToken) || !string.IsNullOrEmpty(QQToken))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT Id,Name,Mobile,Email,QQ,QQToken,WeChatToken,WeChatNum,NickName,UserPic,Sex FROM Tb_User  where 1=1  ");
                if (Mobile != "")
                {
                    sb.Append(" and Mobile='" + Mobile + "'");
                    sb.Append(" and Pwd='" + Pwd + "'");
                }
                if (QQToken != "")
                {
                    sb.Append(" and QQToken='" + QQToken + "'");
                }
                if (WeChatToken != "")
                {
                    sb.Append(" and WeChatToken='" + WeChatToken + "'");
                }


                //登录
                DataTable dTable = new DbHelperSQLP(PubConstant.GetConnectionString("UnifiedConnectionString")).Query(sb.ToString()).Tables[0];
                if (dTable.Rows.Count > 0)
                {
                    if (Global_Var.LoginCorpID =="2045")//嘉禾登录日志
                    {
                        JH_OperateLog.WriteLog("LoginLog");
                    }
                    return JSONHelper.FromString(dTable);
                }
                else
                {
                    return JSONHelper.FromString(false, "账号名或者密码错误");
                }
            }
            else
            {
                return JSONHelper.FromString(false, "账号名或者密码为空");
            }



        }

        private string UserLoginLocationCommunity(DataRow row, Transfer trans)
        {
            string Mobile = "";
            string QQToken = "";
            string WeChatToken = "";
            string Pwd = "";


            if (row.Table.Columns.Contains("Mobile"))
            {
                Mobile = row["Mobile"].ToString();
            }
            if (row.Table.Columns.Contains("QQToken"))
            {
                QQToken = row["QQToken"].ToString();
            }
            if (row.Table.Columns.Contains("WeChatToken"))
            {
                WeChatToken = row["WeChatToken"].ToString();
            }
            if (row.Table.Columns.Contains("Pwd"))
            {
                Pwd = row["Pwd"].ToString();
            }
            if (!string.IsNullOrEmpty(Mobile) && string.IsNullOrEmpty(Pwd))
            {
                return JSONHelper.FromString(false, "密码不能为空");
            }
            if (string.IsNullOrEmpty(Mobile) && string.IsNullOrEmpty(WeChatToken) && string.IsNullOrEmpty(QQToken))
            {
                return JSONHelper.FromString(false, "登录名称不能为空");
            }
            if (!string.IsNullOrEmpty(Mobile) && !string.IsNullOrEmpty(Pwd) || !string.IsNullOrEmpty(WeChatToken) || !string.IsNullOrEmpty(QQToken))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT Id,Name,Mobile,Email,QQ,QQToken,WeChatToken,WeChatNum,NickName,UserPic,Sex FROM Tb_User  where 1=1  ");
                if (Mobile != "")
                {
                    sb.Append(" and Mobile='" + Mobile + "'");
                    sb.Append(" and Pwd='" + Pwd + "'");
                }
                if (QQToken != "")
                {
                    sb.Append(" and QQToken='" + QQToken + "'");
                }
                if (WeChatToken != "")
                {
                    sb.Append(" and WeChatToken='" + WeChatToken + "'");
                }

                using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString("UnifiedConnectionString")))
                {
                    dynamic result = conn.Query(sb.ToString()).FirstOrDefault();

                    if (result != null)

                    {
                        var sql = @"SELECT count(1) FROM syscolumns WHERE id=object_id('Tb_Community') AND name='IsMultiDoorControlServer'";
                        if (conn.Query<int>(sql).FirstOrDefault() > 0)
                        {
                            sql = @"SELECT Top 1 Id,Province,City,Area,CorpID,CorpName,CommID,CommName,Tel,ModuleRights,IsMultiDoorControlServer
                                    FROM Tb_Community WHERE Id IN (SELECT CommunityId FROM Tb_User_Relation 
                                        WHERE isnull(Locked,0)=0 AND UserId=@UserId)";
                        }
                        else
                        {
                            sql = @"SELECT Top 1 Id,Province,City,Area,CorpID,CorpName,CommID,CommName,Tel,ModuleRights,
                                        convert(bit, 0) AS IsMultiDoorControlServer 
                                    FROM Tb_Community WHERE Id IN (SELECT CommunityId FROM Tb_User_Relation 
                                        WHERE isnull(Locked,0)=0 AND UserId=@UserId)";
                        }

                        dynamic commInfo = conn.Query(sql, new { UserId = result.Id }).FirstOrDefault();

                        // 判断是否存在已绑定房号
                        if (commInfo != null)
                        {
                            result.Community = new
                            {
                                Id = commInfo.Id,
                                Province = commInfo.Province,
                                City = commInfo.City,
                                Area = commInfo.Area,
                                CorpID = commInfo.CorpID,
                                CorpName = commInfo.CorpName,
                                CommID = commInfo.CommID,
                                CommName = commInfo.CommName,
                                Tel = commInfo.Tel,
                                ModuleRights = commInfo.ModuleRights,
                                IsMultiDoorControlServer = commInfo.IsMultiDoorControlServer
                            };
                        }

                        return new ApiResult(true, result).toJson();
                    }
                    else
                    {
                        return JSONHelper.FromString(false, "账号名或者密码错误");
                    }
                }
            }
            else
            {
                return JSONHelper.FromString(false, "账号名或者密码为空");
            }
        }


        /// <summary>
        /// //微信公共号登录  UserLogin_WeiXin
        /// </summary>
        /// <param name="row"></param>
        /// OpenId          必填
        /// <returns></returns>
        private string UserLogin_WeiXin(DataRow row)
        {

            if (!row.Table.Columns.Contains("OpenId"))
            {
                return JSONHelper.FromString(false, "OpenId不能为空");
            }
            string openId = row["OpenId"].ToString();
            string headImgUrl = "";
            if (row.Table.Columns.Contains("HeadImgUrl"))
            {
                headImgUrl = row["HeadImgUrl"].ToString();
            }

            //如果存在该用户.并且头像不为空,就修改用户头像
            if (!string.IsNullOrEmpty(headImgUrl)) {
                using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString)) {
                    conn.Execute("UPDATE Tb_User SET UserPic = @UserPic FROM Tb_User AS u INNER JOIN Tb_User_WeiXin wx ON u.Id = wx.UserId  WHERE wx.WeChatToken = @openId", new { UserPic = headImgUrl, openId = openId }, null, null, CommandType.Text);
                }
            }
            DataTable dt = null;
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                dt = conn.ExecuteReader("SELECT u.Id,u.Name,u.Mobile,u.Email,u.QQ,u.QQToken,w.WeChatToken,u.WeChatNum,u.NickName,u.UserPic,u.Sex FROM Tb_User as u left join Tb_User_WeiXin as w on u.id = w.UserId where w.WeChatToken = @WeChatToken", new { WeChatToken = openId }, null, null, CommandType.Text).ToDataSet().Tables[0];
            }
            if (null== dt || dt.Rows.Count == 0)
            {
                return JSONHelper.FromString(false, "账号名或者密码错误");
            }
            return JSONHelper.FromString(dt);
        }


        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string RegisterUser(DataRow Row)
        {
            // 鸿坤
            if (HttpContext.Current.Request.Url.Host.Contains("wuyth-test.hongkun.com.cn") || HttpContext.Current.Request.Url.Host.Contains("wyyth-app.hongkun.com.cn"))
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("Mobile", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Password", typeof(string)));
                
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = Row["Mobile"];
                dataRow[1] = Row["Pwd"];
                return new Register_th().RegisterUser(dataRow);
            }

            string backstr = "";
            try
            {

                if (Row.Table.Columns.Contains("Mobile") && !string.IsNullOrEmpty(Row["Mobile"].ToString()))
                {
                    DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT Name FROM Tb_User  where Mobile='" + Row["Mobile"].ToString() + "'").Tables[0];
                    if (dTable.Rows.Count > 0)
                    {
                        return JSONHelper.FromString(false, "此用户已存在");
                    }
                    else
                    {
                        string Pwd = "";
                        if (Row.Table.Columns.Contains("Pwd"))
                        {
                            Pwd = Row["Pwd"].ToString();
                        }
                        if (string.IsNullOrEmpty(Pwd))
                        {
                            return JSONHelper.FromString(false, "密码不能为空");
                        }
                        Random ro = new Random();
                        string Name = "";
                        string Email = "";
                        string QQ = "";
                        string QQToken = "";
                        string WeChatToken = "";
                        string NickName = Row["Mobile"].ToString();//游客

                        string UserPic = "";
                        int Sex = 1;
                        if (Row.Table.Columns.Contains("Name"))
                        {
                            Name = Row["Name"].ToString();
                        }
                        if (Row.Table.Columns.Contains("Email"))
                        {
                            Email = Row["Email"].ToString();
                        }
                        if (Row.Table.Columns.Contains("QQ"))
                        {
                            QQ = Row["QQ"].ToString();
                        }
                        if (Row.Table.Columns.Contains("QQToken"))
                        {
                            QQToken = Row["QQToken"].ToString();
                        }
                        if (Row.Table.Columns.Contains("WeChatToken"))
                        {
                            WeChatToken = Row["WeChatToken"].ToString();
                        }
                        if (Row.Table.Columns.Contains("NickName") && !string.IsNullOrEmpty(Row["NickName"].ToString()))
                        {
                            NickName = Row["NickName"].ToString();
                        }

                        if (Row.Table.Columns.Contains("UserPic"))
                        {
                            UserPic = Row["UserPic"].ToString();
                        }
                        if (Row.Table.Columns.Contains("Sex"))
                        {
                            Sex = AppGlobal.StrToInt(Row["Sex"].ToString());
                        }
                        MobileSoft.Model.Unified.Tb_User User = new MobileSoft.Model.Unified.Tb_User();
                        User.Id = Guid.NewGuid().ToString();
                        User.Name = Name;
                        User.Mobile = Row["Mobile"].ToString();
                        User.QQ = QQ;
                        User.QQToken = QQToken;
                        User.WeChatNum = WeChatToken;
                        User.WeChatToken = WeChatToken;
                        User.Pwd = Pwd;
                        User.NickName = NickName ?? User.Mobile;
                        User.Email = Email;
                        User.UserPic = UserPic;
                        User.Sex = Sex;
                        User.RegDate = DateTime.Now;
                        new MobileSoft.BLL.Unified.Bll_Tb_User().Add(User);

                    }
                }
                else
                {
                    return JSONHelper.FromString(false, "用户名不能为空!");
                }
            }
            catch (Exception ex)
            {

                backstr = ex.Message;
            }
            if (backstr == "")
            {
                return JSONHelper.FromString(true, "注册成功");
            }
            else
            {
                return JSONHelper.FromString(false, backstr);
            }

        }

        /// <summary>
        /// 注册用户微信共众号  RegisterUser_WeiXin
        /// </summary>
        /// <param name="Row"></param>
        /// OpenId              必填
        /// <returns></returns>
        private string RegisterUser_WeiXin(DataRow Row)
        {

            if (!Row.Table.Columns.Contains("OpenId") || string.IsNullOrEmpty(Row["OpenId"].ToString()))
            {
                return JSONHelper.FromString(false, "请使用微信客户端打开");
            }
            string OpenId = Row["OpenId"].ToString();
            //微信公众号注册,默认密码为123456
            //针对某些公众号系统不需要设置密码的问题(例如华南城)
            string Pwd;
            if (!Row.Table.Columns.Contains("Pwd") || string.IsNullOrEmpty(Row["Pwd"].ToString()))
            {
                Pwd = "123456";
            }
            else
            {
                Pwd = Row["Pwd"].ToString();
            }
            if (!Row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(Row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "手机号不能为空");
            }
            string Mobile = Row["Mobile"].ToString();

            //查询系统中是否存在该openid
            DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT * FROM Tb_User_WeiXin as w JOIN Tb_User as u on w.UserId = u.id  where w.WeChatToken = '" + Row["OpenId"].ToString() + "'").Tables[0];
            if (null != dTable && dTable.Rows.Count > 0)
            {
                //如果系统中已存在该openid,直接返回注册成功
                return JSONHelper.FromString(true, "注册成功");
            }
            //默认姓名为空
            string Name = "";
            //默认邮箱为空
            string Email = "";
            //默认QQ为空
            string QQ = "";
            //默认QQ登录为空
            string QQToken = "";
            //默认微信登录为空
            string WeChatToken = "";
            //默认昵称为手机号
            string NickName = Mobile;
            //默认用户头像为空
            string UserPic = "";
            //默认性别为女
            int Sex = 1;
            if (Row.Table.Columns.Contains("Name") && string.IsNullOrEmpty(Row["Name"].ToString()))
            {
                Name = Row["Name"].ToString();
            }
            if (Row.Table.Columns.Contains("Email") && string.IsNullOrEmpty(Row["Email"].ToString()))
            {
                Email = Row["Email"].ToString();
            }
            if (Row.Table.Columns.Contains("QQ") && string.IsNullOrEmpty(Row["QQ"].ToString()))
            {
                QQ = Row["QQ"].ToString();
            }
            if (Row.Table.Columns.Contains("QQToken") && string.IsNullOrEmpty(Row["QQToken"].ToString()))
            {
                QQToken = Row["QQToken"].ToString();
            }
            if (Row.Table.Columns.Contains("WeChatToken") && string.IsNullOrEmpty(Row["WeChatToken"].ToString()))
            {
                WeChatToken = Row["WeChatToken"].ToString();
            }
            if (Row.Table.Columns.Contains("NickName") && !string.IsNullOrEmpty(Row["NickName"].ToString()))
            {
                NickName = Row["NickName"].ToString();
            }

            if (Row.Table.Columns.Contains("UserPic") && !string.IsNullOrEmpty(Row["UserPic"].ToString()))
            {
                UserPic = Row["UserPic"].ToString();
            }
            if (Row.Table.Columns.Contains("Sex") && !string.IsNullOrEmpty(Row["Sex"].ToString()))
            {
                Sex = AppGlobal.StrToInt(Row["Sex"].ToString());
            }
            // 如果不存在该用户才新增,否则只增加绑定关系
            string UserId;
            DataTable dt = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT * FROM Tb_User WHERE Mobile = '" + Mobile + "'").Tables[0];
            if (null == dt || dt.Rows.Count == 0)
            {
                UserId = Guid.NewGuid().ToString();
                MobileSoft.Model.Unified.Tb_User User = new MobileSoft.Model.Unified.Tb_User();
                User.Id = UserId;
                User.Name = Name;
                User.Mobile = Mobile;
                User.QQ = QQ;
                User.QQToken = QQToken;
                User.WeChatNum = WeChatToken;
                User.WeChatToken = WeChatToken;
                User.Pwd = Pwd;
                User.NickName = NickName;
                User.Email = Email;
                User.UserPic = UserPic;
                User.Sex = Sex;
                User.RegDate = DateTime.Now;
                //新增用户
                new MobileSoft.BLL.Unified.Bll_Tb_User().Add(User);
            }
            else
            {
                UserId = dt.Rows[0]["Id"].ToString();
            }
            //新增openid
            int result = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).ExecuteSql("insert into Tb_User_WeiXin(UserId,WeChatToken) values('" + UserId + "','" + Row["OpenId"] + "')");
            
            if (result == 0)
            {
                return JSONHelper.FromString(false, "注册失败,请重试");
            }
            return JSONHelper.FromString(true, "注册成功");
        }

        /// <summary>
        /// 注册用户微信共众号  RegisterUser_WeiXin
        /// </summary>
        /// <param name="Row"></param>
        /// OpenId              必填
        /// <returns></returns>
        private string RegisterUser_WeiXin_hnc(DataRow Row)
        {

            if (!Row.Table.Columns.Contains("OpenId") || string.IsNullOrEmpty(Row["OpenId"].ToString()))
            {
                return JSONHelper.FromString(false, "请使用微信客户端打开");
            }
            string OpenId = Row["OpenId"].ToString();
            //微信公众号注册,默认密码为123456
            //针对某些公众号系统不需要设置密码的问题(例如华南城)
            string Pwd;
            if (!Row.Table.Columns.Contains("Pwd") || string.IsNullOrEmpty(Row["Pwd"].ToString()))
            {
                Pwd = "123456";
            }
            else
            {
                Pwd = Row["Pwd"].ToString();
            }
            if (!Row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(Row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "手机号不能为空");
            }
            string Mobile = Row["Mobile"].ToString();
            //查询系统中是否存在该openid
            DataTable dTable = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT * FROM Tb_User_WeiXin as w JOIN Tb_User as u on w.UserId = u.id  where w.WeChatToken = '" + Row["OpenId"].ToString() + "'").Tables[0];
            if (null != dTable && dTable.Rows.Count > 0)
            {
                //如果系统中已存在该openid,直接返回注册成功
                return JSONHelper.FromString(true, "注册成功");
            }
            //默认姓名为空
            string Name = "";
            //默认邮箱为空
            string Email = "";
            //默认QQ为空
            string QQ = "";
            //默认QQ登录为空
            string QQToken = "";
            //默认微信登录为空
            string WeChatToken = "";
            //默认昵称为手机号
            string NickName = Mobile;
            //默认用户头像为空
            string UserPic = "";
            //默认性别为女
            int Sex = 1;
            if (Row.Table.Columns.Contains("Name") && string.IsNullOrEmpty(Row["Name"].ToString()))
            {
                Name = Row["Name"].ToString();
            }
            if (Row.Table.Columns.Contains("Email") && string.IsNullOrEmpty(Row["Email"].ToString()))
            {
                Email = Row["Email"].ToString();
            }
            if (Row.Table.Columns.Contains("QQ") && string.IsNullOrEmpty(Row["QQ"].ToString()))
            {
                QQ = Row["QQ"].ToString();
            }
            if (Row.Table.Columns.Contains("QQToken") && string.IsNullOrEmpty(Row["QQToken"].ToString()))
            {
                QQToken = Row["QQToken"].ToString();
            }
            if (Row.Table.Columns.Contains("WeChatToken") && string.IsNullOrEmpty(Row["WeChatToken"].ToString()))
            {
                WeChatToken = Row["WeChatToken"].ToString();
            }
            if (Row.Table.Columns.Contains("NickName") && !string.IsNullOrEmpty(Row["NickName"].ToString()))
            {
                NickName = Row["NickName"].ToString();
            }

            if (Row.Table.Columns.Contains("UserPic") && !string.IsNullOrEmpty(Row["UserPic"].ToString()))
            {
                UserPic = Row["UserPic"].ToString();
            }
            if (Row.Table.Columns.Contains("Sex") && !string.IsNullOrEmpty(Row["Sex"].ToString()))
            {
                Sex = AppGlobal.StrToInt(Row["Sex"].ToString());
            }
            // 如果不存在该用户才新增,否则只增加绑定关系
            string UserId;
            DataTable dt = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).Query("SELECT * FROM Tb_User WHERE Mobile = '" + Mobile + "'").Tables[0];
            if (null == dt || dt.Rows.Count == 0)
            {
                UserId = Guid.NewGuid().ToString();
                MobileSoft.Model.Unified.Tb_User User = new MobileSoft.Model.Unified.Tb_User();
                User.Id = UserId;
                User.Name = Name;
                User.Mobile = Mobile;
                User.QQ = QQ;
                User.QQToken = QQToken;
                User.WeChatNum = WeChatToken;
                User.WeChatToken = WeChatToken;
                User.Pwd = Pwd;
                User.NickName = NickName;
                User.Email = Email;
                User.UserPic = UserPic;
                User.Sex = Sex;
                User.RegDate = DateTime.Now;
                //新增用户
                new MobileSoft.BLL.Unified.Bll_Tb_User().Add(User);
            }
            else
            {
                UserId = dt.Rows[0]["Id"].ToString();
            }
            //新增openid
            int result = new DbHelperSQLP(PubConstant.UnifiedContionString.ToString()).ExecuteSql("insert into Tb_User_WeiXin(UserId,WeChatToken) values('" + UserId + "','" + Row["OpenId"] + "')");

            if (result == 0)
            {
                return JSONHelper.FromString(false, "注册失败,请重试");
            }
            try
            {
                string erpString = AppGlobal.GetConnectionString("HNC_HM_ConnectionString");
                // 查询业主
                using (IDbConnection conn = new SqlConnection(erpString))
                {
                    IEnumerable<dynamic> resultSet = conn.Query(string.Format(@"SELECT a.CustID,a.CustName,a.CommID,b.RoomID,c.RoomSign FROM Tb_HSPR_Customer as a JOIN Tb_HSPR_CustomerLive AS b ON a.CustID = b.CustID JOIN Tb_HSPR_Room AS c ON b.RoomID = c.RoomID
                        WHERE (b.LiveType=1 OR b.LiveType=2) AND isnull(IsDelLive,0)= 0 AND a.MobilePhone LIKE '%{0}%'", Mobile));
                    
                    // 业主存在，自动绑定房屋
                    if (resultSet.Count() > 0)
                    {
                        foreach (dynamic item in resultSet)
                        {
                            Tb_Community tb_Community = getTbCommunity(item.CommID + "");
                            if (null == tb_Community)
                            {
                                continue;
                            }
                            using (IDbConnection conn2 = new SqlConnection(PubConstant.UnifiedContionString))
                            {
                                conn2.Execute(@"INSERT INTO Tb_User_Relation(Id, UserId, CommunityId, CustId, RoomId, RegDate, CustName, RoomSign, CustMobile)
                                            VALUES(newid(), @UserId, @CommunityId, @CustID, @RoomID, getdate(), @CustName, @RoomSign, @Mobile)",
                                                new { UserId = UserId, CommunityId = tb_Community.Id, CustID = item.CustID, RoomID = item.RoomID, CustName = item.CustName, RoomSign = item.RoomSign, Mobile = Mobile });

                            }
                        }
                        return JSONHelper.FromString(true, "注册成功，已自动绑定房屋");
                    }
                    return JSONHelper.FromString(true, "注册成功");
                }
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(true, "注册成功,但自动绑定失败(" + ex.Message + ")");
            }
            
        }
        /// <summary>
        /// 获取小区配置
        /// </summary>
        /// <param name="CommunityId"></param>
        /// <returns></returns>
        private Tb_Community getTbCommunity(string CommID)
        {
            if (string.IsNullOrEmpty(CommID))
            {
                return null;
            }
            using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
            {
                return conn.QueryFirstOrDefault<Tb_Community>("SELECT * FROM Tb_Community WHERE CommID = @CommID", new { CommID = CommID }, null, null, CommandType.Text);
            }
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetVerifyCode(DataRow Row, Common.Transfer Trans)
        {
            if (Row.Table.Columns.Contains("Mobile") && !String.IsNullOrEmpty(Row["Mobile"].ToString()))
            {
                if (Row.Table.Columns.Contains("VerifyCode") && !String.IsNullOrEmpty(Row["VerifyCode"].ToString()))
                {
                    // 鸿坤 单独写接口
                    //// 查询该手机号是否已经注册
                    //using (var conn = new SqlConnection(PubConstant.UnifiedContionString))
                    //{
                    //    if (conn.Query(@"SELECT * FROM Tb_User WHERE Mobile=@Mobile", new { Mobile = Row["Mobile"].ToString() }).Count() > 0)
                    //    {
                    //        return JSONHelper.FromString(false, @"该手机号已注册");
                    //    }
                    //}


                    //解密验证码
                    int code = AppGlobal.StrToInt(RSAHelper.getInstance().Decrypt(Row["VerifyCode"].ToString()));
                    //新增4位验证码
                    if (code > 0 && (code.ToString().Length == 6 || code.ToString().Length == 4))//六位字验证
                    {
                        //MAC验证
                        DataSet ds = new Bll_Tb_SendMessageRecord().GetList(" MacCode='" + Trans.Mac + "'  ");
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            return JSONHelper.FromString(false, "无效操作");
                        }
                        Tb_Sms_Account smsModel = SmsInfo.GetSms_Account();
                        string Content = "您的验证码为：" + code + "" + smsModel.Sign;
                        Tb_SendMessageRecord m = new Tb_SendMessageRecord();

                        try
                        {
                            //记录短信
                            m = new Bll_Tb_SendMessageRecord().Add(Row["Mobile"].ToString(), Content, Trans.Mac, "天问验证码", "");
                        }
                        catch (Exception ex)
                        {
                            return JSONHelper.FromString(false, "无效操作");
                        }

                        //发送短信
                        //int Result = Common.Sms.Send(smsModel.SmsAccount, smsModel.SmsPwd, Row["Mobile"].ToString(), Content, "", "");
                        int Result = Common.Sms.Send_v2(smsModel.SmsUserId, smsModel.SmsAccount, smsModel.SmsPwd, Row["Mobile"].ToString(), Content, out string strErrMsg);
                        string Resul = "";
                        switch (Result)
                        {
                            case 0:
                                Resul = "发送成功";
                                break;
                            case -4:
                                Resul = "手机号码格式不正确";
                                break;
                            default:
                                Resul = "发送失败：" + Result;
                                break;
                        }
                        //修改状态
                        m.SendState = Result.ToString();
                        //重写短信记录状态
                        new Bll_Tb_SendMessageRecord().Update(m);
                        if (Result == 0)
                        {
                            return JSONHelper.FromString(true, Resul);
                        }
                        else
                        {
                            return JSONHelper.FromString(false, strErrMsg);
                        }

                    }
                    else
                    {
                        //new Bll_Tb_SendMessageRecord().Add(Row["Mobile"].ToString(), "", "", "天问验证码", "");

                        return JSONHelper.FromString(false, "参数VerifyCode格式错误");
                    }
                }
                else
                {
                    //new Bll_Tb_SendMessageRecord().Add(Row["Mobile"].ToString(), "", "", "天问验证码", "");
                    return JSONHelper.FromString(false, "缺少参数VerifyCode");
                }
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数Mobile");
            }
        }

        /// <summary>
        /// 获取验证码忘记密码
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetVerifyCodeForPwd(DataRow Row, Common.Transfer Trans)
        {
            if (Row.Table.Columns.Contains("Mobile") && !String.IsNullOrEmpty(Row["Mobile"].ToString()))
            {
                if (Row.Table.Columns.Contains("VerifyCode") && !String.IsNullOrEmpty(Row["VerifyCode"].ToString()))
                {
                    //解密验证码
                    int code = AppGlobal.StrToInt(RSAHelper.getInstance().Decrypt(Row["VerifyCode"].ToString()));
                    if (code > 0 && code.ToString().Length == 6)//六位字验证
                    {
                        //MAC验证
                        DataSet ds = new Bll_Tb_SendMessageRecord().GetList(" MacCode='" + Trans.Mac + "'  ");
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            return JSONHelper.FromString(false, "无效操作");
                        }
                        Tb_Sms_Account smsModel = SmsInfo.GetSms_Account();
                        string Content = "您的验证码为：" + code + "" + smsModel.Sign;
                        Tb_SendMessageRecord m = new Tb_SendMessageRecord();
                        try
                        {
                            //记录短信
                            m = new Bll_Tb_SendMessageRecord().Add(Row["Mobile"].ToString(), Content, Trans.Mac, "忘记密码", "");
                        }
                        catch (Exception ex)
                        {
                            return JSONHelper.FromString(false, "无效操作");
                        }

                        //发送短信
                        //int Result = Common.Sms.Send(smsModel.SmsAccount, smsModel.SmsPwd, Row["Mobile"].ToString(), Content, "", "");
                        int Result = Common.Sms.Send_v2(smsModel.SmsUserId, smsModel.SmsAccount, smsModel.SmsPwd, Row["Mobile"].ToString(), Content, out string strErrMsg);
                        string Resul = "";
                        switch (Result)
                        {
                            case 0:
                                Resul = "发送成功";
                                break;
                            case -4:
                                Resul = "手机号码格式不正确";
                                break;
                            default:
                                Resul = "发送失败：" + Result;
                                break;
                        }
                        //修改状态
                        m.SendState = Result.ToString();
                        //重写短信记录状态
                        new Bll_Tb_SendMessageRecord().Update(m);
                        if (Result == 0)
                        {
                            return JSONHelper.FromString(true, Resul);
                        }
                        else
                        {
                            return JSONHelper.FromString(false, strErrMsg);
                        }

                    }
                    else
                    {
                        //new Bll_Tb_SendMessageRecord().Add(Row["Mobile"].ToString(), "", "", "天问验证码", "");

                        return JSONHelper.FromString(false, "参数VerifyCode格式错误");
                    }
                }
                else
                {
                    //new Bll_Tb_SendMessageRecord().Add(Row["Mobile"].ToString(), "", "", "天问验证码", "");
                    return JSONHelper.FromString(false, "缺少参数VerifyCode");
                }
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数Mobile");
            }
        }


        /// <summary>
        /// 验证验证码 10分钟有效
        /// </summary>
        /// <param name="Row"></param>
        /// Mobile  手机号【必填】
        /// VerifyCode  验证码【必填】
        /// 返回：
        ///     true:验证成功
        ///     false:错误信息
        /// <returns></returns>
        private string CheckVerifyCode(DataRow Row)
        {
            if (Row["Mobile"] == null || Row["Mobile"].ToString() == "")
            {
                return JSONHelper.FromString(false, "缺少参数Mobile");
            }
            if (Row["VerifyCode"] == null || Row["VerifyCode"].ToString() == "")
            {
                return JSONHelper.FromString(false, "缺少参数VerifyCode");
            }

            string conStr = PubConstant.UnifiedContionString.ToString();
            IDbConnection con = new SqlConnection(conStr);
            string sql = "select * from Tb_SendMessageRecord where Mobile=@Mobile and SendContent='您的验证码为：'+@SendContent+'【天问互联】' and SendType='忘记密码' and SendState=0 and DATEDIFF(mi,SendTime,GETDATE())<10";
            Tb_SendMessageRecord model = con.Query<Tb_SendMessageRecord>(sql, new { Mobile = Row["Mobile"].ToString(), SendContent = Row["VerifyCode"] }).ToList<Tb_SendMessageRecord>().SingleOrDefault();
            if (model == null)
            {
                return JSONHelper.FromString(false, "验证失败");
            }
            else
            {
                return JSONHelper.FromString(true, "验证成功");
            }

        }


        /// <summary>
        /// 修改密码 
        /// </summary>
        /// <param name="row"></param>
        /// Mobile  手机号【必填】
        /// Pwd  密码【必填】
        /// 返回：
        ///     true:密码修改成功
        ///     false:错误信息
        /// <returns></returns>
        private string UpdateUserPwd(DataRow row)
        {
            if (!row.Table.Columns.Contains("Mobile") || string.IsNullOrEmpty(row["Mobile"].ToString()))
            {
                return JSONHelper.FromString(false, "手机号不能为空");
            }
            if (!row.Table.Columns.Contains("Pwd") || string.IsNullOrEmpty(row["Pwd"].ToString()))
            {
                return JSONHelper.FromString(false, "密码不能为空");
            }

            IDbConnection Connectionstr = new SqlConnection(Connection.GetConnection("4"));
            string Sql = "SELECT  * FROM Tb_User where Mobile=@Mobile";
            Tb_User user = Connectionstr.Query<Tb_User>(Sql, new { Mobile = row["Mobile"].ToString() }).SingleOrDefault();
            if (user == null)
            {
                return JSONHelper.FromString(false, "该用户不存在");
            }
            else
            {
                user.Pwd = row["Pwd"].ToString();
                Connectionstr.Update(user);
                return JSONHelper.FromString(true, "密码修改成功");
            }
        }

    }
}