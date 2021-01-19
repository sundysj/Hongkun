using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_System_Log 。(属性说明自动提取数据库字段的描述信息)
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
		[DisplayName("系统日志表ID")]
		public string LogCode
		{
			set{ _logcode=value;}
			get{return _logcode;}
		}
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("商家名称")]
		public string BussName
		{
			set{ _bussname=value;}
			get{return _bussname;}
		}
		[DisplayName("操作人编码")]
		public string ManagerCode
		{
			set{ _managercode=value;}
			get{return _managercode;}
		}
		[DisplayName("操作人姓名")]
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
		[DisplayName("操作时间")]
		public DateTime? LogTime
		{
			set{ _logtime=value;}
			get{return _logtime;}
		}
		[DisplayName("菜单名称")]
		public string PNodeName
		{
			set{ _pnodename=value;}
			get{return _pnodename;}
		}
		[DisplayName("操作人姓名")]
		public string OperateName
		{
			set{ _operatename=value;}
			get{return _operatename;}
		}
		[DisplayName("操作URL")]
		public string OperateURL
		{
			set{ _operateurl=value;}
			get{return _operateurl;}
		}
		[DisplayName("备注")]
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		[DisplayName("是否删除")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

