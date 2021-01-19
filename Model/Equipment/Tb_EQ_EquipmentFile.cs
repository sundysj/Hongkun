using System;
namespace HM.Model.Eq
{
	/// <summary>
	/// ʵ����Tb_EQ_EquipmentFile ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_EQ_EquipmentFile
	{
		public Tb_EQ_EquipmentFile()
		{}
		#region Model
		private string _id;
		private string _equiid;
		private string _filename;
		private string _fix;
		private int? _isdelete;
		private string _filepath;
		private string _phonename;
		private DateTime? _phototime;
		private string _photopid;
		/// <summary>
		/// ������Id
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// �����ID
		/// </summary>
		public string EquiId
		{
			set{ _equiid=value;}
			get{return _equiid;}
		}
		/// <summary>
		/// �ļ�����
		/// </summary>
		public string FileName
		{
			set{ _filename=value;}
			get{return _filename;}
		}
		/// <summary>
		/// ��׺��
		/// </summary>
		public string Fix
		{
			set{ _fix=value;}
			get{return _fix;}
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
		/// �ļ�·��
		/// </summary>
		public string FilePath
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string PhoneName
		{
			set{ _phonename=value;}
			get{return _phonename;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime? PhotoTime
		{
			set{ _phototime=value;}
			get{return _phototime;}
		}
		/// <summary>
		/// ��¼�ʺ�
		/// </summary>
		public string PhotoPId
		{
			set{ _photopid=value;}
			get{return _photopid;}
		}
		#endregion Model

	}
}

