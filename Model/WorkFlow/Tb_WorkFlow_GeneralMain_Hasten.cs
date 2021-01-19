using System;
namespace MobileSoft.Model.WorkFlow
{
	/// <summary>
	/// 实体类Tb_WorkFlow_GeneralMain_Hasten 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_WorkFlow_GeneralMain_Hasten
	{
		public Tb_WorkFlow_GeneralMain_Hasten()
		{}
		#region Model
		private Guid _generalmaincode;
		private string _title;
		private string _fqr;
		private string _cbr;
		private string _bcbbm;
		private string _bcbr;
		private DateTime? _fqsj;
		private string _zxblr;
		private DateTime? _zxblsj;
		private int _cutid;
		/// <summary>
		/// 
		/// </summary>
		public Guid GeneralMainCode
		{
			set{ _generalmaincode=value;}
			get{return _generalmaincode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FQR
		{
			set{ _fqr=value;}
			get{return _fqr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CBR
		{
			set{ _cbr=value;}
			get{return _cbr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BCBBM
		{
			set{ _bcbbm=value;}
			get{return _bcbbm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BCBR
		{
			set{ _bcbr=value;}
			get{return _bcbr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? FQSJ
		{
			set{ _fqsj=value;}
			get{return _fqsj;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ZXBLR
		{
			set{ _zxblr=value;}
			get{return _zxblr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ZXBLSJ
		{
			set{ _zxblsj=value;}
			get{return _zxblsj;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CutID
		{
			set{ _cutid=value;}
			get{return _cutid;}
		}
		#endregion Model

	}
}

