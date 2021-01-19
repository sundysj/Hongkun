using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Model.Model.Buss
{
	/// <summary>
	/// 实体类Tb_System_BusinessCorp 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_System_BusinessCorp
	{
		public Tb_System_BusinessCorp()
		{}
		#region Model
		private string _bussid;
		private string _regbigtype;
		private string _regsmalltype;
		private string _bussname;
		private string _busscode;
		private string _busstype;
		private string _bussnature;
		private string _bussshortname;
		private string _province;
		private string _city;
		private string _area;
		private string _borough;
		private string _logincode;
		private string _loginpasswd;
		private string _bussaddress;
		private string _busszipcode;
		private string _busslinkman;
		private string _bussmobiletel;
		private string _bussworkedtel;
		private string _bussemail;
		private string _busswebname;
		private string _bussqq;
		private string _bussweixin;
		private string _logoimgurl;
		private string _mapimgurl;
		private string _sysdir;
		private string _sysversion;
		private DateTime? _regdate;
		private int? _isrecommend;
		private string _recommendindex;
		private string _recommendtitle;
		private string _recommendcontent;
		private string _corporatename;
		private string _legalrepresentative;
		private string _legalrepresentativeid;
		private string _businesslicenseid;
		private string _organizationallnstitutionid;
		private string _raxregistrationid;
		private string _managementid;
		private string _longitudelatitude;
		private string _imglogo1;
		private string _imglogo2;
		private string _imglogo3;
		private string _imglogo4;
		private string _imglogo5;
		private string _imglogo6;
		private string _imglogo7;
		private string _imglogo8;
		private string _imglogo9;
		private string _imglogo10;
		private int? _isdelete;
		private string _imglogotitle1;
		private string _imglogotitle2;
		private string _imglogotitle3;
		private string _imglogotitle4;
		private string _imglogotitle5;
		private string _imglogourl1;
		private string _imglogourl2;
		private string _imglogourl3;
		private string _imglogourl4;
		private string _imglogourl5;
		private string _logoimgaddress;
		[DisplayName("商家ID")]
		public string BussId
		{
			set{ _bussid=value;}
			get{return _bussid;}
		}
		[DisplayName("注册大类")]
		public string RegBigType
		{
			set{ _regbigtype=value;}
			get{return _regbigtype;}
		}
		[DisplayName("注册小类")]
		public string RegSmallType
		{
			set{ _regsmalltype=value;}
			get{return _regsmalltype;}
		}
		[DisplayName("商家名称")]
		public string BussName
		{
			set{ _bussname=value;}
			get{return _bussname;}
		}
		[DisplayName("商家编号")]
		public string BussCode
		{
			set{ _busscode=value;}
			get{return _busscode;}
		}
		[DisplayName("商家类别（单位/个人）")]
		public string BussType
		{
			set{ _busstype=value;}
			get{return _busstype;}
		}
		[DisplayName("商家性质（商家性质：平台商城、物管商城、周边商家）")]
		public string BussNature
		{
			set{ _bussnature=value;}
			get{return _bussnature;}
		}
		[DisplayName("商家名称简称")]
		public string BussShortName
		{
			set{ _bussshortname=value;}
			get{return _bussshortname;}
		}
		[DisplayName("省")]
		public string Province
		{
			set{ _province=value;}
			get{return _province;}
		}
		[DisplayName("市")]
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		[DisplayName("区")]
		public string Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		[DisplayName("街道")]
		public string Borough
		{
			set{ _borough=value;}
			get{return _borough;}
		}
		[DisplayName("登录账号")]
		public string LoginCode
		{
			set{ _logincode=value;}
			get{return _logincode;}
		}
		[DisplayName("登录密码")]
		public string LoginPassWD
		{
			set{ _loginpasswd=value;}
			get{return _loginpasswd;}
		}
		[DisplayName("商家地址")]
		public string BussAddress
		{
			set{ _bussaddress=value;}
			get{return _bussaddress;}
		}
		[DisplayName("邮编")]
		public string BussZipCode
		{
			set{ _busszipcode=value;}
			get{return _busszipcode;}
		}
		[DisplayName("联系人")]
		public string BussLinkMan
		{
			set{ _busslinkman=value;}
			get{return _busslinkman;}
		}
		[DisplayName("移动电话")]
		public string BussMobileTel
		{
			set{ _bussmobiletel=value;}
			get{return _bussmobiletel;}
		}
		[DisplayName("办公电话")]
		public string BussWorkedTel
		{
			set{ _bussworkedtel=value;}
			get{return _bussworkedtel;}
		}
		[DisplayName("邮件")]
		public string BussEmail
		{
			set{ _bussemail=value;}
			get{return _bussemail;}
		}
		[DisplayName("网址")]
		public string BussWebName
		{
			set{ _busswebname=value;}
			get{return _busswebname;}
		}
		[DisplayName("QQ")]
		public string BussQQ
		{
			set{ _bussqq=value;}
			get{return _bussqq;}
		}
		[DisplayName("微信")]
		public string BussWeiXin
		{
			set{ _bussweixin=value;}
			get{return _bussweixin;}
		}
		[DisplayName("商家LOGO")]
		public string LogoImgUrl
		{
			set{ _logoimgurl=value;}
			get{return _logoimgurl;}
		}
		[DisplayName("商家百度地图图片")]
		public string MapImgUrl
		{
			set{ _mapimgurl=value;}
			get{return _mapimgurl;}
		}
		[DisplayName("商家系统版本号")]
		public string SysDir
		{
			set{ _sysdir=value;}
			get{return _sysdir;}
		}
		[DisplayName("商家系统版本")]
		public string SysVersion
		{
			set{ _sysversion=value;}
			get{return _sysversion;}
		}
		[DisplayName("注册日期")]
		public DateTime? RegDate
		{
			set{ _regdate=value;}
			get{return _regdate;}
		}
		[DisplayName("是否推荐商家")]
		public int? IsRecommend
		{
			set{ _isrecommend=value;}
			get{return _isrecommend;}
		}
		[DisplayName("推荐序号")]
		public string RecommendIndex
		{
			set{ _recommendindex=value;}
			get{return _recommendindex;}
		}
		[DisplayName("推荐标题")]
		public string RecommendTitle
		{
			set{ _recommendtitle=value;}
			get{return _recommendtitle;}
		}
		[DisplayName("推荐内容")]
		public string RecommendContent
		{
			set{ _recommendcontent=value;}
			get{return _recommendcontent;}
		}
		[DisplayName("公司名称")]
		public string CorporateName
		{
			set{ _corporatename=value;}
			get{return _corporatename;}
		}
		[DisplayName("法定代表人")]
		public string LegalRepresentative
		{
			set{ _legalrepresentative=value;}
			get{return _legalrepresentative;}
		}
		[DisplayName("法定代表人身份证")]
		public string LegalRepresentativeID
		{
			set{ _legalrepresentativeid=value;}
			get{return _legalrepresentativeid;}
		}
		[DisplayName("营业执照注册号")]
		public string BusinessLicenseID
		{
			set{ _businesslicenseid=value;}
			get{return _businesslicenseid;}
		}
		[DisplayName("组织机构代码号")]
		public string OrganizationalLnstitutionID
		{
			set{ _organizationallnstitutionid=value;}
			get{return _organizationallnstitutionid;}
		}
		[DisplayName("登记税务号")]
		public string RaxRegistrationID
		{
			set{ _raxregistrationid=value;}
			get{return _raxregistrationid;}
		}
		[DisplayName("经营许可证号")]
		public string ManagementID
		{
			set{ _managementid=value;}
			get{return _managementid;}
		}
		[DisplayName("经度纬度")]
		public string LongitudeLatitude
		{
			set{ _longitudelatitude=value;}
			get{return _longitudelatitude;}
		}
		[DisplayName("店招图片1")]
		public string ImgLogo1
		{
			set{ _imglogo1=value;}
			get{return _imglogo1;}
		}
		[DisplayName("店招图片2")]
		public string ImgLogo2
		{
			set{ _imglogo2=value;}
			get{return _imglogo2;}
		}
		[DisplayName("店招图片3")]
		public string ImgLogo3
		{
			set{ _imglogo3=value;}
			get{return _imglogo3;}
		}
		[DisplayName("店招图片4")]
		public string ImgLogo4
		{
			set{ _imglogo4=value;}
			get{return _imglogo4;}
		}
		[DisplayName("店招图片5")]
		public string ImgLogo5
		{
			set{ _imglogo5=value;}
			get{return _imglogo5;}
		}
		[DisplayName("预留")]
		public string ImgLogo6
		{
			set{ _imglogo6=value;}
			get{return _imglogo6;}
		}
		[DisplayName("预留")]
		public string ImgLogo7
		{
			set{ _imglogo7=value;}
			get{return _imglogo7;}
		}
		[DisplayName("预留")]
		public string ImgLogo8
		{
			set{ _imglogo8=value;}
			get{return _imglogo8;}
		}
		[DisplayName("预留")]
		public string ImgLogo9
		{
			set{ _imglogo9=value;}
			get{return _imglogo9;}
		}
		[DisplayName("预留")]
		public string ImgLogo10
		{
			set{ _imglogo10=value;}
			get{return _imglogo10;}
		}
		[DisplayName("是否删除")]
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		[DisplayName("店招图片1标题")]
		public string ImgLogoTitle1
		{
			set{ _imglogotitle1=value;}
			get{return _imglogotitle1;}
		}
		[DisplayName("店招图片2标题")]
		public string ImgLogoTitle2
		{
			set{ _imglogotitle2=value;}
			get{return _imglogotitle2;}
		}
		[DisplayName("店招图片3标题")]
		public string ImgLogoTitle3
		{
			set{ _imglogotitle3=value;}
			get{return _imglogotitle3;}
		}
		[DisplayName("店招图片4标题")]
		public string ImgLogoTitle4
		{
			set{ _imglogotitle4=value;}
			get{return _imglogotitle4;}
		}
		[DisplayName("店招图片5标题")]
		public string ImgLogoTitle5
		{
			set{ _imglogotitle5=value;}
			get{return _imglogotitle5;}
		}
		[DisplayName("店招图片1连接地址")]
		public string ImgLogoUrl1
		{
			set{ _imglogourl1=value;}
			get{return _imglogourl1;}
		}
		[DisplayName("店招图片2连接地址")]
		public string ImgLogoUrl2
		{
			set{ _imglogourl2=value;}
			get{return _imglogourl2;}
		}
		[DisplayName("店招图片3连接地址")]
		public string ImgLogoUrl3
		{
			set{ _imglogourl3=value;}
			get{return _imglogourl3;}
		}
		[DisplayName("店招图片4连接地址")]
		public string ImgLogoUrl4
		{
			set{ _imglogourl4=value;}
			get{return _imglogourl4;}
		}
		[DisplayName("店招图片5连接地址")]
		public string ImgLogoUrl5
		{
			set{ _imglogourl5=value;}
			get{return _imglogourl5;}
		}
		[DisplayName("Logo连接地址")]
		public string LogoImgAddress
		{
			set{ _logoimgaddress=value;}
			get{return _logoimgaddress;}
		}
		#endregion Model

	}
}

