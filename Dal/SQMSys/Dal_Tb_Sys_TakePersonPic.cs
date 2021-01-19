using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MobileSoft.DBUtility;//请先添加引用
namespace MobileSoft.DAL.SQMSys
{
	/// <summary>
	/// 数据访问类Dal_Tb_Sys_TakePersonPic。
	/// </summary>
	public class Dal_Tb_Sys_TakePersonPic
	{
		public Dal_Tb_Sys_TakePersonPic()
		{
			DbHelperSQL.ConnectionString = PubConstant.hmWyglConnectionString;
		}
		#region  成员方法

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long StatID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			int result= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePersonPic_Exists",parameters,out rowsAffected);
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
		public int Add(MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@StatType", SqlDbType.SmallInt,2),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@StatDate", SqlDbType.DateTime),
					new SqlParameter("@TotalHNum", SqlDbType.Int,4),
					new SqlParameter("@TotalHNum1", SqlDbType.Int,4),
					new SqlParameter("@TotalHNum2", SqlDbType.Int,4),
					new SqlParameter("@TotalHNum3", SqlDbType.Int,4),
					new SqlParameter("@TotalHNum4", SqlDbType.Int,4),
					new SqlParameter("@TotalNum", SqlDbType.Int,4),
					new SqlParameter("@TotalNum1", SqlDbType.Int,4),
					new SqlParameter("@TotalNum2", SqlDbType.Int,4),
					new SqlParameter("@TotalNum3", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum1", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum2", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum3", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum4", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum5", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum6", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum7", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum1", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum2", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum3", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum4", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum5", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum6", SqlDbType.Int,4),
					new SqlParameter("@EmploymentNum", SqlDbType.Int,4),
					new SqlParameter("@EmploymentNum1", SqlDbType.Int,4),
					new SqlParameter("@EmploymentNum2", SqlDbType.Int,4),
					new SqlParameter("@EmploymentRate", SqlDbType.Decimal,9),
					new SqlParameter("@TrainYearTimes", SqlDbType.Int,4),
					new SqlParameter("@TrainYearNum", SqlDbType.Int,4),
					new SqlParameter("@RecommendYearTimes", SqlDbType.Int,4),
					new SqlParameter("@RecommendYearNum", SqlDbType.Int,4),
					new SqlParameter("@SecurityNum1", SqlDbType.Int,4),
					new SqlParameter("@SecurityNum2", SqlDbType.Int,4),
					new SqlParameter("@SecurityNum3", SqlDbType.Int,4),
					new SqlParameter("@SecurityNum4", SqlDbType.Int,4),
					new SqlParameter("@SuspectNum", SqlDbType.Int,4),
					new SqlParameter("@EmphasisNum", SqlDbType.Int,4),
					new SqlParameter("@FertilityNum", SqlDbType.Int,4),
					new SqlParameter("@PreschoolNum", SqlDbType.Int,4),
					new SqlParameter("@BabyNum", SqlDbType.Int,4),
					new SqlParameter("@BabyNum1", SqlDbType.Int,4),
					new SqlParameter("@BabyNum2", SqlDbType.Int,4),
					new SqlParameter("@PartyBranchNum", SqlDbType.Int,4),
					new SqlParameter("@PartyNum", SqlDbType.Int,4),
					new SqlParameter("@PartyNum1", SqlDbType.Int,4),
					new SqlParameter("@PartyNum2", SqlDbType.Int,4),
					new SqlParameter("@PartyNum3", SqlDbType.Int,4)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.StatType;
			parameters[2].Value = model.StreetCode;
			parameters[3].Value = model.CommCode;
			parameters[4].Value = model.StatDate;
			parameters[5].Value = model.TotalHNum;
			parameters[6].Value = model.TotalHNum1;
			parameters[7].Value = model.TotalHNum2;
			parameters[8].Value = model.TotalHNum3;
			parameters[9].Value = model.TotalHNum4;
			parameters[10].Value = model.TotalNum;
			parameters[11].Value = model.TotalNum1;
			parameters[12].Value = model.TotalNum2;
			parameters[13].Value = model.TotalNum3;
			parameters[14].Value = model.TotalPHNum1;
			parameters[15].Value = model.TotalPHNum2;
			parameters[16].Value = model.TotalPHNum3;
			parameters[17].Value = model.TotalPHNum4;
			parameters[18].Value = model.TotalPHNum5;
			parameters[19].Value = model.TotalPHNum6;
			parameters[20].Value = model.TotalPHNum7;
			parameters[21].Value = model.TotalPNum1;
			parameters[22].Value = model.TotalPNum2;
			parameters[23].Value = model.TotalPNum3;
			parameters[24].Value = model.TotalPNum4;
			parameters[25].Value = model.TotalPNum5;
			parameters[26].Value = model.TotalPNum6;
			parameters[27].Value = model.EmploymentNum;
			parameters[28].Value = model.EmploymentNum1;
			parameters[29].Value = model.EmploymentNum2;
			parameters[30].Value = model.EmploymentRate;
			parameters[31].Value = model.TrainYearTimes;
			parameters[32].Value = model.TrainYearNum;
			parameters[33].Value = model.RecommendYearTimes;
			parameters[34].Value = model.RecommendYearNum;
			parameters[35].Value = model.SecurityNum1;
			parameters[36].Value = model.SecurityNum2;
			parameters[37].Value = model.SecurityNum3;
			parameters[38].Value = model.SecurityNum4;
			parameters[39].Value = model.SuspectNum;
			parameters[40].Value = model.EmphasisNum;
			parameters[41].Value = model.FertilityNum;
			parameters[42].Value = model.PreschoolNum;
			parameters[43].Value = model.BabyNum;
			parameters[44].Value = model.BabyNum1;
			parameters[45].Value = model.BabyNum2;
			parameters[46].Value = model.PartyBranchNum;
			parameters[47].Value = model.PartyNum;
			parameters[48].Value = model.PartyNum1;
			parameters[49].Value = model.PartyNum2;
			parameters[50].Value = model.PartyNum3;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePersonPic_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt,8),
					new SqlParameter("@StatType", SqlDbType.SmallInt,2),
					new SqlParameter("@StreetCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@CommCode", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@StatDate", SqlDbType.DateTime),
					new SqlParameter("@TotalHNum", SqlDbType.Int,4),
					new SqlParameter("@TotalHNum1", SqlDbType.Int,4),
					new SqlParameter("@TotalHNum2", SqlDbType.Int,4),
					new SqlParameter("@TotalHNum3", SqlDbType.Int,4),
					new SqlParameter("@TotalHNum4", SqlDbType.Int,4),
					new SqlParameter("@TotalNum", SqlDbType.Int,4),
					new SqlParameter("@TotalNum1", SqlDbType.Int,4),
					new SqlParameter("@TotalNum2", SqlDbType.Int,4),
					new SqlParameter("@TotalNum3", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum1", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum2", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum3", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum4", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum5", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum6", SqlDbType.Int,4),
					new SqlParameter("@TotalPHNum7", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum1", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum2", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum3", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum4", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum5", SqlDbType.Int,4),
					new SqlParameter("@TotalPNum6", SqlDbType.Int,4),
					new SqlParameter("@EmploymentNum", SqlDbType.Int,4),
					new SqlParameter("@EmploymentNum1", SqlDbType.Int,4),
					new SqlParameter("@EmploymentNum2", SqlDbType.Int,4),
					new SqlParameter("@EmploymentRate", SqlDbType.Decimal,9),
					new SqlParameter("@TrainYearTimes", SqlDbType.Int,4),
					new SqlParameter("@TrainYearNum", SqlDbType.Int,4),
					new SqlParameter("@RecommendYearTimes", SqlDbType.Int,4),
					new SqlParameter("@RecommendYearNum", SqlDbType.Int,4),
					new SqlParameter("@SecurityNum1", SqlDbType.Int,4),
					new SqlParameter("@SecurityNum2", SqlDbType.Int,4),
					new SqlParameter("@SecurityNum3", SqlDbType.Int,4),
					new SqlParameter("@SecurityNum4", SqlDbType.Int,4),
					new SqlParameter("@SuspectNum", SqlDbType.Int,4),
					new SqlParameter("@EmphasisNum", SqlDbType.Int,4),
					new SqlParameter("@FertilityNum", SqlDbType.Int,4),
					new SqlParameter("@PreschoolNum", SqlDbType.Int,4),
					new SqlParameter("@BabyNum", SqlDbType.Int,4),
					new SqlParameter("@BabyNum1", SqlDbType.Int,4),
					new SqlParameter("@BabyNum2", SqlDbType.Int,4),
					new SqlParameter("@PartyBranchNum", SqlDbType.Int,4),
					new SqlParameter("@PartyNum", SqlDbType.Int,4),
					new SqlParameter("@PartyNum1", SqlDbType.Int,4),
					new SqlParameter("@PartyNum2", SqlDbType.Int,4),
					new SqlParameter("@PartyNum3", SqlDbType.Int,4)};
			parameters[0].Value = model.StatID;
			parameters[1].Value = model.StatType;
			parameters[2].Value = model.StreetCode;
			parameters[3].Value = model.CommCode;
			parameters[4].Value = model.StatDate;
			parameters[5].Value = model.TotalHNum;
			parameters[6].Value = model.TotalHNum1;
			parameters[7].Value = model.TotalHNum2;
			parameters[8].Value = model.TotalHNum3;
			parameters[9].Value = model.TotalHNum4;
			parameters[10].Value = model.TotalNum;
			parameters[11].Value = model.TotalNum1;
			parameters[12].Value = model.TotalNum2;
			parameters[13].Value = model.TotalNum3;
			parameters[14].Value = model.TotalPHNum1;
			parameters[15].Value = model.TotalPHNum2;
			parameters[16].Value = model.TotalPHNum3;
			parameters[17].Value = model.TotalPHNum4;
			parameters[18].Value = model.TotalPHNum5;
			parameters[19].Value = model.TotalPHNum6;
			parameters[20].Value = model.TotalPHNum7;
			parameters[21].Value = model.TotalPNum1;
			parameters[22].Value = model.TotalPNum2;
			parameters[23].Value = model.TotalPNum3;
			parameters[24].Value = model.TotalPNum4;
			parameters[25].Value = model.TotalPNum5;
			parameters[26].Value = model.TotalPNum6;
			parameters[27].Value = model.EmploymentNum;
			parameters[28].Value = model.EmploymentNum1;
			parameters[29].Value = model.EmploymentNum2;
			parameters[30].Value = model.EmploymentRate;
			parameters[31].Value = model.TrainYearTimes;
			parameters[32].Value = model.TrainYearNum;
			parameters[33].Value = model.RecommendYearTimes;
			parameters[34].Value = model.RecommendYearNum;
			parameters[35].Value = model.SecurityNum1;
			parameters[36].Value = model.SecurityNum2;
			parameters[37].Value = model.SecurityNum3;
			parameters[38].Value = model.SecurityNum4;
			parameters[39].Value = model.SuspectNum;
			parameters[40].Value = model.EmphasisNum;
			parameters[41].Value = model.FertilityNum;
			parameters[42].Value = model.PreschoolNum;
			parameters[43].Value = model.BabyNum;
			parameters[44].Value = model.BabyNum1;
			parameters[45].Value = model.BabyNum2;
			parameters[46].Value = model.PartyBranchNum;
			parameters[47].Value = model.PartyNum;
			parameters[48].Value = model.PartyNum1;
			parameters[49].Value = model.PartyNum2;
			parameters[50].Value = model.PartyNum3;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePersonPic_Update",parameters,out rowsAffected);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(long StatID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePersonPic_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic GetModel(long StatID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@StatID", SqlDbType.BigInt)};
			parameters[0].Value = StatID;

			MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic model=new MobileSoft.Model.SQMSys.Tb_Sys_TakePersonPic();
			DataSet ds= DbHelperSQL.RunProcedure("Proc_Tb_Sys_TakePersonPic_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["StatID"].ToString()!="")
				{
					model.StatID=long.Parse(ds.Tables[0].Rows[0]["StatID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StatType"].ToString()!="")
				{
					model.StatType=int.Parse(ds.Tables[0].Rows[0]["StatType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StreetCode"].ToString()!="")
				{
					model.StreetCode=new Guid(ds.Tables[0].Rows[0]["StreetCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CommCode"].ToString()!="")
				{
					model.CommCode=new Guid(ds.Tables[0].Rows[0]["CommCode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["StatDate"].ToString()!="")
				{
					model.StatDate=DateTime.Parse(ds.Tables[0].Rows[0]["StatDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalHNum"].ToString()!="")
				{
					model.TotalHNum=int.Parse(ds.Tables[0].Rows[0]["TotalHNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalHNum1"].ToString()!="")
				{
					model.TotalHNum1=int.Parse(ds.Tables[0].Rows[0]["TotalHNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalHNum2"].ToString()!="")
				{
					model.TotalHNum2=int.Parse(ds.Tables[0].Rows[0]["TotalHNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalHNum3"].ToString()!="")
				{
					model.TotalHNum3=int.Parse(ds.Tables[0].Rows[0]["TotalHNum3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalHNum4"].ToString()!="")
				{
					model.TotalHNum4=int.Parse(ds.Tables[0].Rows[0]["TotalHNum4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalNum"].ToString()!="")
				{
					model.TotalNum=int.Parse(ds.Tables[0].Rows[0]["TotalNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalNum1"].ToString()!="")
				{
					model.TotalNum1=int.Parse(ds.Tables[0].Rows[0]["TotalNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalNum2"].ToString()!="")
				{
					model.TotalNum2=int.Parse(ds.Tables[0].Rows[0]["TotalNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalNum3"].ToString()!="")
				{
					model.TotalNum3=int.Parse(ds.Tables[0].Rows[0]["TotalNum3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPHNum1"].ToString()!="")
				{
					model.TotalPHNum1=int.Parse(ds.Tables[0].Rows[0]["TotalPHNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPHNum2"].ToString()!="")
				{
					model.TotalPHNum2=int.Parse(ds.Tables[0].Rows[0]["TotalPHNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPHNum3"].ToString()!="")
				{
					model.TotalPHNum3=int.Parse(ds.Tables[0].Rows[0]["TotalPHNum3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPHNum4"].ToString()!="")
				{
					model.TotalPHNum4=int.Parse(ds.Tables[0].Rows[0]["TotalPHNum4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPHNum5"].ToString()!="")
				{
					model.TotalPHNum5=int.Parse(ds.Tables[0].Rows[0]["TotalPHNum5"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPHNum6"].ToString()!="")
				{
					model.TotalPHNum6=int.Parse(ds.Tables[0].Rows[0]["TotalPHNum6"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPHNum7"].ToString()!="")
				{
					model.TotalPHNum7=int.Parse(ds.Tables[0].Rows[0]["TotalPHNum7"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPNum1"].ToString()!="")
				{
					model.TotalPNum1=int.Parse(ds.Tables[0].Rows[0]["TotalPNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPNum2"].ToString()!="")
				{
					model.TotalPNum2=int.Parse(ds.Tables[0].Rows[0]["TotalPNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPNum3"].ToString()!="")
				{
					model.TotalPNum3=int.Parse(ds.Tables[0].Rows[0]["TotalPNum3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPNum4"].ToString()!="")
				{
					model.TotalPNum4=int.Parse(ds.Tables[0].Rows[0]["TotalPNum4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPNum5"].ToString()!="")
				{
					model.TotalPNum5=int.Parse(ds.Tables[0].Rows[0]["TotalPNum5"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TotalPNum6"].ToString()!="")
				{
					model.TotalPNum6=int.Parse(ds.Tables[0].Rows[0]["TotalPNum6"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EmploymentNum"].ToString()!="")
				{
					model.EmploymentNum=int.Parse(ds.Tables[0].Rows[0]["EmploymentNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EmploymentNum1"].ToString()!="")
				{
					model.EmploymentNum1=int.Parse(ds.Tables[0].Rows[0]["EmploymentNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EmploymentNum2"].ToString()!="")
				{
					model.EmploymentNum2=int.Parse(ds.Tables[0].Rows[0]["EmploymentNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EmploymentRate"].ToString()!="")
				{
					model.EmploymentRate=decimal.Parse(ds.Tables[0].Rows[0]["EmploymentRate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TrainYearTimes"].ToString()!="")
				{
					model.TrainYearTimes=int.Parse(ds.Tables[0].Rows[0]["TrainYearTimes"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TrainYearNum"].ToString()!="")
				{
					model.TrainYearNum=int.Parse(ds.Tables[0].Rows[0]["TrainYearNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RecommendYearTimes"].ToString()!="")
				{
					model.RecommendYearTimes=int.Parse(ds.Tables[0].Rows[0]["RecommendYearTimes"].ToString());
				}
				if(ds.Tables[0].Rows[0]["RecommendYearNum"].ToString()!="")
				{
					model.RecommendYearNum=int.Parse(ds.Tables[0].Rows[0]["RecommendYearNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SecurityNum1"].ToString()!="")
				{
					model.SecurityNum1=int.Parse(ds.Tables[0].Rows[0]["SecurityNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SecurityNum2"].ToString()!="")
				{
					model.SecurityNum2=int.Parse(ds.Tables[0].Rows[0]["SecurityNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SecurityNum3"].ToString()!="")
				{
					model.SecurityNum3=int.Parse(ds.Tables[0].Rows[0]["SecurityNum3"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SecurityNum4"].ToString()!="")
				{
					model.SecurityNum4=int.Parse(ds.Tables[0].Rows[0]["SecurityNum4"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SuspectNum"].ToString()!="")
				{
					model.SuspectNum=int.Parse(ds.Tables[0].Rows[0]["SuspectNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EmphasisNum"].ToString()!="")
				{
					model.EmphasisNum=int.Parse(ds.Tables[0].Rows[0]["EmphasisNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["FertilityNum"].ToString()!="")
				{
					model.FertilityNum=int.Parse(ds.Tables[0].Rows[0]["FertilityNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PreschoolNum"].ToString()!="")
				{
					model.PreschoolNum=int.Parse(ds.Tables[0].Rows[0]["PreschoolNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BabyNum"].ToString()!="")
				{
					model.BabyNum=int.Parse(ds.Tables[0].Rows[0]["BabyNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BabyNum1"].ToString()!="")
				{
					model.BabyNum1=int.Parse(ds.Tables[0].Rows[0]["BabyNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["BabyNum2"].ToString()!="")
				{
					model.BabyNum2=int.Parse(ds.Tables[0].Rows[0]["BabyNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PartyBranchNum"].ToString()!="")
				{
					model.PartyBranchNum=int.Parse(ds.Tables[0].Rows[0]["PartyBranchNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PartyNum"].ToString()!="")
				{
					model.PartyNum=int.Parse(ds.Tables[0].Rows[0]["PartyNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PartyNum1"].ToString()!="")
				{
					model.PartyNum1=int.Parse(ds.Tables[0].Rows[0]["PartyNum1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PartyNum2"].ToString()!="")
				{
					model.PartyNum2=int.Parse(ds.Tables[0].Rows[0]["PartyNum2"].ToString());
				}
				if(ds.Tables[0].Rows[0]["PartyNum3"].ToString()!="")
				{
					model.PartyNum3=int.Parse(ds.Tables[0].Rows[0]["PartyNum3"].ToString());
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
			strSql.Append("select StatID,StatType,StreetCode,CommCode,StatDate,TotalHNum,TotalHNum1,TotalHNum2,TotalHNum3,TotalHNum4,TotalNum,TotalNum1,TotalNum2,TotalNum3,TotalPHNum1,TotalPHNum2,TotalPHNum3,TotalPHNum4,TotalPHNum5,TotalPHNum6,TotalPHNum7,TotalPNum1,TotalPNum2,TotalPNum3,TotalPNum4,TotalPNum5,TotalPNum6,EmploymentNum,EmploymentNum1,EmploymentNum2,EmploymentRate,TrainYearTimes,TrainYearNum,RecommendYearTimes,RecommendYearNum,SecurityNum1,SecurityNum2,SecurityNum3,SecurityNum4,SuspectNum,EmphasisNum,FertilityNum,PreschoolNum,BabyNum,BabyNum1,BabyNum2,PartyBranchNum,PartyNum,PartyNum1,PartyNum2,PartyNum3 ");
			strSql.Append(" FROM Tb_Sys_TakePersonPic ");
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
			strSql.Append(" StatID,StatType,StreetCode,CommCode,StatDate,TotalHNum,TotalHNum1,TotalHNum2,TotalHNum3,TotalHNum4,TotalNum,TotalNum1,TotalNum2,TotalNum3,TotalPHNum1,TotalPHNum2,TotalPHNum3,TotalPHNum4,TotalPHNum5,TotalPHNum6,TotalPHNum7,TotalPNum1,TotalPNum2,TotalPNum3,TotalPNum4,TotalPNum5,TotalPNum6,EmploymentNum,EmploymentNum1,EmploymentNum2,EmploymentRate,TrainYearTimes,TrainYearNum,RecommendYearTimes,RecommendYearNum,SecurityNum1,SecurityNum2,SecurityNum3,SecurityNum4,SuspectNum,EmphasisNum,FertilityNum,PreschoolNum,BabyNum,BabyNum1,BabyNum2,PartyBranchNum,PartyNum,PartyNum1,PartyNum2,PartyNum3 ");
			strSql.Append(" FROM Tb_Sys_TakePersonPic ");
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
			parameters[5].Value = "SELECT * FROM Tb_Sys_TakePersonPic WHERE 1=1 " + StrCondition;
			parameters[6].Value = "StatID";
			DataSet Ds=DbHelperSQL.RunProcedure("Proc_System_TurnPage",parameters,"RetDataSet");
			PageCount = Convert.ToInt32(parameters[7].Value);
			Counts = Convert.ToInt32(parameters[8].Value);
			return Ds;
		}

		#endregion  成员方法
	}
}

