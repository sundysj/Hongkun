using MobileSoft.Common;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ReceFeesPrec_BGY
    {
        private static string ConnectionString = PubConstant.GetConnectionString("SQLConnection").ToString();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCommID"></param>
        /// <param name="iCustID"></param>
        /// <param name="iRoomID"></param>
        /// <param name="strCostIDs"></param>
        /// <param name="strChargeMode"></param>
        /// <param name="iPrecAmount"></param>
        /// <param name="strUserCode"></param>
        /// <param name="HitResult"></param>
        /// <param name="ErrMsg"></param>
        /// <param name="iReceID"></param>
        /// <returns></returns>
        #region 预收
        public static int ReceivePrecFees(int iCommID, long iCustID, long iRoomID, string strCostIDs
            , string strChargeMode, decimal iPrecAmount, string strUserCode, ref string HitResult, ref string ErrMsg, ref long iReceID)
        {
            int iReturn = 0;
            bool IsPrint = true;

            int Percent = 0;//完成百分比 
            iReceID = 0;

            HitResult = "";

            string strBillsSign = "";
            int iPrintTimes = 0;
            DateTime dBillsDate = DateTime.Now;
            string strBillsDate = dBillsDate.ToString();

            int iAccountWay = TypeRule.TWAccountWay.PreCosts;
            string strReceMemo = "";
            int iIsDelete = 0;

            //string strInvoiceBills = "";
            //long iInvoiceTypeID = 0;

            string strInvoiceBill = "";
            string strInvoiceUnit = "";
            string strRemitterUnit = "";

            string strBankName = "";
            string strBankAccount = "";

            string strChequeBill = "";
            long iBillTypeID = 0;

            try
            {
                #region 保存明细
                string strSQL = "  and CommID = " + iCommID.ToString() + "  ";
                strSQL = strSQL + " and CustID = -1 and RoomID = -1 ";

                DataTable dTableDetail = HSPR_PreCostsDetail_Filter(strSQL);

                if (dTableDetail != null)
                {
                    DataRow DRow = dTableDetail.NewRow();
                    DRow["RecdID"] = 0;
                    DRow["PrecID"] = 0;
                    DRow["CommID"] = iCommID;
                    DRow["CostID"] = AppGlobal.StrToLong(strCostIDs);
                    DRow["CustID"] = iCustID;
                    DRow["RoomID"] = iRoomID;
                    //DRow["PrecAmount"]=AppGlobal.StrToDec(PrecAmount.Value.ToString());
                    if (iPrecAmount <= 0)
                    {
                        DRow["PrecAmount"] = 0;
                    }
                    else
                    {
                        DRow["PrecAmount"] = iPrecAmount;
                    }
                    string strPrecMemo = "";
                    if (strPrecMemo == "请点击费用备注生成费用期间")
                    {
                        strPrecMemo = "";
                    }
                    DRow["PrecMemo"] = strPrecMemo;
                    DRow["BillsSign"] = strBillsSign;
                    DRow["PrecDate"] = DateTime.Now.ToString();
                    DRow["UserCode"] = strUserCode;


                    DRow["CostIDs"] = strCostIDs;
                    DRow["RoomSign"] = "";
                    DRow["CostNames"] = "";
                    DRow["UserName"] = "";
                    DRow["ChargeMode"] = strChargeMode;
                    DRow["AccountWay"] = TypeRule.TWAccountWay.PreCosts;

                    DRow["HandID"] = 0;

                    DRow["HandIDs"] = "";

                    DRow["ParkNames"] = "";

                    DRow["IsDelete"] = 0;

                    dTableDetail.Rows.Add(DRow);
                    dTableDetail.AcceptChanges();


                }
                #endregion

                HitResult = "";
                IsPrint = false;

                decimal iTotalAmount = 0;
                int iFeesLimitCount = 0;

                string strProcName = "Proc_HSPR_PreCosts_InsertAuto";

                string ChildSQLEx = "";

                string[] arrSQLEx = new string[10];

                int iSelCount = 0;
                int iCut = 0;

                //每段笔数
                int iExcCount = 10;

                //*行计数器
                int k = 0;
                Decimal Len = 1;

                if (dTableDetail != null)
                {
                    if (dTableDetail.Rows.Count != 0)
                    {
                        Len = Convert.ToDecimal(dTableDetail.Rows.Count + 1);

                        foreach (DataRow DRow in dTableDetail.Rows)
                        {
                            k++;
                            Percent = Convert.ToInt32(k / Len * 100);

                            #region 取值
                            int iiCommID = AppGlobal.StrToInt(DRow["CommID"].ToString());
                            long iiCustID = AppGlobal.StrToLong(DRow["CustID"].ToString());
                            long iiRoomID = AppGlobal.StrToLong(DRow["RoomID"].ToString());
                            long iiCostID = AppGlobal.StrToLong(DRow["CostID"].ToString());
                            decimal iiPrecAmount = AppGlobal.StrToDec(DRow["PrecAmount"].ToString());
                            string strPrecMemo = DRow["PrecMemo"].ToString();
                            long iiHandID = AppGlobal.StrToLong(DRow["HandID"].ToString());

                            string strTmpCostIDs = DRow["CostIDs"].ToString();
                            string strTmpHandIDs = DRow["HandIDs"].ToString();

                            #endregion

                            #region 合计
                            iTotalAmount = iTotalAmount + iiPrecAmount;

                            if (iiPrecAmount > 0)
                            {
                                iFeesLimitCount++;
                            }
                            #endregion

                            //有需要预交的费用
                            if (iiPrecAmount > 0)
                            {
                                #region 明细生成

                                ChildSQLEx = " exec " + strProcName + " " + iiCommID.ToString() + ","
                                    + iiCustID.ToString() + ","
                                    + iiRoomID.ToString() + ","
                                    + iiCostID.ToString() + ","
                                    + iiPrecAmount.ToString() + ","
                                    + "$ReceID$" + ","
                                    + "N'" + strPrecMemo + "',"
                                    + iiHandID.ToString() + ","
                                    + "N'" + strTmpCostIDs + "',"
                                    + "N'" + strTmpHandIDs + "'"
                                    + "\r\n";

                                #region 分成多段
                                if ((iSelCount < (iExcCount * (iCut + 1))) && (iSelCount >= iExcCount * iCut))
                                {
                                    arrSQLEx[iCut] = arrSQLEx[iCut] + ChildSQLEx;
                                }

                                iSelCount++;

                                if (iSelCount >= (iExcCount * (iCut + 1)))
                                {
                                    iCut++;
                                }

                                #endregion

                                #endregion
                            }
                        }

                        //有需要预交的费用
                        if (iFeesLimitCount > 0)
                        {
                            #region 票据
                            DataTable dTable = HSPR_BillUse_GetBillsSignUseRange(iCommID, "", "手机端");

                            if (dTable.Rows.Count > 0)
                            {
                                DataRow DRow = dTable.Rows[0];

                                iBillTypeID = AppGlobal.StrToLong(DRow["BillTypeID"].ToString());
                                strBillsSign = DRow["BillsSign"].ToString();
                            }
                            dTable.Dispose();
                            #endregion

                            //预交收据
                            iReceID = HSPR_PreCostsReceipts_InsertAuto(iCommID, iCustID, iRoomID, strBillsSign
                                , iPrintTimes, strBillsDate, strUserCode, strChargeMode, iAccountWay, strReceMemo, iTotalAmount, iIsDelete
                                , strInvoiceBill, strInvoiceUnit, strRemitterUnit, strBankName, strBankAccount, strChequeBill, iBillTypeID
                                , arrSQLEx[0], arrSQLEx[1], arrSQLEx[2], arrSQLEx[3], arrSQLEx[4], arrSQLEx[5], arrSQLEx[6], arrSQLEx[7], arrSQLEx[8], arrSQLEx[9]);

                            #region 收据记录
                            if (strBillsSign != "")
                            {
                                if (iReceID != 0)
                                {
                                    int iSourceType = 2;//预交收据
                                    HSPR_BillUseInstead_InsUpdate(iCommID, iBillTypeID, strBillsSign, strUserCode, iReceID, iSourceType, TypeRule.TWBillUseCase.PreCosts);

                                }

                                //更新号段数量和金额
                                if ((iBillTypeID != 0) && (strBillsSign != ""))
                                {
                                    HSPR_BillUse_UpdateData(iCommID, iBillTypeID, strBillsSign);
                                }
                            }
                            #endregion


                            if (iReceID != 0)
                            {
                                IsPrint = true;
                            }
                            else
                            {
                                HitResult = "预存费用失败！";
                                IsPrint = false;
                            }
                        }
                    }
                    else
                    {
                        HitResult = "请添加需要预交的费用！";
                        IsPrint = false;
                    }
                }

            }
            catch (Exception ex)
            {

                ErrMsg = ex.Message;

            }
            finally
            {

            }

            if (IsPrint == true)
            {
                iReturn = 1;
            }
            else
            {
                iReturn = 0;
            }


            return iReturn;

        }
        #endregion


        #region 凤凰会预收
        public static int ReceivePrecFees_FHH(string Memo, int iCommID, long iCustID, long iRoomID, string strCostIDs
            , string strChargeMode, decimal iPrecAmount, string strUserCode, ref string HitResult, ref string ErrMsg, ref long iReceID)
        {
            int iReturn = 0;
            bool IsPrint = true;

            int Percent = 0;//完成百分比 
            iReceID = 0;

            HitResult = "";

            string strBillsSign = "";
            int iPrintTimes = 0;
            DateTime dBillsDate = DateTime.Now;
            string strBillsDate = dBillsDate.ToString();

            int iAccountWay = TypeRule.TWAccountWay.PreCosts;
            string strReceMemo = "";
            int iIsDelete = 0;

            //string strInvoiceBills = "";
            //long iInvoiceTypeID = 0;

            string strInvoiceBill = "";
            string strInvoiceUnit = "";
            string strRemitterUnit = "";

            string strBankName = "";
            string strBankAccount = "";

            string strChequeBill = "";
            long iBillTypeID = 0;

            try
            {
                #region 保存明细
                string strSQL = "  and CommID = " + iCommID.ToString() + "  ";
                strSQL = strSQL + " and CustID = -1 and RoomID = -1 ";

                DataTable dTableDetail = HSPR_PreCostsDetail_Filter(strSQL);

                if (dTableDetail != null)
                {
                    DataRow DRow = dTableDetail.NewRow();
                    DRow["RecdID"] = 0;
                    DRow["PrecID"] = 0;
                    DRow["CommID"] = iCommID;
                    DRow["CostID"] = AppGlobal.StrToLong(strCostIDs);
                    DRow["CustID"] = iCustID;
                    DRow["RoomID"] = iRoomID;
                    //DRow["PrecAmount"]=AppGlobal.StrToDec(PrecAmount.Value.ToString());
                    if (iPrecAmount <= 0)
                    {
                        DRow["PrecAmount"] = 0;
                    }
                    else
                    {
                        DRow["PrecAmount"] = iPrecAmount;
                    }
                    string strPrecMemo = Memo;
                    if (strPrecMemo == "请点击费用备注生成费用期间")
                    {
                        strPrecMemo = "";
                    }
                    DRow["PrecMemo"] = strPrecMemo;
                    DRow["BillsSign"] = strBillsSign;
                    DRow["PrecDate"] = DateTime.Now.ToString();
                    DRow["UserCode"] = strUserCode;


                    DRow["CostIDs"] = strCostIDs;
                    DRow["RoomSign"] = "";
                    DRow["CostNames"] = "";
                    DRow["UserName"] = "";
                    DRow["ChargeMode"] = strChargeMode;
                    DRow["AccountWay"] = TypeRule.TWAccountWay.PreCosts;

                    DRow["HandID"] = 0;

                    DRow["HandIDs"] = "";

                    DRow["ParkNames"] = "";

                    DRow["IsDelete"] = 0;

                    dTableDetail.Rows.Add(DRow);
                    dTableDetail.AcceptChanges();


                }
                #endregion

                HitResult = "";
                IsPrint = false;

                decimal iTotalAmount = 0;
                int iFeesLimitCount = 0;

                string strProcName = "Proc_HSPR_PreCosts_InsertAuto";

                string ChildSQLEx = "";

                string[] arrSQLEx = new string[10];

                int iSelCount = 0;
                int iCut = 0;

                //每段笔数
                int iExcCount = 10;

                //*行计数器
                int k = 0;
                Decimal Len = 1;

                if (dTableDetail != null)
                {
                    if (dTableDetail.Rows.Count != 0)
                    {
                        Len = Convert.ToDecimal(dTableDetail.Rows.Count + 1);

                        foreach (DataRow DRow in dTableDetail.Rows)
                        {
                            k++;
                            Percent = Convert.ToInt32(k / Len * 100);

                            #region 取值
                            int iiCommID = AppGlobal.StrToInt(DRow["CommID"].ToString());
                            long iiCustID = AppGlobal.StrToLong(DRow["CustID"].ToString());
                            long iiRoomID = AppGlobal.StrToLong(DRow["RoomID"].ToString());
                            long iiCostID = AppGlobal.StrToLong(DRow["CostID"].ToString());
                            decimal iiPrecAmount = AppGlobal.StrToDec(DRow["PrecAmount"].ToString());
                            string strPrecMemo = DRow["PrecMemo"].ToString();
                            long iiHandID = AppGlobal.StrToLong(DRow["HandID"].ToString());

                            string strTmpCostIDs = DRow["CostIDs"].ToString();
                            string strTmpHandIDs = DRow["HandIDs"].ToString();

                            #endregion

                            #region 合计
                            iTotalAmount = iTotalAmount + iiPrecAmount;

                            if (iiPrecAmount > 0)
                            {
                                iFeesLimitCount++;
                            }
                            #endregion

                            //有需要预交的费用
                            if (iiPrecAmount > 0)
                            {
                                #region 明细生成

                                ChildSQLEx = " exec " + strProcName + " " + iiCommID.ToString() + ","
                                    + iiCustID.ToString() + ","
                                    + iiRoomID.ToString() + ","
                                    + iiCostID.ToString() + ","
                                    + iiPrecAmount.ToString() + ","
                                    + "$ReceID$" + ","
                                    + "N'" + strPrecMemo + "',"
                                    + iiHandID.ToString() + ","
                                    + "N'" + strTmpCostIDs + "',"
                                    + "N'" + strTmpHandIDs + "'"
                                    + "\r\n";

                                #region 分成多段
                                if ((iSelCount < (iExcCount * (iCut + 1))) && (iSelCount >= iExcCount * iCut))
                                {
                                    arrSQLEx[iCut] = arrSQLEx[iCut] + ChildSQLEx;
                                }

                                iSelCount++;

                                if (iSelCount >= (iExcCount * (iCut + 1)))
                                {
                                    iCut++;
                                }

                                #endregion

                                #endregion
                            }
                        }

                        //有需要预交的费用
                        if (iFeesLimitCount > 0)
                        {
                            #region 票据
                            DataTable dTable = HSPR_BillUse_GetBillsSignUseRange(iCommID, "", "手机端");

                            if (dTable.Rows.Count > 0)
                            {
                                DataRow DRow = dTable.Rows[0];

                                iBillTypeID = AppGlobal.StrToLong(DRow["BillTypeID"].ToString());
                                strBillsSign = DRow["BillsSign"].ToString();
                                HitResult = strBillsSign;
                            }
                            dTable.Dispose();
                            #endregion

                            //预交收据
                            iReceID = HSPR_PreCostsReceipts_InsertAuto(iCommID, iCustID, iRoomID, strBillsSign
                                , iPrintTimes, strBillsDate, strUserCode, strChargeMode, iAccountWay, strReceMemo, iTotalAmount, iIsDelete
                                , strInvoiceBill, strInvoiceUnit, strRemitterUnit, strBankName, strBankAccount, strChequeBill, iBillTypeID
                                , arrSQLEx[0], arrSQLEx[1], arrSQLEx[2], arrSQLEx[3], arrSQLEx[4], arrSQLEx[5], arrSQLEx[6], arrSQLEx[7], arrSQLEx[8], arrSQLEx[9]);

                            #region 收据记录
                            if (strBillsSign != "")
                            {
                                if (iReceID != 0)
                                {
                                    int iSourceType = 2;//预交收据
                                    HSPR_BillUseInstead_InsUpdate(iCommID, iBillTypeID, strBillsSign, strUserCode, iReceID, iSourceType, TypeRule.TWBillUseCase.PreCosts);
                                }

                                //更新号段数量和金额
                                if ((iBillTypeID != 0) && (strBillsSign != ""))
                                {
                                    HSPR_BillUse_UpdateData(iCommID, iBillTypeID, strBillsSign);
                                }
                            }
                            #endregion

                            #region  回發票據號
                            string ContionStringr = ConnectionString;
                            string strSqlr = " select BillsSign,BillsDate   from Tb_HSPR_PreCostsReceipts where receid='" + iReceID + "' ";
                            DataTable dtr = new DbHelperSQLP(PubConstant.GetConnectionString("SQLConnection").ToString()).Query(strSqlr).Tables[0];
                            if (dtr.Rows.Count > 0)
                            {
                                DataRow DRow = dtr.Rows[0];
                                strBillsSign = DRow["BillsSign"].ToString();
                                HitResult = JSONHelper.FromString(dtr.Rows[0]);
                            }

                            #endregion

                            if (iReceID != 0)
                            {
                                IsPrint = true;
                            }
                            else
                            {
                                HitResult = JSONHelper.FromString("预存费用失败！"); ;
                                IsPrint = false;
                            }
                        }
                    }
                    else
                    {
                        HitResult = JSONHelper.FromString("请添加需要预交的费用！"); ;

                        IsPrint = false;
                    }
                }

            }
            catch (Exception ex)
            {

                ErrMsg = ex.Message;

            }
            finally
            {

            }

            if (IsPrint == true)
            {
                iReturn = 1;
            }
            else
            {
                iReturn = 0;
            }


            return iReturn;

        }
        #endregion

        #region POS机预收
        public static int ReceivePrecFees_POS(string Memo, int iCommID, long iCustID, long iRoomID, string[] CostIDs
            , string strChargeMode, string[] iPrecAmounts, string strUserCode, ref string HitResult, ref string strBillsDate, ref string ErrMsg, ref long iReceID)
        {
            int iReturn = 0;
            bool IsPrint = true;

            int Percent = 0;//完成百分比 
            iReceID = 0;

            HitResult = "";

            string strBillsSign = "";
            int iPrintTimes = 0;
            DateTime dBillsDate = DateTime.Now;
            strBillsDate = dBillsDate.ToString();

            int iAccountWay = TypeRule.TWAccountWay.PreCosts;
            string strReceMemo = "";
            int iIsDelete = 0;

            //string strInvoiceBills = "";
            //long iInvoiceTypeID = 0;

            string strInvoiceBill = "";
            string strInvoiceUnit = "";
            string strRemitterUnit = "";

            string strBankName = "";
            string strBankAccount = "";

            string strChequeBill = "";
            long iBillTypeID = 0;

            try
            {
                #region 保存明细
                string strSQL = "  and CommID = " + iCommID.ToString() + "  ";
                strSQL = strSQL + " and CustID = -1 and RoomID = -1 ";

                DataTable dTableDetail = HSPR_PreCostsDetail_Filter(strSQL);

                if (dTableDetail != null)
                {
                    for (int i = 0; i < CostIDs.Length; i++)
                    {
                        DataRow DRow = dTableDetail.NewRow();
                        DRow["RecdID"] = 0;
                        DRow["PrecID"] = 0;
                        DRow["CommID"] = iCommID;
                        DRow["CostID"] = AppGlobal.StrToLong(CostIDs[i]);
                        DRow["CustID"] = iCustID;
                        DRow["RoomID"] = iRoomID;
                        decimal amount = AppGlobal.StrToDec(iPrecAmounts[i]);
                        //DRow["PrecAmount"]=AppGlobal.StrToDec(PrecAmount.Value.ToString());
                        if (amount <= 0)
                        {
                            DRow["PrecAmount"] = 0;
                        }
                        else
                        {
                            DRow["PrecAmount"] = amount;
                        }
                        string strPrecMemo = Memo;
                        if (strPrecMemo == "请点击费用备注生成费用期间")
                        {
                            strPrecMemo = "";
                        }
                        DRow["PrecMemo"] = strPrecMemo;
                        DRow["BillsSign"] = strBillsSign;
                        DRow["PrecDate"] = DateTime.Now.ToString();
                        DRow["UserCode"] = strUserCode;


                        DRow["CostIDs"] = AppGlobal.StrToLong(CostIDs[i]);
                        DRow["RoomSign"] = "";
                        DRow["CostNames"] = "";
                        DRow["UserName"] = "";
                        DRow["ChargeMode"] = strChargeMode;
                        DRow["AccountWay"] = TypeRule.TWAccountWay.PreCosts;

                        DRow["HandID"] = 0;

                        DRow["HandIDs"] = "";

                        DRow["ParkNames"] = "";

                        DRow["IsDelete"] = 0;

                        dTableDetail.Rows.Add(DRow);
                    }

                    dTableDetail.AcceptChanges();


                }
                #endregion

                HitResult = "";
                IsPrint = false;

                decimal iTotalAmount = 0;
                int iFeesLimitCount = 0;

                string strProcName = "Proc_HSPR_PreCosts_InsertAuto";

                string ChildSQLEx = "";

                string[] arrSQLEx = new string[10];

                int iSelCount = 0;
                int iCut = 0;

                //每段笔数
                int iExcCount = 10;

                //*行计数器
                int k = 0;
                Decimal Len = 1;

                if (dTableDetail != null)
                {
                    if (dTableDetail.Rows.Count != 0)
                    {
                        Len = Convert.ToDecimal(dTableDetail.Rows.Count + 1);

                        foreach (DataRow DRow in dTableDetail.Rows)
                        {
                            k++;
                            Percent = Convert.ToInt32(k / Len * 100);

                            #region 取值
                            int iiCommID = AppGlobal.StrToInt(DRow["CommID"].ToString());
                            long iiCustID = AppGlobal.StrToLong(DRow["CustID"].ToString());
                            long iiRoomID = AppGlobal.StrToLong(DRow["RoomID"].ToString());
                            long iiCostID = AppGlobal.StrToLong(DRow["CostID"].ToString());
                            decimal iiPrecAmount = AppGlobal.StrToDec(DRow["PrecAmount"].ToString());
                            string strPrecMemo = DRow["PrecMemo"].ToString();
                            long iiHandID = AppGlobal.StrToLong(DRow["HandID"].ToString());

                            string strTmpCostIDs = DRow["CostIDs"].ToString();
                            string strTmpHandIDs = DRow["HandIDs"].ToString();

                            #endregion

                            #region 合计
                            iTotalAmount = iTotalAmount + iiPrecAmount;

                            if (iiPrecAmount > 0)
                            {
                                iFeesLimitCount++;
                            }
                            #endregion

                            //有需要预交的费用
                            if (iiPrecAmount > 0)
                            {
                                #region 明细生成

                                ChildSQLEx = " exec " + strProcName + " " + iiCommID.ToString() + ","
                                    + iiCustID.ToString() + ","
                                    + iiRoomID.ToString() + ","
                                    + iiCostID.ToString() + ","
                                    + iiPrecAmount.ToString() + ","
                                    + "$ReceID$" + ","
                                    + "N'" + strPrecMemo + "',"
                                    + iiHandID.ToString() + ","
                                    + "N'" + strTmpCostIDs + "',"
                                    + "N'" + strTmpHandIDs + "'"
                                    + "\r\n";

                                #region 分成多段
                                if ((iSelCount < (iExcCount * (iCut + 1))) && (iSelCount >= iExcCount * iCut))
                                {
                                    arrSQLEx[iCut] = arrSQLEx[iCut] + ChildSQLEx;
                                }

                                iSelCount++;

                                if (iSelCount >= (iExcCount * (iCut + 1)))
                                {
                                    iCut++;
                                }

                                #endregion

                                #endregion
                            }
                        }

                        //有需要预交的费用
                        if (iFeesLimitCount > 0)
                        {
                            #region 票据
                            DataTable dTable = HSPR_BillUse_GetBillsSignUseRange(iCommID, "", "POS机端");

                            if (dTable.Rows.Count > 0)
                            {
                                DataRow DRow = dTable.Rows[0];

                                iBillTypeID = AppGlobal.StrToLong(DRow["BillTypeID"].ToString());
                                strBillsSign = DRow["BillsSign"].ToString();
                                HitResult = strBillsSign;
                            }
                            dTable.Dispose();
                            #endregion

                            //预交收据
                            iReceID = HSPR_PreCostsReceipts_InsertAuto(iCommID, iCustID, iRoomID, strBillsSign
                                , iPrintTimes, strBillsDate, strUserCode, strChargeMode, iAccountWay, strReceMemo, iTotalAmount, iIsDelete
                                , strInvoiceBill, strInvoiceUnit, strRemitterUnit, strBankName, strBankAccount, strChequeBill, iBillTypeID
                                , arrSQLEx[0], arrSQLEx[1], arrSQLEx[2], arrSQLEx[3], arrSQLEx[4], arrSQLEx[5], arrSQLEx[6], arrSQLEx[7], arrSQLEx[8], arrSQLEx[9]);

                            #region 收据记录
                            if (strBillsSign != "")
                            {
                                if (iReceID != 0)
                                {
                                    int iSourceType = 2;//预交收据
                                    HSPR_BillUseInstead_InsUpdate(iCommID, iBillTypeID, strBillsSign, strUserCode, iReceID, iSourceType, TypeRule.TWBillUseCase.PreCosts);
                                }

                                //更新号段数量和金额
                                if ((iBillTypeID != 0) && (strBillsSign != ""))
                                {
                                    HSPR_BillUse_UpdateData(iCommID, iBillTypeID, strBillsSign);
                                }
                            }
                            #endregion

                            #region  回發票據號
                            string ContionStringr = ConnectionString;
                            string strSqlr = " select BillsSign,BillsDate   from Tb_HSPR_PreCostsReceipts where receid='" + iReceID + "' ";
                            DataTable dtr = new DbHelperSQLP(PubConstant.GetConnectionString("SQLConnection").ToString()).Query(strSqlr).Tables[0];
                            if (dtr.Rows.Count > 0)
                            {
                                DataRow DRow = dtr.Rows[0];
                                strBillsSign = DRow["BillsSign"].ToString();
                                HitResult = strBillsSign;
                                strBillsDate = DRow["BillsDate"].ToString();
                            }

                            #endregion

                            if (iReceID != 0)
                            {
                                IsPrint = true;
                            }
                            else
                            {
                                HitResult = "预存费用失败！";
                                IsPrint = false;
                            }
                        }
                    }
                    else
                    {
                        HitResult = "请添加需要预交的费用！";

                        IsPrint = false;
                    }
                }

            }
            catch (Exception ex)
            {

                ErrMsg = ex.Message;

            }
            finally
            {

            }

            if (IsPrint == true)
            {
                iReturn = 1;
            }
            else
            {
                iReturn = 0;
            }


            return iReturn;

        }
        #endregion



        public static DataTable HSPR_PreCostsDetail_Filter(string SQLEx)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@SQLEx", SqlDbType.VarChar),
            };
            parameters[0].Value = SQLEx;
            DataSet Ds = new DbHelperSQLP(ConnectionString).RunProcedure("Proc_HSPR_PreCostsDetail_Filter", parameters, "Ds");
            return Ds.Tables[0];
        }

        public static DataTable HSPR_BillUse_GetBillsSignUseRange(int CommID, string UserCode, string UseRange)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@CommID", SqlDbType.Int),
                    new SqlParameter("@UserCode", SqlDbType.VarChar),
                    new SqlParameter("@UseRange", SqlDbType.VarChar),
            };
            parameters[0].Value = CommID;
            parameters[1].Value = UserCode;
            parameters[2].Value = UseRange;

            DataSet Ds = new DbHelperSQLP(ConnectionString).RunProcedure("Proc_HSPR_BillUse_GetBillsSignUseRange", parameters, "Ds");
            return Ds.Tables[0];
        }

        public static long HSPR_PreCostsReceipts_InsertAuto(int CommID, long CustID, long RoomID, string BillsSign, int PrintTimes
            , string BillsDate, string UserCode, string ChargeMode, int AccountWay, string ReceMemo, decimal PrecAmount, int IsDelete
            , string InvoiceBill, string InvoiceUnit, string RemitterUnit, string BankName, string BankAccount, string ChequeBill, long BillTypeID
            , string SQLEx1, string SQLEx2, string SQLEx3, string SQLEx4, string SQLEx5, string SQLEx6, string SQLEx7, string SQLEx8, string SQLEx9, string SQLEx10)
        {
            long iReceID = 0;

            SqlParameter[] parameters = {
                    new SqlParameter("@CommID", SqlDbType.Int),
                    new SqlParameter("@CustID", SqlDbType.VarChar),
                    new SqlParameter("@RoomID", SqlDbType.VarChar),
                    new SqlParameter("@BillsSign", SqlDbType.VarChar),
                    new SqlParameter("@PrintTimes", SqlDbType.VarChar),
                    new SqlParameter("@BillsDate", SqlDbType.VarChar),
                    new SqlParameter("@UserCode", SqlDbType.VarChar),
                    new SqlParameter("@ChargeMode", SqlDbType.VarChar),
                    new SqlParameter("@AccountWay", SqlDbType.VarChar),
                    new SqlParameter("@ReceMemo", SqlDbType.VarChar),
                    new SqlParameter("@PrecAmount", SqlDbType.VarChar),
                    new SqlParameter("@IsDelete", SqlDbType.VarChar),
                    new SqlParameter("@InvoiceBill", SqlDbType.VarChar),
                    new SqlParameter("@InvoiceUnit", SqlDbType.VarChar),
                    new SqlParameter("@RemitterUnit", SqlDbType.VarChar),
                    new SqlParameter("@BankName", SqlDbType.VarChar),
                    new SqlParameter("@BankAccount", SqlDbType.VarChar),
                    new SqlParameter("@ChequeBill", SqlDbType.VarChar),
                    new SqlParameter("@BillTypeID", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx1", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx2", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx3", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx4", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx5", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx6", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx7", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx8", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx9", SqlDbType.VarChar),
                    new SqlParameter("@SQLEx10", SqlDbType.VarChar),
                    new SqlParameter("@ReceID", SqlDbType.BigInt, 0,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null)

            };
            parameters[0].Value = CommID;
            parameters[1].Value = CustID;
            parameters[2].Value = RoomID;

            parameters[3].Value = BillsSign;
            parameters[4].Value = PrintTimes;
            parameters[5].Value = BillsDate;

            parameters[6].Value = UserCode;
            parameters[7].Value = ChargeMode;
            parameters[8].Value = AccountWay;

            parameters[9].Value = ReceMemo;
            parameters[10].Value = PrecAmount;
            parameters[11].Value = IsDelete;

            parameters[12].Value = InvoiceBill;
            parameters[13].Value = InvoiceUnit;
            parameters[14].Value = RemitterUnit;

            parameters[15].Value = BankName;
            parameters[16].Value = BankAccount;
            parameters[17].Value = ChequeBill;

            parameters[18].Value = BillTypeID;
            parameters[19].Value = SQLEx1;

            parameters[20].Value = SQLEx2;
            parameters[21].Value = SQLEx3;
            parameters[22].Value = SQLEx4;

            parameters[23].Value = SQLEx5;
            parameters[24].Value = SQLEx6;
            parameters[25].Value = SQLEx7;

            parameters[26].Value = SQLEx8;
            parameters[27].Value = SQLEx9;
            parameters[28].Value = SQLEx10;

            DataSet Ds = new DbHelperSQLP(ConnectionString).RunProcedure("Proc_HSPR_PreCostsReceipts_InsertAuto", parameters, "Ds");

            iReceID = Convert.ToInt64(parameters[29].Value);

            return iReceID;
        }

        public static void HSPR_BillUseInstead_InsUpdate(int CommID, long BillTypeID, string BillsSign, string UserCode, long SourceID, int SourceType, int BillUseCase)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@CommID", SqlDbType.Int),
                    new SqlParameter("@BillTypeID", SqlDbType.VarChar),
                    new SqlParameter("@BillsSign", SqlDbType.VarChar),
                    new SqlParameter("@UserCode", SqlDbType.VarChar),
                    new SqlParameter("@SourceID", SqlDbType.VarChar),
                    new SqlParameter("@SourceType", SqlDbType.VarChar),
                    new SqlParameter("@BillUseCase", SqlDbType.VarChar)
            };
            parameters[0].Value = CommID;
            parameters[1].Value = BillTypeID;
            parameters[2].Value = BillsSign;
            parameters[3].Value = UserCode;
            parameters[4].Value = SourceID;
            parameters[5].Value = SourceType;
            parameters[6].Value = BillUseCase;

            DataSet Ds = new DbHelperSQLP(ConnectionString).RunProcedure("Proc_HSPR_BillUseInstead_InsUpdate", parameters, "Ds");
        }

        public static void HSPR_BillUse_UpdateData(int CommID, long BillTypeID, string BillsSign)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@CommID", SqlDbType.Int),
                    new SqlParameter("@BillTypeID", SqlDbType.VarChar),
                    new SqlParameter("@BillsSign", SqlDbType.VarChar)
            };
            parameters[0].Value = CommID;
            parameters[1].Value = BillTypeID;
            parameters[2].Value = BillsSign;
            DataSet Ds = new DbHelperSQLP(ConnectionString).RunProcedure("Proc_HSPR_BillUse_UpdateData", parameters, "Ds");
        }

    }
}
