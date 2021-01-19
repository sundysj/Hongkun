using System;
namespace MobileSoft.Model.OAPublicWork
{
	/// <summary>
	/// 实体类Tb_OAPublicWork_WorkRecord 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_OAPublicWork_WorkRecord
	{
		public Tb_OAPublicWork_WorkRecord()
		{}
		#region Model
		private int _infoid;
		private string _usercode;
		private string _nowrecord;
		private string _afterrecord;
		private DateTime? _writedate;
		private string _ex1;
		private string _ex2;
		private string _ex3;
		private string _ex4;
		private string _ex5;
		private string _ex6;
		/// <summary>
		/// 
		/// </summary>
		public int InfoId
		{
			set{ _infoid=value;}
			get{return _infoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NowRecord
		{
			set{ _nowrecord=value;}
			get{return _nowrecord;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AfterRecord
		{
			set{ _afterrecord=value;}
			get{return _afterrecord;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WriteDate
		{
			set{ _writedate=value;}
			get{return _writedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ex1
		{
			set{ _ex1=value;}
			get{return _ex1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ex2
		{
			set{ _ex2=value;}
			get{return _ex2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ex3
		{
			set{ _ex3=value;}
			get{return _ex3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ex4
		{
			set{ _ex4=value;}
			get{return _ex4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ex5
		{
			set{ _ex5=value;}
			get{return _ex5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ex6
		{
			set{ _ex6=value;}
			get{return _ex6;}
		}
		#endregion Model

	}
}

