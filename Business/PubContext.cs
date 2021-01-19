using Common;
using log4net;
using MobileSoft.Common;
using System;
using System.Data;
using System.Reflection;
using System.Web;

namespace Business
{
    public class PubContext
    {
        #region 反射-实例化MD5
        public static void Operate(ref Common.Transfer Trans)
        {
            try
            {
                PubInfo Rp = (PubInfo)Assembly.Load("Business").CreateInstance("Business." + Trans.Class);
                var HashString = Trans.Attribute.ToString() + DateTime.Now.ToString("yyyyMMdd") + Rp.Token;
                var Mac = AppPKI.getMd5Hash(HashString);
                #region 针对参数值带特殊字符的进行修改转义
                if (Trans.Attribute.Contains("&"))
                {
                    Trans.Attribute = Trans.Attribute.Replace("&", "&amp;");
                }
                #endregion
                //鸿坤第三方 不要安全验证 单独去掉
                if (Trans.Class == "HKCallCenter")
                {
                    Rp.Operate(ref Trans);
                }
                else if (Trans.Class == "DataVHJ")
                {
                    //合景datav 大屏 去掉验证
                    Rp.Operate(ref Trans);
                }
                else if (Trans.Class == "ContractAuditInfo_FanWei")
                {
                    //合景经营合同审核 去掉验证
                    Rp.Operate(ref Trans);
                }
                else if (Trans.Class == "PolyDataV")
                {
                    //合景datav 大屏 去掉验证
                    Rp.Operate(ref Trans);
                }
                else if (Trans.Class == "CostInfoSunshineNewest")
                {
                    Rp.Operate(ref Trans);
                }
                else if (Trans.Class == "HKParkCostInfo")
                {
                    Rp.Operate(ref Trans);
                }
                else if (Trans.Class == "CostInfo_RS" || Trans.Class == "Meter_RS" || Trans.Class == "Material_RS")
                {
                    if (Trans.Mac == Mac)
                    {
                        Rp.Operate(ref Trans);
                    }
                    else
                    {
                        Trans.Error = "验证令牌错误";
                    }
                }
                else
                {
                    if (HttpContext.Current.Request.Url.Host.ToLower() == "localhost")
                    {
                        Rp.Operate(ref Trans);
                    }
                    else
                    {
                        if (!OperateKnownClass(ref Trans))
                        {
                            if (Trans.Mac == Mac)
                            {
                                Rp.Operate(ref Trans);
                            }
                            else
                            {
                                Trans.Error = "验证令牌错误";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PubInfo.GetLog().Error(ex);
                Trans.Error = new ApiResult(false, ex.Message + Environment.NewLine + ex.StackTrace).toJson();
            }
        }
        #endregion



        #region 反射-实例化RSA
        public static void OperateRSA(ref Common.Transfer Trans, string MacType)
        {
            try
            {
                PubInfo Rp = (PubInfo)Assembly.Load("Business").CreateInstance("Business." + Trans.Class);
                //验证令牌请求
                //string HashString = Trans.Attribute.ToString() + DateTime.Now.ToString("yyyyMMdd") + Rp.Token.ToString();
                //解密参数
                //string mac = RSAHelperBGY.getInstance(MacType).Encrypt("<attributes><LoginCode>yinlian</LoginCode><Password>yl@123</Password><ComCode>UnionPay</ComCode><Token>20180528IntelligencePOS_Y</Token></attributes>");
                //new Logger().WriteLog("Mac", Trans.Mac);
                if (MacType == "UnionPay")
                {
                    Trans.Attribute = System.Web.HttpUtility.UrlDecode(RSAHelperBGY.getInstance(MacType).Decrypt(Trans.Mac));
                    new Logger().WriteLog("post数据", "http://218.13.58.36:8888/Service/BGYPost.ashx?Class=" + Trans.Class + "&Command=" + Trans.Command + "&ComCode=UnionPay&Mac=" + Trans.Mac + "&Attribute=" + Trans.Attribute);
                }
                else
                {
                    Trans.Attribute = System.Web.HttpUtility.UrlDecode(RSAHelperBGY_ALL.getInstance(MacType).Decrypt(Trans.Mac));
                }


                Rp.Operate(ref Trans);
            }
            catch (Exception ex)
            {
                Trans.Error = JSONHelper.JsonConvertBGY("0001", ex.Message, "");
            }
        }
        #endregion

        public static bool OperateKnownClass(ref Transfer trans)
        {
            var pubInfo = default(PubInfo);

            switch (trans.Class)
            {
                case "QualityManage":
                    pubInfo = new QualityManage();
                    break;
                case "EquipmentManage":
                    pubInfo = new EquipmentManage();
                    break;
                default:
                    return false;
            }

            if (pubInfo == null)
                return false;

            var HashString = trans.Attribute.ToString() + DateTime.Now.ToString("yyyyMMdd") + pubInfo.Token;
            var Mac = AppPKI.getMd5Hash(HashString);

            //if (trans.Mac == Mac)
            {
                pubInfo.Operate(ref trans);

                return true;
            }

            return false;
        }
    }
}