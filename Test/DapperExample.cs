using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Data.SqlClient;
using DapperExtensions;

namespace Test
{
    public partial class DapperExample : Form
    {
        public static string ConnStr = "Pooling=false;Data Source=(local);Initial Catalog=Dapper;User ID=sa;Password=110";
        public DapperExample()
        {
            InitializeComponent();
        }

        private void button60_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            string Query = "INSERT INTO User(Id,Name,Sex) VALUES(@Id,@Name,@Sex)";
            Conn.Execute(Query, new { Id = Guid.NewGuid().ToString(), Name = "艾中", Sex = "男性" });
        }

        private void button62_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            string Query = "DELETE FROM Test WHERE Name=@Name";
            Conn.Execute(Query, new { Name = "艾中" });
        }

        private void button63_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            string Query = "UPDATE User SET Sex='姓性' WHERE Name=@Name";
            Conn.Execute(Query, new { Name = "艾中" });
        }

        private void button64_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            string query = "SELECT * FROM [User] WHERE Name=@Name";
            User T = Conn.Query<User>(query, new { Name = "艾中" }).SingleOrDefault();
            UserName.Text = T.Name.ToString();

            Result.Text = MobileSoft.Common.JSONHelper.FromObject(T);
        }

        private void button66_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            string query = "SELECT * FROM [User] A LEFT OUTER JOIN Book B ON A.Id=B.UserId WHERE Name=@Name";
            UserBook T = Conn.Query<UserBook>(query, new { Name = "艾中" }).SingleOrDefault();
            UserName.Text = T.Name.ToString();
            Sex.Text = T.Sex.ToString();
            BookName.Text = T.BookName.ToString();
        }

        private void button67_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            var sql =
                        @"
                        select * from [User] where Id = @id
                        select * from Book where UserId = @id";

            using (var multi = Conn.QueryMultiple(sql, new { id = "94597e51-7a40-447a-8ddb-76ae8e4dc6c1" }))
            {
                var User = multi.Read<User>().Single();
                var Book = multi.Read<Book>().Single();

                UserName.Text = User.Name.ToString();
                Sex.Text = User.Sex.ToString();
                BookName.Text = Book.BookName.ToString();
            }
        }

        private void button68_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            List<UserBook> users = new List<UserBook>();
            using (IDbConnection cnn = Conn)
            {
                users = cnn.Query<UserBook>("dbo.Proc_UserBook",
                                        new { Name = "艾中" },
                                        null,
                                        true,
                                        null,
                                        CommandType.StoredProcedure).ToList();
            }
            
            UserName.Text = users[0].Name;
            Sex.Text = users[0].Sex.ToString();
            BookName.Text = users[0].BookName.ToString();
            //存储过程返回值
            //DynamicParameters dp = new DynamicParameters();
            //dp.Add("@RoleId", "1");
            //dp.Add("@RoleName", "", DbType.String, ParameterDirection.Output);
            //using (IDbConnection con = OpenConnection())
            //{
            //    con.Execute("QueryRoleWithParms", dp, null, null, CommandType.StoredProcedure);
            //    string roleName = dp.Get<string>("@RoleName");
            //}
        }

        private void button69_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Name", "艾中");
            DataTable Dt = new DataTable();
            //using (IDbConnection con = Conn)
            //{
            //    DataSet Ds = con.ExecuteReader("dbo.Proc_UserBook", dp, null, null, CommandType.StoredProcedure).ToDataSet();
            //    Dt = Ds.Tables[0];
            //    DataRow Row = Dt.Rows[0];
            //    UserName.Text = Row["Name"].ToString();
            //    Sex.Text = Row["Sex"].ToString();
            //    BookName.Text = Row["BookName"].ToString();
            //}

        }

        private void DapperExample_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            User P = new User();
            P.Id = Guid.NewGuid().ToString();
            P.Name = "陈雯";
            P.Sex = "女";
            Conn.Insert(P);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            using (Conn)
            {
                Conn.Open();
                IDbTransaction transaction = Conn.BeginTransaction();
                try
                {
                    string query = "UPDATE Book SET BookName=@BookName WHERE UserId = @UserId";
                    string query2 = "UPDATE [User] SET Name=@Name WHERE Id = @Id";
                    Conn.Execute(query, new { BookName="测试",UserId = "1" }, transaction, null, null);
                    Conn.Execute(query2, new { Name="测试",Id = "2"}, transaction, null, null);
                    //提交事务
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    //出现异常，事务Rollback
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IDbConnection Conn = new SqlConnection(ConnStr);
            string query = "SELECT * FROM [User] WHERE Name=@Name";
            User T = Conn.Query<User>(query, new { Name = "艾中" }).SingleOrDefault();
            UserName.Text = T.Name.ToString();

            var taskResult = Conn.Query(query, new
            {
                Name = "艾中"
            });

            foreach (dynamic taskItem in taskResult)
            {
                
            }
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }

    }

    public class Book
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string BookName { get; set; }

    }

    public class UserBook
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public string BookName { get; set; }

    }
}
