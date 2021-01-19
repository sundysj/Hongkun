using System;
using System.Collections.Generic;
using System.Text;

namespace MobileSoft.Common
{
    public class DataSecurity
    {
        #region Sql注入字符串过滤
        public static string FilteSQLStr(string Str)
        {
            Str = Str.ToLower();

            string[] Pattern = {"'"," ",";"};
            for (int i = 0; i < Pattern.Length; i++)
            {
                Str = Str.Replace(Pattern[i].ToLower(), "");
            }
            return Str;
        }

        #endregion

        #region 类型转换

        public static DateTime StrToDateTime(string Str)
        {
            DateTime Ret;

            try
            {
                Ret = Convert.ToDateTime(Str);
            }
            catch (Exception)
            {
                Ret = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            }
            return Ret;
        }


        public static string DateToShortDate(string Str)
        {
            string Ret;

            try
            {
                Ret = Convert.ToDateTime(Str).ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {
                Ret = "";
            }
            return Ret;
        }

        public static int StrToInt(string Str)
        {
            int Ret = 0;

            try
            {
                Ret = Convert.ToInt32(Str);
            }
            catch (Exception)
            {
                Ret = 0;
            }
            return Ret;
        }

        public static decimal StrToDecimal(string Str)
        {
            decimal Ret = 0;

            try
            {
                Ret = Convert.ToDecimal(Str);
            }
            catch (Exception)
            {
                Ret = 0;
            }
            return Ret;
        }

        public static Int64 StrToLong(string Str)
        {
            Int64 Ret = 0;

            try
            {
                Ret = Convert.ToInt64(Str);
            }
            catch (Exception)
            {
                Ret = 0;
            }
            return Ret;
        }

        #endregion

        #region 转换并检查日期
        /// <summary>
        /// 转换并检查日期
        /// </summary>
        /// <param name="strYear">年</param>
        /// <param name="strMonth">月</param>
        /// <param name="strDay">日</param>
        /// <returns>yyyy-MM-dd型日期</returns>
        public static string CheckDate(string strYear, string strMonth, string strDay)
        {
            string NDate = "";
            if ((strYear != "") && (strMonth != "") && (strDay != ""))
            {
                int MaxDay = 31;
                int iYear = 1900;
                int iMonth = 1;
                int iDay = 1;

                try
                {
                    iYear = Convert.ToInt32(strYear);
                }
                catch
                {
                }
                try
                {
                    iMonth = Convert.ToInt32(strMonth);
                }
                catch
                {
                }
                try
                {
                    iDay = Convert.ToInt32(strDay);
                }
                catch
                {
                }


                if (iMonth == 2)
                {
                    if (DateTime.IsLeapYear(iYear))
                    {
                        MaxDay = 29;
                    }
                    else
                    {
                        MaxDay = 28;
                    }
                }
                if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
                {
                    MaxDay = 31;
                }
                if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
                {
                    MaxDay = 30;
                }

                if (iDay > MaxDay)
                {
                    iDay = MaxDay;
                }

                DateTime CkDate = new DateTime(iYear, iMonth, iDay);
                NDate = CkDate.ToString("yyyy-MM-dd");
            }

            return NDate;
        }

        /// <summary>
        /// 转换并检查日期
        /// </summary>
        /// <param name="strYear">年</param>
        /// <param name="strMonth">月</param>
        /// <param name="strDay">日</param>
        /// <returns>日期DateTime</returns>
        public static DateTime CheckDate(int iYear, int iMonth, int iDay)
        {
            DateTime CkDate = new DateTime(1900, 1, 1);
            if ((iYear != 0) && (iMonth != 0) && (iDay != 0))
            {
                int MaxDay = 31;

                if (iMonth == 2)
                {
                    if (DateTime.IsLeapYear(iYear))
                    {
                        MaxDay = 29;
                    }
                    else
                    {
                        MaxDay = 28;
                    }
                }
                if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
                {
                    MaxDay = 31;
                }
                if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
                {
                    MaxDay = 30;
                }

                if (iDay > MaxDay)
                {
                    iDay = MaxDay;
                }

                CkDate = new DateTime(iYear, iMonth, iDay);
            }

            return CkDate;
        }
        #endregion

        #region 转换并检查日期时间
        /// <summary>
        /// 转换并检查日期时间
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="strDay"></param>
        /// <param name="strHour"></param>
        /// <param name="strMinute"></param>
        /// <param name="strSecond"></param>
        /// <returns></returns>
        public static string CheckDateTime(string strYear, string strMonth, string strDay, string strHour, string strMinute, string strSecond)
        {
            string NDate = "";
            if ((strYear != "") && (strMonth != "") && (strDay != ""))
            {
                int MaxDay = 31;
                int iYear = 1900;
                int iMonth = 1;
                int iDay = 1;
                int iHour = 0;
                int iMinute = 0;
                int iSecond = 0;

                try
                {
                    iYear = Convert.ToInt32(strYear);
                }
                catch
                {
                }
                try
                {
                    iMonth = Convert.ToInt32(strMonth);
                }
                catch
                {
                }
                try
                {
                    iDay = Convert.ToInt32(strDay);
                }
                catch
                {
                }
                try
                {
                    iHour = Convert.ToInt32(strHour);
                }
                catch
                {
                }
                try
                {
                    iMinute = Convert.ToInt32(strMinute);
                }
                catch
                {
                }
                try
                {
                    iSecond = Convert.ToInt32(strSecond);
                }
                catch
                {
                }


                if (iMonth == 2)
                {
                    if (DateTime.IsLeapYear(iYear))
                    {
                        MaxDay = 29;
                    }
                    else
                    {
                        MaxDay = 28;
                    }
                }
                if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
                {
                    MaxDay = 31;
                }
                if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
                {
                    MaxDay = 30;
                }

                if (iDay > MaxDay)
                {
                    iDay = MaxDay;
                }

                try
                {
                    DateTime CkDate = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
                    NDate = CkDate.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                }
            }

            return NDate;
        }

        /// <summary>
        /// 转换并检查日期时间
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="strDay"></param>
        /// <param name="strHour"></param>
        /// <param name="strMinute"></param>
        /// <param name="strSecond"></param>
        /// <returns></returns>
        public static DateTime CheckDateTime(int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond)
        {


            int MaxDay = 31;

            if (iYear < 0)
            {
                iYear = 1900;
            }
            if (iMonth <= 0)
            {
                iMonth = 1;
            }
            if (iDay <= 0)
            {
                iDay = 1;
            }
            if (iHour < 0)
            {
                iHour = 0;
            }
            if (iMinute < 0)
            {
                iMinute = 0;
            }
            if (iSecond < 0)
            {
                iSecond = 0;
            }

            if (iMonth == 2)
            {
                if (DateTime.IsLeapYear(iYear))
                {
                    MaxDay = 29;
                }
                else
                {
                    MaxDay = 28;
                }
            }
            if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
            {
                MaxDay = 31;
            }
            if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
            {
                MaxDay = 30;
            }

            if (iDay > MaxDay)
            {
                iDay = MaxDay;
            }

            DateTime CkDate = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);

            return CkDate;
        }

        /// <summary>
        /// 转换并检查日期时间
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="strDay"></param>
        /// <param name="strHour"></param>
        /// <param name="strMinute"></param>
        /// <param name="strSecond"></param>
        /// <returns></returns>
        public static DateTime CheckDateTime(int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iMillisecond)
        {


            int MaxDay = 31;

            if (iYear < 0)
            {
                iYear = 1900;
            }
            if (iMonth <= 0)
            {
                iMonth = 1;
            }
            if (iDay <= 0)
            {
                iDay = 1;
            }
            if (iHour < 0)
            {
                iHour = 0;
            }
            if (iMinute < 0)
            {
                iMinute = 0;
            }
            if (iSecond < 0)
            {
                iSecond = 0;
            }

            if (iMonth == 2)
            {
                if (DateTime.IsLeapYear(iYear))
                {
                    MaxDay = 29;
                }
                else
                {
                    MaxDay = 28;
                }
            }
            if ((iMonth == 1) || (iMonth == 3) || (iMonth == 5) || (iMonth == 7) || (iMonth == 8) || (iMonth == 10) || (iMonth == 12))
            {
                MaxDay = 31;
            }
            if ((iMonth == 4) || (iMonth == 6) || (iMonth == 9) || (iMonth == 11))
            {
                MaxDay = 30;
            }

            if (iDay > MaxDay)
            {
                iDay = MaxDay;
            }

            DateTime CkDate = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond, iMillisecond);

            return CkDate;
        }
        #endregion
    }
}
