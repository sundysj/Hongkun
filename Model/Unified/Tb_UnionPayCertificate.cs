using System;
namespace MobileSoft.Model.Unified
{
	/// <summary>
	/// ʵ����Tb_UnionPayCertificate ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_UnionPayCertificate
	{
		public Tb_UnionPayCertificate()
		{}
		#region Model
		private string _id;
		private string _communityid;
		private string _signcertpath;
		private string _signcertpwd;
		private string _validatecertdir;
		private string _encryptcert;
		private string _merid;
		private string _accno;
		/// <summary>
		/// �������ñ�
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// С��ID
		/// </summary>
		public string CommunityId
		{
			set{ _communityid=value;}
			get{return _communityid;}
		}
		/// <summary>
		/// ����˽Կ·��
		/// </summary>
		public string signCertPath
		{
			set{ _signcertpath=value;}
			get{return _signcertpath;}
		}
		/// <summary>
		/// ����˽Կ����
		/// </summary>
		public string signCertPwd
		{
			set{ _signcertpwd=value;}
			get{return _signcertpwd;}
		}
		/// <summary>
		/// ��ǩĿ¼
		/// </summary>
		public string validateCertDir
		{
			set{ _validatecertdir=value;}
			get{return _validatecertdir;}
		}
		/// <summary>
		/// ���ܹ�Կ֤��·��
		/// </summary>
		public string encryptCert
		{
			set{ _encryptcert=value;}
			get{return _encryptcert;}
		}
		/// <summary>
		/// �̻���
		/// </summary>
		public string merId
		{
			set{ _merid=value;}
			get{return _merid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string accNo
		{
			set{ _accno=value;}
			get{return _accno;}
		}
		#endregion Model

	}
}

