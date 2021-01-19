using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_System_CorpConfig ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_System_CorpConfig
	{
		public Tb_System_CorpConfig()
		{}
		#region Model
		private string _id;
		private int? _corpid;
		private string _servicetype;
		private string _sort;
		private int? _isdelete;
		[DisplayName("")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("��ҵCorp")]
		public int? CorpId
		{
			set{ _corpid=value;}
			get{return _corpid;}
		}
		[DisplayName("����ҵ̬")]
		public string ServiceType
		{
			set{ _servicetype=value;}
			get{return _servicetype;}
		}
		[DisplayName("")]
		public string Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		[DisplayName("")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

