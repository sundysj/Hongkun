using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Information_AboutUs ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		[DisplayName("��������ID")]
		public long AboutId
		{
			set{ _aboutid=value;}
			get{return _aboutid;}
		}
		[DisplayName("�̼�ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("����")]
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		[DisplayName("������")]
		public string AboutPublisher
		{
			set{ _aboutpublisher=value;}
			get{return _aboutpublisher;}
		}
		[DisplayName("��������")]
		public DateTime? PubulishDate
		{
			set{ _pubulishdate=value;}
			get{return _pubulishdate;}
		}
		[DisplayName("��������")]
		public string AboutContent
		{
			set{ _aboutcontent=value;}
			get{return _aboutcontent;}
		}
		[DisplayName("ͼƬ")]
		public string AboutImage
		{
			set{ _aboutimage=value;}
			get{return _aboutimage;}
		}
		[DisplayName("�Ƿ�ɾ��")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("�̼ҵ�ַ")]
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

