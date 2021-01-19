using Common;
using MobileSoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	public class SyncIncidentStatusForZL: PubInfo
	{
		public SyncIncidentStatusForZL() //获取小区、项目信息
		{
			base.Token = "20201223SyncIncidentStatusForZL";
		}
		public override void Operate(ref Common.Transfer Trans)
		{
			Trans.Result = JSONHelper.FromString(false, "未知错误");

			DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
			DataRow Row = dAttributeTable.Rows[0];

			switch (Trans.Command)
			{
				case "SendStatusForZL":                // 获取小区信息
					string str = SendStatusForZL(0,0,"","");
					break;
				default:
					break;
			}
		}


		public static string getSignature(Dictionary<string, string> parameters)
		{
			string data = "";
			string secret = Global_Fun.AppWebSettings("ZLSecret");  //"3KUkEEV0zTLLXSqWG2KMRCgOU+GZDHj5cL1i8xdFVKD1QXG2lLCvOEYZs9SUKTqEbLSFtDO5DllNxYSG/nff6A==";
			byte[] key = Convert.FromBase64String(secret);
			HMACSHA1 myhmacsha1 = new HMACSHA1(key);
			myhmacsha1.Initialize();
			byte[] b = null;
			var vDic = parameters.OrderBy(x => x.Key, new ComparerString()).ToDictionary(x => x.Key, y => y.Value);
			foreach (KeyValuePair<string, string> kv in vDic)
			{
				data = data + kv.Key + kv.Value;
			}
			b = myhmacsha1.ComputeHash(Encoding.UTF8.GetBytes(data));
			return Convert.ToBase64String(b);
		}

		public  string SendStatusForZL(long IncidentID,int status,string flowNode,string message)
		{
			string requestUrl = Global_Fun.AppWebSettings("ZLRequestUrl");  //"http://test118.zuolin.com/vdocking/privilege/hasManagePrivilege";

			ZLDataModel model = new ZLDataModel();
			model.appKey = Global_Fun.AppWebSettings("ZLAppKey");  //"9057b579-1f68-456d-b98d-a151ddeacd35";
			model.timeStamp = ConvertDateTimeToLong(DateTime.Now);

			Random rad = new Random();//实例化随机数产生器rad；
			model.nonce = rad.Next(1000, 10000).ToString();

			model.namespaceId = int.Parse(Global_Fun.AppWebSettings("ZLNamespaceId"));  //11;
			model.sendNo = ConvertDateTimeToLong(DateTime.Now);
			model.incidentId = 19730200002222;
			model.status = 2;
			model.flowNode = "11111";
			model.message = "11111";

			Dictionary<string, string> parameters = new Dictionary<string, string>();
			parameters.Add("appKey", model.appKey);
			parameters.Add("nonce", model.nonce);
			parameters.Add("timeStamp", model.timeStamp);
			parameters.Add("namespaceId", model.namespaceId.ToString());
			parameters.Add("sendNo", model.sendNo);
			parameters.Add("data[0].incidentId", model.incidentId.ToString());
			parameters.Add("data[0].status", model.status.ToString());
			parameters.Add("data[0].flowNode", model.flowNode);
			parameters.Add("data[0].message", model.message);

			model.signature = getSignature(parameters);

			parameters.Add("signature", model.signature);

			string contents = string.Format(@"appKey={1}&timeStamp={2}&nonce={3}&namespaceId={4}&sendNo={5}&data[0].incidentId={6}&data[0].status={7}&data[0].flowNode={8}&data[0].message={9}&signature={10}", requestUrl, model.appKey,model.timeStamp,model.nonce, model.namespaceId, model.sendNo, model.incidentId, model.status, model.flowNode, model.message,model.signature);

			string str = SendHttp(requestUrl, contents);

			return str;
		}

		private bool CheckValidationResult(
		   Object sender,
		   X509Certificate certificate,
		   X509Chain chain,
		   SslPolicyErrors sslPolicyErrors
	   )
		{
			if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != SslPolicyErrors.RemoteCertificateNotAvailable)
				return true;
			throw new Exception("SSL验证失败");
		}

		public string SendHttp(string Url, string Contents)
		{
			#region 发送
			HttpWebRequest request = null;
			byte[] postData;
			Uri uri = new Uri(Url);
			if (uri.Scheme == "https")
			{
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(this.CheckValidationResult);
			}
			HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
			HttpWebRequest.DefaultCachePolicy = policy;

			request = (HttpWebRequest)WebRequest.Create(uri);
			request.AllowAutoRedirect = false;
			request.AllowWriteStreamBuffering = false;
			request.Method = "POST";

			postData = Encoding.GetEncoding("utf-8").GetBytes(Contents);

			request.ContentType = "application/x-www-form-urlencoded";
			//request.ContentType = "text/plain;charset = utf-8"; //request.ContentType = "text/plain";
			request.ContentLength = postData.Length;
			request.KeepAlive = false;

			Stream reqStream = request.GetRequestStream();
			reqStream.Write(postData, 0, postData.Length);
			reqStream.Close();
			#endregion

			#region 响应
			string respText = "";

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream resStream = response.GetResponseStream();

			MemoryStream ms = new MemoryStream();
			byte[] buf = new byte[4096];
			int count;
			while ((count = resStream.Read(buf, 0, buf.Length)) > 0)
			{
				ms.Write(buf, 0, count);
			}
			resStream.Close();
			respText = Encoding.GetEncoding("utf-8").GetString(ms.ToArray());
			#endregion

			return respText;
		}

		public static string ConvertDateTimeToLong(DateTime dt)
		{
			DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			TimeSpan toNow = dt.Subtract(dtStart);
			long timeStamp = toNow.Ticks;
			//timeStamp = long.Parse(timeStamp.ToString());
			return timeStamp.ToString();
		}


		// long --> DateTime
		public static DateTime ConvertLongToDateTime(long d)
		{
			DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long lTime = long.Parse(d + "0000");
			TimeSpan toNow = new TimeSpan(lTime);
			DateTime dtResult = dtStart.Add(toNow);
			return dtResult;
		}

	}

	public class ComparerString : IComparer<string>
	{
		public int Compare(string x, string y)
		{
			return string.CompareOrdinal(x, y);
		}
	}

	public class ZLDataModel
    {
		public string appKey { get; set; }

		public string nonce { get; set; }

		public string timeStamp { get; set; }

		public int namespaceId { get; set; }

		public string sendNo { get; set; }

		public long incidentId { get; set; }

		public int status { get; set; }

		public string flowNode { get; set; }

		public string message { get; set; }

		public string signature { get; set; }


		//public List<ZLDataListModel> data { get; set; }
	}

	public class ZLDataListModel
    {
		public long incidentId { get; set; }

		public int status { get; set; }

		public string flowNode { get; set; }

		public string message { get; set; }

	}

}
