using Common;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Business
{
    /// <summary>
    /// 动态，谭洋，2017年6月27日16:27:31
    /// </summary>
    public class BusinessDynamic_ty
    {
        /// <summary>
        /// 收费动态
        /// </summary>
        /// <returns></returns>
        public string GetChargeDynamic(DataRow row)
        {
            #region 
            int iCommID = 0;
            if (row.Table.Columns.Contains("CommID"))
            {
                iCommID = AppGlobal.StrToInt(row["CommID"].ToString());
            }
            string strCommName = AppGlobal.GetCommName(iCommID);
            string strOrganName = "";

            string strDate = "";
            string strReText = "";

            decimal iChargeRate1 = 0;
            decimal iChargeRate2 = 0;
            decimal iChargeRate3 = 0;
            int iFeesCreateNum = 0;
            int iFeesCancelNum = 0;

            #endregion

            string strSQL = "";
            int temp = 3;

            // 项目收费动态
            if (iCommID != 0)
            {
                strSQL = " and CommID = " + iCommID.ToString() + " and datediff(day,StatDate,getdate()) = 1 and StatType = 1 ";
            }
            else
            {
                string organCode = null;

                if (row.Table.Columns.Contains("OrganCode"))
                {
                    organCode = row["OrganCode"].ToString();
                }

                if (string.IsNullOrEmpty(organCode))
                {
                    organCode = "01";
                }

                strOrganName = AppGlobal.GetOrganName4(organCode);

                // 分公司
                if (organCode.Length == 4)
                {
                    temp = 2;
                    strSQL = " and OrganCode like '" + organCode + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 2 ";
                }

                // 总公司
                if (organCode.Length == 2)
                {
                    temp = 1;
                    strSQL = " and datediff(day,StatDate,getdate()) = 1 and StatType = 3 ";
                }
            }

            MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic();

            DataTable dTable = Bll.GetListFromProc(strSQL).Tables[0];

            if (dTable.Rows.Count > 0)
            {
                DataRow DRow = dTable.Rows[0];

                iChargeRate1 = AppGlobal.StrToDec(DRow["ChargeRate1"].ToString());
                iChargeRate2 = AppGlobal.StrToDec(DRow["ChargeRate2"].ToString());
                iChargeRate3 = AppGlobal.StrToDec(DRow["ChargeRate3"].ToString());

                iFeesCreateNum = AppGlobal.StrToInt(DRow["FeesCreateNum"].ToString());
                iFeesCancelNum = AppGlobal.StrToInt(DRow["FeesCancelNum"].ToString());

                strDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日");
            }
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                    new DataColumn(){ ColumnName = dTable.Columns["ChargeRate1"].ColumnName, DataType = dTable.Columns["ChargeRate1"].DataType },
                    new DataColumn(){ ColumnName = dTable.Columns["ChargeRate2"].ColumnName, DataType = dTable.Columns["ChargeRate2"].DataType },
                    new DataColumn(){ ColumnName = "Info", DataType = typeof(string) }
                });
            DataRow resultRow = dt.NewRow();

            if (dTable.Rows.Count > 0)
            {
                DataRow DRow = dTable.Rows[0];
                resultRow["ChargeRate1"] = DRow["ChargeRate1"];
                resultRow["ChargeRate2"] = DRow["ChargeRate2"];
            } else
            {
                resultRow["ChargeRate1"] = 0;
                resultRow["ChargeRate2"] = 0;
            }

            dTable.Dispose();

            switch (temp)
            {
                case 3:
                    strReText = string.Format(@"【{0}】<br/>1、截止到{1}，物管费本月收费率{2}%，本年累计收费率{3}%，以前年度欠费回收率{4}%。<br/>2、截止到{5}，本月共有{6}笔审核费用入账，{7}笔费用撤销。",
                strCommName, strDate, iChargeRate1.ToString("N2"), iChargeRate2.ToString("N2"), iChargeRate3.ToString("N2"), strDate, iFeesCreateNum.ToString(), iFeesCancelNum.ToString());
                    break;

                case 2:
                case 1:
                    strReText = string.Format(@"【{0}】<br/>1、截止到{1}，物管费本月收费率{2}%，本年累计收费率{3}%，以前年度欠费回收率{4}%。<br/>2、截止到{5}，本月共有{6}笔审核费用入账，{7}笔费用撤销。",
                strOrganName, strDate, iChargeRate1.ToString("N2"), iChargeRate2.ToString("N2"), iChargeRate3.ToString("N2"), strDate, iFeesCreateNum.ToString(), iFeesCancelNum.ToString());
                    break;
            }
            
            resultRow["Info"] = strReText;
            dt.Rows.Add(resultRow);

            return JSONHelper.FromString(dt);
        }


        /// <summary>
        /// 客服动态
        /// </summary>
        /// <returns></returns>
        public string GetIncidentDynamic(DataRow row)
        {
            #region 
            int iCommID = 0;
            if (row.Table.Columns.Contains("CommID"))
            {
                iCommID = AppGlobal.StrToInt(row["CommID"].ToString());
            }
            string userCode = Global_Var.LoginUserCode;
            if (row.Table.Columns.Contains("UserCode"))
            {
                userCode = row["UserCode"].ToString();
            }
            string organCode = null;
            string strCommName = AppGlobal.GetCommName(iCommID);
            string strOrganName = "";

            //未分派的报事
            int NoDispIncidentNum = 0;
            //未完结的协调单
            int NoCoordinateIncidentNum = 0;
            //逾期的协调单
            int BeOverdueIncidentNum = 0;

            string strDate = "";
            string strReText = "";

            int iIncidentNum1 = 0;
            int iIncidentNum2 = 0;
            int iIncidentNum3 = 0;
            int iIncidentNum4 = 0;
            int iIncidentNum5 = 0;

            decimal iIncidentRate1 = 0;
            decimal iIncidentRate2 = 0;
            decimal iIncidentRate3 = 0;
            decimal iIncidentRate4 = 0;
            #endregion

            string strSQL = "";
            int temp = 3;

            // 项目客服动态
            if (iCommID != 0)
            {
                strSQL = " and CommID = " + iCommID.ToString() + " and datediff(day,StatDate,getdate()) = 1 and StatType = 1 ";
            }
            else
            {
                if (row.Table.Columns.Contains("OrganCode"))
                {
                    organCode = row["OrganCode"].ToString();
                }

                if (string.IsNullOrEmpty(organCode))
                {
                    organCode = "01";
                }

                strOrganName = AppGlobal.GetOrganName4(organCode);

                // 分公司
                if (organCode.Length == 4)
                {
                    temp = 2;
                    strSQL = " and OrganCode like '" + organCode + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 2 ";
                }

                // 总公司
                if (organCode.Length == 2)
                {
                    temp = 1;
                    strSQL = " and datediff(day,StatDate,getdate()) = 1 and StatType = 3 ";
                }
            }

            MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic();

            DataTable dTable = Bll.GetListFromProc(strSQL).Tables[0];

            if (dTable.Rows.Count > 0)
            {
                DataRow DRow = dTable.Rows[0];

                NoDispIncidentNum = AppGlobal.StrToInt(DRow["NoDispIncidentNum"].ToString());
                NoCoordinateIncidentNum = AppGlobal.StrToInt(DRow["NoCoordinateIncidentNum"].ToString());
                BeOverdueIncidentNum = AppGlobal.StrToInt(DRow["BeOverdueIncidentNum"].ToString());

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
            }
            dTable.Dispose();

            if (string.IsNullOrEmpty(strDate))
            {
                strDate = DateTime.Now.ToString("yyyy年MM月dd日");
            }

            // 读取报时预警信息
            MobileSoft.BLL.HSPR.Bll_Tb_HSPR_TempIncidentWarning Bll3 = new MobileSoft.BLL.HSPR.Bll_Tb_HSPR_TempIncidentWarning();
            DataTable dTable3 = Bll3.GetList(userCode, iCommID).Tables[0];

            string value1 = "";
            string value2 = "";
            string value3 = "";

            if (dTable3.Rows.Count > 0)
            {
                DataRow DRow = dTable3.Rows[0];
                value1 = DRow[0].ToString();
                value2 = DRow[1].ToString();
                value3 = DRow[2].ToString();
            }

            switch (temp)
            {
                case 3:
                    strReText = string.Format(@"【{0}】<br/>1、截止到{1}，未分派报事{2}件，未完结的派工单{3}件(其中逾期{4}件)，未完结的协调单{5}件(其中逾期{6}件)；本月派工单完结率{7}%，本年累计{8}%；本月协调单完结率{9}%，本年累计{10}%。<br/>2、{11}，共有{12}件报事，其中{13}件未完结。<br/>3、超时预警：已受理未分派超时工单{14}件,已分派未处理超时工单{15}件,已处理未回访超时工单{16}件；",
                        strCommName, strDate, NoDispIncidentNum.ToString(), iIncidentNum1.ToString(), iIncidentNum2.ToString(), NoCoordinateIncidentNum.ToString(), BeOverdueIncidentNum.ToString(), iIncidentRate1.ToString("N2"), iIncidentRate2.ToString("N2"), iIncidentRate3.ToString("N2"), iIncidentRate4.ToString("N2"), strDate, iIncidentNum4.ToString(), iIncidentNum5.ToString(), value1, value2, value3);
                    break;

                case 2:
                case 1:
                    strReText = string.Format(@"【{0}】<br/>1、截止到{1}，未分派报事{2}件，未完结的派工单{3}件(其中逾期{4}件)，未完结的协调单{5}件(其中逾期{6}件)；本月派工单完结率{7}%，本年累计{8}%；本月协调单完结率{9}%，本年累计{10}%。<br/>2、{11}，共有{12}件报事，其中{13}件未完结。<br/>3、超时预警：已受理未分派超时工单{14}件，已分派未处理超时工单{15}件,已处理未回访超时工单{16}件；",
                        strOrganName, strDate, NoDispIncidentNum.ToString(), iIncidentNum1.ToString(), iIncidentNum2.ToString(), NoCoordinateIncidentNum.ToString(), BeOverdueIncidentNum.ToString(), iIncidentRate1.ToString("N2"), iIncidentRate2.ToString("N2"), iIncidentRate3.ToString("N2"), iIncidentRate4.ToString("N2"), strDate, iIncidentNum4.ToString(), iIncidentNum5.ToString(), value1, value2, value3);
                    break;
            }

            // 客服动态图表统计
            MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicIncidentTypeDeskTop Bll2 = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicIncidentTypeDeskTop();
            DataTable dTable2 = Bll2.GetList(organCode, iCommID).Tables[0];

            string chart = JSONHelper.FromDataTable(dTable2);

            return JSONHelper.FromJsonString(true, "{\"Info\":\"" + strReText + "\", \"Chart\":" + chart + "}");
        }
    }
}
