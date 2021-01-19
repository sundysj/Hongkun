using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// ʵ����Tb_Resources_PropertyRelation ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Tb_Resources_PropertyRelation
	{
		public Tb_Resources_PropertyRelation()
		{}
		#region Model
		private string _id;
		private long _bussid;
		private string _resourcesid;
		private string _propertyid;
		[DisplayName("��Ʒ�����б�ID")]
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
		[DisplayName("��ƷID")]
		public string ResourcesID
		{
			set{ _resourcesid=value;}
			get{return _resourcesid;}
		}
		[DisplayName("����ID")]
		public string PropertyId
		{
			set{ _propertyid=value;}
			get{return _propertyid;}
		}
		#endregion Model

	}
}

