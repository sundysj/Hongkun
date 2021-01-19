using System;
using MobileSoft.DBUtility;
using MobileSoft.Common;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace Business
{
    public class CommunityNotificationTW : PubInfo
    {
        private static string imageAddr = System.Configuration.ConfigurationManager.AppSettings["ImageFileAddr"].ToString();
        public CommunityNotificationTW()
        {
            base.Token = "20160913CommunityNotificationTW";
        }
        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误!");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            switch (Trans.Command)
            {
                case "CommunityNotificationTWList"://社区公告
                    Trans.Result = CommunityNotificationTWList(Row);
                    break;
                case "CommunityCultureList"://社区文化
                    Trans.Result = CommunityCultureList(Row);
                    break;
                case "CommunityActivitiesList"://社区活动
                    Trans.Result = CommunityActivitiesList(Row);
                    break;
                case "ServiceInformationList"://服务指南
                    Trans.Result = ServiceInformationList(Row);
                    break;
                case "GetAll":
                    Trans.Result = GetAll(Row);
                    break;
                default:
                    break;
            }
        }
        #region 社区公告
        /// <summary>
        /// 亲情提示、社区资讯
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string CommunityNotificationTWList(DataRow Row)
        {
            string result = "";

            try
            {
                #region 接受参数
                string strUserId = "";
                string strCommunityId = "";//项目ID
                string strCurrPage = "1";//第几页
                string strPageSize = "10";//分页的大小

                if (Row.Table.Columns.Contains("CommunityId"))
                {
                    strCommunityId = AppGlobal.ChkStr(Row["CommunityId"].ToString());
                }
                else
                {
                    return JSONHelper.FromString(false, "缺少参数CommID");
                }
                if (Row.Table.Columns.Contains("UserID") && !string.IsNullOrEmpty(Row["UserID"].ToString()))
                {
                    strUserId = Row["UserID"].ToString();
                }
                if (Row.Table.Columns.Contains("CurrPage"))
                {
                    strCurrPage = AppGlobal.ChkNum(Row["CurrPage"].ToString());
                }

                if (Row.Table.Columns.Contains("PageSize"))
                {
                    strPageSize = AppGlobal.ChkNum(Row["PageSize"].ToString());
                }


                #endregion

                #region 变量定义
                string SQLContionString = "";

                int PageCount = 0;
                int Counts = 0;
                StringBuilder sListContent = new StringBuilder("");

                int iCurrPage = AppGlobal.StrToInt(strCurrPage);
                int iPageSize = AppGlobal.StrToInt(strPageSize);


                #endregion
                SQLContionString = ConnectionDb.GetConnection(Row["CommunityId"].ToString());
                MobileSoft.Model.Unified.Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(Row["CommunityId"].ToString());

                if (Community == null)
                {
                    return JSONHelper.FromString(false, "该小区不存在");
                }

                #region 查询亲情提示、社区咨询

                string strSQLNotiHis;
             

                switch (Community.CorpID)
                {
                    case 1975:   // 华南城社区新闻设置了多选小区发放功能
                        strSQLNotiHis = " and isnull(IsDelete,0)=0 and isnull(IsAudit, 0)=0 AND (CommID=0 OR CommID = " + Community.CommID + " OR CHARINDEX('" + Community.CommID + "',CommIdNvarchar)>0) and (InfoType = 'qqts' or InfoType = 'dtzx') AND (ShowEndDate is null or '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'< ShowEndDate) ";

                        break;
                    //case 1973:
                    //    strSQLNotiHis = " and isnull(IsDelete,0)=0 and isnull(IsAudit, 0)=0 AND (CommID=0 OR CommID = " + Community.CommID + ") and InfoType = 'dtzx' AND (ShowEndDate is null or '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'< ShowEndDate) ";

                    //    break;
                    default:
                        strSQLNotiHis = " and isnull(IsDelete,0)=0 and isnull(IsAudit, 0)=0 AND (CommID=0 OR CommID = " + Community.CommID + ") and (InfoType = 'qqts' or InfoType = 'dtzx') AND (ShowEndDate is null or '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'< ShowEndDate) ";

                        break;
                }
        

                DataTable dTableNotiHis = null;
                dTableNotiHis = (new Business.TWBusinRule(SQLContionString)).HSPR_CommunityInfo_CutPage(out PageCount, out Counts, strSQLNotiHis, iCurrPage, iPageSize);

                if (!pageHasData(iCurrPage, PageCount, Counts))
                {
                    dTableNotiHis.Dispose();
                    dTableNotiHis = new DataTable();
                }

                if (dTableNotiHis.Rows.Count > 0)
                {
                    using (IDbConnection conn = new SqlConnection(PubConstant.UnifiedContionString))
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("InfoID", typeof(string)));
                        dt.Columns.Add(new DataColumn("Heading", typeof(string)));
                        dt.Columns.Add(new DataColumn("IssueDate", typeof(string)));
                        dt.Columns.Add(new DataColumn("ImageUrl", typeof(string)));
                        dt.Columns.Add(new DataColumn("IsRead", typeof(int)));

                        string sql = @"SELECT count(0) FROM Tb_BBS_ReadRecord WHERE InfoID=@InfoID AND UserID=@UserID";

                        foreach (DataRow DRow in dTableNotiHis.Rows)
                        {
                            DataRow dr = dt.NewRow();
                            dr["InfoID"] = DRow["InfoID"].ToString();
                            dr["Heading"] = DRow["Heading"].ToString();
                            dr["IssueDate"] = DRow["IssueDate"].ToString();
                            dr["ImageUrl"] = string.IsNullOrEmpty(DRow["ImageUrl"].AsString()) ? "" : DRow["ImageUrl"].AsString().IndexOf("http") >= 0 ? DRow
                                ["ImageUrl"].AsString() : imageAddr + DRow["ImageUrl"].AsString();

                            if (!string.IsNullOrEmpty(strUserId))
                            {
                                dr["IsRead"] = conn.Query<int>(sql, new { InfoID = DRow["InfoID"].ToString(), UserID = strUserId }).FirstOrDefault();
                            }
                            else
                            {
                                dr["IsRead"] = 0;
                            }

                            dt.Rows.Add(dr);
                        }
                        result += JSONHelper.FromString(dt);
                    }
                }
                else
                {
                    result += JSONHelper.FromString(dTableNotiHis);
                }
                #endregion
                dTableNotiHis.Dispose();
            }
            catch (Exception ex)
            {
                result = ex.Message + "\r\n" + ex.StackTrace;
            }
            return result;
        }
        #endregion

        #region 社区文化
        /// <summary>
        /// 社区文化
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string CommunityCultureList(DataRow Row)
        {
            string result = "";

            #region 接受参数
            string strCommunityId = "";//项目ID
            string strCurrPage = "1";//第几页
            string strPageSize = "10";//分页的大小

            if (Row.Table.Columns.Contains("CommunityId"))
            {
                strCommunityId = AppGlobal.ChkStr(Row["CommunityId"].ToString());
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数CommID");
            }
            if (Row.Table.Columns.Contains("CurrPage"))
            {
                strCurrPage = AppGlobal.ChkNum(Row["CurrPage"].ToString());
            }

            if (Row.Table.Columns.Contains("PageSize"))
            {
                strPageSize = AppGlobal.ChkNum(Row["PageSize"].ToString());
            }


            #endregion

            #region 变量定义
            string strErrMsg = "";
            string strCommID = "";

            string SQLContionString = "";

            int PageCount = 0;
            int Counts = 0;
            StringBuilder sListContent = new StringBuilder("");

            int iCurrPage = AppGlobal.StrToInt(strCurrPage);
            int iPageSize = AppGlobal.StrToInt(strPageSize);


            #endregion
            SQLContionString = ConnectionDb.GetConnection(Row["CommunityId"].ToString());

            MobileSoft.Model.Unified.Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(Row["CommunityId"].ToString());

            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }


            #region 查询社区文化
            string strSQLNotiHis = " and isnull(IsDelete,0)=0 and isnull(IsAudit, 0)=0 AND (CommID=0 OR CommID = " + Community.CommID + ")  AND InfoType = 'sqwh' AND (ShowEndDate is null or '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'< ShowEndDate) ";

            //string strSQLNotiHis = " and isnull(IsDelete,0)=0 and isnull(IsAudit, 0)=0 AND CommID = " + Community.CommID + " and InfoType = 'sqwh' AND (ShowEndDate is null or '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'< ShowEndDate) ";

            DataTable dTableNotiHis = null;
            dTableNotiHis = (new Business.TWBusinRule(SQLContionString)).HSPR_CommunityInfo_CutPage(out PageCount, out Counts, strSQLNotiHis, iCurrPage, iPageSize);
            if (!pageHasData(iCurrPage, PageCount, Counts))
            {
                dTableNotiHis.Dispose();
                dTableNotiHis = new DataTable();
            }

            if (dTableNotiHis.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("InfoID", typeof(string)));
                dt.Columns.Add(new DataColumn("Heading", typeof(string)));
                dt.Columns.Add(new DataColumn("IssueDate", typeof(string)));
                dt.Columns.Add(new DataColumn("ImageUrl", typeof(string)));

                foreach (DataRow DRow in dTableNotiHis.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["InfoID"] = DRow["InfoID"].ToString();
                    dr["Heading"] = DRow["Heading"].ToString();
                    dr["IssueDate"] = DRow["IssueDate"].ToString();
                    dr["ImageUrl"] = string.IsNullOrEmpty(DRow["ImageUrl"].ToString()) ? "" : DRow["ImageUrl"].ToString().IndexOf("http") >= 0 ? DRow["ImageUrl"].ToString() : imageAddr + DRow["ImageUrl"].ToString();

                    dt.Rows.Add(dr);
                }
                result = JSONHelper.FromString(dt);
            }
            else
            {
                result = JSONHelper.FromString(dTableNotiHis);
            }
            #endregion
            dTableNotiHis.Dispose();
            return result;
        }
        #endregion

        #region 社区活动
        /// <summary>
        /// 社区活动
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string CommunityActivitiesList(DataRow Row)
        {
            string result = "";

            #region 接受参数
            string strCommunityId = "";//项目ID
            string strCurrPage = "1";//第几页
            string strPageSize = "10";//分页的大小

            if (Row.Table.Columns.Contains("CommunityId"))
            {
                strCommunityId = AppGlobal.ChkStr(Row["CommunityId"].ToString());
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数CommID");
            }
            if (Row.Table.Columns.Contains("CurrPage"))
            {
                strCurrPage = AppGlobal.ChkNum(Row["CurrPage"].ToString());
            }

            if (Row.Table.Columns.Contains("PageSize"))
            {
                strPageSize = AppGlobal.ChkNum(Row["PageSize"].ToString());
            }


            #endregion

            #region 变量定义
            string strErrMsg = "";
            string strCommID = "";

            string SQLContionString = "";

            int PageCount = 0;
            int Counts = 0;
            StringBuilder sListContent = new StringBuilder("");

            int iCurrPage = AppGlobal.StrToInt(strCurrPage);
            int iPageSize = AppGlobal.StrToInt(strPageSize);


            #endregion
            SQLContionString = ConnectionDb.GetConnection(Row["CommunityId"].ToString());
            MobileSoft.Model.Unified.Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(Row["CommunityId"].ToString());

            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            #region 查询社区活动
            string strSQLCommAct = "";
            if (Community.CorpID == 1975)
            {
                strSQLCommAct = " and isnull(IsDelete,0)=0 And isnull(IsRun, 1)=1 AND (CommID = " + Community.CommID + " OR CHARINDEX('" + Community.CommID + "',CommIdNvarchar)>0) ";
            }
            else
            {
                strSQLCommAct = " and isnull(IsDelete, 0)=0 And isnull(IsRun, 1)=1 AND CommID = " + Community.CommID;
            } 

            DataTable dTableCommAct = null;
            dTableCommAct = (new Business.TWBusinRule(SQLContionString)).HSPR_CommActivities_CutPage(out PageCount, out Counts, strSQLCommAct, iCurrPage, iPageSize);
            if (!pageHasData(iCurrPage, PageCount, Counts))
            {
                dTableCommAct.Dispose();
                dTableCommAct = new DataTable();
            }

            if (dTableCommAct.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("InfoID", typeof(string)));
                dt.Columns.Add(new DataColumn("Heading", typeof(string)));
                dt.Columns.Add(new DataColumn("IssueDate", typeof(string)));
                dt.Columns.Add(new DataColumn("ImageUrl", typeof(string)));

                foreach (DataRow DRow in dTableCommAct.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["InfoID"] = DRow["ActivitiesID"].ToString();
                    dr["Heading"] = DRow["ActivitiesTheme"].ToString();
                    dr["IssueDate"] = DRow["IssueDate"].ToString();
                    dr["ImageUrl"] = string.IsNullOrEmpty(DRow["ActivitiesImages"].ToString()) ? "" : DRow["ActivitiesImages"].ToString().IndexOf("http") >= 0 ? DRow["ActivitiesImages"].ToString() : imageAddr + DRow["ActivitiesImages"].ToString();

                    dt.Rows.Add(dr);
                }
                result = JSONHelper.FromString(dt);
            }
            else
            {
                result = JSONHelper.FromString(dTableCommAct);
            }
            #endregion
            dTableCommAct.Dispose();
            return result;
        }
        #endregion

        #region 服务指南
        /// <summary>
        /// 亲情提示、社区咨询
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string ServiceInformationList(DataRow Row)
        {
            string result = "";

            #region 接受参数
            string strCommunityId = "";//项目ID
            string strCurrPage = "1";//第几页
            string strPageSize = "10";//分页的大小

            if (Row.Table.Columns.Contains("CommunityId"))
            {
                strCommunityId = AppGlobal.ChkStr(Row["CommunityId"].ToString());
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数CommID");
            }
            if (Row.Table.Columns.Contains("CurrPage"))
            {
                strCurrPage = AppGlobal.ChkNum(Row["CurrPage"].ToString());
            }

            if (Row.Table.Columns.Contains("PageSize"))
            {
                strPageSize = AppGlobal.ChkNum(Row["PageSize"].ToString());
            }


            #endregion

            #region 变量定义
            string strErrMsg = "";
            string strCommID = "";

            string SQLContionString = "";

            int PageCount = 0;
            int Counts = 0;
            StringBuilder sListContent = new StringBuilder("");

            int iCurrPage = AppGlobal.StrToInt(strCurrPage);
            int iPageSize = AppGlobal.StrToInt(strPageSize);


            #endregion
            SQLContionString = ConnectionDb.GetConnection(Row["CommunityId"].ToString());
            MobileSoft.Model.Unified.Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(Row["CommunityId"].ToString());

            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }
            //strCommID = strCommunityId.Substring(5, 6);
            #region 查询服务指南    
            //游军铖加条件 AND (IsAudit=0 or IsAudit is null)只显示未屏蔽信息
            string strSQLSer = "  and isnull(IsDelete,0)=0 and isnull(IsAudit, 0)=0 AND CommID = " + Community.CommID + " and InfoType = 'fwzn' AND (ShowEndDate is null or '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'< ShowEndDate) ";

            DataTable dTableSer = null;
            dTableSer = (new Business.TWBusinRule(SQLContionString)).HSPR_CommunityInfo_CutPage(out PageCount, out Counts, strSQLSer, iCurrPage, iPageSize);
            if (!pageHasData(iCurrPage, PageCount, Counts))
            {
                dTableSer.Dispose();
                dTableSer = new DataTable();
            }

            if (dTableSer.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("InfoID", typeof(string)));
                dt.Columns.Add(new DataColumn("Heading", typeof(string)));
                dt.Columns.Add(new DataColumn("IssueDate", typeof(string)));
                dt.Columns.Add(new DataColumn("ImageUrl", typeof(string)));

                foreach (DataRow DRow in dTableSer.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["InfoID"] = DRow["InfoID"].ToString();
                    dr["Heading"] = DRow["Heading"].ToString();
                    dr["IssueDate"] = DRow["IssueDate"].ToString();
                    dr["ImageUrl"] = string.IsNullOrEmpty(DRow["ImageUrl"].ToString()) ? "" : DRow["ImageUrl"].ToString().IndexOf("http") >= 0 ? DRow["ImageUrl"].ToString() : imageAddr + DRow["ImageUrl"].ToString();

                    dt.Rows.Add(dr);
                }
                result = JSONHelper.FromString(dt);
            }
            else
            {
                result = JSONHelper.FromString(dTableSer);
            }
            #endregion
            dTableSer.Dispose();
            return result;
        }
        #endregion


        /// <summary>
        /// 判断分页是否应该返回数据
        /// </summary>
        /// <param name="currPage">当前的页码</param>
        /// <param name="pageSize">总页数</param>
        /// <param name="allCount">总条数</param>
        /// <returns></returns>
        private bool pageHasData(int currPage, int pageSize, int allCount)
        {
            if (pageSize >= currPage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetAll(DataRow Row)
        {
            string result = "";

            #region 接受参数
            string strCommunityId = "";//项目ID
            string strCurrPage = "1";//第几页
            string strPageSize = "10";//分页的大小

            if (Row.Table.Columns.Contains("CommunityId"))
            {
                strCommunityId = AppGlobal.ChkStr(Row["CommunityId"].ToString());
            }
            else
            {
                return JSONHelper.FromString(false, "缺少参数CommID");
            }
            if (Row.Table.Columns.Contains("CurrPage"))
            {
                strCurrPage = AppGlobal.ChkNum(Row["CurrPage"].ToString());
            }

            if (Row.Table.Columns.Contains("PageSize"))
            {
                strPageSize = AppGlobal.ChkNum(Row["PageSize"].ToString());
            }


            #endregion

            #region 变量定义
            string strErrMsg = "";
            string strCommID = "";

            string SQLContionString = "";

            int PageCount = 0;
            int Counts = 0;
            StringBuilder sListContent = new StringBuilder("");

            int iCurrPage = AppGlobal.StrToInt(strCurrPage);
            int iPageSize = AppGlobal.StrToInt(strPageSize);


            #endregion
            SQLContionString = ConnectionDb.GetConnection(Row["CommunityId"].ToString());
            MobileSoft.Model.Unified.Tb_Community Community = new MobileSoft.BLL.Unified.Bll_Tb_Community().GetModel(Row["CommunityId"].ToString());

            if (Community == null)
            {
                return JSONHelper.FromString(false, "该小区不存在");
            }

            #region 
            string strSQLNotiHis = " and isnull(IsAudit, 0)=0 AND CommID = " + Community.CommID + " AND (ShowEndDate is null or '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'< ShowEndDate) ";

            DataTable dTableNotiHis = null;
            dTableNotiHis = (new Business.TWBusinRule(SQLContionString)).HSPR_CommunityInfo_CutPage(out PageCount, out Counts, strSQLNotiHis, iCurrPage, iPageSize);

            if (!pageHasData(iCurrPage, PageCount, Counts))
            {
                dTableNotiHis.Dispose();
                dTableNotiHis = new DataTable();
            }

            if (dTableNotiHis.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("InfoID", typeof(string)));
                dt.Columns.Add(new DataColumn("Heading", typeof(string)));
                dt.Columns.Add(new DataColumn("IssueDate", typeof(string)));
                dt.Columns.Add(new DataColumn("ImageUrl", typeof(string)));

                foreach (DataRow DRow in dTableNotiHis.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["InfoID"] = DRow["InfoID"].ToString();
                    dr["Heading"] = DRow["Heading"].ToString();
                    dr["IssueDate"] = DRow["IssueDate"].ToString();
                    dr["ImageUrl"] = string.IsNullOrEmpty(DRow["ImageUrl"].ToString()) ? "" : DRow["ImageUrl"].ToString().IndexOf("http") >= 0 ? DRow["ImageUrl"].ToString() : imageAddr + DRow["ImageUrl"].ToString();

                    dt.Rows.Add(dr);
                }
                result += JSONHelper.FromString(dt);
            }
            else
            {
                result += JSONHelper.FromString(dTableNotiHis);
            }
            #endregion
            dTableNotiHis.Dispose();
            return result;
        }
    }

}
