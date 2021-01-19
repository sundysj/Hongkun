using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// ʵ����Tb_HSPR_CommFriend ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_CommFriend
	{
		public Tb_HSPR_CommFriend()
		{}
		#region Model
		private string _friendcode;
		private string _custid;
		private string _friendcustid;
		private string _roomsign;
		private int? _isdelete;
		/// <summary>
		/// 
		/// </summary>
		public string FriendCode
		{
			set{ _friendcode=value;}
			get{return _friendcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustId
		{
			set{ _custid=value;}
			get{return _custid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FriendCustId
		{
			set{ _friendcustid=value;}
			get{return _friendcustid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoomSign
		{
			set{ _roomsign=value;}
			get{return _roomsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

