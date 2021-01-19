using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using KernelDev;
using KernelDev.DataAccess;
using Common;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml;

namespace Business
{

    /// <summary>
    /// 鸿坤400同步报事
    /// </summary>
    public class SynchronizeIncidentData
    {

        public SynchronizeIncidentData() { }

        #region 报事处理

        /// <summary>
        ///  报事处理 增 删 改  同步
        /// </summary>
        /// <param name="drData"></param>
        /// <param name="strLoginSQLConnStr"></param>
        /// <param name="sTitle">日志表中State 加入特定词 区分数据源</param>
        public static void SynchronizeData_WorkOrderInfo(Dictionary<string, string> drData, string strLoginSQLConnStr,string sTitle=null)
        {

            string Results = "";
            string json = "";
            string status = "";
            string sLogTitle = string.IsNullOrEmpty(sTitle) ? "" : "("+sTitle+")";
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                //加载参数  
                //string sql = string.Format("select * from View_Incident_Reception_Filter where ReceCode='{0}'");
                //DataTable dTable = new HM.DBUtility.DbHelperSQLP(strLoginSQLConnStr).Query(sql).Tables[0];
                Dictionary<string, string> dir = new Dictionary<string, string>();

                //请求 
                HK_webServiecReference.hkserviceImplService hkserver = new HK_webServiecReference.hkserviceImplService();

                json = "[" + (new JavaScriptSerializer()).Serialize(drData) + "]";
                json = json.Replace("null", "\" \"");
                Results = hkserver.workOrderInfoSynchronize(json);
                //  Results = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><?access-control allow=\" * \"?><response><Status>1</Status><message>成功</message><callCenterIncidentID>314</callCenterIncidentID></response>";
                // JObject obj = (JObject)JsonConvert.DeserializeObject(Results);  json 读取    
                xmlDoc.LoadXml(Results); //xml 读取  
                status = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "Status").Trim() == "0" ? "成功" : "失败";



                //Status	结果状态0:成功 1:失败
                //message 返回结果对应的描述  

                //反写数据  
                if (status == "成功" && drData["operateType"] == "0")
                {
                    //返回CallCenterid    
                    //存入 提交的报事单中  
                    SqlParameter[] parameters = {
                                        new SqlParameter("@incidentID", SqlDbType.BigInt),
                                        new SqlParameter("@callCenterIncidentID", SqlDbType.NVarChar,100)
                                      };
                    parameters[0].Value = Convert.ToInt64(drData["incidentID"]);
                    parameters[1].Value = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "callCenterIncidentID").Trim();


                    new MobileSoft.DBUtility.DbHelperSQLP(strLoginSQLConnStr).RunProcedure("proc_Interface_workOrderInfoSynchronize", parameters);
                }
            }
            catch (Exception ms)
            {
                Model.Task.Tb_HSPR_IncidentError IncidentError = new Model.Task.Tb_HSPR_IncidentError();

                //  JObject obj1 = (JObject)JsonConvert.DeserializeObject(Results);
                //写入日志

                IncidentError.Method = "workOrderInfoSynchronize";
                IncidentError.Parameter = json;


                IncidentError.State = "[app]请求接口失败" + sLogTitle;
                IncidentError.ErrorDate = DateTime.Now;
                IncidentError.ErrorContent = ms.ToString();
                new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError);
                return;
            }


            //写入日志
            Model.Task.Tb_HSPR_IncidentError IncidentError2 = new Model.Task.Tb_HSPR_IncidentError();
            IncidentError2.Method = "workOrderInfoSynchronize";
            IncidentError2.Parameter = json;
            IncidentError2.State ="[app]"+status+ sLogTitle;
            IncidentError2.ErrorDate = DateTime.Now;
            IncidentError2.ErrorContent = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "message").Trim();

            new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError2);

        }


        #endregion


        #region   报事处理状态同步

        /// <summary>
        /// 报事处理状态同步
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="TaskItem_1"></param>
        /// <returns></returns>
        public static void SynchronizeData_WorkOrderStatus(Dictionary<string, string> drData, string strLoginSQLConnStr)
        {
            string Results = "";
            string json = "";
            string status = "";
            XmlDocument xmlDoc = new XmlDocument();

            try
            {   //请求 
                HK_webServiecReference.hkserviceImplService hkserver = new HK_webServiecReference.hkserviceImplService();

                json = "[" + (new JavaScriptSerializer()).Serialize(drData) + "]";
                json = json.Replace("null", "\" \"");
                Results = hkserver.workOrderStatusSynchronize(json);
                //  JObject obj = (JObject)JsonConvert.DeserializeObject(Results); 
                xmlDoc.LoadXml(Results); //xml 读取

                status = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "Status").Trim() == "0" ? "成功" : "失败";

            }
            catch (Exception ms)
            {
                Model.Task.Tb_HSPR_IncidentError IncidentError = new Model.Task.Tb_HSPR_IncidentError();

                //  JObject obj1 = (JObject)JsonConvert.DeserializeObject(Results);
                //写入日志

                IncidentError.Method = "workOrderStatusSynchronize";
                IncidentError.Parameter = json;


                IncidentError.State = "[App]请求接口失败";
                IncidentError.ErrorDate = DateTime.Now;
                IncidentError.ErrorContent = ms.ToString();
                new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError);


                return;
            }

            //写入日志
            Model.Task.Tb_HSPR_IncidentError IncidentError2 = new Model.Task.Tb_HSPR_IncidentError();
            IncidentError2.Method = "workOrderStatusSynchronize";
            IncidentError2.Parameter = json;
            IncidentError2.State = "[App]" + status;
            IncidentError2.ErrorDate = DateTime.Now;
            IncidentError2.ErrorContent = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "message").Trim();
            new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError2);
        }


        #endregion



        #region   报事跟进

        /// <summary>
        /// 报事报事跟进
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="TaskItem_1"></param>
        /// <returns></returns>
        public static void SynchronizeData_workFollowSynchronize(Dictionary<string, string> drData, string strLoginSQLConnStr)
        {
            string Results = "";
            string json = "";
            string status = "";
            XmlDocument xmlDoc = new XmlDocument();

            try
            {   //请求 
                HK_webServiecReference.hkserviceImplService hkserver = new HK_webServiecReference.hkserviceImplService();

                json = "[" + (new JavaScriptSerializer()).Serialize(drData) + "]";
                json = json.Replace("null", "\" \"");
                Results = hkserver.workFollowSynchronize(json);

                xmlDoc.LoadXml(Results); //xml 读取

                status = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "Status").Trim() == "0" ? "成功" : "失败";

            }
            catch (Exception ms)
            {
                Model.Task.Tb_HSPR_IncidentError IncidentError = new Model.Task.Tb_HSPR_IncidentError();

                //  JObject obj1 = (JObject)JsonConvert.DeserializeObject(Results);
                //写入日志

                IncidentError.Method = "workFollowSynchronize";
                IncidentError.Parameter = json;


                IncidentError.State = "[App]请求接口失败";
                IncidentError.ErrorDate = DateTime.Now;
                IncidentError.ErrorContent = ms.ToString();
                new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError);


                return;
            }

            //写入日志
            Model.Task.Tb_HSPR_IncidentError IncidentError2 = new Model.Task.Tb_HSPR_IncidentError();
            IncidentError2.Method = "workFollowSynchronize";
            IncidentError2.Parameter = json;
            IncidentError2.State = "[App]" + status;
            IncidentError2.ErrorDate = DateTime.Now;
            IncidentError2.ErrorContent = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "message").Trim();
            new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError2);
        }


        #endregion



        #region   房屋信息同步

        /// <summary>
        /// 房屋信息同步
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="TaskItem_1"></param>
        /// <returns></returns>
        public static void SynchronizeData_roomInfoSynchronize(Dictionary<string, string> drData, string strLoginSQLConnStr)
        {
            string Results = "";
            string json = "";

            string status = "";
            XmlDocument xmlDoc = new XmlDocument();
            try
            {   //请求 
                HK_webServiecReference.hkserviceImplService hkserver = new HK_webServiecReference.hkserviceImplService();

                json = "[" + (new JavaScriptSerializer()).Serialize(drData) + "]";
                json = json.Replace("null", "\" \"");
                Results = hkserver.roomInfoSynchronize(json);
                xmlDoc.LoadXml(Results); //xml 读取

                status = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "Status").Trim() == "0" ? "成功" : "失败";

            }
            catch (Exception ms)
            {

                Model.Task.Tb_HSPR_IncidentError IncidentError = new Model.Task.Tb_HSPR_IncidentError();

                //  JObject obj1 = (JObject)JsonConvert.DeserializeObject(Results);
                //写入日志

                IncidentError.Method = "roomInfoSynchronize";
                IncidentError.Parameter = json;


                IncidentError.State = "[App]请求接口失败";
                IncidentError.ErrorDate = DateTime.Now;
                IncidentError.ErrorContent = ms.ToString();
                new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError);
                return;
            }

            //写入日志
            Model.Task.Tb_HSPR_IncidentError IncidentError2 = new Model.Task.Tb_HSPR_IncidentError();
            IncidentError2.Method = "roomInfoSynchronize";
            IncidentError2.Parameter = json;
            IncidentError2.State = "[App]" + status;
            IncidentError2.ErrorDate = DateTime.Now;
            IncidentError2.ErrorContent = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "message").Trim();
            new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError2);
        }
        #endregion


        #region   客户信息

        /// <summary>
        /// 客户信息
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="TaskItem_1"></param>
        /// <returns></returns>
        public static void SynchronizeData_customInfoSynchronize(Dictionary<string, string> drData, string strLoginSQLConnStr)
        {
            string Results = "";
            string json = "";
            string status = "";
            XmlDocument xmlDoc = new XmlDocument();

            try
            {   //请求 
                HK_webServiecReference.hkserviceImplService hkserver = new HK_webServiecReference.hkserviceImplService();

                json = "[" + (new JavaScriptSerializer()).Serialize(drData) + "]";
                json = json.Replace("null", "\" \"");
                Results = hkserver.customInfoSynchronize(json);

                xmlDoc.LoadXml(Results); //xml 读取
                status = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "Status").Trim() == "0" ? "成功" : "失败";
            }
            catch (Exception ms)
            {
                Model.Task.Tb_HSPR_IncidentError IncidentError = new Model.Task.Tb_HSPR_IncidentError();

                //  JObject obj1 = (JObject)JsonConvert.DeserializeObject(Results);
                //写入日志

                IncidentError.Method = "customInfoSynchronize";
                IncidentError.Parameter = json;


                IncidentError.State = "[App]请求接口失败";
                IncidentError.ErrorDate = DateTime.Now;
                IncidentError.ErrorContent = ms.ToString();
                new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError);
                return;
            }
            //写入日志
            Model.Task.Tb_HSPR_IncidentError IncidentError2 = new Model.Task.Tb_HSPR_IncidentError();
            IncidentError2.Method = "customInfoSynchronize";
            IncidentError2.Parameter = json;
            IncidentError2.State = "[App]" + status;
            IncidentError2.ErrorDate = DateTime.Now;
            IncidentError2.ErrorContent = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "message").Trim();
            new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError2);
        }
        #endregion


        #region   家庭成员

        /// <summary>
        /// 家庭成员
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="TaskItem_1"></param>
        /// <returns></returns>
        public static void SynchronizeData_familyMemberSynchronize(Dictionary<string, string> drData, string strLoginSQLConnStr)
        {
            string Results = "";
            string json = "";

            string status = "";
            XmlDocument xmlDoc = new XmlDocument();
            try
            {   //请求 
                HK_webServiecReference.hkserviceImplService hkserver = new HK_webServiecReference.hkserviceImplService();

                json = "[" + (new JavaScriptSerializer()).Serialize(drData) + "]";
                json = json.Replace("null", "\" \"");
                Results = hkserver.familyMemberSynchronize(json);
                // JObject obj = (JObject)JsonConvert.DeserializeObject(Results);
                xmlDoc.LoadXml(Results); //xml 读取

                status = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "Status").Trim() == "0" ? "成功" : "失败";
            }
            catch (Exception ms)
            {
                Model.Task.Tb_HSPR_IncidentError IncidentError = new Model.Task.Tb_HSPR_IncidentError();

                //  JObject obj1 = (JObject)JsonConvert.DeserializeObject(Results);
                //写入日志

                IncidentError.Method = "familyMemberSynchronize";
                IncidentError.Parameter = json;


                IncidentError.State = "[App]请求接口失败";
                IncidentError.ErrorDate = DateTime.Now;
                IncidentError.ErrorContent = ms.ToString();
                new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError);
                return;
            }

            //写入日志
            Model.Task.Tb_HSPR_IncidentError IncidentError2 = new Model.Task.Tb_HSPR_IncidentError();
            IncidentError2.Method = "familyMemberSynchronize";
            IncidentError2.Parameter = json;
            IncidentError2.State = "[App]" + status;
            IncidentError2.ErrorDate = DateTime.Now;
            IncidentError2.ErrorContent = GetxmlNodeValue(xmlDoc.SelectSingleNode("response").ChildNodes, "message").Trim();
            new BLL.Task.Bll_Tb_HSPR_IncidentError().Add(IncidentError2);
        }
        #endregion


        #region xml处理
        /// <summary>
        /// 获取xml节点值
        /// </summary>
        /// <param name="nodelist"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetxmlNodeValue(XmlNodeList nodelist, string key)
        {
            string strReturn = "";

            foreach (XmlNode xn in nodelist)//遍历所有子节点
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
                if (xe.Name == key)
                {
                    strReturn = xe.InnerText.Trim();
                    break;
                }
            }


            return strReturn;

        }
        #endregion


    }
}
