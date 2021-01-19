using Business;
using MobileSoft.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //new Business.CommunityManage_YD().TwCommIdAU();
            //new Business.CarParkManage_YD().CarParkManageAU();//车位

            //new Business.RegionManage_YD().RegionManageAU();//组团
            //new Business.CustomerManage_YD().CustomerManageAU();//业主
            //new Business.RoomManage_YD().RoomManageAU();//房屋
            //new Business.BuildingManage_YD().BuildingManageAU();//楼宇

            //new Business.CallCenter_JF_IVR();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string tw2bsConnectionString = "Pooling=false;Data Source=192.168.8.184,8433;Initial Catalog=tw2_bs;User ID=LFUser;Password=LF123SPoss";
            //IncidentAcceptProPush.SynchPushIndoorIncidentDispatch(tw2bsConnectionString, "1862", "186253", "95409fc5-866e-47a4-b8d8-39dc20a1f167", "18620653552");
        }
    }
}
