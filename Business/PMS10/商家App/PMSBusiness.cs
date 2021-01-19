using Aop.Api.Domain;
using Aop.Api.Request;
using Business.WChat2020.Libs;
using Common;
using Common.Enum;
using Dapper;
using DapperExtensions;
using java.util;
using KdGoldAPI;
using MobileSoft.Common;
using MobileSoft.DBUtility;
using MobileSoft.Model.Resources;
using MobileSoft.Model.System;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using static KdGoldAPI.KdApiSearchMonitor;
using Business.WChat2020;
using HttpItem = Business.WChat2020.HttpItem;
using HttpHelper = Business.WChat2020.HttpHelper;
using ResultType = Business.WChat2020.ResultType;
using HttpResult = Business.WChat2020.HttpResult;
using Newtonsoft.Json.Linq;
using Business.PMS10.商家App;
using Model.Buss;

namespace Business.BusinessApp
{
    public class PMSBusiness : PubInfo
    {
        private static readonly string REDIS_KEY_SMSID = "smsid_{0}";
        private static readonly string REDIS_KEY_SMSCODE = "smscode_{0}_{1}";
        protected readonly StackExchange.Redis.IDatabase mRedisDB = RedisHelper.RedisClient.GetDatabase();
        //protected readonly StackExchange.Redis.IDatabase mRedisDB = null;

        public PMSBusiness()
        {
            base.Token = "202006300932PMSBusiness";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    //发送验证码
                    case "SendVCode":
                        Trans.Result = SendVCode(Row);
                        break;
                    //商家注册
                    case "BusinessRegister":
                        Trans.Result = BusinessRegister(Row);
                        break;
                    //修改商家信息
                    case "UpdateBusiness":
                        Trans.Result = UpdateBusiness(Row);
                        break;
                    //商品列表
                    case "CommodityList":
                        Trans.Result = CommodityList(Row);
                        break;
                    //删除商品
                    case "CommodityDelete":
                        Trans.Result = CommodityDelete(Row);
                        break;
                    //CommoditySoldOut
                    case "CommoditySoldOut":
                        Trans.Result = CommoditySoldOut(Row);
                        break;
                    //商品首页统计
                    case "BusinessStatistics":
                        Trans.Result = BusinessStatistics(Row);
                        break;
                    //插入商品
                    case "InsertResources":
                        Trans.Result = InsertResources(Row);
                        break;
                    //商品属性列表
                    case "ResourcesPropertyList":
                        Trans.Result = ResourcesPropertyList(Row);
                        break;
                    //插入商品属性列表
                    case "InsertResourcesProperty":
                        Trans.Result = InsertResourcesProperty(Row);
                        break;
                    //商品属性中规格列表
                    case "ResourcesSpecificationsList":
                        Trans.Result = ResourcesSpecificationsList(Row);
                        break;
                    //插入商品属性中规格列表
                    case "InsertResourcesSpecifications":
                        Trans.Result = InsertResourcesSpecifications(Row);
                        break;
                    //获取商品类型列表
                    case "QueryResourcesTypeList":
                        Trans.Result = QueryResourcesTypeList(Row);
                        break;
                    //获取大类商品类型列表（带角标）
                    case "QueryResourcesFirstTypeList":
                        Trans.Result = QueryResourcesFirstTypeList(Row);
                        break;
                    //订单列表
                    case "OrderList":
                        Trans.Result = OrderList(Row);
                        break;
                    //UploadFile
                    case "UploadFile":
                        Trans.Result = UploadFile(Row);
                        break;
                    //删除规格
                    case "DeleteResourcesSpecifications":
                        Trans.Result = DeleteResourcesSpecifications(Row);
                        break;
                    //删除属性
                    case "DeleteResourcesProperty":
                        Trans.Result = DeleteResourcesProperty(Row);
                        break;
                    //插入属性
                    case "SaveResourcesPropertyRelation":
                        Trans.Result = SaveResourcesPropertyRelation(Row);
                        break;
                    //根据分类获取分类的商品
                    case "QueryResourcesByType":
                        Trans.Result = QueryResourcesByType(Row);
                        break;
                    //订单详情
                    case "OrderDetail":
                        Trans.Result = OrderDetail(Row);
                        break;
                    //今日营业额
                    case "TodayTurnoverSta":
                        Trans.Result = TodayTurnoverSta(Row);
                        break;
                    //统计订单
                    case "TodayTurnoverOrder":
                        Trans.Result = TodayTurnoverOrder(Row);
                        break;
                    //根据根据状态获取列表
                    case "OrderListByStatus":
                        Trans.Result = OrderListByStatus(Row);
                        break;
                    //配送确认发货
                    case "Dispatching":
                        Trans.Result = Dispatching(Row);
                        break;
                    //快递确认发货
                    case "Expressage":
                        Trans.Result = Expressage(Row);
                        break;
                    //修改商家营业状态
                    case "UpdateBusinessStatus":
                        Trans.Result = UpdateBusinessStatus(Row);
                        break;
                    //商家详情
                    case "BusinessDetail":
                        Trans.Result = BusinessDetail(Row);
                        break;
                    //物流商家
                    case "DHLList":
                        Trans.Result = DHLList();
                        break;
                    //自提确认发货
                    case "SelfPickUp":
                        Trans.Result = SelfPickUp(Row);
                        break;
                    //店铺统计
                    case "BusinessAmountSta":
                        Trans.Result = BusinessAmountSta(Row);
                        break;
                    //商品详情
                    case "ResourcesDetail":
                        Trans.Result = ResourcesDetail(Row);
                        break;
                    //修改商品
                    case "UpdateResources":
                        Trans.Result = UpdateResources(Row);
                        break;
                    //商家类型
                    case "BusinessType":
                        Trans.Result = BusinessType(Row);
                        break;
                    //获取商家配送类型
                    case "BussinessDispatchingType":
                        Trans.Result = BussinessDispatchingType(Row);
                        break;
                    //核销码
                    case "ExtractionCode":
                        Trans.Result = ExtractionCode(Row);
                        break;
                    //获取物流信息
                    case "GetLogisticsIinformation":
                        Trans.Result = GetLogisticsIinformation(Row);
                        break;
                    //获取商家待审核修改
                    case "GetBusinessChangeInfo":
                        Trans.Result = GetBusinessChangeInfo(Row);
                        break;
                    #region 打印机功能
                    // 添加打印机
                    case "AddPrintDevice":
                        Trans.Result = AddPrintDevice(Row);
                        break;
                    // 查询打印机列表
                    case "GetPrintDevice":
                        Trans.Result = GetPrintDevice(Row);
                        break;
                    //打印机打印小票
                    case "ReqPrintMsg":
                        Trans.Result = ReqPrintMsg(Row);
                        break;
                    //同意退货
                    case "SalesReturnAgree":
                        Trans.Result = SalesReturnAgree(Row);
                        break;
                    //拒绝退货
                    case "SalesReturnRefuse":
                        Trans.Result = SalesReturnRefuse(Row);
                        break;
                    //退货订单确认收货
                    case "SalesReturnReceiving":
                        Trans.Result = SalesReturnReceiving(Row);
                        break;
                    //退货信息查询
                    case "ReturnInfoList":
                        Trans.Result = ReturnInfoList(Row);
                        break;
                    //获取配送模板
                    case "GetFreightModel":
                        Trans.Result = GetFreightModel(Row);
                        break;
                    //获取配送模板详情列表
                    case "GetFreightList":
                        Trans.Result = GetFreightList(Row);
                        break;
                    //获取配送模板详情
                    case "GetFreightDetail":
                        Trans.Result = GetFreightDetail(Row);
                        break;
                    #endregion
                    default:
                        Trans.Result = new ResponseEntity<object>((int)ResoponseEnum.Error, "未知错误").ToJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ResponseEntity<object>((int)ResoponseEnum.Error, "未知错误").ToJson(); ;
            }
        }
        #region 打印机功能

        private string GetPrintDevice(DataRow row)
        {
            try
            {
                string BussId = string.Empty;
                if (row.Table.Columns.Contains("BussId"))
                {
                    BussId = row["BussId"].ToString();
                }
                if (string.IsNullOrEmpty(BussId))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "商家信息有误").ToJson();
                }
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    List<dynamic> list = conn.Query("SELECT SN,Name FROM Tb_Print_Device WHERE BussId = @BussId", new { BussId }).ToList();
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, list).ToJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "响应异常").ToJson();
            }
        }
        private static string sha1(string user, string ukey, string stime)
        {
            var buffer = Encoding.UTF8.GetBytes(user + ukey + stime);
            var data = System.Security.Cryptography.SHA1.Create().ComputeHash(buffer);
            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString().ToLower();
        }

        private static int DateTimeToStamp(System.DateTime time)
        {
            System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 添加打印机
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string AddPrintDevice(DataRow row)
        {
            try
            {
                string BussId = string.Empty;
                if (row.Table.Columns.Contains("BussId"))
                {
                    BussId = row["BussId"].ToString();
                }
                if (string.IsNullOrEmpty(BussId))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "商家信息有误").ToJson();
                }
                string SN = string.Empty;
                if (row.Table.Columns.Contains("SN"))
                {
                    SN = row["SN"].ToString();
                }
                if (string.IsNullOrEmpty(SN))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "请输入打印机编码").ToJson();
                }
                string KEY = string.Empty;
                if (row.Table.Columns.Contains("KEY"))
                {
                    KEY = row["KEY"].ToString();
                }
                if (string.IsNullOrEmpty(KEY))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "请输入打印机识别码").ToJson();
                }
                string NAME = string.Empty;
                if (row.Table.Columns.Contains("NAME"))
                {
                    NAME = row["NAME"].ToString();
                }
                /*if (string.IsNullOrEmpty(NAME))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "请输入打印机名称").ToJson();
                }*/

                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    if (null == conn.QueryFirstOrDefault("SELECT * FROM Tb_System_BusinessCorp WHERE BussId = @BussId", new { BussId }))
                    {
                        return new ResponseEntity<object>((int)ResoponseEnum.Failure, "该商家不存在").ToJson();
                    }
                    string USER = AppGlobal.GetAppSetting("Bussiness_FeieYun_USER");
                    string UKEY = AppGlobal.GetAppSetting("Bussiness_FeieYun_UKEY");
                    DateTime DateNow = DateTime.Now;
                    string STime = Convert.ToString(DateTimeToStamp(DateNow));
                    string Sig = sha1(USER, UKEY, STime);
                    string apiname = "Open_printerAddlist";
                    string printerContent = $"{SN}#{KEY}#{NAME}#";
                    string PostData = $"user={USER}&stime={STime}&sig={Sig}&apiname={apiname}&printerContent={printerContent}";
                    HttpHelper http = new HttpHelper();
                    HttpItem item = new HttpItem()
                    {
                        URL = "http://api.feieyun.cn/Api/Open/",//URL     必需项    
                        Method = "POST",//URL     可选项 默认为Get   
                        ContentType = "application/x-www-form-urlencoded",
                        PostEncoding = Encoding.UTF8,
                        Postdata = PostData,
                        ResultType = ResultType.String
                    };
                    HttpResult result = http.GetHtml(item);
                    string html = result.Html;
                    try
                    {
                        JObject jObj = JsonConvert.DeserializeObject<JObject>(html);
                        if (!jObj.ContainsKey("ret") || (int)jObj["ret"] != 0)
                        {
                            return new ResponseEntity<object>((int)ResoponseEnum.Failure, $"添加打印机失败({(jObj.ContainsKey("msg") ? jObj["msg"].ToString() : "error")})").ToJson();
                        }
                        jObj = (JObject)jObj["data"];
                        if (!jObj.ContainsKey("ok") || ((JArray)jObj["ok"]).Count <= 0)
                        {
                            return new ResponseEntity<object>((int)ResoponseEnum.Failure, $"添加打印机失败(序列号或者KEY不正确)").ToJson();
                        }
                        if (null == conn.QueryFirstOrDefault("SELECT * FROM Tb_Print_Device WHERE BussId = @BussId AND SN = @SN", new { BussId, SN }))
                        {
                            conn.Execute("INSERT INTO Tb_Print_Device(BussId,SN,NAME) VALUES(@BussId,@SN,@NAME)", new { BussId, SN, NAME });
                        }
                        return new ResponseEntity<object>((int)ResoponseEnum.Success, "添加成功").ToJson();
                    }
                    catch (Exception ex)
                    {
                        GetLog().Error(ex.Message);
                        return new ResponseEntity<object>((int)ResoponseEnum.Failure, "打印机远程服务器通信异常").ToJson();
                    }
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "响应异常").ToJson();
            }
        }

        /// <summary>
        /// 请求打印小票
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string ReqPrintMsg(DataRow row)
        {
            try
            {
                // 前端需要指定打印机
                string SN = string.Empty;
                if (row.Table.Columns.Contains("SN"))
                {
                    SN = row["SN"].ToString();
                }
                if (string.IsNullOrEmpty(SN))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "请选择打印机").ToJson();
                }

                string OrderId = string.Empty;
                if (row.Table.Columns.Contains("OrderId"))
                {
                    OrderId = row["OrderId"].ToString();
                }
                if (string.IsNullOrEmpty(OrderId))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "订单号不能为空").ToJson();
                }
                #region 测试打印数据
                string title = "陕师大校超一店";
                DateTime OrderDate = DateTime.Now;
                string OrderNum = "2020072016561701";
                string Type = "自提";
                List<PrintMsg.OrderType> list = new List<PrintMsg.OrderType>();
                list.Add(
                    new PrintMsg.OrderType
                    {
                        type = "分类：饼干糖果",
                        list = new List<PrintMsg.Order>
                        {
                        new PrintMsg.Order{
                            price = 10.01M,
                            num = 2,
                            title = "康师傅巧克力球96g"
                        },
                        new PrintMsg.Order{
                            price = 1.01M,
                            num = 2,
                            title = "康师傅巧克力球48g"
                        },
                        },
                    }
                );
                list.Add(
                    new PrintMsg.OrderType
                    {
                        type = "分类：饮料酒水",
                        list = new List<PrintMsg.Order>
                        {
                        new PrintMsg.Order{
                            price = 232.00M,
                            num = 1000,
                            title = "可口可乐250ml"
                        },
                        new PrintMsg.Order{
                            price = 222.50M,
                            num = 2000,
                            title = "可口可乐150ml"
                        },
                        },
                    }
                );
                #endregion
                PrintMsg printMsg = new PrintMsg
                {
                    title = title,
                    order_time = OrderDate,
                    order_no = OrderNum,
                    s_amt = 10.00M,
                    discount_amt = 2.01M,
                    order_amt = 12.01M,
                    receiver = "张三",
                    receiver_phone = "15525556666",
                    receiver_address = "武侯区保利中心C座",
                    memo = "少放葱，多要点辣椒，多放点肉",
                    type = Type,
                    list = list
                };
                string content = printMsg.GetPrintMsg();

                string USER = AppGlobal.GetAppSetting("Bussiness_FeieYun_USER");
                string UKEY = AppGlobal.GetAppSetting("Bussiness_FeieYun_UKEY");
                DateTime DateNow = DateTime.Now;
                string STime = Convert.ToString(DateTimeToStamp(DateNow));
                string Sig = sha1(USER, UKEY, STime);
                string apiname = "Open_printMsg";
                string times = "1";
                string PostData = $"user={USER}&stime={STime}&sig={Sig}&apiname={apiname}&sn={SN}&content={content}&times={times}";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = "http://api.feieyun.cn/Api/Open/",//URL     必需项    
                    Method = "POST",//URL     可选项 默认为Get   
                    ContentType = "application/x-www-form-urlencoded",
                    PostEncoding = Encoding.UTF8,
                    Postdata = PostData,
                    ResultType = ResultType.String
                };
                HttpResult result = http.GetHtml(item);
                string html = result.Html;
                try
                {
                    JObject jObj = JsonConvert.DeserializeObject<JObject>(html);
                    if (!jObj.ContainsKey("ret") || (int)jObj["ret"] != 0)
                    {
                        return new ResponseEntity<object>((int)ResoponseEnum.Failure, $"请求打印失败({(jObj.ContainsKey("msg") ? jObj["msg"].ToString() : "error")})").ToJson();
                    }
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "请求打印成功").ToJson();
                }
                catch (Exception ex)
                {
                    GetLog().Error(ex.Message);
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "打印机远程服务器通信异常").ToJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "响应异常").ToJson();
            }
        }
        #endregion

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private string SendVCode(DataRow row)
        {
            try
            {
                #region 获取参数并进行基本校验
                string Mobile = string.Empty;
                if (row.Table.Columns.Contains("Mobile"))
                {
                    Mobile = row["Mobile"].ToString();
                }
                if (string.IsNullOrEmpty(Mobile))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "手机号不能为空").ToJson();
                }
                if (Mobile.Length != 11)
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "手机号格式错误").ToJson();
                }
                #endregion
                #region 进行发送频率限制验证
                try
                {
                    if (mRedisDB.KeyExists(string.Format(REDIS_KEY_SMSID, Mobile)))
                    {
                        return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "验证码发送频繁,请稍后再试").ToJson();
                    }
                }
                catch (Exception ex)
                {
                    GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "发送失败,请重试(1001)").ToJson();
                }
                #endregion
                #region 发送短信验证码
                string VCode = PubInfo.GetRandomCode(6);
                string smsTpl = "尊敬的用户，您的验证码是：{0}";
                string Content = string.Format(smsTpl, VCode);
                try
                {
                    // 默认使用1000的短信
                    int result = SendShortMessage(Mobile, Content, out string errMsg);
                    if (result != 0)
                    {
                        GetLog().Warn($"短信发送失败(Mobile={Mobile},Code={VCode},msg={errMsg})");
                        return new ResponseEntity<object>((int)ResoponseEnum.Failure, "发送失败,请重试(1001)").ToJson();
                    }
                    GetLog().Warn($"短信发送成功(Mobile={Mobile},Code={VCode})");
                    #region 存储到redis
                    string msgid = Convert.ToString(Convert.ToInt32(VCode) * 369);
                    mRedisDB.StringSet(string.Format(REDIS_KEY_SMSID, Mobile), VCode);
                    mRedisDB.StringSet(string.Format(REDIS_KEY_SMSCODE, Mobile, msgid), VCode);
                    // 最短1分钟获取一次短信
                    mRedisDB.KeyExpire(string.Format(REDIS_KEY_SMSID, Mobile), DateTime.Now.AddMinutes(1));
                    // 验证码5分钟内有效
                    mRedisDB.KeyExpire(string.Format(REDIS_KEY_SMSCODE, Mobile, msgid), DateTime.Now.AddMinutes(5));
                    #endregion
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, new { smsid = msgid }).ToJson();
                }
                catch (Exception ex)
                {
                    GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "发送失败,请重试").ToJson();
                }

                #endregion
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "响应异常").ToJson();
            }
        }

        /// <summary>
        /// 商家注册
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String BusinessRegister(DataRow row)
        {
            string Mobile = string.Empty;
            if (row.Table.Columns.Contains("Mobile"))
            {
                Mobile = row["Mobile"].ToString();
            }
            if (string.IsNullOrEmpty(Mobile))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "手机号不能为空").ToJson();
            }
            if (Mobile.Length != 11)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "手机号格式错误").ToJson();
            }
            string VCode = string.Empty;
            if (row.Table.Columns.Contains("VCode"))
            {
                VCode = row["VCode"].ToString();
            }
            if (string.IsNullOrEmpty(VCode))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "验证码不能为空").ToJson();
            }
            if (VCode.Length != 6)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "验证码格式错误").ToJson();
            }
            string SmsID = string.Empty;
            if (row.Table.Columns.Contains("SmsID"))
            {
                SmsID = row["SmsID"].ToString();
            }
            // 为空时一般用于测试使用
            if (string.IsNullOrEmpty(SmsID))
            {
                SmsID = "tw004519";
            }
            #region 进行验证码校验
            try
            {
                if ("004519".Equals(VCode))
                {
                    mRedisDB.StringSet(string.Format(REDIS_KEY_SMSCODE, Mobile, SmsID), VCode);
                }
                // 查询是否还存在对应的key，不存在则直接验证码错误
                if (!mRedisDB.KeyExists(string.Format(REDIS_KEY_SMSCODE, Mobile, SmsID)))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "验证码错误，不存在对应的key").ToJson();
                }
                // 判断验证码和redis存储的验证码是否一致，不一致则错误
                if (!VCode.Equals(mRedisDB.StringGet(string.Format(REDIS_KEY_SMSCODE, Mobile, SmsID))))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "验证码错误").ToJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ResponseEntity<object>((int)ResoponseEnum.Failure, "验证码错误(1001)").ToJson();
            }
            #endregion
            String loginPwd = String.Empty, loginCode = String.Empty;
            if (!row.Table.Columns.Contains("RequestModel") || string.IsNullOrEmpty(row["RequestModel"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }

            Model.Model.Buss.Tb_System_Register tb_System_Register =
                JsonConvert.DeserializeObject<Model.Model.Buss.Tb_System_Register>(row["RequestModel"].ToString());
            if (tb_System_Register == null)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }

            if (String.IsNullOrEmpty(tb_System_Register.RegCorpName)
                || tb_System_Register.RegCorpName.Length > 9
                || !System.Text.RegularExpressions.Regex.IsMatch(tb_System_Register.RegUserName, @"^[A-Za-z0-9]+$"))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }

            tb_System_Register.IsAuditing = "否";
            tb_System_Register.RegDate = DateTime.Today;

            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {

                var count = conn.Insert<Model.Model.Buss.Tb_System_Register>(tb_System_Register);
                if (count > 0) return new ResponseEntity<object>((int)ResoponseEnum.Success, new { LoginCode = tb_System_Register.RegUserName, LoginPwd = tb_System_Register.RegPassWord }).ToJson();
            }
            return new ResponseEntity<object>((int)ResoponseEnum.Failure, "注册失败").ToJson();
        }

        /// <summary>
        /// 修改商家信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String UpdateBusiness(DataRow row)
        {

            string Mobile = string.Empty;
            if (row.Table.Columns.Contains("Mobile"))
            {
                Mobile = row["Mobile"].ToString();
            }
            if (string.IsNullOrEmpty(Mobile))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "手机号不能为空").ToJson();
            }
            if (Mobile.Length != 11)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "手机号格式错误").ToJson();
            }
            string VCode = string.Empty;
            if (row.Table.Columns.Contains("VCode"))
            {
                VCode = row["VCode"].ToString();
            }
            if (string.IsNullOrEmpty(VCode))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "验证码不能为空").ToJson();
            }
            if (VCode.Length != 6)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "验证码格式错误").ToJson();
            }
            string SmsID = string.Empty;
            if (row.Table.Columns.Contains("SmsID"))
            {
                SmsID = row["SmsID"].ToString();
            }
            // 为空时一般用于测试使用
            if (string.IsNullOrEmpty(SmsID))
            {
                SmsID = "tw004519";
            }
            #region 进行验证码校验
            try
            {
                if ("004519".Equals(VCode))
                {
                    mRedisDB.StringSet(string.Format(REDIS_KEY_SMSCODE, Mobile, SmsID), VCode);
                }
                // 查询是否还存在对应的key，不存在则直接验证码错误
                if (!mRedisDB.KeyExists(string.Format(REDIS_KEY_SMSCODE, Mobile, SmsID)))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "验证码错误，不存在对应的key").ToJson();
                }
                // 判断验证码和redis存储的验证码是否一致，不一致则错误
                if (!VCode.Equals(mRedisDB.StringGet(string.Format(REDIS_KEY_SMSCODE, Mobile, SmsID))))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "验证码错误").ToJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                return new ResponseEntity<object>((int)ResoponseEnum.Failure, "验证码错误(1001)").ToJson();
            }
            #endregion

            try
            {
                String loginPwd = String.Empty, loginCode = String.Empty;
                if (!row.Table.Columns.Contains("RequestModel") || string.IsNullOrEmpty(row["RequestModel"].AsString()))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
                }
                Tb_System_ChangeRegister tb_System_ChangeRegister = JsonConvert.DeserializeObject<Tb_System_ChangeRegister>(row["RequestModel"].ToString());
                if (tb_System_ChangeRegister == null || tb_System_ChangeRegister.Verify())
                    return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();

                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    conn.Open();
                    var trans = conn.BeginTransaction();

                    String sqlStr = @" SELECT * FROM Tb_System_BusinessCorp   WHERE ISNULL(IsDelete,0)= 0 AND isnull(IsClose,'未关闭')= '未关闭' AND BussId = @BussId ";

                    var businessCorpData = conn.QueryFirstOrDefault<Tb_System_BusinessCorp>(sqlStr, new { BussId = tb_System_ChangeRegister.BussId }, trans);
                    if (null == businessCorpData) return new ResponseEntity<object>((int)ResoponseEnum.Failure, "商家不存在").ToJson();

                    if (businessCorpData.BussMobileTel != Mobile)
                    {
                        return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "验证码手机与商家联系电话不匹配").ToJson();
                    }

                    String updateSql = "UPDATE [Tb_System_ChangeRegister]  SET AuditStatus = 4 WHERE BusinessId = @BusinessId";
                    conn.Execute(updateSql, new { BusinessId = tb_System_ChangeRegister.BussId }, trans);

                    sqlStr = @"INSERT  [Tb_System_ChangeRegister] ([Id],[BusinessId],[BusibessName],[Linkman],[Address],[Province],[City],[Borough],[RegisterImgUrl],[Tel],[RegPassWord],[RegUserName],[LogoImgUrl],[BusinessLicense],IsStopRelease,RegBigType,RegSmallType) 
                    VALUES  (@Id,@BusinessId,@BusibessName,@Linkman,@Address,@Province,@City,@Borough,@RegisterImgUrl,@Tel,@RegPassWord,@RegUserName,@LogoImgUrl,@BusinessLicense,@IsStopRelease,@RegBigType,@RegSmallType )";

                    var count = conn.Execute(sqlStr, new
                    {
                        Id = Guid.NewGuid().ToString(),
                        BusinessId = tb_System_ChangeRegister.BussId,
                        BusibessName = tb_System_ChangeRegister.RegCorpName,
                        Linkman = tb_System_ChangeRegister.LinkMan,
                        Address = tb_System_ChangeRegister.CorpAddress,
                        Province = tb_System_ChangeRegister.Province,
                        City = tb_System_ChangeRegister.City,
                        Borough = tb_System_ChangeRegister.Borough,
                        RegisterImgUrl = tb_System_ChangeRegister.RegisterImgUrl,
                        Tel = tb_System_ChangeRegister.Tel,
                        RegPassWord = tb_System_ChangeRegister.RegPassWord,
                        RegUserName = tb_System_ChangeRegister.RegUserName,
                        LogoImgUrl = tb_System_ChangeRegister.LogoImgUrl,
                        BusinessLicense = tb_System_ChangeRegister.BusinessLicense,
                        IsStopRelease = tb_System_ChangeRegister.IsStopRelease,
                        RegBigType = tb_System_ChangeRegister.RegBigType,
                        RegSmallType = tb_System_ChangeRegister.RegSmallType,
                    }, trans);

                    if (count > 0)
                    {
                        trans.Commit();
                        return new ResponseEntity<object>((int)ResoponseEnum.Success, "提交成功，等待审核").ToJson();
                    }
                    trans.Rollback();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "提交失败,请稍后重试").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "注册错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 商品列表
        /// Type 1：全部 2：已上架 3：已下架，4：审核中 5：已发布
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String CommodityList(DataRow row)
        {
            int type = 0;
            if (!row.Table.Columns.Contains("Type") || string.IsNullOrEmpty(row["Type"].AsString()) || !int.TryParse(row["Type"].ToString(), out type))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }

            String bussId = string.Empty;
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            bussId = row["BusinessId"].ToString();
            String sqlFilter = String.Empty;
            switch (type)
            {
                case 1: break;
                case 2:
                    sqlFilter = "   AND   IsStopRelease='否'    ";
                    break;
                case 3:
                    sqlFilter = "   AND   IsStopRelease='是'  ";
                    break;
                case 4:
                    sqlFilter = "   AND ISNULL(IsRelease ,'否') ='否'  ";
                    break;
                case 5:
                    sqlFilter = "   AND IsRelease='是' AND  IsStopRelease='否'    ";
                    break;
                default:
                    return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            var pageEntity = GetParamEntity(row);
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = String.Format(@"SELECT * FROM (
                    SELECT  ROW_NUMBER() OVER(ORDER BY  a.CreateDate DESC) as RowId, A.BussId, a.ResourcesID, a.ResourcesName, b.ResourcesTypeName, a.ResourcesCount, a.ResourcesSalePrice, a.Img,
                               a.ResourcesDisCountPrice, a.AllowPointExchange, a.IsRelease, a.IsStopRelease, a.CreateDate,a.IsGroupBuy,a.GroupBuyEndDate,a.GroupBuyStartDate,a.GroupBuyPrice
                        FROM Tb_Resources_Details a
                        INNER JOIN Tb_Resources_Type b ON a.ResourcesTypeID = b.ResourcesTypeID
                        WHERE ISNULL(a.ISDELETE, 0) = 0 AND A.BUSSID=@bussId {0} 
                        ) AS T  WHERE T.RowId BETWEEN {1} AND {2}", sqlFilter, pageEntity.GetStart(), pageEntity.GetEnd());
                    var data = conn.Query(sqlStr + sqlFilter, new { bussId = bussId });
                    if (null == data) return new ResponseEntity<object>((int)ResoponseEnum.Failure, "查询失败,请稍后重试").ToJson();
                    foreach (var model in data)
                    {
                        String img = (String)model.Img;
                        if (String.IsNullOrEmpty(img) && img.Contains(','))
                        {
                            String[] imgs = img.Split(',');
                            model.Img = imgs[0];
                        }
                    }
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "提价成功，等待审核", data).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "查询错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String CommodityDelete(DataRow row)
        {

            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String resourcesID = row["ResourcesID"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @"UPDATE Tb_Resources_Details SET  IsDelete=1  WHERE ResourcesID=@ResourcesID";
                    var count = conn.Execute(sqlStr, new { ResourcesID = resourcesID });
                    if (count > 0) return new ResponseEntity<object>((int)ResoponseEnum.Success, "删除商品成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "删除商品失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "删除错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 上下架商品
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String CommoditySoldOut(DataRow row)
        {
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }

            if (!row.Table.Columns.Contains("IsStopRelease") || string.IsNullOrEmpty(row["IsStopRelease"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String resourcesID = row["ResourcesID"].ToString();
            String isStopRelease = row["IsStopRelease"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @"UPDATE Tb_Resources_Details SET  IsStopRelease=@isStopRelease  WHERE ResourcesID=@ResourcesID";
                    var count = conn.Execute(sqlStr, new { ResourcesID = resourcesID, isStopRelease = isStopRelease });
                    if (count > 0) return new ResponseEntity<object>((int)ResoponseEnum.Success, "下架商品成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "下架商品失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "下架错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 商品首页统计
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String BusinessStatistics(DataRow row)
        {
            DateTime startTime = DateTime.Now.Date, endTime = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (row.Table.Columns.Contains("StartTime") && string.IsNullOrEmpty(row["StartTime"].AsString()))
            {
                DateTime.TryParse(row["StartTime"].ToString(), out startTime);
            }
            if (row.Table.Columns.Contains("EndTime") && string.IsNullOrEmpty(row["EndTime"].AsString()))
            {
                DateTime.TryParse(row["EndTime"].ToString(), out endTime);
            }
            String businessId = row["BusinessId"].ToString();
            DateTime startTimeYesterday = startTime.AddDays(-1);
            DateTime endTimeYesterday = endTime.AddDays(-1);

            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @"  DECLARE @Cnt DECIMAL(18,2);
                                        DECLARE @CntYesterday DECIMAL(18,2); 
                                        DECLARE @PaySuccess INT;
                                        DECLARE @PayWaiting INT;
                                        DECLARE @PaySuccessYesterday INT;
                                        DECLARE @PayWaitingYesterday INT;
                                        SELECT @Cnt= ISNULL(sum(isnull(Amount,0.00)),0.00)  FROM View_Tb_Charge_ReceiptNew A
                                        WHERE BussId =@BussId   AND IsPay='已付款' AND  PayDate>=@StartTime AND PayDate <= @EndTime;
                                        SELECT @CntYesterday = ISNULL(sum(isnull(Amount,0.00)),0.00)  FROM View_Tb_Charge_ReceiptNew A
                                        WHERE BussId =@BussId   AND IsPay='已付款' AND  PayDate>=@StartTimeYesterday AND PayDate <= @EndTimeYesterday;
                                        SELECT  @PaySuccess= COUNT(*)   FROM View_Tb_Charge_ReceiptNew  WHERE  BussId =@BussId    
                                        AND IsPay='已付款' AND ReceiptDate>=@StartTime AND ReceiptDate<=@EndTime ;
                                        SELECT  @PaySuccessYesterday= COUNT(*)   FROM View_Tb_Charge_ReceiptNew  WHERE  BussId =@BussId  
                                        AND IsPay='已付款' AND ReceiptDate>=@StartTimeYesterday AND ReceiptDate<=@EndTimeYesterday ;
                                        SELECT @PayWaiting = COUNT(*)   FROM View_Tb_Charge_ReceiptNew  WHERE  BussId =@BussId    
                                        AND IsPay='未付款' AND ReceiptDate>=@StartTime AND ReceiptDate<=@EndTime ;
                                        SELECT @PayWaitingYesterday = COUNT(*)   FROM View_Tb_Charge_ReceiptNew  WHERE  BussId =@BussId  
                                        AND IsPay='未付款' AND ReceiptDate>=@StartTimeYesterday AND ReceiptDate<=@EndTimeYesterday ;

                                        SELECT @Cnt AS Cut, @CntYesterday AS CntYesterday, @PaySuccess AS  PaySuccess,@PaySuccessYesterday AS PaySuccessYesterday,@PayWaiting AS PayWaiting ,@PayWaitingYesterday AS PayWaitingYesterday
                                        ";
                    var data = conn.QueryFirstOrDefault(sqlStr, new
                    {

                        StartTime = startTime,
                        EndTime = endTime,
                        BussId = businessId,
                        StartTimeYesterday = startTimeYesterday,
                        EndTimeYesterday = endTimeYesterday,
                    });
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "首页统计", data).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "首页统计错误,请稍后重试").ToJson();
            }
        }

        #region 

        /// <summary>
        /// 插入商品
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String InsertResources(DataRow row)
        {
            List<InsertAttributeModel> insertAttributeModels = null;
            if (!row.Table.Columns.Contains("RequestModel") || string.IsNullOrEmpty(row["RequestModel"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (row.Table.Columns.Contains("AttributeModel") && !String.IsNullOrEmpty(row["AttributeModel"].ToString()))
            {
                insertAttributeModels = JsonConvert.DeserializeObject<List<InsertAttributeModel>>(row["AttributeModel"].ToString());
            }
            decimal expressage = 0M;
            if (row.Table.Columns.Contains("Expressage") && !String.IsNullOrEmpty(row["Expressage"].ToString()))
            {
                decimal.TryParse(row["Expressage"].ToString(), out expressage);
            }

            try
            {
                Model.Model.Buss.Tb_Resources_Details tb_Resources_Details =
                JsonConvert.DeserializeObject<Model.Model.Buss.Tb_Resources_Details>(row["RequestModel"].ToString());
                if (tb_Resources_Details == null)
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
                }
                tb_Resources_Details.ResourcesID = Guid.NewGuid().ToString();
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    conn.Open();
                    var trans = conn.BeginTransaction();
                    if (String.IsNullOrEmpty(tb_Resources_Details.IsStopRelease)) tb_Resources_Details.IsStopRelease = "否";
                    tb_Resources_Details.CreateDate = DateTime.Now;
                    tb_Resources_Details.IsRelease = "否";
                    tb_Resources_Details.IsBp = "否";
                    tb_Resources_Details.IsDelete = 0;
                    tb_Resources_Details.IsSupportCoupon = "0";

                    var count = conn.Insert<Model.Model.Buss.Tb_Resources_Details>(tb_Resources_Details, trans);
                    String updateStr = "UPDATE Tb_Resources_Details SET  Expressage=@Expressage WHERE  ResourcesID=@ResourcesID";
                    conn.Execute(updateStr, new { Expressage = expressage, ResourcesID = tb_Resources_Details.ResourcesID }, trans);

                    StringBuilder insertProperty = new StringBuilder();
                    StringBuilder insertSpecification = new StringBuilder();
                    int insertPropertyCount = 0;
                    int insertSpecificationCount = 0;
                    if (insertAttributeModels != null)
                    {
                        foreach (var model in insertAttributeModels)
                        {
                            insertProperty.AppendFormat(@"INSERT Tb_Resources_PropertyRelation (Id,BussId,ResourcesID,PropertyId) VALUES('{0}',{1},'{2}','{3}');",
                                Guid.NewGuid().ToString(), tb_Resources_Details.BussId, tb_Resources_Details.ResourcesID, model.PropertyId);

                            insertPropertyCount++;
                            foreach (var value in model.Specification)
                            {
                                if (value.FreightID != null)
                                {
                                    insertSpecification.AppendFormat
                                        (@"INSERT  Tb_ResourcesSpecificationsPrice (Id,BussId,ResourcesID,PropertyId,SpecId,Price,FreightId,Inventory,DiscountAmount,GroupBuyingPrice) 
                                    VALUES ('{0}',{1},'{2}','{3}','{4}',{5},'{6}',{7},{8},{9});", Guid.NewGuid().ToString(), tb_Resources_Details.BussId, tb_Resources_Details.ResourcesID, model.PropertyId, value.SpecId, value.Price, value.FreightID, value.Inventory, value.DiscountAmount, value.GroupBuyingPrice);
                                }
                                else
                                {
                                    insertSpecification.AppendFormat(@"INSERT  Tb_ResourcesSpecificationsPrice (Id,BussId,ResourcesID,PropertyId,SpecId,Price,Inventory,DiscountAmount,GroupBuyingPrice) 
                                    VALUES ('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8});", Guid.NewGuid().ToString(), tb_Resources_Details.BussId, tb_Resources_Details.ResourcesID, model.PropertyId, value.SpecId, value.Price, value.Inventory, value.DiscountAmount, value.GroupBuyingPrice);
                                }

                                insertSpecificationCount++;
                            }
                        }
                    }

                    int propertyCount = 0, specificationCount = 0;
                    if (!String.IsNullOrEmpty(insertProperty.ToString()) && !String.IsNullOrEmpty(insertProperty.ToString()))
                    {
                        propertyCount = conn.Execute(insertProperty.ToString(), null, trans);
                        specificationCount = conn.Execute(insertSpecification.ToString(), null, trans);
                    }

                    if (!String.IsNullOrEmpty(count) && propertyCount == insertPropertyCount && specificationCount == insertSpecificationCount)
                    {
                        RefreshMoney(tb_Resources_Details.ResourcesID, conn, trans);
                        trans.Commit();
                        return new ResponseEntity<String>((int)ResoponseEnum.Success, "插入商品成功", tb_Resources_Details.ResourcesID).ToJson();
                    }
                    trans.Rollback();
                    return new ResponseEntity<String>((int)ResoponseEnum.Failure, "插入商品失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "插入商品错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String UpdateResources(DataRow row)
        {
            if (!row.Table.Columns.Contains("RequestModel") || string.IsNullOrEmpty(row["RequestModel"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            try
            {
                var model = JsonConvert.DeserializeObject<UpdateResourcesModel>(row["RequestModel"].ToString());
                if (model == null || String.IsNullOrEmpty(model.ResourcesID))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
                }
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    conn.Open();
                    var trans = conn.BeginTransaction();
                    String sql = @"SELECT  BussId  FROM Tb_Resources_Details  where  ISNULL(ISDELETE,0)=0 AND ResourcesID=@ResourcesID";
                    var bussId = conn.QueryFirstOrDefault<String>(sql, new { ResourcesID = model.ResourcesID }, trans);
                    if (String.IsNullOrEmpty(bussId)) return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "商品不存在").ToJson();

                    sql = @"  UPDATE Tb_Resources_Details SET  
                      ResourcesName =@ResourcesName,Img=@Img,ResourcesTypeId=@ResourcesTypeId,ResourcesCount=@ResourcesCount,IsRelease=@IsRelease,IsStopRelease=@IsStopRelease,FreightID=@FreightID,ReleaseAdContent=@ReleaseAdContent,ResourcesSalePrice=@ResourcesSalePrice,ResourcesDisCountPrice=@ResourcesDisCountPrice,Expressage=@Expressage,IsGroupBuy=@IsGroupBuy,GroupBuyStartDate=@GroupBuyStartDate,GroupBuyEndDate=@GroupBuyEndDate
                      WHERE ResourcesID=@ResourcesID    ";
                    conn.Execute(sql, new
                    {
                        ResourcesName = model.ResourcesName,
                        Img = model.Img,
                        ResourcesTypeId = model.ResourcesTypeId,
                        ResourcesCount = model.ResourcesCount,
                        ReleaseAdContent = model.ReleaseAdContent,
                        ResourcesSalePrice = model.ResourcesSalePrice,
                        ResourcesDisCountPrice = model.ResourcesDisCountPrice,
                        Expressage = model.Expressage,
                        ResourcesID = model.ResourcesID,
                        IsStopRelease = model.IsStopRelease,
                        FreightID = model.FreightID,
                        IsGroupBuy = model.IsGroupBuy,
                        GroupBuyStartDate = model.GroupBuyStartDate,
                        GroupBuyEndDate = model.GroupBuyEndDate,
                        IsRelease = "否",
                    }, trans);

                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat(@"DELETE FROM  Tb_ResourcesSpecificationsPrice WHERE ResourcesID=@ResourcesID;");
                    sb.AppendFormat(@"DELETE FROM  Tb_Resources_PropertyRelation WHERE [ResourcesID] = @ResourcesID;");


                    var propertyIds = model.SpeInfoModels.GroupBy(l => l.PropertyId).ToList();
                    foreach (var propertyId in propertyIds)
                    {
                        sb.AppendFormat(@"  INSERT Tb_Resources_PropertyRelation (Id,BussId,ResourcesID,PropertyId) VALUES  ('{0}','{1}','{2}','{3}');  ", Guid.NewGuid().ToString(), bussId, model.ResourcesID, propertyId.Key);
                    }

                    StringBuilder addResourcesStore = new StringBuilder();

                    foreach (var value in model.SpeInfoModels)
                    {
                        if (value.FreightID != null)
                            sb.AppendFormat(@" INSERT Tb_ResourcesSpecificationsPrice(Id,BussId,ResourcesID,PropertyId,SpecId,Price,FreightID,Inventory,DiscountAmount,GroupBuyingPrice) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');  "
                            , Guid.NewGuid().ToString(), bussId, model.ResourcesID, value.PropertyId, value.SpecId, value.Price, value.FreightID, value.Inventory, value.DiscountAmount, value.GroupBuyingPrice);
                        else
                            sb.AppendFormat(@" INSERT Tb_ResourcesSpecificationsPrice(Id,BussId,ResourcesID,PropertyId,SpecId,Price,Inventory,DiscountAmount,GroupBuyingPrice) VALUES ('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8});  "
                                , Guid.NewGuid().ToString(), bussId, model.ResourcesID, value.PropertyId, value.SpecId, value.Price, value.Inventory, value.DiscountAmount, value.GroupBuyingPrice);

                        if (value.AddInventory)
                        {
                            addResourcesStore.Append($"INSERT Tb_Resources_Store VALUES(NEWID(),{bussId},@ResourcesID,{value.AddInventoryValue},GETDATE(),'{value.SpecId}','{value.PropertyId}');");
                        }
                    }
                    if (!String.IsNullOrEmpty(addResourcesStore.ToString()))
                    {
                        conn.Execute(addResourcesStore.ToString(), new { ResourcesID = model.ResourcesID }, trans);
                    }
                    conn.Execute(sb.ToString(), new { ResourcesID = model.ResourcesID }, trans);
                    RefreshMoney(model.ResourcesID, conn, trans);
                    trans.Commit();
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "修改商品成功").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "修改商品错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String ResourcesDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String resourcesID = row["ResourcesID"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    string Sql = @"SELECT A.*,B.ResourcesTypeName as SecondTypeName,C.ResourcesTypeName AS FirstTypeName,A.FreightID,D.FreightName FROM 
                                (SELECT * FROM Tb_Resources_Details WHERE ResourcesID = @Id) as A
                                LEFT JOIN(SELECT* FROM Tb_Resources_Type) AS B ON A.ResourcesTypeID = B.ResourcesTypeID
                                LEFT JOIN(SELECT* FROM Tb_Resources_Type) AS C ON B.ParentID = C.ResourcesTypeID
                                LEFT JOIN   ( SELECT * FROM Tb_Resources_Freight where  ISNULL (IsDelete,0)=0 ) AS D ON D.ID=A.FreightID ; ";
                    var data = conn.QueryFirstOrDefault(Sql, new { Id = resourcesID });
                    Sql = @"SELECT A.PropertyId ,B.PropertyName,A.SpecId,C.SpecName, A.Price,A.FreightID,D.FreightName,A.DiscountAmount,A.GroupBuyingPrice,A.Inventory   FROM 
                            (
                            SELECT * FROM Tb_ResourcesSpecificationsPrice WHERE ResourcesID=@ResourcesID
                            )AS A
                            LEFT JOIN (
                             SELECT * FROM Tb_Resources_Property
                             ) AS B ON A.PROPERTYID=B.ID
                             LEFT JOIN 
                             (
                             SELECT * FROM Tb_Resources_Specifications
                             ) AS C ON C.ID=A.SpecId
                             LEFT JOIN   (  SELECT * FROM Tb_Resources_Freight where  ISNULL (IsDelete,0)=0 ) AS D ON D.ID=A.FreightID
                             ORDER by  A.PropertyId,A.SpecId";
                    var datas = conn.Query<ResourcesSpecificationsModel>(Sql, new { ResourcesID = resourcesID });
                    object specifications = null;
                    if (datas != null && datas.Any())
                    {
                        specifications = datas.GroupBy(l => l.PropertyId).Select(l =>
                            (
                                 new
                                 {
                                     PropertyId = l.Key,
                                     PropertyName = l.FirstOrDefault().PropertyName,
                                     SpecInfoModel = l.Select(p => new
                                     {
                                         SpecId = p.SpecId,
                                         SpecName = p.SpecName,
                                         Price = p.Price,
                                         FreightID = p.FreightID,
                                         FreightName = p.FreightName,
                                         DiscountAmount = p.DiscountAmount,
                                         GroupBuyingPrice = p.GroupBuyingPrice,
                                         Inventory = p.Inventory
                                     })
                                 }
                            ));
                    }

                    if (null != data) return new ResponseEntity<Object>((int)ResoponseEnum.Success, "获取商品属性列表成功",
                        new
                        {
                            ResourcesDetails = data,
                            ResourcesSpecifications = specifications
                        }).ToJson();
                    return new ResponseEntity<String>((int)ResoponseEnum.Failure, "获取商品属性列表失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                GetLog().Info(ex);
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "商品属性列表错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 插入属性
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String SaveResourcesPropertyRelation(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("ResourcesID") || string.IsNullOrEmpty(row["ResourcesID"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("PropertyId") || string.IsNullOrEmpty(row["PropertyId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String businessId = row["BusinessId"].ToString();
            String resourcesID = row["ResourcesID"].ToString();
            String propertyId = row["PropertyId"].ToString();

            var result = SaveResourcesPropertyRelation(businessId, resourcesID, propertyId);
            return Result(result);
        }

        /// <summary>
        /// 增加商品属性
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="resourcesID"></param>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        private int SaveResourcesPropertyRelation(String businessId, String resourcesID, String propertyId)
        {
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    var trans = conn.BeginTransaction();

                    String insertSql = @"INSERT Tb_Resources_PropertyRelation (Id,BussId,ResourcesID,PropertyId) VALUES(@Id,@BussId,@ResourcesID,@PropertyId)";
                    var insertCount = conn.Execute(insertSql, new { Id = Guid.NewGuid().ToString(), BussId = businessId, ResourcesID = resourcesID, PropertyId = propertyId }, trans);
                    // 插入规格
                    String sql = @"SELECT Id FROM Tb_Resources_Specifications WHERE BussId=@BussId AND PropertyId=@PropertyId;";
                    var specs = conn.Query<string>(sql, new { BussId = businessId, PropertyId = propertyId });
                    int count = 0;
                    foreach (var specId in specs)
                    {
                        count = count + conn.Insert(new Tb_ResourcesSpecificationsPrice()
                        {
                            Id = Guid.NewGuid().ToString(),
                            ResourcesID = resourcesID,
                            PropertyId = propertyId,
                            SpecId = specId,
                            BussId = int.Parse(businessId),
                            Price = 0
                        }, trans);
                    }
                    if (count == specs.Count() && insertCount > 0)
                    {
                        trans.Commit();
                        return (int)ResoponseEnum.Success;
                    }
                    trans.Rollback();
                    return (int)ResoponseEnum.Failure;
                }
            }
            catch (Exception ex)
            {
                return (int)ResoponseEnum.Error;
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 商品属性列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String ResourcesPropertyList(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @"SELECT * FROM Tb_Resources_Property WHERE ISNULL(ISDELETE,0)=0 AND BussId=@BussId";
                    var data = conn.Query(sql, new { BussId = businessId });
                    if (null != data) return new ResponseEntity<Object>((int)ResoponseEnum.Success, "获取商品属性列表成功", data).ToJson();
                    return new ResponseEntity<String>((int)ResoponseEnum.Failure, "获取商品属性列表失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "商品属性列表错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 插入商品属性列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String InsertResourcesProperty(DataRow row)
        {
            String remarks = String.Empty;
            int sort = 0;
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String businessId = row["BusinessId"].ToString();
            if (!row.Table.Columns.Contains("PropertyName") || string.IsNullOrEmpty(row["PropertyName"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String propertyName = row["PropertyName"].ToString();
            if (row.Table.Columns.Contains("Remarks") && !String.IsNullOrEmpty(row["Remarks"].AsString()))
            {
                remarks = row["Remarks"].ToString();
            }
            if (row.Table.Columns.Contains("Sort") && !String.IsNullOrEmpty(row["Sort"].AsString()))
            {
                int.TryParse(row["Sort"].ToString(), out sort);
            }
            var result = InsertResourcesProperty(businessId, propertyName, remarks, sort);
            return Result(result);
        }

        /// <summary>
        /// 插入处理
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="propertyName"></param>
        /// <param name="remarks"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        private int InsertResourcesProperty(String businessId, String propertyName, String remarks = "", int sort = 0)
        {
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @"INSERT Tb_Resources_Property (Id,BussId,PropertyName,Remarks,Sort) VALUES (@Id,@BussId,@PropertyName,@Remarks,@Sort);";
                    var count = conn.Execute(sql, new
                    {
                        Id = Guid.NewGuid().ToString(),
                        BussId = businessId,
                        PropertyName = propertyName,
                        Remarks = remarks,
                        Sort = sort
                    });

                    if (count > 0) return (int)ResoponseEnum.Success;
                    return (int)ResoponseEnum.Failure;
                }
            }
            catch (Exception ex)
            {
                return (int)ResoponseEnum.Error;
            }
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <returns></returns>
        private String DeleteResourcesProperty(DataRow row)
        {
            String propertyId = String.Empty;
            if (!row.Table.Columns.Contains("PropertyId") || string.IsNullOrEmpty(row["PropertyId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            propertyId = row["PropertyId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @"select COUNT(*) as Number from Tb_ResourcesSpecificationsPrice where PropertyId=@PropertyId";

                    var number = conn.QueryFirstOrDefault<int>(sql, new { PropertyId = propertyId });
                    if (number > 0)
                    {
                        return new ResponseEntity<object>((int)ResoponseEnum.Failure, "商品已使用该属性，删除失败").ToJson();
                    }

                    sql = @"UPDATE Tb_Resources_Property SET  ISDELETE=1 WHERE Id=@Id;";
                    var count = conn.Execute(sql, new
                    {
                        Id = propertyId,
                    });

                    if (count > 0) return new ResponseEntity<object>((int)ResoponseEnum.Success, "删除属性成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "删除属性失败,请稍后重试").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "删除属性错误,请稍后重试").ToJson();
            }
        }


        #endregion

        #region 商品属性中规格

        /// <summary>
        /// 商品属性中规格列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String ResourcesSpecificationsList(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("PropertyId") || string.IsNullOrEmpty(row["PropertyId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String propertyId = row["PropertyId"].ToString();
            String businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @"SELECT * FROM View_Tb_Resources_Specifications WHERE  BussId=@BussId AND PropertyId=@Id ORDER BY Sort ASC";
                    var data = conn.Query(sql, new { BussId = businessId, Id = propertyId });
                    if (null != data) return new ResponseEntity<Object>((int)ResoponseEnum.Success, "获取商品属性列表成功", data).ToJson();
                    return new ResponseEntity<String>((int)ResoponseEnum.Failure, "获取商品属性规格列表失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "商品属性规格列表错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 插入商品属性中规格列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String InsertResourcesSpecifications(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("PropertyId") || string.IsNullOrEmpty(row["PropertyId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("SpecName") || string.IsNullOrEmpty(row["SpecName"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            int sort = 1;
            if (row.Table.Columns.Contains("Sort") && !String.IsNullOrEmpty(row["Sort"].AsString()))
            {
                int.TryParse(row["Sort"].ToString(), out sort);
            }
            String propertyId = row["PropertyId"].ToString();
            String businessId = row["BusinessId"].ToString();
            String specName = row["SpecName"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @"INSERT Tb_Resources_Specifications ( Id,BussId,PropertyId,SpecName,Sort ) VALUES (@Id,@BussId,@PropertyId,@SpecName,@Sort)";
                    var count = conn.Execute(sql, new { Id = Guid.NewGuid().ToString(), BussId = businessId, PropertyId = propertyId, SpecName = specName, Sort = sort });
                    if (count > 0) return new ResponseEntity<String>((int)ResoponseEnum.Success, "插入商品属性列表成功").ToJson();
                    return new ResponseEntity<String>((int)ResoponseEnum.Failure, "插入商品属性规格列表失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "插入商品属性规格列表错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 删除规格
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String DeleteResourcesSpecifications(DataRow row)
        {
            String id = String.Empty;
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            id = row["Id"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @"DELETE FROM Tb_Resources_Specifications   WHERE Id=@Id;";
                    var count = conn.Execute(sql, new
                    {
                        Id = id,
                    });

                    if (count > 0) return new ResponseEntity<String>((int)ResoponseEnum.Success, "删除商品属性列表成功").ToJson();
                    return new ResponseEntity<String>((int)ResoponseEnum.Failure, "删除商品属性列表失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "删除商品属性列表错误,请稍后重试").ToJson();
            }
        }

        #endregion
        /// <summary>
        /// 获取商品类型列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String QueryResourcesFirstTypeList(DataRow row)
        {
            String businessId = String.Empty;
            if (!row.Table.Columns.Contains("BusinessId") || String.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @"SELECT A.ResourcesTypeID,A.ResourcesSortCode,A.ResourcesTypeName ,ISNULL( B.Number ,0) as Number FROM
                            (SELECT * FROM Tb_Resources_Type where ParentID IS NULL ) AS A
                            LEFT JOIN 
                            (
                            SELECT B.ParentID,count(*) as Number FROM
                            (SELECT * FROM Tb_Resources_Details where ISNULL(IsDelete ,0) =0  AND BussId=@businessId) AS A
                            LEFT JOIN (SELECT * FROM Tb_Resources_Type) AS B ON A.ResourcesTypeID = B.ResourcesTypeID 
                            GROUP BY b.ParentID
                            ) AS B  ON A.ResourcesTypeID=B.ParentID ";

                    var data = conn.Query(sql, new { businessId = businessId });
                    if (null != data) return new ResponseEntity<Object>((int)ResoponseEnum.Success, "获取商品类型列表成功", data).ToJson();
                    return new ResponseEntity<String>((int)ResoponseEnum.Failure, "获取商品类型列表失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "获取商品类型列表错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 获取商品类型列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String QueryResourcesTypeList(DataRow row)
        {
            String resourcesTypeName = String.Empty, parentID = String.Empty;
            if (row.Table.Columns.Contains("ParentID") && !String.IsNullOrEmpty(row["ParentID"].AsString()))
            {
                parentID = row["ParentID"].ToString();
            }
            if (row.Table.Columns.Contains("ResourcesTypeName") && !String.IsNullOrEmpty(row["ResourcesTypeName"].AsString()))
            {
                resourcesTypeName = row["ResourcesTypeName"].ToString();
            }
            try
            {
                String sqlFilter = String.Empty;
                if (!string.IsNullOrEmpty(resourcesTypeName))
                {
                    resourcesTypeName = resourcesTypeName.Trim();
                    sqlFilter += $" AND a.ResourcesTypeName LIKE '%{resourcesTypeName}%'";
                }
                if (!String.IsNullOrEmpty(parentID))
                {
                    sqlFilter += $" AND ParentID = '{parentID}'";
                }
                else
                {
                    sqlFilter += $" AND ParentID IS NULL ";
                }

                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @"SELECT ResourcesTypeID,ResourcesSortCode,ResourcesTypeName,ParentID,(SELECT resourcesTypeName FROM Tb_Resources_Type b 
                                    WHERE b.resourcesTypeID=a.ParentID) AS ParentName
                        FROM Tb_Resources_Type a WHERE 1=1  ";
                    //AND isnull(a.ParentID,'')<> ''
                    var data = conn.Query(sql + sqlFilter);
                    if (null != data) return new ResponseEntity<Object>((int)ResoponseEnum.Success, "获取商品类型列表成功", data).ToJson();
                    return new ResponseEntity<String>((int)ResoponseEnum.Failure, "获取商品类型列表失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "获取商品类型列表错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 根据分类获取分类的商品
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String QueryResourcesByType(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String businessId = row["BusinessId"].ToString();

            if (!row.Table.Columns.Contains("ResourcesTypeID") || string.IsNullOrEmpty(row["ResourcesTypeID"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String resourcesTypeID = row["ResourcesTypeID"].ToString();

            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @" SELECT * FROM Tb_Resources_Details WHERE ResourcesTypeID IN 
                     (
                      SELECT ResourcesTypeID FROM Tb_Resources_Type  WHERE ResourcesTypeID =@ResourcesTypeID
                     UNION 
                     SELECT ResourcesTypeID FROM Tb_Resources_Type  WHERE ParentID = @ResourcesTypeID
                     )
                     AND  BussId =@BusinessId  
                     AND isnull(IsDelete,0)=0 ";

                    var data = conn.Query(sqlStr, new { ResourcesTypeID = resourcesTypeID, BusinessId = businessId });
                    if (null != data) return new ResponseEntity<Object>((int)ResoponseEnum.Success, "根据分类获取分类的商品成功", data).ToJson();
                    return new ResponseEntity<Object>((int)ResoponseEnum.Failure, "根据分类获取分类的商品失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<Object>((int)ResoponseEnum.Error, "根据分类获取分类的商品错误").ToJson();
            }
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String OrderList(DataRow row)
        {
            DateTime startTime = DateTime.Now.Date, endTime = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (row.Table.Columns.Contains("StartTime") && !string.IsNullOrEmpty(row["StartTime"].AsString()))
            {
                DateTime.TryParse(row["StartTime"].ToString(), out startTime);
            }
            if (row.Table.Columns.Contains("EndTime") && !string.IsNullOrEmpty(row["EndTime"].AsString()))
            {
                DateTime.TryParse(row["EndTime"].ToString(), out endTime);
            }
            int type = 0;
            if (row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(row["Type"].AsString()))
            {
                int.TryParse(row["Type"].ToString(), out type);
            }
            String businessId = row["BusinessId"].ToString();
            var pageEntity = GetParamEntity(row);
            try
            {
                String sqlFilter = String.Empty;
                switch (type)
                {
                    case 1:
                        sqlFilter = "";
                        break;
                    //未发货 待处理
                    case 2:
                        sqlFilter = " AND IsDeliver ='未发货'  AND IsReceive='未收货' and EvaluateContent is NULL ";
                        break;
                    //已处理
                    case 3:
                        sqlFilter = " AND IsDeliver <>'未发货' AND IsReceive='未收货' and EvaluateContent is NULL  ";
                        break;
                    //已收货 待评价
                    case 4:
                        sqlFilter = " AND IsReceive='已收货' and EvaluateContent is NULL  ";
                        break;
                    //已收货 已评价
                    case 5:
                        sqlFilter = " AND  IsReceive='已收货' and EvaluateContent is NOT NULL ";
                        break;
                    //待退款
                    case 6:
                        sqlFilter = " AND  IsRetreat='是' and  ISNULL(IsAgree,'是' )='是' AND  ISNULL(IsPlatformRefund  ,'否') ='否' ";
                        break;
                    //已退款 
                    case 7:
                        sqlFilter = " AND  IsPlatformRefund='是' and PlatformRefundTime is NOT NULL ";
                        break;
                    //已拒绝
                    case 8:
                        sqlFilter = " AND  IsAgree='否' ";
                        break;
                }
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = $@"SELECT * FROM 
                                (
                                SELECT ROW_NUMBER() OVER(ORDER BY ReceiptDate DESC) AS ROWIDS ,* FROM
                                (
                                SELECT row_number() over(partition by A.ID order by ReceiptSign) as rownum ,A.*,c.EvaluateContent FROM 
                                ( SELECT * FROM Tb_Charge_Receipt  where isnull(IsDelete,0)=0)  AS A
                                LEFT JOIN (SELECT * FROM Tb_Charge_ReceiptDetail ) AS B   ON A.Id=B.ReceiptCode
                                LEFT JOIN (SELECT * FROM Tb_Resources_CommodityEvaluation ) AS C ON B.RpdCode=C.RpdCode
                                ) AS z
                                where rownum = 1 AND  BussId=@BussId AND ReceiptDate>=@StartTime AND ReceiptDate<=@EndTime   {sqlFilter}
                                ) AS T WHERE T.ROWIDS BETWEEN @StartPage AND @Endpage";

                    var data = conn.Query(sql, new { BussId = businessId, StartTime = startTime, EndTime = endTime, StartPage = pageEntity.GetStart(), Endpage = pageEntity.GetEnd() });
                    if (null != data)
                    {
                        var dataInfo = data.Where(l => ((decimal)l.RealAmount) == 0 && l.IsPay == "未付款");
                        if (dataInfo != null && dataInfo.Any())
                        {
                            String moneyStr = @"SELECT Price,A.ResourcesID,B.ResourcesDisCountPrice,A.Quantity,A.Receiptcode FROM 
                                            (select Quantity,ShoppingId,ResourcesID,Receiptcode from Tb_Charge_ReceiptDetail where Receiptcode in @Receiptcode) AS A
                                            LEFT JOIN (SELECT  ResourcesDisCountPrice,ResourcesID  FROM  Tb_Resources_Details) AS B ON A.ResourcesID=B.ResourcesID
                                            LEFT JOIN (SELECT  ShoppingId,SpecId  FROM  Tb_ShoppingDetailed) AS C ON C.ShoppingId=A.ShoppingId
                                            LEFT JOIN (SELECT Price,ResourcesID,SpecId FROM  Tb_ResourcesSpecificationsPrice) AS D ON D.ResourcesID=B.ResourcesID AND D.SpecId=C.SpecId
                                            WHERE D.Price IS NOT NULL";
                            var moneyData = conn.Query(moneyStr, new { Receiptcode = dataInfo.Select(l => l.Id) });
                            foreach (var model in data)
                            {
                                if ((decimal)model.RealAmount == 0 && model.IsPay == "未付款")
                                {
                                    var resourcesPrices = moneyData.Where(l => l.Receiptcode == model.Id);
                                    decimal price = 0;
                                    if (resourcesPrices != null && resourcesPrices.Any())
                                    {
                                        foreach (var resourceInfo in resourcesPrices)
                                        {
                                            price += (decimal)resourceInfo.Quantity * ((decimal)resourceInfo.Price - (decimal)resourceInfo.ResourcesDisCountPrice);
                                        }
                                    }
                                    if (price <= 0) price = 0;
                                    model.RealAmount = Math.Round(price, 2).ToString();
                                }
                            }
                        }
                        return new ResponseEntity<object>((int)ResoponseEnum.Success, "获取订单列表规格列表成功", data).ToJson();
                    }
                }
                return new ResponseEntity<object>((int)ResoponseEnum.Failure, "获取订单列表规格列表失败,请稍后重试").ToJson();
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "获取订单列表规格列表错误,请稍后重试").ToJson();
            }
        }

        #region  图片/视频上传

        /// <summary>
        /// 图片上传,限制图片格式为png，jpg
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String UploadFile(DataRow row)
        {
            List<String> fileUrls = new List<string>();
            HttpFileCollection files = httpContext.Request.Files;
            if (httpContext.Request.Files.Count > 9)
                return new ResponseEntity<Object>((int)ResoponseEnum.Error, "文件已到上限").ToJson();
            for (int i = 0; i < httpContext.Request.Files.Count; i++)
            {
                try
                {
                    if (null == httpContext)
                    {
                        return new ResponseEntity<Object>((int)ResoponseEnum.Error, "请求异常").ToJson();
                    }

                    if (httpContext.Request.Files.Count == 0)
                    {
                        return new ResponseEntity<Object>((int)ResoponseEnum.Error, "请上传附件").ToJson();
                    }
                    HttpPostedFile file = files[i];
                    //使用正则表达式确实是否是合适的格式
                    string rxg = "[.](jpg|png|JPG|PNG)$";
                    Regex rgx = new Regex(rxg);
                    bool isMatch = rgx.IsMatch(file.FileName.ToLower());
                    //文件格式
                    var fileType = rgx.Match(file.FileName).Value;

                    if (httpContext.Request.Files.Count == 0)
                    {
                        return new ResponseEntity<Object>((int)ResoponseEnum.Failure, "暂不支持该格式").ToJson();
                    }

                    if (file.ContentLength > 10 * 1024 * 1024)
                    {
                        return new ResponseEntity<Object>((int)ResoponseEnum.Failure, "图片过大").ToJson();
                    }

                    string BusinessAppFileUploadPath = Global_Fun.AppWebSettings("BusinessAppFileUploadPath");
                    if (!Directory.Exists(BusinessAppFileUploadPath))
                    {
                        Directory.CreateDirectory(BusinessAppFileUploadPath);
                    }

                    //文件名规则(4位随机数字组合+文件类型+当前时间)
                    string fileName = GetRandomCode(4) + ("image") + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;
                    string filePath = BusinessAppFileUploadPath;
                    file.SaveAs(filePath + fileName);
                    string BusinessAppFileUploadUrlPrefix = Global_Fun.AppWebSettings("BusinessAppFileUploadUrlPrefix");
                    string fileUrl = BusinessAppFileUploadUrlPrefix + fileName;
                    fileUrls.Add(fileUrl);
                }
                catch (Exception ex)
                {
                    GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace);
                    return new ResponseEntity<Object>((int)ResoponseEnum.Failure, "上传图片失败").ToJson();
                }
            }
            GetLog().Info(fileUrls.FirstOrDefault());
            return new ResponseEntity<Object>((int)ResoponseEnum.Success, "上传成功", fileUrls).ToJson();
        }

        #endregion

        /// <summary>
        /// 今日营业额
        /// </summary>
        /// <returns></returns>
        public String TodayTurnoverSta(DataRow row)
        {
            DateTime startTime = DateTime.Now.Date, endTime = DateTime.Now.AddDays(1).AddMilliseconds(-1);
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (row.Table.Columns.Contains("StartTime") && !string.IsNullOrEmpty(row["StartTime"].AsString()))
            {
                DateTime.TryParse(row["StartTime"].ToString(), out startTime);
            }
            if (row.Table.Columns.Contains("EndTime") && !string.IsNullOrEmpty(row["EndTime"].AsString()))
            {
                DateTime.TryParse(row["EndTime"].ToString(), out endTime);
            }
            String businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = "SELECT SUM(RealAmount) AS [MONEY] FROM View_Tb_Charge_ReceiptNew  WHERE  IsPay='已付款' AND PayDate >= @startTime AND PayDate<= @endTime AND BussId= @BusinessId;";
                    var money = conn.QueryFirstOrDefault<decimal?>(sql, new { startTime = startTime, endTime = endTime, BusinessId = businessId });

                    sql = "SELECT  Method,PayDate,RealAmount ,Id  FROM View_Tb_Charge_ReceiptNew  WHERE  IsPay='已付款' AND PayDate >= @startTime AND PayDate<= @endTime AND BussId= @BusinessId;";
                    var data = conn.Query(sql, new { startTime = startTime, endTime = endTime, BusinessId = businessId });
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "营业额信息", new { Money = money, Data = data }).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Failure, "今日营业额失败,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 统计订单
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public String TodayTurnoverOrder(DataRow row)
        {
            DateTime startTime = DateTime.Now.Date, endTime = DateTime.Now.AddDays(1).AddMilliseconds(-1);
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (row.Table.Columns.Contains("StartTime") && !string.IsNullOrEmpty(row["StartTime"].AsString()))
            {
                DateTime.TryParse(row["StartTime"].ToString(), out startTime);
            }
            if (row.Table.Columns.Contains("EndTime") && !string.IsNullOrEmpty(row["EndTime"].AsString()))
            {
                DateTime.TryParse(row["EndTime"].ToString(), out endTime);
            }
            if (!row.Table.Columns.Contains("Type") || string.IsNullOrEmpty(row["Type"].AsString()) || !int.TryParse(row["Type"].ToString(), out var type))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }

            String sqlFiter = "";
            if (type == 1)
            { sqlFiter = "已付款"; }
            else { { sqlFiter = "未付款"; } }

            String businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = $"SELECT Count(*) AS [MONEY]FROM View_Tb_Charge_ReceiptNew  WHERE  IsPay=@IsPay AND RECEIPTDATE >= @startTime AND RECEIPTDATE<= @endTime AND BussId= @BusinessId;";
                    var money = conn.QueryFirstOrDefault<decimal?>(sql, new { startTime = startTime, endTime = endTime, BusinessId = businessId, IsPay = sqlFiter });

                    sql = $"SELECT  *  FROM View_Tb_Charge_ReceiptImg  WHERE  IsPay='{sqlFiter}' AND RECEIPTDATE >= @startTime AND RECEIPTDATE<= @endTime AND BussId= @BusinessId ORDER BY ReceiptDate DESC;";
                    var data = conn.Query(sql, new { startTime = startTime, endTime = endTime, BusinessId = businessId, IsPay = sqlFiter });
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "营业额信息", data).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Failure, "今日营业额失败,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 快递确认发货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public String Expressage(DataRow row)
        {
            if (!row.Table.Columns.Contains("RequestModel") || string.IsNullOrEmpty(row["RequestModel"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            try
            {
                ExpressageModel expressage = JsonConvert.DeserializeObject<ExpressageModel>(row["RequestModel"].ToString());
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "SELECT COUNT(*)  FROM Tb_Charge_Receipt where CourierNumber = @CourierNumber";
                    var num = conn.QueryFirstOrDefault<int>(sqlStr, new { CourierNumber = expressage.ExpressageNumber });
                    if (num > 0) return new ResponseEntity<object>((int)ResoponseEnum.Failure, "快递单号重复").ToJson();

                    String sql = @"  UPDATE Tb_Charge_Receipt SET  IsDeliver='已发货',ExpressName=@ExpressName,CourierNumber=@CourierNumber,TheSender=@TheSender ,TheSenderPhone= @TheSenderPhone,DeliveryTime=GETDATE() WHERE ID=@ID ";
                    int count = conn.Execute(sql, new { ExpressName = expressage.ExpressageCompany, CourierNumber = expressage.ExpressageNumber, TheSender = expressage.Sender, TheSenderPhone = expressage.Mobile, ID = expressage.OrderId });
                    if (count > 0)
                        return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 自提确认发货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public String SelfPickUp(DataRow row)
        {
            if (!row.Table.Columns.Contains("RequestModel") || string.IsNullOrEmpty(row["RequestModel"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            try
            {
                TakeTheirModel model = JsonConvert.DeserializeObject<TakeTheirModel>(row["RequestModel"].ToString());
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @" UPDATE Tb_Charge_Receipt SET  IsDeliver='已发货',Express=@Express,MerchantPickUpTime=@MerchantPickUpTime ,
                    MerchantPickUpLocation=@MerchantPickUpLocation,PickUpContactsPhone=@PickUpContactsPhone,PickUpRemarks=@PickUpRemarks,PickUpContacts=@PickUpContacts  WHERE ID=@ID ";
                    int count = conn.Execute(sql, new
                    {
                        Express = "",
                        //EstimatedPickUpTime = model.EstimatedPickUpTime,
                        MerchantPickUpTime = model.MerchantPickUpTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        MerchantPickUpLocation = model.Address,
                        PickUpContactsPhone = model.Mobile,
                        PickUpContacts = model.Man,
                        PickUpRemarks = model.Remark,
                        ID = model.OrderId,
                    });
                    if (count > 0)
                        return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 配送确认发货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public String Dispatching(DataRow row)
        {
            if (!row.Table.Columns.Contains("RequestModel") || string.IsNullOrEmpty(row["RequestModel"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            try
            {
                DispatchingModel dispatching = JsonConvert.DeserializeObject<DispatchingModel>(row["RequestModel"].ToString());
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @" UPDATE Tb_Charge_Receipt SET  IsDeliver='已发货',  IsSendOut  ='是',Express=@Express,DeliveredBy=@DeliveredBy ,DeliveredPhone=@DeliveredPhone,ExpectedDeliveryTime=@PredictDeliveryTime  WHERE ID=@ID  ";
                    int count = conn.Execute(sql, new
                    {
                        Express = "",
                        DeliveredBy = dispatching.Man,
                        DeliveredPhone = dispatching.Moblie,
                        PredictDeliveryTime = dispatching.PredictDeliveryTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        ID = dispatching.OrderId
                    });
                    if (count > 0)
                        return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 修改商家营业状态
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public String UpdateBusinessStatus(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }

            if (!row.Table.Columns.Contains("Type") || string.IsNullOrEmpty(row["Type"].AsString()) || !int.TryParse(row["Type"].ToString(), out var type))

            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @" UPDATE Tb_System_BusinessCorp SET  BusinessStatus  = @Type  WHERE BussId=@BussId  ";
                    var count = conn.Execute(sql, new { BussId = businessId, Type = type });
                    if (count > 0) return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 商家详情
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String BusinessDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @" SELECT A.*,B.BusinessTypeName AS FirstTypeName,c.BusinessTypeName as SecondTypeName FROM 
                        (SELECT * FROM Tb_System_BusinessCorp WHERE  ISNULL(IsDelete,0) =0 AND  IsClose='未关闭'  AND BussId=@BussId) AS A
                        LEFT JOIN (SELECT * FROM Tb_System_BusinessType) AS B ON A.RegBigType= B.BusinessTypeCode
                        LEFT JOIN (SELECT * FROM Tb_System_BusinessType) AS C ON A.RegSmallType= C.BusinessTypeCode
                         ";
                    var data = conn.QueryFirstOrDefault(sql, new { BussId = businessId });
                    if (null != data) return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", data).ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String OrderDetail(DataRow row)
        {
            if (!row.Table.Columns.Contains("Id") || string.IsNullOrEmpty(row["Id"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String orderId = row["Id"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "SELECT ShippingInfo ='', * FROM View_Tb_Charge_ReceiptImg WHERE Id=@Id";
                    var viewData = conn.QueryFirstOrDefault(sqlStr, new { Id = orderId });
                    if (null != viewData)
                    {
                        int dispatchingType = (int)viewData.DispatchingType;
                        String address = (String)viewData.DeliverAddress;
                        Object objectData = null;
                        switch (dispatchingType)
                        {
                            case 1:
                                if (viewData.ExpressName != null)
                                {
                                    objectData = new
                                    {
                                        CompanyName = viewData.ExpressName,
                                        Number = viewData.CourierNumber,
                                        Name = viewData.TheSender,
                                        Moblie = viewData.TheSenderPhone,
                                        Time = viewData.DeliveryTime,
                                    };
                                }
                                break;
                            case 2:
                                if (viewData.ExpectedDeliveryTime != null)
                                {
                                    DateTime.TryParse((String)viewData.ExpectedDeliveryTime, out var expectedDeliveryTime);
                                    objectData = new
                                    {
                                        Name = viewData.DeliveredBy,
                                        Moblie = viewData.DeliveredPhone,
                                        Time = expectedDeliveryTime,
                                    };
                                }
                                break;
                            case 4:
                                if (viewData.MerchantPickUpLocation != null)
                                {
                                    objectData = new
                                    {
                                        ShopName = viewData.BussName,
                                        Address = viewData.MerchantPickUpLocation,
                                        Name = viewData.PickUpContacts,
                                        Moblie = viewData.PickUpContactsPhone,
                                        Time = viewData.MerchantPickUpTime,
                                        Remark = viewData.PickUpRemarks,
                                    };
                                }
                                break;
                        }

                        viewData.ShippingInfo = objectData;
                        return new ResponseEntity<object>((int)ResoponseEnum.Success, "订单详情成功", new { Data = viewData, Resources = ResourcesInfo(orderId) }).ToJson();
                    }

                }
                return new ResponseEntity<object>((int)ResoponseEnum.Failure, "订单详情失败,请稍后重试").ToJson();
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "订单详情错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private Object ResourcesInfo(String orderId)
        {
            using (var conn = new SqlConnection(PubConstant.BusinessContionString))
            {
                var sql = "SELECT * FROM View_Tb_Charge_ReceiptDetail WHERE Id=@Id";
                var details = conn.Query<View_Tb_Charge_ReceiptDetailAdd>(sql, new { Id = orderId }).ToList();
                sql = @"SELECT  a.ShoppingId,C.PropertyName AS PropertyName,D.SpecName,E.Price AS SpecPrice,e.GroupBuyingPrice,e.DiscountAmount,A.UnitPrice  FROM  Tb_Charge_ReceiptDetail AS A 
                        INNER JOIN Tb_ShoppingDetailed AS  B ON A.ShoppingId = B.ShoppingId
                        INNER JOIN Tb_Resources_Property AS C ON B.PropertysId=C.Id
                        INNER JOIN Tb_Resources_Specifications AS D ON D.Id= B.SpecId
                        INNER JOIN Tb_ResourcesSpecificationsPrice AS E ON E.SpecId=B.SpecId AND E.PropertyId=B.PropertysId AND E.ResourcesID=A.ResourcesID
                        WHERE ReceiptCode=@ReceiptID;

                        SELECT isnull(IsPay,'未付款') FROM Tb_Charge_Receipt WHERE Id=@ReceiptID;";
                foreach (var item in details)
                {
                    var reader = conn.QueryMultiple(sql, new { ReceiptID = orderId });
                    var tmp = reader.Read().FirstOrDefault();
                    if (tmp != null)
                    {
                        item.ShoppingId = tmp.ShoppingId;
                        item.PropertyName = tmp.PropertyName;
                        item.SpecName = tmp.SpecName;
                        item.SpecPrice = tmp.SpecPrice;
                        item.DiscountAmount = tmp.DiscountAmount;
                        item.GroupBuyingPrice = tmp.GroupBuyingPrice;
                        item.UnitPrice = tmp.UnitPrice;
                    }

                    item.IsPay = reader.Read<string>().FirstOrDefault();
                    if (!String.IsNullOrEmpty(item.Img))
                    {
                        item.Img = item.Img.Split(',')[0];
                    }
                }
                return details;
            }
        }

        /// <summary>
        /// 物流商家
        /// </summary>
        /// <returns></returns>
        private String DHLList()
        {
            try
            {
                var dhlList = new List<String>();
                foreach (int v in Enum.GetValues(typeof(ShipperCode)))
                {
                    var name = EnumHelper.GetEnumDesc(typeof(ShipperCode), v);
                    dhlList.Add(name);
                }
                return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", dhlList).ToJson();
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 根据根据状态获取列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String OrderListByStatus(DataRow row)
        {
            if (!row.Table.Columns.Contains("Type") || string.IsNullOrEmpty(row["Type"].AsString()) || !int.TryParse(row["Type"].ToString(), out var type))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String businessId = row["BusinessId"].ToString();

            try
            {
                String isDeliver = String.Empty;
                if (type == 1) isDeliver = " AND  IsDeliver='未发货'  ";
                else
                    isDeliver = " AND  IsDeliver <> '未发货'  ";
                var pageEntity = GetParamEntity(row);

                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = $@"SELECT * FROM 
                            (
                            SELECT ROW_NUMBER() OVER( ORDER BY DispatchingType ASC,ReceiptDate DESC ) AS ROWID ,ObjectData='', * FROM Tb_Charge_Receipt  where  1=1  {isDeliver} AND Bussid=@Bussid 
                            ) AS  T  WHERE T.ROWID BETWEEN @StartTime AND @EndTime
                            ";
                    var viewData = conn.Query(sqlStr, new { Bussid = businessId, StartTime = pageEntity.GetStart(), EndTime = pageEntity.GetEnd() });
                    foreach (var model in viewData)
                    {
                        var expressNum = (String)model.ExpressNum;
                        if (!String.IsNullOrEmpty(expressNum))
                        {
                            var detail = JsonConvert.DeserializeObject<ExpressageDetailModel>(expressNum);
                            model.ObjectData = detail;
                        }
                    }
                    if (null != viewData)
                        return new ResponseEntity<object>((int)ResoponseEnum.Success, "订单列表成功", new { Data = viewData }).ToJson();
                }
                return new ResponseEntity<object>((int)ResoponseEnum.Failure, "订单列表失败,请稍后重试").ToJson();
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "订单详情错误,请稍后重试").ToJson();
            }
        }

        /// <summary>
        /// 店铺统计
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String BusinessAmountSta(DataRow row)
        {
            DateTime startTime = default(DateTime), endTime = default(DateTime);
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (row.Table.Columns.Contains("StartTime") && !string.IsNullOrEmpty(row["StartTime"].AsString()))
            {
                DateTime.TryParse(row["StartTime"].ToString(), out startTime);
            }
            if (row.Table.Columns.Contains("EndTime") && !string.IsNullOrEmpty(row["EndTime"].AsString()))
            {
                DateTime.TryParse(row["EndTime"].ToString(), out endTime);
            }
            String businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    var page = GetParamEntity(row);
                    object data = null;
                    String sqlMoney = @"SELECT SUM(RealAmount) FROM View_Tb_Charge_ReceiptNew WHERE  BussId =@BusinessId   AND IsPay='已付款'";
                    decimal money = conn.QueryFirstOrDefault<decimal>(sqlMoney, new { BusinessId = businessId });
                    if (startTime == default(DateTime) || endTime == default(DateTime))
                    {

                        String sqlStr = @"SELECT [Time],[Money] FROM 
                        (SELECT T.[Month] as [Time],SUM(RealAmount) AS [Money]  ,ROW_NUMBER() OVER(ORDER BY  T.[Month] DESC) AS RowId   FROM
                        (SELECT  CONVERT(varchar(7),PayDate,120) as [Month],* FROM View_Tb_Charge_ReceiptNew WHERE  BussId =@BusinessId AND IsPay='已付款') AS T
                        GROUP BY T.[Month]
                        ) AS C WHERE C.RowId BETWEEN @Start AND @End";
                        data = conn.Query(sqlStr, new { BusinessId = businessId, Start = page.GetStart(), End = page.GetEnd() });
                    }
                    else
                    {
                        String sqlStr = $@"SELECT [Time],[Money] FROM 
                        (SELECT T.[Day] as [Time],SUM(RealAmount) AS [Money]  ,ROW_NUMBER() OVER(ORDER BY  T.[Day] DESC) AS RowId   FROM
                        (SELECT  CONVERT(varchar(10),PayDate,120) as [Day],* FROM View_Tb_Charge_ReceiptNew WHERE  BussId = @BusinessId  AND IsPay='已付款' 
						and PayDate>=@Start AND PayDate<= @End  ) AS T
                        GROUP BY T.[Day]
                        ) AS C  WHERE C.RowId BETWEEN @StartPage AND @EndPage";
                        data = conn.Query(sqlStr, new { BusinessId = businessId, Start = startTime, End = endTime, StartPage = page.GetStart(), EndPage = page.GetEnd() });

                    }
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", new { Money = money, Data = data }).ToJson();
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// 商家类型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String BusinessType(DataRow row)
        {
            int type = 0;
            if (row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(row["Type"].AsString()))
            {
                int.TryParse(row["Type"].ToString(), out type);
            }
            String id = String.Empty;
            if (row.Table.Columns.Contains("BusinessTypeCode") && !string.IsNullOrEmpty(row["BusinessTypeCode"].AsString()))
            {
                id = row["BusinessTypeCode"].ToString();
            }
            String keyWords = String.Empty;
            if (row.Table.Columns.Contains("KeyWords") && !string.IsNullOrEmpty(row["KeyWords"].AsString()))
            {
                keyWords = row["KeyWords"].ToString();
            }

            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlParam = String.Empty;
                    String sqlKeyWords = String.Empty;
                    if (!String.IsNullOrEmpty(keyWords))
                    {
                        sqlKeyWords = $" AND BusinessTypeName LIKE   '%{keyWords}%' ";
                    }

                    String sql = String.Empty;
                    if (type == 0)
                    {
                        sqlParam = "大类";
                        sql = "SELECT * FROM Tb_System_BusinessType WHERE BusinessCategory=@BusinessCategory";
                    }
                    else
                    {
                        sqlParam = "小类";
                        if (String.IsNullOrEmpty(id))
                        {
                            return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
                        }
                        sql = $"SELECT * FROM Tb_System_BusinessType WHERE BusinessCategory=@BusinessCategory AND  BusinessTypeCode LIKE '{id}%'";
                    }

                    if (!String.IsNullOrEmpty(keyWords))
                    {
                        sql = String.Format("{0} {1} ", sql, sqlKeyWords);
                    }
                    var result = conn.Query<Tb_System_BusinessType>(sql, new { BusinessCategory = sqlParam }).ToList();
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", result).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }

        }

        /// <summary>
        /// 获取商家配送类型
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public String BussinessDispatchingType(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                //new ApiResult(false, "").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "缺少参数BusinessId").ToJson();
            }
            String bussId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "SELECT  [DispatchingType] FROM Tb_System_BusinessCorp WHERE BussId = @BussId; ";
                    var dispatchingType = conn.QueryFirstOrDefault<String>(sqlStr, new { BussId = bussId });
                    if (!String.IsNullOrEmpty(dispatchingType))
                    {
                        String[] result = dispatchingType.Split(',');

                        return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", result).ToJson();
                    }
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 核销码
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String ExtractionCode(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].AsString()))
            {
                //return new ApiResult(false, "").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "缺少参数OrderId").ToJson();
            }
            if (!row.Table.Columns.Contains("ExtractionCode") || string.IsNullOrEmpty(row["ExtractionCode"].AsString()))
            {
                //return new ApiResult(false, "").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "缺少参数ExtractionCode").ToJson();
            }
            String extractionCode = row["ExtractionCode"].ToString();
            String orderId = row["OrderId"].ToString();

            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String extractionCodeStr = @"select ExtractionCode from Tb_Charge_Receipt where  Id=@Id; ";
                    var extractionCodeData = conn.QueryFirstOrDefault<String>(extractionCodeStr, new { Id = orderId });

                    if (!String.IsNullOrEmpty(extractionCodeData) && extractionCodeData.ToLower() == extractionCode.ToLower())
                    {
                        String updateStr = @" UPDATE Tb_Charge_Receipt SET IsReceive='已收货',ConfirmReceivedTime =GETDATE()  WHERE Id= @Id ";
                        var count = conn.Execute(updateStr, new { Id = orderId });
                        if (count > 0) return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功").ToJson();
                    }
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 返回参数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public String Result(int code, Object data = null)
        {
            switch (code)
            {
                case (int)ResoponseEnum.Success:
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "操作成功", data).ToJson();
                case (int)ResoponseEnum.Failure:
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "操作失败", data).ToJson();
                case (int)ResoponseEnum.Error:
                default:
                    return new ResponseEntity<object>((int)ResoponseEnum.Error, "操作错误", data).ToJson();
            }
        }

        /// <summary>
        /// 获取物流信息
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String GetLogisticsIinformation(DataRow row)
        {
            if (!row.Table.Columns.Contains("ExpressName") || string.IsNullOrEmpty(row["ExpressName"].AsString()))
            {
                //return new ApiResult(false, "").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "缺少参数ExpressName").ToJson();
            }
            if (!row.Table.Columns.Contains("CourierNumber") || string.IsNullOrEmpty(row["CourierNumber"].AsString()))
            {
                //return new ApiResult(false, "CourierNumber").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "缺少参数CourierNumber").ToJson();
            }
            String expressName = row["ExpressName"].ToString();
            String courierNumber = row["CourierNumber"].ToString();

            try
            {
                var code = EnumHelper.GetEnumByDesc(typeof(ShipperCode), expressName);
                var appkey = Global_Fun.AppWebSettings("kdnAppKey");
                var appSecret = Global_Fun.AppWebSettings("kdnAppSecret");
                if (!KdApiSearchMonitor.getOrderTracesByJson(appkey, appSecret, (ShipperCode)Enum.Parse(typeof(ShipperCode), code), courierNumber, out string result))
                {
                    return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
                }
                return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", result).ToJson();
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 获取商家待审核修改列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String GetBusinessChangeInfo(DataRow row)
        {
            if (!row.Table.Columns.Contains("BusinessId") || string.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                //return new ApiResult(false, "缺少参数BusinessId").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "缺少参数BusinessId").ToJson();
            }
            String businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    var sql = @"SELECT * FROM Tb_System_ChangeRegister WHERE BusinessId =@BusinessId AND AuditStatus=1;";
                    var details = conn.QueryFirstOrDefault(sql, new { BusinessId = businessId });
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", details).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 同意退货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String SalesReturnAgree(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].ToString()))
            {
                //return new ApiResult(false, "参数错误").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("Address") || string.IsNullOrEmpty(row["Address"].ToString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("LinkMan") || string.IsNullOrEmpty(row["LinkMan"].ToString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("LinkMoblie") || string.IsNullOrEmpty(row["LinkMoblie"].ToString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String order = row["OrderId"].ToString();
            String address = row["Address"].ToString();
            String man = row["LinkMan"].ToString();
            String moblie = row["LinkMoblie"].ToString();
            try
            {
                String orderInfo = null;
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "select IsRetreat from Tb_Charge_Receipt where Id=@Id";
                    orderInfo = conn.QueryFirstOrDefault<String>(sqlStr, new { Id = order });
                    if (null == orderInfo)
                        //return new ApiResult(false, "").toJson();
                        return new ResponseEntity<object>((int)ResoponseEnum.Error, "订单信息不存在").ToJson();

                    if (String.IsNullOrEmpty(orderInfo) || orderInfo == "否")
                        //return new ApiResult(false, "").toJson();
                        return new ResponseEntity<object>((int)ResoponseEnum.Error, "当前订单并不是退款订单").ToJson();

                    sqlStr = @" UPDATE Tb_Charge_Receipt SET  RetreatAddress=@RetreatAddress,Recipient=@Recipient,RecipientTelephone=@RecipientTelephone,IsAgree='是',AgreeTime=getdate() WHERE Id=@Id ";
                    var count = conn.Execute(sqlStr, new { RetreatAddress = address, Recipient = man, RecipientTelephone = moblie, Id = order });

                    if (count > 0) return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex) { return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson(); }
        }

        /// <summary>
        /// 拒绝退货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String SalesReturnRefuse(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].ToString()))
            {
                //return new ApiResult(false, "").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("Reason") || string.IsNullOrEmpty(row["Reason"].ToString()))
            {
                //return new ApiResult(false, "参数错误").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "参数错误").ToJson();
            }

            String order = row["OrderId"].ToString();
            String reason = row["Reason"].ToString();
            try
            {
                String orderInfo = null;
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = "select IsRetreat from Tb_Charge_Receipt where Id=@Id";
                    orderInfo = conn.QueryFirstOrDefault<String>(sqlStr, new { Id = order });
                    if (null == orderInfo)
                        //return new ApiResult(false, "").toJson();
                        return new ResponseEntity<object>((int)ResoponseEnum.Error, "订单信息不存在").ToJson();


                    if (String.IsNullOrEmpty(orderInfo) || orderInfo == "否")
                        //return new ApiResult(false, "").toJson();
                        return new ResponseEntity<object>((int)ResoponseEnum.Error, "当前订单并不是退款订单").ToJson();

                    sqlStr = @" UPDATE Tb_Charge_Receipt SET  DisagreeReason=@DisagreeReason,IsAgree='否',AgreeTime=getdate() WHERE Id=@Id ";
                    var count = conn.Execute(sqlStr, new { DisagreeReason = reason, Id = order });

                    if (count > 0) return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex) { return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson(); }
        }



        /// <summary>
        /// 退货订单确认收货
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String SalesReturnReceiving(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].ToString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "参数错误").ToJson();
            }
            String order = row["OrderId"].ToString();
            try
            {
                dynamic orderInfo = null;
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    //RetreatCourierNumber
                    String sqlStr = "select * from Tb_Charge_Receipt where Id=@Id";
                    orderInfo = conn.QueryFirstOrDefault(sqlStr, new { Id = order });
                    if (null == orderInfo)
                        return new ResponseEntity<object>((int)ResoponseEnum.Error, "订单信息不存在").ToJson();


                    if (orderInfo.DispatchingType == 1 && String.IsNullOrEmpty(orderInfo.RetreatCourierNumber))
                        //return new ApiResult(false, "").toJson();
                        return new ResponseEntity<object>((int)ResoponseEnum.Error, "当前订单没有快递信息").ToJson();

                    sqlStr = @" UPDATE Tb_Charge_Receipt SET  MerchantReceivedTime=GETDATE(),IsMerchantReceived='是' WHERE Id=@Id ";
                    var count = conn.Execute(sqlStr, new { Id = order });

                    if (count > 0) return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功").ToJson();
                    return new ResponseEntity<object>((int)ResoponseEnum.Failure, "失败").ToJson();
                }
            }
            catch (Exception ex) { return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson(); }
        }

        /// <summary>
        /// 退货信息查询
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String ReturnInfoList(DataRow row)
        {
            if (!row.Table.Columns.Contains("OrderId") || string.IsNullOrEmpty(row["OrderId"].ToString()))
            {
                //return new ApiResult(false, "参数错误").toJson();
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String orderId = row["OrderId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @"SELECT [CustomerApplicationTime],[RetreatReason],[RetreatExplain],[RetreatImages] ,[IsAgree]
                                      ,[AgreeTime],[RetreatAddress],[Recipient],[RecipientTelephone] ,[DisagreeReason],[RetreatTime]
                                      ,[RetreatCourierNumber],[RetreatExpressName],[IsMerchantReceived],[MerchantReceivedTime]
                                      ,[IsPlatformRefund],[PlatformRefundTime],[CancellationType],[CancellationReason]
                                      ,[IsCancellation],[CancellationTime],[IsRetreat],[ConfirmReceivedTime]
                                  FROM [BussinessDb].[dbo].[Tb_Charge_Receipt] where Id=@OrderId";
                    var orderInfo = conn.QueryFirstOrDefault(sqlStr, new { OrderId = orderId });
                    if (null == orderInfo)
                        return new ResponseEntity<object>((int)ResoponseEnum.Error, "订单信息不存在").ToJson();

                    List<Object> result = new List<Object>();
                    int status = 0;

                    //判断是否退货
                    if (orderInfo.IsRetreat == "是")
                    {
                        status = (int)OrderSalesReturnEnum.ApplyFor;
                        result.Add(new
                        {
                            Type = "退货办理",
                            Time = orderInfo.CustomerApplicationTime,
                            Reason = orderInfo.RetreatReason,
                            Explain = orderInfo.RetreatExplain,
                            Images = orderInfo.RetreatImages,
                        });
                        //判断是否审核状态
                        if (!String.IsNullOrEmpty(orderInfo.IsAgree))
                        {
                            status = orderInfo.IsAgree == "是" ? (int)OrderSalesReturnEnum.Agree : (int)OrderSalesReturnEnum.Refuse;
                            if (status == 2) orderInfo.DisagreeReason = "您的退货申请已经审核通过";
                            result.Add(new
                            {
                                Type = "商家审核",
                                Time = orderInfo.AgreeTime,
                                Reason = orderInfo.DisagreeReason,
                            });

                            //判断是否发货
                            if (orderInfo.IsAgree == "是" && !String.IsNullOrEmpty(orderInfo.RetreatExpressName))
                            {
                                status = (int)OrderSalesReturnEnum.SalesReturn;
                                result.Add(new
                                {
                                    Type = "退货申请",
                                    Time = orderInfo.RetreatTime,
                                    RetreatCourierNumber = orderInfo.RetreatCourierNumber,
                                    RetreatExpressName = orderInfo.RetreatExpressName,
                                });

                                //判断是否到货
                                if (!String.IsNullOrEmpty(orderInfo.IsPlatformRefund) && orderInfo.IsMerchantReceived == "是")
                                {
                                    status = (int)OrderSalesReturnEnum.Receiving;
                                    result.Add(new
                                    {
                                        Type = "收货确认",
                                        Time = orderInfo.MerchantReceivedTime,
                                    });

                                    //平台是否退款
                                    if (!String.IsNullOrEmpty(orderInfo.IsPlatformRefund))
                                    {
                                        status = (int)OrderSalesReturnEnum.Refund;
                                        result.Add(new
                                        {
                                            Type = "退款办理",
                                            Time = orderInfo.PlatformRefundTime,
                                            Reason = "退款已完成,已退回至你的付款账户,请查收！",
                                        });
                                    }
                                }
                            }
                        }
                    }
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", new { Data = result, Status = status }).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 获取配送模板
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String GetFreightModel(DataRow row)
        {
            String businessId = String.Empty;
            if (!row.Table.Columns.Contains("BusinessId") || String.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @"SELECT  A.FreightName,B.* FROM
                                        (SELECT * FROM Tb_Resources_Freight where BussId=@BussId and  ISNULL (IsDelete,0)=0) AS A
                                        LEFT JOIN (SELECT * FROM  Tb_Resources_FreightDetail where ISNULL (IsDelete,0)=0) AS B ON A.ID=B.FreightID where  B.ID IS NOT NULL";
                    var freightInfo = conn.Query<FreightModel>(sqlStr, new { BussId = businessId });
                    Object result = null;
                    if (null != freightInfo && freightInfo.Any())
                    {
                        result = freightInfo.GroupBy(l => new { l.FreightID, l.FreightName }).Select(l => new
                        {
                            Name = l.Key.FreightName,
                            Id = l.Key.FreightID,
                            Detail = l.Select(p => new
                            {
                                Area = p.Area.Split(','),
                                FirstCost = p.FirstCost,
                                ContinuedCost = p.ContinuedCost,
                                FirstRange = p.FirstRange

                            }),
                        }); ;
                    }
                    if (result == null) result = new List<Object>();
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", result).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 获取配送模板
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String GetFreightDetail(DataRow row)
        {

            if (!row.Table.Columns.Contains("BusinessId") || String.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            if (!row.Table.Columns.Contains("FreightID") || String.IsNullOrEmpty(row["FreightID"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            String businessId = row["BusinessId"].ToString();
            String freightID = row["FreightID"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @"SELECT  A.FreightName,B.* FROM
                                        (SELECT * FROM Tb_Resources_Freight where BussId=@BussId and  ISNULL (IsDelete,0)=0) AS A
                                        LEFT JOIN (SELECT * FROM  Tb_Resources_FreightDetail where ISNULL (IsDelete,0)=0) AS B ON A.ID=B.FreightID
                                        Where b.FreightID=@FreightID";
                    var freightInfo = conn.Query<FreightModel>(sqlStr, new { BussId = businessId, FreightID = freightID });
                    List<Object> result = new List<object>();
                    if (null != freightInfo && freightInfo.Any())
                    {
                        foreach (var model in freightInfo)
                        {
                            if (null == model.Area) continue;
                            result.Add(new
                            {
                                Name = model.FreightName,
                                Area = model.Area.Split(','),
                                Id = model.FreightID,
                                FirstCost = model.FirstCost,
                                ContinuedCost = model.ContinuedCost,
                                FirstRange = model.FirstRange
                            });
                        }
                    }
                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", result).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private String GetFreightList(DataRow row)
        {
            String businessId = String.Empty;
            if (!row.Table.Columns.Contains("BusinessId") || String.IsNullOrEmpty(row["BusinessId"].AsString()))
            {
                return new ResponseEntity<object>((int)ResoponseEnum.FailureParam, "参数错误").ToJson();
            }
            businessId = row["BusinessId"].ToString();
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sqlStr = @"SELECT * FROM Tb_Resources_Freight WHERE ISNULL(IsDelete,0)=0 AND BussId=@BussId";
                    var freightInfo = conn.Query(sqlStr, new { BussId = businessId });

                    return new ResponseEntity<object>((int)ResoponseEnum.Success, "成功", freightInfo).ToJson();
                }
            }
            catch (Exception ex)
            {
                return new ResponseEntity<object>((int)ResoponseEnum.Error, "错误").ToJson();
            }
        }

        /// <summary>
        /// 刷新价格
        /// </summary>
        public void RefreshMoney(String resourcesID, SqlConnection Conn = null, SqlTransaction trans = null)
        {
            if (Conn == null) Conn = new SqlConnection(PubConstant.BusinessContionString);

            if (Conn.State == ConnectionState.Closed) Conn.Open();
            if (trans == null) trans = Conn.BeginTransaction();
            String sqlStr = "SELECT top 1 Price,DiscountAmount,GroupBuyingPrice FROM  Tb_ResourcesSpecificationsPrice WHERE ResourcesID=@ResourcesID ORDER BY Price";
            var priceInfo = Conn.QueryFirstOrDefault(sqlStr, new { ResourcesID = resourcesID }, trans);
            if (null != priceInfo)
            {
                sqlStr = @"UPDATE Tb_Resources_Details SET  ResourcesDisCountPrice=@ResourcesDisCountPrice,ResourcesSalePrice=@ResourcesSalePrice,GroupBuyPrice=@GroupBuyPrice  WHERE ResourcesID=@ResourcesID";

                Conn.Execute(sqlStr, new { ResourcesDisCountPrice = priceInfo.DiscountAmount, ResourcesSalePrice = priceInfo.Price, GroupBuyPrice = priceInfo.GroupBuyingPrice, ResourcesID = resourcesID }, trans);
            }
        }

        /// <summary>
        /// 订单自动审核
        /// <paramr name="orderId">订单主键</paramr>
        /// </summary>
        public static void Examine(String orderId)
        {
            try
            {
                using (var conn = new SqlConnection(PubConstant.BusinessContionString))
                {
                    String sql = @"SELECT * FROM  Tb_Charge_Receipt WHERE Id=@Id AND ISNULL(IsDelete,0)=0;";
                    Model.Model.Buss.Tb_Charge_Receipt tb_Charge_Receipt = conn.QueryFirstOrDefault<Model.Model.Buss.Tb_Charge_Receipt>(sql, new { Id = orderId });
                    if (tb_Charge_Receipt == null) return;
                    //有且仅有自提的时候自动完成顶单
                    if (tb_Charge_Receipt.DispatchingType == (int)EnumDispatchingType.TakeTheir)
                    {
                        sql = @"SELECT Province,City,Area,BussAddress,BussMobileTel,BussLinkMan FROM Tb_System_BusinessCorp WHERE  BussId=@BussId ;";
                        var tb_System_BusinessCorp = conn.QueryFirstOrDefault(sql, new { BussId = tb_Charge_Receipt.BussId });
                        if (tb_System_BusinessCorp == null) return;
                        sql = @" UPDATE Tb_Charge_Receipt SET  IsDeliver='已发货',Express=@Express,MerchantPickUpTime=@MerchantPickUpTime ,
                    MerchantPickUpLocation=@MerchantPickUpLocation,PickUpContactsPhone=@PickUpContactsPhone,PickUpRemarks=@PickUpRemarks,PickUpContacts=@PickUpContacts  WHERE ID=@ID ";
                        int count = conn.Execute(sql, new
                        {
                            Express = "",
                            MerchantPickUpTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            MerchantPickUpLocation =
                                String.Format("{0}{1}{2}{3}", tb_System_BusinessCorp.Province, tb_System_BusinessCorp.City, tb_System_BusinessCorp.Area, tb_System_BusinessCorp.BussAddress),
                            PickUpContactsPhone = tb_System_BusinessCorp.BussMobileTel,
                            PickUpContacts = tb_System_BusinessCorp.BussLinkMan,
                            PickUpRemarks = String.Empty,
                            ID = orderId,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                //ignore
            }
        }
    }
}

