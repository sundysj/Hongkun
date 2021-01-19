using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_Information_Activities 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Information_Activities
	{
		public Tb_Information_Activities()
		{}
		#region Model
		private long _actid;
		private string _bussid;
		private string _title;
		private string _actpublisher;
		private DateTime? _publishdate;
		private string _actcontent;
		private string _actimage;
		private int? _isdelete;
		[DisplayName("商家活动ID")]
		public long ActId
		{
			set{ _actid=value;}
			get{return _actid;}
		}
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("标题")]
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		[DisplayName("发布人")]
		public string ActPublisher
		{
			set{ _actpublisher=value;}
			get{return _actpublisher;}
		}
		[DisplayName("发布日期")]
		public DateTime? PublishDate
		{
			set{ _publishdate=value;}
			get{return _publishdate;}
		}
		[DisplayName("发布内容")]
		public string ActContent
		{
			set{ _actcontent=value;}
			get{return _actcontent;}
		}
		[DisplayName("图片")]
		public string ActImage
		{
			set{ _actimage=value;}
			get{return _actimage;}
		}
		[DisplayName("")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

