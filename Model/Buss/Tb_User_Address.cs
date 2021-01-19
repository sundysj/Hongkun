using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_User_Address ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        [DisplayName("�û�������ʷ")]
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		[DisplayName("�̼�ID")]
		public long BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("�û�ID")]
		public string UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		public string UserName { get; set; }

		[DisplayName("���͵�ַ")]
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		[DisplayName("�绰")]
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}

        [DisplayName("����޸�����")]
		public DateTime? UpdataTime
        {
            set { _updatatime = value; }
            get { return _updatatime; }
        }

        [DisplayName("�Ƿ�Ĭ��")]
        public int? IsDefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
        }
        #endregion Model

    }
}

