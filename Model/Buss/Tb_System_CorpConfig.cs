using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_System_CorpConfig 。(属性说明自动提取数据库字段的描述信息)
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
		[DisplayName("物业Corp")]
		public int? CorpId
		{
			set{ _corpid=value;}
			get{return _corpid;}
		}
		[DisplayName("服务业态")]
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

