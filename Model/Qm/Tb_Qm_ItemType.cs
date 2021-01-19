using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// 实体类Tb_Qm_ItemType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Qm_ItemType
	{
		public Tb_Qm_ItemType()
		{}
		#region Model
		private string _id;
		private string _commid;
		private string _itemtypeid;
		private int? _isdelete;
		/// <summary>
		/// 项目类型ID
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 项目编号
		/// </summary>
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 项目类型ID
		/// </summary>
		public string ItemTypeId
		{
			set{ _itemtypeid=value;}
			get{return _itemtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

