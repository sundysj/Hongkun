using System;
using System.Text.RegularExpressions;
public static class StringExtensions
{
    /// <summary>
    /// 按单字节字符串向左填充长度
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length"></param>
    /// <param name="paddingChar"></param>
    /// <returns></returns>
    public static string PadLeftWhileDouble(this string input, int length, char paddingChar = '\0')
    {
        var singleLength = GetSingleLength(input);
        return input.PadLeft(length - singleLength + input.Length, paddingChar);
    }
    private static int GetSingleLength(string input)
    {
        return Regex.Replace(input, @"[^\x00-\xff]", "aa").Length;//计算得到该字符串对应单字节字符串的长度
    }
    /// <summary>
    /// 按单字节字符串向右填充长度
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length"></param>
    /// <param name="paddingChar"></param>
    /// <returns></returns>
    public static string PadRightWhileDouble(this string input, int length, char paddingChar = '\0')
    {
        var singleLength = GetSingleLength(input);
        return input.PadRight(length - singleLength + input.Length, paddingChar);
    }

    public static string AsString(this object strValue)
    {
        string str = "";
        if (strValue != null)
        {
            str = strValue.ToString();
        }
        return str;

    }
    /// <summary>
    /// 增删改返回前台JSON
    /// </summary>
    /// <param name="Result">结果 TRUE,FALSE</param>
    /// <param name="Message">消息</param>
    /// <returns></returns>
    public static string ResultOperationJsonString(string Result, string Message)
    {
        string strJsonMessage = "{'Result':'" + Result + "','Message':'" + Message + "'}";
        return strJsonMessage;
    }


    public static DateTime ToDateTime235959(this DateTime datetime)
    {
        DateTime date1 = DateTime.Now;

        if (DateTime.TryParse(datetime.ToShortDateString(), out date1))
        {
            date1 = Convert.ToDateTime(date1.ToShortDateString() + " 23:59:59");
        }
        return date1;

    }
    public static DateTime ToDateTime000000(this DateTime datetime)
    {
        DateTime date1 = DateTime.Now;

        if (DateTime.TryParse(datetime.ToShortDateString(), out date1))
        {
            date1 = Convert.ToDateTime(date1.ToShortDateString() + " 00:00:00");
        }
        return date1;

    }
}