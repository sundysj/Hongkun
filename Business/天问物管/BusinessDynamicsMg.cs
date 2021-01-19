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
    public  class BusinessDynamicsMg
    {
        public string GetDynamic(DataRow row,string Type)
        {
            String result = "";
            StringBuilder strReText = new StringBuilder("");
            #region 变量定义
            //int iCommID = AppGlobal.StrToInt(Global_Var.LoginCommID);
            int iCommID = 0;
            if (row.Table.Columns.Contains("CommID"))
            {
                iCommID = AppGlobal.StrToInt(row["CommID"].ToString());
            }
            string strCommName = AppGlobal.GetCommName(iCommID);
            string strOrganName = AppGlobal.GetOrganName4(Global_Var.LoginOrganCorp);
            string strDate = "";
            string strPark = "";
            string strRoomState = "";
            string strOrganParkList = "";
            string strStateList = "";
            string strParkList = "";
            string strOrganDate = "";
            string strOrganRoomState = "";
            string strOrganPark = "";
            string strOrganStateList = "";
            decimal iAllArea = 0;
            decimal iOrganAllArea = 0;
            decimal iChargeRate1 = 0;
            decimal iChargeRate2 = 0;
            decimal iChargeRate3 = 0;
            
            int iRoomNum4 = 0;
            int iRoomNum5 = 0;
            int iRoomNum6 = 0;
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
            #endregion

            // 项目
            if (iCommID != 0)
            {
                string strSQL = " CommID = " + iCommID.ToString() + " and datediff(day,StatDate,getdate()) = 1 and StatType = 1 ";

                MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic();

                //DataTable dTable = Bll.GetList(strSQL, "RoomNum4,RoomNum5,RoomNum6,ChargeRate1,ChargeRate2,ChargeRate3,FeesCreateNum,FeesCancelNum,AllArea,StatDate,IncidentNum1,IncidentNum2,IncidentNum3,IncidentNum4,IncidentNum5,IncidentRate1,IncidentRate2,IncidentRate3,IncidentRate4").Tables[0];
                DataTable dTable = Bll.GetList(1000, strSQL, "CommID").Tables[0];
                if (dTable.Rows.Count > 0)
                {
                    DataRow DRow = dTable.Rows[0];
                    iRoomNum4 = AppGlobal.StrToInt(DRow["RoomNum4"].ToString());
                    iRoomNum5 = AppGlobal.StrToInt(DRow["RoomNum5"].ToString());
                    iRoomNum6 = AppGlobal.StrToInt(DRow["RoomNum6"].ToString());
                    iChargeRate1 = AppGlobal.StrToDec(DRow["ChargeRate1"].ToString());
                    iChargeRate2 = AppGlobal.StrToDec(DRow["ChargeRate2"].ToString());
                    iChargeRate3 = AppGlobal.StrToDec(DRow["ChargeRate3"].ToString());
                    iFeesCreateNum = AppGlobal.StrToInt(DRow["FeesCreateNum"].ToString());
                    iFeesCancelNum = AppGlobal.StrToInt(DRow["FeesCancelNum"].ToString());
                    iAllArea = AppGlobal.StrToDec(DRow["AllArea"].ToString());
                    strDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日");

                    iIncidentNum1 = AppGlobal.StrToInt(DRow["IncidentNum1"].ToString());
                    iIncidentNum2 = AppGlobal.StrToInt(DRow["IncidentNum2"].ToString());
                    iIncidentNum3 = AppGlobal.StrToInt(DRow["IncidentNum3"].ToString());
                    iIncidentNum4 = AppGlobal.StrToInt(DRow["IncidentNum4"].ToString());
                    iIncidentNum5 = AppGlobal.StrToInt(DRow["IncidentNum5"].ToString());

                    iIncidentRate1 = AppGlobal.StrToDec(DRow["IncidentRate1"].ToString());
                    iIncidentRate2 = AppGlobal.StrToDec(DRow["IncidentRate2"].ToString());
                    iIncidentRate3 = AppGlobal.StrToDec(DRow["IncidentRate3"].ToString());
                    iIncidentRate4 = AppGlobal.StrToDec(DRow["IncidentRate4"].ToString());

                }

                #region 房屋状态
                string strSQL2 = " CommID = " + iCommID.ToString() + " and datediff(day,StatDate,getdate()) = 1 and StatType = 1 ";
                MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState BllState = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState();
                DataTable dTable2 = BllState.GetList(strSQL2).Tables[0];
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


                switch (Type)
                {
                    case "1":
                        //获取资源动态
                        strReText.Append("【" + strCommName + "】<br/>");
                        strReText.Append("1、截止到" + strDate + "，可收物管费面积共" + iAllArea.ToString() + "平方米" + strRoomState + "<br/>2、截止到" + strDate + "，" + strPark + "<br/>");
                        strReText.Append("3、" + strDate + "共有" + iRoomNum4.ToString() + "套办理转让，" + iRoomNum5.ToString() + "套办理租赁，" + iRoomNum6.ToString() + "套车位办理转让。");
                        break;
                    case "2":
                        //收费动态
                        strReText.Append("【" + strCommName + "】<br/>");
                        strReText.Append("1、截止到" + strDate + "，物管费本月收费率" + iChargeRate1.ToString("N2") + "%，本年累计收费率" + iChargeRate2.ToString("N2") + "%，以前年度欠费回收率" + iChargeRate3.ToString("N2") + "%。" + "<br/>");
                        strReText.Append("2、" + strDate + "，共有" + iFeesCreateNum.ToString() + "笔审核费用入账，" + iFeesCancelNum.ToString() + "笔费用撤销。");

                        break;
                    case "3":
                        //客服动态
                        strReText.Append("【" + strCommName + "】<br/>");
                        strReText.Append("1、截止到" + strDate + "，未完结的派工单" + iIncidentNum1.ToString() + "件（其中逾期" + iIncidentNum2.ToString() + "件），超过60天未完结的协调单" + iIncidentNum3.ToString() + "件；本月派工单完结率" + iIncidentRate1.ToString("N2") + "%，本年累计" + iIncidentRate2.ToString("N2") + "%；本月协调单完结率" + iIncidentRate3.ToString("N2") + "%，本年累计" + iIncidentRate4.ToString("N2") + "%。<br/>");
                        strReText.Append("2、" + strDate + "，共有" + iIncidentNum4.ToString() + "件报事，其中" + iIncidentNum5.ToString() + "件未完结；<br/>");
                        break;
                }
                #endregion
            }

            else
            {
                #region
                if (Global_Var.LoginOrganCode == "" || Global_Var.LoginOrganCode == "01")
                {
                    #region 集团

                    #region 集团
                    string strSQLOrgan = " datediff(day,StatDate,getdate()) = 1 and StatType = 3 ";

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic();
                    DataTable dTableOrgan = Bll.GetList(strSQLOrgan, "AllArea,StatDate,ChargeRate1,ChargeRate2,ChargeRate3,IncidentNum1,IncidentNum2,IncidentNum3,IncidentNum4,IncidentNum5,IncidentRate1,IncidentRate2,IncidentRate3,IncidentRate4").Tables[0];
                   // DataTable dTableOrgan = Bll.GetList(strSQLOrgan).Tables[0];
                    decimal iOrganChargeRate1 = 0;
                    decimal iOrganChargeRate2 = 0;
                    decimal iOrganChargeRate3 = 0;
                    int iOrganIncidentNum1 = 0;
                    int iOrganIncidentNum2 = 0;
                    int iOrganIncidentNum3 = 0;
                    int iOrganIncidentNum4 = 0;
                    int iOrganIncidentNum5 = 0;
                    decimal iOrganIncidentRate1 = 0;
                    decimal iOrganIncidentRate2 = 0;
                    decimal iOrganIncidentRate3 = 0;
                    decimal iOrganIncidentRate4 = 0;
                    if (dTableOrgan.Rows.Count > 0)
                    {
                        DataRow DRow = dTableOrgan.Rows[0];
                        iOrganAllArea = AppGlobal.StrToDec(DRow["AllArea"].ToString());
                        strOrganDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日");

                        iOrganChargeRate1 = AppGlobal.StrToDec(DRow["ChargeRate1"].ToString());
                        iOrganChargeRate2 = AppGlobal.StrToDec(DRow["ChargeRate2"].ToString());
                        iOrganChargeRate3 = AppGlobal.StrToDec(DRow["ChargeRate3"].ToString());

                        iOrganIncidentNum1 = AppGlobal.StrToInt(DRow["IncidentNum1"].ToString());
                        iOrganIncidentNum2 = AppGlobal.StrToInt(DRow["IncidentNum2"].ToString());
                        iOrganIncidentNum3 = AppGlobal.StrToInt(DRow["IncidentNum3"].ToString());
                        iOrganIncidentNum4 = AppGlobal.StrToInt(DRow["IncidentNum4"].ToString());
                        iOrganIncidentNum5 = AppGlobal.StrToInt(DRow["IncidentNum5"].ToString());

                        iOrganIncidentRate1 = AppGlobal.StrToDec(DRow["IncidentRate1"].ToString());
                        iOrganIncidentRate2 = AppGlobal.StrToDec(DRow["IncidentRate2"].ToString());
                        iOrganIncidentRate3 = AppGlobal.StrToDec(DRow["IncidentRate3"].ToString());
                        iOrganIncidentRate4 = AppGlobal.StrToDec(DRow["IncidentRate4"].ToString());
                    }
                    dTableOrgan.Dispose();
                    #endregion

                    #region 房屋状态(集团)
                    string strSQLOrgan2 = " OrganCode like '" + Global_Var.LoginOrganCorp + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 3 ";

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState BllState = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState();

                    DataTable dTableOrgan2 = BllState.GetList(strSQLOrgan2).Tables[0];
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
                    string strSQLOrgan3 = " OrganCode like '" + Global_Var.LoginOrganCorp + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 3 ";
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

                    switch (Type)
                    {
                        case "1":
                            strReText.Append("1、截止到" + strOrganDate + "，可收物管费面积共" + iOrganAllArea.ToString() + "平方米" + strOrganRoomState + "<br/>2、截止到" + strOrganDate + "，" + strOrganPark);
                            break;
                        case "2":
                            strReText.Append("截止到" + strOrganDate + "：<br/>1、物管费本月收费率" + iOrganChargeRate1.ToString("N2") + "%；<br/>2、本年累计收费率" + iOrganChargeRate2.ToString("N2") + "%；<br/>3、以前年度欠费回收率" + iOrganChargeRate3.ToString("N2") + "%。");

                            break;
                        case "3":
                            strReText.Append("1、截止到" + strOrganDate + "，未完结的派工单" + iOrganIncidentNum1.ToString() + "件（其中逾期" + iOrganIncidentNum2.ToString() + "件），超过60天未完结的协调单" + iOrganIncidentNum3.ToString() + "件；<br/>2、本月派工单完结率" + iOrganIncidentRate1.ToString("N2") + "%，本年累计" + iOrganIncidentRate2.ToString("N2") + "%；本月协调单完结率" + iOrganIncidentRate3.ToString("N2") + "%，本年累计" + iOrganIncidentRate4.ToString("N2") + "%；<br/>");
                            break;
                    }

                    #endregion
                    #endregion
                }
                else
                {
                    #region 公司

                    #region 地区公司
                    string strSQLOrgan = " OrganCode like '" + Global_Var.LoginOrganCorp + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 2 ";

                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic Bll = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePic();
                    decimal iOrganChargeRate1 = 0;
                    decimal iOrganChargeRate2 = 0;
                    decimal iOrganChargeRate3 = 0;

                    int iOrganIncidentNum1 = 0;
                    int iOrganIncidentNum2 = 0;
                    int iOrganIncidentNum3 = 0;
                    int iOrganIncidentNum4 = 0;

                    decimal iOrganIncidentRate1 = 0;
                    decimal iOrganIncidentRate2 = 0;
                    decimal iOrganIncidentRate3 = 0;
                    decimal iOrganIncidentRate4 = 0;
                    DataTable dTableOrgan = Bll.GetList(strSQLOrgan, "AllArea,StatDate,ChargeRate1,ChargeRate2,ChargeRate3,IncidentNum1,IncidentNum2,IncidentNum3,IncidentNum4,IncidentRate1,IncidentRate2,IncidentRate3,IncidentRate4").Tables[0];
                    if (dTableOrgan.Rows.Count > 0)
                    {
                        #region 取值
                        DataRow DRow = dTableOrgan.Rows[0];
                        iOrganAllArea = AppGlobal.StrToDec(DRow["AllArea"].ToString());
                        strOrganDate = AppGlobal.StrToDate(DRow["StatDate"].ToString()).ToString("yyyy年MM月dd日");
                        iOrganChargeRate1 = AppGlobal.StrToDec(DRow["ChargeRate1"].ToString());
                        iOrganChargeRate2 = AppGlobal.StrToDec(DRow["ChargeRate2"].ToString());
                        iOrganChargeRate3 = AppGlobal.StrToDec(DRow["ChargeRate3"].ToString());

                        iOrganIncidentNum1 = AppGlobal.StrToInt(DRow["IncidentNum1"].ToString());
                        iOrganIncidentNum2 = AppGlobal.StrToInt(DRow["IncidentNum2"].ToString());
                        iOrganIncidentNum3 = AppGlobal.StrToInt(DRow["IncidentNum3"].ToString());
                        iOrganIncidentNum4 = AppGlobal.StrToInt(DRow["IncidentNum4"].ToString());

                        iOrganIncidentRate1 = AppGlobal.StrToDec(DRow["IncidentRate1"].ToString());
                        iOrganIncidentRate2 = AppGlobal.StrToDec(DRow["IncidentRate2"].ToString());
                        iOrganIncidentRate3 = AppGlobal.StrToDec(DRow["IncidentRate3"].ToString());
                        iOrganIncidentRate4 = AppGlobal.StrToDec(DRow["IncidentRate4"].ToString());

                        #endregion
                    }

                    dTableOrgan.Dispose();
                    #endregion

                    #region 房屋状态(地区公司)
                    string strSQLOrgan2 = " OrganCode like '" + Global_Var.LoginOrganCorp + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 2 ";
                    MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState BllState = new MobileSoft.BLL.Sys.Bll_Tb_Sys_TakePicRoomState();
                    DataTable dTableOrgan2 = BllState.GetList(strSQLOrgan2).Tables[0];
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
                    string strSQLOrgan3 = " OrganCode like '" + Global_Var.LoginOrganCorp + "%' and datediff(day,StatDate,getdate()) = 1 and StatType = 2  ";
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
                   switch (Type)
                    {
                        case "1":
                            strReText.Append("1、截止到" + strOrganDate + "，可收物管费面积共" + iOrganAllArea.ToString() + "平方米" + strOrganRoomState + "<br/>2、截止到" + strOrganDate + "，" + strOrganPark);
                            break;
                        case "2":
                            strReText.Append("截止到" + strOrganDate + "，物管费本月收费率" + iOrganChargeRate1.ToString("N2") + "%，本年累计收费率" + iOrganChargeRate2.ToString("N2") + "%，以前年度欠费回收率" + iOrganChargeRate3.ToString("N2") + "%。");
                            break;
                        case "3":
                            strReText.Append("1、截止到" + strOrganDate + "，未完结的派工单" + iOrganIncidentNum1.ToString() + "件（其中逾期" + iOrganIncidentNum2.ToString() + "件），超过60天未完结的协调单" + iOrganIncidentNum3.ToString() + "件；<br/>2、本月派工单完结率" + iOrganIncidentRate1.ToString("N2") + "%，本年累计" + iOrganIncidentRate2.ToString("N2") + "%；本月协调单完结率" + iOrganIncidentRate3.ToString("N2") + "%，本年累计" + iOrganIncidentRate4.ToString("N2") + "%；<br/>");
                            break;
                    }
                    #endregion

                    #endregion
                }
                #endregion
            }

            if (strReText.ToString().Trim() == "")
            {
                result = "暂无内容";
            }
            else
            {
                result = JSONHelper.FromString(true, strReText.ToString());
            }
            return result;
        }


    }
}
