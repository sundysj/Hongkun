using System;
namespace Mobile.Model.Pm
{
	/// <summary>
	/// ʵ����Tb_Pm_Dictionary ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Pm_Dictionary
	{
		public Tb_Pm_Dictionary()
		{}
		#region Model
		private string _id;
		private string _dtype;
		private string _name;
		private string _code;
		private string _sort;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// �ֵ����
		/// </summary>
		public string DType
		{
			set{ _dtype=value;}
			get{return _dtype;}
		}
		/// <summary>
		/// �ֵ�����
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// �ֵ����
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// ���
		/// </summary>
		public string Sort
		{
			set{ _sort=value;}
			get{return _sort;}
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

