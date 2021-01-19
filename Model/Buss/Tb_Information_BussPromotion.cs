using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Information_BussPromotion 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Information_BussPromotion
	{
		public Tb_Information_BussPromotion()
		{}
		#region Model
		private long _proid;
		private string _bussid;
		private string _title;
		private string _publisher;
		private DateTime? _publishdate;
		private string _reason;
		private string _proimage;
		private int? _isdelete;
		private int? _sort;
		private string _msgcontent;
		private string _proimageurl;
		private string _remark;
		[DisplayName("商家促销信息ID")]
		public long ProID
		{
			set{ _proid=value;}
			get{return _proid;}
		}
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("促销标题")]
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		[DisplayName("发布人")]
		public string Publisher
		{
			set{ _publisher=value;}
			get{return _publisher;}
		}
		[DisplayName("发布日期")]
		public DateTime? PublishDate
		{
			set{ _publishdate=value;}
			get{return _publishdate;}
		}
		[DisplayName("原因")]
		public string Reason
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		[DisplayName("图片")]
		public string ProImage
		{
			set{ _proimage=value;}
			get{return _proimage;}
		}
		[DisplayName("是否删除")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("排列序号")]
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		[DisplayName("内容")]
		public string MsgContent
		{
			set{ _msgcontent=value;}
			get{return _msgcontent;}
		}
		[DisplayName("图片转跳路径")]
		public string ProImageUrl
		{
			set{ _proimageurl=value;}
			get{return _proimageurl;}
		}
		[DisplayName("备注")]
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

