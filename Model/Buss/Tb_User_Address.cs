using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_User_Address 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_User_Address
	{
		public Tb_User_Address()
		{}
		#region Model
		private string _id;
		private long _bussid;
		private string _userid;
		private string _address;
		private string _mobile;
        private DateTime? _updatatime;
        private int? _isdefault;
        [DisplayName("用户配送历史")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("商家ID")]
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("用户ID")]
		public string UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		public string UserName { get; set; }

		[DisplayName("配送地址")]
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		[DisplayName("电话")]
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}

        [DisplayName("最后修改日期")]
		public DateTime? UpdataTime
        {
            set { _updatatime = value; }
            get { return _updatatime; }
        }

        [DisplayName("是否默认")]
        public int? IsDefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
        }
        #endregion Model

    }
}

