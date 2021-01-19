using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// ʵ����Tb_Qm_ItemType ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ��Ŀ����ID
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ��Ŀ���
		/// </summary>
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// ��Ŀ����ID
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

