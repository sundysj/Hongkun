using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_System_Log ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_Log
	{
		public Tb_System_Log()
		{}
		#region Model
		private string _logcode;
		private string _bussid;
		private string _bussname;
		private string _managercode;
		private string _username;
		private string _locationip;
		private DateTime? _logtime;
		private string _pnodename;
		private string _operatename;
		private string _operateurl;
		private string _memo;
		private int? _isdelete;
		[DisplayName("ϵͳ��־��ID")]
		public string LogCode
		{
			set{ _logcode=value;}
			get{return _logcode;}
		}
		[DisplayName("�̼�ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("�̼�����")]
		public string BussName
		{
			set{ _bussname=value;}
			get{return _bussname;}
		}
		[DisplayName("�����˱���")]
		public string ManagerCode
		{
			set{ _managercode=value;}
			get{return _managercode;}
		}
		[DisplayName("����������")]
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		[DisplayName("IP")]
		public string LocationIP
		{
			set{ _locationip=value;}
			get{return _locationip;}
		}
		[DisplayName("����ʱ��")]
		public DateTime? LogTime
		{
			set{ _logtime=value;}
			get{return _logtime;}
		}
		[DisplayName("�˵�����")]
		public string PNodeName
		{
			set{ _pnodename=value;}
			get{return _pnodename;}
		}
		[DisplayName("����������")]
		public string OperateName
		{
			set{ _operatename=value;}
			get{return _operatename;}
		}
		[DisplayName("����URL")]
		public string OperateURL
		{
			set{ _operateurl=value;}
			get{return _operateurl;}
		}
		[DisplayName("��ע")]
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		[DisplayName("�Ƿ�ɾ��")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

