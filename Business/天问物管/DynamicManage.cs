using Common;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using KernelDev.DataAccess;
using System.Reflection;

namespace Business
{
    public class DynamicManage : PubInfo
    {
        public DynamicManage()
        {
            base.Token = "20160505DynamicManage";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //验证登录
            if (!new Login().isLogin(ref Trans))
                return;

            //项目，责任人，时间

            Global_Var.LoginCommID = Row["CommID"].ToString();
            if (string.IsNullOrEmpty(Global_Var.LoginCommID))
            {
                Global_Var.LoginCommID = "0";
            }
            Global_Var.LoginOrganCode = Row["OrganCorpCode"].ToString();
            if (string.IsNullOrEmpty(Global_Var.LoginOrganCode))
            {
                Global_Var.LoginOrganCode = "01";
            }
            switch (Trans.Command)
            {
                //获取资源动态
                case "GetResoureDynamic":
                    Trans.Result = new BusinessDynamicsMg().GetDynamic(Row, "1");
                    break;
                //获取资源动态图表数据
                case "GetResoureDynamicChart":
                    Trans.Result = GetResoureDynamicChart(Row);
                    break;
                //获取收费动态
                case "GetChargeDynamic":
                    Trans.Result = new BusinessDynamicsMg().GetDynamic(Row, "2");
                    break;
                case "GetChargeDynamic_New":
                    Trans.Result = new BusinessDynamic_ty().GetChargeDynamic(Row);
                    break;
                //获取收费动态图表数据
                case "GetChargeDynamicChart":
                    Trans.Result = GetChargeDynamicChart(Row);
                    break;
                //获取客服动态
                case "GetServiceDynamic":
                    Trans.Result = new BusinessDynamicsMg().GetDynamic(Row, "3");
                    break;
                case "GetServiceDynamic_New":
                    Trans.Result = new BusinessDynamic_ty().GetIncidentDynamic(Row);
                    break;
                //获取客服动态图表数据
                case "GetServiceDynamicChart":
                    Trans.Result = GetServiceDynamicChart(Row["OrganCorpCode"].ToString(), AppGlobal.StrToInt(Row["CommID"].ToString()));
                    break;
                //获取物资动态数据
                case "GetMaterialDynamic":
                    Trans.Result = GetMaterialDynamic();
                    break;
                //获取物资动态图表数据
                case "GetMaterialDynamicChart":
                    Trans.Result = GetMaterialDynamicChart();
                    break;
                //获取人事动态数据
                case "GetHRDynamic":
                    Trans.Result = GetHRDynamic(Row);
                    break;
                //获取人事动态图表数据
                case "GetHRDynamicChart":
                    Trans.Result = GetHRDynamicChart(Row);
                    break;
                //获取品质动态数据
                case "GetQualityDynamic":
                    Trans.Result = GetQualityDynamic(Row);
                    break;
                //获取品质图表数据
                case "GetQualityDynamicChart":
                    Trans.Result = GetQualityDynamicChart(Row);
                    break;
                //获取设备动态数据
                case "GetDeviceDynamic":
                    Trans.Result = GetDeviceDynamic(Row);
                    break;
                //获取设备动态图表数据
                case "GetDeviceDynamicChart":
                    Trans.Result = GetDeviceDynamicChart(Row);
                    break;
                //获取成本动态数据
                case "GetCostDynamic":
                    Trans.Result = GetCostDynamic();
                    break;
                //获取成本动态图表数据
                case "GetCostDynamicChart":
                    Trans.Result = GetCostDynamicChart();
                    break;
                #region 资源动态(New)
                //获取资源数据(新桌面,2017-08-08添加)
                case "GetResourceKPI":
                    Trans.Result = GetResourceKPI(Row);
                    break;
                //获取公司管理项目数据(新桌面,2017-08-08添加)
                case "GetProject":
                    Trans.Result = GetProject(Row);
                    break;
                //获取公司管理业态数据(新桌面,2017-08-08添加)
                case "GetManageformat":
                    Trans.Result = GetManageformat(Row);
                    break;
                #endregion

                #region 客服动态(New)
                //获取客服KPI数据
                case "GetServiceKPI":
                    Trans.Result = GetServiceKPI(Row);
                    break;
                case "GetServiceKPI_lichuang":
                    Trans.Result = GetServiceKPI_lichuang(Row);
                    break;
                //获取客服--报事来源数据
                case "GetServiceSource":
                    Trans.Result = GetServiceSource(Row);
                    break;
                //获取客服--本月投诉数据
                case "GetServiceComplaints":
                    Trans.Result = GetServiceComplaints(Row);
                    break;
                //获取客服--本月报事数据
                case "GetServiceTrends":
                    Trans.Result = GetServiceTrends(Row);
                    break;
                #endregion

                #region 人事动态(New)
                case "GetPersonelKPI":
                    Trans.Result = GetPersonalKPI(Row);
                    break;
                ////获取人事动态公司人员编制数据
                case "GetPersonelCompanyStaffing":
                    Trans.Result = GetPersonelCompanyStaffing(Row);
                    break;
                ////获取人事动态公司人员结构数据
                case "GetPersonelCompanyStructure":
                    Trans.Result = GetPersonelCompanyStructure(Row);
                    break;
                #endregion

                #region 品质动态(New)
                case "GetQualityCompanyStaffing":
                    Trans.Result = GetQualityCompanyStaffing(Row);
                    break;
                case "GetQualityQuestion":
                    Trans.Result = GetQualityQuestion(Row);
                    break;
                #endregion

                #region 收费动态(New)
                case "GetChargeKpi":
                    Trans.Result = GetChargeKpi(Row);
                    break;
                case "GetChargeList":
                    Trans.Result = GetChargeList(Row);
                    break;
                case "GetChargeRate":
                    Trans.Result = GetChargeRate(Row);
                    break;
                #endregion

                #region 设备动态(New)
                case "GetEquipmentDynamic":
                    Trans.Result = GetEquipmentDynamic(Row);
                    break;
                case "GetEquipmentIntactRate":
                    Trans.Result = GetEquipmentIntactRate(Row);
                    break;
                case "GetEquipmentStatus":
                    Trans.Result = GetEquipmentStatus(Row);
                    break;
                #endregion
                default:
                    Trans.Result = JSONHelper.FromString(true, "没有动态!");
                    break;
            }
        }
        /// <summary>
        /// 获取往年欠费回收，往年欠费，本年实收，本年应收
        /// </summary>
        /// <param name="Rows"></param>
        /// <returns></returns>
        private string GetHJFeesDynamic(DataRow Rows)
        {
            string result = "", sql = "";
            try
            {
                if (Rows.Table.Columns.Contains("CommID") && !string.IsNullOrEmpty(Rows["CommID"].ToString()))
                {
                    sql = "select BefLastFeesRate_Mole ,BefLastFeesRate_Deno,CurYearFeesRate_Mole,CurYearFeesRate_Deno  from Tb_Sys_TakePic where CommID=" + Rows["CommID"].ToString();

                }
                if (Rows.Table.Columns.Contains("OrganCode") && !string.IsNullOrEmpty(Rows["OrganCode"].ToString()) && Rows["OrganCode"].ToString().Length == 4)
                {
                    sql = @"select sum( BefLastFeesRate_Mole) BefLastFeesRate_Mole ,sum( BefLastFeesRate_Deno) BefLastFeesRate_Deno,sum( CurYearFeesRate_Mole) CurYearFeesRate_Mole,
                            sum( CurYearFeesRate_Deno) CurYearFeesRate_Deno  
                            from Tb_Sys_TakePic where OrganCode = '" + Rows["OrganCode"].ToString() + "'";
                }
                if (Rows.Table.Columns.Contains("OrganCode") && !string.IsNullOrEmpty(Rows["OrganCode"].ToString()) && Rows["OrganCode"].ToString().Length == 2)
                {
                    sql = @"select sum( BefLastFeesRate_Mole) BefLastFeesRate_Mole ,sum( BefLastFeesRate_Deno) BefLastFeesRate_Deno,sum( CurYearFeesRate_Mole) CurYearFeesRate_Mole,
                            sum( CurYearFeesRate_Deno) CurYearFeesRate_Deno  
                            from Tb_Sys_TakePic ";
                }

                DataTable dt = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).Query(sql).Tables[0];
                result = JSONHelper.FromDataTable(dt);
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, "请联系管理员处理。" + ex.Message);
            }

            return result;
        }




        #region 设备动态(New)

        private string GetEquipmentDynamic(DataRow Row)
        {
            return GetEquipmentDynamic(Global_Var.LoginOrganCorp, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetEquipmentDynamic(string organCode, string commId, string connString)
        {
            try
            {
                string rs = "";
                StringBuilder strWhere = new StringBuilder();
                string CommID = "", OrganCorp = "";
                if (commId != "" && commId != "0" && commId.Length == 6)
                {
                    strWhere.Append(" AND CommID='" + commId + "' ");
                    CommID = commId;
                }
                else
                {
                    if (organCode != "01")
                    {
                        strWhere.Append(" AND CommID IN (SELECT CommID FROM Tb_HSPR_Community WHERE OrganCode = '" + organCode + "') ");
                        OrganCorp = organCode;
                    }
                }
                double wcl = 0.00, GZL = 0.00;//设备完好率
                                              ////////////
                SqlParameter[] parametersWCL = {
                   new SqlParameter("@CommID", SqlDbType.VarChar,36),
                    new SqlParameter("@OrganCode", SqlDbType.VarChar,36),
                     new SqlParameter("@Type", SqlDbType.VarChar,36)
                                 };
                parametersWCL[0].Value = CommID;
                parametersWCL[1].Value = OrganCorp;
                parametersWCL[2].Value = "WHL";
                DataTable DsWCL = new DbHelperSQLP(connString).RunProcedure("Proc_Eq_Statistics_Deasktop", parametersWCL, "RetDataSet").Tables[0];
                ///////////////

                if (DsWCL.Rows.Count > 0)
                {
                    DataRow[] ROWSIN = DsWCL.Select("Type='WHL'");
                    wcl = Convert.ToDouble(ROWSIN[0][0]);
                    wcl = Math.Round(wcl, 2);
                    DataRow[] ROWSINGZ = DsWCL.Select("Type='GZL'");
                    GZL = Convert.ToDouble(ROWSINGZ[0][0]);
                    GZL = Math.Round(GZL, 2);
                }



                DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime date2 = date1.AddMonths(1).AddSeconds(-1);
                SqlParameter[] parameters = {
                    new SqlParameter("@date1", SqlDbType.DateTime),
                   new SqlParameter("@date2", SqlDbType.DateTime),
                   new SqlParameter("@ItemCode", SqlDbType.VarChar,36),
                   new SqlParameter("@OrganCode", SqlDbType.VarChar,36)
                                 };
                parameters[0].Value = date1;
                parameters[1].Value = date2;
                parameters[2].Value = commId;
                parameters[3].Value = organCode;
                DataSet Ds = new DbHelperSQLP(connString).RunProcedure("Proc_Eq_Tasktop_Return", parameters, "RetDataSet");
                if (Ds != null && Ds.Tables.Count > 0)
                {
                    rs = wcl.ToString() + ',' + Ds.Tables[0].Rows[0][0].ToString() + "," + GZL.ToString();
                }
                else
                {
                    rs = wcl.ToString() + ",0,0,0,0" + "," + GZL.ToString();
                }

                return new ApiResult(true, rs).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }


        private string GetEquipmentIntactRate(DataRow Row)
        {
            try
            {
                string rs = "";
                StringBuilder strWhere = new StringBuilder();
                if (Global_Var.LoginCommID != "" && Global_Var.LoginCommID != "0")
                {
                    strWhere.Append(" AND CommID='" + Global_Var.LoginCommID + "' ");
                }
                else
                {
                    if (Global_Var.LoginOrganCode != "01")
                    {
                        strWhere.Append(" AND CommID IN (SELECT CommID FROM Tb_HSPR_Community WHERE OrganCode = '" + Global_Var.LoginOrganCode + "') ");
                    }
                }
                int zs = (int)DbHelperSQL.GetSingle(@" SELECT COUNT(1) FROM dbo.Tb_EQ_Equipment WHERE  ISNULL(IsDelete, 0) = 0 " + strWhere.ToString());
                int zcs = (int)DbHelperSQL.GetSingle(@"SELECT COUNT(1) FROM dbo.Tb_EQ_Equipment WHERE  ISNULL(IsDelete, 0) = 0   AND Statue = '正常' " + strWhere.ToString());
                double wcl = 0.00;//设备完好率
                if (zcs != 0 && zs != 0)
                {
                    wcl = zcs / (double)zs;
                    wcl = Math.Round(wcl * 100, 2);
                }
                DateTime date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime date2 = date1.AddMonths(1).AddSeconds(-1);
                SqlParameter[] parameters = {
                    new SqlParameter("@date1", SqlDbType.DateTime),
                   new SqlParameter("@date2", SqlDbType.DateTime),
                   new SqlParameter("@ItemCode", SqlDbType.VarChar,36),
                   new SqlParameter("@OrganCode", SqlDbType.VarChar,36)
                                 };
                parameters[0].Value = date1;
                parameters[1].Value = date2;
                parameters[2].Value = Global_Var.LoginCommID;
                parameters[3].Value = Global_Var.LoginOrganCode;
                DataSet Ds = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_Eq_Tasktop_Return", parameters, "RetDataSet");
                if (Ds != null && Ds.Tables.Count > 0)
                {
                    rs = wcl.ToString() + ',' + Ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    rs = wcl.ToString() + ",0,0,0,0";
                }
                return new ApiResult(true, rs).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        private string GetEquipmentStatus(DataRow Row)
        {
            return GetEquipmentStatus(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetEquipmentStatus(string organCode, string commId, string connString)
        {
            try
            {
                int PageCount;
                int Counts;
                int StartIndex = 1, EndIndex = 100;

                SqlParameter[] parameters = {
                    new SqlParameter("@CommID", SqlDbType.VarChar, 255),
                    new SqlParameter("@EqId", SqlDbType.VarChar, 255),
                    new SqlParameter("@StatisticsDate",SqlDbType.DateTime, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };

                if (commId != "0" && commId.Length == 6)
                {
                    parameters[0].Value = commId;
                }
                else
                {
                    if (organCode != "01")
                    {
                        //查询区域下所有CommID
                        string sqlOrgan = "SELECT CommID FROM dbo.Tb_HSPR_Community WHERE OrganCode=" + organCode + " AND ISNULL(IsDelete,0)=0 ";
                        IDbConnection conn = new SqlConnection(connString);
                        DataSet dsOrgan = conn.ExecuteReader(sqlOrgan).ToDataSet();

                        for (int i = 0; i < dsOrgan.Tables[0].Rows.Count; i++)
                        {
                            commId += dsOrgan.Tables[0].Rows[i]["CommID"] + ",";
                        }
                        commId = commId.Substring(0, commId.Length - 1);
                        parameters[0].Value = "'" + commId + "'";
                    }
                    else
                    {

                        parameters[0].Value = "";
                    }
                }

                parameters[1].Value = "";
                parameters[2].Value = Convert.ToDateTime(DateTime.Now.ToString());
                parameters[3].Value = Convert.ToInt32(EndIndex);
                parameters[4].Value = Convert.ToInt32(StartIndex);
                DataSet Ds = new DbHelperSQLP(connString).RunProcedure("Proc_Mt_EquipmentIntactRateStatisticsSys", parameters, "RetDataSet");
                PageCount = Convert.ToInt32(parameters[5].Value);
                Counts = Convert.ToInt32(parameters[6].Value);

                //查询所有统计的设备系统
                DataRow[] row = Ds.Tables[0].Select("OrganName='全部'");
                DataTable dt = Ds.Tables[0].Copy();
                dt.Clear();
                for (int i = 1; i < row.Length; i++)
                {
                    dt.ImportRow(row[i]);
                }
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                foreach (DataRow item in dt.Rows)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("RankName", item["RankName"]);
                    dic.Add("设备数量", item["设备数量"]);
                    dic.Add("正常数量", item["正常数量"]);
                    dic.Add("维修数量", item["维修数量"]);
                    dic.Add("维保数量", item["维保数量"]);
                    dic.Add("故障数量", item["故障数量"]);
                    dic.Add("正常停机数量", item["正常停机数量"]);
                    dic.Add("异常停机数量", item["异常停机数量"]);
                    dic.Add("暂无状态数量", item["暂无状态数量"]);
                    list.Add(dic);

                }
                return new ApiResult(true, list).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }
        #endregion

        #region 收费动态(New)

        private string GetChargeRate(DataRow Row)
        {
            return GetChargeRate(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetChargeRate(string organCode, string commId, string connString)
        {
            try
            {
                string strSQL = string.Format("select * from Tb_Sys_TakePic_DeskTop收费率 where StatYear in ('{0}','{1}')",
                DateTime.Now.AddYears(-1).Year.ToString(), DateTime.Now.Year.ToString());
                if (commId != "0" && commId.Length == 6)
                {
                    strSQL += string.Format(" and CommID={0}", commId);
                }
                else
                {
                    strSQL += string.Format(" and OrganCode='{0}'", organCode);
                }

                DataTable dt = new DbHelperSQLP(connString).Query(strSQL).Tables[0];
                return new ApiResult(true, dt).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        /// <summary>
        /// 获取收费排行榜数据
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetChargeList(DataRow Row)
        {
            return GetChargeList(Global_Var.LoginOrganCode, PubConstant.hmWyglConnectionString, Row["type"].ToString(), AppGlobal.StrToInt(Row["page"].ToString()), AppGlobal.StrToInt(Row["size"].ToString()));
        }

        public string GetChargeList(string organCode, string connString, string type, int page = 1, int size = 5)
        {
            try
            {
                string nums = string.Empty;
                string strSQL = @"select top 10  * from (select ROW_NUMBER() over(order by ChargeRate2 desc)
                                        as ROWINDEX,* from view_Sys_TakePic_Filter where 1>0";
                string strSQL_nums = "select COUNT(1) from view_Sys_TakePic_Filter where 1>0";

                if (organCode == "01")
                {
                    if (type == "1")
                    {
                        strSQL += @" and datediff(day,StatDate,getdate()) = 1 and StatType = 2)";
                        strSQL_nums += @" and datediff(day,StatDate,getdate()) = 1 and StatType = 2";
                        nums = new DbHelperSQLP(connString).GetSingle(strSQL_nums).ToString();
                    }
                    else
                    {
                        strSQL += @" and datediff(day,StatDate,getdate()) = 1 and StatType = 1)";
                        strSQL_nums += @" and datediff(day,StatDate,getdate()) = 1 and StatType = 1";
                        nums = new DbHelperSQLP(connString).GetSingle(strSQL_nums).ToString();
                    }
                }
                else
                {
                    strSQL += string.Format(@" and OrganCode = '{0}' and datediff(day,StatDate,getdate()) = 1 and StatType = 1)", organCode);
                    strSQL_nums += string.Format(@" and OrganCode = '{0}' and datediff(day,StatDate,getdate()) = 1 and StatType = 1", organCode);
                    nums = new DbHelperSQLP(connString).GetSingle(strSQL_nums).ToString();
                }

                strSQL += string.Format("as a where a.ROWINDEX between {0} and {1}", (page - 1) * size + 1, ((page - 1) * size) + size);

                DataTable dTable = new DbHelperSQLP(connString).Query(strSQL).Tables[0];

                string strwgphbxAxisName = "[";
                //本月物管费
                string strChargeRate1 = "[";
                //本年月物管费
                string strChargeRate2 = "[";
                if (null != dTable && dTable.Rows.Count > 0)
                {
                    foreach (DataRow DRow in dTable.Rows)
                    {
                        if (organCode == "01" && type == "1")
                        {
                            strwgphbxAxisName = strwgphbxAxisName + "'" + DRow["OrganName"].ToString() + "'" + ",";
                        }
                        else
                        {
                            strwgphbxAxisName = strwgphbxAxisName + "'" + DRow["CommName"].ToString() + "'" + ",";
                        }
                        strChargeRate1 = strChargeRate1 + DRow["ChargeRate1"].ToString() + ",";
                        strChargeRate2 = strChargeRate2 + DRow["ChargeRate2"].ToString() + ",";
                    }
                    strwgphbxAxisName = strwgphbxAxisName.Substring(0, strwgphbxAxisName.Length - 1);
                    strChargeRate1 = strChargeRate1.Substring(0, strChargeRate1.Length - 1);
                    strChargeRate2 = strChargeRate2.Substring(0, strChargeRate2.Length - 1);
                }

                strwgphbxAxisName += "]";
                strChargeRate1 += "]";
                strChargeRate2 += "]";

                //项目名称
                string CommList = strwgphbxAxisName;
                //本年物管收缴率排行
                string YearChargeData = strChargeRate2;
                //本月物管收缴率排行
                string MonthChargeData = strChargeRate1;

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("CommList", CommList);
                dic.Add("MonthChargeData", MonthChargeData);
                dic.Add("YearChargeData", YearChargeData);
                dic.Add("count", nums);
                return new ApiResult(true, dic).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        /// <summary>
        /// 获取收费kpi数据
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetChargeKpi(DataRow Row)
        {
            return GetChargeKpi(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetChargeKpi(string organCode, string commId, string connString)
        {
            try
            {
                string strSQL = "SELECT ISNULL(年初往年欠费,0) AS kpi_1,ISNULL(本年应收收入,0) AS kpi_2,ISNULL(本年实际收入,0) AS kpi_3,ISNULL(本年收费率,0) AS kpi_4,ISNULL(往年欠费追缴率,0) AS kpi_5,ISNULL(本年前期欠费,0) AS kpi_6,ISNULL(本月应收收入,0) AS kpi_7,ISNULL(本月实际收入,0) AS kpi_8,ISNULL(本月收费率,0) AS kpi_9,ISNULL(本年前期追缴率,0) AS kpi_10,ISNULL(本月房屋转让办理,0) AS kpi_11,ISNULL(本月房屋租赁办理,0) AS kpi_12,ISNULL(本月房屋装修办理,0) AS kpi_13,ISNULL(本月房屋状态变更,0) AS kpi_14,ISNULL(本月合同时间到期,0) AS kpi_15,ISNULL(本月合同费用到期,0) AS kpi_16 FROM Tb_Sys_TakePic_DeskTopKPI";
                if (commId != "0" && commId.Length == 6)
                {
                    strSQL += string.Format(" WHERE CommID = {0}", commId);
                }
                else
                {
                    strSQL += string.Format(" WHERE OrganCode = '{0}' AND CommID = 0", organCode);
                }

                using (var conn = new SqlConnection(connString))
                {
                    dynamic resultInfo = conn.Query(strSQL).FirstOrDefault();

                    if (resultInfo != null)
                    {
                        return new ApiResult(true, resultInfo).toJson();
                    }

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("kpi_1", 0);
                    dic.Add("kpi_2", 0);
                    dic.Add("kpi_3", 0);
                    dic.Add("kpi_4", 0);
                    dic.Add("kpi_5", 0);
                    dic.Add("kpi_6", 0);
                    dic.Add("kpi_7", 0);
                    dic.Add("kpi_8", 0);
                    dic.Add("kpi_9", 0);
                    dic.Add("kpi_10", 0);
                    dic.Add("kpi_11", 0);
                    dic.Add("kpi_12", 0);
                    dic.Add("kpi_13", 0);
                    dic.Add("kpi_14", 0);
                    dic.Add("kpi_15", 0);
                    dic.Add("kpi_16", 0);
                    return new ApiResult(true, dic).toJson();
                }
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }
        #endregion

        #region 品质动态(New)
        /// <summary>
        /// 获取品质动态问题情况
        /// </summary>
        /// <returns></returns>
        private string GetQualityQuestion(DataRow Row)
        {
            return GetQualityQuestion(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetQualityQuestion(string organCode, string commId, string connString)
        {
            try
            {
                #region 取得数据
                string ItemCode = "", OrganCode = "";
                if (commId != "" && commId != "0" && commId.Length == 6)
                {
                    ItemCode = commId;
                }
                else
                {
                    if (organCode != "01")
                    {
                        OrganCode = organCode;
                    }
                }
                DataTable dTable = new DataTable();
                SqlParameter[] parameters = {
                    new SqlParameter("@Month", SqlDbType.VarChar,36),
                     new SqlParameter("@ItemCode", SqlDbType.VarChar,36),
                      new SqlParameter("@OrganCode", SqlDbType.VarChar,36)
                                        };
                parameters[0].Value = DateTime.Now.Month.ToString();
                parameters[1].Value = ItemCode;
                parameters[2].Value = OrganCode;
                dTable = new DbHelperSQLP(connString).RunProcedure("Proc_Qm_Tasktop_Statistics", parameters, "RetDataSet").Tables[0];
                DataView dv = new DataView(dTable);
                dTable = dv.ToTable(true, GetColumnsByDataTable(dTable));
                #endregion

                StringBuilder strMale = new StringBuilder();
                StringBuilder strFemale = new StringBuilder();
                decimal totalCount = 0;
                if (dTable.Rows.Count > 0)
                {
                    totalCount = Convert.ToDecimal(dTable.Compute("sum(NowProblemCount)", "true"));
                }
                if (totalCount > 0)
                {
                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        strFemale.Append("{\"name\": \"" + dTable.Rows[i]["ProfessionalName"] + "\",\"NowProblemCount\":" + dTable.Rows[i]["NowProblemCount"] + ",\"Proportion\":" + (Math.Round((Convert.ToDecimal(dTable.Rows[i]["NowProblemCount"]) / totalCount) * 100, 2)) + ",\"y\":" + dTable.Rows[i]["NowProblemCount"] + "},");
                    }
                }
                string rs = "[]";
                if (strFemale.ToString() != "")
                    rs = "[" + strMale.ToString() + strFemale.ToString().Substring(0, strFemale.ToString().Length - 1) + "]";
                else
                {
                    strFemale.Append("{\"name\": \"暂无数据\",\"NowProblemCount\":0,\"Proportion\":0,\"y\":120},{\"name\": \"暂无数据\",\"NowProblemCount\":0,\"Proportion\":0,\"y\":60},");
                    rs = "[" + strMale.ToString() + strFemale.ToString().Substring(0, strFemale.ToString().Length - 1) + "]";
                }
                return new ApiResult(true, rs).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        /// <summary>
        /// 获取品质动态本月核查情况
        /// </summary>
        /// <returns></returns>
        private string GetQualityCompanyStaffing(DataRow Row)
        {
            int page = 1, rows = 5;
            if (Row.Table.Columns.Contains("page") == true && Row["page"].ToString() != "")
            {
                page = Convert.ToInt32(Row["page"].ToString());
            }
            if (Row.Table.Columns.Contains("rows") == true && Row["rows"].ToString() != "")
            {
                rows = Convert.ToInt32(Row["rows"].ToString());
            }

            return GetQualityCompanyStaffing(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString, page, rows);
        }

        public string GetQualityCompanyStaffing(string organCode, string commId, string connString, int page = 1, int rows = 5)
        {
            try
            {
                #region 取得数据
                System.Text.StringBuilder strWhere = new System.Text.StringBuilder();
                int PageCount, Counts;

                StringBuilder strWher = new StringBuilder();
                strWher.Append(" AND TaskCount!=0 ");
                string Types = "0";
                if (commId != "" && commId != "0" && commId.Length == 6)
                {
                    strWher.Append(" AND ItemCode='" + commId + "' ");
                }
                else
                {
                    Types = "1";
                    if (organCode != "01")
                    {
                        strWher.Append(" AND ItemCode IN (SELECT CommID FROM Tb_HSPR_Community WHERE OrganCode = '" + organCode + "') ");
                    }
                }
                #endregion

                DataTable dTable = GetList(connString, out PageCount, out Counts, strWher.ToString(), page, rows, "TaskCount", 0, "Tb_Qm_Task_Month_Desktop_Temp", "RoleCode", Types).Tables[0];
                dTable.Columns.Add("TaskRateB", typeof(decimal));
                dTable.Columns.Add("ProblemRateB", typeof(decimal));
                dTable.Columns.Add("CompleteRateB", typeof(decimal));
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                foreach (DataRow dr in dTable.Rows)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("RoleName", dr["RoleName"]);
                    dic.Add("TaskRate", dr["TaskRate"]);
                    dic.Add("ProblemRate", dr["ProblemRate"]);
                    dic.Add("CompleteRate", dr["CompleteRate"]);
                    dic.Add("TaskCount", dr["TaskCount"]);

                    dic.Add("TaskRateB", 100 - Convert.ToDecimal(dr["TaskRate"]));
                    dic.Add("ProblemRateB", 100 - Convert.ToDecimal(dr["ProblemRate"]));
                    dic.Add("CompleteRateB", 100 - Convert.ToDecimal(dr["CompleteRate"]));
                    list.Add(dic);
                }

                Dictionary<string, object> resultDic = new Dictionary<string, object>();
                resultDic.Add("count", Counts);
                resultDic.Add("list", list);
                return new ApiResult(true, resultDic).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        private static DataSet GetList(string LoginSQLConnStr, out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize, string SortField, int Sort, string SQLView, string SQLParaKey, string types)
        {
            StringBuilder strSql = new StringBuilder();
            if (types == "0")
            {
                strSql.Append("SELECT RoleCode,RoleName,TaskCount ,TaskRate,ProblemRate,CompleteRate FROM " + SQLView + " WHERE 1=1 " + StrCondition);
            }
            else
            {
                strSql.Append("SELECT RoleCode,RoleName,SUM(TaskCount) AS TaskCount,");
                strSql.Append("(CONVERT(DECIMAL(18,2),(CONVERT(DECIMAL,sum(TaskOkCount))/CONVERT(DECIMAL,sum(TaskCount)) * 100))) AS TaskRate,  (CONVERT(DECIMAL(18, 2), (CONVERT(DECIMAL, sum(TaskAbarbeitungCount)) / CONVERT(DECIMAL,  CASE WHEN  sum(TaskAbarbeitungSaveCount) != 0 THEN sum(TaskAbarbeitungSaveCount) ELSE 1 END  ) * 100))) AS ProblemRate, ");
                strSql.Append(" (CONVERT(DECIMAL(18,2),(CONVERT(DECIMAL,sum(TaskAbarbeitungIsOkCount))/CONVERT(DECIMAL,    CASE WHEN  sum(TaskAbarbeitungCount) != 0 THEN sum(TaskAbarbeitungCount) ELSE 1 END   ) * 100))) AS CompleteRate");
                strSql.Append(" FROM Tb_Qm_Task_Month_Desktop_Temp WHERE 1=1  " + StrCondition + " GROUP BY RoleCode,RoleName ");
            }
            SqlParameter[] parameters = {
                    new SqlParameter("@FldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Sort", SqlDbType.Int),
                    new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
                    new SqlParameter("@Id", SqlDbType.VarChar, 50),
                    new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
                    };
            parameters[0].Value = "*";
            parameters[1].Value = PageSize;
            parameters[2].Value = PageIndex == 0 ? 1 : PageIndex;
            parameters[3].Value = SortField;
            parameters[4].Value = Sort;
            parameters[5].Value = strSql.ToString();
            parameters[6].Value = SQLParaKey;
            DataSet Ds = new DbHelperSQLP(LoginSQLConnStr).RunProcedure("Proc_System_TurnPage", parameters, "RetDataSet");
            PageCount = Convert.ToInt32(parameters[7].Value);
            Counts = Convert.ToInt32(parameters[8].Value);
            return Ds;
        }

        #endregion

        #region 人事动态(New)
        /// <summary>
        /// 获取人事结构
        /// </summary>
        /// <returns></returns>
        private string GetPersonelCompanyStructure(DataRow Row)
        {
            return GetPersonelCompanyStructure(Global_Var.LoginOrganCode, Global_Var.LoginCommID, Global_Var.LoginUserCode, PubConstant.hmWyglConnectionString);
        }

        public string GetPersonelCompanyStructure(string organCode, string commId, string userCode, string connString)
        {
            try
            {
                string LoginRoles = string.Empty;
                IDataParameter[] iData = new SqlParameter[3];
                iData[0] = new SqlParameter("@UserCode", userCode);
                iData[1] = new SqlParameter("@OrganCode", organCode);
                iData[2] = new SqlParameter("@CommID", AppGlobal.StrToInt(commId));

                DataTable dTable = new DbHelperSQLP(connString).RunProcedure("Proc_Sys_User_FilterRoles", iData).ToDataSet().Tables[0];
                if (dTable.Rows.Count > 0)
                {
                    LoginRoles = dTable.Rows[0][0].ToString();
                }
                LoginRoles = LoginRoles.Replace("'", "");
                string strColumn = "", strColumnValue = "";
                StringBuilder StrSql = new StringBuilder();
                string strPro = "Proc_Tb_Pm_PersonList_PersonType_Statistics";//机构统计
                StrSql.Append("  LEFT JOIN (SELECT CRoleCode FROM Tb_Sys_RoleRolePope WHERE CHARINDEX(RoleCode,'" + LoginRoles + "')>0) AS A ON View_Tb_Pm_PersonList_Filter.RoleCode = A.CRoleCode ");
                StrSql.Append(" WHERE 1=1  and ISNULL(PersonType,'')!=''  AND ISNULL(IsDelete,0)=0 ");
                // StrSql.Append(" AND (RoleCode IN() OR ISNULL(RoleCode,'')='') ");//权限
                StringBuilder StrSql1 = new StringBuilder();
                StrSql1.Append("  LEFT JOIN (SELECT CRoleCode FROM Tb_Sys_RoleRolePope WHERE CHARINDEX(RoleCode,'" + LoginRoles + "')>0) AS B ON A.RoleCode = B.CRoleCode ");
                StrSql1.Append(" WHERE 1=1  and ISNULL(PersonType,'')!=''  AND ISNULL(IsDelete,0)=0 ");

                DataTable dtColumn = new DbHelperSQLP(connString).Query(@"select Id,DType,Name,Code,Sort,IsDelete FROM Tb_Pm_Dictionary where DType='人员类别' AND ISNULL(IsDelete,'0')=0 order by Sort desc").Tables[0];
                DataTable NewdtColumn = new DataTable();
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                Dictionary<string, object> resultDic;
                if (dtColumn.Rows.Count > 0)
                {
                    for (int i = 0; i < dtColumn.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            strColumn += " '人员类别'=[人员类别]";
                            strColumnValue += " '人员类别'=99999 ";
                        }
                        strColumn += ",'" + dtColumn.Rows[i]["Name"].ToString().Trim() + "'=[" + dtColumn.Rows[i]["Name"].ToString().Trim() + "]";
                        strColumnValue += ",'" + dtColumn.Rows[i]["Name"].ToString().Trim() + "'= (select COUNT(*) from #View_Tb_Pm_PersonList_Filter where  PersonType='" + dtColumn.Rows[i]["Id"].ToString().Trim() + "') ";
                        //DataColumn dc = new DataColumn("");
                        //NewdtColumn.Columns.Add(dc);
                    }

                    DataTable dt = getRylbtjListProcedure(StrSql.ToString(), StrSql1.ToString(), strColumn, strColumnValue, strPro, connString).Tables[0];

                    Dictionary<string, int> dic = new Dictionary<string, int>();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dic.Add(dt.Columns[i].ColumnName.ToString(), Convert.ToInt32(dt.Rows[1][dt.Columns[i].ColumnName]));
                    }
                    Dictionary<string, int> dic1desc = dic.OrderByDescending(o => o.Value).ToDictionary(o => o.Key, p => p.Value);

                    foreach (KeyValuePair<string, int> item in dic1desc)
                    {
                        DataColumn dc = new DataColumn(item.Key);
                        NewdtColumn.Columns.Add(dc);
                    }
                    DataRow dr = NewdtColumn.NewRow();
                    foreach (KeyValuePair<string, int> item in dic1desc)
                    {
                        dr[item.Key] = item.Value;
                    }
                    NewdtColumn.Rows.Add(dr);
                    if (NewdtColumn.Rows.Count > 0)
                    {
                        decimal downTotal = 0;
                        decimal topEnd = 0;
                        for (int i = 1; i < NewdtColumn.Columns.Count; i++)
                        {
                            downTotal += Convert.ToDecimal(NewdtColumn.Rows[0][NewdtColumn.Columns[i].ColumnName]);
                        }
                        for (int i = 1; i < NewdtColumn.Columns.Count; i++)
                        {
                            if (downTotal > 0)
                                topEnd = (Convert.ToDecimal(NewdtColumn.Rows[0][NewdtColumn.Columns[i].ColumnName]) / downTotal) * 100;
                            resultDic = new Dictionary<string, object>();
                            resultDic.Add("name", NewdtColumn.Columns[i].ColumnName);
                            resultDic.Add("PersonNum", Convert.ToDecimal(NewdtColumn.Rows[0][NewdtColumn.Columns[i].ColumnName]));
                            resultDic.Add("y", Math.Round(topEnd, 2));
                            list.Add(resultDic);
                        }
                    }
                }
                return new ApiResult(true, list).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        /// <summary>
        /// 获取人事动态KPI数据
        /// </summary>
        /// <returns></returns>
        private string GetPersonelCompanyStaffing(DataRow Row)
        {
            return GetPersonelCompanyStaffing(PubConstant.hmWyglConnectionString, Row["Type"].ToString(), AppGlobal.StrToInt(Row["StartIndex"].ToString()), AppGlobal.StrToInt(Row["EndIndex"].ToString()));
        }

        public string GetPersonelCompanyStaffing(string connString, string type, int startIndex = 0, int endIndex = 0)
        {
            try
            {
                string sqlStr = "";

                if (type == "1")
                {
                    //              sqlStr = @"select * from (select a.DepName,ROW_NUMBER() OVER (ORDER BY  a.DepName desc) AS  ROWNUMBER,
                    //                   ISNULL((select sum(PersonNum) from View_Tb_Pm_Allocation_Filter where SortDepCode like a.SortDepCode+'%' and ISNULL(IsDelete,0)=0),0) as PersonNum,
                    //                  (select count(1) from View_Tb_Pm_PersonList_Filter where SortDepCode like a.SortDepCode+'%' and ISNULL(IsDelete,0)=0 and PersonState!='已离职') as RealNum,
                    //                  isnull((CONVERT(NUMERIC(18,2),(select sum(PersonNum) from View_Tb_Pm_Allocation_Filter where SortDepCode like a.SortDepCode+'%' and ISNULL(IsDelete,0)=0))-CONVERT(NUMERIC(18,2),(select count(1) from View_Tb_Pm_PersonList_Filter where SortDepCode like a.SortDepCode+'%' and ISNULL(IsDelete,0)=0 and PersonState!='已离职'))),0) as LostNum,
                    //CONVERT(varchar,((isnull((CONVERT(NUMERIC(18,2),((CONVERT(NUMERIC(18,2),(isnull(((CONVERT(NUMERIC(18,2),(select sum(PersonNum) from View_Tb_Pm_Allocation_Filter where SortDepCode like a.SortDepCode+'%' and ISNULL(IsDelete,0)=0))-CONVERT(NUMERIC(18,2),
                    //(select count(1) from View_Tb_Pm_PersonList_Filter where SortDepCode like a.SortDepCode+'%' and ISNULL(IsDelete,0)=0 and PersonState!='已离职'))) ),0)))/nullif(CONVERT(NUMERIC(18,2),(ISNULL((select sum(PersonNum) from View_Tb_Pm_Allocation_Filter where SortDepCode like a.SortDepCode+'%' and ISNULL(IsDelete,0)=0),0))),0))))),0))*100))+'%' as Point
                    //                  from Tb_Sys_Department a where len(SortDepCode)=8 ) as ROWNUMBERs";

                    sqlStr = "select * from View_Tb_Pm_DepAllocation a order by a.PersonNum desc";

                }
                else
                {
                    //            sqlStr = @"select * from (SELECT ROW_NUMBER() OVER (ORDER BY  a.id desc) AS  ROWNUMBER, A.PersonNum,c.TypeName as DepName,
                    //                      (select COUNT(1) from Tb_Pm_PersonList t where t.RoleCode = A.RoleCode AND t.DepCode=A.DepCode and PersonState!='已离职' AND ISNULL(t.IsDelete,0)=0  )  as RealNum,
                    // ((A.PersonNum - (select COUNT(1) from Tb_Pm_PersonList t       
                    //                        WHERE t.RoleCode = A.RoleCode AND t.DepCode=A.DepCode and PersonState!='已离职'  AND ISNULL(t.IsDelete,0)=0  ))) as LostNum,
                    //CONVERT(varchar,isnull((CONVERT(NUMERIC(18,2),((CONVERT(NUMERIC(18,2),(((A.PersonNum - (select COUNT(1) from Tb_Pm_PersonList t       
                    //                        WHERE t.RoleCode = A.RoleCode AND t.DepCode=A.DepCode and PersonState!='已离职'  AND ISNULL(t.IsDelete,0)=0  )))))/nullif((CONVERT(NUMERIC(18,2),(A.PersonNum))),0))))),0)*100)+'%' as Point
                    //                        FROM Tb_Pm_Allocation A  LEFT OUTER JOIN Tb_Sys_Department B ON A.DepCode=B.DepCode  LEFT OUTER JOIN Tb_Pm_PersonRole C ON A.RoleCode=C.Code where TypeName!='' group by a.id,A.PersonNum,c.TypeName,a.RoleCode,a.DepCode  ) as ROWNUMBERs ";

                    sqlStr = "select * from View_Tb_Pm_RoleAllocation a order by a.PersonNum desc";
                }

                DataTable dTable = new DbHelperSQLP(connString).Query(sqlStr).Tables[0];
                int allCount = dTable.Rows.Count;
                var result = (from DataRow order in dTable.Rows
                              where Convert.ToInt32(order["ROWNUMBER"]) > startIndex && Convert.ToInt32(order["ROWNUMBER"]) <= endIndex
                              select new { ROWNUMBER = order["ROWNUMBER"], PersonNum = order["PersonNum"], DepName = order["DepName"], RealNum = order["RealNum"], LostNum = order["LostNum"], Point = order["Point"] }).ToList();

                dTable = ToDataTable(result);

                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

                if (null != dTable && dTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dTable.Rows)
                    {
                        Dictionary<string, object> dic2 = new Dictionary<string, object>();
                        dic2.Add("DepName", row["DepName"]);
                        dic2.Add("PersonNum", row["PersonNum"]);
                        dic2.Add("RealNum", row["RealNum"]);
                        dic2.Add("LostNum", row["LostNum"]);
                        dic2.Add("Point", row["Point"]);
                        list.Add(dic2);
                    }
                }
                dic.Add("list", list);
                dic.Add("count", allCount);
                return new ApiResult(true, dic).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        public static DataTable ToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names 
            PropertyInfo[] oProps = null;
            if (varlist == null)
                return dtReturn;
            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                             == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        /// <summary>
        /// 获取人事动态KPI数据
        /// </summary>
        /// <returns></returns>
        private string GetPersonalKPI(DataRow Row)
        {
            return GetPersonalKPI(PubConstant.hmWyglConnectionString);
        }

        public string GetPersonalKPI(string connString)
        {
            try
            {
                string sqlStr = @"select distinct (select sum(PersonNum) from View_Tb_Pm_Allocation_Filter where isnull(isdelete,0)=0) as PersonNum,
							(select COUNT(1) from View_Tb_Pm_PersonList_Filter where PersonState<>'已离职' AND ISNULL(IsDelete,0)=0) as PersonState ,
							cast((nullif(CAST((select sum(PersonNum) from View_Tb_Pm_Allocation_Filter where isnull(isdelete,0)=0)-(select COUNT(1) from View_Tb_Pm_PersonList_Filter where PersonState<>'已离职' AND ISNULL(IsDelete,0)=0) as DECIMAL(18,2)),0))/nullif(CAST((select sum(PersonNum) from 
							View_Tb_Pm_Allocation_Filter where isnull(isdelete,0)=0) as DECIMAL(18,2)),0) as DECIMAL(18,2)) *100   as Vacancies,
                            (select count(1) from Tb_Pm_Contract a left join View_Tb_Pm_PersonList_Filter b on a.Pid=b.Id where PersonState<>'已离职' AND ISNULL(a.IsDelete,0)=0) as ContractNum,
                            (select count(1) from Tb_Pm_Contract a left join View_Tb_Pm_PersonList_Filter b on a.Pid =b.Id where PersonState<>'已离职' AND ISNULL(a.IsDelete,0)=0  and EndDate  > DATEADD(mm, DATEDIFF(mm,0,getdate()), 0) and EndDate < DATEADD(ms,-3,DATEADD(mm, DATEDIFF(m,0,getdate())+1, 0))) AS ContractExpire,
                            (select count(1) from Tb_Pm_Certificate a left join View_Tb_Pm_PersonList_Filter b on a.Pid=b.Id where ISNULL(a.IsDelete,0)=0) as CertificateNum,
                            cast((nullif(cast((select count(1) from Tb_Pm_Certificate a left join View_Tb_Pm_PersonList_Filter b on a.Pid=b.Id where ISNULL(a.IsDelete,0)=0) as DECIMAL(18,2)),0)/nullif(cast((select COUNT(1) from View_Tb_Pm_PersonList_Filter where PersonState<>'已离职' AND ISNULL(IsDelete,0)=0) as DECIMAL(18,2)),0) *100) as DECIMAL(18,2)) as Certificate,
                            (select count(1) from View_Tb_Pm_PersonList_Filter  where EntryDate > DATEADD(mm, DATEDIFF(mm,0,getdate()), 0) and EntryDate < DATEADD(ms,-3,DATEADD(mm, DATEDIFF(m,0,getdate())+1, 0)) and ISNULL(IsDelete,0)=0) as EntryNum,
                            (select count(1) from View_Tb_Pm_PersonList_Filter  where WorkDimissionDate > DATEADD(mm, DATEDIFF(mm,0,getdate()), 0) and WorkDimissionDate <  DATEADD(ms,-3,DATEADD(mm, DATEDIFF(m,0,getdate())+1, 0)) and ISNULL(IsDelete,0)=0) as WorkDimissionNum,
                            cast((nullif(cast((select count(1) from View_Tb_Pm_PersonList_Filter  where WorkDimissionDate > DATEADD(mm, DATEDIFF(mm,0,getdate()), 0) and WorkDimissionDate<  DATEADD(ms,-3,DATEADD(mm, DATEDIFF(m,0,getdate())+1, 0)) and ISNULL(IsDelete,0)=0) as DECIMAL(18,2)),0)/nullif(cast((select COUNT(1) from View_Tb_Pm_PersonList_Filter where PersonState<>'已离职' AND ISNULL(IsDelete,0)=0) as DECIMAL(18,2)),0)*100) as DECIMAL(18,2)) as Quit
                            from View_Tb_Pm_PersonList_Filter";

                DataTable dt = new DbHelperSQLP(connString).Query(sqlStr).Tables[0];
                Dictionary<string, object> obj = new Dictionary<string, object>();
                if (dt.Rows.Count > 0)
                {
                    obj.Add("PersonNum", AppGlobal.StrToInt(dt.Rows[0]["PersonNum"].ToString()));
                    obj.Add("PersonState", AppGlobal.StrToInt(dt.Rows[0]["PersonState"].ToString()));
                    obj.Add("Vacancies", AppGlobal.StrToInt(dt.Rows[0]["Vacancies"].ToString()));
                    obj.Add("ContractNum", AppGlobal.StrToInt(dt.Rows[0]["ContractNum"].ToString()));
                    obj.Add("ContractExpire", AppGlobal.StrToInt(dt.Rows[0]["ContractExpire"].ToString()));
                    obj.Add("CertificateNum", AppGlobal.StrToInt(dt.Rows[0]["CertificateNum"].ToString()));
                    obj.Add("Certificate", AppGlobal.StrToInt(dt.Rows[0]["Certificate"].ToString()));
                    obj.Add("EntryNum", AppGlobal.StrToInt(dt.Rows[0]["EntryNum"].ToString()));
                    obj.Add("WorkDimissionNum", AppGlobal.StrToInt(dt.Rows[0]["WorkDimissionNum"].ToString()));
                    obj.Add("Quit", AppGlobal.StrToInt(dt.Rows[0]["Quit"].ToString()));
                }
                else
                {
                    obj.Add("PersonNum", 0);
                    obj.Add("PersonState", 0);
                    obj.Add("Vacancies", 0);
                    obj.Add("ContractNum", 0);
                    obj.Add("ContractExpire", 0);
                    obj.Add("CertificateNum", 0);
                    obj.Add("Certificate", 0);
                    obj.Add("EntryNum", 0);
                    obj.Add("WorkDimissionNum", 0);
                    obj.Add("Quit", 0);
                }
                return new ApiResult(true, obj).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }
        #endregion

        #region 客户动态(New)
        /// <summary>
        /// 获取客户本月报事数据
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetServiceTrends(DataRow Row)
        {
            return GetServiceTrends(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetServiceTrends(string organCode, string commId, string connString)
        {
            try
            {
                IDataParameter[] iData = new SqlParameter[2];
                if (!"0".Equals(commId) && commId.Length == 6)
                {
                    iData[0] = new SqlParameter("@OrganCode", "");
                    iData[1] = new SqlParameter("@CommID", commId);
                }
                else
                {
                    if (TypeRule.TWSysNodeCode.BlocCode.Equals(organCode))
                    {
                        iData[0] = new SqlParameter("@OrganCode", "");
                        iData[1] = new SqlParameter("@CommID", "0");
                    }
                    else
                    {
                        iData[0] = new SqlParameter("@OrganCode", organCode);
                        iData[1] = new SqlParameter("@CommID", "0");
                    }
                }
                DataTable dt = new DbHelperSQLP(connString).RunProcedure("Proc_Sys_TakePicIncidentTypeDeskTop_Filter", iData).ToDataSet().Tables[0];
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("TypeName", row["TypeName"]);
                        dic.Add("AllCount", row["AllAcount"]);
                        list.Add(dic);
                    }
                }
                return new ApiResult(true, list).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        /// <summary>
        /// 获取客户报事来源信息
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetServiceSource(DataRow Row)
        {
            return GetServiceSource(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetServiceSource(string organCode, string commId, string connString)
        {
            try
            {
                string strSQL = "select IncidentMode,SUM(AllAcount) as AllAcount from Tb_Sys_TakePicIncidentTypeDeskTopIncidentMode";
                if (!"0".Equals(commId) && commId.Length == 6)
                {
                    strSQL += string.Format(" where CommID='{0}'", commId);
                }
                else
                {
                    if (organCode != TypeRule.TWSysNodeCode.BlocCode)
                    {
                        strSQL += string.Format(" where OrganCode='{0}'", organCode);
                    }
                }
                strSQL += " group by IncidentMode";
                DataTable dt = new DbHelperSQLP(connString).Query(strSQL).Tables[0];
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("AllAcount", row["AllAcount"]);
                        dic.Add("IncidentMode", row["IncidentMode"]);
                        list.Add(dic);
                    }
                }
                return new ApiResult(true, list).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        /// <summary>
        /// 获取客户本月投诉信息
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetServiceComplaints(DataRow Row)
        {
            return GetServiceComplaints(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetServiceComplaints(string organCode, string commId, string connString)
        {
            try
            {
                IDataParameter[] iData = new SqlParameter[2];
                if (!"0".Equals(commId) && commId.Length == 6)
                {
                    iData[0] = new SqlParameter("@OrganCode", "");
                    iData[1] = new SqlParameter("@CommID", commId);
                }
                else
                {
                    if (TypeRule.TWSysNodeCode.BlocCode.Equals(organCode))
                    {
                        iData[0] = new SqlParameter("@OrganCode", "");
                        iData[1] = new SqlParameter("@CommID", "0");
                    }
                    else
                    {
                        iData[0] = new SqlParameter("@OrganCode", organCode);
                        iData[1] = new SqlParameter("@CommID", "0");
                    }
                }
                DataTable dt = new DbHelperSQLP(connString).RunProcedure("Proc_Sys_TakePicIncidentTypeDeskTop_Ts_Filter", iData).ToDataSet().Tables[0];
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("TypeName", row["TypeName"]);
                        dic.Add("AllCount", row["AllAcount"]);
                        list.Add(dic);
                    }
                }
                return new ApiResult(true, list).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        /// <summary>
        /// 获取客服kpi数据
        /// </summary>
        /// <param name="Row"></param>
        /// <returns></returns>
        private string GetServiceKPI(DataRow Row)
        {
            return GetServiceKPI(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetServiceKPI(string organCode, string commId, string connString)
        {
            try
            {
                string strSQL = "select ISNULL(本月报事发生量,0) as kpi_1,ISNULL(本月报事分派量,0) as kpi_2,ISNULL(报事分派率,0) as kpi_3,ISNULL(逾期未分派报事,0) as kpi_4,ISNULL(本月报事处理量,0) as kpi_5,ISNULL(报事处理及时率,0) as kpi_6,ISNULL(逾期未处理报事,0) as kpi_7,ISNULL(本月报事回访量,0) as kpi_8,ISNULL(报事回访及时率,0) as kpi_9,ISNULL(逾期未回访报事,0) as kpi_10,ISNULL(本月投诉发生量,0) as kpi_11,ISNULL(本月客户满意度,0) as kpi_12  from Tb_Sys_TakePic_DeskTopKPI";
                if (commId != "0" && commId.Length == 6)
                {
                    strSQL += string.Format(" where CommID={0}", commId);
                }
                else
                {
                    strSQL += string.Format(" where OrganCode='{0}'", organCode);
                }
                DataTable dt = new DbHelperSQLP(connString).Query(strSQL).Tables[0];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (dt.Rows.Count > 0)
                {
                    dic.Add("kpi_1", dt.Rows[0]["kpi_1"]);
                    dic.Add("kpi_2", dt.Rows[0]["kpi_2"]);
                    dic.Add("kpi_3", dt.Rows[0]["kpi_3"]);
                    dic.Add("kpi_4", dt.Rows[0]["kpi_4"]);
                    dic.Add("kpi_5", dt.Rows[0]["kpi_5"]);
                    dic.Add("kpi_6", dt.Rows[0]["kpi_6"]);
                    dic.Add("kpi_7", dt.Rows[0]["kpi_7"]);
                    dic.Add("kpi_8", dt.Rows[0]["kpi_8"]);
                    dic.Add("kpi_9", dt.Rows[0]["kpi_9"]);
                    dic.Add("kpi_10", dt.Rows[0]["kpi_10"]);
                    dic.Add("kpi_11", dt.Rows[0]["kpi_11"]);
                    dic.Add("kpi_12", dt.Rows[0]["kpi_12"]);
                }
                else
                {
                    dic.Add("kpi_1", 0);
                    dic.Add("kpi_2", 0);
                    dic.Add("kpi_3", 0);
                    dic.Add("kpi_4", 0);
                    dic.Add("kpi_5", 0);
                    dic.Add("kpi_6", 0);
                    dic.Add("kpi_7", 0);
                    dic.Add("kpi_8", 0);
                    dic.Add("kpi_9", 0);
                    dic.Add("kpi_10", 0);
                    dic.Add("kpi_11", 0);
                    dic.Add("kpi_12", 0);
                }
                return new ApiResult(true, dic).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }


        #endregion


        private string GetServiceKPI_lichuang(DataRow Row)
        {
            return GetServiceKPI_lichuang(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetServiceKPI_lichuang(string organCode, string commId, string connString)
        {
            try
            {
                string strSQL = @"SELECT 
                                    ISNULL(本月报事发生量,0)AS kpi_1,
                                    ISNULL(本月报事完成率,0)AS kpi_2,
                                    ISNULL(本月报事关闭率,0)AS kpi_3,
                                    ISNULL(本月投诉发生量,0)AS kpi_4,
                                    ISNULL(本月投诉关闭率,0)AS kpi_5,
                                    ISNULL(本月客户满意度,0)AS kpi_6,
                                    ISNULL(年度报事发生量,0)AS kpi_7,
                                    ISNULL(年度报事完成率,0)AS kpi_8,
                                    ISNULL(年度报事关闭率,0)AS kpi_9,
                                    ISNULL(年度投诉总量,0)AS kpi_10,
                                    ISNULL(年度投诉关闭率,0)AS kpi_11,
                                    ISNULL(年度报事满意度,0)AS kpi_12
                                    FROM Tb_Sys_TakePic_DeskTopKPI";
                if (commId != "0" && commId.Length == 6)
                {
                    strSQL += string.Format(" WHERE CommID={0}", commId);
                }
                else
                {
                    strSQL += string.Format(" WHERE OrganCode='{0}'", organCode);
                }
                DataTable dt = new DbHelperSQLP(connString).Query(strSQL).Tables[0];
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (dt.Rows.Count > 0)
                {
                    dic.Add("kpi_1", dt.Rows[0]["kpi_1"]);
                    dic.Add("kpi_2", dt.Rows[0]["kpi_2"]);
                    dic.Add("kpi_3", dt.Rows[0]["kpi_3"]);
                    dic.Add("kpi_4", dt.Rows[0]["kpi_4"]);
                    dic.Add("kpi_5", dt.Rows[0]["kpi_5"]);
                    dic.Add("kpi_6", dt.Rows[0]["kpi_6"]);
                    dic.Add("kpi_7", dt.Rows[0]["kpi_7"]);
                    dic.Add("kpi_8", dt.Rows[0]["kpi_8"]);
                    dic.Add("kpi_9", dt.Rows[0]["kpi_9"]);
                    dic.Add("kpi_10", dt.Rows[0]["kpi_10"]);
                    dic.Add("kpi_11", dt.Rows[0]["kpi_11"]);
                    dic.Add("kpi_12", dt.Rows[0]["kpi_12"]);
                }
                else
                {
                    dic.Add("kpi_1", 0);
                    dic.Add("kpi_2", 0);
                    dic.Add("kpi_3", 0);
                    dic.Add("kpi_4", 0);
                    dic.Add("kpi_5", 0);
                    dic.Add("kpi_6", 0);
                    dic.Add("kpi_7", 0);
                    dic.Add("kpi_8", 0);
                    dic.Add("kpi_9", 0);
                    dic.Add("kpi_10", 0);
                    dic.Add("kpi_11", 0);
                    dic.Add("kpi_12", 0);
                }
                return new ApiResult(true, dic).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }


        #region 资源动态(New)
        /// <summary>
        /// 获取管理业态数据
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        private string GetManageformat(DataRow Row)
        {
            //默认按面积
            int type = 0;
            if (Row.Table.Columns.Contains("type") && !string.IsNullOrEmpty(Row["type"].ToString()))
            {
                if (!int.TryParse(Row["type"].ToString(), out type))
                {
                    type = 0;
                }
            }

            return GetManageformat(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString, type);
        }

        public string GetManageformat(string organCode, string commId, string connString, int type = 0)
        {
            // type = 0，默认按面积
            try
            {
                string strSQL = string.Empty;

                if (type == 0)
                {

                    strSQL = "select ISNULL(PropertyUses,'住宅') as [name], SUM(BuildArea) as [value] from Tb_HSPR_Room where IsDelete =0 and IsSplitUnite <> 1  and IsSplitUnite <> 3 ";
                    if (organCode != "01")
                    {
                        if (organCode.Length != 6)
                        {
                            strSQL = strSQL + " and CommID in (select CommID from Tb_HSPR_Community where OrganCode = '" + organCode + "' and IsDelete = 0 )";
                        }
                        if (!string.IsNullOrEmpty(commId) && commId.Length == 6)
                        {
                            strSQL = strSQL + " and CommID = " + commId + "";
                        }
                    }
                    strSQL = strSQL + "group by ISNULL(PropertyUses,'住宅')";
                }
                else
                {

                    strSQL = "select ISNULL(PropertyUses,'住宅') as [name], COUNT(1) as [value] from Tb_HSPR_Room   where IsDelete =0 and IsSplitUnite <> 1  and IsSplitUnite <> 3 ";
                    if (organCode != "01")
                    {
                        if (organCode.Length != 6)
                        {
                            strSQL = strSQL + " and CommID in (select CommID from Tb_HSPR_Community where OrganCode = '" + organCode + "' and IsDelete = 0 )";

                        }
                        if (!string.IsNullOrEmpty(commId) && commId.Length == 6)
                        {
                            strSQL = strSQL + " and CommID = " + commId + "";
                        }
                    }
                    strSQL = strSQL + "group by ISNULL(PropertyUses,'住宅')";




                    //按户数
                    //strSQL = "select ISNULL(PropertyUses,'住宅') as [name], COUNT(1) as [value] from Tb_HSPR_Room   where IsDelete =0 and IsSplitUnite <> 1  and IsSplitUnite <> 3 ";
                    //if (organCode != "01" && organCode.Length != 6)
                    //{
                    //    strSQL = strSQL + " and CommID in (select CommID from Tb_HSPR_Community where OrganCode = '" + organCode + "' and IsDelete = 0 )";
                    //    if (!string.IsNullOrEmpty(commId) && commId.Length == 6)
                    //    {
                    //        strSQL = strSQL + " and CommID = " + commId + "";
                    //    }
                    //}
                    //strSQL = strSQL + "group by ISNULL(PropertyUses,'住宅')";
                }
                DataTable dt = new DbHelperSQLP(connString).Query(strSQL).Tables[0];
                Dictionary<string, object> obj;
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                foreach (DataRow item in dt.Rows)
                {
                    obj = new Dictionary<string, object>();
                    obj.Add("name", item["name"].ToString());
                    obj.Add("value", item["value"].ToString());
                    list.Add(obj);
                }
                return new ApiResult(true, list).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        /// <summary>
        /// 获取资源数据
        /// (新桌面,2017-08-08添加)
        /// </summary>
        /// <returns></returns>
        private string GetResourceKPI(DataRow Row)
        {
            return GetResourceKPI(Global_Var.LoginOrganCode, Global_Var.LoginCommID, PubConstant.hmWyglConnectionString);
        }

        public string GetResourceKPI(string organCode, string commId, string connString)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            string strsql = string.Empty;

            try
            {
                if (!"0".Equals(commId) && commId.Length == 6)
                {
                    //管理区域
                    dic.Add("manage_area", 0);
                    //管理项目
                    dic.Add("manage_pro", 1);
                }
                else
                {
                    if (organCode == "01")
                    {
                        //管理区域
                        strsql = "select OrganCode from Tb_HSPR_Community where IsDelete=0 group by OrganCode";
                        dic.Add("manage_area", new DbHelperSQLP(connString).Query(strsql).Tables[0].Rows.Count);
                    }
                    else
                    {

                        dic.Add("manage_area", 1);
                    }

                    //管理项目
                    strsql = "select CommID from Tb_HSPR_Community where IsDelete=0";
                    if (!"01".Equals(organCode))
                    {
                        strsql = string.Format(strsql + " and OrganCode = '{0}'", organCode);
                    }
                    dic.Add("manage_pro", new DbHelperSQLP(connString).Query(strsql).Tables[0].Rows.Count);
                }

                //住宅项目数量
                if ("01".Equals(organCode))
                {
                    strsql = "select COUNT(1) from Tb_HSPR_Community where IsDelete=0 and CommKind='住宅'";
                }
                else
                {
                    if (string.IsNullOrEmpty(commId) || commId.Length < 6)
                    {
                        strsql = string.Format("select COUNT(1) from Tb_HSPR_Community where IsDelete=0 and CommKind='住宅' and OrganCode='{0}'", organCode);
                    }
                    else
                    {
                        strsql = string.Format("select COUNT(1) from Tb_HSPR_Community where IsDelete=0 and CommKind='住宅' and CommID= {0}", commId);
                    }
                }
                dic.Add("house_pro", new DbHelperSQLP(connString).GetSingle(strsql).ToString());

                //公建项目
                if ("01".Equals(organCode))
                {
                    strsql = "select COUNT(1) from Tb_HSPR_Community where IsDelete=0 and (CommKind = '公建' OR CommKind = '公共')";
                }
                else
                {
                    if (string.IsNullOrEmpty(commId) || commId.Length < 6)
                    {
                        strsql = string.Format("select COUNT(1) from Tb_HSPR_Community where IsDelete=0 and (CommKind = '公建' OR CommKind = '公共') and OrganCode='{0}'", organCode);
                    }
                    else
                    {
                        strsql = string.Format("select COUNT(1) from Tb_HSPR_Community where IsDelete=0 and (CommKind = '公建' OR CommKind = '公共') and CommID={0}", commId);
                    }
                }
                dic.Add("public_pro", new DbHelperSQLP(connString).GetSingle(strsql).ToString());

                //建筑面积
                if ("01".Equals(organCode))
                {
                    strsql = "select  ISNULL(SUM(BuildArea),0) from Tb_HSPR_Room where IsDelete=0 and CommID in(select CommID from tb_hspr_community where isdelete = 0)";
                }
                else
                {
                    if (string.IsNullOrEmpty(commId) || commId.Length < 6)
                    {
                        strsql = string.Format(@"select  ISNULL(SUM(BuildArea),0) from Tb_HSPR_Room
                                                    where IsDelete=0 and CommID in
                                                    (select CommID from tb_hspr_community where isdelete = 0  and OrganCode='{0}')", organCode);
                    }
                    else
                    {
                        strsql = string.Format("select  ISNULL(SUM(BuildArea),0) from Tb_HSPR_Room where IsDelete=0 and CommID='{0}'", commId);
                    }
                }
                dic.Add("build_area", Math.Round(AppGlobal.StrToDec(new DbHelperSQLP(connString).GetSingle(strsql).ToString()) / 10000, 2));

                //套内面积
                if ("01".Equals(organCode))
                {
                    strsql = "select  ISNULL(SUM(InteriorArea),0) from Tb_HSPR_Room where IsDelete=0 and CommID in(select CommID from tb_hspr_community where isdelete = 0)";
                }
                else
                {
                    if (string.IsNullOrEmpty(commId) || commId.Length < 6)
                    {
                        strsql = string.Format(@"select  ISNULL(SUM(InteriorArea),0) from Tb_HSPR_Room
                                                    where IsDelete=0 and CommID in
                                                    (select CommID from tb_hspr_community where isdelete = 0  and OrganCode='{0}')", organCode);
                    }
                    else
                    {
                        strsql = string.Format("select ISNULL(SUM(InteriorArea),0) from Tb_HSPR_Room where IsDelete=0 and CommID='{0}'", commId);
                    }
                }
                dic.Add("jacket_area", Math.Round(AppGlobal.StrToDec(new DbHelperSQLP(connString).GetSingle(strsql).ToString()) / 10000, 2));

                // 海亮添加在管面积
                if (Global_Var.LoginCorpID == "2021")
                {
                    if ("01".Equals(organCode))
                    {
                        strsql = "select ISNULL(SUM(ISNULL(BuildArea,0)),0) from tb_hspr_community where isdelete = 0";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(commId) || commId.Length < 6)
                        {
                            strsql = string.Format(@"select ISNULL(SUM(ISNULL(BuildArea,0)),0) from tb_hspr_community where isdelete = 0 and OrganCode='{0}'", organCode);
                        }
                        else
                        {
                            strsql = string.Format("select ISNULL(SUM(ISNULL(BuildArea,0)),0) from tb_hspr_community where isdelete = 0 and CommID='{0}'", commId);
                        }
                    }
                    dic.Add("manage_build_area", Math.Round(AppGlobal.StrToDec(new DbHelperSQLP(connString).GetSingle(strsql).ToString()) / 10000, 2));
                }

                //楼宇数量
                if ("01".Equals(organCode))
                {
                    strsql = @"select COUNT(1) from Tb_HSPR_Building as b where IsDelete=0
                                    and CommID in(select CommID from tb_hspr_community where isdelete = 0 )  
                                     and (select count(*) from Tb_HSPR_Room where CommID = b.CommID and Isdelete = 0)>0";
                }
                else
                {
                    if (string.IsNullOrEmpty(commId) || commId.Length < 6)
                    {
                        strsql = string.Format(@"select COUNT(1) from Tb_HSPR_Building as b where IsDelete=0
                                                    and CommID in(select CommID from tb_hspr_community where isdelete = 0 and OrganCode='{0}')  
                                                     and (select count(*) from Tb_HSPR_Room where CommID = b.CommID and Isdelete = 0)>0", organCode);
                    }
                    else
                    {
                        strsql = string.Format("select COUNT(1) from Tb_HSPR_Building where IsDelete=0 and CommID='{0}'", commId);
                    }
                }
                dic.Add("build_sum", new DbHelperSQLP(connString).GetSingle(strsql).ToString());

                //房屋数量
                if ("01".Equals(organCode))
                {
                    strsql = "select COUNT(1) from Tb_HSPR_Room where IsDelete=0 and commid in(select CommID from tb_hspr_community where isdelete = 0 )";
                }
                else
                {
                    if (string.IsNullOrEmpty(commId) || commId.Length < 6)
                    {

                        strsql = string.Format(@"select  COUNT(1) from Tb_HSPR_Room
                                                    where IsDelete=0 and CommID in
                                                    (select CommID from tb_hspr_community where isdelete = 0  and OrganCode='{0}')", organCode);
                    }
                    else
                    {
                        strsql = string.Format("select COUNT(1) from Tb_HSPR_Room where IsDelete=0 and CommID='{0}'", commId);
                    }
                }

                dic.Add("room_sum", new DbHelperSQLP(connString).GetSingle(strsql).ToString());

                //车位数量
                if ("01".Equals(organCode))
                {
                    strsql = "select COUNT(1) from Tb_HSPR_Parking where IsDelete=0 and commid in(select CommID from tb_hspr_community where isdelete = 0 )";
                }
                else
                {
                    if (string.IsNullOrEmpty(commId) || commId.Length < 6)
                    {
                        strsql = string.Format(@"select  COUNT(1) from Tb_HSPR_Parking
                                                    where IsDelete=0 and CommID in
                                                    (select CommID from tb_hspr_community where isdelete = 0  and OrganCode='{0}')", organCode);
                    }
                    else
                    {
                        strsql = string.Format("select COUNT(1) from Tb_HSPR_Parking where IsDelete=0 and CommID='{0}'", commId);
                    }
                }

                dic.Add("park_sum", new DbHelperSQLP(connString).GetSingle(strsql).ToString());

                //客户数量
                if ("01".Equals(organCode))
                {
                    strsql = @"select  Count(*) as CustNums
		                        from Tb_HSPR_Customer as a 
		                        inner join Tb_HSPR_CustomerLive as b
		                        on a.CustID = b.CustID 
		                        where isnull(a.IsDelete,0) = 0 and( b.LiveType = 1  or b.LiveType = 2 )and isnull(b.IsDelLive,0) = 0
                         and commid in(select CommID from tb_hspr_community where isdelete = 0 )";
                }
                else
                {
                    if (string.IsNullOrEmpty(commId) || commId.Length < 6)
                    {
                        strsql = string.Format(@"select  Count(*) as CustNums
		                                            from Tb_HSPR_Customer as a 
		                                            inner join Tb_HSPR_CustomerLive as b
		                                            on a.CustID = b.CustID 
		                                            where isnull(a.IsDelete,0) = 0 and( b.LiveType = 1  or b.LiveType = 2 )and isnull(b.IsDelLive,0) = 0
                                             and commid in(select CommID from tb_hspr_community where isdelete = 0 and OrganCode='{0}')", organCode);
                    }
                    else
                    {
                        strsql = string.Format(@"select  Count(*) as CustNums
		                                            from Tb_HSPR_Customer as a 
		                                            inner join Tb_HSPR_CustomerLive as b
		                                            on a.CustID = b.CustID 
		                                            where isnull(a.IsDelete,0) = 0 and( b.LiveType = 1  or b.LiveType = 2 )and isnull(b.IsDelLive,0) = 0
                                             and commid in(select CommID from tb_hspr_community where isdelete = 0 and CommID={0})", commId);
                    }
                }

                dic.Add("customer_sum", new DbHelperSQLP(connString).GetSingle(strsql).ToString());

                return new ApiResult(true, dic).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message + Environment.NewLine + ex.StackTrace).toJson();
            }
        }

        /// <summary>
        /// 获取公司管理项目数据
        /// (新桌面,2017-08-08添加)
        /// </summary>
        /// <returns></returns>
        private string GetProject(DataRow Row)
        {
            //默认按面积
            int type = 0;
            if (Row.Table.Columns.Contains("type") && !string.IsNullOrEmpty(Row["type"].ToString()))
            {
                if (!int.TryParse(Row["type"].ToString(), out type))
                {
                    type = 0;
                }
            }

            return GetProject(PubConstant.hmWyglConnectionString, type, " ");
        }

        public string GetProject(string connString, int type = 0, string oragcode = "")
        {
            // type = 0，默认按面积
            try
            {
                string strSQL = string.Empty;
                if (type == 0)
                {
                    strSQL = @"select SUBSTRING(Province,0,LEN(Province)) as [name],sum(BuildArea) as [value] from Tb_HSPR_Community as c
                           left join (select commid,SUM(BuildArea) BuildArea from  Tb_HSPR_Room where IsDelete =0 and IsSplitUnite <> 1  and IsSplitUnite <> 3
                           group by CommID ) r on c.CommID=r.CommID 
                           where isdelete = 0
                           group by Province";
                }
                else
                {

                    //ERP
                    //strSQL = "select ISNULL(PropertyUses,'住宅') as [name], COUNT(1) as [value] from Tb_HSPR_Room   where IsDelete =0 and IsSplitUnite <> 1  and IsSplitUnite <> 3 ";
                    //if (Bp.LoginOrganCode != "01")
                    //{
                    //    strSQL = strSQL + " and CommID in (select CommID from Tb_HSPR_Community where OrganCode = '" + Bp.LoginOrganCode + "' and IsDelete = 0 )";
                    //    if (AppGlobal.StrToInt(Bp.LoginCommID) != 0)
                    //    {
                    //        strSQL = strSQL + " and CommID = " + Bp.LoginCommID + "";
                    //    }
                    //}
                    //strSQL = strSQL + "group by ISNULL(PropertyUses,'住宅')";




                    //按项目
                    strSQL = "select SUBSTRING(Province,0,LEN(Province)) as [name] ,count(1) as [value] from Tb_HSPR_Community where ISNULL(IsDelete,0)=0 group by Province";
                }
                DataTable dt = new DbHelperSQLP(connString).Query(strSQL).Tables[0];
                Dictionary<string, object> obj;
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                foreach (DataRow item in dt.Rows)
                {
                    obj = new Dictionary<string, object>();
                    obj.Add("name", item["name"].ToString());
                    obj.Add("value", item["value"].ToString());
                    list.Add(obj);
                }
                return new ApiResult(true, list).toJson();
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ex.Message).toJson();
            }
        }

        #endregion


        private string GetCostDynamic()
        {

            SqlParameter[] parameters_1 = {
                   };
            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_CM_StatDesktop", parameters_1, "RetDataSet").Tables[0];
            StringBuilder Sb = new StringBuilder();
            if (dTable.Rows.Count > 0)
            {
                DataRow Row = dTable.Rows[0];
                Sb.Append("1、当前执行分供方合同数量" + Row["当前执行分供方合同数量"].ToString() + "，其中本月到期数量" + Row["本月到期数量"].ToString() + "，合同总额" + Row["合同总额"].ToString() + "元，已付总额" + Row["已付总额"].ToString() + "元，未付总额" + Row["未付总额"].ToString() + "元，本月应付" + Row["本月应付"].ToString() + "元");
                Sb.Append("<br/>2、本月计划支付" + Row["本月计划付"].ToString() + "元，实际支付" + Row["实际支付"].ToString() + "元。");
            }
            return JSONHelper.FromString(true, Sb.ToString());
        }

        private string GetCostDynamicChart()
        {

            SqlParameter[] parameters = {
                   };
            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_CM_StatDesktopTB", parameters, "RetDataSet").Tables[0];

            return JSONHelper.FromString(dTable);
        }

        private string GetDeviceDynamicChart(DataRow Row)
        {
            int zcs = (int)DbHelperSQL.GetSingle(@"SELECT COUNT(1) FROM dbo.Tb_EQ_Equipment WHERE  ISNULL(IsDelete, 0) = 0   AND Statue = '正常' ");
            int zs = (int)DbHelperSQL.GetSingle(@" SELECT COUNT(1) FROM dbo.Tb_EQ_Equipment WHERE  ISNULL(IsDelete, 0) = 0    ");
            double wcl = 0.00;
            if (zcs != 0 && zs != 0)
            {
                wcl = zcs / (double)zs;
                wcl = Math.Round(wcl * 100, 2);
            }
            string Ret = "{\"Result\":true,\"data\":[{";

            #region 仪表图 设备完好率

            Ret += "\"sb\":" + wcl;

            #region 巡检完成率
            DataTable Logins = DbHelperSQL.Query("SELECT RoleCode FROM TB_SYS_USERROLE WHERE USERCODE = '" + Global_Var.LoginUserCode + "'").Tables[0];
            if (Logins.Rows.Count > 0)
            {
                string roles = "";
                for (int i = 0; i < Logins.Rows.Count; i++)
                {
                    roles += Logins.Rows[i]["RoleCode"] + ",";
                }
                if (roles != "")
                {
                    Global_Var.LoginRoles = roles.Substring(0, roles.Length - 1);
                }
            }
            string begindate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01 00:00:00";
            string enddate = Convert.ToDateTime(begindate).AddMonths(1).AddDays(-1).ToShortDateString() + " 23:59:59";

            DataTable dTableBase = new DataTable();
            SqlParameter[] parametersTxt = {
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@LoginRoles", SqlDbType.NVarChar,500)
                                        };
            parametersTxt[0].Value = begindate;
            parametersTxt[1].Value = enddate;
            parametersTxt[2].Value = Global_Var.LoginRoles;
            dTableBase = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_Tb_EQ_Task_Show_PhoneChar_Statistics", parametersTxt, "RetDataSet").Tables[0];
            int rwzs = 0, wcsl = 0, wbrwzs = 0, wbwcsl = 0;
            decimal rwwcl = 0.00M;
            if (dTableBase.Rows.Count > 0)
            {
                rwzs = Convert.ToInt32(dTableBase.Rows[0]["totalnum"]);
                wcsl = Convert.ToInt32(dTableBase.Rows[0]["donum"]);//
                wbrwzs = Convert.ToInt32(dTableBase.Rows[0]["wbTotalNum"]);
                wbwcsl = Convert.ToInt32(dTableBase.Rows[0]["wbDoNum"]);
            }
            if (rwzs != 0 && wcsl != 0)
            {
                rwwcl = wcsl / (decimal)rwzs;
                rwwcl = Math.Round(rwwcl * 100, 2);
            }

            Ret = Ret + ",\"xj\":" + rwwcl.ToString();
            #region  维保任务

            decimal wbrwwcl = 0.00M;
            if (wbrwzs != 0 && wbwcsl != 0)
            {
                wbrwwcl = wbwcsl / (decimal)wbrwzs;
                wbrwwcl = Math.Round(wbrwwcl * 100, 2);
            }
            #endregion


            #endregion

            #region  维保完成率

            if (wbrwzs != 0 && wbwcsl != 0)
            {
                wbrwwcl = wbwcsl / (decimal)wbrwzs;
                wbrwwcl = Math.Round(wbrwwcl, 2);
            }
            Ret = Ret + ",\"wb\":" + wbrwwcl.ToString();
            #endregion
            Ret += "}]}";
            return Ret;
            #endregion
        }

        #region 根据datatable获得列名   
        /// <summary>
        /// 根据datatable获得列名
        /// </summary>
        /// <param name="dt">表对象</param>
        /// <returns>返回结果的数据列数组</returns>
        public static string[] GetColumnsByDataTable(DataTable dt)
        {
            string[] strColumns = null;
            if (dt.Columns.Count > 0)
            {
                int columnNum = 0;
                columnNum = dt.Columns.Count;
                strColumns = new string[columnNum];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strColumns[i] = dt.Columns[i].ColumnName;
                }
            }
            return strColumns;
        }
        #endregion

        private string GetDeviceDynamic(DataRow Row)
        {
            //
            string StrSql = "";

            DataTable Logins = DbHelperSQL.Query("SELECT RoleCode FROM TB_SYS_USERROLE WHERE USERCODE = '" + Global_Var.LoginUserCode + "'").Tables[0];
            if (Logins.Rows.Count > 0)
            {
                string roles = "";
                for (int i = 0; i < Logins.Rows.Count; i++)
                {
                    roles += Logins.Rows[i]["RoleCode"] + ",";
                }
                if (roles != "")
                {
                    Global_Var.LoginRoles = roles.Substring(0, roles.Length - 1);
                }
            }
            string begindate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01 00:00:00";
            string enddate = Convert.ToDateTime(begindate).AddMonths(1).AddDays(-1).ToShortDateString() + " 23:59:59";

            //本月总任务数
            int zs = (int)DbHelperSQL.GetSingle(@" SELECT COUNT(1) FROM dbo.Tb_EQ_Equipment WHERE  ISNULL(IsDelete, 0) = 0    ");

            // AND b.BeginTime >= '" + AppGlobal.StrToStr(Row["BeginTimeStart"].ToString().Trim()) + "'

            StrSql += "1、当前设备总数" + zs;

            int zcs = (int)DbHelperSQL.GetSingle(@"SELECT COUNT(1) FROM dbo.Tb_EQ_Equipment WHERE  ISNULL(IsDelete, 0) = 0   AND Statue = '正常' ");

            double wcl = 0.00;
            if (zcs != 0 && zs != 0)
            {
                wcl = zcs / (double)zs;
                wcl = Math.Round(wcl * 100, 2);
            }
            StrSql += "，设备完好率" + wcl + "%";

            DataTable dTableBase = new DataTable();
            SqlParameter[] parametersTxt = {
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@LoginRoles", SqlDbType.NVarChar,500)
                                        };
            parametersTxt[0].Value = begindate;
            parametersTxt[1].Value = enddate;
            parametersTxt[2].Value = Global_Var.LoginRoles;
            dTableBase = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_Tb_EQ_Task_Show_Statistics", parametersTxt, "RetDataSet").Tables[0];
            int rwzs = 0, wcsl = 0, wbrwzs = 0, wbwcsl = 0;
            decimal baoshinum = 0, equinum = 0, wanchengnum = 0, anqiwanchengnum = 0, wbbaoshinum = 0, wbequinum = 0, wbwanchengnum = 0, wbanqiwanchengnum = 0;
            decimal rwwcl = 0.00M, bxfsl = 0.00M, clwjl = 0.00M, cljsl = 0.00M, wbbxfsl = 0.00M, wbclwjl = 0.00M, wbcljsl = 0.00M;

            if (dTableBase.Rows.Count > 0)
            {
                rwzs = Convert.ToInt32(dTableBase.Rows[0]["totalnum"]);
                wcsl = Convert.ToInt32(dTableBase.Rows[0]["donum"]);//
                wbrwzs = Convert.ToInt32(dTableBase.Rows[0]["wbTotalNum"]);
                wbwcsl = Convert.ToInt32(dTableBase.Rows[0]["wbDoNum"]);

                //   baoshinum = Convert.ToDecimal(dTableBase.Rows[0]["baoshinum"]);
                equinum = Convert.ToDecimal(dTableBase.Rows[0]["equinum"]);
                wanchengnum = Convert.ToDecimal(dTableBase.Rows[0]["wanchengnum"]);
                anqiwanchengnum = Convert.ToDecimal(dTableBase.Rows[0]["anqiwanchengnum"]);
                /////
                wbbaoshinum = Convert.ToDecimal(dTableBase.Rows[0]["wbbaoshinum"]);
                wbequinum = Convert.ToDecimal(dTableBase.Rows[0]["wbequinum"]);
                wbwanchengnum = Convert.ToDecimal(dTableBase.Rows[0]["wbwanchengnum"]);
                wbanqiwanchengnum = Convert.ToDecimal(dTableBase.Rows[0]["wbanqiwanchengnum"]);
            }
            if (baoshinum != 0)
            {
                if (equinum != 0)
                {
                    bxfsl = baoshinum / equinum;
                    bxfsl = Math.Round(bxfsl * 100, 2);
                }
                if (wanchengnum != 0)
                {
                    clwjl = wanchengnum / baoshinum;
                    clwjl = Math.Round(clwjl * 100, 2);
                }
                if (anqiwanchengnum != 0)
                {
                    cljsl = anqiwanchengnum / baoshinum;
                    cljsl = Math.Round(cljsl * 100, 2);
                }
            }
            if (wbbaoshinum != 0)
            {
                if (wbequinum != 0)
                {
                    wbbxfsl = wbbaoshinum / wbequinum;
                    wbbxfsl = Math.Round(wbbxfsl * 100, 2);
                }
                if (wbwanchengnum != 0)
                {
                    wbclwjl = wbwanchengnum / wbbaoshinum;
                    wbclwjl = Math.Round(wbclwjl * 100, 2);
                }
                if (wbanqiwanchengnum != 0)
                {
                    wbcljsl = wbanqiwanchengnum / wbbaoshinum;
                    wbcljsl = Math.Round(wbcljsl * 100, 2);
                }
            }
            if (rwzs != 0 && wcsl != 0)
            {
                rwwcl = wcsl / (decimal)rwzs;
                rwwcl = Math.Round(rwwcl * 100, 2);
            }
            #region  维保任务

            decimal wbrwwcl = 0.00M;
            if (wbrwzs != 0 && wbwcsl != 0)
            {
                wbrwwcl = wbwcsl / (decimal)wbrwzs;
                wbrwwcl = Math.Round(wbrwwcl * 100, 2);
            }
            #endregion
            StrSql += @"<br/>2、本月设备巡查任务数" + rwzs + @"条，完成任务数" + wcsl + @"条；任务完成率" + rwwcl + @"%，报修发生率" + bxfsl + @"%，处理完结率" + clwjl + @"%，处理及时率" + cljsl + @"%。
<br/>3、本月设备维保任务数" + wbrwzs + @"条，完成任务数" + wbwcsl + @"条；任务完成率" + wbrwwcl + @"%，报修发生率" + wbbxfsl + @"%，处理完结率" + wbclwjl + @"%，处理及时率" + wbcljsl + @"%。";

            return JSONHelper.FromString(true, StrSql);
        }

        private string GetQualityDynamicChart(DataRow Row)
        {
            string rs = "";
            StringBuilder strWhere = new StringBuilder();

            DataTable dTable = new DataTable();

            SqlParameter[] parameters = {
                    new SqlParameter("@strWhere", SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = strWhere.ToString();
            dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Tb_Qm_TaskAbarbeitung_ProblemDistribution_Statistics", parameters, "RetDataSet").Tables[0];
            if (dTable != null && dTable.Rows.Count > 0)
                rs = JSONHelper.FromString(dTable.Rows[0]);
            else
                rs = JSONHelper.FromString(true, "");
            return rs;
        }

        private string GetQualityDynamic(DataRow Row)
        {
            #region 文本统计

            string StrSql = "";
            DataTable Logins = DbHelperSQL.Query("SELECT RoleCode FROM TB_SYS_USERROLE WHERE USERCODE = '" + Global_Var.LoginUserCode + "'").Tables[0];
            if (Logins.Rows.Count > 0)
            {
                string roles = "";
                for (int i = 0; i < Logins.Rows.Count; i++)
                {
                    roles += Logins.Rows[i]["RoleCode"] + ",";
                }
                if (roles != "")
                {
                    Global_Var.LoginRoles = roles.Substring(0, roles.Length - 1);
                }
            }
            string begindate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-01 00:00:00";
            string enddate = Convert.ToDateTime(begindate).AddMonths(1).AddDays(-1).ToShortDateString() + " 23:59:59";
            DataTable dTableBase = new DataTable();
            SqlParameter[] parametersTxt = {
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@LoginUser", SqlDbType.NVarChar,72),
                    new SqlParameter("@LoginRoles", SqlDbType.NVarChar,500)
                                        };
            parametersTxt[0].Value = begindate;
            parametersTxt[1].Value = enddate;
            parametersTxt[2].Value = Global_Var.UserCode;
            parametersTxt[3].Value = Global_Var.LoginRoles;
            dTableBase = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_Quality_Txt_Statistics", parametersTxt, "RetDataSet").Tables[0];
            int zs = 0, ywc = 0, cjwts = 0, cfkf = 0, fchgs = 0;
            if (dTableBase != null && dTableBase.Rows.Count > 0)
            {
                string[] strBase = dTableBase.Rows[0][0].ToString().Split(',');
                zs = Convert.ToInt32(strBase[0]);
                ywc = Convert.ToInt32(strBase[1]);
                cjwts = Convert.ToInt32(strBase[2]);
                cfkf = Convert.ToInt32(strBase[3]);
                fchgs = Convert.ToInt32(strBase[4]);
            }
            //本月总任务数

            StrSql += "1、本月品质核查任务数" + zs + "条";

            StrSql += "，完成任务数" + ywc + "条";

            StrSql += "，查见问题数" + cjwts + "条";

            double wcl = 0.00;
            if (ywc != 0 && zs != 0)
            {
                wcl = ywc / (double)zs;
                wcl = Math.Round(wcl, 2);
            }
            StrSql += "，任务完成率" + wcl + "%";

            double wtfxl = 0.00;
            if (cjwts != 0 && zs != 0)
            {
                wtfxl = cjwts / (double)zs;
                wtfxl = Math.Round(wtfxl, 2);
            }
            StrSql += "，问题发现率" + wtfxl + "%";

            double cfkfl = 0.00;
            if (cfkf != 0 && cjwts != 0)
            {
                cfkfl = cfkf / (double)cjwts;
                cfkfl = Math.Round(cfkfl, 2);
            }
            StrSql += "，处罚扣分率" + cfkfl + "%";
            double fchgl = 0.00;
            if (fchgs != 0 && cjwts != 0)
            {
                fchgl = fchgs / (double)cjwts;
                fchgl = Math.Round(fchgl, 2);
            }
            StrSql += "，复查合格率" + fchgl + "%";
            string str = "<br/>2、本月查见问题数" + cjwts + "条:";
            SqlParameter[] parametersTxtDetail = {
                    new SqlParameter("@BeginDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@LoginUser", SqlDbType.NVarChar,72),
                    new SqlParameter("@LoginRoles", SqlDbType.NVarChar,500)
                                        };
            parametersTxt[0].Value = begindate;
            parametersTxt[1].Value = enddate;

            parametersTxt[2].Value = Global_Var.UserCode;
            parametersTxt[3].Value = Global_Var.LoginRoles;
            dTableBase = new DbHelperSQLP(PubConstant.hmWyglConnectionString).RunProcedure("Proc_Tb_Qm_TaskAbarbeitung_ProblemTotal_Statistics", parametersTxtDetail, "RetDataSet").Tables[0];
            string sTxt = "";
            for (int i = 0; i < dTableBase.Rows.Count; i++)
            {
                #region 文本
                double t1 = Convert.ToDouble(dTableBase.Rows.Count) / cjwts;
                sTxt += "," + dTableBase.Rows[i]["ProfessionalName"] + "专业" + dTableBase.Rows[i]["ProblemTotal"].ToString() + "条,占" + Math.Round(t1, 2) + "%;";
                #endregion
            }
            if (sTxt.Length > 0)
            {
                sTxt = sTxt.Substring(1, sTxt.Length - 1);
                str += "其中：" + sTxt;
            }
            //ServiceInfo.InnerHtml = StrSql + "<br />" + str;
            #endregion

            return JSONHelper.FromString(true, StrSql + " \n" + str);
        }

        private string GetHRDynamicChart(DataRow Row)
        {
            ////获取登录人员岗位列表
            ////////////////////////
            string strColumn = "", strColumnValue = "", strRylbContainerData = "[";
            StringBuilder StrSql = new StringBuilder();
            string strPro = "Proc_Tb_Pm_PersonList_PersonType_Statistics";//机构统计
            StrSql.Append("  LEFT JOIN (SELECT CRoleCode FROM Tb_Sys_RoleRolePope WHERE CHARINDEX(RoleCode,'" + Global_Var.LoginRoles.Replace("'", "").ToString() + "')>0) AS A ON View_Tb_Pm_PersonList_Filter.RoleCode = A.CRoleCode ");
            StrSql.Append(" WHERE 1=1  and ISNULL(PersonType,'')!=''  AND ISNULL(IsDelete,0)=0 ");
            // StrSql.Append(" AND (RoleCode IN() OR ISNULL(RoleCode,'')='') ");//权限
            StringBuilder StrSql1 = new StringBuilder();
            StrSql1.Append("  LEFT JOIN (SELECT CRoleCode FROM Tb_Sys_RoleRolePope WHERE CHARINDEX(RoleCode,'" + Global_Var.LoginRoles.Replace("'", "").ToString() + "')>0) AS B ON A.RoleCode = B.CRoleCode ");
            StrSql1.Append(" WHERE 1=1  and ISNULL(PersonType,'')!=''  AND ISNULL(IsDelete,0)=0 ");
            DataTable dtColumn = new Mobile.BLL.Pm.Bll_Tb_Pm_Dictionary().GetList("DType='人员类别' AND ISNULL(IsDelete,'0')=0 ").Tables[0];
            if (dtColumn.Rows.Count > 0)
            {
                for (int i = 0; i < dtColumn.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strColumn += " '人员类别'=[人员类别]";
                        strColumnValue += " '人员类别'=99999 ";
                    }
                    strColumn += ",'" + dtColumn.Rows[i]["Name"].ToString().Trim() + "'=[" + dtColumn.Rows[i]["Name"].ToString().Trim() + "]";
                    strColumnValue += ",'" + dtColumn.Rows[i]["Name"].ToString().Trim() + "'= (select COUNT(*) from #View_Tb_Pm_PersonList_Filter where  PersonType='" + dtColumn.Rows[i]["Id"].ToString().Trim() + "') ";

                }
                DataTable dt = getRylbtjListProcedure(StrSql.ToString(), StrSql1.ToString(), strColumn, strColumnValue, strPro).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    decimal downTotal = 0;
                    decimal topEnd = 0;
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        downTotal += Convert.ToDecimal(dt.Rows[0][dt.Columns[i].ColumnName]);
                    }
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        if (downTotal > 0)
                            topEnd = (Convert.ToDecimal(dt.Rows[0][dt.Columns[i].ColumnName]) / downTotal) * 100;
                        strRylbContainerData = strRylbContainerData + "{name: '" + dt.Columns[i].ColumnName + "',count:" + Convert.ToDecimal(dt.Rows[0][dt.Columns[i].ColumnName]) + "},";

                    }
                    if (strRylbContainerData.Length > 1)
                        strRylbContainerData = strRylbContainerData.Substring(0, strRylbContainerData.Length - 1) + "]";
                    else
                        strRylbContainerData = strRylbContainerData + "]";
                }
                else
                {
                    strRylbContainerData = strRylbContainerData + "]";
                }
            }
            else
            {
                strRylbContainerData = strRylbContainerData + "]";
            }
            return JSONHelper.FromString(true, strRylbContainerData);
            //////////////////////////
        }

        public DataSet getRylbtjListProcedure(string strWhere, string strWhere1, string strColumn, string strColumnValue, string strProcedure, string connString = null)
        {
            DbHelperSQL.ConnectionString = Global_Var.LoginSQLConnStr.ToString();
            SqlParameter[] parameters = {
                    new SqlParameter("@strWhere", SqlDbType.VarChar, 5000),
                    new SqlParameter("@strWhere1", SqlDbType.VarChar, 5000),
                    new SqlParameter("@strColumn", SqlDbType.VarChar, 5000),
                    new SqlParameter("@strColumnValue", SqlDbType.VarChar, 10000)
                   };
            parameters[0].Value = strWhere;
            parameters[1].Value = strWhere1;
            parameters[2].Value = strColumn;
            parameters[3].Value = strColumnValue;
            DataSet Ds = new DbHelperSQLP(connString ?? PubConstant.hmWyglConnectionString).RunProcedure(strProcedure, parameters, "RetDataSet");
            return Ds;
        }

        private string GetHRDynamic(DataRow Row)
        {
            GetRoles(Row["UserCode"].ToString(), Row["OrganCorpCode"].ToString(), Row["CommID"].ToString());
            string Mark = GetCustomerInfo_New(Global_Var.LoginRoles);
            return Mark;
        }

        private void GetRoles(string UserCode, string OrganCode, string CommID)
        {
            string strUserRoles = "";

            MobileSoft.BLL.Sys.Bll_Tb_Sys_User Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_User();

            DataTable dTable = Bll.FilterRoles(UserCode, OrganCode, AppGlobal.StrToInt(CommID));

            if (dTable.Rows.Count > 0)
            {
                strUserRoles = dTable.Rows[0][0].ToString();
            }

            if (strUserRoles.Trim() == "")
            {
                dTable = Bll.FilterRoles(UserCode, OrganCode, 0);
                if (dTable.Rows.Count > 0)
                {
                    strUserRoles = dTable.Rows[0][0].ToString();
                }
            }

            Global_Var.LoginRoles = strUserRoles.ToString().Replace("'", "").ToString();
        }

        private string GetMaterialDynamic()
        {
            StringBuilder Sb = new StringBuilder();

            SqlParameter[] parameters_1 = {
                   };
            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Mt_StatDesktopInfo", parameters_1, "RetDataSet").Tables[0];
            if (dTable.Rows.Count > 0)
            {
                DataRow Row = dTable.Rows[0];
                Sb.Append("1、本月计划采购数量" + Row["本月计划采购数量"].ToString() + "，计划采购金额" + Row["本月计划采购金额"].ToString() + "元，实际入库数量" + Row["实际入库数量"].ToString() + "，实际入库金额" + Row["实际入库金额"].ToString() + "元，计划采购完成率" + Row["计划采购完成率"].ToString());
                Sb.Append("<br/>2、本月非计划采购数量" + Row["本月非计划采购数量"].ToString() + "，非计划采购金额" + Row["本月非计划采购金额"].ToString() + "，非计划采购占比" + Row["非计划采购占比"].ToString() + "。");
                Sb.Append("<br/>3、安全库存数量" + Row["安全库存数量"].ToString() + "，当前库存数量" + Row["当前库存数量"].ToString() + "，当前库存金额" + Row["当前库存金额"].ToString() + "元。");
            }
            return JSONHelper.FromString(true, Sb.ToString());
        }

        private string GetMaterialDynamicChart()
        {
            SqlParameter[] parameters = {
                   };
            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Mt_StatDesktop", parameters, "RetDataSet").Tables[0];
            return JSONHelper.FromString(dTable);
        }

        #region 获取房屋状态图形数据
        #region 房屋状态查询
        public DataTable Sys_TakePicRoomState_Filter(string SQLEx)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SQLEx", SQLEx);
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            DataTable dTable = con.ExecuteReader("Proc_Sys_TakePicRoomState_Filter", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            return dTable;
        }

        #endregion
        private String GetResoureDynamicChart(DataRow row)
        {
            Dictionary<string, int> dicRoomStateRate = new Dictionary<string, int>();
            string strSQLOrgan2 = " and OrganCode like '" + Global_Var.LoginOrganCode + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 2 ";
            DataTable dTableOrgan2 = Sys_TakePicRoomState_Filter(strSQLOrgan2);
            foreach (DataRow DRow in dTableOrgan2.Rows)
            {
                string strStateName = DRow["StateName"].ToString();
                string strRoomStateTemp = DRow["RoomState"].ToString();
                if (strStateName == "")
                {
                    strStateName = "[无]";
                }
                if (strRoomStateTemp != "999")
                {
                    if (dicRoomStateRate.ContainsKey(strStateName))
                    {
                        dicRoomStateRate[strStateName] = dicRoomStateRate[strStateName] + AppGlobal.StrToInt(DRow["Counts"].ToString());
                    }
                    else
                    {
                        dicRoomStateRate.Add(strStateName, AppGlobal.StrToInt(DRow["Counts"].ToString()));
                    }
                }
            }
            string strRoomStateRate = "[";
            int totalCount = 0;
            foreach (var item in dicRoomStateRate.OrderByDescending(jj => jj.Value))
            {
                totalCount = totalCount + item.Value;
                strRoomStateRate = strRoomStateRate + "{\"StateName\":\"" + item.Key + "\",\"Counts\":" + item.Value + "},";
            }
            strRoomStateRate = strRoomStateRate + "{\"StateName\":\"合计\",\"Counts\":" + totalCount + "},";
            strRoomStateRate = strRoomStateRate.Substring(0, strRoomStateRate.Length - 1) + "]";
            //wydtData.Value = strRoomStateRate;
            StringBuilder sb = new StringBuilder();

            sb.Append("{\"Result\":\"true\",");

            sb.Append("\"data\":");
            sb.Append(strRoomStateRate);
            sb.Append("}");
            return sb.ToString();

        }

        #endregion
        #region
        public DataTable Sys_TakePic_FilterTopNum(int TopNum, string SQLEx)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TopNum", TopNum);
            parameters.Add("@SQLEx", SQLEx);
            IDbConnection con = new SqlConnection(PubConstant.hmWyglConnectionString);
            DataTable dTable = con.ExecuteReader("Proc_Sys_TakePic_FilterTopNum", parameters, null, null, CommandType.StoredProcedure).ToDataSet().Tables[0];
            return dTable;
        }
        #endregion
        #region 获取收费动态图形数据
        private String GetChargeDynamicChart(DataRow row)
        {
            int iiCommID = AppGlobal.StrToInt(Global_Var.LoginCommID);
            string OrganCode = Global_Var.OrganCode.Trim();
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(" and datediff(day,StatDate,getdate()) = 1 and StatType = 1 ");
            if (row.Table.Columns.Contains("CommID"))
            {
                iiCommID = AppGlobal.StrToInt(row["CommID"].ToString());
                if (iiCommID.ToString().Length >= 6)
                    strSQL.Append(" and CommID = '" + iiCommID + "' ");
            }
            //if (OrganCode == "" && row.Table.Columns.Contains("OrganCode"))
            //{
            //    OrganCode = row["OrganCode"].ToString();
            //    if (iiCommID.ToString().Length >= 6)
            //        strSQL.Append(" and OrganCode = '" + OrganCode + "' ");
            //    else
            //    {
            //        strSQL.Append(" and OrganCode = '" + iiCommID + "' ");
            //    }
            //}
            if (row.Table.Columns.Contains("OrganCode"))
            {
                OrganCode = row["OrganCode"].ToString();
                if (OrganCode != "null")
                    strSQL.Append(" and OrganCode = '" + OrganCode + "' ");
                //if (iiCommID.ToString().Length >= 6)
                //    strSQL.Append(" and OrganCode = '" + OrganCode + "' ");
                //else
                //{
                //    strSQL.Append(" and OrganCode = '" + iiCommID + "' ");
                //}
            }
            else
            {
                if (row.Table.Columns.Contains("OrganCorpCode"))
                {
                    OrganCode = row["OrganCorpCode"].ToString();
                    if (OrganCode != "null")
                        strSQL.Append(" and OrganCode = '" + OrganCode + "' ");
                    //if (iiCommID.ToString().Length >= 6)
                    //    strSQL.Append(" and OrganCode = '" + OrganCode + "' ");
                    //else
                    //{
                    //    strSQL.Append(" and OrganCode = '" + iiCommID + "' ");
                    //}
                }
            }
            //
            DataTable dTable = Sys_TakePic_FilterTopNum(1, strSQL.ToString());

            #region 增加序号列
            ////*增加序号列
            //DataColumn SerialCol = new DataColumn("SerialNum", typeof(Int32));
            //dTable.Columns.Add(SerialCol);

            #endregion

            int i = 1;
            string strwgphbxAxisName = "[";
            //本月物管费
            string strChargeRate1 = "[";
            //本年月物管费
            string strChargeRate2 = "[";

            string strFiles = "[";
            foreach (DataRow DRow in dTable.Rows)
            {
                //DRow["SerialNum"] = i;
                strFiles = strFiles + "{\"CommName\":\"" + DRow["CommName"].ToString() + "\",\"ChargeRate1\":" + DRow["ChargeRate1"] + ",\"ChargeRate2\":" + DRow["ChargeRate2"] + "},";
                //strwgphbxAxisName = strwgphbxAxisName + "'" + DRow["CommName"].ToString() + "'" + ",";
                //strChargeRate1 = strChargeRate1 + DRow["ChargeRate1"].ToString() + ",";
                //strChargeRate2 = strChargeRate2 + DRow["ChargeRate2"].ToString() + ",";
                i++;
            }
            if (strFiles.Length > 1)
                strFiles = strFiles.Substring(0, strFiles.Length - 1) + "]";
            else
                strFiles = strFiles + "]";
            //项目名称
            string CommList = strwgphbxAxisName;
            //本年物管收缴率排行
            string YearChargeData = strChargeRate2;
            //本月物管收缴率排行
            string MonthChargeData = strChargeRate1;

            string Result = CommList + "|" + MonthChargeData + "|" + YearChargeData;

            StringBuilder sb = new StringBuilder();

            sb.Append("{\"Result\":\"true\",");

            sb.Append("\"data\":");
            sb.Append(strFiles);
            sb.Append("}");
            return sb.ToString();
            // return GetMsg(row, 6);
        }
        #endregion

        private string GetServiceDynamic(DataRow row)
        {
            return GetMsg(row, 3);
        }
        private string GetChargeDynamic(DataRow row)
        {
            return GetMsg(row, 2);
        }
        private string GetResoureDynamic(DataRow row)
        {
            return GetMsg(row, 1);
        }

        private string GetStatisticsCount(DataRow row)
        {
            return null;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="type">1是资源动态,2是收费动态</param>
        /// <returns></returns>
        /// 
         #region 获取业务动态
        private String GetMsg(DataRow row, int type)
        {
            String result = "";
            StringBuilder strReText1 = new StringBuilder("");
            StringBuilder strReText1_1 = new StringBuilder("");

            StringBuilder strReText2 = new StringBuilder("");
            StringBuilder strReText2_1 = new StringBuilder("");
            StringBuilder strReText3 = new StringBuilder("");
            StringBuilder strReText4 = new StringBuilder("");

            #region 变量定义
            int iCommID = AppGlobal.StrToInt(Global_Var.LoginCommID);
            string strCommName = AppGlobal.GetCommName(iCommID);
            string strOrganName = AppGlobal.GetOrganName4(Global_Var.LoginOrganCode);
            string strDate = "";
            string strFullDate = "";
            string strRoomState = "";
            string strOrganRoomState = "";

            string strPark = "";
            string strOrganPark = "";

            string strOrganDate = "";

            string strStateList = "";
            string strOrganStateList = "";

            string strParkList = "";
            string strOrganParkList = "";

            decimal iAllArea = 0;
            int iRoomNum1 = 0;
            int iRoomNum2 = 0;
            int iRoomNum3 = 0;
            int iRoomNum4 = 0;
            int iRoomNum5 = 0;
            int iRoomNum6 = 0;
            decimal iChargeRate1 = 0;
            decimal iChargeRate2 = 0;
            decimal iChargeRate3 = 0;
            int iFeesCreateNum = 0;
            int iFeesCancelNum = 0;

            int iIncidentNum1 = 0;
            int iIncidentNum2 = 0;
            int iIncidentNum3 = 0;
            int iIncidentNum4 = 0;
            int iIncidentNum5 = 0;

            decimal iIncidentRate1 = 0;
            decimal iIncidentRate2 = 0;
            decimal iIncidentRate3 = 0;
            decimal iIncidentRate4 = 0;

            decimal iOrganAllArea = 0;
            int iOrganRoomNum1 = 0;
            int iOrganRoomNum2 = 0;
            int iOrganRoomNum3 = 0;
            int iOrganRoomNum4 = 0;
            int iOrganRoomNum5 = 0;
            int iOrganRoomNum6 = 0;
            decimal iOrganChargeRate1 = 0;
            decimal iOrganChargeRate2 = 0;
            decimal iOrganChargeRate3 = 0;
            int iOrganFeesCreateNum = 0;
            int iOrganFeesCancelNum = 0;

            int iOrganIncidentNum1 = 0;
            int iOrganIncidentNum2 = 0;
            int iOrganIncidentNum3 = 0;
            int iOrganIncidentNum4 = 0;
            int iOrganIncidentNum5 = 0;

            decimal iOrganIncidentRate1 = 0;
            decimal iOrganIncidentRate2 = 0;
            decimal iOrganIncidentRate3 = 0;
            decimal iOrganIncidentRate4 = 0;

            int iEquipmentDeviceCount = 0;
            int iEquipmentPeriodHappenNum = 0;
            int iEquipmentPeriodFinlishNum = 0;
            int iEquipmentPeriodUnFinlishNum = 0;
            int iEquipmentTempHappenNum = 0;
            int iEquipmentTempFinlishNum = 0;
            int iEquipmentTempUnFinlishNum = 0;

            int iOrganEquipmentDeviceCount = 0;
            int iOrganEquipmentPeriodHappenNum = 0;
            int iOrganEquipmentPeriodFinlishNum = 0;
            int iOrganEquipmentPeriodUnFinlishNum = 0;
            int iOrganEquipmentTempHappenNum = 0;
            int iOrganEquipmentTempFinlishNum = 0;
            int iOrganEquipmentTempUnFinlishNum = 0;

            #endregion

            if (iCommID != 0)
            {
                #region 社区

                string strSQL = " CommID = " + iCommID.ToString() + " and datediff(day,StatDate,getdate()) = 1 and StatType = 1 ";

                MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic();

                DataTable dTable = Bll.GetList(strSQL).Tables[0];

                strReText2_1.Append(JSONHelper.FromString(dTable));

                if (dTable.Rows.Count > 0)
                {
                    DataRow DRow = dTable.Rows[0];

                    #region 取值
                    iAllArea = AppGlobal.StrToDec(DRow["AllArea"].ToString());
                    iRoomNum1 = AppGlobal.StrToInt(DRow["RoomNum1"].ToString());
                    iRoomNum2 = AppGlobal.StrToInt(DRow["RoomNum2"].ToString());
                    iRoomNum3 = AppGlobal.StrToInt(DRow["RoomNum3"].ToString());
                    iRoomNum4 = AppGlobal.StrToInt(DRow["RoomNum4"].ToString());
                    iRoomNum5 = AppGlobal.StrToInt(DRow["RoomNum5"].ToString());
                    iRoomNum6 = AppGlobal.StrToInt(DRow["RoomNum6"].ToString());

                    iChargeRate1 = AppGlobal.StrToDec(DRow["ChargeRate1"].ToString());
                    iChargeRate2 = AppGlobal.StrToDec(DRow["ChargeRate2"].ToString());
                    iChargeRate3 = AppGlobal.StrToDec(DRow["ChargeRate3"].ToString());

                    iFeesCreateNum = AppGlobal.StrToInt(DRow["FeesCreateNum"].ToString());
                    iFeesCancelNum = AppGlobal.StrToInt(DRow["FeesCancelNum"].ToString());

                    iIncidentNum1 = AppGlobal.StrToInt(DRow["IncidentNum1"].ToString());
                    iIncidentNum2 = AppGlobal.StrToInt(DRow["IncidentNum2"].ToString());
                    iIncidentNum3 = AppGlobal.StrToInt(DRow["IncidentNum3"].ToString());
                    iIncidentNum4 = AppGlobal.StrToInt(DRow["IncidentNum4"].ToString());
                    iIncidentNum5 = AppGlobal.StrToInt(DRow["IncidentNum5"].ToString());

                    iIncidentRate1 = AppGlobal.StrToDec(DRow["IncidentRate1"].ToString());
                    iIncidentRate2 = AppGlobal.StrToDec(DRow["IncidentRate2"].ToString());
                    iIncidentRate3 = AppGlobal.StrToDec(DRow["IncidentRate3"].ToString());
                    iIncidentRate4 = AppGlobal.StrToDec(DRow["IncidentRate4"].ToString());

                    strDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日");

                    strFullDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日 HH:mm:ss");

                    iEquipmentDeviceCount = AppGlobal.StrToInt(DRow["EquipmentDeviceCount"].ToString());
                    iEquipmentPeriodHappenNum = AppGlobal.StrToInt(DRow["EquipmentPeriodHappenNum"].ToString());
                    iEquipmentPeriodFinlishNum = AppGlobal.StrToInt(DRow["EquipmentPeriodFinlishNum"].ToString());
                    iEquipmentPeriodUnFinlishNum = AppGlobal.StrToInt(DRow["EquipmentPeriodUnFinlishNum"].ToString());
                    iEquipmentTempHappenNum = AppGlobal.StrToInt(DRow["EquipmentTempHappenNum"].ToString());
                    iEquipmentTempFinlishNum = AppGlobal.StrToInt(DRow["EquipmentTempFinlishNum"].ToString());
                    iEquipmentTempUnFinlishNum = AppGlobal.StrToInt(DRow["EquipmentTempUnFinlishNum"].ToString());

                    #endregion
                }

                dTable.Dispose();
                #endregion

                #region 房屋状态
                string strSQL2 = " CommID = " + iCommID.ToString() + " and datediff(day,StatDate,getdate()) = 1 and StatType = 1 ";

                MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState BllState = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState();

                DataTable dTable2 = BllState.GetList(strSQL2).Tables[0];

                strReText1_1.Append(JSONHelper.FromString(dTable2));

                if (dTable2.Rows.Count > 0)
                {
                    strRoomState = "，其中";
                }
                else
                {
                    strRoomState = "。";
                }

                foreach (DataRow DRow in dTable2.Rows)
                {
                    string strStateName = DRow["StateName"].ToString();
                    if (strStateName == "")
                    {
                        strStateName = "[无]";
                    }

                    if (strStateList != "")
                    {
                        strStateList = strStateList + "，" + strStateName + "" + DRow["Counts"].ToString() + "" + "套";
                    }
                    else
                    {
                        strStateList = strStateName + "" + DRow["Counts"].ToString() + "" + "套";
                    }
                }

                if (strStateList != "")
                {
                    strRoomState = strRoomState + strStateList + "。";
                }

                dTable2.Dispose();
                #endregion

                #region 车位数
                string strSQL3 = " CommID = " + iCommID.ToString() + " and datediff(day,StatDate,getdate()) = 1 and StatType = 1 ";

                MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicPark BllPark = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicPark();

                DataTable dTable3 = BllPark.GetList(strSQL3).Tables[0];

                if (dTable3.Rows.Count > 0)
                {
                    strPark = "车位：";
                }
                else
                {
                    strPark = "无车位信息";
                }


                foreach (DataRow DRow in dTable3.Rows)
                {
                    string strParkTypeName = DRow["ParkTypeName"].ToString();

                    if (strParkTypeName == "")
                    {
                        strParkTypeName = "[无]";
                    }

                    if (strParkList != "")
                    {
                        strParkList = strParkList + "，" + strParkTypeName + "" + DRow["Counts"].ToString() + "" + "个";
                    }
                    else
                    {
                        strParkList = strParkTypeName + "" + DRow["Counts"].ToString() + "" + "个";
                    }

                }

                if (strParkList != "")
                {
                    strPark = strPark + strParkList + "。";
                }


                dTable3.Dispose();
                #endregion

                #region 设置社区

                //物业动态
                strReText1.Append("【" + strCommName + "】\n");
                strReText1.Append("1、截止到" + strDate + "，可收物管费面积共" + iAllArea.ToString() + "平方米" + strRoomState + "\n2、截止到" + strDate + "，" + strPark + "\n");
                strReText1.Append("3、" + strDate + "共有" + iRoomNum4.ToString() + "套办理转让，" + iRoomNum5.ToString() + "套办理租赁，" + iRoomNum6.ToString() + "套车位办理转让。");


                //收费动态
                strReText2.Append("【" + strCommName + "】\n");
                strReText2.Append("1、截止到" + strDate + "，物管费本月收费率" + iChargeRate1.ToString("N2") + "%，本年累计收费率" + iChargeRate2.ToString("N2") + "%，以前年度欠费回收率" + iChargeRate3.ToString("N2") + "%。" + "\n");
                strReText2.Append("2、" + strDate + "，共有" + iFeesCreateNum.ToString() + "笔审核费用入账，" + iFeesCancelNum.ToString() + "笔费用撤销。");

                //客服动态
                strReText3.Append("【" + strCommName + "】\n");
                strReText3.Append("1、截止到" + strDate + "，未完结的派工单" + iIncidentNum1.ToString() + "件（其中逾期" + iIncidentNum2.ToString() + "件），超过60天未完结的协调单" + iIncidentNum3.ToString() + "件；本月派工单完结率" + iIncidentRate1.ToString("N2") + "%，本年累计" + iIncidentRate2.ToString("N2") + "%；本月协调单完结率" + iIncidentRate3.ToString("N2") + "%，本年累计" + iIncidentRate4.ToString("N2") + "%。\n");
                strReText3.Append("2、" + strDate + "，共有" + iIncidentNum4.ToString() + "件报事，其中" + iIncidentNum5.ToString() + "件未完结；\n");

                #endregion


                //_tempP += "超时预警：已受理未分派超时工单<a href='Incident/IncidentAssignedBrowse.aspx' style='color:red'>" + dTable11.Rows[0][0] + "</a>件,已分派未处理超时工单<a href='Incident/IncidentProcessingBrowse.aspx' style='color:red'>" + dTable22.Rows[0][0] + "</a>件,已处理未回访超时工单<a href='Incident/IncidentReplyBrowse.aspx' style='color:red'>" + dTable33.Rows[0][0] + "</a>件；";
                DataTable dTable11 = HSPR_IncidentWarningCount1_Filter(row["UserCode"].ToString(), iCommID);

                string _tempP = "3、超时预警：已受理未分派超时工单" + dTable11.Rows[0][0] + "件,已分派未处理超时工单" + dTable11.Rows[0][1] + "件,已处理未回访超时工单" + dTable11.Rows[0][2] + "件；";


                strReText3.Append(_tempP);

            }
            else
            {
                if (Global_Var.LoginOrganCode == "" || Global_Var.LoginOrganCode == "01")
                {
                    #region 集团
                    string strSQLOrgan = " datediff(day,StatDate,getdate()) = 1 and StatType = 3 ";

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic();

                    DataTable dTableOrgan = Bll.GetList(strSQLOrgan).Tables[0];

                    strReText2_1.Append(JSONHelper.FromString(dTableOrgan));
                    if (dTableOrgan.Rows.Count > 0)
                    {
                        DataRow DRow = dTableOrgan.Rows[0];

                        #region 取值

                        iOrganAllArea = AppGlobal.StrToDec(DRow["AllArea"].ToString());
                        iOrganRoomNum1 = AppGlobal.StrToInt(DRow["RoomNum1"].ToString());
                        iOrganRoomNum2 = AppGlobal.StrToInt(DRow["RoomNum2"].ToString());
                        iOrganRoomNum3 = AppGlobal.StrToInt(DRow["RoomNum3"].ToString());
                        iOrganRoomNum4 = AppGlobal.StrToInt(DRow["RoomNum4"].ToString());
                        iOrganRoomNum5 = AppGlobal.StrToInt(DRow["RoomNum5"].ToString());
                        iOrganRoomNum6 = AppGlobal.StrToInt(DRow["RoomNum6"].ToString());

                        iOrganChargeRate1 = AppGlobal.StrToDec(DRow["ChargeRate1"].ToString());
                        iOrganChargeRate2 = AppGlobal.StrToDec(DRow["ChargeRate2"].ToString());
                        iOrganChargeRate3 = AppGlobal.StrToDec(DRow["ChargeRate3"].ToString());

                        iOrganFeesCreateNum = AppGlobal.StrToInt(DRow["FeesCreateNum"].ToString());
                        iOrganFeesCancelNum = AppGlobal.StrToInt(DRow["FeesCancelNum"].ToString());

                        iOrganIncidentNum1 = AppGlobal.StrToInt(DRow["IncidentNum1"].ToString());
                        iOrganIncidentNum2 = AppGlobal.StrToInt(DRow["IncidentNum2"].ToString());
                        iOrganIncidentNum3 = AppGlobal.StrToInt(DRow["IncidentNum3"].ToString());
                        iOrganIncidentNum4 = AppGlobal.StrToInt(DRow["IncidentNum4"].ToString());
                        iOrganIncidentNum5 = AppGlobal.StrToInt(DRow["IncidentNum5"].ToString());

                        iOrganIncidentRate1 = AppGlobal.StrToDec(DRow["IncidentRate1"].ToString());
                        iOrganIncidentRate2 = AppGlobal.StrToDec(DRow["IncidentRate2"].ToString());
                        iOrganIncidentRate3 = AppGlobal.StrToDec(DRow["IncidentRate3"].ToString());
                        iOrganIncidentRate4 = AppGlobal.StrToDec(DRow["IncidentRate4"].ToString());

                        strOrganDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日");

                        strFullDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日 HH:mm:ss");

                        iOrganEquipmentDeviceCount = AppGlobal.StrToInt(DRow["EquipmentDeviceCount"].ToString());
                        iOrganEquipmentPeriodHappenNum = AppGlobal.StrToInt(DRow["EquipmentPeriodHappenNum"].ToString());
                        iOrganEquipmentPeriodFinlishNum = AppGlobal.StrToInt(DRow["EquipmentPeriodFinlishNum"].ToString());
                        iOrganEquipmentPeriodUnFinlishNum = AppGlobal.StrToInt(DRow["EquipmentPeriodUnFinlishNum"].ToString());
                        iOrganEquipmentTempHappenNum = AppGlobal.StrToInt(DRow["EquipmentTempHappenNum"].ToString());
                        iOrganEquipmentTempFinlishNum = AppGlobal.StrToInt(DRow["EquipmentTempFinlishNum"].ToString());
                        iOrganEquipmentTempUnFinlishNum = AppGlobal.StrToInt(DRow["EquipmentTempUnFinlishNum"].ToString());

                        #endregion
                    }

                    dTableOrgan.Dispose();
                    #endregion

                    #region 房屋状态(集团)
                    string strSQLOrgan2 = " OrganCode like '" + Global_Var.LoginOrganCode + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 3 ";

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState BllState = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState();

                    DataTable dTableOrgan2 = BllState.GetList(strSQLOrgan2).Tables[0];

                    strReText1_1.Append(JSONHelper.FromString(dTableOrgan2));

                    if (dTableOrgan2.Rows.Count > 0)
                    {
                        strOrganRoomState = "，其中";
                    }
                    else
                    {
                        strOrganRoomState = "。";
                    }


                    foreach (DataRow DRow in dTableOrgan2.Rows)
                    {
                        string strStateName = DRow["StateName"].ToString();
                        if (strStateName == "")
                        {
                            strStateName = "[无]";
                        }

                        if (strOrganStateList != "")
                        {
                            strOrganStateList = strOrganStateList + "，" + strStateName + "" + DRow["Counts"].ToString() + "" + "套";
                        }
                        else
                        {
                            strOrganStateList = strStateName + "" + DRow["Counts"].ToString() + "" + "套";
                        }

                    }

                    if (strOrganStateList != "")
                    {
                        strOrganRoomState = strOrganRoomState + strOrganStateList + "。";
                    }

                    dTableOrgan2.Dispose();
                    #endregion

                    #region 车位数(集团)
                    string strSQLOrgan3 = " OrganCode like '" + Global_Var.LoginOrganCode + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 3 ";

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicPark BllPark = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicPark();

                    DataTable dTableOrgan3 = BllPark.GetList(strSQLOrgan3).Tables[0];

                    if (dTableOrgan3.Rows.Count > 0)
                    {
                        strOrganPark = "车位：";
                    }
                    else
                    {
                        strOrganPark = "无车位信息";
                    }


                    foreach (DataRow DRow in dTableOrgan3.Rows)
                    {
                        string strParkTypeName = DRow["ParkTypeName"].ToString();
                        if (strParkTypeName == "")
                        {
                            strParkTypeName = "[无]";
                        }

                        if (strOrganParkList != "")
                        {
                            strOrganParkList = strOrganParkList + "，" + strParkTypeName + "" + DRow["Counts"].ToString() + "" + "个";
                        }
                        else
                        {
                            strOrganParkList = strParkTypeName + "" + DRow["Counts"].ToString() + "" + "个";
                        }

                    }

                    if (strOrganParkList != "")
                    {
                        strOrganPark = strOrganPark + strOrganParkList + "。";
                    }

                    dTableOrgan3.Dispose();
                    #endregion

                    #region 设置集团
                    strReText1.Append("1、截止到" + strOrganDate + "，可收物管费面积共" + iOrganAllArea.ToString() + "平方米" + strOrganRoomState + "\n2、截止到" + strOrganDate + "，" + strOrganPark);
                    strReText2.Append("截止到" + strOrganDate + "，物管费本月收费率" + iOrganChargeRate1.ToString("N2") + "%，本年累计收费率" + iOrganChargeRate2.ToString("N2") + "%，以前年度欠费回收率" + iOrganChargeRate3.ToString("N2") + "%。");
                    strReText3.Append("1、截止到" + strOrganDate + "，未完结的派工单" + iOrganIncidentNum1.ToString() + "件（其中逾期" + iOrganIncidentNum2.ToString() + "件），超过60天未完结的协调单" + iOrganIncidentNum3.ToString() + "件；\n2、本月派工单完结率" + iOrganIncidentRate1.ToString("N2") + "%，本年累计" + iOrganIncidentRate2.ToString("N2") + "%；本月协调单完结率" + iOrganIncidentRate3.ToString("N2") + "%，本年累计" + iOrganIncidentRate4.ToString("N2") + "%；\n");




                    #endregion


                }
                else
                {
                    #region 地区公司
                    string strSQLOrgan = " OrganCode like '" + Global_Var.LoginOrganCode + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 2 ";

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic();

                    DataTable dTableOrgan = Bll.GetList(strSQLOrgan).Tables[0];

                    strReText2_1.Append(JSONHelper.FromString(dTableOrgan));

                    if (dTableOrgan.Rows.Count > 0)
                    {
                        DataRow DRow = dTableOrgan.Rows[0];

                        #region 取值

                        iOrganAllArea = AppGlobal.StrToDec(DRow["AllArea"].ToString());
                        iOrganRoomNum1 = AppGlobal.StrToInt(DRow["RoomNum1"].ToString());
                        iOrganRoomNum2 = AppGlobal.StrToInt(DRow["RoomNum2"].ToString());
                        iOrganRoomNum3 = AppGlobal.StrToInt(DRow["RoomNum3"].ToString());
                        iOrganRoomNum4 = AppGlobal.StrToInt(DRow["RoomNum4"].ToString());
                        iOrganRoomNum5 = AppGlobal.StrToInt(DRow["RoomNum5"].ToString());
                        iOrganRoomNum6 = AppGlobal.StrToInt(DRow["RoomNum6"].ToString());

                        iOrganChargeRate1 = AppGlobal.StrToDec(DRow["ChargeRate1"].ToString());
                        iOrganChargeRate2 = AppGlobal.StrToDec(DRow["ChargeRate2"].ToString());
                        iOrganChargeRate3 = AppGlobal.StrToDec(DRow["ChargeRate3"].ToString());

                        iOrganFeesCreateNum = AppGlobal.StrToInt(DRow["FeesCreateNum"].ToString());
                        iOrganFeesCancelNum = AppGlobal.StrToInt(DRow["FeesCancelNum"].ToString());

                        iOrganIncidentNum1 = AppGlobal.StrToInt(DRow["IncidentNum1"].ToString());
                        iOrganIncidentNum2 = AppGlobal.StrToInt(DRow["IncidentNum2"].ToString());
                        iOrganIncidentNum3 = AppGlobal.StrToInt(DRow["IncidentNum3"].ToString());
                        iOrganIncidentNum4 = AppGlobal.StrToInt(DRow["IncidentNum4"].ToString());
                        iOrganIncidentNum5 = AppGlobal.StrToInt(DRow["IncidentNum5"].ToString());

                        iOrganIncidentRate1 = AppGlobal.StrToDec(DRow["IncidentRate1"].ToString());
                        iOrganIncidentRate2 = AppGlobal.StrToDec(DRow["IncidentRate2"].ToString());
                        iOrganIncidentRate3 = AppGlobal.StrToDec(DRow["IncidentRate3"].ToString());
                        iOrganIncidentRate4 = AppGlobal.StrToDec(DRow["IncidentRate4"].ToString());

                        strOrganDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日");

                        strFullDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日 HH:mm:ss");

                        iOrganEquipmentDeviceCount = AppGlobal.StrToInt(DRow["EquipmentDeviceCount"].ToString());
                        iOrganEquipmentPeriodHappenNum = AppGlobal.StrToInt(DRow["EquipmentPeriodHappenNum"].ToString());
                        iOrganEquipmentPeriodFinlishNum = AppGlobal.StrToInt(DRow["EquipmentPeriodFinlishNum"].ToString());
                        iOrganEquipmentPeriodUnFinlishNum = AppGlobal.StrToInt(DRow["EquipmentPeriodUnFinlishNum"].ToString());
                        iOrganEquipmentTempHappenNum = AppGlobal.StrToInt(DRow["EquipmentTempHappenNum"].ToString());
                        iOrganEquipmentTempFinlishNum = AppGlobal.StrToInt(DRow["EquipmentTempFinlishNum"].ToString());
                        iOrganEquipmentTempUnFinlishNum = AppGlobal.StrToInt(DRow["EquipmentTempUnFinlishNum"].ToString());

                        #endregion
                    }

                    dTableOrgan.Dispose();
                    #endregion

                    #region 房屋状态(地区公司)
                    string strSQLOrgan2 = " OrganCode like '" + Global_Var.LoginOrganCode + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 2 ";

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState BllState = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState();

                    DataTable dTableOrgan2 = BllState.GetList(strSQLOrgan2).Tables[0];

                    strReText1_1.Append(JSONHelper.FromString(dTableOrgan2));

                    if (dTableOrgan2.Rows.Count > 0)
                    {
                        strOrganRoomState = "，其中";
                    }
                    else
                    {
                        strOrganRoomState = "。";
                    }


                    foreach (DataRow DRow in dTableOrgan2.Rows)
                    {
                        string strStateName = DRow["StateName"].ToString();
                        if (strStateName == "")
                        {
                            strStateName = "[无]";
                        }

                        if (strOrganStateList != "")
                        {
                            strOrganStateList = strOrganStateList + "，" + strStateName + "" + DRow["Counts"].ToString() + "" + "套";
                        }
                        else
                        {
                            strOrganStateList = strStateName + "" + DRow["Counts"].ToString() + "" + "套";
                        }

                    }

                    if (strOrganStateList != "")
                    {
                        strOrganRoomState = strOrganRoomState + strOrganStateList + "。";
                    }

                    dTableOrgan2.Dispose();
                    #endregion

                    #region 车位数(地区公司)
                    string strSQLOrgan3 = " OrganCode like '" + Global_Var.LoginOrganCode + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 2  ";

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicPark BllPark = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicPark();

                    DataTable dTableOrgan3 = BllPark.GetList(strSQLOrgan3).Tables[0];

                    if (dTableOrgan3.Rows.Count > 0)
                    {
                        strOrganPark = "车位：";
                    }
                    else
                    {
                        strOrganPark = "无车位信息";
                    }


                    foreach (DataRow DRow in dTableOrgan3.Rows)
                    {
                        string strParkTypeName = DRow["ParkTypeName"].ToString();
                        if (strParkTypeName == "")
                        {
                            strParkTypeName = "[无]";
                        }

                        if (strOrganParkList != "")
                        {
                            strOrganParkList = strOrganParkList + "，" + strParkTypeName + "" + DRow["Counts"].ToString() + "" + "个";
                        }
                        else
                        {
                            strOrganParkList = strParkTypeName + "" + DRow["Counts"].ToString() + "" + "个";
                        }

                    }

                    if (strOrganParkList != "")
                    {
                        strOrganPark = strOrganPark + strOrganParkList + "。";
                    }

                    dTableOrgan3.Dispose();
                    #endregion

                    #region 设置地区公司
                    strReText1.Append("1、截止到" + strOrganDate + "，可收物管费面积共" + iOrganAllArea.ToString() + "平方米" + strOrganRoomState + "\n2、截止到" + strOrganDate + "，" + strOrganPark);
                    strReText2.Append("截止到" + strOrganDate + "，物管费本月收费率" + iOrganChargeRate1.ToString("N2") + "%，本年累计收费率" + iOrganChargeRate2.ToString("N2") + "%，以前年度欠费回收率" + iOrganChargeRate3.ToString("N2") + "%。");
                    strReText3.Append("1、截止到" + strOrganDate + "，未完结的派工单" + iOrganIncidentNum1.ToString() + "件（其中逾期" + iOrganIncidentNum2.ToString() + "件），超过60天未完结的协调单" + iOrganIncidentNum3.ToString() + "件；\n2、本月派工单完结率" + iOrganIncidentRate1.ToString("N2") + "%，本年累计" + iOrganIncidentRate2.ToString("N2") + "%；本月协调单完结率" + iOrganIncidentRate3.ToString("N2") + "%，本年累计" + iOrganIncidentRate4.ToString("N2") + "%；\n");

                    #endregion
                }

                #region 报事预警数


                //_tempP += "超时预警：已受理未分派超时工单<a href='Incident/IncidentAssignedBrowse.aspx' style='color:red'>" + dTable11.Rows[0][0] + "</a>件,已分派未处理超时工单<a href='Incident/IncidentProcessingBrowse.aspx' style='color:red'>" + dTable22.Rows[0][0] + "</a>件,已处理未回访超时工单<a href='Incident/IncidentReplyBrowse.aspx' style='color:red'>" + dTable33.Rows[0][0] + "</a>件；";
                DataTable dTable11 = HSPR_IncidentWarningCount1_Filter(row["UserCode"].ToString(), iCommID);

                string _tempP = "3、超时预警：已受理未分派超时工单" + dTable11.Rows[0][0] + "件,已分派未处理超时工单" + dTable11.Rows[0][1] + "件,已处理未回访超时工单" + dTable11.Rows[0][2] + "件；";


                strReText3.Append(_tempP);
                #endregion
            }
            switch (type)
            {
                case 1: result = JSONHelper.FromString(true, strReText1.ToString()); break;
                case 2: result = JSONHelper.FromString(true, strReText2.ToString()); break;
                case 3: result = JSONHelper.FromString(true, strReText3.ToString()); break;
                case 4: result = JSONHelper.FromString(true, strReText4.ToString()); break;
                case 5: result = strReText1_1.ToString(); break;
                case 6: result = strReText2_1.ToString(); break;
                default: result = ""; break;
            }

            if (result == "")
            {
                result = "暂无内容";
            }
            return result;
        }
        #endregion

        public string GetServiceDynamicChart(string OrganCode, int CommID)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@OrganCode", SqlDbType.VarChar),
                    new SqlParameter("@CommID", SqlDbType.VarChar)
            };
            parameters[0].Value = OrganCode;
            parameters[1].Value = CommID;

            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Sys_TakePicIncidentTypeDeskTop_Filter", parameters, "RetDataSet").Tables[0];

            return JSONHelper.FromString(dTable);
        }

        public string GetCustomerInfo_New(string RoleCode)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@RoleCode", SqlDbType.VarChar)
            };
            parameters[0].Value = RoleCode;

            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_Personnel_Prompt_Filter_NewMobile", parameters, "RetDataSet").Tables[0];

            if (dTable.Rows.Count > 0)
            {
                string data = dTable.Rows[0][0].ToString();
                if (string.IsNullOrEmpty(data))
                {
                    data = "暂无人事相关数据";
                }
                return JSONHelper.FromString(true, data);
            }
            else
            {
                return JSONHelper.FromString(true, "暂无数据");
            }
        }


        #region 报事预警

        public static DataTable Sys_Incident_WaringData_Filter(string usercode, string tempSQL)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@usercode", SqlDbType.VarChar,36),
                    new SqlParameter("@tempSQL", SqlDbType.VarChar,3999)
            };
            parameters[0].Value = usercode;
            parameters[1].Value = tempSQL;

            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_HSPR_IncidentWarningMeassge_Filter", parameters, "RetDataSet").Tables[0];

            return dTable;
        }

        public static DataTable HSPR_IncidentWarningCount1_Filter(string usercode, int CommID)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@usercode", SqlDbType.VarChar,36),
                    new SqlParameter("@commid", SqlDbType.Int)
            };
            parameters[0].Value = usercode;
            parameters[1].Value = CommID;

            DataTable dTable = new DbHelperSQLP(PubConstant.hmWyglConnectionString.ToString()).RunProcedure("Proc_HSPR_IncidentWarningCount1_Filter", parameters, "RetDataSet").Tables[0];

            return dTable;



        }

        #endregion
    }
}