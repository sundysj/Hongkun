using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using System.Web.SessionState;
using System.Reflection;
using System.IO;
using System.Data;
using System.Xml;
using MobileSoft.Common;
using System.Data.SqlClient;
using Dapper;
using System.Text;
using MobileSoft.DBUtility;
using System.Threading;
using System.Configuration;
using System.Net;
using System.Collections;

namespace Service
{
    /// <summary>
    /// Service 的摘要说明
    /// </summary>
    public class Files : IHttpHandler, IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            Common.Transfer Trans = new Common.Transfer();
            try
            {
                HttpRequest Request = context.Request;
                Trans.Class = Request["Class"].ToString();
                Trans.Command = Request["Command"].ToString();
                Trans.Attribute = HttpUtility.UrlDecode(Request["Attribute"].ToString());
                Trans.Mac = Request["Mac"].ToString();

                //验证令牌请求
                string HashString = Trans.Attribute.ToString() + DateTime.Now.ToString("yyyyMMdd") + "20160324QualityManageFiles";

                string Mac = AppPKI.getMd5Hash(HashString);

                if (HttpContext.Current.Request.Url.Host.Contains("localhost"))
                    Trans.Mac = Mac;

                if (Trans.Mac == Mac)
                {
                    switch (Trans.Command)
                    {
                        case "SaveFiles":
                            Trans.Result = SaveFiles(context, Trans);//碧桂园文件图片上传
                            break;
                        case "saveQualityTaskFiles":
                            Trans.Result = SaveQualityTaskFiles(context, Trans);
                            break;
                        case "SaveOwnerFiles":
                            Trans.Result = SaveOwnerFiles(context, Trans);
                            break;
                        case "SaveIncidentAcceptFiles":
                            Trans.Result = SaveIncidentAcceptFiles(context, Trans);
                            break;
                        case "SaveIncidentFiles_JH":
                            Trans.Result = SaveIncidentFiles_JH(context, Trans);
                            break;
                        case "SaveHousingFiles"://华南城装修巡查上传图片附件
                            Trans.Result = SaveHousingFiles(context, Trans);
                            break;
                        case "SaveRoomStateChangeHistoryFile"://华南城开业登记上传图片附件
                            Trans.Result = SaveRoomStateChangeHistoryFile(context, Trans);
                            break;
                        case "ComprehensiveInspectionFiles":
                            Trans.Result = ComprehensiveInspectionFiles(context, Trans);
                            break;
                        case "SaveSecondHandMarketFiles":
                            Trans.Result = SaveSecondHandMarketFiles(context, Trans);
                            break;
                        case "SaveBBSFiles":    // 社区论坛图片
                            Trans.Result = SaveBBSFiles(context, Trans);
                            break;
                        case "SaveIncidentBeforeProcessFiles":  // 报事处理前照片
                            Trans.Result = SaveIncidentBeforeProcessFiles(context, Trans);
                            break;
                        case "SaveIDCardFiles":  // 上传身份证
                            Trans.Result = SaveIDCardFiles(context, Trans);
                            break;
                        case "SaveFileOnUrl":
                            Trans.Result = SaveFileOnUrl(context, Trans);
                            break;
                        case "SaveHouseInspectFiles":   // 分户查验
                            Trans.Result = SaveHouseInspectFiles(context, Trans);
                            break;
                        case "SaveAppFile":
                            Trans.Result = SaveAppFile(context, Trans);
                            break;
                        case "Bl_SaveFiles":        // 保利上传文件
                            Trans.Result = Bl_SaveFiles(context, Trans);
                            break;
                        case "Bl_SaveIncidentFiles":        // 保利上传报事跟进、关闭文件
                            Trans.Result = Bl_SaveIncidentFiles(context, Trans);
                            break;
                        case "MeterSaveFiles"://平安需求3487 抄表图片上传
                            Trans.Result = MeterSaveFiles(context, Trans);
                            break;
                        case "RentalSaveFiles":
                            Trans.Result = RentalSaveFiles(context, Trans);//4042
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Trans.Error = "验证令牌错误";
                }

            }
            catch (Exception E)
            {
                Trans.Error = E.Message + E.StackTrace;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(Trans.Output());
        }

        private string SaveHouseInspectFiles(HttpContext context, Common.Transfer Trans)
        {
            try
            {
                if (context.Request.Files.Count == 0)
                {
                    return JSONHelper.FromString(false, "附件上传失败，附件已丢失");
                }

                //验证登录
                if (!new Login().isLogin(ref Trans))
                    return new ApiResult(false, "请先登录").toJson();

                #region 先保存起文件再进行数据判断(保证数据有问题时，图片先传成功)
                DateTime DateNow = DateTime.Now;
                string uploadPath = string.Format(@"{0}/{1}/{2}/{3}/", ConfigurationManager.AppSettings["HouseFilesPath"],
                    DateNow.Year, DateNow.Month, DateNow.Day);
                string RelaPath = string.Format(@"{0}/{1}/{2}/{3}/", ConfigurationManager.AppSettings["HouseFilesPathRela"],
                    DateNow.Year, DateNow.Month, DateNow.Day);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                if (context.Request.Files.Count == 0)
                {
                    return new ApiResult(false, "没有需要上传的文件").toJson();
                }
                string orignalFileName = context.Request.Files[0].FileName;
                string fileName = string.Format("App_{0}_{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    new Random(DateTime.Now.GetHashCode()).Next(999999).ToString().PadLeft(6, '0'),
                    Path.GetExtension(context.Request.Files[0].FileName));
                string FilePath = RelaPath + fileName;
                fileName = uploadPath + fileName;

                #endregion

                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];

                if (!Row.Table.Columns.Contains("TaskId"))
                {
                    return new ApiResult(false, "对象不包含TaskId属性").toJson();
                }
                if (!Row.Table.Columns.Contains("RoomID"))
                {
                    return new ApiResult(false, "对象不包含RoomID属性").toJson();
                }

                string TaskId = Row["TaskId"].ToString();
                string RoomID = Row["RoomID"].ToString();
                string StandardId = null;

                using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                {
                    conn.Open();
                    var trans = conn.BeginTransaction();

                    // 中南，添加签名图片
                    if (Row.Table.Columns.Contains("IsSignature") && Row["IsSignature"].ToString() == "1")
                    {
                        conn.Execute("UPDATE Tb_HI_TaskHouse SET SignatureImg=@SignatureImg WHERE TaskId=@TaskId AND HouseId=@HouseId",
                                        new { SignatureImg = FilePath, TaskId = TaskId, HouseId = RoomID }, trans);

                        trans.Commit();

                        context.Request.Files[0].SaveAs(fileName);

                        return new ApiResult(true, FilePath).toJson();
                    }

                    // 中南，返工
                    if (Row.Table.Columns.Contains("IsRework") && Row["IsRework"].ToString() == "1")
                    {
                        if (Row.Table.Columns.Contains("StandardId"))
                        {
                            StandardId = Row["StandardId"].ToString();
                        }

                        conn.Execute(@"DECLARE @TsId nvarchar(36), @IID nvarchar(36);
                                        SELECT @TsId=Id FROM Tb_HI_TaskStandard
                                        WHERE TaskId=@TaskId AND HouseId=@HouseId AND StandardId=@StandardId AND isnull(IsDelete,0)=0;

                                        SELECT TOP 1 @IID=Id FROM Tb_HI_TaskStandardReworkRecord WHERE TsID=@TsId ORDER BY AddTime DESC;

                                        UPDATE Tb_HI_TaskStandardReworkRecord SET ProblemImages=isnull(ProblemImages,'')+@FilePath+',' WHERE Id=@IID",
                                    new { TaskId = TaskId, HouseId = RoomID, StandardId = StandardId, FilePath = FilePath }, trans);

                        trans.Commit();

                        context.Request.Files[0].SaveAs(fileName);

                        return new ApiResult(true, FilePath).toJson();
                    }

                    string drawingId = null;
                    if (Row.Table.Columns.Contains("InspectionDrawingsId") && !string.IsNullOrEmpty(Row["InspectionDrawingsId"].ToString()))
                    {
                        drawingId = Row["InspectionDrawingsId"].ToString();
                    }

                    #region 查询对应任务的对应房屋是否存在
                    dynamic TaskHouse = conn.Query("SELECT * FROM Tb_HI_TaskHouse WHERE TaskId = @TaskId AND HouseId = @RoomID",
                        new { TaskId, RoomID }, trans).FirstOrDefault();

                    if (null == TaskHouse)
                    {
                        trans.Rollback();
                        return new ApiResult(false, "任务下的房屋不存在,保存失败(TaskId=" + TaskId + ",RoomID=" + RoomID + ")").toJson();
                    }
                    #endregion

                    // 是否是图纸
                    // 当没有上传StandardId字段的时候，为图纸上传，PkId为TaskHouse的Id
                    // 当为问题图片是，PkId为任务标准的Id
                    string PkId;

                    // 问题图片上传
                    if (Row.Table.Columns.Contains("StandardId"))
                    {
                        StandardId = Row["StandardId"].ToString();

                        #region 查询对应任务对象标准是否存在
                        dynamic Stanard = conn.Query("SELECT * FROM Tb_HI_TaskStandard WHERE TaskId = @TaskId AND HouseId = @RoomID AND StandardId = @StandardId",
                            new { TaskId, RoomID, StandardId }, trans).FirstOrDefault();
                        if (null == Stanard)
                        {
                            trans.Rollback();
                            return new ApiResult(false, "任务房屋下的对象标准不存在,保存失败(TaskId=" + TaskId + ",RoomID=" + RoomID + ",StandardId=" + StandardId + ")").toJson();
                        }
                        #endregion
                        PkId = Convert.ToString(Stanard.Id);
                    }
                    else
                    {
                        PkId = Convert.ToString(TaskHouse.Id);
                    }

                    if (conn.Execute("INSERT INTO Tb_HI_Files VALUES(NEWID(),@PkId,@FilesNo,NULL,NULL,@FilePath,NULL,NULL,NULL,0,@AddPid,@AddDate,NULL,NULL)",
                        new { PkId, FilePath, FilesNo = drawingId, AddPid = Global_Var.LoginUserCode, AddDate = DateNow.ToString() }, trans) <= 0)
                    {
                        trans.Rollback();
                        return new ApiResult(false, "保存文件信息失败").toJson();
                    }
                    trans.Commit();

                    context.Request.Files[0].SaveAs(fileName);

                    return new ApiResult(true, FilePath).toJson();
                }
            }
            catch (Exception e)
            {
                PubInfo.GetLog().Error(e);
                return new ApiResult(false, "接口抛出了一个异常" + e.StackTrace).toJson();
            }
        }

        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Trans"></param>
        /// <returns></returns>
        private string SaveFileOnUrl(HttpContext context, Common.Transfer Trans)
        {
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow dr = dAttributeTable.Rows[0];

                DateTime dateTime = DateTime.Now;
                string uploadPath = string.Format(@"{0}/{1}/{2}/{3}/", ConfigurationManager.AppSettings["HouseFilesPath"], dateTime.Year, dateTime.Month, dateTime.Day);
                string RelaPath = string.Format(@"{0}/{1}/{2}/{3}/", ConfigurationManager.AppSettings["HouseFilesPathRela"], dateTime.Year, dateTime.Month, dateTime.Day);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                if (context.Request.Files.Count == 0)
                {
                    return new ApiResult(false, "没有需要上传的文件").toJson();
                }

                string filePath = RelaPath + context.Request.Files[0].FileName;
                string fileName = uploadPath + context.Request.Files[0].FileName;
                // 如果存在同文件名，就删除之前文件
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                context.Request.Files[0].SaveAs(fileName);
                return new ApiResult(true, filePath).toJson();
            }
            catch (Exception e)
            {
                PubInfo.GetLog().Error(e);
                return new ApiResult(false, "接口抛出了一个异常").toJson();
            }
        }

        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Trans"></param>
        /// <returns></returns>
        private string SaveAppFile(HttpContext context, Common.Transfer Trans)
        {
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow dr = dAttributeTable.Rows[0];

                var dicName = "AppFiles";
                if (dAttributeTable.Columns.Contains("DicName"))
                {
                    if (!string.IsNullOrEmpty(dr["DicName"].ToString()))
                    {
                        dicName = dr["DicName"].ToString();
                    }
                }

                DateTime dateTime = DateTime.Now;
                string uploadPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", ConfigurationManager.AppSettings["AppFilesPath"], dicName,
                    dateTime.Year, dateTime.Month, dateTime.Day);
                string RelaPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", ConfigurationManager.AppSettings["AppFilesPathRela"], dicName,
                    dateTime.Year, dateTime.Month, dateTime.Day);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                if (context.Request.Files.Count == 0)
                {
                    return new ApiResult(false, "没有需要上传的文件").toJson();
                }

                string fileName = string.Format("App_{0}_{1}{2}",
                    dateTime.ToString("yyyyMMddHHmmss"),
                    new Random().Next(999999).ToString().PadLeft(6, '0'),
                    Path.GetExtension(context.Request.Files[0].FileName));

                context.Request.Files[0].SaveAs(uploadPath + fileName);
                return new ApiResult(true, RelaPath + fileName).toJson();
            }
            catch (Exception e)
            {
                PubInfo.GetLog().Error(e);
                return new ApiResult(false, "接口抛出了一个异常").toJson();
            }
        }


        /// <summary>
        /// 碧桂园入住登记文件上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Trans"></param>
        /// <returns></returns>
        private string SaveFiles(HttpContext context, Common.Transfer Trans)
        {
            //传入参数示例
            // <Attributes><Type>0:身份证正反面图片,1:入住登记业主+公司盖章图片,2:新增钥匙业主+经办人签字图片,3:领回钥匙业主+经办人签字图片</Type></Attributes>
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow dr = dAttributeTable.Rows[0];
                string folder = "FHH";
                //switch (dr["Type"].ToString())
                //{
                //    case "0":
                //        folder = "rzdjsfz";
                //        break;
                //    case "1":
                //        folder = "rzdjyzjz";
                //        break;
                //    case "2":
                //        folder = "ysxzyzjz";
                //        break;
                //    case "3":
                //        folder = "yslhyzjz";
                //        break;
                //    case "4":
                //        folder = "weituoren";
                //        break;

                //}
                string uploadPath = System.Configuration.ConfigurationManager.AppSettings["BGYFilesPath"];
                string relaPath = System.Configuration.ConfigurationManager.AppSettings["BGYFilesPathRela"];
                //string nFileDirectory = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/";
                uploadPath = uploadPath + folder + "/";
                relaPath = relaPath + folder + "/";
                DataTable dt = new DataTable("DT");
                dt.Columns.Add("FileName", Type.GetType("System.String"));
                dt.Columns.Add("FileUrl", Type.GetType("System.String"));
                string fileName = "";
                if (context.Request.Files.Count > 0)
                {
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        string[] str = context.Request.Files[i].FileName.Split('.');
                        if (str.Length <= 1)
                        {
                            json = JSONHelper.FromString(false, "上传失败：文件格式不正确");
                        }
                        //随机文件名
                        fileName = Guid.NewGuid().ToString("N") + "." + str[1];
                        dt.Rows.Add(new string[] { fileName, relaPath + fileName });
                        //保存
                        context.Request.Files[i].SaveAs(uploadPath + fileName);
                    }
                    json = JSONHelper.FromString(true, dt);
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;
        }


        /// <summary>
        /// 品质,设备任务   上传附件接口  
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Trans"></param>
        /// <returns></returns>
        private string SaveQualityTaskFiles(HttpContext context, Common.Transfer Trans)
        {
            //传入参数示例
            // <Attributes><CommID>1</CommID><Data>[{"TaskId":"1","PhotoTime":"2016-01-01 12:11:11"
            //  ,"Fix":"mp3/video/img","AddPId":"登录人员帐号","FileName":"xxx.jpg"}]</Data></Attributes>
            string json = "";
            try
            {

                //验证登录
                if (!new Login().isLogin(ref Trans))
                    return JSONHelper.FromString(false, "请先登录！");

                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "对象不包含CommId属性！");
                }

                DateTime dateTime = DateTime.Now;
                string uploadPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", ConfigurationManager.AppSettings["QualityTaskFilesPath"], Row["CommID"].ToString(), dateTime.Year, dateTime.Month, dateTime.Day);
                string RelaPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", ConfigurationManager.AppSettings["QualityTaskFilesPathRela"], Row["CommID"].ToString(), dateTime.Year, dateTime.Month, dateTime.Day);

                if (context.Request.Files.Count > 0)
                {
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        string extension = Path.GetExtension(context.Request.Files[i].FileName).ToLower();

                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }
                        //string  pathname = httpFile.FileName + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour +
                        //    DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + extension;

                        context.Request.Files[i].SaveAs(uploadPath + context.Request.Files[i].FileName);
                    }
                }

                DataTable dt = JSONHelper.JsonToDataTable(Row["data"].ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    // 品质
                    if (dt.Rows[0]["Type"].ToString() == "1")
                    {
                        QualityManage qmbll = new QualityManage();
                        foreach (DataRow dr in dt.Rows)
                        {
                            qmbll.AddQuanlityFiles(dr["TaskId"].ToString().Trim(), dr["PhotoTime"].ToString().Trim(), dr["Fix"].ToString().Trim(), dr["AddPId"].ToString().Trim(), dr["FileName"].ToString().Trim(), RelaPath);
                        }
                        json = JSONHelper.FromString(true, "上传成功");
                    }
                    // 设备
                    else
                    {
                        EquipmentManage qmbll = new EquipmentManage();
                        foreach (DataRow dr in dt.Rows)
                        {
                            qmbll.AddQuanlityFiles(dr["TaskId"].ToString().Trim(), dr["PhotoTime"].ToString().Trim(), dr["Fix"].ToString().Trim(), dr["AddPId"].ToString().Trim(), dr["FileName"].ToString().Trim(), RelaPath);
                        }
                        json = JSONHelper.FromString(true, "上传成功");
                    }
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;
        }

        /// <summary>
        /// 装修巡查图片
        /// </summary>
        private string SaveHousingFiles(HttpContext context, Common.Transfer Trans)
        {
            string json = "";
            try
            {

                //验证登录
                if (!new Login().isLogin(ref Trans))
                    return JSONHelper.FromString(false, "请先登录！");

                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "对象不包含CommId属性！");
                }
                if (!Row.Table.Columns.Contains("RoomID"))
                {
                    return JSONHelper.FromString(false, "对象不包含RoomID属性！");
                }

                DateTime dateTime = DateTime.Now;
                string uploadPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", ConfigurationManager.AppSettings["HouseFilesPath"], Row["CommID"].ToString(), dateTime.Year, dateTime.Month, dateTime.Day);
                string RelaPath = string.Format(@"{0}/{1}/{2}/{3}/{4}/", ConfigurationManager.AppSettings["HouseFilesPathRela"], Row["CommID"].ToString(), dateTime.Year, dateTime.Month, dateTime.Day);

                DataTable dt = JSONHelper.JsonToDataTable(Row["data"].ToString());

                if (context.Request.Files.Count > 0 && dt != null && dt.Rows.Count > 0 && context.Request.Files.Count == dt.Rows.Count)
                {
                    QualityManage qmbll = new QualityManage();

                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        string extension = Path.GetExtension(context.Request.Files[i].FileName).ToLower();

                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        string filePath = RelaPath + context.Request.Files[i].FileName;

                        context.Request.Files[i].SaveAs(uploadPath + context.Request.Files[i].FileName);
                        qmbll.AddHousingFiles(Row["CommID"].ToString(),
                            dr["InspID"]?.ToString(),
                            dr["ProjID"]?.ToString(),
                            filePath,
                            Row["RoomID"].ToString());
                    }
                    json = JSONHelper.FromString(true, "上传成功");
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败，数据错误");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;
        }

        /// <summary>
        /// 开业登记图片
        /// </summary>
        private string SaveRoomStateChangeHistoryFile(HttpContext context, Common.Transfer Trans)
        {
            //传入参数示例
            // <Attributes><CommID>小区编号1000-10013</CommID></Attributes>
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "小区编号不能为空！");
                }
                string uploadPath = ConfigurationManager.AppSettings["RoomStateHistoryFilesPath"] + Row["CommID"].ToString() + "/";
                string RelaPath = ConfigurationManager.AppSettings["RoomStateHistoryFilesPathRela"] + Row["CommID"].ToString() + "/";

                if (context.Request.Files.Count > 0)
                {
                    Random random = new Random();
                    var tmp = new List<string>();

                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        string[] str = context.Request.Files[i].FileName.Split('.');
                        if (str.Length <= 1)
                        {
                            json = JSONHelper.FromString(false, "上传失败：文件格式不正确");
                        }

                        string fileName = string.Format("App_{0}_{1}.{2}", DateTime.Now.ToString("yyyyMMddHHmmss"),
                                            random.Next(999999).ToString().PadLeft(6, '0'), str[1]);
                        //随机文件名
                        context.Request.Files[i].SaveAs(uploadPath + fileName);
                        tmp.Add(RelaPath + fileName);
                    }

                    json = JSONHelper.FromString(true, string.Join(",", tmp));
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;
        }

        /// <summary>
        /// 业主APP 头像上传接口
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Trans"></param>
        /// <returns></returns>
        private string SaveOwnerFiles(HttpContext context, Common.Transfer Trans)
        {
            //传入参数示例
            // <Attributes><CommID>小区编号1000-10013</CommID><Mobile>手机号</Mobile><UserPic>传""</UserPic></Attributes>
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "对象不包含CommId属性！");
                }
                if (!Row.Table.Columns.Contains("Mobile"))
                {
                    return JSONHelper.FromString(false, "手机号不能为空！");
                }

                string uploadPath = System.Configuration.ConfigurationManager.AppSettings["OwnerTaskFilesPath"] + Row["CommID"].ToString() + "/";
                string RelaPath = System.Configuration.ConfigurationManager.AppSettings["OwnerTaskFilesPathRela"] + Row["CommID"].ToString() + "/";

                if (context.Request.Files.Count > 0)
                {
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        string[] str = context.Request.Files[i].FileName.Split('.');
                        if (str.Length <= 1)
                        {
                            json = JSONHelper.FromString(false, "上传失败：文件格式不正确");
                        }
                        string fileName = Guid.NewGuid().ToString() + "." + str[1];
                        RelaPath += fileName;
                        //随机文件名
                        context.Request.Files[i].SaveAs(uploadPath + fileName);
                    }
                    Row["UserPic"] = RelaPath;
                    new BindUserInfo().Save(Row);
                    json = JSONHelper.FromString(true, RelaPath);
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;

        }

        /// <summary>
        /// 业主报事图片上传接口
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Trans"></param>
        /// CommID 小区编号必填
        /// 返回信息：
        ///     trur:图片路径
        ///     fasle:错误信息
        /// <returns></returns>
        private string SaveIncidentAcceptFiles(HttpContext context, Common.Transfer Trans)
        {
            //传入参数示例
            // <Attributes><CommID>小区编号1000-10013</CommID></Attributes>
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "小区编号不能为空！");
                }
                string uploadPath = ConfigurationManager.AppSettings["IncidentAcceptTaskFilesPath"] + Row["CommID"].ToString() + "/";
                string RelaPath = ConfigurationManager.AppSettings["IncidentAcceptTaskFilesPathRela"] + Row["CommID"].ToString() + "/";

                if (context.Request.Files.Count > 0)
                {
                    string UrlStr = "";
                    Random random = new Random();

                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        string[] str = context.Request.Files[i].FileName.Split('.');
                        if (str.Length <= 1)
                        {
                            json = JSONHelper.FromString(false, "上传失败：文件格式不正确");
                        }

                        string fileName = string.Format("App_{0}_{1}.{2}", DateTime.Now.ToString("yyyyMMddHHmmss"),
                                            random.Next(999999).ToString().PadLeft(6, '0'), str[1]);
                        //随机文件名
                        context.Request.Files[i].SaveAs(uploadPath + fileName);
                        if (i > 0)
                        {
                            UrlStr += "," + RelaPath + fileName;
                        }
                        else
                        {
                            UrlStr += RelaPath + fileName;
                        }

                    }

                    json = JSONHelper.FromString(true, UrlStr);
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;
        }

        /// <summary>
        /// 综合巡查文件
        /// </summary>
        private string ComprehensiveInspectionFiles(HttpContext context, Common.Transfer Trans)
        {
            //传入参数示例
            // <Attributes><CommID>小区编号1000-10013</CommID></Attributes>
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];

                bool verifyPassword = true;

                // 荣盛不验证账号密码
                if (Row["Account"].ToString().StartsWith("2036-"))
                {
                    verifyPassword = false;
                }

                //验证登录
                if (!new Login().isLogin(ref Trans, verifyPassword))
                    return JSONHelper.FromString(false, "请先登录！");

                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "小区编号不能为空！");
                }
                string uploadPath = ConfigurationManager.AppSettings["ComprehensiveInspectionFilesPath"] + Row["CommID"].ToString() + "/";
                string RelaPath = ConfigurationManager.AppSettings["ComprehensiveInspectionFilesPathRela"] + Row["CommID"].ToString() + "/";

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (context.Request.Files.Count > 0)
                {
                    using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        List<string> list = new List<string>();
                        DataTable dt = JSONHelper.JsonToDataTable(Row["data"].ToString());
                        string sql = @"INSERT INTO Tb_CP_TaskFiles(FileId, TaskId, PointId, FileName, Fix, FilePath, PhoneName, 
                                        PhotoTime, PhotoUserCode, IsDelete)
                                        VALUES (newid(),@TaskId,@PointId,@FileName,@Fix,@FilePath,null,getdate(),@UserCode,0)";

                        for (int i = 0; i < context.Request.Files.Count; i++)
                        {
                            string fileName = context.Request.Files[i].FileName;

                            //随机文件名
                            context.Request.Files[i].SaveAs(uploadPath + fileName);

                            list.Add(RelaPath + fileName);

                            // 更新数据库
                            DataRow dataRow = dt.Rows[i];
                            string taskIdStr = dataRow["TaskId"].ToString().Trim();
                            if (string.IsNullOrEmpty(taskIdStr) == false)
                            {
                                string[] idArray = taskIdStr.Split('|');

                                if (idArray.Length == 2)
                                {
                                    conn.Execute(sql, new
                                    {
                                        TaskId = idArray[0],
                                        PointId = idArray[1],
                                        FileName = fileName,
                                        Fix = fileName.Substring(fileName.LastIndexOf('.')),
                                        FilePath = RelaPath + fileName,
                                        UserCode = Global_Var.LoginUserCode
                                    });
                                }
                            }
                        }

                        json = JSONHelper.FromString(true, string.Join(",", list));
                    }
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message + Environment.NewLine + ex.StackTrace);
            }

            return json;

        }

        /// <summary>
        /// 报事跟进图片
        /// </summary>
        private string SaveIncidentFiles_JH(HttpContext context, Common.Transfer Trans)
        {
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow row = dAttributeTable.Rows[0];

                if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
                {
                    return JSONHelper.FromString(false, "缺少参数CommID");
                }
                if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
                {
                    return JSONHelper.FromString(false, "缺少参数IncidentID");
                }
                if (!row.Table.Columns.Contains("AdjunctName") || string.IsNullOrEmpty(row["AdjunctName"].AsString()))
                {
                    return JSONHelper.FromString(false, "缺少参数AdjunctName");
                }
                if (!row.Table.Columns.Contains("Net") || string.IsNullOrEmpty(row["Net"].AsString()))
                {
                    return JSONHelper.FromString(false, "缺少参数Net");
                }
                string commID = row["CommID"].AsString();
                string incidentId = row["IncidentID"].AsString();
                string AdjunctName = row["AdjunctName"].AsString();
                string NetType = row["Net"].ToString();
                if (NetType == "1")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("10Connection");
                }
                if (NetType == "2")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("14Connection");
                }
                if (NetType == "3")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("37Connection");
                }
                if (NetType == "4")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("19Connection");
                }
                if (NetType == "6")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("36Connection");
                }
                if (NetType == "99")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("12Connection");
                }

                string connStr;
                if (!GetDBServerPath(commID, out connStr))
                {
                    return JSONHelper.FromString(false, connStr);
                }

                DateTime dateTime = DateTime.Now;

                string _tempPath = string.Format("{0}/{1}/{2}/", ConfigurationManager.AppSettings["IncidentFollowFilesPath"], dateTime.Year, dateTime.Month);
                _tempPath = _tempPath.Replace("\\", "/");
                string uploadPath = ConfigurationManager.AppSettings["IncidentFollowFilesPathRela"] + dateTime.Year + "\\" + dateTime.Month + "\\";

                if (context.Request.Files.Count > 0)
                {
                    List<string> array = new List<string>();
                    // 保存到数据库
                    using (IDbConnection conn = new SqlConnection(connStr))
                    {
                        string sql = @"INSERT INTO Tb_HSPR_IncidentAdjunct(AdjunctCode,IncidentID, AdjunctName, FilPath,FileExName, FileSize)
                                            VALUES(@AdjunctCode,@IncidentID,@AdjunctName,@FilPath,@FileExName,@FileSize)";

                        for (int i = 0; i < context.Request.Files.Count; i++)
                        {
                            if (!Directory.Exists(uploadPath))
                            {
                                Directory.CreateDirectory(uploadPath);
                            }

                            string[] str = context.Request.Files[i].FileName.Split('.');
                            if (str.Length <= 1)
                            {
                                json = JSONHelper.FromString(false, "上传失败：文件格式不正确");
                            }
                            Thread.Sleep(50);
                            string AdjunctCode = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random((int)(DateTime.Now.Ticks & 0xffffffffL) | (int)(DateTime.Now.Ticks >> 32)).Next(100, 999).ToString();
                            string FileExName = "." + str[1].ToUpper();
                            float FileSize = (context.Request.Files[i].ContentLength / 1024.0F);

                            array.Add(_tempPath + AdjunctCode + FileExName);

                            context.Request.Files[i].SaveAs(uploadPath + AdjunctCode + FileExName);

                            conn.Execute(sql, new
                            {
                                AdjunctCode = AdjunctCode,
                                IncidentID = incidentId,
                                AdjunctName = AdjunctName,
                                FilPath = _tempPath + AdjunctCode + FileExName,
                                FileExName = FileExName,
                                FileSize = FileSize
                            });
                        }

                        json = JSONHelper.FromString(true, string.Join(",", array));
                    }
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json.Replace("\\", "/");
        }

        /// <summary>
        /// 泰禾，跳蚤市场图片
        /// </summary>
        private string SaveSecondHandMarketFiles(HttpContext context, Common.Transfer Trans)
        {
            //传入参数示例
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "小区编号不能为空！");
                }
                string uploadPath = ConfigurationManager.AppSettings["BBSSecondHandMarketFilesPath"] + Row["CommID"].ToString() + "/";
                string RelaPath = ConfigurationManager.AppSettings["BBSSecondHandMarketFilesPathRela"] + Row["CommID"].ToString() + "/";

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (context.Request.Files.Count > 0)
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        string fileName = context.Request.Files[i].FileName;

                        //随机文件名
                        context.Request.Files[i].SaveAs(uploadPath + fileName);

                        list.Add(RelaPath + fileName);
                    }

                    json = JSONHelper.FromString(true, string.Join(",", list));
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;
        }

        /// <summary>
        /// 社区论坛
        /// </summary>
        private string SaveBBSFiles(HttpContext context, Common.Transfer Trans)
        {
            string uploadPath = null;
            string RelaPath = null;

            //传入参数示例
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "小区编号不能为空！");
                }
                if (!Row.Table.Columns.Contains("Type"))
                {
                    return JSONHelper.FromString(false, "类型不能为空");
                }
                string type = Row["Type"].ToString();

                if (type == "2")        // 聊吧
                {
                    uploadPath = ConfigurationManager.AppSettings["BBSChatBarFilesPath"] + Row["CommID"].ToString();
                    RelaPath = ConfigurationManager.AppSettings["BBSChatBarFilesPathRela"] + Row["CommID"].ToString();
                }
                else if (type == "3")   // 圈子
                {
                    uploadPath = ConfigurationManager.AppSettings["BBSCircleFilesPath"] + Row["CommID"].ToString();
                    RelaPath = ConfigurationManager.AppSettings["BBSCircleFilesPathRela"] + Row["CommID"].ToString();
                }
                else if (type == "4")   // 跳蚤
                {
                    uploadPath = ConfigurationManager.AppSettings["BBSSecondHandMarketFilesPath"] + Row["CommID"].ToString();
                    RelaPath = ConfigurationManager.AppSettings["BBSSecondHandMarketFilesPathRela"] + Row["CommID"].ToString();
                }
                else
                {
                    return JSONHelper.FromString(false, "类型错误");
                }

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (context.Request.Files.Count > 0)
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        string fileName = context.Request.Files[i].FileName;

                        //随机文件名
                        context.Request.Files[i].SaveAs(uploadPath + fileName);

                        list.Add(RelaPath + fileName);
                    }

                    json = JSONHelper.FromString(true, string.Join(",", list));
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = new ApiResult(false, uploadPath + ex.Message + ex.StackTrace + ex.InnerException).toJson();
            }

            return json;
        }

        /// <summary>
        /// 中南置地，报事处理前照片
        /// </summary>
        private string SaveIncidentBeforeProcessFiles(HttpContext context, Common.Transfer Trans)
        {
            //传入参数示例
            // <Attributes><CommID>小区编号1000-10013</CommID></Attributes>
            string json = "";
            try
            {
                //验证登录
                if (!new Login().isLogin(ref Trans))
                    return JSONHelper.FromString(false, "请先登录！");

                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "小区编号不能为空！");
                }
                if (!Row.Table.Columns.Contains("TaskCode"))
                {
                    return JSONHelper.FromString(false, "任务编号不能为空！");
                }
                string Type = "1";
                if (Row.Table.Columns.Contains("Type") && !string.IsNullOrEmpty(Row["Type"].ToString()))
                {
                    Type = Row["Type"].ToString();
                }

                string TypeName = "报事处理前图片";
                switch (Type)
                {
                    case "1":
                        TypeName = "报事处理前图片";
                        break;
                    case "2":
                        TypeName = "暂停申请";
                        break;
                    case "3":
                        TypeName = "延期申请";
                        break;
                    case "4":
                        TypeName = "非正常关闭申请";
                        break;
                    default:
                        TypeName = "报事处理前图片";
                        break;
                }

                string CommID = Row["CommID"].ToString();
                string TaskCode = Row["TaskCode"].ToString();
                string TaskFiles = null;
                string uploadPath = ConfigurationManager.AppSettings["IncidentAcceptTaskFilesPath"] + CommID + "/";
                string RelaPath = ConfigurationManager.AppSettings["IncidentAcceptTaskFilesPathRela"] + CommID + "/";

                if (Row.Table.Columns.Contains("TaskFiles") && !string.IsNullOrEmpty(Row["TaskCode"].ToString()))
                {
                    TaskFiles = Row["TaskFiles"].ToString();
                }

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (context.Request.Files.Count > 0)
                {
                    using (IDbConnection conn = new SqlConnection(PubConstant.hmWyglConnectionString))
                    {
                        var list = new List<string>();

                        if (Type == "1")
                        {
                            for (int i = 0; i < context.Request.Files.Count; i++)
                            {
                                string fileName = TaskFiles ?? context.Request.Files[i].FileName;
                                if (fileName.Contains("|"))
                                {
                                    var arr = fileName.Split('|');
                                    if (arr.Length >= 3)
                                    {
                                        list.Add(RelaPath + arr[0]);
                                        context.Request.Files[i].SaveAs(uploadPath + arr[0]);

                                        conn.Execute(@"INSERT INTO Tb_Incident_Task_File (ID,TaskCode,FilePath,FileType,AddDate,
                                                    ShootingTime,ShootingLocation) 
                                                    VALUES(NEWID(),@TaskCode,@FilePath,@FileType,GETDATE(),@ShootingTime,@ShootingLocation)",
                                                    new
                                                    {
                                                        TaskCode = TaskCode,
                                                        FilePath = list.Last(),
                                                        ShootingTime = arr[1],
                                                        ShootingLocation = string.Concat(arr.Skip(2)),
                                                        FileType = TypeName
                                                    });
                                        continue;
                                    }
                                }

                                list.Add(RelaPath + fileName);
                                conn.Execute(@"INSERT INTO Tb_Incident_Task_File (ID,TaskCode,FilePath,FileType,AddDate) 
                                        VALUES(NEWID(),@TaskCode,@FilePath,@FileType,GETDATE())",
                                                new { TaskCode = TaskCode, FilePath = list.Last(), FileType = TypeName });
                            }
                        }
                        else
                        {
                            if (!Row.Table.Columns.Contains("IID"))
                            {
                                return JSONHelper.FromString(false, "IID不能为空！");
                            }

                            string IID = Row["IID"].ToString();
                            Random random = new Random();

                            for (int i = 0; i < context.Request.Files.Count; i++)
                            {
                                string fileName = TaskFiles ?? context.Request.Files[i].FileName;
                                string fix = null;
                                if (fileName.Contains("|"))
                                {
                                    var arr = fileName.Split('|');
                                    if (arr.Length >= 3)
                                    {
                                        fix = Path.GetExtension(arr[0]);

                                        list.Add(RelaPath + arr[0]);
                                        context.Request.Files[i].SaveAs(uploadPath + arr[0]);

                                        conn.Execute(@"INSERT INTO Tb_Incident_Files(ID,KeyID,FileName,Fix,FilePath,TypeName,
                                                        AddTime,AddUser,IsDelete,ShootingTime,ShootingLocation)
                                                        VALUES(newid(),@KeyID,@FileName,@Fix,@FilePath,@TypeName,getdate(),
                                                        @AddUser,0,@ShootingTime,@ShootingLocation)",
                                                        new
                                                        {
                                                            KeyID = IID,
                                                            FileName = string.Format("App_{0}_{1}{2}",
                                                                DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                                random.Next(999999).ToString().PadLeft(6, '0'), fix),
                                                            Fix = fix,
                                                            FilePath = list.Last(),
                                                            TypeName = TypeName,
                                                            AddUser = Global_Var.LoginUserCode,
                                                            ShootingTime = arr[1],
                                                            ShootingLocation = string.Concat(arr.Skip(2))
                                                        });
                                        continue;
                                    }

                                    list.Add(RelaPath + fileName);

                                    fix = Path.GetExtension(fileName);
                                    conn.Execute(@"INSERT INTO Tb_Incident_Files(ID,KeyID,FileName,Fix,FilePath,TypeName,
                                                    AddTime,AddUser,IsDelete)
                                                    VALUES(newid(),@KeyID,@FileName,@Fix,@FilePath,@TypeName,getdate(),@AddUser,0)",
                                                    new
                                                    {
                                                        KeyID = IID,
                                                        FileName = string.Format("App_{0}_{1}{2}",
                                                            DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                            random.Next(999999).ToString().PadLeft(6, '0'), fix),
                                                        Fix = fix,
                                                        FilePath = list.Last(),
                                                        TypeName = TypeName,
                                                        AddUser = Global_Var.LoginUserCode
                                                    });
                                }
                            }
                        }

                        json = JSONHelper.FromString(true, string.Join(",", list));
                    }
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;
        }

        /// <summary>
        /// 上传身份证
        /// </summary>
        private string SaveIDCardFiles(HttpContext context, Common.Transfer Trans)
        {
            //传入参数示例
            // <Attributes><CommID>小区编号1000-10013</CommID></Attributes>
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                if (!Row.Table.Columns.Contains("CommID"))
                {
                    return JSONHelper.FromString(false, "小区编号不能为空！");
                }
                string uploadPath = ConfigurationManager.AppSettings["InviteUserIDCardFilesPath"] + Row["CommID"].ToString() + "/";
                string RelaPath = ConfigurationManager.AppSettings["InviteUserIDCardFilesPathRela"] + Row["CommID"].ToString() + "/";

                if (context.Request.Files.Count > 0)
                {
                    string UrlStr = "";
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        string[] str = context.Request.Files[i].FileName.Split('.');
                        if (str.Length <= 1)
                        {
                            json = JSONHelper.FromString(false, "上传失败：文件格式不正确");
                        }
                        string fileName = Guid.NewGuid().ToString() + "." + str[1];
                        //随机文件名
                        context.Request.Files[i].SaveAs(uploadPath + fileName);
                        if (i > 0)
                        {
                            UrlStr += "," + RelaPath + fileName;
                        }
                        else
                        {
                            UrlStr += RelaPath + fileName;
                        }

                    }

                    json = JSONHelper.FromString(true, UrlStr.Trim(','));
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;

        }

        /// <summary>
        /// 保利上传文件
        /// </summary>
        private string Bl_SaveFiles(HttpContext context, Common.Transfer Trans)
        {
            DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];
            if (!Row.Table.Columns.Contains("DicName") || string.IsNullOrEmpty(Row["DicName"].ToString()))
            {
                return JSONHelper.FromString(false, "要存储的路径不能为空");
            }

            try
            {
                int CorpID = 6008;
                if (Row.Table.Columns.Contains("CorpID") && !string.IsNullOrEmpty(Row["CorpID"].ToString()))
                {
                    CorpID = AppGlobal.StrToInt(Row["CorpID"].ToString());
                }
                var DicName = Row["DicName"].ToString();

                var files = context.Request.Files;
                var postUrl = ConfigurationManager.AppSettings["Bl_FileUploadUrl"];
                var serviceUrl = ConfigurationManager.AppSettings["Bl_FileUploadServicesUrl"];

                // 要返回的url结果集
                List<string> returnUrls = new List<string>();

                for (int i = 0; i < files.Count; i++)
                {
                    var _f = files[i];
                    var fileExt = Path.GetExtension(_f.FileName).ToLower().Substring(1);
                    var fileLen = _f.InputStream.Length;

                    Random random = new Random();
                    string fileName = string.Format("App_{0}_{1}.{2}", DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                            random.Next(999999).ToString().PadLeft(6, '0'), fileExt);

                    byte[] byteFile = new byte[fileLen];
                    _f.InputStream.Read(byteFile, 0, (int)fileLen);
                    _f.InputStream.Seek(0, System.IO.SeekOrigin.Begin); //读取流之后设置设置当前流的位置，因为md5加密也需要用到该流

                    WebClient wc = new WebClient();
                    HttpWebRequest request = WebRequest.Create($"{postUrl}?Ram=Math.random()&CorpId={CorpID}&ExtName={fileExt}&DicName={DicName}") as HttpWebRequest;

                    CookieContainer cookieContainer = new CookieContainer();
                    request.CookieContainer = cookieContainer;
                    request.AllowAutoRedirect = true;
                    request.Method = "POST";
                    request.Timeout = 30 * 1000;
                    string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
                    request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
                    byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                    byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

                    //请求头部信息 
                    StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
                    byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
                    byte[] bArr = byteFile;
                    Stream postStream = request.GetRequestStream();
                    postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                    postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                    postStream.Write(bArr, 0, bArr.Length);
                    postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                    postStream.Close();

                    //发送请求并获取相应回应数据
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                    //直到request.GetResponse()程序才开始向目标网页发送Post请求
                    Stream instream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(instream, Encoding.UTF8);

                    //返回结果网页（html）代码
                    string content = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        try
                        {
                            Hashtable arr = (Hashtable)JSON.Decode(content);
                            if (arr["Result"].ToString() == "true")
                            {
                                returnUrls.Add($"{serviceUrl}{arr["Data"].ToString()}");
                            }
                            else
                            {
                                return JSONHelper.FromString(false, "远程服务器错误：" + arr["Data"].ToString());
                            }
                        }
                        catch (Exception)
                        {
                            return JSONHelper.FromString(false, content);
                        }
                    }
                }

                if (returnUrls.Count == 0)
                {
                    return JSONHelper.FromString(false, "上传失败");
                }

                return JSONHelper.FromString(true, string.Join(",", returnUrls));
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private string Bl_SaveIncidentFiles(HttpContext context, Common.Transfer Trans)
        {
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow row = dAttributeTable.Rows[0];
                if (!row.Table.Columns.Contains("DicName") || string.IsNullOrEmpty(row["DicName"].ToString()))
                {
                    return JSONHelper.FromString(false, "要存储的路径不能为空");
                }

                if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].AsString()))
                {
                    return JSONHelper.FromString(false, "缺少参数CommID");
                }
                if (!row.Table.Columns.Contains("IncidentID") || string.IsNullOrEmpty(row["IncidentID"].AsString()))
                {
                    return JSONHelper.FromString(false, "缺少参数IncidentID");
                }
                if (!row.Table.Columns.Contains("AdjunctName") || string.IsNullOrEmpty(row["AdjunctName"].AsString()))
                {
                    return JSONHelper.FromString(false, "缺少参数AdjunctName");
                }
                if (!row.Table.Columns.Contains("Net") || string.IsNullOrEmpty(row["Net"].AsString()))
                {
                    return JSONHelper.FromString(false, "缺少参数Net");
                }
                string commID = row["CommID"].AsString();
                string incidentId = row["IncidentID"].AsString();
                string AdjunctName = row["AdjunctName"].AsString();
                string NetType = row["Net"].ToString();
                if (NetType == "1")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("10Connection");
                }
                if (NetType == "2")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("14Connection");
                }
                if (NetType == "3")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("37Connection");
                }
                if (NetType == "4")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("19Connection");
                }
                if (NetType == "6")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("36Connection");
                }
                if (NetType == "99")
                {
                    PubConstant.tw2bsConnectionString = Global_Fun.GetConnectionString("12Connection");
                }

                string connStr;
                if (!GetDBServerPath(commID, out connStr))
                {
                    return JSONHelper.FromString(false, "获取数据库信息失败");
                }

                int CorpID = 6008;
                if (row.Table.Columns.Contains("CorpID") && !string.IsNullOrEmpty(row["CorpID"].ToString()))
                {
                    CorpID = AppGlobal.StrToInt(row["CorpID"].ToString());
                }
                var DicName = row["DicName"].ToString();

                var files = context.Request.Files;
                var postUrl = ConfigurationManager.AppSettings["Bl_FileUploadUrl"];
                var serviceUrl = ConfigurationManager.AppSettings["Bl_FileUploadServicesUrl"];

                // 要返回的url结果集
                List<string> returnUrls = new List<string>();

                for (int i = 0; i < files.Count; i++)
                {
                    var _f = files[i];
                    var fileExt = Path.GetExtension(_f.FileName).ToLower().Substring(1);
                    var fileLen = _f.InputStream.Length;

                    byte[] byteFile = new byte[fileLen];
                    _f.InputStream.Read(byteFile, 0, (int)fileLen);
                    _f.InputStream.Seek(0, System.IO.SeekOrigin.Begin); //读取流之后设置设置当前流的位置，因为md5加密也需要用到该流

                    WebClient wc = new WebClient();
                    HttpWebRequest request = WebRequest.Create($"{postUrl}?Ram=Math.random()&CorpId={CorpID}&ExtName={fileExt}&DicName={DicName}") as HttpWebRequest;

                    CookieContainer cookieContainer = new CookieContainer();
                    request.CookieContainer = cookieContainer;
                    request.AllowAutoRedirect = true;
                    request.Method = "POST";
                    string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
                    request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
                    byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                    byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

                    //请求头部信息 
                    StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", _f.FileName));
                    byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
                    byte[] bArr = byteFile;
                    Stream postStream = request.GetRequestStream();
                    postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                    postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                    postStream.Write(bArr, 0, bArr.Length);
                    postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                    postStream.Close();

                    //发送请求并获取相应回应数据
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                    //直到request.GetResponse()程序才开始向目标网页发送Post请求
                    Stream instream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(instream, Encoding.UTF8);

                    //返回结果网页（html）代码
                    string content = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        Hashtable arr = (Hashtable)JSON.Decode(content);
                        if (arr["Result"].ToString() == "true")
                        {
                            returnUrls.Add($"{serviceUrl}{arr["Data"].ToString()}");
                        }
                        else
                        {
                            return JSONHelper.FromString(false, "远程服务器错误：" + arr["Data"].ToString());
                        }
                    }
                }

                if (returnUrls.Count == 0)
                {
                    return JSONHelper.FromString(false, "上传失败");
                }

                List<string> array = new List<string>();
                // 保存到数据库
                using (IDbConnection conn = new SqlConnection(connStr))
                {
                    string sql = @"INSERT INTO Tb_HSPR_IncidentAdjunct(AdjunctCode,IncidentID, AdjunctName, FilPath,FileExName, FileSize)
                                            VALUES(@AdjunctCode,@IncidentID,@AdjunctName,@FilPath,@FileExName,@FileSize)";

                    for (int i = 0; i < returnUrls.Count; i++)
                    {
                        Thread.Sleep(50);
                        string AdjunctCode = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random((int)(DateTime.Now.Ticks & 0xffffffffL) | (int)(DateTime.Now.Ticks >> 32)).Next(100, 999).ToString();
                        conn.Execute(sql, new
                        {
                            AdjunctCode = AdjunctCode,
                            IncidentID = incidentId,
                            AdjunctName = AdjunctName,
                            FilPath = returnUrls[i],
                            FileExName = Path.GetExtension(returnUrls[i]),
                            FileSize = 0
                        });
                    }
                }

                return JSONHelper.FromString(true, string.Join(",", returnUrls));
            }
            catch (Exception ex)
            {
                return JSONHelper.FromString(false, ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }


        /// <summary>
        /// 抄表图片上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Trans"></param>
        /// <returns></returns>
        private string MeterSaveFiles(HttpContext context, Common.Transfer Trans)
        {
            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow dr = dAttributeTable.Rows[0];


                string uploadPath = System.Configuration.ConfigurationManager.AppSettings["MeterHistoryFilesPath"];
                string relaPath = System.Configuration.ConfigurationManager.AppSettings["MeterHistoryFilesPathRela"];

                string fileName = "";
                if (context.Request.Files.Count > 0)
                {
                    Random random = new Random();
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    string[] str = context.Request.Files[0].FileName.Split('.');
                    if (str.Length <= 1)
                    {
                        json = JSONHelper.FromString(false, "上传失败：文件格式不正确");
                    }
                    //随机文件名
                    fileName = string.Format("App_{0}_{1}.{2}", DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                            random.Next(999999).ToString().PadLeft(6, '0'), str[1]);
                    //保存
                    context.Request.Files[0].SaveAs(uploadPath + fileName);

                    json = JSONHelper.FromString(true, relaPath + fileName);
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Trans"></param>
        /// <returns></returns>
        private string RentalSaveFiles(HttpContext context, Common.Transfer Trans)
        {

            string json = "";
            try
            {
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow dr = dAttributeTable.Rows[0];

                string uploadPath = System.Configuration.ConfigurationManager.AppSettings["RentalFilesPath"];
                string relaPath = System.Configuration.ConfigurationManager.AppSettings["RentalFilesPathRela"];




                if (context.Request.Files.Count > 0)
                {
                    Random random = new Random();
                    var tmp = new List<string>();

                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }

                        string[] str = context.Request.Files[i].FileName.Split('.');
                        if (str.Length <= 1)
                        {
                            json = JSONHelper.FromString(false, "上传失败：文件格式不正确");
                        }
                        string fileName = string.Format("App_{0}_{1}.{2}", DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                                random.Next(999999).ToString().PadLeft(6, '0'), str[1]);
                        context.Request.Files[i].SaveAs(uploadPath + fileName);
                        tmp.Add(relaPath + fileName);
                    }
                    json = JSONHelper.FromString(true, string.Join(",", tmp));
                }
                else
                {
                    json = JSONHelper.FromString(false, "上传失败：文件不存在");
                }
            }
            catch (Exception ex)
            {
                json = JSONHelper.FromString(false, "上传失败：" + ex.Message);
            }

            return json;
        }

        public DataTable XmlToDatatTable(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息  
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据  
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据                  
                    ds.ReadXml(Xmlrdr);
                    return ds.Tables[0];
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源  
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }


        public bool GetDBServerPath(string CommID, out string DBPath)
        {
            bool bl = true;
            DBPath = "";
            try
            {
                IDbConnection Connectionstr = new SqlConnection(PubConstant.tw2bsConnectionString);
                string strSql = "select CorpID from Tb_HSPR_community where IsNull( Isdelete,0)=0 and commid='" + CommID + "'";
                var corpId = Connectionstr.Query<int>(strSql).FirstOrDefault();
                if (corpId == 0)
                {
                    corpId = AppGlobal.StrToInt(CommID.ToString().Substring(0, 4));
                }
                DataTable dt = Connectionstr.ExecuteReader("select DBServer,DBName,DBPwd,DBUser from Tb_System_Corp where IsNull(Isdelete,0)=0 and CorpID='" + corpId + "'").ToDataSet().Tables[0];
                Connectionstr.Dispose();
                if (dt.Rows.Count <= 0)
                {
                    bl = false;
                    DBPath = "公司不存在";
                    return bl;
                }
                DataRow dr = dt.Rows[0];
                StringBuilder ConStr = new StringBuilder();
                ConStr.Append("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=100;Min Pool Size=0;");
                ConStr.Append("Pooling = true;");
                ConStr.AppendFormat(" data source = {0};", dr["DBServer"]);
                ConStr.AppendFormat(" initial catalog = {0};", dr["DBName"]);
                ConStr.AppendFormat(" PWD={0};", dr["DBPwd"]);
                ConStr.Append("persist security info=False;");
                ConStr.AppendFormat(" user id = {0};packet size=4096", dr["DBUser"]);
                DBPath = ConStr.ToString();
            }
            catch (Exception ex)
            {
                bl = false;
                DBPath = ex.Message;
            }

            return bl;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}