using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace MobileSoft.Model.DYDZ
{
	/// <summary>
	/// 实体类Tb_HSPR_ParkingHandCarHistory 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_ParkingHandCarHistory
	{
		public Tb_HSPR_ParkingHandCarHistory()
		{}
		#region Model
		private string _businessid;
		private string _commid;
		private string _handid;
		private string _carid;
		private string _carsign;
		private string _cartype;
		private string _facbrands;
		private string _carcolor;
		private string _caremission;
		private bool _isdelete;
		private string _carhostname;
		private string _time;
		[DisplayName("")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string BusinessID
		{
			set{ _businessid=value;}
			get{return _businessid;}
		}
		[DisplayName("")]
		public string CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		[DisplayName("")]
		public string HandID
		{
			set{ _handid=value;}
			get{return _handid;}
		}
		[DisplayName("")]
		public string CarID
		{
			set{ _carid=value;}
			get{return _carid;}
		}
		[DisplayName("")]
		public string CarSign
		{
			set{ _carsign=value;}
			get{return _carsign;}
		}
		[DisplayName("")]
		public string CarType
		{
			set{ _cartype=value;}
			get{return _cartype;}
		}
		[DisplayName("")]
		public string FacBrands
		{
			set{ _facbrands=value;}
			get{return _facbrands;}
		}
		[DisplayName("")]
		public string CarColor
		{
			set{ _carcolor=value;}
			get{return _carcolor;}
		}
		[DisplayName("")]
		public string CarEmission
		{
			set{ _caremission=value;}
			get{return _caremission;}
		}
		[DisplayName("")]
		public bool IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("")]
		public string CarHostName
		{
			set{ _carhostname=value;}
			get{return _carhostname;}
		}
		[DisplayName("")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "必填")]
		public string time
		{
			set{ _time=value;}
			get{return _time;}
		}
		#endregion Model

	}
}

