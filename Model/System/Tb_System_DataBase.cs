using System;
namespace MobileSoft.Model.System
{
	/// <summary>
	/// 实体类Tb_System_DataBase 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_DataBase
	{
		public Tb_System_DataBase()
		{}
		#region Model
		private int _dbid;
		private int? _provinceid;
		private int? _cityid;
		private int? _boroughid;
		private int? _streetid;
		private string _dbserver;
		private string _dbname;
		private string _dbuser;
		private string _dbpwd;
		/// <summary>
		/// 
		/// </summary>
		public int DBID
		{
			set{ _dbid=value;}
			get{return _dbid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProvinceID
		{
			set{ _provinceid=value;}
			get{return _provinceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CityID
		{
			set{ _cityid=value;}
			get{return _cityid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BoroughID
		{
			set{ _boroughid=value;}
			get{return _boroughid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StreetID
		{
			set{ _streetid=value;}
			get{return _streetid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DBServer
		{
			set{ _dbserver=value;}
			get{return _dbserver;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DBName
		{
			set{ _dbname=value;}
			get{return _dbname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DBUser
		{
			set{ _dbuser=value;}
			get{return _dbuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DBPwd
		{
			set{ _dbpwd=value;}
			get{return _dbpwd;}
		}
		#endregion Model

	}
}

