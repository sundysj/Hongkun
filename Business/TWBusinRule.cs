using System;
using System.Collections.Generic;
using System.Text;
using KernelDev;
using KernelDev.DataAccess;
using System.Data;
using Model.HSPR;
using Model.Mt;
using System.Data.SqlClient;

namespace Business
{
    public class TWBusinRule
    {
        DataAccess DAccess;

        #region 构造
        public TWBusinRule()
        {
            DAccess = new DataAccess();
        }

        public TWBusinRule(string ConnectionString)
        {
            try
            {
                DAccess = new DataAccess(ConnectionString);
            }
            catch
            {
                throw new NullReferenceException("未将对象引用设置到对象的实例！");
            }

        }
        #endregion        

        #region 动态编号生成

        public string BulidAutoDateCode(int length)
        {
            System.Threading.Thread.Sleep(5);
            System.DateTime now = System.DateTime.Now;
            string strRe = now.ToString("yyyyMMddhhmmss") + BulidAutoCode("0123456789", length);

            return strRe;
        }

        public string BulidAutoCode(string seed, int length)
        {
            //申明变量
            string outRandomSting = "";
            string strSeed = seed;
            int seedLen;	//= seed.Length;
            int len = length;
            //处理变量
            if (strSeed == null || strSeed.Trim() == "")
            {
                strSeed = "0123456789";
                seedLen = strSeed.Length;
            }
            else
            {
                seedLen = strSeed.Length;
            }

            //开始产生要求长度的随机字符串
            while (len > 0)
            {
                //线程阻滞 10 毫秒后产生随机数,因为这里采用与时间相关的默认种子
                System.Threading.Thread.Sleep(10);
                Random rm = new Random();
                //rm.Next(min,max)是包括 [min,max) 的半开半闭区间
                outRandomSting += strSeed.Substring(rm.Next(0, seedLen), 1);
                len--;
            }
            return outRandomSting;
        }

        #endregion

        #region SQL注入

        public string FilteSQLStr(string Str)
        {
            //Str = Str.ToLower();

            //string[] Pattern = { "insert", "delete", "drop table", "update", "truncate", "xp_cmdshell","netlocalgroup", "administrators", "net user" };
            //for (int i = 0; i < Pattern.Length; i++)
            //{
            //    Str = Str.Replace(Pattern[i].ToLower(), "");
            //}
            return Str;
        }

        #endregion

        #region 存储过程分页

        public DataTable GetProcCutPages(out int Counts, out int pageCount, string tbName, string fldName, int pagesize, int page, string fldsort, int Sort, string strFilter, string ID, int Dist)
        {
            Counts = 0;
            pageCount = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "ListPage";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@tblName,@fldName,@pageSize,@page,@fldSort,@Sort,@strCondition,@ID,@Dist";

            dbParams.Add("tblName", tbName, 200);
            dbParams.Add("fldName", fldName, 500);
            dbParams.Add("pageSize", pagesize, SqlDbType.Int);
            dbParams.Add("page", page, SqlDbType.Int);
            dbParams.Add("fldSort", fldsort, 200);
            dbParams.Add("Sort", Sort, SqlDbType.Int);
            dbParams.Add("strCondition", FilteSQLStr(strFilter), 4000);
            dbParams.Add("ID", ID, 150);
            dbParams.Add("Dist", Dist, SqlDbType.Bit);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@Counts", SqlDbType.Int, 0);
            DAccess.RegisterOutParameter("@pageCount", SqlDbType.Int, 0);
            DataTable dTable = DAccess.DataTable();

            Counts = DAccess.GetInt32("Counts");
            pageCount = DAccess.GetInt32("pageCount");

            int i = dTable.Rows.Count;

            //*****关闭链接
            Dispose();

            return dTable;
        }

        public DataTable GetProcCutPagesPage(out int Counts, out int pageCount, string tbName, string fldName, int pagesize, int page, string fldsort, int Sort, string strFilter, string ID, int Dist)
        {
            string ExtA = "";

            Counts = 0;
            pageCount = 0;
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_TurnPage";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@tblName,@fldName,@pageSize,@page,@fldSort,@Sort,@strCondition,@ID,@Dist,@ExtA";

            dbParams.Add("tblName", tbName, 500);
            dbParams.Add("fldName", fldName, 500);
            dbParams.Add("pageSize", pagesize, SqlDbType.Int);
            dbParams.Add("page", page, SqlDbType.Int);
            dbParams.Add("fldSort", fldsort, 200);
            dbParams.Add("Sort", Sort, SqlDbType.Int);

            dbParams.Add("ID", ID, 150);
            dbParams.Add("Dist", Dist, SqlDbType.Bit);

            if (strFilter.Length > 4000)
            {
                ExtA = strFilter.Substring(3999);
                strFilter = strFilter.Substring(0, 3999);
            }

            dbParams.Add("strCondition", FilteSQLStr(strFilter), 4000);
            dbParams.Add("ExtA", FilteSQLStr(ExtA), 4000);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@Counts", SqlDbType.Int, 0);
            DAccess.RegisterOutParameter("@pageCount", SqlDbType.Int, 0);
            DataTable dTable = DAccess.DataTable();

            Counts = DAccess.GetInt32("Counts");
            pageCount = DAccess.GetInt32("pageCount");

            int i = dTable.Rows.Count;

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #endregion

        #region 系统企业用户
        public DataTable System_Corp_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_System_Corp_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, SqlDbType.NVarChar);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;

        }
        #endregion        

        #region 查询客户费用
        public DataTable TPay_CustFees_Filter(long UnCustID)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_TPay_CustFees_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@UnCustID";

            dbParams.Add("UnCustID", UnCustID, SqlDbType.BigInt);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 查询客户费用Sec
        public DataTable TPay_CustFees_SecFilter(string CustSynchcode)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_TPay_CustFees_SecFilter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CustSynchcode";

            dbParams.Add("CustSynchcode", CustSynchcode, SqlDbType.UniqueIdentifier);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 查询客户费用是否可收取
        public int TPay_CanFees_Filter(long FeesID)
        {
            int IsCan = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_TPay_CanFees_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@FeesID";

            dbParams.Add("FeesID", FeesID, SqlDbType.BigInt);

            DataTable dTable = DAccess.DataTable(dbParams);

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    IsCan = Convert.ToInt32(dTable.Rows[0][0].ToString());
                }
                catch
                {
                    IsCan = 0;
                }
            }
            dTable.Dispose();

            //*****关闭链接
            Dispose();

            return IsCan;
        }
        #endregion

        #region 查询收据
        public DataTable HSPR_FeesReceipts_Filter(string SQLEx)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_FeesReceipts_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);


            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 查询收据明细(已收)新
        public DataTable HSPR_NewFeesReceipts_DetailFilter(int CommID, long ReceID, int ReceiptType)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_NewFeesReceipts_DetailFilter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@ReceID,@ReceiptType";


            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("ReceID", ReceID, SqlDbType.BigInt);
            dbParams.Add("ReceiptType", ReceiptType, SqlDbType.Int);

            //**执行SQL存储过程
            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;

        }
        #endregion 

        #region 查询预收收据
        public DataTable HSPR_PreCostsReceipts_Filter(string SQLEx)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_PreCostsReceipts_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);


            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 一次完成预交

        public long HSPR_PreCostsReceipts_InsertAuto(int CommID, long CustID, long RoomID, string BillsSign, int PrintTimes
            , string BillsDate, string UserCode, string ChargeMode, int AccountWay, string ReceMemo, decimal PrecAmount, int IsDelete
            , string InvoiceBill, string InvoiceUnit, string RemitterUnit, string BankName, string BankAccount, string ChequeBill, long BillTypeID
            , string SQLEx1, string SQLEx2, string SQLEx3, string SQLEx4, string SQLEx5, string SQLEx6, string SQLEx7, string SQLEx8, string SQLEx9, string SQLEx10)
        {
            long iReceID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_PreCostsReceipts_InsertAuto";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@RoomID,@BillsSign,@PrintTimes,@BillsDate,@UserCode,@ChargeMode,@AccountWay,@ReceMemo,@PrecAmount,@IsDelete,@UseRepID,@InvoiceBill,@InvoiceUnit,@RemitterUnit,@BankName,@BankAccount,@ChequeBill,@BillTypeID,@SQLEx1,@SQLEx2,@SQLEx3,@SQLEx4,@SQLEx5,@SQLEx6,@SQLEx7,@SQLEx8,@SQLEx9,@SQLEx10";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("BillsSign", BillsSign, 20);
            dbParams.Add("PrintTimes", PrintTimes, SqlDbType.Int);
            dbParams.Add("BillsDate", BillsDate, SqlDbType.DateTime);
            dbParams.Add("UserCode", UserCode, 20);
            dbParams.Add("ChargeMode", ChargeMode, 20);
            dbParams.Add("AccountWay", AccountWay, SqlDbType.SmallInt);
            dbParams.Add("ReceMemo", ReceMemo, 500);

            dbParams.Add("PrecAmount", PrecAmount, SqlDbType.Decimal);
            dbParams.Add("IsDelete", IsDelete, SqlDbType.SmallInt);
            dbParams.Add("InvoiceBill", InvoiceBill, 20);
            dbParams.Add("InvoiceUnit", InvoiceUnit, 100);
            dbParams.Add("RemitterUnit", RemitterUnit, 100);
            dbParams.Add("BankName", BankName, 50);
            dbParams.Add("BankAccount", BankAccount, 30);
            dbParams.Add("ChequeBill", ChequeBill, 30);
            dbParams.Add("BillTypeID", BillTypeID, SqlDbType.BigInt);
            dbParams.Add("SQLEx1", SQLEx1, 4000);
            dbParams.Add("SQLEx2", SQLEx2, 4000);
            dbParams.Add("SQLEx3", SQLEx3, 4000);
            dbParams.Add("SQLEx4", SQLEx4, 4000);
            dbParams.Add("SQLEx5", SQLEx5, 4000);
            dbParams.Add("SQLEx6", SQLEx6, 4000);
            dbParams.Add("SQLEx7", SQLEx7, 4000);
            dbParams.Add("SQLEx8", SQLEx8, 4000);
            dbParams.Add("SQLEx9", SQLEx9, 4000);
            dbParams.Add("SQLEx10", SQLEx10, 4000);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@ReceID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            iReceID = DAccess.GetInt64("ReceID");

            //*****关闭链接
            Dispose();

            return iReceID;

        }


        #endregion

        #region 票据(使用范围)

        public DataTable HSPR_BillUse_GetBillsSignUseRange(int CommID, string UserCode, string UseRange)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_BillUse_GetBillsSignUseRange";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@UserCode,@UseRange";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("UserCode", UserCode, 20);
            dbParams.Add("UseRange", UseRange, 50);

            //**执行SQL存储过程
            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }


        #endregion

        #region 预收费全部选择的

        public DataTable HSPR_Fees_ReceivePreAll(int CommID, string FeesIDs, int IsOffset, int RenderType)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_ReceivePreAll";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@FeesIDs,@IsOffset,@RenderType";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("FeesIDs", FeesIDs, 4000);
            dbParams.Add("IsOffset", IsOffset, SqlDbType.SmallInt);
            dbParams.Add("RenderType", RenderType, SqlDbType.SmallInt);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;

        }

        #endregion

        #region 单户预交冲抵

        public Decimal HSPR_Fees_OffsetPreOne(int CommID, long FeesID, Decimal ThisAmount, Decimal LateFeeAmount, string UserCode, string ReceCode)
        {
            Decimal iAmount = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_OffsetPreOne";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@FeesID,@CommID,@ThisAmount,@LateFeeAmount,@UserCode,@ReceCode";

            dbParams.Add("FeesID", FeesID, SqlDbType.BigInt);
            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("ThisAmount", ThisAmount, SqlDbType.Decimal);
            dbParams.Add("LateFeeAmount", LateFeeAmount, SqlDbType.Decimal);
            dbParams.Add("UserCode", UserCode, 20);
            dbParams.Add("ReceCode", ReceCode, SqlDbType.UniqueIdentifier);

            DataTable dTable = DAccess.DataTable(dbParams);

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    iAmount = Convert.ToDecimal(dTable.Rows[0][0].ToString());
                }
                catch
                {

                }
            }
            dTable.Dispose();

            //*****关闭链接
            Dispose();

            return iAmount;

        }

        #endregion

        #region 费用结余
        public DataTable HSPR_FeesSurplus_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_FeesSurplus_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 费用节余设置
        public DataTable HSPR_FeesSurplusSetting_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_FeesSurplusSetting_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 银行代收记录
        public DataTable HSPR_BankSurr_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_BankSurr_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 一次完成收费(垫付)

        public long HSPR_AdvanceFeesReceipts_InsertAuto(int CommID, long CustID, long RoomID, string BillsSign, int PrintTimes
            , string BillsDate, string UserCode, string ChargeMode, int AccountWay, string ReceMemo, decimal PerSurplus
            , decimal SurplusAmount, decimal PrecAmount, decimal PaidAmount, int IsDelete, long UseRepID
            , string InvoiceBill, string InvoiceUnit, string RemitterUnit, string BankName, string BankAccount
            , string ChequeBill, int RenderType, long RenderCustID, string RenderCustName, long BillTypeID
            , string SQLEx1, string SQLEx2, string SQLEx3, string SQLEx4, string SQLEx5)
        {
            long iReceID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_AdvanceFeesReceipts_InsertAuto";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@RoomID,@BillsSign,@PrintTimes,@BillsDate,@UserCode,@ChargeMode,@AccountWay,@ReceMemo,@PerSurplus,@SurplusAmount,@PrecAmount,@PaidAmount,@IsDelete,@UseRepID,@InvoiceBill,@InvoiceUnit,@RemitterUnit,@BankName,@BankAccount,@ChequeBill,@RenderType,@RenderCustID,@RenderCustName,@BillTypeID,@SQLEx1,@SQLEx2,@SQLEx3,@SQLEx4,@SQLEx5";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("BillsSign", BillsSign, 20);
            dbParams.Add("PrintTimes", PrintTimes, SqlDbType.Int);
            dbParams.Add("BillsDate", BillsDate, SqlDbType.DateTime);
            dbParams.Add("UserCode", UserCode, 20);
            dbParams.Add("ChargeMode", ChargeMode, 20);
            dbParams.Add("AccountWay", AccountWay, SqlDbType.SmallInt);
            dbParams.Add("ReceMemo", ReceMemo, 500);
            dbParams.Add("PerSurplus", PerSurplus, SqlDbType.Decimal);
            dbParams.Add("SurplusAmount", SurplusAmount, SqlDbType.Decimal);
            dbParams.Add("PrecAmount", PrecAmount, SqlDbType.Decimal);
            dbParams.Add("PaidAmount", PaidAmount, SqlDbType.Decimal);
            dbParams.Add("IsDelete", IsDelete, SqlDbType.SmallInt);
            dbParams.Add("UseRepID", UseRepID, SqlDbType.BigInt);
            dbParams.Add("InvoiceBill", InvoiceBill, 20);
            dbParams.Add("InvoiceUnit", InvoiceUnit, 100);
            dbParams.Add("RemitterUnit", RemitterUnit, 100);
            dbParams.Add("BankName", BankName, 50);
            dbParams.Add("BankAccount", BankAccount, 30);
            dbParams.Add("ChequeBill", ChequeBill, 30);
            dbParams.Add("RenderType", RenderType, SqlDbType.SmallInt);
            dbParams.Add("RenderCustID", RenderCustID, SqlDbType.BigInt);
            dbParams.Add("RenderCustName", RenderCustName, 50);
            dbParams.Add("BillTypeID", BillTypeID, SqlDbType.BigInt);
            dbParams.Add("SQLEx1", SQLEx1, 4000);
            dbParams.Add("SQLEx2", SQLEx2, 4000);
            dbParams.Add("SQLEx3", SQLEx3, 4000);
            dbParams.Add("SQLEx4", SQLEx4, 4000);
            dbParams.Add("SQLEx5", SQLEx5, 4000);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@ReceID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            iReceID = DAccess.GetInt64("ReceID");

            //*****关闭链接
            Dispose();

            return iReceID;

        }


        #endregion

        #region 一次完成收费(自付)

        public long HSPR_FeesReceipts_InsertAuto(int CommID, long CustID, long RoomID, string BillsSign, int PrintTimes
            , string BillsDate, string UserCode, string ChargeMode, int AccountWay, string ReceMemo, decimal PerSurplus
            , decimal SurplusAmount, decimal PrecAmount, decimal PaidAmount, int IsDelete, long UseRepID
            , string InvoiceBill, string InvoiceUnit, string RemitterUnit, string BankName, string BankAccount
            , string ChequeBill, int RenderType, long RenderCustID, string RenderCustName, string ReceCode, long BillTypeID
            , string SQLEx1, string SQLEx2, string SQLEx3, string SQLEx4, string SQLEx5)
        {
            long iReceID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_FeesReceipts_InsertAuto";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@RoomID,@BillsSign,@PrintTimes,@BillsDate,@UserCode,@ChargeMode,@AccountWay,@ReceMemo,@PerSurplus,@SurplusAmount,@PrecAmount,@PaidAmount,@IsDelete,@UseRepID,@InvoiceBill,@InvoiceUnit,@RemitterUnit,@BankName,@BankAccount,@ChequeBill,@RenderType,@RenderCustID,@RenderCustName,@ReceCode,@BillTypeID,@SQLEx1,@SQLEx2,@SQLEx3,@SQLEx4,@SQLEx5";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("BillsSign", BillsSign, 20);
            dbParams.Add("PrintTimes", PrintTimes, SqlDbType.Int);
            dbParams.Add("BillsDate", BillsDate, SqlDbType.DateTime);
            dbParams.Add("UserCode", UserCode, 20);
            dbParams.Add("ChargeMode", ChargeMode, 20);
            dbParams.Add("AccountWay", AccountWay, SqlDbType.SmallInt);
            dbParams.Add("ReceMemo", ReceMemo, 500);
            dbParams.Add("PerSurplus", PerSurplus, SqlDbType.Decimal);
            dbParams.Add("SurplusAmount", SurplusAmount, SqlDbType.Decimal);
            dbParams.Add("PrecAmount", PrecAmount, SqlDbType.Decimal);
            dbParams.Add("PaidAmount", PaidAmount, SqlDbType.Decimal);
            dbParams.Add("IsDelete", IsDelete, SqlDbType.SmallInt);
            dbParams.Add("UseRepID", UseRepID, SqlDbType.BigInt);
            dbParams.Add("InvoiceBill", InvoiceBill, 20);
            dbParams.Add("InvoiceUnit", InvoiceUnit, 100);
            dbParams.Add("RemitterUnit", RemitterUnit, 100);
            dbParams.Add("BankName", BankName, 50);
            dbParams.Add("BankAccount", BankAccount, 30);
            dbParams.Add("ChequeBill", ChequeBill, 30);
            dbParams.Add("RenderType", RenderType, SqlDbType.SmallInt);
            dbParams.Add("RenderCustID", RenderCustID, SqlDbType.BigInt);
            dbParams.Add("RenderCustName", RenderCustName, 50);
            dbParams.Add("ReceCode", ReceCode, SqlDbType.UniqueIdentifier);
            dbParams.Add("BillTypeID", BillTypeID, SqlDbType.BigInt);
            dbParams.Add("SQLEx1", SQLEx1, 4000);
            dbParams.Add("SQLEx2", SQLEx2, 4000);
            dbParams.Add("SQLEx3", SQLEx3, 4000);
            dbParams.Add("SQLEx4", SQLEx4, 4000);
            dbParams.Add("SQLEx5", SQLEx5, 4000);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@ReceID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            iReceID = DAccess.GetInt64("ReceID");

            //*****关闭链接
            Dispose();

            return iReceID;

        }


        #endregion

        #region 换票登记

        public void HSPR_BillUseInstead_InsUpdate(int CommID, long BillTypeID, string BillsSign, string UserCode, long SourceID, int SourceType, int BillUseCase)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_BillUseInstead_InsUpdate";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@BillTypeID,@BillsSign,@UserCode,@SourceID,@SourceType,@BillUseCase";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("BillTypeID", BillTypeID, SqlDbType.BigInt);
            dbParams.Add("BillsSign", BillsSign, 20);
            dbParams.Add("UserCode", UserCode, 20);
            dbParams.Add("SourceID", SourceID, SqlDbType.BigInt);
            dbParams.Add("SourceType", SourceType, SqlDbType.SmallInt);
            dbParams.Add("BillUseCase", BillUseCase, SqlDbType.SmallInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 更新号段的数量和金额

        public void HSPR_BillUse_UpdateData(int CommID, long BillTypeID, string BillsSign)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_BillUse_UpdateData";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@BillTypeID,@BillsSign";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("BillTypeID", BillTypeID, SqlDbType.BigInt);
            dbParams.Add("BillsSign", BillsSign, 20);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 收费换票登记

        public void HSPR_BillUseInstead_FixInsUpdate(int CommID, long BillTypeID, string BillsSign, string UserCode, long SourceID, int SourceType, int BillUseCase)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_BillUseInstead_FixInsUpdate";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@BillTypeID,@BillsSign,@UserCode,@SourceID,@SourceType,@BillUseCase";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("BillTypeID", BillTypeID, SqlDbType.BigInt);
            dbParams.Add("BillsSign", BillsSign, 20);
            dbParams.Add("UserCode", UserCode, 20);
            dbParams.Add("SourceID", SourceID, SqlDbType.BigInt);
            dbParams.Add("SourceType", SourceType, SqlDbType.SmallInt);
            dbParams.Add("BillUseCase", BillUseCase, SqlDbType.SmallInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 多种收款方式生成
        public void HSPR_FeesReceiptsMode_InsUpdate(int CommID, long ReceID, string ChargeMode, decimal ChargeAmount)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_FeesReceiptsMode_InsUpdate";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@ReceID,@ChargeMode,@ChargeAmount";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("ReceID", ReceID, SqlDbType.BigInt);
            dbParams.Add("ChargeMode", ChargeMode, 20);
            dbParams.Add("ChargeAmount", ChargeAmount, SqlDbType.Decimal);


            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 查询统一库客户
        public DataTable Unify_Customer_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Unify_Customer_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 查询统一库公司
        public DataTable Unify_Corp_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Unify_Corp_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 查询统一库小区
        public DataTable Unify_Community_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Unify_Community_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 查询小区所在城市

        public DataTable HSPR_CommCity_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CommCity_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页
        public DataTable HSPR_CommCity_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_CommCity_Filter";
            string fldName = "*";
            string fldsort = "BoroughID";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "BoroughID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }
        #endregion

        #endregion

        #region 查询组团
        public DataTable HSPR_Region_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Region_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 查询小区

        public DataTable HSPR_Community_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Community_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页
        public DataTable HSPR_Community_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_Community_Filter";
            string fldName = "*";
            string fldsort = "OrderCode,CommID";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "CommID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }
        #endregion

        #endregion

        #region 查询楼宇

        public DataTable HSPR_Building_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Building_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        /// <summary>
        /// 分页
        /// </summary>
        public DataTable HSPR_Building_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_Building_Filter";
            string fldName = "*";
            string fldsort = "CommID,BuildSNum";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "BuildID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }

        #endregion

        #endregion

        #region 查询楼宇管家

        public DataTable HSPR_BuildingkeeperYD_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_BuildingkeeperYD_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }



        #endregion

        #region 查询单元

        public DataTable HSPR_Unit_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Unit_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        /// <summary>
        /// 分页
        /// </summary>
        public DataTable HSPR_Unit_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_Unit_Filter";
            string fldName = "*";
            string fldsort = "CommID,BuildSNum,UnitSNum";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "UnitCode";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }

        #endregion

        #endregion

        #region 查询电话
        public DataTable HSPR_Telephone_TelFilter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Telephone_TelFilter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, SqlDbType.NVarChar);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;

        }
        #endregion        

        #region 查询客户

        public DataTable HSPR_Customer_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Customer_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        public void HSPR_CustomerUpdate_HJ(int CommID, string RoomSign, string CustName, string PaperCode, string MobilePhone)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CustomerUpdate_HJ";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@RoomSign,@CustName,@PaperCode,@MobilePhone";

            dbParams.Add("CommID", CommID, SqlDbType.BigInt);
            dbParams.Add("RoomSign", RoomSign, SqlDbType.VarChar);
            dbParams.Add("CustName", CustName, SqlDbType.NVarChar);
            dbParams.Add("PaperCode", PaperCode, SqlDbType.NVarChar);
            dbParams.Add("MobilePhone", MobilePhone, SqlDbType.NVarChar);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();


        }

        public void HSPR_CustomerUpdate_XXW(string CustID, string PaperCode, string MobilePhone)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CustomerUpdate_XXW";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CustID,@PaperCode,@MobilePhone";

            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("PaperCode", PaperCode, SqlDbType.NVarChar);
            dbParams.Add("MobilePhone", MobilePhone, SqlDbType.NVarChar);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();


        }
        public void HSPR_HouseHoldUpdate_XXW(string HoldID, string MemberName, string PaperCode, string MobilePhone, string Relationships)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Household_Update_XXW";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@HoldID,@MemberName,@PaperCode,@MobilePhone,@Relationships";

            dbParams.Add("HoldID", HoldID, SqlDbType.BigInt);
            dbParams.Add("MemberName", MemberName, SqlDbType.NVarChar);
            dbParams.Add("PaperCode", PaperCode, SqlDbType.NVarChar);
            dbParams.Add("MobilePhone", MobilePhone, SqlDbType.NVarChar);
            dbParams.Add("Relationships", Relationships, SqlDbType.NVarChar);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 查询房屋

        public DataTable HSPR_Room_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Room_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        /// <summary>
        /// 分页
        /// </summary>
        public DataTable HSPR_Room_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_Room_Filter";
            string fldName = "*";
            string fldsort = "CommID,BuildSNum,UnitSNum,FloorSNum,RoomSNum,RoomSign";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "RoomID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }

        #endregion

        #endregion

        #region 查询车位

        public DataTable HSPR_Parking_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Parking_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #endregion

        #region 查询车位办理情况

        public DataTable HSPR_ParkingHand_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_ParkingHand_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #endregion

        #region 查询客户绑定
        #region 分页
        public DataTable HSPR_CustomerLive_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_CustomerLive_Filter";
            string fldName = "*";
            string fldsort = "CustID,RoomID";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "LiveID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }

        #endregion

        public DataTable HSPR_CustomerLive_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CustomerLive_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 查询家庭成员

        public DataTable HSPR_HouseHold_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Household_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页
        public DataTable HSPR_Household_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_Household_Filter";
            string fldName = "*";
            string fldsort = "HoldID";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "HoldID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }
        #endregion

        #endregion

        #region 房屋客户绑定

        public void HSPR_CustomerLive_InsUpdate(long LiveID, long RoomID, int CommID, long CustID, int LiveType)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CustomerLive_InsUpdate";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@LiveID,@RoomID,@CustID,@LiveType";

            dbParams.Add("LiveID", LiveID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("LiveType", LiveType, SqlDbType.SmallInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);



            DbParamters dbParams2 = new DbParamters();
            dbParams2.CommandText = "Proc_HSPR_Customer_UpdateLive";
            dbParams2.CommandType = CommandType.StoredProcedure;
            dbParams2.ProcParamters = "@CommID,@CustID";
            dbParams2.Add("CustID", CustID, SqlDbType.BigInt);//@RoomID bigint,
            dbParams2.Add("CommID", CommID, SqlDbType.Int);

            DAccess.Excute(dbParams2);
            //*****关闭链接
            Dispose();

        }


        #endregion

        #region 查询直呼管家信息

        public DataTable HSPR_ServPerson_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_ServPerson_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 主页信息分页

        /// <summary>
        /// 主页信息分页,用法:XXXXXXXXXXXXXXXXXXXXXX。
        /// </summary>
        public DataTable HSPR_ServPerson_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_ServPerson_Filter";
            string fldName = "*";
            string fldsort = "PersonSign";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "PersonID,";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }

        #endregion

        #endregion

        #region 查询小区信息

        public DataTable HSPR_CommunityInfo_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CommunityInfo_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 主页信息分页

        /// <summary>
        /// 主页信息分页,用法:XXXXXXXXXXXXXXXXXXXXXX。
        /// </summary>
        public DataTable HSPR_CommunityInfo_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_CommunityInfo_Filter";
            string fldName = "*";
            string fldsort = "IssueDate";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "InfoID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }

        #endregion

        #endregion

        #region 查询社区活动

        public DataTable HSPR_CommActivities_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CommActivities_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }



        #endregion

        #region 社区活动分页

        /// <summary>
        /// 主页信息分页,用法:XXXXXXXXXXXXXXXXXXXXXX。
        /// </summary>
        public DataTable HSPR_CommActivities_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "VIEW_HSPR_CommActivities_Filter";
            string fldName = "*";
            string fldsort = "ActivitiesEndDate";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "ActivitiesID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }

        #endregion

        #region 报事登记(HM)SEC
        public long HSPR_IncidentAccept_CustSecInsert(int CommID, long CustID, long RoomID, string IncidentDate, string IncidentContent, string IncidentMan, string Phone, string ReserveDate, string IncidentImgs, string CallIncidentType, string Eid)
        {
            long IncidentID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentAccept_CustSecInsert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@RoomID,@IncidentDate,@IncidentContent,@IncidentMan,@ReserveDate,@Phone,@IncidentImgs,@CallIncidentType,@Eid";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("IncidentDate", IncidentDate, SqlDbType.DateTime);
            dbParams.Add("IncidentContent", IncidentContent, 1000);
            dbParams.Add("IncidentMan", IncidentMan, 20);
            dbParams.Add("ReserveDate", ReserveDate, SqlDbType.DateTime);
            dbParams.Add("Phone", Phone, 50);
            dbParams.Add("IncidentImgs", IncidentImgs, 4000);
            dbParams.Add("CallIncidentType", CallIncidentType, 50);
            dbParams.Add("Eid", Eid, 50);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@IncidentID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            IncidentID = DAccess.GetInt64("IncidentID");

            //*****关闭链接
            Dispose();

            return IncidentID;


        }

        #endregion

        #region 鑫苑E家子在线报事
        public long HSPR_IncidentAcceptXYe_CustSecInsert(int CommID, long CustID, long RoomID, string IncidentDate, string IncidentContent, string IncidentMan, string Phone, string ReserveDate, string IncidentImgs, string CallIncidentType, string Eid, string TypeID)
        {
            long IncidentID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentAcceptXYe_CustSecInsert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@RoomID,@IncidentDate,@IncidentContent,@IncidentMan,@ReserveDate,@Phone,@IncidentImgs,@CallIncidentType,@Eid,@TypeID";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("IncidentDate", IncidentDate, SqlDbType.DateTime);
            dbParams.Add("IncidentContent", IncidentContent, 1000);
            dbParams.Add("IncidentMan", IncidentMan, 20);
            dbParams.Add("ReserveDate", ReserveDate, SqlDbType.DateTime);
            dbParams.Add("Phone", Phone, 50);
            dbParams.Add("IncidentImgs", IncidentImgs, 4000);
            dbParams.Add("CallIncidentType", CallIncidentType, 50);
            dbParams.Add("Eid", Eid, 50);
            dbParams.Add("TypeID", TypeID, 4000);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@IncidentID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            IncidentID = DAccess.GetInt64("IncidentID");

            //*****关闭链接
            Dispose();

            return IncidentID;


        }

        #endregion

        #region 报事催办
        public long HSPR_IncidentAccept_RemindersLCInsert(string strIncidentID, int CommID, string strInfoContent)
        {
            long IncidentID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentRemindersLC_Insert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@IncidentID,@CommID,@InfoContent";

            dbParams.Add("IncidentID", strIncidentID, 1000);
            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("InfoContent", strInfoContent, 1000);


            DAccess.PrepareCall(dbParams);

            //DAccess.RegisterOutParameter("@IncidentID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            //IncidentID = DAccess.GetInt64("IncidentID");
            IncidentID = Convert.ToInt64(strIncidentID);

            //*****关闭链接
            Dispose();

            return IncidentID;


        }

        #endregion

        #region 查询催办

        public DataTable HSPR_IncidentRemindersInfo_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentRemindersInfo_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #endregion

        #region 报事登记(投诉)(HM)THD
        public long HSPR_IncidentAccept_CustThdInsert(int CommID, long CustID, long RoomID
            , string IncidentDate, string IncidentContent, string IncidentMan
            , string Phone, string ReserveDate, string IncidentImgs)
        {
            long IncidentID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentAccept_CustThdInsert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@RoomID,@IncidentDate,@IncidentContent,@IncidentMan,@ReserveDate,@Phone,@IncidentImgs";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("IncidentDate", IncidentDate, SqlDbType.DateTime);
            dbParams.Add("IncidentContent", IncidentContent, 1000);
            dbParams.Add("IncidentMan", IncidentMan, 20);
            dbParams.Add("ReserveDate", ReserveDate, SqlDbType.DateTime);
            dbParams.Add("Phone", Phone, 50);
            dbParams.Add("IncidentImgs", IncidentImgs, 4000);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@IncidentID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            IncidentID = DAccess.GetInt64("IncidentID");

            //*****关闭链接
            Dispose();

            return IncidentID;


        }

        #endregion

        #region 报事登记(HM)FOUR
        public long HSPR_IncidentAccept_CustFourInsert(int CommID, long CustID, long RoomID
            , string IncidentDate, string IncidentContent, string IncidentMan
            , string Phone, string ReserveDate, string IncidentImgs
            , long CorpTypeID, string CallIncidentType, string Eid)
        {
            long IncidentID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentAccept_CustFourInsert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@RoomID,@IncidentDate,@IncidentContent,@IncidentMan,@ReserveDate,@Phone,@IncidentImgs,@CorpTypeID,@CallIncidentType,@Eid";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("IncidentDate", IncidentDate, SqlDbType.DateTime);
            dbParams.Add("IncidentContent", IncidentContent, 1000);
            dbParams.Add("IncidentMan", IncidentMan, 20);
            dbParams.Add("ReserveDate", ReserveDate, SqlDbType.DateTime);
            dbParams.Add("Phone", Phone, 50);
            dbParams.Add("IncidentImgs", IncidentImgs, 4000);
            dbParams.Add("CorpTypeID", CorpTypeID, SqlDbType.BigInt);
            dbParams.Add("CallIncidentType", CallIncidentType, 50);
            dbParams.Add("Eid", Eid, 50);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@IncidentID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            IncidentID = DAccess.GetInt64("IncidentID");

            //*****关闭链接
            Dispose();

            return IncidentID;


        }

        #endregion 

        #region 报事评价
        public void HSPR_IncidentAccept_UpdateAppraise(long IncidentID, string AppraiseContent, string CustComments)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentAccept_UpdateAppraise";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@IncidentID,@AppraiseContent,@CustComments";

            dbParams.Add("IncidentID", IncidentID, SqlDbType.BigInt);
            dbParams.Add("AppraiseContent", AppraiseContent, 50);
            dbParams.Add("CustComments", CustComments, 500);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 亿达报事评价(回访评价)
        public void HSPR_IncidentReplyYDAPP_Insert(long CommID, long IncidentID, string ServiceQuality, string ReplyContent)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentReplyYDAPP_Insert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@IncidentID,@ServiceQuality,@ReplyContent";

            dbParams.Add("CommID", CommID, SqlDbType.BigInt);
            dbParams.Add("IncidentID", IncidentID, SqlDbType.BigInt);
            dbParams.Add("ServiceQuality", ServiceQuality, 50);
            dbParams.Add("ReplyContent", ReplyContent, 500);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        

        #region 新希望接口
        #region 新希望联名客户
        public void HSPR_XxwHousehold_Insert(string JDENo,string fdNo, long CommID, string JointCustName, long CustID,string MobilePhone ,string Sex,string PaperName, string PaperCode, string Adderss, int IsDelete , string Memo, DateTime ChargeTime)
        {


            long HoldID = HSPR_XxwHousehold_GetMaxNum(CommID, "");


            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_XxwHousehold_Insert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@JDENo,@fdNo,@CommID,@JointCustName,@CustID,@MobilePhone,@Sex,@PaperName,@PaperCode,@Adderss,@IsDelete,@Memo,@ChargeTime";


            dbParams.Add("JDENo", JDENo, 20);
            dbParams.Add("fdNo", fdNo, 20);
            dbParams.Add("CommID", CommID, SqlDbType.BigInt);
            dbParams.Add("JointCustName", JointCustName, 20);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("MobilePhone", MobilePhone, 20);
            dbParams.Add("Sex", Sex, 20);
            dbParams.Add("PaperName", PaperName, 20);


            dbParams.Add("PaperCode", PaperCode, 20);
            dbParams.Add("Adderss", Adderss, 100);
            dbParams.Add("IsDelete", IsDelete, SqlDbType.SmallInt);
            dbParams.Add("Memo", Memo, 500);
            dbParams.Add("ChargeTime", ChargeTime, SqlDbType.DateTime);
           

           


            //**执行SQL存储过程
            DAccess.Excute(dbParams);


            //*****关闭链接
            Dispose();

        }


        #endregion


        //获取最大的业主ID
        public long HSPR_XxwHousehold_GetMaxNum(long CommID, string strSQL)
        {
            long MaxID = 0;
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_XxwHousehold_GetMaxNum";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@SQLEx";

            dbParams.Add("CommID", CommID, SqlDbType.BigInt);
            dbParams.Add("SQLEx", strSQL, SqlDbType.VarChar);
            //**执行SQL存储过程

            DataTable dTable = DAccess.DataTable(dbParams);

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    MaxID = Convert.ToInt64(dTable.Rows[0][0].ToString());
                }
                catch
                {

                }
            }

            dTable.Dispose();
            //*****关闭链接
            //Dispose();

            return MaxID;
        }
        #endregion



        #region 合景CRM(楼栋楼栋基础数据插入)
        public void HSPR_Building_Insert(long BuildID, long CommID, string BuildSign, string BuildName, string BuildType, string BuildUses, string PropertyRights, string PropertyUses, string BuildHeight, int FloorsNum, int UnitNum, int HouseholdsNum, int UnderFloorsNum, string NamingPatterns, int BuildSNum, int PerFloorNum, int RegionSNum, int IsDelete, int PropertyYear, DateTime CompleteDate, DateTime DeliveryTime, decimal BuildPermitArea, decimal PropertyArea, string MappingStatus, decimal PropertyFeeStandard)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Building_Insert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@BuildID,@CommID,@BuildSign,@BuildName,@BuildType,@BuildUses,@PropertyRights,@PropertyUses,@BuildHeight,@FloorsNum,@UnitNum,@HouseholdsNum,@UnderFloorsNum,@NamingPatterns,@BuildSNum,@PerFloorNum,@RegionSNum,@IsDelete,@PropertyYear,@CompleteDate,@DeliveryTime,@BuildPermitArea,@PropertyArea,@MappingStatus,@PropertyFeeStandard";

            dbParams.Add("BuildID", BuildID, SqlDbType.BigInt);
            dbParams.Add("CommID", CommID, SqlDbType.BigInt);
            dbParams.Add("BuildSign", BuildSign, 10);
            dbParams.Add("BuildName", BuildName, 20);
            dbParams.Add("BuildType", BuildType, 20);
            dbParams.Add("BuildUses", BuildUses, 20);
            dbParams.Add("PropertyRights", PropertyRights, 20);
            dbParams.Add("PropertyUses", PropertyUses, 20);
            dbParams.Add("BuildHeight", BuildHeight, 20);
            dbParams.Add("FloorsNum", FloorsNum, SqlDbType.Int);
            dbParams.Add("UnitNum", UnitNum, SqlDbType.Int);
            dbParams.Add("HouseholdsNum", HouseholdsNum, SqlDbType.Int);
            dbParams.Add("UnderFloorsNum", UnderFloorsNum, SqlDbType.Int);//HouseholdsNum
            dbParams.Add("NamingPatterns", NamingPatterns, 30);//NamingPatterns
            dbParams.Add("BuildSNum", BuildSNum, SqlDbType.Int);//BuildSNum
            dbParams.Add("PerFloorNum", PerFloorNum, SqlDbType.Int);//PerFloorNum
            dbParams.Add("RegionSNum", RegionSNum, SqlDbType.Int);//RegionSNum
            dbParams.Add("IsDelete", IsDelete, SqlDbType.Int);//RegionSNum
            dbParams.Add("PropertyYear", PropertyYear, SqlDbType.Int);//@CompleteDate datetime,
            dbParams.Add("CompleteDate", CompleteDate, SqlDbType.DateTime);
            dbParams.Add("DeliveryTime", DeliveryTime, SqlDbType.DateTime);
            dbParams.Add("BuildPermitArea", BuildPermitArea, SqlDbType.Decimal);
            dbParams.Add("PropertyArea", PropertyArea, SqlDbType.Decimal);
            dbParams.Add("MappingStatus", MappingStatus, 50);
            dbParams.Add("PropertyFeeStandard", PropertyFeeStandard, SqlDbType.Decimal);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion



        #region 合景CRM(楼栋房屋基础数据插入)
        public void HSPR_Room_Insert(long RoomID, long CommID, string RoomSign, string RoomName, int RegionSNum, int BuildSNum, int UnitSNum, int FloorSNum, int RoomSNum, string RoomModel, string RoomType, string PropertyRights, string RoomTowards, decimal BuildArea, decimal InteriorArea, decimal CommonArea,
           string RightsSign, string PropertyUses, string RoomState, long ChargeTypeID, int UsesState, string FloorHeight, string BuildStructure, decimal PoolRatio, string BearParameters, string Renovation, string Configuration, string Advertising, int IsDelete, int IsSplitUnite, decimal GardenArea,
           decimal PropertyArea, int AreaType, decimal YardArea, long BedTypeID, int UseType, DateTime getHouseStartDate, DateTime getHouseEndDate, string SaleState, int PayState
            , string ContSubDate, string TakeOverDate, string ActualSubDate, string FittingTime, string StayTime, string PayBeginDate, string ContractSign, string BuildsRenovation)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Room_Insert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@RoomID,@CommID,@RoomSign,@RoomName,@RegionSNum,@BuildSNum,@UnitSNum,@FloorSNum,@RoomSNum,@RoomModel,@RoomType,@PropertyRights,@RoomTowards,@BuildArea,@InteriorArea,@CommonArea,@RightsSign,@PropertyUses,@RoomState,@ChargeTypeID,@UsesState,@FloorHeight,@BuildStructure,@PoolRatio,@BearParameters,@Renovation,@Configuration,@Advertising,@IsDelete,@IsSplitUnite,@GardenArea,@PropertyArea,@AreaType,@YardArea,@BedTypeID,@UseType,@getHouseStartDate,@getHouseEndDate,@SaleState,@PayState,@ContSubDate,@TakeOverDate,@ActualSubDate,@FittingTime,@StayTime,@PayBeginDate,@ContractSign,@BuildsRenovation";


            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);//@RoomID bigint,
            dbParams.Add("CommID", CommID, SqlDbType.BigInt);
            dbParams.Add("RoomSign", RoomSign, 50);
            dbParams.Add("RoomName", RoomName, 200);
            dbParams.Add("RegionSNum", RegionSNum, SqlDbType.Int);
            dbParams.Add("BuildSNum", BuildSNum, SqlDbType.Int);
            dbParams.Add("UnitSNum", UnitSNum, SqlDbType.Int);
            dbParams.Add("FloorSNum", FloorSNum, SqlDbType.Int);
            dbParams.Add("RoomSNum", RoomSNum, SqlDbType.Int);
            dbParams.Add("RoomModel", RoomModel, 20);
            dbParams.Add("RoomType", RoomType, 20);
            dbParams.Add("PropertyRights", PropertyRights, 20);
            dbParams.Add("RoomTowards", RoomTowards, 10);
            dbParams.Add("BuildArea", BuildArea, SqlDbType.Decimal);
            dbParams.Add("InteriorArea", InteriorArea, SqlDbType.Decimal);
            dbParams.Add("CommonArea", CommonArea, SqlDbType.Decimal);
            dbParams.Add("RightsSign", RightsSign, 50);
            dbParams.Add("PropertyUses", PropertyUses, 20);
            dbParams.Add("RoomState", RoomState, SqlDbType.Int);
            dbParams.Add("ChargeTypeID", ChargeTypeID, SqlDbType.BigInt);
            dbParams.Add("UsesState", UsesState, SqlDbType.SmallInt);
            dbParams.Add("FloorHeight", FloorHeight, 30);
            dbParams.Add("BuildStructure", BuildStructure, 30);
            dbParams.Add("PoolRatio", PoolRatio, SqlDbType.Decimal);
            dbParams.Add("BearParameters", BearParameters, 30);
            dbParams.Add("Renovation", Renovation, 30);
            dbParams.Add("Configuration", Configuration, 80);
            dbParams.Add("Advertising", Advertising, 30);
            dbParams.Add("IsDelete", IsDelete, SqlDbType.SmallInt);
            dbParams.Add("IsSplitUnite", IsSplitUnite, SqlDbType.SmallInt);
            dbParams.Add("GardenArea", GardenArea, SqlDbType.Decimal);
            dbParams.Add("PropertyArea", PropertyArea, SqlDbType.Decimal);
            dbParams.Add("AreaType", AreaType, SqlDbType.SmallInt);
            dbParams.Add("YardArea", YardArea, SqlDbType.Decimal);
            dbParams.Add("BedTypeID", BedTypeID, SqlDbType.Decimal);
            dbParams.Add("UseType", UseType, SqlDbType.SmallInt);
            dbParams.Add("getHouseStartDate", getHouseStartDate, SqlDbType.DateTime);
            dbParams.Add("getHouseEndDate", getHouseEndDate, SqlDbType.DateTime);
            dbParams.Add("SaleState", SaleState, 50);
            dbParams.Add("PayState", PayState, SqlDbType.SmallInt);
            dbParams.Add("ContSubDate", ContSubDate, SqlDbType.DateTime);
            dbParams.Add("TakeOverDate", TakeOverDate, SqlDbType.DateTime);
            dbParams.Add("ActualSubDate", ActualSubDate, SqlDbType.DateTime);
            dbParams.Add("FittingTime", FittingTime, SqlDbType.DateTime);
            dbParams.Add("StayTime", StayTime, SqlDbType.DateTime);
            dbParams.Add("PayBeginDate", PayBeginDate, SqlDbType.DateTime);
            dbParams.Add("ContractSign", ContractSign, 20);
            dbParams.Add("BuildsRenovation", BuildsRenovation, 40);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 合景CRM(楼栋业主基础数据插入)
        public void HSPR_Customer_Insert(long CustID, int CommID, long CustTypeID, string CustName, string FixedTel, string MobilePhone, string FaxTel, string EMail, string Surname, string Name, string Sex, DateTime Birthday, string Nationality,
              string WorkUnit, string PaperName, string PaperCode, string PassSign, string LegalRepr, string LegalReprTel, string Charge, string ChargeTel, string Linkman, string LinkmanTel, string BankName, string BankIDs, string BankAccount,
              string BankAgreement, string InquirePwd, string InquireAccount, string Memo, int IsUnit, int IsDelete, string Address, string PostCode, string Recipient, string Hobbies, string Job, DateTime SendCardDate, int IsSendCard)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Customer_Insert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CustID,@CommID,@CustTypeID,@CustName,@FixedTel,@MobilePhone,@FaxTel,@EMail,@Surname,@Name,@Sex,@Birthday,@Nationality,@WorkUnit,@PaperName,@PaperCode,@PassSign,@LegalRepr,@LegalReprTel,@Charge,@ChargeTel,@Linkman,@LinkmanTel,@BankName,@BankIDs,@BankAccount,@BankAgreement,@InquirePwd,@InquireAccount,@Memo,@IsUnit,@IsDelete,@Address,@PostCode,@Recipient,@Hobbies,@Job,@SendCardDate,@IsSendCard";


            dbParams.Add("CustID", CustID, SqlDbType.BigInt);//@RoomID bigint,
            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustTypeID", CustTypeID, SqlDbType.BigInt);
            dbParams.Add("CustName", CustName, 50);
            dbParams.Add("FixedTel", FixedTel, 20);
            dbParams.Add("MobilePhone", MobilePhone, 500);
            dbParams.Add("FaxTel", FaxTel, 20);
            dbParams.Add("EMail", EMail, 50);
            dbParams.Add("Surname", Surname, 10);
            dbParams.Add("Name", Name, 20);
            dbParams.Add("Sex", Sex, 10);
            dbParams.Add("Birthday", Birthday, SqlDbType.DateTime);
            dbParams.Add("Nationality", Nationality, 30);
            dbParams.Add("WorkUnit", WorkUnit, 300);
            dbParams.Add("PaperName", PaperName, 20);
            dbParams.Add("PaperCode", PaperCode, 30);
            dbParams.Add("PassSign", PassSign, 20);
            dbParams.Add("LegalRepr", LegalRepr, 20);
            dbParams.Add("LegalReprTel", LegalReprTel, 20);
            dbParams.Add("Charge", Charge, 20);
            dbParams.Add("ChargeTel", ChargeTel, 20);
            dbParams.Add("Linkman", Linkman, 20);
            dbParams.Add("LinkmanTel", LinkmanTel, 20);
            dbParams.Add("BankName", BankName, 20);
            dbParams.Add("BankIDs", BankIDs, 50);
            dbParams.Add("BankAccount", BankAccount, 30);
            dbParams.Add("BankAgreement", BankAgreement, 20);
            dbParams.Add("InquirePwd", InquirePwd, 20);
            dbParams.Add("InquireAccount", InquireAccount, 20);
            dbParams.Add("Memo", Memo, 4000);
            dbParams.Add("IsUnit", IsUnit, SqlDbType.SmallInt);
            dbParams.Add("IsDelete", IsDelete, SqlDbType.SmallInt);
            dbParams.Add("Address", Address, 100);
            dbParams.Add("PostCode", PostCode, 10);
            dbParams.Add("Recipient", Recipient, 100);
            dbParams.Add("Hobbies", Hobbies, 200);
            dbParams.Add("Job", Job, 50);
            dbParams.Add("SendCardDate", SendCardDate, SqlDbType.DateTime);
            dbParams.Add("IsSendCard", IsSendCard, SqlDbType.SmallInt);


            //**执行SQL存储过程
            DAccess.Excute(dbParams);



        }

        #endregion



        #region 合景CRM(楼栋业主基础数据插入)
        public void HSPR_Household_Insert(int CommID, long CustID, long RoomID, string Surname, string Name, string Sex
            , string Birthday, string Nationality, string WorkUnit, string PaperName, string PaperCode, string PassSign, string MobilePhone
            , string Relationship, DateTime StayTime, string ChargeCause, DateTime ChargeTime, string InquirePwd, string InquireAccount, string Memo
            , string MemberCode, string MemberName, string Job, int IsDelete, string Linkman, string LinkManTel, string FixedTel)
        {


            long HoldID = HSPR_Household_GetMaxNum(CommID, "");


            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Household_Insert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@HoldID,@CommID,@CustID,@RoomID,@Surname,@Name,@Sex,@Birthday,@Nationality,@WorkUnit,@PaperName,@PaperCode,@PassSign,@MobilePhone,@Relationship,@StayTime,@ChargeCause,@ChargeTime,@InquirePwd,@InquireAccount,@Memo,@MemberCode,@MemberName,@IsDelete,@Job,@Linkman,@LinkManTel,@FixedTel";


            dbParams.Add("HoldID", HoldID, SqlDbType.BigInt);
            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("Surname", Surname, 10);
            dbParams.Add("Name", Name, 20);
            dbParams.Add("Sex", Sex, 10);
            dbParams.Add("Birthday", Birthday, SqlDbType.DateTime);
            dbParams.Add("Nationality", Nationality, 30);
            dbParams.Add("WorkUnit", WorkUnit, 300);
            dbParams.Add("PaperName", PaperName, 20);
            dbParams.Add("PaperCode", PaperCode, 30);
            dbParams.Add("PassSign", PassSign, 20);
            dbParams.Add("MobilePhone", MobilePhone, 500);
            dbParams.Add("Relationship", Relationship, 20);
            dbParams.Add("StayTime", StayTime, SqlDbType.DateTime);
            dbParams.Add("ChargeCause", ChargeCause, 50);
            dbParams.Add("ChargeTime", ChargeTime, SqlDbType.DateTime);
            dbParams.Add("InquirePwd", InquirePwd, 20);
            dbParams.Add("InquireAccount", InquireAccount, 20);
            dbParams.Add("Memo", Memo, 100);

            dbParams.Add("MemberCode", MemberCode, SqlDbType.BigInt);
            dbParams.Add("MemberName", MemberName, 50);
            dbParams.Add("Job", Job, 50);
            dbParams.Add("IsDelete", IsDelete, SqlDbType.SmallInt);
            dbParams.Add("Linkman", Linkman, 30);
            dbParams.Add("LinkManTel", LinkManTel, 20);
            dbParams.Add("FixedTel", FixedTel, 500);


            //**执行SQL存储过程
            DAccess.Excute(dbParams);


            //*****关闭链接
            Dispose();

        }

        #endregion




        //获取最大的楼栋编号
        public int HSPR_Building_GetSNum(int CommID)
        {
            int SID = 0;
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Building_GetSNum";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID";

            dbParams.Add("CommID", CommID, SqlDbType.Int);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    SID = Convert.ToInt32(dTable.Rows[0][0].ToString());
                }
                catch
                {

                }
            }
            return SID;
        }


        //获取最大的楼栋序号

        public long HSPR_Building_GetMaxNum(int CommID, string SQLEx)
        {
            long MaxID = 0;
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Building_GetMaxNum";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@SQLEx";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("SQLEx", SQLEx, SqlDbType.VarChar);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    MaxID = Convert.ToInt64(dTable.Rows[0][0].ToString());
                }
                catch
                {

                }
            }
            return MaxID;
        }
        //获取最大的房屋序号
        public long HSPR_Room_GetMaxNum(int CommID, string strSQL)
        {
            long MaxID = 0;
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Room_GetMaxNum";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@SQLEx";
            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("SQLEx", strSQL, SqlDbType.VarChar);

            //**执行SQL存储过程

            DataTable dTable = DAccess.DataTable(dbParams);

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    MaxID = Convert.ToInt64(dTable.Rows[0][0].ToString());
                }
                catch
                {

                }
            }

            dTable.Dispose();
            //*****关闭链接
            //Dispose();

            return MaxID;
        }

        //获取最大的业主ID
        public long HSPR_Customer_GetMaxNum(int CommID, string strSQL)
        {
            long MaxID = 0;
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Customer_GetMaxNum";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@SQLEx";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("SQLEx", strSQL, SqlDbType.VarChar);
            //**执行SQL存储过程

            DataTable dTable = DAccess.DataTable(dbParams);

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    MaxID = Convert.ToInt64(dTable.Rows[0][0].ToString());
                }
                catch
                {

                }
            }

            dTable.Dispose();
            //*****关闭链接
            //Dispose();

            return MaxID;
        }

        //获取最大的业主ID
        public long HSPR_Household_GetMaxNum(int CommID, string strSQL)
        {
            long MaxID = 0;
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Household_GetMaxNum";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@SQLEx";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("SQLEx", strSQL, SqlDbType.VarChar);
            //**执行SQL存储过程

            DataTable dTable = DAccess.DataTable(dbParams);

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    MaxID = Convert.ToInt64(dTable.Rows[0][0].ToString());
                }
                catch
                {

                }
            }

            dTable.Dispose();
            //*****关闭链接
            //Dispose();

            return MaxID;
        }



        #region 查询公司报事类别

        public DataTable HSPR_CorpIncidentType_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CorpIncidentType_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }



        #endregion

        #region 查询报事

        public DataTable HSPR_IncidentAccept_SearchFilter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentAccept_SearchFilter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        public DataTable HSPR_IncidentAccept_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_IncidentAccept_SearchFilter";
            string fldName = "*";
            string fldsort = "IncidentDate";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "IncidentID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }


        #endregion

        #endregion

        #region 查询报事类别

        public DataTable HSPR_IncidentType_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_IncidentType_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }



        #endregion

        #region 查询费用

        public DataTable HSPR_Fees_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_SearchFilter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        public DataTable HSPR_Fees_FilterXYe(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_SearchFilterXYe";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页查询

        public DataTable HSPR_Fees_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_Fees_SearchFilter";
            string fldName = "*";
            string fldsort = "FeesID";//
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "FeesID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }


        #endregion
        #endregion

        #region 查询费用(按月合计)

        public DataTable HSPR_Fees_SumMonthFilter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_SumMonthFilter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        public DataTable HSPR_Fees_SumMonthFilterXYe(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_SumMonthFilterXYe";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页查询

        public DataTable HSPR_Fees_SumMonth_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_Fees_SumMonthFilter";
            string fldName = "*";
            string fldsort = "CommID,CustID,RoomID,FeesDueYearMonth desc";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "OrderID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }

        public DataTable HSPR_Fees_SumMonthXYe_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize, int iSort)
        {
            string fldsort = "";
            if (iSort == 0)
            {
                fldsort = "CommID,CustID,RoomID,FeesDueYearMonth asc";
            }
            if (iSort == 1)
            {
                fldsort = "CommID,CustID,RoomID,FeesDueYearMonth desc";
            }
            else
            {
                fldsort = "CommID,CustID,RoomID,FeesDueYearMonth desc";
            }
            DataTable dTable = null;

            string tbName = "view_HSPR_Fees_SumMonthFilter";
            string fldName = "*";
            int Sort = iSort;//1为降序 0为升序
            string strFilter = "";
            string ID = "OrderID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }

        #endregion
        #endregion

        #region 查询欠费合计

        public DataTable HSPR_Fees_FilterDebts(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_FilterDebts";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }


        #endregion

        #region 查询收费

        public DataTable HSPR_FeesDetail_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_FeesDetail_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        public DataTable HSPR_FeesDetail_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_FeesDetail_Filter";
            string fldName = "*";
            string fldsort = "RecdID";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "RecdID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }


        #endregion

        #endregion

        #region 查询预交

        public DataTable HSPR_PreCostsDetail_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_PreCostsDetail_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        public DataTable HSPR_PreCostsDetail_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_PreCostsDetail_Filter";
            string fldName = "*";
            string fldsort = "RecdID";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "RecdID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }


        #endregion

        #endregion

        #region 查询费用收取情况（实收预收）

        public DataTable HSPR_Fees_ChargeFilterTop(string SQLEx, int Counts)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_ChargeFilterTop";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx,@Counts";

            dbParams.Add("SQLEx", SQLEx, 1000);
            dbParams.Add("Counts", Counts, SqlDbType.Int);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页查询

        public DataTable HSPR_Fees_Charge_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_Fees_ChargeFilter";
            string fldName = "*";
            string fldsort = "FeesID";//
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "FeesID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }


        #endregion
        #endregion

        #region 查询费用收取情况（实收预收冲抵）

        public void HSPR_FeesReceDetail_InsUpdate(int CommID, long CustID, long RoomID, string BeginDate, string EndDate)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_FeesReceDetail_InsUpdate";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@RoomID,@BeginDate,@EndDate";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("BeginDate", BeginDate, SqlDbType.DateTime);
            dbParams.Add("EndDate", EndDate, SqlDbType.DateTime);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        public DataTable HSPR_FeesReceDetail_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_FeesReceDetail_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        public DataTable HSPR_FeesReceDetail_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_FeesReceDetail_Filter";
            string fldName = "*";
            string fldsort = "CustID,FeesDueDate desc,FeesStartDate desc";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "DetailID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }


        #endregion

        #endregion

        #region 查询预交余额

        public DataTable HSPR_PreCosts_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_PreCosts_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        public DataTable HSPR_PreCosts_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_PreCosts_Filter";
            string fldName = "*";
            string fldsort = "PrecID";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "PrecID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }


        #endregion

        #endregion

        #region 查询预交冲抵

        public DataTable HSPR_OffsetPreDetail_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_OffsetPreDetail_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页
        public DataTable HSPR_OffsetPreDetail_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_OffsetPreDetail_CommissFilter";
            string fldName = "*";
            string fldsort = "AuditDate";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "IID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }
        #endregion

        #endregion

        #region 费用冻结

        public void HSPR_FeesFreeze_InFreeze(int CommID, long FeesID)
        {
            long FreezeID = 0;
            string FrzUserCode = "";
            string FrzReason = "";

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_FeesFreeze_InFreeze";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@FreezeID,@FeesID,@FrzUserCode,@FrzReason";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("FreezeID", FreezeID, SqlDbType.BigInt);
            dbParams.Add("FeesID", FeesID, SqlDbType.BigInt);
            dbParams.Add("FrzUserCode", FrzUserCode, 20);
            dbParams.Add("FrzReason", FrzReason, 100);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 费用代收锁定

        public void HSPR_Fees_UpdateIsBank(long FeesID, int IsBank)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_UpdateIsBank";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@FeesID,@IsBank";

            dbParams.Add("FeesID", FeesID, SqlDbType.BigInt);
            dbParams.Add("IsBank", IsBank, SqlDbType.SmallInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 费用过期解除代收锁定

        public void HSPR_Fees_UpdateIsBankUnchain(long CustID, long RoomID, int LimitTime)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_UpdateIsBankUnchain";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CustID,@RoomID,@LimitTime";

            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("LimitTime", LimitTime, SqlDbType.Int);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 查询费用项目

        public DataTable HSPR_CostItem_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CostItem_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页
        public DataTable HSPR_CostItem_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_CostItem_Filter";
            string fldName = "*";
            string fldsort = "CostSNum";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "CostID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }
        #endregion

        #endregion        

        #region 查询收费标准

        public DataTable HSPR_CostStanSetting_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CostStanSetting_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #endregion

        #region 费用计算（自然月）

        public DataTable HSPR_Fees_CalcAmount(int CommID, long CustID, long RoomID, long HandID, long CostID, long StanID, string FeesStateDate, string FeesEndDate, decimal Amount, decimal Amount2)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_CalcAmount";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@RoomID,@HandID,@CostID,@StanID,@FeesStateDate,@FeesEndDate,@Amount,@Amount2";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("HandID", HandID, SqlDbType.BigInt);
            dbParams.Add("CostID", CostID, SqlDbType.BigInt);
            dbParams.Add("StanID", StanID, SqlDbType.BigInt);
            dbParams.Add("FeesStateDate", FeesStateDate, SqlDbType.DateTime);
            dbParams.Add("FeesEndDate", FeesEndDate, SqlDbType.DateTime);
            dbParams.Add("Amount", Amount, SqlDbType.Decimal);
            dbParams.Add("Amount2", Amount2, SqlDbType.Decimal);

            //**执行SQL存储过程
            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;

        }

        #endregion

        #region 东原报事

        #region 报事登记
        public string Incident_Reception_CustFastInsert(int CommID, long CustID
            , string IncidentDate, string IncidentMan, string IncidentContent
            , string Phone, string IncidentImgs)
        {
            string ReceCode = "";

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Incident_Reception_CustFastInsert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@IncidentDate,@IncidentMan,@IncidentContent,@Phone,@IncidentImgs";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);

            dbParams.Add("IncidentDate", IncidentDate, SqlDbType.DateTime);
            dbParams.Add("IncidentMan", IncidentMan, 20);
            dbParams.Add("IncidentContent", IncidentContent, 1000);

            dbParams.Add("Phone", Phone, 50);
            dbParams.Add("IncidentImgs", IncidentImgs, 4000);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@ReceCode", SqlDbType.VarChar, 36);
            DataTable dTable = DAccess.DataTable();

            ReceCode = DAccess.GetString("ReceCode");

            //*****关闭链接
            Dispose();

            return ReceCode;


        }

        #endregion

        #region 查询报事

        public DataTable Incident_Reception_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Incident_Reception_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        public DataTable Incident_Reception_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_Incident_Reception_Filter";
            string fldName = "*";
            string fldsort = "ReceDate";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "ReceCode";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }


        #endregion

        #endregion

        #region 查询任务

        public DataTable Incident_Task_SecFilter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Incident_Task_SecFilter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页

        public DataTable Incident_Task_Sec_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_Incident_Task_SecFilter";
            string fldName = "*";
            string fldsort = "TaskNum";
            int Sort = 1;//1为降序 0为升序
            string strFilter = "";
            string ID = "TaskCode";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }


        #endregion

        #endregion

        #region 查询进程
        public DataTable Incident_TaskProgress_SecFilter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Incident_TaskProgress_SecFilter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }
        #endregion

        #region 报事评价
        public void Incident_Task_UpdateServiceEvaluation(string TaskCode, string ServiceEvaluation, string VisiteSuggest, string VisiteIsTimely, string VisiteIsSolve, string VisiteIsCharge)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Incident_Task_UpdateServiceEvaluation";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@TaskCode,@ServiceEvaluation,@VisiteSuggest,@VisiteIsTimely,@VisiteIsSolve,@VisiteIsCharge";

            dbParams.Add("TaskCode", TaskCode, 36);
            dbParams.Add("ServiceEvaluation", ServiceEvaluation, 36);
            dbParams.Add("VisiteSuggest", VisiteSuggest, 500);
            dbParams.Add("VisiteIsTimely", VisiteIsTimely, 36);
            dbParams.Add("VisiteIsSolve", VisiteIsSolve, 36);
            dbParams.Add("VisiteIsCharge", VisiteIsCharge, 36);
            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #endregion

        #region 公共接口

        #region 释放数据库链接资源
        //***释放数据库链接资源
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true); // as a service to those who might inherit from us
        }

        protected virtual void Dispose(bool disposing)
        {
            DAccess.Dispose();
        }
        #endregion

        #endregion

        #region 口头派事
        public string Incident_Reception_CustFastInsertOral(int CommID, long CustID
     , string IncidentDate, string IncidentMan, string IncidentContent
     , string Phone, string IncidentImgs)
        {
            string ReceCode = "";

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Incident_Reception_CustFastInsertOral";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@IncidentDate,@IncidentMan,@IncidentContent,@Phone,@IncidentImgs";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);

            dbParams.Add("IncidentDate", IncidentDate, SqlDbType.DateTime);
            dbParams.Add("IncidentMan", IncidentMan, 20);
            dbParams.Add("IncidentContent", IncidentContent, 1000);

            dbParams.Add("Phone", Phone, 50);
            dbParams.Add("IncidentImgs", IncidentImgs, 4000);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@ReceCode", SqlDbType.VarChar, 36);
            DataTable dTable = DAccess.DataTable();

            ReceCode = DAccess.GetString("ReceCode");

            //*****关闭链接
            Dispose();

            return ReceCode;
        }

        #endregion


        #region 支付费用

        #region 支付订单

        public long HSPR_PayCustSurr_InsUpdate(int CommID, long CustID, decimal TotalAmount
            , string FeesIDs0, string FeesIDs1, string ChargeMode)
        {
            long iSurrID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_PayCustSurr_InsUpdate";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@CustID,@TotalAmount,@FeesIDs0,@FeesIDs1,@ChargeMode";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("TotalAmount", TotalAmount, SqlDbType.Decimal);
            dbParams.Add("FeesIDs0", FeesIDs0, 4000);
            dbParams.Add("FeesIDs1", FeesIDs1, 4000);
            dbParams.Add("ChargeMode", ChargeMode, 20);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@SurrID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            iSurrID = DAccess.GetInt64("SurrID");

            //*****关闭链接
            Dispose();

            return iSurrID;

        }
        #endregion

        #region 查询

        public DataTable HSPR_PayCustSurr_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_PayCustSurr_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页
        public DataTable HSPR_PayCustSurr_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "View_HSPR_PayCustSurr_Filter";
            string fldName = "*";
            string fldsort = "SurrID";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "SurrID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }
        #endregion

        #endregion        

        #region 取消订单

        public void HSPR_PayCustSurr_Delete(long SurrID)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_PayCustSurr_Delete";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SurrID";

            dbParams.Add("SurrID", SurrID, SqlDbType.BigInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 订单下账

        public long HSPR_PayCustSurr_CustDeal(long SurrID, int CommID, long CustID, decimal ActPaidAmount
            , string ReturnCode, string ErrMsg, string BankName)
        {
            long iReceID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_PayCustSurr_CustDeal";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SurrID,@CommID,@CustID,@ActPaidAmount,@ReturnCode,@ErrMsg,@BankName";

            dbParams.Add("SurrID", SurrID, SqlDbType.BigInt);
            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("ActPaidAmount", ActPaidAmount, SqlDbType.Decimal);

            dbParams.Add("ReturnCode", ReturnCode, 10);
            dbParams.Add("ErrMsg", ErrMsg, 50);
            dbParams.Add("BankName", BankName, 50);

            DAccess.PrepareCall(dbParams);

            DAccess.RegisterOutParameter("@ReceID", SqlDbType.BigInt, 0);
            DataTable dTable = DAccess.DataTable();

            iReceID = DAccess.GetInt64("ReceID");

            //*****关闭链接
            Dispose();

            return iReceID;

        }


        #endregion

        #region 查询明细

        public DataTable HSPR_PayCustDetail_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_PayCustDetail_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #region 分页
        public DataTable HSPR_PayCustDetail_CutPage(out int pageCount, out int Counts, string SQLEx, int PageNo, int PageSize)
        {
            DataTable dTable = null;

            string tbName = "view_HSPR_PayCustDetail_Filter";
            string fldName = "*";
            string fldsort = "IID";
            int Sort = 0;//1为降序 0为升序
            string strFilter = "";
            string ID = "IID";
            int Dist = 0;

            //***
            strFilter = " ";
            //***
            strFilter = strFilter + SQLEx;

            dTable = GetProcCutPages(out Counts, out pageCount, tbName, fldName, PageSize, PageNo, fldsort, Sort, strFilter, ID, Dist);


            return dTable;
        }
        #endregion



        #endregion


        #endregion

        #region 数据同步时间表

        #region 查询
        public DataTable DataSynch_LastTime_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_DataSynch_LastTime_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, SqlDbType.NVarChar);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;

        }
        #endregion 

        #region 更新
        public void DataSynch_LastTime_UpdateSynch(string Indentify, string LastTime, int IsSynch)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_DataSynch_LastTime_UpdateSynch";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@Indentify,@LastTime,@IsSynch";

            dbParams.Add("Indentify", Indentify, 20);
            dbParams.Add("LastTime", LastTime, SqlDbType.DateTime);
            dbParams.Add("IsSynch", IsSynch, SqlDbType.SmallInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #endregion       

        #region 流程审核表

        #region 查询
        public DataTable DataSynch_FlowAudit_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_DataSynch_FlowAudit_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, SqlDbType.NVarChar);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;

        }
        #endregion 

        #region 更新
        public void DataSynch_FlowAudit_UpdateAudit(string DocID, string DocAuditDate, int IsAudit)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_DataSynch_FlowAudit_UpdateAudit";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@DocID,@DocAuditDate,@IsAudit";

            dbParams.Add("DocID", DocID, 50);
            dbParams.Add("DocAuditDate", DocAuditDate, SqlDbType.DateTime);
            dbParams.Add("IsAudit", IsAudit, SqlDbType.SmallInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #endregion

        #region 更新凭证号
        public void JDEVoucher_Head_UpdateVoucherID(long BatchNumber, long Voucher_ID)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_JDEVoucher_Head_UpdateVoucherID";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@BatchNumber,@Voucher_ID";

            dbParams.Add("BatchNumber", BatchNumber, SqlDbType.BigInt);
            dbParams.Add("Voucher_ID", Voucher_ID, SqlDbType.BigInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion     

        #region 合同费用审核
        public void HSPR_Contract_Base_Audit(long ContID, int IsAuditing, string AuditUserCode, string AuditUserRoles)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Contract_Audit";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@ContID,@IsAuditing,@AuditUserCode,@AuditUserRoles";

            dbParams.Add("ContID", ContID, SqlDbType.BigInt);
            dbParams.Add("IsAuditing", IsAuditing, SqlDbType.SmallInt);
            dbParams.Add("AuditUserCode", AuditUserCode, 20);
            dbParams.Add("AuditUserRoles", AuditUserRoles, 1000);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();
        }

        public DataTable HSPR_Contract_Base_Filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Contract_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, SqlDbType.NVarChar);

            //**执行SQL存储过程
            DataTable dt = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();
            return dt;
        }

        //合同费用明细（需审核）
        public DataTable HSPR_AuditingConFeesDetail_filter(string SQLEx)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_AuditingConFeesDetail_filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";

            dbParams.Add("SQLEx", SQLEx, 1000);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        public long HSPR_Fees_Base_Insert(ref TbHSPRFees Item)
        {
            //**得到下一编号
            long iFeesMaxNum = HSPR_Fees_GetMaxNum(Item.CommID, "");
            Item.FeesID = iFeesMaxNum;
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_Insert";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@FeesID,@CommID,@CostID,@CustID,@RoomID,@FeesDueDate,@FeesStateDate,@FeesEndDate,@DueAmount,@DebtsAmount,@WaivAmount,@PrecAmount,@PaidAmount,@RefundAmount,@IsAudit,@FeesMemo,@AccountFlag,@IsBank,@IsCharge,@CorpStanID,@StanID,@AccountsDueDate,@HandID,@MeterSign,@CalcAmount,@CalcAmount2,@IncidentID,@LeaseContID,@ContID,@PMeterID,@StanMemo,@UserCode,@OrderCode,@IsPast,@AssumeCustID,@ContSetID";
            dbParams.Add("FeesID", Item.FeesID, SqlDbType.BigInt);
            dbParams.Add("CommID", Item.CommID, SqlDbType.Int);
            dbParams.Add("CostID", Item.CostID, SqlDbType.BigInt);
            dbParams.Add("CustID", Item.CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", Item.RoomID, SqlDbType.BigInt);
            dbParams.Add("FeesDueDate", Item.FeesDueDate, SqlDbType.DateTime);
            dbParams.Add("FeesStateDate", Item.FeesStateDate, SqlDbType.DateTime);
            dbParams.Add("FeesEndDate", Item.FeesEndDate, SqlDbType.DateTime);
            dbParams.Add("DueAmount", Item.DueAmount, SqlDbType.Decimal);
            dbParams.Add("DebtsAmount", Item.DebtsAmount, SqlDbType.Decimal);
            dbParams.Add("WaivAmount", Item.WaivAmount, SqlDbType.Decimal);
            dbParams.Add("PrecAmount", Item.PrecAmount, SqlDbType.Decimal);
            dbParams.Add("PaidAmount", Item.PaidAmount, SqlDbType.Decimal);
            dbParams.Add("RefundAmount", Item.RefundAmount, SqlDbType.Decimal);
            dbParams.Add("IsAudit", Item.IsAudit, SqlDbType.SmallInt);
            dbParams.Add("FeesMemo", Item.FeesMemo, 30);
            dbParams.Add("AccountFlag", Item.AccountFlag, SqlDbType.Int);
            dbParams.Add("IsBank", Item.IsBank, SqlDbType.SmallInt);
            dbParams.Add("IsCharge", Item.IsCharge, SqlDbType.SmallInt);
            dbParams.Add("CorpStanID", Item.CorpStanID, SqlDbType.BigInt);
            dbParams.Add("StanID", Item.StanID, SqlDbType.BigInt);
            dbParams.Add("OwnerFeesID", Item.OwnerFeesID, SqlDbType.BigInt);
            dbParams.Add("AccountsDueDate", Item.AccountsDueDate, SqlDbType.DateTime);
            dbParams.Add("HandID", Item.HandID, SqlDbType.BigInt);
            dbParams.Add("MeterSign", Item.MeterSign, 20);
            dbParams.Add("CalcAmount", Item.CalcAmount, SqlDbType.Decimal);
            dbParams.Add("CalcAmount2", Item.CalcAmount2, SqlDbType.Decimal);
            dbParams.Add("IncidentID", Item.IncidentID, SqlDbType.BigInt);
            dbParams.Add("LeaseContID", Item.LeaseContID, SqlDbType.BigInt);
            dbParams.Add("ContID", Item.ContID, SqlDbType.BigInt);
            dbParams.Add("PMeterID", Item.PMeterID, SqlDbType.BigInt);
            dbParams.Add("StanMemo", Item.StanMemo, 200);
            dbParams.Add("UserCode", Item.UserCode, 20);
            dbParams.Add("OrderCode", Item.OrderCode, 50);
            dbParams.Add("IsPast", Item.IsPast, SqlDbType.SmallInt);
            dbParams.Add("AssumeCustID", Item.AssumeCustID, SqlDbType.BigInt);
            dbParams.Add("ContSetID", Item.ContSetID, SqlDbType.BigInt);
            //**执行SQL存储过程
            DAccess.Excute(dbParams);
            //*****关闭链接
            Dispose();
            return iFeesMaxNum;
        }

        private long HSPR_Fees_GetMaxNum(int CommID, string strSQL)
        {
            long MaxID = 0;

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_Fees_GetMaxNum";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@SQLEx";
            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("SQLEx", strSQL, 1000);
            //**执行SQL存储过程
            DataTable dTable = DAccess.DataTable(dbParams);

            if (dTable.Rows.Count > 0)
            {
                try
                {
                    MaxID = Convert.ToInt64(dTable.Rows[0][0].ToString());
                }
                catch
                {

                }
            }

            dTable.Dispose();
            //*****关闭链接
            //Dispose();

            return MaxID;
        }

        public void HSPR_HSPR_AuditingConFeesDetail_Audit(long IID, int IsAuditing, string AuditUserCode, DateTime AuditData)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_AuditingConFeesDetail_Audit";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@IID,@IsAuditing,@AuditUserCode,@AuditData";

            dbParams.Add("IID", IID, SqlDbType.BigInt);
            dbParams.Add("IsAuditing", IsAuditing, SqlDbType.SmallInt);
            dbParams.Add("AuditUserCode", AuditUserCode, SqlDbType.NVarChar);
            dbParams.Add("AuditData", AuditData, SqlDbType.DateTime);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();
        }

        public DataTable HSRP_LeaseContractRenewal_Filter(ref TbHSPRLeaseContractRenewal Item)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_LeaseContractRenewal_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";
            dbParams.Add("SQLEx", Item.SQLEx, 1000);
            //**执行SQL存储过程
            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        public void HSPR_LeaseContract_UpdateReletDate(long ContID, string ReletDate)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_LeaseContract_UpdateReletDate";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@ContID,@ReletDate";

            dbParams.Add("ContID", ContID, SqlDbType.BigInt);
            dbParams.Add("ReletDate", ReletDate, SqlDbType.DateTime);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();
        }

        public DataTable HSPR_LeaseContractEnd_Filter(string SQLEx)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_LeaseContractEnd_Filter";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@SQLEx";
            dbParams.Add("SQLEx", SQLEx, 1000);
            //**执行SQL存储过程
            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        public void HSPR_LeaseContract_UpdateStopDate(long ContID, string StopDate)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_LeaseContract_UpdateStopDate";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@ContID,@StopDate";

            dbParams.Add("ContID", ContID, SqlDbType.BigInt);
            dbParams.Add("StopDate", StopDate, SqlDbType.DateTime);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();
        }

        #endregion

        #region 退款审核
        public void HSPR_RefundSecFees_UpdateAudit(int CommID, long IID, string AuditUsercode, string UserRoles, int IsAudit)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_RefundFees_SecUpdateAudit";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@IID,@AuditUsercode,@UserRoles,@IsAudit";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("IID", IID, SqlDbType.BigInt);
            dbParams.Add("AuditUsercode", AuditUsercode, 20);
            dbParams.Add("UserRoles", UserRoles, 1000);
            dbParams.Add("IsAudit", IsAudit, SqlDbType.SmallInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();
        }
        #endregion

        #region 是否可以减免
        public bool HSPR_WaiversFee_IsCheck(long WaivID, int CommID, long CustID, long RoomID, long CostID, string WaivStateDuring, string WaivEndDuring)
        {
            bool IsOK = true;
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_WaiversFee_IsCheck";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@WaivID,@CommID,@CustID,@RoomID,@CostID,@WaivStateDuring,@WaivEndDuring";

            dbParams.Add("WaivID", WaivID, SqlDbType.BigInt);
            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("CustID", CustID, SqlDbType.BigInt);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("CostID", CostID, SqlDbType.BigInt);
            dbParams.Add("WaivStateDuring", WaivStateDuring, SqlDbType.DateTime);
            dbParams.Add("WaivEndDuring", WaivEndDuring, SqlDbType.DateTime);

            //**执行SQL存储过程
            DataTable dTable = DAccess.DataTable(dbParams);

            if (dTable.Rows.Count > 0)
            {
                IsOK = false;
            }
            dTable.Dispose();
            //*****关闭链接
            Dispose();
            return IsOK;
        }
        #endregion

        #region 更新减免审核
        public void HSPR_WaiversFee_UpdateAudit(int CommID, long WaivID, string AuditUsercode, string UserRoles, string AuditReason, int IsAudit)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_WaiversFee_UpdateAudit";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@WaivID,@AuditUsercode,@UserRoles,@AuditReason,@IsAudit";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("WaivID", WaivID, SqlDbType.BigInt);
            dbParams.Add("AuditUsercode", AuditUsercode, 20);
            dbParams.Add("UserRoles", UserRoles, 1000);
            dbParams.Add("AuditReason", AuditReason, 200);
            dbParams.Add("IsAudit", IsAudit, SqlDbType.SmallInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();
        }
        #endregion

        #region 物资计划审核

        public Tb_Mt_PurchasePlan GetModel(string Id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,50)};
            parameters[0].Value = Id;

            Tb_Mt_PurchasePlan model = new Tb_Mt_PurchasePlan();

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Tb_Mt_PurchasePlan_GetModel";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@Id";
            dbParams.Add("Id", Id, SqlDbType.Int);
            DataSet ds = DAccess.DataSet(dbParams);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Id = ds.Tables[0].Rows[0]["Id"].ToString();
                model.PlanOrganCode = ds.Tables[0].Rows[0]["PlanOrganCode"].ToString();
                model.PlanNum = ds.Tables[0].Rows[0]["PlanNum"].ToString();
                if (ds.Tables[0].Rows[0]["PlanDate"].ToString() != "")
                {
                    model.PlanDate = DateTime.Parse(ds.Tables[0].Rows[0]["PlanDate"].ToString());
                }
                model.PlanYearMonth = ds.Tables[0].Rows[0]["PlanYearMonth"].ToString();
                model.DepCode = ds.Tables[0].Rows[0]["DepCode"].ToString();
                model.UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString();
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                model.State = ds.Tables[0].Rows[0]["State"].ToString();
                model.AttachFile = ds.Tables[0].Rows[0]["AttachFile"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        public void Update(Tb_Mt_PurchasePlan model)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_Tb_Mt_PurchasePlan_Update";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@Id,@PlanOrganCode,@PlanNum,@PlanDate,@PlanYearMonth,@DepCode,@UserCode,@Memo,@State,@AttachFile";

            dbParams.Add("Id", model.Id, SqlDbType.Int);
            dbParams.Add("PlanOrganCode", model.PlanOrganCode, SqlDbType.BigInt);
            dbParams.Add("PlanNum", model.PlanNum, 20);
            dbParams.Add("PlanDate", model.PlanDate, 1000);
            dbParams.Add("PlanYearMonth", model.PlanYearMonth, 200);
            dbParams.Add("DepCode", model.DepCode, SqlDbType.SmallInt);
            dbParams.Add("UserCode", model.UserCode, SqlDbType.SmallInt);
            dbParams.Add("Memo", model.Memo, SqlDbType.SmallInt);
            dbParams.Add("State", model.State, SqlDbType.SmallInt);
            dbParams.Add("AttachFile", model.AttachFile, SqlDbType.SmallInt);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();
        }

        #endregion

        #region 违约金减免审核
        public void HSPR_WaiversLateFee_InWaiv(int CommID, long WaivID, long FeesID, int WaivType, decimal WaivFeesAmount, string LateFeeEndDate, decimal WaivRates, string UserCode, string WaivMemo)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_WaiversLateFee_InWaiv";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@WaivID,@FeesID,@WaivType,@WaivFeesAmount,@LateFeeEndDate,@WaivRates,@UserCode,@WaivMemo";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("WaivID", WaivID, SqlDbType.BigInt);
            dbParams.Add("FeesID", FeesID, SqlDbType.BigInt);

            dbParams.Add("WaivType", WaivType, SqlDbType.SmallInt);
            dbParams.Add("WaivFeesAmount", WaivFeesAmount, SqlDbType.Decimal);
            dbParams.Add("LateFeeEndDate", LateFeeEndDate, SqlDbType.DateTime);
            dbParams.Add("WaivRates", WaivRates, SqlDbType.Decimal);

            dbParams.Add("UserCode", UserCode, 20);
            dbParams.Add("WaivMemo", WaivMemo, 50);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }
        #endregion

        #region 撤销减免
        public void HSPR_WaiversLateFee_UnWaiv(int CommID, long WaivID, long FeesID, string UserCode)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_WaiversLateFee_UnWaiv";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@WaivID,@FeesID,@UserCode";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("WaivID", WaivID, SqlDbType.BigInt);
            dbParams.Add("FeesID", FeesID, SqlDbType.BigInt);
            dbParams.Add("UserCode", UserCode, 20);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }

        #endregion

        #region 取消减免审核
        public void HSPR_WaiversFee_UpdateModifyAudit(long WaivID, int IsModifyAudit, string ModifyAuditUserCode, string ModifyAuditMemo)
        {

            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_WaiversFee_UpdateModifyAudit";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@WaivID,@IsModifyAudit,@ModifyAuditUserCode,@ModifyAuditMemo";

            dbParams.Add("WaivID", WaivID, SqlDbType.BigInt);
            dbParams.Add("IsModifyAudit", IsModifyAudit, SqlDbType.Int);
            dbParams.Add("ModifyAuditUserCode", ModifyAuditUserCode, 20);
            dbParams.Add("ModifyAuditMemo", ModifyAuditMemo, 200);

            //**执行SQL存储过程
            DAccess.Excute(dbParams);

            //*****关闭链接
            Dispose();

        }
        #endregion

        #region 查询抄表的费用

        public DataTable HSPR_CustomerMeterResult_FilterCalFees(int CommID, long RoomID, long MeterID, string strListDate)
        {
            DbParamters dbParams = new DbParamters();
            dbParams.CommandText = "Proc_HSPR_CustomerMeterResult_FilterCalFees";
            dbParams.CommandType = CommandType.StoredProcedure;
            dbParams.ProcParamters = "@CommID,@RoomID,@MeterID,@ListDate";

            dbParams.Add("CommID", CommID, SqlDbType.Int);
            dbParams.Add("RoomID", RoomID, SqlDbType.BigInt);
            dbParams.Add("MeterID", MeterID, SqlDbType.BigInt);
            dbParams.Add("ListDate", strListDate, SqlDbType.DateTime);

            DataTable dTable = DAccess.DataTable(dbParams);

            //*****关闭链接
            Dispose();

            return dTable;
        }

        #endregion
    }
}
