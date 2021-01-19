using System;
namespace MobileSoft.Model.Information
{
	/// <summary>
	/// 实体类Tb_Information_WebEditor 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Information_WebEditor
	{
		public Tb_Information_WebEditor()
		{}
		#region Model
		private long _id;
		private long _bussid;
		private string _webcontent;
		private string _image1;
		private string _image2;
		private string _image3;
		/// <summary>
		/// 
		/// </summary>
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WebContent
		{
			set{ _webcontent=value;}
			get{return _webcontent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Image1
		{
			set{ _image1=value;}
			get{return _image1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Image2
		{
			set{ _image2=value;}
			get{return _image2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Image3
		{
			set{ _image3=value;}
			get{return _image3;}
		}
		#endregion Model

	}
}

