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

namespace Service
{
    /// <summary>
    /// ErpUploadFile 的摘要说明
    /// </summary>
    public class ErpUploadFile : IHttpHandler
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
                string HashString = Trans.Attribute.ToString() + DateTime.Now.ToString("yyyyMMdd") + "ERPSaveFile";

                string Mac = AppPKI.getMd5Hash(HashString);

                if (Trans.Mac == Mac)
                {
                    switch (Trans.Command)
                    {
                        case "SaveFile":
                            Trans.Result = SaveFiles(context, Trans);
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
                Trans.Error = E.Message.ToString();
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(Trans.Output());
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Trans"></param>
        /// <returns></returns>
        private string SaveFiles(HttpContext context, Common.Transfer Trans)
        {
            string json = "";
            try
            {
                string FileName = "";
                string path = "";
                DataTable dAttributeTable = XmlToDatatTable(Trans.Attribute);
                DataRow Row = dAttributeTable.Rows[0];
                //上传物理路径
                string FilesPath = System.Configuration.ConfigurationManager.AppSettings["FilesPath"];
                //上传的绝对URL路径
                string FilesPathUrl = System.Configuration.ConfigurationManager.AppSettings["FilesPathUrl"];
                FileName = Guid.NewGuid().ToString();
                string ExtName = Row["ExtName"].ToString();

                string DirName = Row["DirName"].ToString();

                byte[] bytes = new byte[context.Request.InputStream.Length];
                context.Request.InputStream.Read(bytes, 0, bytes.Length);
                context.Request.InputStream.Seek(0, SeekOrigin.Begin);//设置当前流的位置
                context.Request.InputStream.Flush();
                context.Request.InputStream.Close();
                context.Request.InputStream.Dispose();
                Random rnd = new Random();
                int n = rnd.Next(1000, 9999);
                FileName = FileName + "." + ExtName;

                string nFileDirectory = DateTime.Now.Year.ToString() + "\\" + DateTime.Now.Month.ToString().PadLeft(2, '0');

                FilesPath = FilesPath + DirName+"\\"+nFileDirectory;

                if (Directory.Exists(FilesPath) == false)
                {
                    Directory.CreateDirectory(FilesPath);
                }

                path = FilesPath + "\\"+FileName;

                FileStream fs = new FileStream(path, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(bytes);
                bw.Close();
                fs.Close();

                nFileDirectory = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0');
                FilesPathUrl = FilesPathUrl + DirName+"/"+ nFileDirectory +"/"+FileName.ToString();
                return JSONHelper.FromString(true, FilesPathUrl);

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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}