using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Resources_Type 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Resources_Type
	{
		public Tb_Resources_Type()
		{}
		#region Model
		private string _resourcestypeid;
		private string _resourcessortcode;
		private string _resourcestypename;
		private string _resourcestypeimgurl;
		private int? _resourcestypeindex;
		private string _remark;
		private int? _isdelete;
		[DisplayName("资源类别表ID")]
		public string ResourcesTypeID
		{
			set{ _resourcestypeid=value;}
			get{return _resourcestypeid;}
		}
		[DisplayName("")]
		public string ResourcesSortCode
		{
			set{ _resourcessortcode=value;}
			get{return _resourcessortcode;}
		}
		[DisplayName("资源类别名称")]
		public string ResourcesTypeName
		{
			set{ _resourcestypename=value;}
			get{return _resourcestypename;}
		}
		[DisplayName("资源类别图片")]
		public string ResourcesTypeImgUrl
		{
			set{ _resourcestypeimgurl=value;}
			get{return _resourcestypeimgurl;}
		}
		[DisplayName("资源类别序号")]
		public int? ResourcesTypeIndex
		{
			set{ _resourcestypeindex=value;}
			get{return _resourcestypeindex;}
		}
		[DisplayName("备注")]
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		[DisplayName("是否删除")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

