using Dapper;
using DapperExtensions;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using TWTools.Push;

namespace Business
{
    public class OnlineMessage : PubInfo
    {
        private readonly string hrefString1 = "href=\"/ueditor/net/upload/";
        private readonly string hrefString2 = "href=\"/hm/m_main/oauploadfile/";
        private readonly string hrefString3 = "src=\"/ueditor/net/upload/";
        private readonly string hrefString4 = "href=\"/hm/m_main/jscript-ui/ueditor/net/upload/";

        public OnlineMessage()
        {
            base.Token = "20170728OnlineMessage";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            switch (Trans.Command)
            {
                case "GetUnreadMessageCount":
                    Trans.Result = GetUnreadMessageCount(Row);//获取用户未读消息数量
                    break;
                case "GetMessageList":
                    Trans.Result = GetMessageList(Row);//获取用户未读消息列表
                    break;
                case "MarkMessageRead":
                    Trans.Result = MarkMessageRead(Row);//标记短信为已读
                    break;
                case "GetMessageDetail":
                    Trans.Result = GetMessageDetail(Row);//读取短信详情
                    break;
                case "GetDepartment":
                    Trans.Result = GetDepartment(Row);//获取部门
                    break;
                case "GetUserListWithDep":
                    Trans.Result = GetUserListWithDep(Row);//获取部门下员工列表
                    break;
                case "Insert":
                    Trans.Result = Insert(Row);//插入新消息
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取一周内未读消息数量
        /// </summary>
        private string GetUnreadMessageCount(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            string corpId = row["CorpID"].ToString();
            string userCode = row["UserCode"].ToString();

            return JSONHelper.FromString(true, GetUnreadMessageCount(corpId, userCode).ToString());
        }

        public int GetUnreadMessageCount(string corpId, string userCode)
        {
            string lastWeekTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

            string sql = "SELECT COUNT(*) AS UnreadCount FROM Tb_Sys_Message WHERE SendTime>='" + lastWeekTime + "' AND MsgState=1 AND UserCode='" + userCode + "'";

            using (IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString))
            {
                return con.Query<int>(sql).ToList()[0];
            }
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        private string GetMessageList(DataRow row)
        {
            if (!row.Table.Columns.Contains("UserCode") || string.IsNullOrEmpty(row["UserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "用户编号不能为空");
            }

            if (!row.Table.Columns.Contains("State") || string.IsNullOrEmpty(row["State"].ToString()))
            {
                return JSONHelper.FromString(false, "状态不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            int PageCount;
            int Counts;
            int PageIndex = 1;
            int PageSize = 20;
            if (row.Table.Columns.Contains("PageIndex"))
            {
                PageIndex = AppGlobal.StrToInt(row["PageIndex"].ToString());
            }

            if (row.Table.Columns.Contains("PageSize"))
            {
                PageSize = AppGlobal.StrToInt(row["PageSize"].ToString());
            }

            string state = row["State"].ToString();
            string userCode = row["UserCode"].ToString();
            string lastWeekTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            string sql = "";

            if (state == "1") // 7天内未读
            {
                sql = "SELECT MessageCode,CutID,UserCode,UserName,DepName,MsgTitle,SendTime,SendMan,SendManName,SendManDepName FROM view_Sys_Message_Filter WHERE SendTime>='" + lastWeekTime + "' AND MsgState=" + state + " AND UserCode='" + userCode + "'";
            }
            else if (state == "3") // 自己发送的
            {
                sql = "SELECT MessageCode,CutID,UserCode,UserName,DepName,MsgTitle,SendTime,SendMan,SendManName,SendManDepName FROM view_Sys_Message_Filter WHERE  SendMan='" + userCode + "'";
            }
            else // 已读
            {
                sql = "SELECT MessageCode,CutID,UserCode,UserName,DepName,MsgTitle,SendTime,SendMan,SendManName,SendManDepName FROM view_Sys_Message_Filter WHERE MsgState=" + state + " AND UserCode='" + userCode + "'";
            }

            DataSet ds = GetList(out PageCount, out Counts, sql, PageIndex, PageSize, "SendTime", 1, "MessageCode", strcon);
            
            if (ds != null && ds.Tables.Count > 0)
            {
                var json = JSONHelper.FromString(ds.Tables[0]);
                var regex = new Regex("<a\\shref=[^>]+>([\\s\\S]+?)</a>");
                json = regex.Replace(json, "$1");
                return json;
            }
            else
            {
                return JSONHelper.FromString(new DataTable());
            }
        }

        /// <summary>
        /// 标记短信为已读
        /// </summary>
        private string MarkMessageRead(DataRow row)
        {
            if (!row.Table.Columns.Contains("MessageCode") || string.IsNullOrEmpty(row["MessageCode"].ToString()))
            {
                return JSONHelper.FromString(false, "短信ID不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            string sql = "UPDATE Tb_Sys_Message SET MsgState=2 WHERE MessageCode='" + row["MessageCode"].ToString() + "'";

            using (IDbConnection con = new SqlConnection(strcon))
            {
                return JSONHelper.FromString(true, con.Execute(sql).ToString());
            }
        }

        /// <summary>
        /// 读取短信详情
        /// </summary>
        private string GetMessageDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("MessageCode") || string.IsNullOrEmpty(row["MessageCode"].ToString()))
            {
                return JSONHelper.FromString(false, "短信ID不能为空");
            }

            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            string sql = "SELECT Content FROM Tb_Sys_Message WHERE MessageCode='" + row["MessageCode"].ToString() + "'";
            string contentHtml = "";

            using (IDbConnection con = new SqlConnection(strcon))
            {
                dynamic messageContent = con.Query(sql).ToList().FirstOrDefault();

                if (messageContent != null && !string.IsNullOrEmpty(messageContent.Content.ToString()))
                {
                    contentHtml = messageContent.Content.ToString();
                    contentHtml = contentHtml.ToLower();

                    if (contentHtml.Contains(hrefString1))
                    {
                        contentHtml = contentHtml.Replace(hrefString1, " href=\"#\" onclick=\"openFile(this);return false;\" x=\"/HM/M_Main/Jscript-Ui/UEditor/net/upload/");
                    }

                    if (contentHtml.Contains(hrefString2))
                    {
                        contentHtml = contentHtml.Replace(hrefString2, " href=\"#\" onclick=\"openFile(this);return false;\" x=\"/HM/M_Main/oauploadfile/");
                    }

                    if (contentHtml.Contains(hrefString3))
                    {
                        contentHtml = contentHtml.Replace(hrefString3, "src=\"/HM/M_Main/Jscript-Ui/UEditor/net/upload/");
                    }

                    if (contentHtml.Contains(hrefString4))
                    {
                        contentHtml = contentHtml.Replace(hrefString4, " href=\"#\" onclick=\"openFile(this);return false;\" x=\"/hm/m_main/jscript-ui/ueditor/net/upload/");
                    }
                }
                contentHtml = contentHtml.Replace("\"", "\\\"");

                return JSONHelper.FromString(true, contentHtml);
            }
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        private string GetDepartment(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            string sql = "SELECT DepCode,SortDepCode,DepName FROM Tb_Sys_Department WHERE isnull(IsDelete,0)=0 ORDER BY Sort";

            using (IDbConnection con = new SqlConnection(strcon))
            {
                return JSONHelper.FromString(con.ExecuteReader(sql).ToDataSet().Tables[0]);
            }
        }

        /// <summary>
        /// 获取部门下用户列表
        /// </summary>
        private string GetUserListWithDep(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            if (!row.Table.Columns.Contains("DepCode") || string.IsNullOrEmpty(row["DepCode"].ToString()))
            {
                return JSONHelper.FromString(false, "部门不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            string sql = "SELECT DepCode,UserCode,UserName FROM Tb_Sys_User WHERE DepCode='" + row["DepCode"] + "' AND isnull(IsDelete,0)=0 ORDER BY UserCode";

            using (IDbConnection con = new SqlConnection(strcon))
            {
                return JSONHelper.FromString(con.ExecuteReader(sql).ToDataSet().Tables[0]);
            }
        }

        /// <summary>
        /// 插入新消息
        /// </summary>
        private string Insert(DataRow row)
        {
            if (!row.Table.Columns.Contains("CorpID") || string.IsNullOrEmpty(row["CorpID"].ToString()))
            {
                return JSONHelper.FromString(false, "公司ID不能为空");
            }

            if (!row.Table.Columns.Contains("MessageCode") || string.IsNullOrEmpty(row["MessageCode"].ToString()))
            {
                return JSONHelper.FromString(false, "短信ID不能为空");
            }

            if (!row.Table.Columns.Contains("SendMan") || string.IsNullOrEmpty(row["SendMan"].ToString()))
            {
                return JSONHelper.FromString(false, "发件人不能为空");
            }

            if (!row.Table.Columns.Contains("SendManDep") || string.IsNullOrEmpty(row["SendManDep"].ToString()))
            {
                return JSONHelper.FromString(false, "发件人部门不能为空");
            }

            if (!row.Table.Columns.Contains("RecieveUserCode") || string.IsNullOrEmpty(row["RecieveUserCode"].ToString()))
            {
                return JSONHelper.FromString(false, "收件人不能为空");
            }

            string corpId = row["CorpID"].ToString();

            //构建链接字符串
            string strcon = "";
            bool bl = GetDBServerPathWithCorpID(corpId, out strcon);
            if (bl == false)
            {
                return JSONHelper.FromString(false, strcon);
            }

            string messageCode = row["MessageCode"].ToString();
            string sendMan = row["SendMan"].ToString();
            string recieveUserCode = row["RecieveUserCode"].ToString();
            string addedContent = "";

            if (row.Table.Columns.Contains("Content") && row["Content"] != null && !string.IsNullOrEmpty(row["Content"].ToString()))
            {
                addedContent = row["Content"].ToString();
            }

            using (IDbConnection conn = new SqlConnection(strcon))
            {
                IEnumerable<dynamic> result = conn.Query("SELECT * FROM Tb_Sys_Message WHERE MessageCode='" + messageCode + "'");

                if (result.Count() == 0)
                {
                    return JSONHelper.FromString(false, "短信已被删除，无法操作");
                }
                else
                {
                    dynamic msg = result.First();

                    string msgTitlePrefix = "回复";

                    // 判断是转发还是回复
                    if ((msg.UserCode as string) != recieveUserCode) // 转发
                    {
                        msgTitlePrefix = "转发";
                    }

                    string msgTitle = msgTitlePrefix + msg.MsgTitle as string;
                    string msgContent = msg.Content as string;

                    // 消息内容、回复内容其中一个不为空
                    if (string.IsNullOrEmpty(addedContent) == false)
                    {
                        if (string.IsNullOrEmpty(msgContent) == false)
                        {
                            msgContent = msgContent + @"<br/><br/>" + addedContent;
                        }
                        else
                        {
                            msgContent = addedContent;
                        }
                    }

                    string sql = string.Format(@"INSERT INTO Tb_Sys_Message(MessageCode,UserCode,MsgTitle,Content,SendTime,MsgType,SendMan,MsgState,IsRemind)
                            VALUES(newid(),'{0}','[{1}]{2}','{3}',getdate(),1,'{4}',1,0)", recieveUserCode, msgTitlePrefix, msgTitle, msgContent, sendMan);
                    conn.Execute(sql);

                    if (Common.Push.GetAppKeyAndAppSecret(PubConstant.tw2bsConnectionString, corpId,
                        out string appIdentifier, out string appKey, out string appSecret))
                    {
                        // 推送
                        PushModel pushModel = new PushModel(appKey, appSecret)
                        {
                            AppIdentifier = appIdentifier,
                            Badge = 1,
                            Command = PushCommand.ERP_SMS,
                            CommandName = PushCommand.CommandNameDict[PushCommand.ERP_SMS],
                            Message = string.Format("{0}给发送了一条在线短信，请注意查收", sendMan)
                        };
                        pushModel.Audience.Category = PushAudienceCategory.Tags;
                        pushModel.Audience.Objects.Add(corpId);
                        pushModel.Audience.Objects.Add(recieveUserCode);

                        Push.SendAsync(pushModel);
                    }

                    return JSONHelper.FromString(true, msgTitlePrefix + "成功");
                }
            }
        }

    }
}
