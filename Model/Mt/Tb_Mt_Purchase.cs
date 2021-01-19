using System;
namespace Model.Mt
{
	/// <summary>
	/// 实体类Tb_Mt_Purchase 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_Mt_Purchase
	{
		public Tb_Mt_Purchase()
		{}
		#region Model
		private string _id;
		private string _purchasewarehouseid;
		private string _inwarehouseid;
		private string _purchasetype;
		private string _purchasenum;
		private string _plandate;
		private DateTime _purchasedate;
		private string _depcode;
		private string _originator;
		private string _memo;
		private string _state;
		private string _purchaseusercode;
		private int? _isdelete;
        private string _attachfile;
        /// <summary>
        /// 采购申请单
        /// </summary>
        public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 申购仓库(便于调拨时直接筛选)
		/// </summary>
		public string PurchaseWarehouseId
		{
			set{ _purchasewarehouseid=value;}
			get{return _purchasewarehouseid;}
		}
		/// <summary>
		/// 入库仓库
		/// </summary>
		public string InWareHouseId
		{
			set{ _inwarehouseid=value;}
			get{return _inwarehouseid;}
		}
		/// <summary>
		/// 申购类型 计划采购/零星/紧急/专项,除计划外直接走审批
		/// </summary>
		public string PurchaseType
		{
			set{ _purchasetype=value;}
			get{return _purchasetype;}
		}
		/// <summary>
		/// 申购单号
		/// </summary>
		public string PurchaseNum
		{
			set{ _purchasenum=value;}
			get{return _purchasenum;}
		}
		/// <summary>
		/// 计划年月
		/// </summary>
		public string PlanDate
		{
			set{ _plandate=value;}
			get{return _plandate;}
		}
		/// <summary>
		/// 申购日期
		/// </summary>
		public DateTime PurchaseDate
		{
			set{ _purchasedate=value;}
			get{return _purchasedate;}
		}
		/// <summary>
		/// 申购部门
		/// </summary>
		public string DepCode
		{
			set{ _depcode=value;}
			get{return _depcode;}
		}
		/// <summary>
		/// 制单人
		/// </summary>
		public string Originator
		{
			set{ _originator=value;}
			get{return _originator;}
		}
		/// <summary>
		/// 申购备注
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 申购单状态
		/// </summary>
		public string State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 操作人编码
		/// </summary>
		public string PurchaseUserCode
		{
			set{ _purchaseusercode=value;}
			get{return _purchaseusercode;}
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
        /// 附件列表
        /// </summary>
        public string AttachFile
        {
            set { _attachfile = value; }
            get { return _attachfile; }
        }

        #endregion Model

    }
}

