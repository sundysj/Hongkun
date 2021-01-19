using Business.WChat2020;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace KdGoldAPI
{
    /**
     *
     * 快递鸟在途监控：即时查询(增值版)接口
     *
     * @see: http://kdniao.com/api-monitor
     * @copyright: 深圳市快金数据技术服务有限公司
     * 
     * ID和Key请到官网申请：http://kdniao.com/reg
     */
    public class KdApiSearchMonitor
    {
        //请求url
        private static string ReqURL = "http://api.kdniao.com/Ebusiness/EbusinessOrderHandle.aspx";

        public static void test()
        {
            //电商ID
            string EBusinessID = "1682955";
            //电商加密私钥，快递鸟提供，注意保管，不要泄漏
            string AppKey = "1b619510-8803-4192-bc06-63fd2f1f7570";
            // string result = KdGoldAPI.KdApiSearchMonitor.getOrderTracesByJson(AppKey, EBusinessID, KdApiSearchMonitor.ShipperCode.YTO, "809726387339");
        }
        /// <summary>
        /// Json方式 查询订单物流轨迹
        /// </summary>
        /// <returns></returns>
        public static bool getOrderTracesByJson(string appkey, string eBusinessID, ShipperCode shipperCode, string logisticCode, out string result)
        {
            Dictionary<string, string> requestData = new Dictionary<string, string>
            {
                { "OrderCode", "" },
                { "ShipperCode", shipperCode.ToString() },
                { "LogisticCode", logisticCode },
            };
            string param = $"RequestData={HttpUtility.UrlEncode(JsonConvert.SerializeObject(requestData), Encoding.UTF8)}&EBusinessID={eBusinessID}&RequestType={1002}&DataSign={HttpUtility.UrlEncode(encrypt(JsonConvert.SerializeObject(requestData), appkey, "UTF-8"), Encoding.UTF8)}&DataType=2";
            try
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = ReqURL,//URL     必需项  
                    Method = "POST",//URL     可选项 默认为Get  
                    Timeout = 3000,//连接超时时间     可选项默认为100000  
                    ReadWriteTimeout = 3000,//写入Post数据超时时间     可选项默认为30000  
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值  
                    ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值  
                    PostEncoding = Encoding.UTF8,
                    Postdata = param,//Post数据     可选项GET时不需要写  
                    ResultType = ResultType.String,//返回数据类型，是Byte还是String  
                };
                HttpResult httpResult = http.GetHtml(item);
                result = httpResult.Html;
                return true;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
        }
        
        ///<summary>
        ///电商Sign签名
        ///</summary>
        ///<param name="content">内容</param>
        ///<param name="keyValue">Appkey</param>
        ///<param name="charset">URL编码 </param>
        ///<returns>DataSign签名</returns>
        private static string encrypt(String content, String keyValue, String charset)
        {
            if (keyValue != null)
            {
                return base64(MD5(content + keyValue, charset), charset);
            }
            return base64(MD5(content, charset), charset);
        }

        ///<summary>
        /// 字符串MD5加密
        ///</summary>
        ///<param name="str">要加密的字符串</param>
        ///<param name="charset">编码方式</param>
        ///<returns>密文</returns>
        private static string MD5(string str, string charset)
        {
            byte[] buffer = System.Text.Encoding.GetEncoding(charset).GetBytes(str);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider check;
                check = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] somme = check.ComputeHash(buffer);
                string ret = "";
                foreach (byte a in somme)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("X");
                    else
                        ret += a.ToString("X");
                }
                return ret.ToLower();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="str">内容</param>
        /// <param name="charset">编码方式</param>
        /// <returns></returns>
        private static string base64(String str, String charset)
        {
            return Convert.ToBase64String(System.Text.Encoding.GetEncoding(charset).GetBytes(str));
        }

        /// <summary>
        /// 快递编码枚举，具体对应哪个公司查看快递公司编码(http://www.kdniao.com/file/%E5%BF%AB%E9%80%92%E9%B8%9F%E6%8E%A5%E5%8F%A3%E6%94%AF%E6%8C%81%E5%BF%AB%E9%80%92%E5%85%AC%E5%8F%B8%E7%BC%96%E7%A0%81.xlsx)
        /// </summary>
        public enum ShipperCode
        {
            [Description("顺丰速运")]
            SF,
            [Description("百世快递")]
            HTKY,
            [Description("中通快递")]
            ZTO,
            [Description("申通快递")]
            STO,
            [Description("圆通速递")]
            YTO,
            [Description("韵达速递")]
            YD,
            [Description("邮政快递包裹")]
            YZPY,
            [Description("EMS")]
            EMS,
            [Description("天天快递")]
            HHTT,
            [Description("京东快递")]
            JD,
            [Description("优速快递")]
            UC,
            [Description("德邦快递")]
            DBL,
            [Description("宅急送")]
            ZJS,
            [Description("安捷快递")]
            AJ,
            [Description("阿里跨境电商物流")]
            ALKJWL,
            [Description("安迅物流")]
            AX,
            [Description("安邮美国")]
            AYUS,
            [Description("亚马逊物流")]
            AMAZON,
            [Description("澳门邮政")]
            AOMENYZ,
            [Description("安能物流")]
            ANE,
            [Description("澳多多")]
            ADD,
            [Description("澳邮专线")]
            AYCA,
            [Description("安鲜达")]
            AXD,
            [Description("安能快运")]
            ANEKY,
            [Description("澳邦国际")]
            ABGJ,
            [Description("安得物流")]
            ANNTO,
            [Description("八达通")]
            BDT,
            [Description("百腾物流")]
            BETWL,
            [Description("北极星快运")]
            BJXKY,
            [Description("奔腾物流")]
            BNTWL,
            [Description("百福东方")]
            BFDF,
            [Description("贝海国际")]
            BHGJ,
            [Description("八方安运")]
            BFAY,
            [Description("百世快运")]
            BTWL,
            [Description("帮帮发转运")]
            BBFZY,
            [Description("百城通物流")]
            BCTWL,
            [Description("春风物流")]
            CFWL,
            [Description("诚通物流")]
            CHTWL,
            [Description("传喜物流")]
            CXHY,
            [Description("城市100")]
            CITY100,
            [Description("城际快递")]
            CJKD,
            [Description("CNPEX中邮快递")]
            CNPEX,
            [Description("COE东方快递")]
            COE,
            [Description("长沙创一")]
            CSCY,
            [Description("成都善途速运")]
            CDSTKY,
            [Description("联合运通")]
            CTG,
            [Description("疯狂快递")]
            CRAZY,
            [Description("CBO钏博物流")]
            CBO,
            [Description("佳吉快运")]
            CNEX,
            [Description("承诺达")]
            CND,
            [Description("畅顺通达")]
            CSTD,
            [Description("D速物流")]
            DSWL,
            [Description("到了港")]
            DLG,
            [Description("大田物流")]
            DTWL,
            [Description("东骏快捷物流")]
            DJKJWL,
            [Description("德坤")]
            DEKUN,
            [Description("德邦快运")]
            DBLKY,
            //[Description("大马鹿")]
            //DML,
            //DNWL,
            //EST365,
            //ETK,
            //EMS2,
            //EWE,
            //FKD,
            //FTD,
            //FYKD,
            //FASTGO,
            //FBKD,
            //FBOX,
            //FHKD,
            //FRGYL,
            //FYPS,
            //FYSD,
            //FT,
            //GD,
            //GDEMS,
            //GSD,
            //GTONG,
            //GDKD,
            //GHX,
            //GTKD,
            //GTKY,
            //GAI,
            //GKSD,
            //GTSD,
            //HGLL,
            //HLWL,
            //HOAU,
            //HOTSCM,
            //HPTEX,
            //hq568,
            //HQSY,
            //HXLWL,
            //HBJH,
            //HF,
            //HHKD,
            //HHWL,
            //HMJKD,
            //HMSD,
            //HQKY,
            //HSWL,
            //HTWL,
            //HXWL,
            //HFHW,
            //HLONGWL,
            //HQKD,
            //HRWL,
            //HTKD,
            //HYH,
            //HLYSD,
            //HJWL,
            //HISENSE,
            //JAD,
            //JGSD,
            //JIUYE,
            //JXD,
            //JYKD,
            //JCEX,
            //JTKD,
            //JYSY,
            //JYM,
            //JGWL,
            //JYWL,
            //JDKY,
            //JDWL,
            //JTSD,
            //KYSY,
            //KFW,
            //KSDWL,
            //KLWL,
            //KTKD,
            //KYDSD,
            //KYWL,
            //KBSY,
            //LB,
            //LHKD,
            //LJD,
            //LJS,
            //LHT,
            //MB,
            //MHKD,
            //MK,
            //MDM,
            //MD,
            //MSKD,
            //MRDY,
            //MLWL,
            //NFCM,
            //NJSBWL,
            //NEDA,
            //PADTF,
            //PANEX,
            //PJ,
            //PXWL,
            //PCA,
            //QCKD,
            //QRT,
            //QUICK,
            //QXT,
            //QYZY,
            //RFD,
            //RQ,
            //RRS,
            //RLWL,
            //RFEX,
            //SAD,
            //SNWL,
            //SAWL,
            //SBWL,
            //SDWL,
            //SFWL,
            //ST,
            //STWL,
            //SUBIDA,
            //SDEZ,
            //SCZPDS,
            //SURE,
            //SDHH,
            //SFGJ,
            //SHWL,
            //SJWL,
            //STSD,
            //SXHMJ,
            //SYKD,
            //SS,
            //STKD,
            //SJ,
            //SX,
            //SQWL,
            //SYJWDX,
            //TAIWANYZ,
            //TSSTO,
            //TJS,
            //TYWL,
            //TDHY,
            //THTX,
            //TLWL,
            //UAPEX,
            //UBI,
            //UEQ,
            //WJK,
            //WJWL,
            //WHTZX,
            //WPE,
            //WM,
            //WXWL,
            //WTP,
            //WTWL,
            //XCWL,
            //XFEX,
            //XYT,
            //XBWL,
            //XLYT,
            //XJ,
            //YADEX,
            //YCWL,
            //YCSY,
            //YDH,
            //YDT,
            //YFHEX,
            //YFSD,
            //YTKD,
            //YXKD,
            //YUNDX,
            //YMDD,
            //YZBK,
            //YZTSY,
            //YFSUYUN,
            //YSDF,
            //YF,
            //YDKY,
            //YL,
            //YBJ,
            //YFEX,
            //YJSD,
            //YLJY,
            //YLSY,
            //YMWL,
            //YSH,
            //YSKY,
            //YTD,
            //YTFH,
            //YTOGJ,
            //YXWL,
            //YZGN,
            //YZT,
            //YBWL,
            //ZENY,
            //ZRSD,
            //ZTKY,
            //ZTWY,
            //ZWYSD,
            //ZY_AZY,
            //ZY_BDA,
            //ZY_BYECO,
            //ZY_CTM,
            //ZY_CUL,
            //ZY_ETD,
            //ZY_FCKD,
            //ZY_FLSD,
            //ZY_HCYD,
            //ZY_HDB,
            //ZY_HFMZ,
            //ZY_HJSD,
            //ZY_HMKD,
            //ZY_HTAO,
            //ZY_HTCUN,
            //ZY_HTKE,
            //ZY_HTONG,
            //ZY_HXKD,
            //ZY_HXSY,
            //ZY_IHERB,
            //ZY_LPZ,
            //ZY_LZWL,
            //ZY_MBZY,
            //ZY_MJ,
            //ZY_MZ,
            //ZY_OEJ,
            //ZY_OZF,
            //ZY_OZGO,
            //ZY_QMT,
            //ZY_SCS,
            //ZY_SOHO,
            //ZY_SONIC,
            //ZY_TCM,
            //ZY_TPAK,
            //ZY_TTHT,
            //ZY_TZKD,
            //ZY_XDKD,
            //ZY_XDSY,
            //ZY_XGX,
            //ZY_XJ,
            //ZY_YGKD,
            //ZY_YJSD,
            //ZY_YPW,
            //ZY_YSW,
            //ZYQS,
            //ZYWL,
            //ZHQKD,
            //ZTE,
            //ZTOKY,
            //ZYKD,
            //ZMKM,
            //ZHWL,
            //ZTWL,
            //ZHN,
            //ZYE,
            //AAE,
            //ACS,
            //ADP,
            //ANGUILAYOU,
            //APAC,
            //ARAMEX,
            //AT,
            //AOL,
            //AUSTRALIA,
            //AUEX,
            //BEL,
            //BHT,
            //BILUYOUZHE,
            //BR,
            //BALUNZHI,
            //BETWL_Crack,
            //BEUROPE,
            //BCWELT,
            //BN,
            //BKWL,
            //BLZ,
            //BNTWL_Crack,
            //BQXHM,
            //BUDANYOUZH,
            //BSWL,
            //CCES,
            //CKY,
            //CNXLM,
            //CDEK,
            //CA,
            //CG,
            //DBYWL,
            //DDWL,
            //DGYKD,
            //DLGJ,
            //DHL,
            //DHL_DE,
            //DHL_EN,
            //DHL_GLB,
            //DHLGM,
            //DK,
            //DCWL,
            //DHL_C,
            //DHL_USA,
            //DHWL,
            //DTKD,
            //DYWL,
            //DPD,
            //D4PX,
            //DPEX,
            //DRL,
            //EMSGJ,
            //EKM,
            //ESHIPPER,
            //EPS,
            //FCWL,
            //FX,
            //FQ,
            //FLYZ,
            //FZGJ,
            //FEDEX_GJ,
            //FEDEX,
            //GJEYB,
            //GJYZ,
            //GE2D,
            //GLS,
            //GT,
            //IOZYZ,
            //IADLYYZ,
            //IAEBNYYZ,
            //IAEJLYYZ,
            //IAFHYZ,
            //IAGLYZ,
            //IAJYZ,
            //IALBYZ,
            //IALYYZ,
            //IASBJYZ,
            //IBCWNYZ,
            //IBDLGYZ,
            //IBDYZ,
            //IBELSYZ,
            //IBHYZ,
            //IBJLYYZ,
            //IBJSTYZ,
            //IBLNYZ,
            //IBOLYZ,
            //IBTD,
            //IBYB,
            //IDGYZ,
            //IWDMLYZ,
            //IWGDYZ,
            //IWKLEMS,
            //IWKLYZ,
            //IWLGYZ,
            //ILKKD,
            //IWLYZ,
            //IXGLDNYYZ,
            //IE,
            //IXPWL,
            //IYDYZ,
            //IXPSJ,
            //IEGDEYZ,
            //IELSYZ,
            //IFTWL,
            //IGDLPDYZ,
            //IGSDLJYZ,
            //IHGYZ,
            //IHLY,
            //IHSKSTYZ,
            //IHSYZ,
            //IJBBWYZ,
            //IJEJSSTYZ,
            //IJKYZ,
            //IJNYZ,
            //IJPZYZ,
            //IADLSQDYZ,
            //IAGTYZ,
            //IALQDYZ,
            //IAMYZ,
            //IASEBYYZ,
            //IASNYYZ,
            //IASSDYZ,
            //IBLSD,
            //IBLYZ,
            //IBMDYZ,
            //IDFWL,
            //IELTLYYZ,
            //IGDLPDEMS,
            //IGJESD,
            //IGLBYYZ,
            //IGLLYZ,
            //IKTDWYZ,
            //IKTEYZ,
            //ILBYYZ,
            //ILSBYZ,
            //ILTWYYZ,
            //ILTWYZ,
            //ILZDSDYZ,
            //IMEDFYZ,
            //IMJLGEMS,
            //IMLGYZ,
            //IMLQSYZ,
            //IMLXYEMS,
            //IMLXYYZ,
            //IMQDYZ,
            //IMTNKEMS,
            //IMTNKYZ,
            //IMXGYZ,
            //INFYZ,
            //INWYZ,
            //IPTYYZ,
            //IQQKD,
            //IQTWL,
            //ISDYZ,
            //ISEWDYZ,
            //ISLFKYZ,
            //ISLWNYYZ,
            //ISTALBYZ,
            //ITEQYZ,
            //ITGYZ,
            //ITLNDHDBGE,
            //ITNSYZ,
            //ITSNYYZ,
            //IWZBKSTEMS,
            //IXFLWL,
            //IXJPYZ,
            //IXLYYZ,
            //IXYLYZ,
            //IYDNXYYZ,
            //IYLYZ,
            //IYNYZ,
            //IYSLYZ,
            //IYTG,
            //IYWWL,
            //IZBLTYZ,
            //IKNDYYZ,
            //IKNYYZ,
            //IKTDWEMS,
            //ILMNYYZ,
            //IMEDWYZ,
            //IMETYZ,
            //INRLYYZ,
            //ISEWYYZ,
            //ISPLSYZ,
            //IWZBKSTYZ,
            //IXBYYZ,
            //IXJPEMS,
            //IXLYZ,
            //IXXLYZ,
            //IYDLYZ,
            //IYGYZ,
            //IYMNYYZ,
            //IZLYZ,
            //IYMYZ,
            //JP,
            //JFGJ,
            //JGZY,
            //JXYKD,
            //JLDT,
            //JYSD,
            //JPKD,
            //LYT,
            //LHKDS,
            //NSF,
            //NL,
            //ONTRAC,
            //OCS,
            //PAPA,
            //POSTEIBE,
            //QQYZ,
            //QYHY,
            //RDSE,
            //RLG,
            //SKYPOST,
            //SHLDHY,
            //SYJHE,
            //SWCH,
            //SDSY,
            //SK,
            //STONG,
            //STO_INTL,
            //SUNSHINE,
            //TNT,
            //TAILAND138,
            //UBONEX,
            //UEX,
            //USPS,
            //UPU,
            //UPS,
            //VENUCIA,
            //VCTRANS,
            //XKGJ,
            //XD,
            //XGYZ,
            //XLKD,
            //XSRD,
            //XYGJ,
            //XYJ,
            //XYGJSD,
            //YAMA,
            //YODEL,
            //YHXGJSD,
            //YUEDANYOUZ,
            //YMSY,
            //YYSD,
            //YJD,
            //YBG,
            //YJ,
            //ZY_AG,
            //ZY_AOZ,
            //ZY_AUSE,
            //ZY_AXO,
            //ZY_BH,
            //ZY_BEE,
            //ZY_BL,
            //ZY_BM,
            //ZY_BT,
            //ZY_CM,
            //ZY_EFS,
            //ZY_ESONG,
            //ZY_FD,
            //ZY_FG,
            //ZY_FX,
            //ZY_FXSD,
            //ZY_FY,
            //ZY_HC,
            //ZY_HYSD,
            //ZY_JA,
            //ZY_JD,
            //ZY_JDKD,
            //ZY_JDZY,
            //ZY_JH,
            //ZY_JHT,
            //ZY_LBZY,
            //ZY_LX,
            //ZY_MGZY,
            //ZY_MST,
            //ZY_MXZY,
            //ZY_QQEX,
            //ZY_RT,
            //ZY_RTSD,
            //ZY_SDKD,
            //ZY_SFZY,
            //ZY_ST,
            //ZY_TJ,
            //ZY_TM,
            //ZY_TN,
            //ZY_TPY,
            //ZY_TSZ,
            //ZY_TWC,
            //ZY_RDGJ,
            //ZY_TX,
            //ZY_TY,
            //ZY_DGHT,
            //ZY_DYW,
            //ZY_WDCS,
            //ZY_TZH,
            //ZY_UCS,
            //ZY_XC,
            //ZY_XF,
            //ZY_YQ,
            //ZY_YSSD,
            //ZY_YTUSA,
            //ZY_ZCSD,
            //ZYZOOM,
            //ZH,
            //ZO,
            //ZSKY,
            //ZWSY,
            //ZZJH,
            //ZTOGLOBAL
        }
    }
}