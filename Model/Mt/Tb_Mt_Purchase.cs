using System;
namespace Model.Mt
{
	/// <summary>
	/// ʵ����Tb_Mt_Purchase ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// �ɹ����뵥
        /// </summary>
        public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// �깺�ֿ�(���ڵ���ʱֱ��ɸѡ)
		/// </summary>
		public string PurchaseWarehouseId
		{
			set{ _purchasewarehouseid=value;}
			get{return _purchasewarehouseid;}
		}
		/// <summary>
		/// ���ֿ�
		/// </summary>
		public string InWareHouseId
		{
			set{ _inwarehouseid=value;}
			get{return _inwarehouseid;}
		}
		/// <summary>
		/// �깺���� �ƻ��ɹ�/����/����/ר��,���ƻ���ֱ��������
		/// </summary>
		public string PurchaseType
		{
			set{ _purchasetype=value;}
			get{return _purchasetype;}
		}
		/// <summary>
		/// �깺����
		/// </summary>
		public string PurchaseNum
		{
			set{ _purchasenum=value;}
			get{return _purchasenum;}
		}
		/// <summary>
		/// �ƻ�����
		/// </summary>
		public string PlanDate
		{
			set{ _plandate=value;}
			get{return _plandate;}
		}
		/// <summary>
		/// �깺����
		/// </summary>
		public DateTime PurchaseDate
		{
			set{ _purchasedate=value;}
			get{return _purchasedate;}
		}
		/// <summary>
		/// �깺����
		/// </summary>
		public string DepCode
		{
			set{ _depcode=value;}
			get{return _depcode;}
		}
		/// <summary>
		/// �Ƶ���
		/// </summary>
		public string Originator
		{
			set{ _originator=value;}
			get{return _originator;}
		}
		/// <summary>
		/// �깺��ע
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// �깺��״̬
		/// </summary>
		public string State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// �����˱���
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
        /// �����б�
        /// </summary>
        public string AttachFile
        {
            set { _attachfile = value; }
            get { return _attachfile; }
        }

        #endregion Model

    }
}

