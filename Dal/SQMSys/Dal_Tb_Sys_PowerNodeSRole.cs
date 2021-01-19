using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_PowerNodeSRole。
	/// </summary>
	public class Dal_Tb_Sys_PowerNodeSRole
	{
		public Dal_Tb_Sys_PowerNodeSRole()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long IID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSRole_Exists",parameters,out rowsAffected);
			if(result==1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		///  增加一条数据
		/// </summary>
		public int Add(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@OrganName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsComp", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CkAll", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck1", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck2", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck3", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck4", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck5", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck6", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck7", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck8", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck9", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck10", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck11", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck12", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck13", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck14", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck15", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck16", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck17", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck18", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck19", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck20", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck21", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck22", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck23", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck24", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck25", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck26", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck27", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck28", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck29", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck30", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck31", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck32", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck33", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck34", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck35", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck36", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck37", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck38", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck39", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck40", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck41", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck42", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck43", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck44", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck45", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck46", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck47", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck48", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck49", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck50", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck51", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck52", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck53", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck54", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck55", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck56", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck57", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck58", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck59", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck60", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck61", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck62", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck63", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck64", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck65", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck66", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck67", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck68", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck69", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck70", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck71", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck72", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck73", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck74", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck75", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck76", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck77", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck78", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck79", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck80", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck81", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck82", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck83", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck84", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck85", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck86", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck87", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck88", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck89", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck90", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck91", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck92", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck93", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck94", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck95", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck96", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck97", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck98", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck99", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck100", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck101", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck102", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck103", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck104", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck105", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck106", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck107", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck108", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck109", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck110", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck111", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck112", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck113", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck114", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck115", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck116", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck117", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck118", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck119", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck120", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck121", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck122", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck123", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck124", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck125", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck126", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck127", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck128", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck129", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck130", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck131", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck132", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck133", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck134", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck135", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck136", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck137", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck138", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck139", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck140", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck141", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck142", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck143", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck144", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck145", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck146", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck147", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck148", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck149", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck150", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck151", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck152", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck153", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck154", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck155", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck156", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck157", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck158", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck159", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck160", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck161", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck162", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck163", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck164", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck165", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck166", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck167", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck168", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck169", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck170", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck171", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck172", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck173", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck174", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck175", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck176", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck177", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck178", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck179", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck180", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck181", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck182", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck183", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck184", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck185", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck186", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck187", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck188", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck189", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck190", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck191", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck192", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck193", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck194", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck195", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck196", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck197", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck198", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck199", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck200", SqlDbType.SmallInt,2)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.OrganCode;
			parameters[3].Value = model.OrganName;
			parameters[4].Value = model.IsComp;
			parameters[5].Value = model.CommID;
			parameters[6].Value = model.CommCode;
			parameters[7].Value = model.CkAll;
			parameters[8].Value = model.Ck1;
			parameters[9].Value = model.Ck2;
			parameters[10].Value = model.Ck3;
			parameters[11].Value = model.Ck4;
			parameters[12].Value = model.Ck5;
			parameters[13].Value = model.Ck6;
			parameters[14].Value = model.Ck7;
			parameters[15].Value = model.Ck8;
			parameters[16].Value = model.Ck9;
			parameters[17].Value = model.Ck10;
			parameters[18].Value = model.Ck11;
			parameters[19].Value = model.Ck12;
			parameters[20].Value = model.Ck13;
			parameters[21].Value = model.Ck14;
			parameters[22].Value = model.Ck15;
			parameters[23].Value = model.Ck16;
			parameters[24].Value = model.Ck17;
			parameters[25].Value = model.Ck18;
			parameters[26].Value = model.Ck19;
			parameters[27].Value = model.Ck20;
			parameters[28].Value = model.Ck21;
			parameters[29].Value = model.Ck22;
			parameters[30].Value = model.Ck23;
			parameters[31].Value = model.Ck24;
			parameters[32].Value = model.Ck25;
			parameters[33].Value = model.Ck26;
			parameters[34].Value = model.Ck27;
			parameters[35].Value = model.Ck28;
			parameters[36].Value = model.Ck29;
			parameters[37].Value = model.Ck30;
			parameters[38].Value = model.Ck31;
			parameters[39].Value = model.Ck32;
			parameters[40].Value = model.Ck33;
			parameters[41].Value = model.Ck34;
			parameters[42].Value = model.Ck35;
			parameters[43].Value = model.Ck36;
			parameters[44].Value = model.Ck37;
			parameters[45].Value = model.Ck38;
			parameters[46].Value = model.Ck39;
			parameters[47].Value = model.Ck40;
			parameters[48].Value = model.Ck41;
			parameters[49].Value = model.Ck42;
			parameters[50].Value = model.Ck43;
			parameters[51].Value = model.Ck44;
			parameters[52].Value = model.Ck45;
			parameters[53].Value = model.Ck46;
			parameters[54].Value = model.Ck47;
			parameters[55].Value = model.Ck48;
			parameters[56].Value = model.Ck49;
			parameters[57].Value = model.Ck50;
			parameters[58].Value = model.Ck51;
			parameters[59].Value = model.Ck52;
			parameters[60].Value = model.Ck53;
			parameters[61].Value = model.Ck54;
			parameters[62].Value = model.Ck55;
			parameters[63].Value = model.Ck56;
			parameters[64].Value = model.Ck57;
			parameters[65].Value = model.Ck58;
			parameters[66].Value = model.Ck59;
			parameters[67].Value = model.Ck60;
			parameters[68].Value = model.Ck61;
			parameters[69].Value = model.Ck62;
			parameters[70].Value = model.Ck63;
			parameters[71].Value = model.Ck64;
			parameters[72].Value = model.Ck65;
			parameters[73].Value = model.Ck66;
			parameters[74].Value = model.Ck67;
			parameters[75].Value = model.Ck68;
			parameters[76].Value = model.Ck69;
			parameters[77].Value = model.Ck70;
			parameters[78].Value = model.Ck71;
			parameters[79].Value = model.Ck72;
			parameters[80].Value = model.Ck73;
			parameters[81].Value = model.Ck74;
			parameters[82].Value = model.Ck75;
			parameters[83].Value = model.Ck76;
			parameters[84].Value = model.Ck77;
			parameters[85].Value = model.Ck78;
			parameters[86].Value = model.Ck79;
			parameters[87].Value = model.Ck80;
			parameters[88].Value = model.Ck81;
			parameters[89].Value = model.Ck82;
			parameters[90].Value = model.Ck83;
			parameters[91].Value = model.Ck84;
			parameters[92].Value = model.Ck85;
			parameters[93].Value = model.Ck86;
			parameters[94].Value = model.Ck87;
			parameters[95].Value = model.Ck88;
			parameters[96].Value = model.Ck89;
			parameters[97].Value = model.Ck90;
			parameters[98].Value = model.Ck91;
			parameters[99].Value = model.Ck92;
			parameters[100].Value = model.Ck93;
			parameters[101].Value = model.Ck94;
			parameters[102].Value = model.Ck95;
			parameters[103].Value = model.Ck96;
			parameters[104].Value = model.Ck97;
			parameters[105].Value = model.Ck98;
			parameters[106].Value = model.Ck99;
			parameters[107].Value = model.Ck100;
			parameters[108].Value = model.Ck101;
			parameters[109].Value = model.Ck102;
			parameters[110].Value = model.Ck103;
			parameters[111].Value = model.Ck104;
			parameters[112].Value = model.Ck105;
			parameters[113].Value = model.Ck106;
			parameters[114].Value = model.Ck107;
			parameters[115].Value = model.Ck108;
			parameters[116].Value = model.Ck109;
			parameters[117].Value = model.Ck110;
			parameters[118].Value = model.Ck111;
			parameters[119].Value = model.Ck112;
			parameters[120].Value = model.Ck113;
			parameters[121].Value = model.Ck114;
			parameters[122].Value = model.Ck115;
			parameters[123].Value = model.Ck116;
			parameters[124].Value = model.Ck117;
			parameters[125].Value = model.Ck118;
			parameters[126].Value = model.Ck119;
			parameters[127].Value = model.Ck120;
			parameters[128].Value = model.Ck121;
			parameters[129].Value = model.Ck122;
			parameters[130].Value = model.Ck123;
			parameters[131].Value = model.Ck124;
			parameters[132].Value = model.Ck125;
			parameters[133].Value = model.Ck126;
			parameters[134].Value = model.Ck127;
			parameters[135].Value = model.Ck128;
			parameters[136].Value = model.Ck129;
			parameters[137].Value = model.Ck130;
			parameters[138].Value = model.Ck131;
			parameters[139].Value = model.Ck132;
			parameters[140].Value = model.Ck133;
			parameters[141].Value = model.Ck134;
			parameters[142].Value = model.Ck135;
			parameters[143].Value = model.Ck136;
			parameters[144].Value = model.Ck137;
			parameters[145].Value = model.Ck138;
			parameters[146].Value = model.Ck139;
			parameters[147].Value = model.Ck140;
			parameters[148].Value = model.Ck141;
			parameters[149].Value = model.Ck142;
			parameters[150].Value = model.Ck143;
			parameters[151].Value = model.Ck144;
			parameters[152].Value = model.Ck145;
			parameters[153].Value = model.Ck146;
			parameters[154].Value = model.Ck147;
			parameters[155].Value = model.Ck148;
			parameters[156].Value = model.Ck149;
			parameters[157].Value = model.Ck150;
			parameters[158].Value = model.Ck151;
			parameters[159].Value = model.Ck152;
			parameters[160].Value = model.Ck153;
			parameters[161].Value = model.Ck154;
			parameters[162].Value = model.Ck155;
			parameters[163].Value = model.Ck156;
			parameters[164].Value = model.Ck157;
			parameters[165].Value = model.Ck158;
			parameters[166].Value = model.Ck159;
			parameters[167].Value = model.Ck160;
			parameters[168].Value = model.Ck161;
			parameters[169].Value = model.Ck162;
			parameters[170].Value = model.Ck163;
			parameters[171].Value = model.Ck164;
			parameters[172].Value = model.Ck165;
			parameters[173].Value = model.Ck166;
			parameters[174].Value = model.Ck167;
			parameters[175].Value = model.Ck168;
			parameters[176].Value = model.Ck169;
			parameters[177].Value = model.Ck170;
			parameters[178].Value = model.Ck171;
			parameters[179].Value = model.Ck172;
			parameters[180].Value = model.Ck173;
			parameters[181].Value = model.Ck174;
			parameters[182].Value = model.Ck175;
			parameters[183].Value = model.Ck176;
			parameters[184].Value = model.Ck177;
			parameters[185].Value = model.Ck178;
			parameters[186].Value = model.Ck179;
			parameters[187].Value = model.Ck180;
			parameters[188].Value = model.Ck181;
			parameters[189].Value = model.Ck182;
			parameters[190].Value = model.Ck183;
			parameters[191].Value = model.Ck184;
			parameters[192].Value = model.Ck185;
			parameters[193].Value = model.Ck186;
			parameters[194].Value = model.Ck187;
			parameters[195].Value = model.Ck188;
			parameters[196].Value = model.Ck189;
			parameters[197].Value = model.Ck190;
			parameters[198].Value = model.Ck191;
			parameters[199].Value = model.Ck192;
			parameters[200].Value = model.Ck193;
			parameters[201].Value = model.Ck194;
			parameters[202].Value = model.Ck195;
			parameters[203].Value = model.Ck196;
			parameters[204].Value = model.Ck197;
			parameters[205].Value = model.Ck198;
			parameters[206].Value = model.Ck199;
			parameters[207].Value = model.Ck200;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSRole_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt,8),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,20),
					new SqlParameter("@OrganCode", SqlDbType.NVarChar,20),
					new SqlParameter("@OrganName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsComp", SqlDbType.SmallInt,2),
					new SqlParameter("@CommID", SqlDbType.BigInt,8),
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CkAll", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck1", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck2", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck3", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck4", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck5", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck6", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck7", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck8", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck9", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck10", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck11", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck12", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck13", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck14", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck15", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck16", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck17", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck18", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck19", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck20", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck21", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck22", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck23", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck24", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck25", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck26", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck27", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck28", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck29", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck30", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck31", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck32", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck33", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck34", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck35", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck36", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck37", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck38", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck39", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck40", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck41", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck42", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck43", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck44", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck45", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck46", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck47", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck48", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck49", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck50", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck51", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck52", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck53", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck54", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck55", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck56", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck57", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck58", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck59", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck60", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck61", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck62", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck63", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck64", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck65", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck66", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck67", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck68", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck69", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck70", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck71", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck72", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck73", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck74", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck75", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck76", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck77", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck78", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck79", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck80", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck81", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck82", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck83", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck84", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck85", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck86", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck87", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck88", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck89", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck90", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck91", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck92", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck93", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck94", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck95", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck96", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck97", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck98", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck99", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck100", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck101", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck102", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck103", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck104", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck105", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck106", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck107", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck108", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck109", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck110", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck111", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck112", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck113", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck114", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck115", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck116", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck117", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck118", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck119", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck120", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck121", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck122", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck123", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck124", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck125", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck126", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck127", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck128", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck129", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck130", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck131", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck132", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck133", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck134", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck135", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck136", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck137", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck138", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck139", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck140", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck141", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck142", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck143", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck144", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck145", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck146", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck147", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck148", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck149", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck150", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck151", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck152", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck153", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck154", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck155", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck156", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck157", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck158", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck159", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck160", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck161", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck162", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck163", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck164", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck165", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck166", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck167", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck168", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck169", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck170", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck171", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck172", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck173", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck174", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck175", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck176", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck177", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck178", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck179", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck180", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck181", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck182", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck183", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck184", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck185", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck186", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck187", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck188", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck189", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck190", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck191", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck192", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck193", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck194", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck195", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck196", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck197", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck198", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck199", SqlDbType.SmallInt,2),
					new SqlParameter("@Ck200", SqlDbType.SmallInt,2)};
			parameters[0].Value = model.IID;
			parameters[1].Value = model.UserCode;
			parameters[2].Value = model.OrganCode;
			parameters[3].Value = model.OrganName;
			parameters[4].Value = model.IsComp;
			parameters[5].Value = model.CommID;
			parameters[6].Value = model.CommCode;
			parameters[7].Value = model.CkAll;
			parameters[8].Value = model.Ck1;
			parameters[9].Value = model.Ck2;
			parameters[10].Value = model.Ck3;
			parameters[11].Value = model.Ck4;
			parameters[12].Value = model.Ck5;
			parameters[13].Value = model.Ck6;
			parameters[14].Value = model.Ck7;
			parameters[15].Value = model.Ck8;
			parameters[16].Value = model.Ck9;
			parameters[17].Value = model.Ck10;
			parameters[18].Value = model.Ck11;
			parameters[19].Value = model.Ck12;
			parameters[20].Value = model.Ck13;
			parameters[21].Value = model.Ck14;
			parameters[22].Value = model.Ck15;
			parameters[23].Value = model.Ck16;
			parameters[24].Value = model.Ck17;
			parameters[25].Value = model.Ck18;
			parameters[26].Value = model.Ck19;
			parameters[27].Value = model.Ck20;
			parameters[28].Value = model.Ck21;
			parameters[29].Value = model.Ck22;
			parameters[30].Value = model.Ck23;
			parameters[31].Value = model.Ck24;
			parameters[32].Value = model.Ck25;
			parameters[33].Value = model.Ck26;
			parameters[34].Value = model.Ck27;
			parameters[35].Value = model.Ck28;
			parameters[36].Value = model.Ck29;
			parameters[37].Value = model.Ck30;
			parameters[38].Value = model.Ck31;
			parameters[39].Value = model.Ck32;
			parameters[40].Value = model.Ck33;
			parameters[41].Value = model.Ck34;
			parameters[42].Value = model.Ck35;
			parameters[43].Value = model.Ck36;
			parameters[44].Value = model.Ck37;
			parameters[45].Value = model.Ck38;
			parameters[46].Value = model.Ck39;
			parameters[47].Value = model.Ck40;
			parameters[48].Value = model.Ck41;
			parameters[49].Value = model.Ck42;
			parameters[50].Value = model.Ck43;
			parameters[51].Value = model.Ck44;
			parameters[52].Value = model.Ck45;
			parameters[53].Value = model.Ck46;
			parameters[54].Value = model.Ck47;
			parameters[55].Value = model.Ck48;
			parameters[56].Value = model.Ck49;
			parameters[57].Value = model.Ck50;
			parameters[58].Value = model.Ck51;
			parameters[59].Value = model.Ck52;
			parameters[60].Value = model.Ck53;
			parameters[61].Value = model.Ck54;
			parameters[62].Value = model.Ck55;
			parameters[63].Value = model.Ck56;
			parameters[64].Value = model.Ck57;
			parameters[65].Value = model.Ck58;
			parameters[66].Value = model.Ck59;
			parameters[67].Value = model.Ck60;
			parameters[68].Value = model.Ck61;
			parameters[69].Value = model.Ck62;
			parameters[70].Value = model.Ck63;
			parameters[71].Value = model.Ck64;
			parameters[72].Value = model.Ck65;
			parameters[73].Value = model.Ck66;
			parameters[74].Value = model.Ck67;
			parameters[75].Value = model.Ck68;
			parameters[76].Value = model.Ck69;
			parameters[77].Value = model.Ck70;
			parameters[78].Value = model.Ck71;
			parameters[79].Value = model.Ck72;
			parameters[80].Value = model.Ck73;
			parameters[81].Value = model.Ck74;
			parameters[82].Value = model.Ck75;
			parameters[83].Value = model.Ck76;
			parameters[84].Value = model.Ck77;
			parameters[85].Value = model.Ck78;
			parameters[86].Value = model.Ck79;
			parameters[87].Value = model.Ck80;
			parameters[88].Value = model.Ck81;
			parameters[89].Value = model.Ck82;
			parameters[90].Value = model.Ck83;
			parameters[91].Value = model.Ck84;
			parameters[92].Value = model.Ck85;
			parameters[93].Value = model.Ck86;
			parameters[94].Value = model.Ck87;
			parameters[95].Value = model.Ck88;
			parameters[96].Value = model.Ck89;
			parameters[97].Value = model.Ck90;
			parameters[98].Value = model.Ck91;
			parameters[99].Value = model.Ck92;
			parameters[100].Value = model.Ck93;
			parameters[101].Value = model.Ck94;
			parameters[102].Value = model.Ck95;
			parameters[103].Value = model.Ck96;
			parameters[104].Value = model.Ck97;
			parameters[105].Value = model.Ck98;
			parameters[106].Value = model.Ck99;
			parameters[107].Value = model.Ck100;
			parameters[108].Value = model.Ck101;
			parameters[109].Value = model.Ck102;
			parameters[110].Value = model.Ck103;
			parameters[111].Value = model.Ck104;
			parameters[112].Value = model.Ck105;
			parameters[113].Value = model.Ck106;
			parameters[114].Value = model.Ck107;
			parameters[115].Value = model.Ck108;
			parameters[116].Value = model.Ck109;
			parameters[117].Value = model.Ck110;
			parameters[118].Value = model.Ck111;
			parameters[119].Value = model.Ck112;
			parameters[120].Value = model.Ck113;
			parameters[121].Value = model.Ck114;
			parameters[122].Value = model.Ck115;
			parameters[123].Value = model.Ck116;
			parameters[124].Value = model.Ck117;
			parameters[125].Value = model.Ck118;
			parameters[126].Value = model.Ck119;
			parameters[127].Value = model.Ck120;
			parameters[128].Value = model.Ck121;
			parameters[129].Value = model.Ck122;
			parameters[130].Value = model.Ck123;
			parameters[131].Value = model.Ck124;
			parameters[132].Value = model.Ck125;
			parameters[133].Value = model.Ck126;
			parameters[134].Value = model.Ck127;
			parameters[135].Value = model.Ck128;
			parameters[136].Value = model.Ck129;
			parameters[137].Value = model.Ck130;
			parameters[138].Value = model.Ck131;
			parameters[139].Value = model.Ck132;
			parameters[140].Value = model.Ck133;
			parameters[141].Value = model.Ck134;
			parameters[142].Value = model.Ck135;
			parameters[143].Value = model.Ck136;
			parameters[144].Value = model.Ck137;
			parameters[145].Value = model.Ck138;
			parameters[146].Value = model.Ck139;
			parameters[147].Value = model.Ck140;
			parameters[148].Value = model.Ck141;
			parameters[149].Value = model.Ck142;
			parameters[150].Value = model.Ck143;
			parameters[151].Value = model.Ck144;
			parameters[152].Value = model.Ck145;
			parameters[153].Value = model.Ck146;
			parameters[154].Value = model.Ck147;
			parameters[155].Value = model.Ck148;
			parameters[156].Value = model.Ck149;
			parameters[157].Value = model.Ck150;
			parameters[158].Value = model.Ck151;
			parameters[159].Value = model.Ck152;
			parameters[160].Value = model.Ck153;
			parameters[161].Value = model.Ck154;
			parameters[162].Value = model.Ck155;
			parameters[163].Value = model.Ck156;
			parameters[164].Value = model.Ck157;
			parameters[165].Value = model.Ck158;
			parameters[166].Value = model.Ck159;
			parameters[167].Value = model.Ck160;
			parameters[168].Value = model.Ck161;
			parameters[169].Value = model.Ck162;
			parameters[170].Value = model.Ck163;
			parameters[171].Value = model.Ck164;
			parameters[172].Value = model.Ck165;
			parameters[173].Value = model.Ck166;
			parameters[174].Value = model.Ck167;
			parameters[175].Value = model.Ck168;
			parameters[176].Value = model.Ck169;
			parameters[177].Value = model.Ck170;
			parameters[178].Value = model.Ck171;
			parameters[179].Value = model.Ck172;
			parameters[180].Value = model.Ck173;
			parameters[181].Value = model.Ck174;
			parameters[182].Value = model.Ck175;
			parameters[183].Value = model.Ck176;
			parameters[184].Value = model.Ck177;
			parameters[185].Value = model.Ck178;
			parameters[186].Value = model.Ck179;
			parameters[187].Value = model.Ck180;
			parameters[188].Value = model.Ck181;
			parameters[189].Value = model.Ck182;
			parameters[190].Value = model.Ck183;
			parameters[191].Value = model.Ck184;
			parameters[192].Value = model.Ck185;
			parameters[193].Value = model.Ck186;
			parameters[194].Value = model.Ck187;
			parameters[195].Value = model.Ck188;
			parameters[196].Value = model.Ck189;
			parameters[197].Value = model.Ck190;
			parameters[198].Value = model.Ck191;
			parameters[199].Value = model.Ck192;
			parameters[200].Value = model.Ck193;
			parameters[201].Value = model.Ck194;
			parameters[202].Value = model.Ck195;
			parameters[203].Value = model.Ck196;
			parameters[204].Value = model.Ck197;
			parameters[205].Value = model.Ck198;
			parameters[206].Value = model.Ck199;
			parameters[207].Value = model.Ck200;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSRole_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long IID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSRole_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole GetModel(long IID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@IID", SqlDbType.BigInt)};
			parameters[0].Value = IID;

			MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole model=new MobileSoft.Model.SQMSys.Tb_Sys_PowerNodeSRole();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_PowerNodeSRole_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["IID"].ToString()!="")
				{
					model.IID=long.Parse(ds.Tables[0].Rows[0]["IID"].ToString());
				}
				model.UserCode=ds.Tables[0].Rows[0]["UserCode"].ToString();
				model.OrganCode=ds.Tables[0].Rows[0]["OrganCode"].ToString();
				model.OrganName=ds.Tables[0].Rows[0]["OrganName"].ToString();
				if(ds.Tables[0].Rows[0]["IsComp"].ToString()!="")
				{
					model.IsComp=int.Parse(ds.Tables[0].Rows[0]["IsComp"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommID"].ToString()!="")
				{
					model.CommID=long.Parse(ds.Tables[0].Rows[0]["CommID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommCode"].ToString()!="")
				{
					model.CommCode=new Guid(ds.Tables[0].Rows[0]["CommCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CkAll"].ToString()!="")
				{
					model.CkAll=int.Parse(ds.Tables[0].Rows[0]["CkAll"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck1"].ToString()!="")
				{
					model.Ck1=int.Parse(ds.Tables[0].Rows[0]["Ck1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck2"].ToString()!="")
				{
					model.Ck2=int.Parse(ds.Tables[0].Rows[0]["Ck2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck3"].ToString()!="")
				{
					model.Ck3=int.Parse(ds.Tables[0].Rows[0]["Ck3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck4"].ToString()!="")
				{
					model.Ck4=int.Parse(ds.Tables[0].Rows[0]["Ck4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck5"].ToString()!="")
				{
					model.Ck5=int.Parse(ds.Tables[0].Rows[0]["Ck5"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck6"].ToString()!="")
				{
					model.Ck6=int.Parse(ds.Tables[0].Rows[0]["Ck6"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck7"].ToString()!="")
				{
					model.Ck7=int.Parse(ds.Tables[0].Rows[0]["Ck7"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck8"].ToString()!="")
				{
					model.Ck8=int.Parse(ds.Tables[0].Rows[0]["Ck8"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck9"].ToString()!="")
				{
					model.Ck9=int.Parse(ds.Tables[0].Rows[0]["Ck9"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck10"].ToString()!="")
				{
					model.Ck10=int.Parse(ds.Tables[0].Rows[0]["Ck10"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck11"].ToString()!="")
				{
					model.Ck11=int.Parse(ds.Tables[0].Rows[0]["Ck11"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck12"].ToString()!="")
				{
					model.Ck12=int.Parse(ds.Tables[0].Rows[0]["Ck12"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck13"].ToString()!="")
				{
					model.Ck13=int.Parse(ds.Tables[0].Rows[0]["Ck13"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck14"].ToString()!="")
				{
					model.Ck14=int.Parse(ds.Tables[0].Rows[0]["Ck14"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck15"].ToString()!="")
				{
					model.Ck15=int.Parse(ds.Tables[0].Rows[0]["Ck15"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck16"].ToString()!="")
				{
					model.Ck16=int.Parse(ds.Tables[0].Rows[0]["Ck16"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck17"].ToString()!="")
				{
					model.Ck17=int.Parse(ds.Tables[0].Rows[0]["Ck17"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck18"].ToString()!="")
				{
					model.Ck18=int.Parse(ds.Tables[0].Rows[0]["Ck18"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck19"].ToString()!="")
				{
					model.Ck19=int.Parse(ds.Tables[0].Rows[0]["Ck19"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck20"].ToString()!="")
				{
					model.Ck20=int.Parse(ds.Tables[0].Rows[0]["Ck20"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck21"].ToString()!="")
				{
					model.Ck21=int.Parse(ds.Tables[0].Rows[0]["Ck21"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck22"].ToString()!="")
				{
					model.Ck22=int.Parse(ds.Tables[0].Rows[0]["Ck22"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck23"].ToString()!="")
				{
					model.Ck23=int.Parse(ds.Tables[0].Rows[0]["Ck23"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck24"].ToString()!="")
				{
					model.Ck24=int.Parse(ds.Tables[0].Rows[0]["Ck24"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck25"].ToString()!="")
				{
					model.Ck25=int.Parse(ds.Tables[0].Rows[0]["Ck25"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck26"].ToString()!="")
				{
					model.Ck26=int.Parse(ds.Tables[0].Rows[0]["Ck26"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck27"].ToString()!="")
				{
					model.Ck27=int.Parse(ds.Tables[0].Rows[0]["Ck27"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck28"].ToString()!="")
				{
					model.Ck28=int.Parse(ds.Tables[0].Rows[0]["Ck28"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck29"].ToString()!="")
				{
					model.Ck29=int.Parse(ds.Tables[0].Rows[0]["Ck29"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck30"].ToString()!="")
				{
					model.Ck30=int.Parse(ds.Tables[0].Rows[0]["Ck30"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck31"].ToString()!="")
				{
					model.Ck31=int.Parse(ds.Tables[0].Rows[0]["Ck31"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck32"].ToString()!="")
				{
					model.Ck32=int.Parse(ds.Tables[0].Rows[0]["Ck32"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck33"].ToString()!="")
				{
					model.Ck33=int.Parse(ds.Tables[0].Rows[0]["Ck33"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck34"].ToString()!="")
				{
					model.Ck34=int.Parse(ds.Tables[0].Rows[0]["Ck34"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck35"].ToString()!="")
				{
					model.Ck35=int.Parse(ds.Tables[0].Rows[0]["Ck35"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck36"].ToString()!="")
				{
					model.Ck36=int.Parse(ds.Tables[0].Rows[0]["Ck36"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck37"].ToString()!="")
				{
					model.Ck37=int.Parse(ds.Tables[0].Rows[0]["Ck37"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck38"].ToString()!="")
				{
					model.Ck38=int.Parse(ds.Tables[0].Rows[0]["Ck38"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck39"].ToString()!="")
				{
					model.Ck39=int.Parse(ds.Tables[0].Rows[0]["Ck39"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck40"].ToString()!="")
				{
					model.Ck40=int.Parse(ds.Tables[0].Rows[0]["Ck40"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck41"].ToString()!="")
				{
					model.Ck41=int.Parse(ds.Tables[0].Rows[0]["Ck41"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck42"].ToString()!="")
				{
					model.Ck42=int.Parse(ds.Tables[0].Rows[0]["Ck42"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck43"].ToString()!="")
				{
					model.Ck43=int.Parse(ds.Tables[0].Rows[0]["Ck43"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck44"].ToString()!="")
				{
					model.Ck44=int.Parse(ds.Tables[0].Rows[0]["Ck44"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck45"].ToString()!="")
				{
					model.Ck45=int.Parse(ds.Tables[0].Rows[0]["Ck45"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck46"].ToString()!="")
				{
					model.Ck46=int.Parse(ds.Tables[0].Rows[0]["Ck46"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck47"].ToString()!="")
				{
					model.Ck47=int.Parse(ds.Tables[0].Rows[0]["Ck47"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck48"].ToString()!="")
				{
					model.Ck48=int.Parse(ds.Tables[0].Rows[0]["Ck48"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck49"].ToString()!="")
				{
					model.Ck49=int.Parse(ds.Tables[0].Rows[0]["Ck49"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck50"].ToString()!="")
				{
					model.Ck50=int.Parse(ds.Tables[0].Rows[0]["Ck50"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck51"].ToString()!="")
				{
					model.Ck51=int.Parse(ds.Tables[0].Rows[0]["Ck51"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck52"].ToString()!="")
				{
					model.Ck52=int.Parse(ds.Tables[0].Rows[0]["Ck52"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck53"].ToString()!="")
				{
					model.Ck53=int.Parse(ds.Tables[0].Rows[0]["Ck53"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck54"].ToString()!="")
				{
					model.Ck54=int.Parse(ds.Tables[0].Rows[0]["Ck54"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck55"].ToString()!="")
				{
					model.Ck55=int.Parse(ds.Tables[0].Rows[0]["Ck55"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck56"].ToString()!="")
				{
					model.Ck56=int.Parse(ds.Tables[0].Rows[0]["Ck56"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck57"].ToString()!="")
				{
					model.Ck57=int.Parse(ds.Tables[0].Rows[0]["Ck57"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck58"].ToString()!="")
				{
					model.Ck58=int.Parse(ds.Tables[0].Rows[0]["Ck58"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck59"].ToString()!="")
				{
					model.Ck59=int.Parse(ds.Tables[0].Rows[0]["Ck59"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck60"].ToString()!="")
				{
					model.Ck60=int.Parse(ds.Tables[0].Rows[0]["Ck60"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck61"].ToString()!="")
				{
					model.Ck61=int.Parse(ds.Tables[0].Rows[0]["Ck61"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck62"].ToString()!="")
				{
					model.Ck62=int.Parse(ds.Tables[0].Rows[0]["Ck62"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck63"].ToString()!="")
				{
					model.Ck63=int.Parse(ds.Tables[0].Rows[0]["Ck63"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck64"].ToString()!="")
				{
					model.Ck64=int.Parse(ds.Tables[0].Rows[0]["Ck64"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck65"].ToString()!="")
				{
					model.Ck65=int.Parse(ds.Tables[0].Rows[0]["Ck65"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck66"].ToString()!="")
				{
					model.Ck66=int.Parse(ds.Tables[0].Rows[0]["Ck66"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck67"].ToString()!="")
				{
					model.Ck67=int.Parse(ds.Tables[0].Rows[0]["Ck67"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck68"].ToString()!="")
				{
					model.Ck68=int.Parse(ds.Tables[0].Rows[0]["Ck68"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck69"].ToString()!="")
				{
					model.Ck69=int.Parse(ds.Tables[0].Rows[0]["Ck69"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck70"].ToString()!="")
				{
					model.Ck70=int.Parse(ds.Tables[0].Rows[0]["Ck70"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck71"].ToString()!="")
				{
					model.Ck71=int.Parse(ds.Tables[0].Rows[0]["Ck71"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck72"].ToString()!="")
				{
					model.Ck72=int.Parse(ds.Tables[0].Rows[0]["Ck72"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck73"].ToString()!="")
				{
					model.Ck73=int.Parse(ds.Tables[0].Rows[0]["Ck73"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck74"].ToString()!="")
				{
					model.Ck74=int.Parse(ds.Tables[0].Rows[0]["Ck74"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck75"].ToString()!="")
				{
					model.Ck75=int.Parse(ds.Tables[0].Rows[0]["Ck75"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck76"].ToString()!="")
				{
					model.Ck76=int.Parse(ds.Tables[0].Rows[0]["Ck76"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck77"].ToString()!="")
				{
					model.Ck77=int.Parse(ds.Tables[0].Rows[0]["Ck77"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck78"].ToString()!="")
				{
					model.Ck78=int.Parse(ds.Tables[0].Rows[0]["Ck78"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck79"].ToString()!="")
				{
					model.Ck79=int.Parse(ds.Tables[0].Rows[0]["Ck79"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck80"].ToString()!="")
				{
					model.Ck80=int.Parse(ds.Tables[0].Rows[0]["Ck80"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck81"].ToString()!="")
				{
					model.Ck81=int.Parse(ds.Tables[0].Rows[0]["Ck81"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck82"].ToString()!="")
				{
					model.Ck82=int.Parse(ds.Tables[0].Rows[0]["Ck82"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck83"].ToString()!="")
				{
					model.Ck83=int.Parse(ds.Tables[0].Rows[0]["Ck83"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck84"].ToString()!="")
				{
					model.Ck84=int.Parse(ds.Tables[0].Rows[0]["Ck84"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck85"].ToString()!="")
				{
					model.Ck85=int.Parse(ds.Tables[0].Rows[0]["Ck85"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck86"].ToString()!="")
				{
					model.Ck86=int.Parse(ds.Tables[0].Rows[0]["Ck86"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck87"].ToString()!="")
				{
					model.Ck87=int.Parse(ds.Tables[0].Rows[0]["Ck87"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck88"].ToString()!="")
				{
					model.Ck88=int.Parse(ds.Tables[0].Rows[0]["Ck88"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck89"].ToString()!="")
				{
					model.Ck89=int.Parse(ds.Tables[0].Rows[0]["Ck89"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck90"].ToString()!="")
				{
					model.Ck90=int.Parse(ds.Tables[0].Rows[0]["Ck90"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck91"].ToString()!="")
				{
					model.Ck91=int.Parse(ds.Tables[0].Rows[0]["Ck91"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck92"].ToString()!="")
				{
					model.Ck92=int.Parse(ds.Tables[0].Rows[0]["Ck92"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck93"].ToString()!="")
				{
					model.Ck93=int.Parse(ds.Tables[0].Rows[0]["Ck93"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck94"].ToString()!="")
				{
					model.Ck94=int.Parse(ds.Tables[0].Rows[0]["Ck94"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck95"].ToString()!="")
				{
					model.Ck95=int.Parse(ds.Tables[0].Rows[0]["Ck95"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck96"].ToString()!="")
				{
					model.Ck96=int.Parse(ds.Tables[0].Rows[0]["Ck96"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck97"].ToString()!="")
				{
					model.Ck97=int.Parse(ds.Tables[0].Rows[0]["Ck97"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck98"].ToString()!="")
				{
					model.Ck98=int.Parse(ds.Tables[0].Rows[0]["Ck98"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck99"].ToString()!="")
				{
					model.Ck99=int.Parse(ds.Tables[0].Rows[0]["Ck99"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck100"].ToString()!="")
				{
					model.Ck100=int.Parse(ds.Tables[0].Rows[0]["Ck100"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck101"].ToString()!="")
				{
					model.Ck101=int.Parse(ds.Tables[0].Rows[0]["Ck101"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck102"].ToString()!="")
				{
					model.Ck102=int.Parse(ds.Tables[0].Rows[0]["Ck102"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck103"].ToString()!="")
				{
					model.Ck103=int.Parse(ds.Tables[0].Rows[0]["Ck103"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck104"].ToString()!="")
				{
					model.Ck104=int.Parse(ds.Tables[0].Rows[0]["Ck104"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck105"].ToString()!="")
				{
					model.Ck105=int.Parse(ds.Tables[0].Rows[0]["Ck105"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck106"].ToString()!="")
				{
					model.Ck106=int.Parse(ds.Tables[0].Rows[0]["Ck106"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck107"].ToString()!="")
				{
					model.Ck107=int.Parse(ds.Tables[0].Rows[0]["Ck107"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck108"].ToString()!="")
				{
					model.Ck108=int.Parse(ds.Tables[0].Rows[0]["Ck108"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck109"].ToString()!="")
				{
					model.Ck109=int.Parse(ds.Tables[0].Rows[0]["Ck109"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck110"].ToString()!="")
				{
					model.Ck110=int.Parse(ds.Tables[0].Rows[0]["Ck110"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck111"].ToString()!="")
				{
					model.Ck111=int.Parse(ds.Tables[0].Rows[0]["Ck111"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck112"].ToString()!="")
				{
					model.Ck112=int.Parse(ds.Tables[0].Rows[0]["Ck112"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck113"].ToString()!="")
				{
					model.Ck113=int.Parse(ds.Tables[0].Rows[0]["Ck113"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck114"].ToString()!="")
				{
					model.Ck114=int.Parse(ds.Tables[0].Rows[0]["Ck114"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck115"].ToString()!="")
				{
					model.Ck115=int.Parse(ds.Tables[0].Rows[0]["Ck115"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck116"].ToString()!="")
				{
					model.Ck116=int.Parse(ds.Tables[0].Rows[0]["Ck116"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck117"].ToString()!="")
				{
					model.Ck117=int.Parse(ds.Tables[0].Rows[0]["Ck117"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck118"].ToString()!="")
				{
					model.Ck118=int.Parse(ds.Tables[0].Rows[0]["Ck118"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck119"].ToString()!="")
				{
					model.Ck119=int.Parse(ds.Tables[0].Rows[0]["Ck119"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck120"].ToString()!="")
				{
					model.Ck120=int.Parse(ds.Tables[0].Rows[0]["Ck120"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck121"].ToString()!="")
				{
					model.Ck121=int.Parse(ds.Tables[0].Rows[0]["Ck121"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck122"].ToString()!="")
				{
					model.Ck122=int.Parse(ds.Tables[0].Rows[0]["Ck122"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck123"].ToString()!="")
				{
					model.Ck123=int.Parse(ds.Tables[0].Rows[0]["Ck123"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck124"].ToString()!="")
				{
					model.Ck124=int.Parse(ds.Tables[0].Rows[0]["Ck124"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck125"].ToString()!="")
				{
					model.Ck125=int.Parse(ds.Tables[0].Rows[0]["Ck125"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck126"].ToString()!="")
				{
					model.Ck126=int.Parse(ds.Tables[0].Rows[0]["Ck126"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck127"].ToString()!="")
				{
					model.Ck127=int.Parse(ds.Tables[0].Rows[0]["Ck127"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck128"].ToString()!="")
				{
					model.Ck128=int.Parse(ds.Tables[0].Rows[0]["Ck128"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck129"].ToString()!="")
				{
					model.Ck129=int.Parse(ds.Tables[0].Rows[0]["Ck129"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck130"].ToString()!="")
				{
					model.Ck130=int.Parse(ds.Tables[0].Rows[0]["Ck130"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck131"].ToString()!="")
				{
					model.Ck131=int.Parse(ds.Tables[0].Rows[0]["Ck131"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck132"].ToString()!="")
				{
					model.Ck132=int.Parse(ds.Tables[0].Rows[0]["Ck132"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck133"].ToString()!="")
				{
					model.Ck133=int.Parse(ds.Tables[0].Rows[0]["Ck133"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck134"].ToString()!="")
				{
					model.Ck134=int.Parse(ds.Tables[0].Rows[0]["Ck134"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck135"].ToString()!="")
				{
					model.Ck135=int.Parse(ds.Tables[0].Rows[0]["Ck135"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck136"].ToString()!="")
				{
					model.Ck136=int.Parse(ds.Tables[0].Rows[0]["Ck136"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck137"].ToString()!="")
				{
					model.Ck137=int.Parse(ds.Tables[0].Rows[0]["Ck137"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck138"].ToString()!="")
				{
					model.Ck138=int.Parse(ds.Tables[0].Rows[0]["Ck138"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck139"].ToString()!="")
				{
					model.Ck139=int.Parse(ds.Tables[0].Rows[0]["Ck139"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck140"].ToString()!="")
				{
					model.Ck140=int.Parse(ds.Tables[0].Rows[0]["Ck140"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck141"].ToString()!="")
				{
					model.Ck141=int.Parse(ds.Tables[0].Rows[0]["Ck141"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck142"].ToString()!="")
				{
					model.Ck142=int.Parse(ds.Tables[0].Rows[0]["Ck142"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck143"].ToString()!="")
				{
					model.Ck143=int.Parse(ds.Tables[0].Rows[0]["Ck143"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck144"].ToString()!="")
				{
					model.Ck144=int.Parse(ds.Tables[0].Rows[0]["Ck144"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck145"].ToString()!="")
				{
					model.Ck145=int.Parse(ds.Tables[0].Rows[0]["Ck145"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck146"].ToString()!="")
				{
					model.Ck146=int.Parse(ds.Tables[0].Rows[0]["Ck146"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck147"].ToString()!="")
				{
					model.Ck147=int.Parse(ds.Tables[0].Rows[0]["Ck147"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck148"].ToString()!="")
				{
					model.Ck148=int.Parse(ds.Tables[0].Rows[0]["Ck148"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck149"].ToString()!="")
				{
					model.Ck149=int.Parse(ds.Tables[0].Rows[0]["Ck149"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck150"].ToString()!="")
				{
					model.Ck150=int.Parse(ds.Tables[0].Rows[0]["Ck150"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck151"].ToString()!="")
				{
					model.Ck151=int.Parse(ds.Tables[0].Rows[0]["Ck151"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck152"].ToString()!="")
				{
					model.Ck152=int.Parse(ds.Tables[0].Rows[0]["Ck152"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck153"].ToString()!="")
				{
					model.Ck153=int.Parse(ds.Tables[0].Rows[0]["Ck153"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck154"].ToString()!="")
				{
					model.Ck154=int.Parse(ds.Tables[0].Rows[0]["Ck154"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck155"].ToString()!="")
				{
					model.Ck155=int.Parse(ds.Tables[0].Rows[0]["Ck155"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck156"].ToString()!="")
				{
					model.Ck156=int.Parse(ds.Tables[0].Rows[0]["Ck156"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck157"].ToString()!="")
				{
					model.Ck157=int.Parse(ds.Tables[0].Rows[0]["Ck157"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck158"].ToString()!="")
				{
					model.Ck158=int.Parse(ds.Tables[0].Rows[0]["Ck158"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck159"].ToString()!="")
				{
					model.Ck159=int.Parse(ds.Tables[0].Rows[0]["Ck159"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck160"].ToString()!="")
				{
					model.Ck160=int.Parse(ds.Tables[0].Rows[0]["Ck160"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck161"].ToString()!="")
				{
					model.Ck161=int.Parse(ds.Tables[0].Rows[0]["Ck161"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck162"].ToString()!="")
				{
					model.Ck162=int.Parse(ds.Tables[0].Rows[0]["Ck162"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck163"].ToString()!="")
				{
					model.Ck163=int.Parse(ds.Tables[0].Rows[0]["Ck163"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck164"].ToString()!="")
				{
					model.Ck164=int.Parse(ds.Tables[0].Rows[0]["Ck164"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck165"].ToString()!="")
				{
					model.Ck165=int.Parse(ds.Tables[0].Rows[0]["Ck165"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck166"].ToString()!="")
				{
					model.Ck166=int.Parse(ds.Tables[0].Rows[0]["Ck166"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck167"].ToString()!="")
				{
					model.Ck167=int.Parse(ds.Tables[0].Rows[0]["Ck167"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck168"].ToString()!="")
				{
					model.Ck168=int.Parse(ds.Tables[0].Rows[0]["Ck168"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck169"].ToString()!="")
				{
					model.Ck169=int.Parse(ds.Tables[0].Rows[0]["Ck169"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck170"].ToString()!="")
				{
					model.Ck170=int.Parse(ds.Tables[0].Rows[0]["Ck170"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck171"].ToString()!="")
				{
					model.Ck171=int.Parse(ds.Tables[0].Rows[0]["Ck171"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck172"].ToString()!="")
				{
					model.Ck172=int.Parse(ds.Tables[0].Rows[0]["Ck172"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck173"].ToString()!="")
				{
					model.Ck173=int.Parse(ds.Tables[0].Rows[0]["Ck173"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck174"].ToString()!="")
				{
					model.Ck174=int.Parse(ds.Tables[0].Rows[0]["Ck174"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck175"].ToString()!="")
				{
					model.Ck175=int.Parse(ds.Tables[0].Rows[0]["Ck175"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck176"].ToString()!="")
				{
					model.Ck176=int.Parse(ds.Tables[0].Rows[0]["Ck176"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck177"].ToString()!="")
				{
					model.Ck177=int.Parse(ds.Tables[0].Rows[0]["Ck177"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck178"].ToString()!="")
				{
					model.Ck178=int.Parse(ds.Tables[0].Rows[0]["Ck178"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck179"].ToString()!="")
				{
					model.Ck179=int.Parse(ds.Tables[0].Rows[0]["Ck179"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck180"].ToString()!="")
				{
					model.Ck180=int.Parse(ds.Tables[0].Rows[0]["Ck180"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck181"].ToString()!="")
				{
					model.Ck181=int.Parse(ds.Tables[0].Rows[0]["Ck181"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck182"].ToString()!="")
				{
					model.Ck182=int.Parse(ds.Tables[0].Rows[0]["Ck182"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck183"].ToString()!="")
				{
					model.Ck183=int.Parse(ds.Tables[0].Rows[0]["Ck183"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck184"].ToString()!="")
				{
					model.Ck184=int.Parse(ds.Tables[0].Rows[0]["Ck184"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck185"].ToString()!="")
				{
					model.Ck185=int.Parse(ds.Tables[0].Rows[0]["Ck185"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck186"].ToString()!="")
				{
					model.Ck186=int.Parse(ds.Tables[0].Rows[0]["Ck186"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck187"].ToString()!="")
				{
					model.Ck187=int.Parse(ds.Tables[0].Rows[0]["Ck187"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck188"].ToString()!="")
				{
					model.Ck188=int.Parse(ds.Tables[0].Rows[0]["Ck188"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck189"].ToString()!="")
				{
					model.Ck189=int.Parse(ds.Tables[0].Rows[0]["Ck189"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck190"].ToString()!="")
				{
					model.Ck190=int.Parse(ds.Tables[0].Rows[0]["Ck190"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck191"].ToString()!="")
				{
					model.Ck191=int.Parse(ds.Tables[0].Rows[0]["Ck191"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck192"].ToString()!="")
				{
					model.Ck192=int.Parse(ds.Tables[0].Rows[0]["Ck192"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck193"].ToString()!="")
				{
					model.Ck193=int.Parse(ds.Tables[0].Rows[0]["Ck193"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck194"].ToString()!="")
				{
					model.Ck194=int.Parse(ds.Tables[0].Rows[0]["Ck194"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck195"].ToString()!="")
				{
					model.Ck195=int.Parse(ds.Tables[0].Rows[0]["Ck195"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck196"].ToString()!="")
				{
					model.Ck196=int.Parse(ds.Tables[0].Rows[0]["Ck196"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck197"].ToString()!="")
				{
					model.Ck197=int.Parse(ds.Tables[0].Rows[0]["Ck197"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck198"].ToString()!="")
				{
					model.Ck198=int.Parse(ds.Tables[0].Rows[0]["Ck198"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck199"].ToString()!="")
				{
					model.Ck199=int.Parse(ds.Tables[0].Rows[0]["Ck199"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Ck200"].ToString()!="")
				{
					model.Ck200=int.Parse(ds.Tables[0].Rows[0]["Ck200"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select IID,UserCode,OrganCode,OrganName,IsComp,CommID,CommCode,CkAll,Ck1,Ck2,Ck3,Ck4,Ck5,Ck6,Ck7,Ck8,Ck9,Ck10,Ck11,Ck12,Ck13,Ck14,Ck15,Ck16,Ck17,Ck18,Ck19,Ck20,Ck21,Ck22,Ck23,Ck24,Ck25,Ck26,Ck27,Ck28,Ck29,Ck30,Ck31,Ck32,Ck33,Ck34,Ck35,Ck36,Ck37,Ck38,Ck39,Ck40,Ck41,Ck42,Ck43,Ck44,Ck45,Ck46,Ck47,Ck48,Ck49,Ck50,Ck51,Ck52,Ck53,Ck54,Ck55,Ck56,Ck57,Ck58,Ck59,Ck60,Ck61,Ck62,Ck63,Ck64,Ck65,Ck66,Ck67,Ck68,Ck69,Ck70,Ck71,Ck72,Ck73,Ck74,Ck75,Ck76,Ck77,Ck78,Ck79,Ck80,Ck81,Ck82,Ck83,Ck84,Ck85,Ck86,Ck87,Ck88,Ck89,Ck90,Ck91,Ck92,Ck93,Ck94,Ck95,Ck96,Ck97,Ck98,Ck99,Ck100,Ck101,Ck102,Ck103,Ck104,Ck105,Ck106,Ck107,Ck108,Ck109,Ck110,Ck111,Ck112,Ck113,Ck114,Ck115,Ck116,Ck117,Ck118,Ck119,Ck120,Ck121,Ck122,Ck123,Ck124,Ck125,Ck126,Ck127,Ck128,Ck129,Ck130,Ck131,Ck132,Ck133,Ck134,Ck135,Ck136,Ck137,Ck138,Ck139,Ck140,Ck141,Ck142,Ck143,Ck144,Ck145,Ck146,Ck147,Ck148,Ck149,Ck150,Ck151,Ck152,Ck153,Ck154,Ck155,Ck156,Ck157,Ck158,Ck159,Ck160,Ck161,Ck162,Ck163,Ck164,Ck165,Ck166,Ck167,Ck168,Ck169,Ck170,Ck171,Ck172,Ck173,Ck174,Ck175,Ck176,Ck177,Ck178,Ck179,Ck180,Ck181,Ck182,Ck183,Ck184,Ck185,Ck186,Ck187,Ck188,Ck189,Ck190,Ck191,Ck192,Ck193,Ck194,Ck195,Ck196,Ck197,Ck198,Ck199,Ck200 ");
			strSql.Append(" FROM Tb_Sys_PowerNodeSRole ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string fieldOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" IID,UserCode,OrganCode,OrganName,IsComp,CommID,CommCode,CkAll,Ck1,Ck2,Ck3,Ck4,Ck5,Ck6,Ck7,Ck8,Ck9,Ck10,Ck11,Ck12,Ck13,Ck14,Ck15,Ck16,Ck17,Ck18,Ck19,Ck20,Ck21,Ck22,Ck23,Ck24,Ck25,Ck26,Ck27,Ck28,Ck29,Ck30,Ck31,Ck32,Ck33,Ck34,Ck35,Ck36,Ck37,Ck38,Ck39,Ck40,Ck41,Ck42,Ck43,Ck44,Ck45,Ck46,Ck47,Ck48,Ck49,Ck50,Ck51,Ck52,Ck53,Ck54,Ck55,Ck56,Ck57,Ck58,Ck59,Ck60,Ck61,Ck62,Ck63,Ck64,Ck65,Ck66,Ck67,Ck68,Ck69,Ck70,Ck71,Ck72,Ck73,Ck74,Ck75,Ck76,Ck77,Ck78,Ck79,Ck80,Ck81,Ck82,Ck83,Ck84,Ck85,Ck86,Ck87,Ck88,Ck89,Ck90,Ck91,Ck92,Ck93,Ck94,Ck95,Ck96,Ck97,Ck98,Ck99,Ck100,Ck101,Ck102,Ck103,Ck104,Ck105,Ck106,Ck107,Ck108,Ck109,Ck110,Ck111,Ck112,Ck113,Ck114,Ck115,Ck116,Ck117,Ck118,Ck119,Ck120,Ck121,Ck122,Ck123,Ck124,Ck125,Ck126,Ck127,Ck128,Ck129,Ck130,Ck131,Ck132,Ck133,Ck134,Ck135,Ck136,Ck137,Ck138,Ck139,Ck140,Ck141,Ck142,Ck143,Ck144,Ck145,Ck146,Ck147,Ck148,Ck149,Ck150,Ck151,Ck152,Ck153,Ck154,Ck155,Ck156,Ck157,Ck158,Ck159,Ck160,Ck161,Ck162,Ck163,Ck164,Ck165,Ck166,Ck167,Ck168,Ck169,Ck170,Ck171,Ck172,Ck173,Ck174,Ck175,Ck176,Ck177,Ck178,Ck179,Ck180,Ck181,Ck182,Ck183,Ck184,Ck185,Ck186,Ck187,Ck188,Ck189,Ck190,Ck191,Ck192,Ck193,Ck194,Ck195,Ck196,Ck197,Ck198,Ck199,Ck200 ");
			strSql.Append(" FROM Tb_Sys_PowerNodeSRole ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + fieldOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(out int PageCount, out int Counts, string StrCondition, int PageIndex, int PageSize,string SortField,int Sort)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@FldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@FldSort", SqlDbType.VarChar, 1000),
					new SqlParameter("@Sort", SqlDbType.Int),
					new SqlParameter("@StrCondition", SqlDbType.VarChar, 8000),
					new SqlParameter("@Id", SqlDbType.VarChar, 50),
					new SqlParameter("@PageCount", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
					new SqlParameter("@Counts", SqlDbType.Int, 4,ParameterDirection.Output, false, 0, 0,string.Empty, DataRowVersion.Default, null),
					};
			parameters[0].Value = "*";
			parameters[1].Value = PageSize;
			parameters[2].Value = PageIndex;
			parameters[3].Value = SortField;
			parameters[4].Value = Sort;
			parameters[5].Value = "SELECT * FROM Tb_Sys_PowerNodeSRole WHERE 1=1 " + StrCondition;
			parameters[6].Value = "IID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

