using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using MobileSoft.Common;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Cache;
using System.Security.Cryptography.X509Certificates;

namespace Test
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"D:\newwwwroot\天问接口\TWInterface\Test\bin\Debug\RoomJson.txt", Encoding.Default);
            String line;
            string strROom = "";
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
                strROom = strROom + line;
            }
            List<HouseEntity> Ls = JsonConvert.DeserializeObject<List<HouseEntity>>(strROom);
            TwRoomService(Ls);


        }
        public string TwRoomService(List<HouseEntity> ModelList)
        {

            #region 接受参数
            int iCorpID = 0;
            string strErrMsg = "";
            string strCon = "";
            string strReturn = "";
            int iCommID = 0;
            #endregion

            #region 变量定义

            int IsSucc = 0;
            string SQLContionString = "";
            StringBuilder sListContent = new StringBuilder("");

            long RoomID = 0;
            long CommID = 0;
            string RoomSign = "";
            string RoomName = ""; ;
            int RegionSNum = 0;
            int BuildSNum = 0;
            int UnitSNum = 0;
            int FloorSNum = 0;
            int RoomSNum = 0;
            string RoomModel = "";
            string RoomType = "";
            string PropertyRights = "";
            string RoomTowards = "";
            decimal BuildArea = 0;
            decimal InteriorArea = 0;
            decimal CommonArea = 0;
            string RightsSign = "";
            string PropertyUses = "";
            int RoomState = 1;
            long ChargeTypeID = 0;
            int UsesState = 0;
            string FloorHeight = "";
            string BuildStructure = "";
            decimal PoolRatio = 0;
            string BearParameters = "";
            string Renovation = "";
            string Configuration = "";
            string Advertising = "";
            int IsDelete = 0;
            int IsSplitUnite = 0;
            decimal GardenArea = 0;
            decimal PropertyArea = 0;
            int AreaType = 0;
            decimal YardArea = 0;
            long BedTypeID = 0;
            int UseType = 0;
            DateTime getHouseStartDate = DateTime.Now;
            DateTime getHouseEndDate = DateTime.Now;
            string SaleState = "";
            int PayState = 0;
            //DateTime ContSubDate = DateTime.Now;
            //DateTime TakeOverDate = DateTime.Now;
            //DateTime ActualSubDate = DateTime.Now;
            //DateTime FittingTime = DateTime.Now;
            //DateTime StayTime = DateTime.Now;
            //DateTime PayBeginDate = DateTime.Now;
            string ContractSign = "";
            string BuildsRenovation = "";

            #endregion

            #region 获取链接字符串
           
            #region 验证小区名称

            //string strSQLCommName = " and CommName = '" + strCommName + "' and isnull(IsDelete,0) = 0 ";

            //DataTable dTableComm = (new Business.TWBusinRule(strCon)).HSPR_Community_Filter(strSQLCommName);

            //if (dTableComm.Rows.Count > 0)
            //{
            //    DataRow DRowCorp = dTableComm.Rows[0];
            //    iCorpID = AppGlobal.StrToInt(DRowCorp["CorpID"].ToString());
            //    iCommID = AppGlobal.StrToInt(DRowCorp["CommID"].ToString());
            //}
            //dTableComm.Dispose();
            iCommID = 175545;
            #endregion
            SQLContionString = "Pooling=false;Data Source=125.64.16.12,8433;Initial Catalog=HM_NEW_TEST;User ID=LFUser;Password=LF123SPoss";
            #endregion

            if (!string.IsNullOrEmpty(SQLContionString))
            {
                #region 主业务
                try
                {
                    

                    DataTable dTable = (new Business.TWBusinRule(SQLContionString)).HSPR_Building_Filter(" and CommID = " + iCommID + " and  IsDelete = 0 ");


                    //DataTable dTableModel = FillDataTable(ModelList);//接收实体类转换成DataTable
                    #region 遍历List<HouseEntity> ModelList

                    foreach (HouseEntity model in ModelList)
                    {

                        RoomID = (new Business.TWBusinRule(SQLContionString)).HSPR_Room_GetMaxNum(iCommID, "");
                        CommID = iCommID;
                        //BuildSNum = TempBuildSNum += 1;
                        RegionSNum = 0;
                        UnitSNum = AppGlobal.StrToInt(model.Number.Substring(model.Number.Length - 6, 1));
                        FloorSNum = AppGlobal.StrToInt(model.Number.Substring(model.Number.Length - 4, 2)); ;
                        RoomSNum = AppGlobal.StrToInt(model.Number.Substring(model.Number.Length - 2));
                        RoomSign = model.Number;//房屋编号
                        RoomName = model.Number;//明源房间名称
                                                   //RoomName = model.Number;//房屋名称
                        PropertyRights = "自有产权";//产权性质
                        BuildArea = model.BuildArea;//产权面积
                        InteriorArea = model.ForecastSetArea;//套内面积
                        string ActualSubDate = DateTime.Now.ToString();//明源交楼时间
                        try
                        {
                            ActualSubDate = DateTime.Parse(model.FactDate.ToString()).ToString();

                        }
                        catch
                        { ActualSubDate = DateTime.Now.ToString(); }
                        if (DateTime.Parse(ActualSubDate).Year < 2000)
                        {
                            ActualSubDate = DateTime.Now.ToString();
                        }
                  
                        //PayBeginDate = model.FactDate;//明源交楼时间也是开始收费时间
                        CommonArea = 0; //公摊面积
                        PropertyUses = model.Purpose;//使用性质
                                                     //if (model.Purpose != "住宅")
                                                     //{ RoomState = 3; }//使用状态
                        UsesState = 1;
                        BuildsRenovation = model.HouseName; //保存明源房屋主键
                        string str = "buildName = '" + model.BuildNumber.ToString() + "' ";
                        DataRow DR = dTable.Select(str)[0];
                        if (DR.Table.Rows.Count > 0)
                        {
                            BuildSNum = AppGlobal.StrToInt(DR["BuildSNum"].ToString());
                        }
                        else
                        {
                            BuildSNum = 0;
                        }


                        #region 插入房屋信息
                        (new Business.TWBusinRule(SQLContionString)).HSPR_Room_Insert(RoomID, CommID, RoomSign, RoomName, RegionSNum, BuildSNum, UnitSNum, FloorSNum, RoomSNum, RoomModel, RoomType, PropertyRights, RoomTowards, BuildArea, InteriorArea, CommonArea,
                           RightsSign, PropertyUses,null, ChargeTypeID, UsesState, FloorHeight, BuildStructure, PoolRatio, BearParameters, Renovation, Configuration, Advertising, IsDelete, IsSplitUnite, GardenArea,
                           PropertyArea, AreaType, YardArea, BedTypeID, UseType, getHouseStartDate, getHouseEndDate, SaleState, PayState,null,null, ActualSubDate,null,null,null, ContractSign, BuildsRenovation);

                        #endregion

                    }
                    #endregion 

                    //strReturn = "房屋更新成功!";
                    //strReturn = strReturn + " IsSucc=1";
                }
                catch (Exception ex)
                {
                    strErrMsg = ex.Message.ToString();
                    strReturn = "strErrMsg:" + strErrMsg + ";" + sListContent.ToString() + "IsSucc=" + IsSucc;
                }
                #endregion
            }
            else
            {
                strErrMsg += "链接字符串异常或为空！";
                strReturn = strErrMsg;
            }
            return strReturn;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"D:\newwwwroot\天问接口\TWInterface\Test\bin\Debug\CustJson.txt", Encoding.Default);
            String line;
            string strROom = "";
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
                strROom = strROom + line;
            }
            // ToDataTableTwo(strROom);
            List<CustomerEntity> LsCust = new List<CustomerEntity>();

           List<Dictionary<string, object>> Ls = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(strROom);
            foreach (Dictionary<string, object> item in Ls)
            {
                CustomerEntity Ce = new CustomerEntity();

                foreach (string current in item.Keys)
                {
                  

                    if (current == "CustomerNumber")
                    {
                        Ce.CustomerNumber = item[current].ToString();
                    }
                    if (current == "OrganizationNumber")
                    {
                        Ce.OrganizationNumber = item[current].ToString();

                    }
                    if (current == "Name")
                    {
                        Ce.Name = item[current].ToString();
                    }
                    if (current == "CustomerType")
                    {
                        Ce.CustomerType = item[current].ToString();
                    }
                    if (current == "CertificateType")
                    {
                        Ce.CertificateType = item[current].ToString();
                    }
                    if (current == "PersonID")
                    {
                        Ce.PersonID = item[current].ToString();
                    }
                    if (current == "Mobile")
                    {
                        Ce.Mobile = item[current].ToString();
                    }
                    if (current == "Phone")
                    {
                        Ce.Phone = item[current].ToString();

                    }
                    if (current == "Sex")
                    {
                        Ce.Sex = item[current].ToString();
                    }
                    if (current == "IsOwner")
                    {
                        Ce.IsOwner = bool.Parse(item[current].ToString());
                    }
                 
                    //"OrganizationNumber": "NJWY.HZYY",
                    //"Name": "姚丽香",
                    //"CustomerType": "002",
                    //"CertificateType": "身份证",
                    //"PersonID": "330184199012023921",
                    //"Address": "杭州市余杭区西溪北苑29-1-402室",
                    //"Mobile": "15968192980",
                    //"Phone": "13958166838",
                    //"Sex": "女",
                    //"IsOwner": "true",


                }
                LsCust.Add(Ce);
            }
           
             TwCustomerService(LsCust);
        }

        public static DataTable ToDataTableTwo(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                
                ArrayList arrayList = JsonConvert.DeserializeObject<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        //Columns
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        //Rows
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            result = dataTable;
            return result;
        }


        public string TwCustomerService( List<CustomerEntity> ModelList)
        {
            #region 接受参数
            int iCorpID = 1000;
            string strErrMsg = "";
            string strCon = "";
            string strReturn = "";
            int iCommID = 0;
            #endregion

            #region 变量定义

            int IsSucc = 0;
            string SQLContionString = "";
            StringBuilder sListContent = new StringBuilder("");

            long CustID = 0;
            int CommID = 0;
            long CustTypeID = 0;
            string CustName = "";
            string FixedTel = "";
            string MobilePhone = "";
            string FaxTel = "";
            string EMail = "";
            string Surname = "";
            string Name = "";
            string Sex = "";
            DateTime Birthday = System.DateTime.Now;
            string Nationality = "";
            string WorkUnit = "";
            string PaperName = "";
            string PaperCode = "";
            string PassSign = "";
            string LegalRepr = "";
            string LegalReprTel = "";
            string Charge = "";
            string ChargeTel = "";
            string Linkman = "";
            string LinkmanTel = "";
            string BankName = "";
            string BankIDs = "";
            string BankAccount = "";
            string BankAgreement = "";
            string InquirePwd = "";
            string InquireAccount = "";
            string Memo = "";
            int IsUnit = 0;
            int IsDelete = 0;
            string Address = "";
            string PostCode = "";
            string Recipient = "";
            string Hobbies = "";
            string Job = "";
            DateTime SendCardDate = System.DateTime.Now;
            int IsSendCard = 0;

            long CustomerRoomID = 0;
            string HouseNumber = "";
            long LiveID = 0;
            int LiveType = 1;
            #endregion

            #region 获取链接字符串
            //try
            //{
            //    strCon = Global_Fun.GetConnectionString("SQLConnection");
            //}
            //catch (Exception ex)
            //{
            //    strReturn = ex.Message.ToString();
            //}
            #region 验证小区名称

            //string strSQLCommName = " and CommName = '" + strCommName + "' and isnull(IsDelete,0) = 0 ";

            //DataTable dTableComm = (new Business.TWBusinRule(strCon)).HSPR_Community_Filter(strSQLCommName);

            //if (dTableComm.Rows.Count > 0)
            //{
            //    DataRow DRowCorp = dTableComm.Rows[0];
            //    iCorpID = AppGlobal.StrToInt(DRowCorp["CorpID"].ToString());
            //    iCommID = AppGlobal.StrToInt(DRowCorp["CommID"].ToString());
            //}  dTableComm.Dispose();
            #endregion
            SQLContionString = "Pooling=false;Data Source=125.64.16.12,8433;Initial Catalog=HM_NEW_TEST;User ID=LFUser;Password=LF123SPoss";
            iCommID = 175545;
            #endregion 

            if (!string.IsNullOrEmpty(SQLContionString))
            {
                #region 主业务
                try
                {





                    DataTable dTableRoom = (new Business.TWBusinRule(SQLContionString)).HSPR_Room_Filter("and CommID = " + iCommID + " and IsDelete = 0");


                    #region 遍历List<CustomerEntity> ModelList
                    foreach (CustomerEntity model in ModelList)
                    {
                        CustID = (new Business.TWBusinRule(SQLContionString)).HSPR_Customer_GetMaxNum(iCommID, "");

                        CommID = iCommID;
                        CustName = model.Name;
                        MobilePhone = model.Mobile;
                        PaperName = model.CertificateType;
                        PaperCode = model.PersonID;
                        LinkmanTel = model.Phone;

                        //CustomerHouseEntity[] CustomerHouseEntity = model.CustomerHouses;

                        CustomerRoomID = AppGlobal.StrToLong(dTableRoom.Select("RoomSign='" + model.CustomerNumber + "'")[0]["RoomID"].ToString());
                     
                        LiveID = AppGlobal.StrToLong(BulidAutoDateCode(4));
                        (new Business.TWBusinRule(SQLContionString)).HSPR_CustomerLive_InsUpdate(LiveID, CustomerRoomID,iCommID, CustID, LiveType);
                        //if (dTableRoom.Rows.Count > 0)
                        //{
                        //    foreach (var item in CustomerHouseEntity)
                        //    {
                        //        HouseNumber = item.HouseNumber;//通过这个字段匹配房间表(and BuildsRenovation =item.HouseNumber) 取RoomID
                        //        if (dTableRoom.Select("RoomSign='" + HouseNumber + "'").Length > 0)
                        //        {

                        //            #endregion
                        //        }
                        //    }
                        //}
                        (new Business.TWBusinRule(SQLContionString)).HSPR_Customer_Insert(CustID, CommID, CustTypeID, CustName, FixedTel, MobilePhone, FaxTel, EMail, Surname, Name, Sex, Birthday, Nationality, WorkUnit, PaperName, PaperCode, PassSign, LegalRepr, LegalReprTel, Charge, ChargeTel, Linkman, LinkmanTel, BankName, BankIDs, BankAccount, BankAgreement, InquirePwd, InquireAccount, Memo, IsUnit, IsDelete, Address, PostCode, Recipient, Hobbies, Job, SendCardDate, IsSendCard);


                    }
                    #endregion 

                    //strReturn = "房屋更新成功!";
                    //strReturn = strReturn + " IsSucc=1";
                }
                catch (Exception ex)
                {
                    strErrMsg = ex.Message.ToString();
                    strReturn = "strErrMsg:" + strErrMsg + ";" + sListContent.ToString() + "IsSucc=" + IsSucc;
                }
                #endregion
            }
            else
            {
                strErrMsg += "链接字符串异常或为空！";
                strReturn = strErrMsg;
            }
            return strReturn;
        }

        private string BulidAutoDateCode(int length)
        {
            System.Threading.Thread.Sleep(5);
            System.DateTime now = System.DateTime.Now;
            string strRe = now.ToString("yyyyMMddhhmmss") + BulidAutoCode("0123456789", length);

            return strRe;
        }
        private string BulidAutoCode(string seed, int length)
        {
            //申明变量
            string outRandomSting = "";
            string strSeed = seed;
            int seedLen;	//= seed.Length;
            int len = length;
            //处理变量
            if (strSeed == null || strSeed.Trim() == "")
            {
                strSeed = "0123456789";
                seedLen = strSeed.Length;
            }
            else
            {
                seedLen = strSeed.Length;
            }

            //开始产生要求长度的随机字符串
            while (len > 0)
            {
                //线程阻滞 10 毫秒后产生随机数,因为这里采用与时间相关的默认种子
                System.Threading.Thread.Sleep(10);
                Random rm = new Random();
                //rm.Next(min,max)是包括 [min,max) 的半开半闭区间
                outRandomSting += strSeed.Substring(rm.Next(0, seedLen), 1);
                len--;
            }

            // 11
            return outRandomSting;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HMCRM.HJ_Service hj = new HMCRM.HJ_Service();

            HMCRM.HouseEntity houseEntity = new HMCRM.HouseEntity();



            StreamReader sr = new StreamReader(@"D:\newwwwroot\天问接口\TWInterface\Test\bin\Debug\RoomJson.txt", Encoding.Default);
            String line;
            string strROom = "";
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
                strROom = strROom + line;
            }
            List<HMCRM.HouseEntity> Ls = JsonConvert.DeserializeObject<List<HMCRM.HouseEntity>>(strROom);
            int i = 0; 
            
            HMCRM.HouseEntity[] hs = Ls.ToArray();

            textBox1.Text = hj.TwRoomService("测试项目2", hs);

            //string str = "杭州映月-一期-HZYY-一期-16幢-1-0104";

            //string ss = str.Substring(str.Length - 6, 1);
           // MessageBox.Show("111");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HMCRM.HJ_Service hj = new HMCRM.HJ_Service();

            HMCRM.HouseEntity houseEntity = new HMCRM.HouseEntity();



            StreamReader sr = new StreamReader(@"D:\newwwwroot\天问接口\TWInterface\Test\bin\Debug\BuildJson.txt", Encoding.Default);
            String line;
            string strROom = "";
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
                strROom = strROom + line;
            }
            List<HMCRM.BuildEntity> Ls = JsonConvert.DeserializeObject<List<HMCRM.BuildEntity>>(strROom);
            int i = 0;

            HMCRM.BuildEntity[] hs = Ls.ToArray();

            textBox1.Text = hj.TwBuildingService("测试项目2", hs);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HMCRM.HJ_Service hj = new HMCRM.HJ_Service();

            HMCRM.CustomerMemberEntity CustomerMemberEntity = new HMCRM.CustomerMemberEntity();



            StreamReader sr = new StreamReader(@"D:\newwwwroot\天问接口\TWInterface\Test\bin\Debug\lmJosn.txt", Encoding.Default);
            String line;
            string strROom = "";
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
                strROom = strROom + line;
            }
            List<HMCRM.CustomerMemberEntity> Ls = JsonConvert.DeserializeObject<List<HMCRM.CustomerMemberEntity>>(strROom);
            int i = 0;

            HMCRM.CustomerMemberEntity[] hs = Ls.ToArray();

            textBox1.Text = hj.TwCustomerMemberService("杭州香悦", hs);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string url = tbInterfaceLoalURL.Text; //http://125.71.214.172:806/TWInterface/Service/HKService.ashx
            string param = "{\"parkId\":1,\"roomNum\":\"00900332232\",\"dutyId\":\"D_00201_01260_1548774133283\",\"tollManName\":\"xws001\",\"parkName\":\"曦望山\",\"startTime\":\"2019-01-29 23:02:13\",\"endTime\":\"2019-01-30 11:00:04\",\"duration\": 717,\"feeExpected\":100,\"feeCash\":20,\"feeAli\":10,\"feeWechat\":20,\"feeCoupon\":10,\"feeFree\":30,\"feeException\":10,\"sign\":\"86AC9CB396EB67420CF7908585D250C3\"}";
            //string param1= "{\"parkName\":\"æ¦æå±±\",\"tollManName\":\"cuiwenliang\",\"sign\":\"30a49770d4e00016a7d995e461dddac6\",\"feeExpected\":66.00,\"parkId\":223,\"feeWechat\":0,\"duration\":82992,\"roomNum\":\" \",\"feeAli\":0,\"feeCoupon\":0.00,\"feeException\":0.00,\"feeFree\":0.00,\"dutyId\":\"D_00223_00636_1561572010772\",\"startTime\":\"2019-06-27 02:00:11\",\"endTime\":\"2019-08-23 17:14:17\",\"feeCash\":30.00}";

            string ss = SendHttp(url, param);
            textBox1.Text = ss;
        }


        #region CheckValidationResult
        private bool CheckValidationResult(
            Object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors
        )
        {
            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != SslPolicyErrors.RemoteCertificateNotAvailable)
                return true;
            throw new Exception("SSL验证失败");
        }
        #endregion

        #region 发送http
        public string SendHttp(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = System.Text.Encoding.GetEncoding("utf-8");
            byte[] data = encoding.GetBytes(postData);
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
        }
        #endregion
    }
    #region 明源实体类

    #region 楼栋实体类
    /// <summary>
    /// 楼栋实体类
    /// </summary>
    public partial class BuildEntity
    {

        private string numberField;

        private string otherName1Field;

        private string communityNameField;

        private string otherName2Field;

        private string communityNumberField;

        private string nameField;

        private string saleNameField;

        private string streetField;

        private string addressField;

        private System.DateTime startBuildDateField;

        private System.DateTime joinDateField;

        private decimal planBuildAreaField;

        private decimal buildAreaField;

        private decimal overBuildAreaField;

        private decimal underBuildAreaField;

        private decimal houseNumField;

        private decimal planUseAreaField;

        private decimal useAreaField;

        private string purposeField;

        private string descriptionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OtherName1
        {
            get
            {
                return this.otherName1Field;
            }
            set
            {
                this.otherName1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CommunityName
        {
            get
            {
                return this.communityNameField;
            }
            set
            {
                this.communityNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OtherName2
        {
            get
            {
                return this.otherName2Field;
            }
            set
            {
                this.otherName2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CommunityNumber
        {
            get
            {
                return this.communityNumberField;
            }
            set
            {
                this.communityNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SaleName
        {
            get
            {
                return this.saleNameField;
            }
            set
            {
                this.saleNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Street
        {
            get
            {
                return this.streetField;
            }
            set
            {
                this.streetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime StartBuildDate
        {
            get
            {
                return this.startBuildDateField;
            }
            set
            {
                this.startBuildDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime JoinDate
        {
            get
            {
                return this.joinDateField;
            }
            set
            {
                this.joinDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal PlanBuildArea
        {
            get
            {
                return this.planBuildAreaField;
            }
            set
            {
                this.planBuildAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal BuildArea
        {
            get
            {
                return this.buildAreaField;
            }
            set
            {
                this.buildAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal OverBuildArea
        {
            get
            {
                return this.overBuildAreaField;
            }
            set
            {
                this.overBuildAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal UnderBuildArea
        {
            get
            {
                return this.underBuildAreaField;
            }
            set
            {
                this.underBuildAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal HouseNum
        {
            get
            {
                return this.houseNumField;
            }
            set
            {
                this.houseNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal PlanUseArea
        {
            get
            {
                return this.planUseAreaField;
            }
            set
            {
                this.planUseAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal UseArea
        {
            get
            {
                return this.useAreaField;
            }
            set
            {
                this.useAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Purpose
        {
            get
            {
                return this.purposeField;
            }
            set
            {
                this.purposeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }
    #endregion

    #region 房间实体类
    /// <summary>
    /// 房间实体类
    /// </summary>
    public partial class HouseEntity
    {
        private string houseNameField;//地产原房屋名称

        private string simpleHouseNumField;

        private string buildNumberField;

        private string houseNumerField;//在明源业主集合里面有对应字段

        private string numberField;

        private string houseTakeStatusField;

        private System.DateTime openingDateField;

        private System.DateTime factDateField;

        private System.DateTime actualDateField;

        private decimal buildAreaField;

        private decimal actualBuiltupAreaField;

        private decimal forecastBuiltupAreaField;

        private decimal forecastSetAreaField;

        private decimal actualSetAreaField;

        private decimal gardenAreaField;

        private string houseShapeField;

        private string directionField;

        private string addressField;

        private string purposeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string HouseName
        {
            get
            {
                return this.houseNameField;
            }
            set
            {
                this.houseNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SimpleHouseNum
        {
            get
            {
                return this.simpleHouseNumField;
            }
            set
            {
                this.simpleHouseNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BuildNumber
        {
            get
            {
                return this.buildNumberField;
            }
            set
            {
                this.buildNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string HouseNumer
        {
            get
            {
                return this.houseNumerField;
            }
            set
            {
                this.houseNumerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string HouseTakeStatus
        {
            get
            {
                return this.houseTakeStatusField;
            }
            set
            {
                this.houseTakeStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime OpeningDate
        {
            get
            {
                return this.openingDateField;
            }
            set
            {
                this.openingDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime FactDate
        {
            get
            {
                return this.factDateField;
            }
            set
            {
                this.factDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime ActualDate
        {
            get
            {
                return this.actualDateField;
            }
            set
            {
                this.actualDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal BuildArea
        {
            get
            {
                return this.buildAreaField;
            }
            set
            {
                this.buildAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualBuiltupArea
        {
            get
            {
                return this.actualBuiltupAreaField;
            }
            set
            {
                this.actualBuiltupAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ForecastBuiltupArea
        {
            get
            {
                return this.forecastBuiltupAreaField;
            }
            set
            {
                this.forecastBuiltupAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ForecastSetArea
        {
            get
            {
                return this.forecastSetAreaField;
            }
            set
            {
                this.forecastSetAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal ActualSetArea
        {
            get
            {
                return this.actualSetAreaField;
            }
            set
            {
                this.actualSetAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal GardenArea
        {
            get
            {
                return this.gardenAreaField;
            }
            set
            {
                this.gardenAreaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string HouseShape
        {
            get
            {
                return this.houseShapeField;
            }
            set
            {
                this.houseShapeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Direction
        {
            get
            {
                return this.directionField;
            }
            set
            {
                this.directionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Purpose
        {
            get
            {
                return this.purposeField;
            }
            set
            {
                this.purposeField = value;
            }
        }
    }
    #endregion

    #region 业主(客户)实体类
    /// <summary>
    /// 业主(客户)实体类
    /// </summary>
    public partial class CustomerEntity
    {

        private CustomerHouses[] customerHouses;

        private string customerNumberField;

        private string organizationNumberField;

        private string nameField;

        private string customerTypeField;

        private string certificateTypeField;

        private string personIDField;

        private string addressField;

        private string mobileField;

        private string phoneField;

        private string phone2Field;

        private string emailField;

        private string workPlaceField;

        private string sexField;

        private string marryStateField;

        private string customerCountryField;

        private string degreeField;

        private string customerOccpField;

        private string corporationField;

        private string compTypeField;

        private string licenseNumField;

        private bool isOwnerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CustomerHouses")]
        public CustomerHouses[] CustomerHouses
        {
            get
            {
                return this.customerHouses;
            }
            set
            {
                this.customerHouses = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CustomerNumber
        {
            get
            {
                return this.customerNumberField;
            }
            set
            {
                this.customerNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OrganizationNumber
        {
            get
            {
                return this.organizationNumberField;
            }
            set
            {
                this.organizationNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CustomerType
        {
            get
            {
                return this.customerTypeField;
            }
            set
            {
                this.customerTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CertificateType
        {
            get
            {
                return this.certificateTypeField;
            }
            set
            {
                this.certificateTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PersonID
        {
            get
            {
                return this.personIDField;
            }
            set
            {
                this.personIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Mobile
        {
            get
            {
                return this.mobileField;
            }
            set
            {
                this.mobileField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Phone2
        {
            get
            {
                return this.phone2Field;
            }
            set
            {
                this.phone2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WorkPlace
        {
            get
            {
                return this.workPlaceField;
            }
            set
            {
                this.workPlaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Sex
        {
            get
            {
                return this.sexField;
            }
            set
            {
                this.sexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MarryState
        {
            get
            {
                return this.marryStateField;
            }
            set
            {
                this.marryStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CustomerCountry
        {
            get
            {
                return this.customerCountryField;
            }
            set
            {
                this.customerCountryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Degree
        {
            get
            {
                return this.degreeField;
            }
            set
            {
                this.degreeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CustomerOccp
        {
            get
            {
                return this.customerOccpField;
            }
            set
            {
                this.customerOccpField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Corporation
        {
            get
            {
                return this.corporationField;
            }
            set
            {
                this.corporationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CompType
        {
            get
            {
                return this.compTypeField;
            }
            set
            {
                this.compTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LicenseNum
        {
            get
            {
                return this.licenseNumField;
            }
            set
            {
                this.licenseNumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsOwner
        {
            get
            {
                return this.isOwnerField;
            }
            set
            {
                this.isOwnerField = value;
            }
        }
    }
    #endregion

    #region 明源客户房间列表实体类
    /// <summary>
    /// 明源客户房间列表实体类
    /// </summary>
    public partial class CustomerHouses
    {
        private string houseNumberField;

        private string buildNumberField;

        private System.DateTime inDateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string HouseNumber
        {
            get
            {
                return this.houseNumberField;
            }
            set
            {
                this.houseNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BuildNumber
        {
            get
            {
                return this.buildNumberField;
            }
            set
            {
                this.buildNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime InDate
        {
            get
            {
                return this.inDateField;
            }
            set
            {
                this.inDateField = value;
            }
        }
    }
    #endregion

    

    


    #endregion
}
