using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        static List<string> flist = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = textBox1.Text;

                GetAllDir(path);
                label3.Text = flist.Count.ToString();

            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
            

        }

        public static void GetAllDir(string dir1)
        {
            List<string> list = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(dir1);
            DirectoryInfo[] dirinfo = dir.GetDirectories();

            FileInfo[] fileinfo = dir.GetFiles();

            for (int i = 0; i < fileinfo.Length; i++)
            {
                // flist.Add(fileinfo[i].FullName);
                string sql = "insert into aaaaaafiles(FilePath) values('"+ fileinfo[i].FullName + "')";
                new DbHelperSQLP("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=100;Min Pool Size=0;Pooling=true;data source=192.168.5.180;initial catalog=HM_wygl_new_1329;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096").ExecuteSql(sql); 
            }

            for (int i = 0; i < dirinfo.Length; i++)
            {
                GetAllDir(dirinfo[i].FullName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                string sql1 = "select * from  aaaaaafiles";

                DataTable dt1 = new DbHelperSQLP("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=100;Min Pool Size=0;Pooling=true;data source=192.168.5.180;initial catalog=HM_wygl_new_1329;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096").Query(sql1).Tables[0];

                foreach (DataRow dr in dt1.Rows)
                {
                    string item = dr["FilePath"].ToString();
                    string filename = item.Substring(item.LastIndexOf("\\"), item.Length - item.LastIndexOf("\\"));
                    filename = filename.Substring(1);
                    string sql = "select * from Tb_EQ_EquipmentFile where FilePath like '%" + filename + "%' order by PhotoTime asc";
                    DataTable dt = new DbHelperSQLP("Connect Timeout=60;Connection Lifetime=60;Max Pool Size=100;Min Pool Size=0;Pooling=true;data source=192.168.5.180;initial catalog=HM_wygl_new_1329;PWD=LF123SPoss;persist security info=False;user id=LFUser;packet size=4096").Query(sql).Tables[0];
                    if (dt.Rows.Count == 1)
                    {
                        lblcg.Text = (int.Parse(lblcg.Text) + 1).ToString();

                        // string str = "http://wyerp.radiance.com.cn/HM/M_main/images/Quality//132954/2018/5/4/2018050414015251717.jpg";
                        string str = dt.Rows[0]["FilePath"].ToString();
                        str = str.Replace("http://wyerp.radiance.com.cn/HM/M_main/images/Quality//", "");
                        str = str.Substring(0, str.LastIndexOf("/"));

                        if (!Directory.Exists(textBox2.Text + "\\" + str))
                        {
                            Directory.CreateDirectory(textBox2.Text + "\\" + str);
                        }
                        File.Copy(item, textBox2.Text + "\\" + str + "\\" + item.Substring(item.LastIndexOf("\\"), item.Length - item.LastIndexOf("\\")));
                    }
                    else
                    {
                        lblcf.Text = (int.Parse(lblcf.Text) + 1).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
            }

        }
    }
}
