using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Information_AboutUs 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Information_AboutUs
	{
		public Tb_Information_AboutUs()
		{}
		#region Model
		private long _aboutid;
		private string _bussid;
		private string _title;
		private string _aboutpublisher;
		private DateTime? _pubulishdate;
		private string _aboutcontent;
		private string _aboutimage;
		private int? _isdelete;
		private string _bussaddress;
		private string _busszipcode;
		private string _busslinkman;
		private string _bussmobiletel;
		private string _bussemail;
		private string _bussworkedtel;
		private string _busswebname;
		private string _bussqq;
		private string _bussweixin;
		private string _bussmappoint;
		private string _bussname;
		[DisplayName("关于我们ID")]
		public long AboutId
		{
			set{ _aboutid=value;}
			get{return _aboutid;}
		}
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("标题")]
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		[DisplayName("发布人")]
		public string AboutPublisher
		{
			set{ _aboutpublisher=value;}
			get{return _aboutpublisher;}
		}
		[DisplayName("发布日期")]
		public DateTime? PubulishDate
		{
			set{ _pubulishdate=value;}
			get{return _pubulishdate;}
		}
		[DisplayName("发布内容")]
		public string AboutContent
		{
			set{ _aboutcontent=value;}
			get{return _aboutcontent;}
		}
		[DisplayName("图片")]
		public string AboutImage
		{
			set{ _aboutimage=value;}
			get{return _aboutimage;}
		}
		[DisplayName("是否删除")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("商家地址")]
		public string BussAddress
		{
			set{ _bussaddress=value;}
			get{return _bussaddress;}
		}
		[DisplayName("")]
		public string BussZipCode
		{
			set{ _busszipcode=value;}
			get{return _busszipcode;}
		}
		[DisplayName("")]
		public string BussLinkMan
		{
			set{ _busslinkman=value;}
			get{return _busslinkman;}
		}
		[DisplayName("")]
		public string BussMobileTel
		{
			set{ _bussmobiletel=value;}
			get{return _bussmobiletel;}
		}
		[DisplayName("")]
		public string BussEmail
		{
			set{ _bussemail=value;}
			get{return _bussemail;}
		}
		[DisplayName("")]
		public string BussWorkedTel
		{
			set{ _bussworkedtel=value;}
			get{return _bussworkedtel;}
		}
		[DisplayName("")]
		public string BussWebName
		{
			set{ _busswebname=value;}
			get{return _busswebname;}
		}
		[DisplayName("")]
		public string BussQQ
		{
			set{ _bussqq=value;}
			get{return _bussqq;}
		}
		[DisplayName("")]
		public string BussWeiXin
		{
			set{ _bussweixin=value;}
			get{return _bussweixin;}
		}
		[DisplayName("")]
		public string BussMapPoint
		{
			set{ _bussmappoint=value;}
			get{return _bussmappoint;}
		}
		[DisplayName("")]
		public string BussName
		{
			set{ _bussname=value;}
			get{return _bussname;}
		}
		#endregion Model

	}
}

