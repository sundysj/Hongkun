using Common;
using System;
using System.IO;
using System.Net;


namespace Service
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //Dapper.SqlMapper.AddTypeHandler(typeof(Guid), new DapperExtensions.GuidHandler());
            Dapper.SqlMapper.AddTypeHandler(typeof(Guid), new DapperExtensions.GuidHandler());

            MobileSoft.Common.Global_Fun.SetDateTimeFormat();
            TWLogger.Instance().Register();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            /*
            // 如果是北方，请求文件，转发
            if (Request.Url.OriginalString.Contains("113.247.231.148") || Request.Url.OriginalString.Contains("localhost"))
            {
                string filename = Request.QueryString[0];
                string last = filename.ToLower();

                if (last.Contains(".jpg") || last.Contains(".jpeg") || 
                    last.Contains(".gif") || last.Contains(".png") || last.Contains(".bmp") ||
                    last.Contains(".doc") || last.Contains(".docx") ||
                    last.Contains(".xls") || last.Contains(".xlsx") ||
                    last.Contains(".ppt") || last.Contains(".pptx") ||
                    last.Contains(".pdf") || last.Contains(".txt") ||
                    last.Contains(".rar") || last.Contains(".zip") ||
                    last.Contains(".mp4") || last.Contains(".mov") || last.Contains(".avi") ||
                    last.Contains(".mp3") || last.Contains(".wav") || last.Contains(".wma"))
                {
                    string url = null;
                    if (Request.Url.OriginalString.Contains("localhost"))
                    {
                        url = @"http://download.microsoft.com/download/C/1/4/C144048E-BB10-4D0A-8EB2-477319F78DC2/rewrite_x86_zh-CN.msi";
                    }
                    else
                    {
                        url = $"http://192.168.22.22/{last}";
                    }

                    try
                    {
                        string path = CopyFileByUrl(url);
                        Response.Write(path);
                        Response.End();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message + Environment.NewLine + ex.StackTrace);
                        Response.End();
                    }

                    

                    //if (!string.IsNullOrEmpty(path))
                    //{
                    //    Context.RewritePath(path);
                    //    Response.End();
                    //}
                }
            }
            */
        }

        public string CopyFileByUrl(string url)
        {
            string name = url.Substring(url.LastIndexOf('/') + 1);//获取名字
            string fileFolder = "..//..//TransferFiles";
            string filePath = Path.Combine(fileFolder, name);//存放地址就是本地的upload下的同名的文件
            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);

            string returnPath = GetSimplePath(filePath);//需要返回的路径
            if (File.Exists(filePath))
            {//如果已经存在，那么就不需要拷贝了，如果没有，那么就进行拷贝
                return returnPath;
            }
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ProtocolVersion = new Version(1, 1);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return string.Empty;//找不到则直接返回null
            }
            // 转换为byte类型
            Stream stream = response.GetResponseStream();

            //创建本地文件写入流
            Stream fs = new FileStream(filePath, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = stream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                fs.Write(bArr, 0, size);
                size = stream.Read(bArr, 0, (int)bArr.Length);
            }
            fs.Close();
            stream.Close();
            return returnPath;
        }

        public string GetSimplePath(string path)
        {
            path = path.Replace(@"\", "/");
            int pos = path.IndexOf("TransferFile");
            if (pos != -1)
            {
                pos = pos - 1;//拿到前面那个/,这样为绝对路径，直接保存在整个项目下的upload文件夹下
                return path.Substring(pos, path.Length - pos);
            }
            return "";
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}