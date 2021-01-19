using Dapper;
using MobileSoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class IncidentAccept_bl : PubInfo
    {
        public IncidentAccept_bl()
        {
            base.Token = "20190606IncidentAccept_bl";
        }

        public override void Operate(ref Common.Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "IncidentAccept":              // 报事提交
                    Trans.Result = IncidentAccept(Row);
                    break;
                default:
                    break;
            }
        }

        private string IncidentAccept(DataRow row)
        {
            if (!row.Table.Columns.Contains("CommID") || string.IsNullOrEmpty(row["CommID"].ToString()))
            {
                return JSONHelper.FromString(false, "小区编号不能为空");
            }
            if (!row.Table.Columns.Contains("Content") || string.IsNullOrEmpty(row["Content"].ToString()))
            {
                return JSONHelper.FromString(false, "报事内容不能为空");
            }

            var commId = AppGlobal.StrToInt(row["CommID"].ToString());
            var content = row["Content"].ToString();

            var custId = default(long);
            var roomId = default(long);
            var images = default(string);

            if (row.Table.Columns.Contains("CustID") && !string.IsNullOrEmpty(row["CustID"].ToString()))
            {
                custId = AppGlobal.StrToLong(row["CustID"].ToString());
            }
            if (row.Table.Columns.Contains("RoomID") && !string.IsNullOrEmpty(row["RoomID"].ToString()))
            {
                roomId = AppGlobal.StrToLong(row["RoomID"].ToString());
            }
            if (row.Table.Columns.Contains("Images") && !string.IsNullOrEmpty(row["Images"].ToString()))
            {
                images = row["Images"].ToString();
            }

            using (var conn = new SqlConnection(Global_Fun.GetConnectionString("SQLConnection").ToString()))
            {
                var sql = @"DECLARE @IncidentID bigint;
                            DECLARE @IncidentNum nvarchar(20);
                            DECLARE @IncidentMan nvarchar(20),@Phone nvarchar(50);

                            SELECT @IncidentID = isnull(max(IncidentID)+1,(@CommID*CAST(1000000000000 AS bigint) +1))
                            FROM Tb_HSPR_IncidentAccept
                            WHERE CommID=@CommID;

                            EXEC Proc_HSPR_IncidentAssigned_GetCoordinateNum @CommID,3,'',@IncidentNum OUTPUT;

                            SELECT @IncidentMan=CustName,@Phone=isnull(MobilePhone,LinkmanTel) 
                            FROM Tb_HSPR_Customer WHERE CustID=@CustID;

                            INSERT INTO Tb_HSPR_IncidentAccept(IncidentID,CommID,CustID,RoomID,DrClass,IncidentNum,IncidentPlace,
                                IncidentMan,IncidentDate,IncidentMode,IncidentContent,Phone,AdmiMan,AdmiDate,IncidentImgs,
                                Duty,IncidentSource,BigCorpTypeID,BigCorpTypeCode)
                            VALUES(@IncidentID,@CommID,@CustID,@RoomID,1,@IncidentNum,'户内',@IncidentMan,getdate(),'小程序（安全生产月）',
                                @IncidentContent,@Phone,'保利微服务',getdate(),@Images,'物业类','客户报事',100000092,'000100030001');";

                var i = conn.Execute(sql, new { CommID = commId, CustID = custId, RoomID = roomId, IncidentContent = content, Images = images });

                if (i == 1)
                    return JSONHelper.FromString(true, "提交成功");

                return JSONHelper.FromString(false, "提交失败");
            }
        }
    }
}
