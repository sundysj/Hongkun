using System;
namespace HM.Model.Qm
{
	/// <summary>
	/// ʵ����Tb_Qm_Standard ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Qm_Standard
	{
		public Tb_Qm_Standard()
		{}
		#region Model
		private string _id;
		private string _code;
		private string _suitableitemtypeid;
		private string _professional;
		private string _type;
		private string _typedescription;
		private string _checkstandard;
		private string _checkway;
		private decimal _point;
		private int _iscoerce;
		private int _isuse;
		private int? _isdelete;
		private int _sort;
		/// <summary>
		/// ��׼����Id
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// ������Ŀ����
		/// </summary>
		public string SuitableItemTypeId
		{
			set{ _suitableitemtypeid=value;}
			get{return _suitableitemtypeid;}
		}
		/// <summary>
		/// ����רҵ
		/// </summary>
		public string Professional
		{
			set{ _professional=value;}
			get{return _professional;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public string TypeDescription
		{
			set{ _typedescription=value;}
			get{return _typedescription;}
		}
		/// <summary>
		/// �˲��׼
		/// </summary>
		public string CheckStandard
		{
			set{ _checkstandard=value;}
			get{return _checkstandard;}
		}
		/// <summary>
		/// ��˱�׼
		/// </summary>
		public string CheckWay
		{
			set{ _checkway=value;}
			get{return _checkway;}
		}
		/// <summary>
		/// ��ֵ
		/// </summary>
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		/// <summary>
		/// �Ƿ�ǿ��
		/// </summary>
		public int IsCoerce
		{
			set{ _iscoerce=value;}
			get{return _iscoerce;}
		}
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public int IsUse
		{
			set{ _isuse=value;}
			get{return _isuse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion Model

	}
}

