using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Resources_Type ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		[DisplayName("��Դ����ID")]
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
		[DisplayName("��Դ�������")]
		public string ResourcesTypeName
		{
			set{ _resourcestypename=value;}
			get{return _resourcestypename;}
		}
		[DisplayName("��Դ���ͼƬ")]
		public string ResourcesTypeImgUrl
		{
			set{ _resourcestypeimgurl=value;}
			get{return _resourcestypeimgurl;}
		}
		[DisplayName("��Դ������")]
		public int? ResourcesTypeIndex
		{
			set{ _resourcestypeindex=value;}
			get{return _resourcestypeindex;}
		}
		[DisplayName("��ע")]
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		[DisplayName("�Ƿ�ɾ��")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		#endregion Model

	}
}

