using MobileSoft.Model.Unified;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Business
{
   internal class SmsInfo
    {
        public static Tb_Sms_Account GetSms_Account()
        {
            IDbConnection Connectionstr = new SqlConnection(Connection.GetConnection("4"));
            
            string Sql = "SELECT top 1 * FROM Tb_Sms_Account";
            Tb_Sms_Account T_A = Connectionstr.Query<Tb_Sms_Account>(Sql).FirstOrDefault();
            if (T_A == null)
                T_A = new Tb_Sms_Account()
                {
                    Id = "1001",
                    Sign = "【天问互联】",
                    SmsAccount = "AD00190",
                    SmsPwd = "12345678",
                    SmsAddress = "http://sms.ht3g.com/sms.aspx",
                    SmsUserId = 992
                };
            return T_A;
        }
    }
}
