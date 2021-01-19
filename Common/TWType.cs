using System;

namespace TypeRule
{
    #region 居住类型(1业主、2租户、3临时)
    /// <summary>
    /// 居住类型(1业主、2租户、3临时)
    /// </summary>
    public sealed class TWLiveType
    {
        #region 私有变量

        private static int mOwner = 1;		//**业主
        private static int mClient = 2;		//**租户
        private static int mOTher = 3;		//**临时
        private static int mSplit = 4;      //**拆分客户
        private static int mMerger = 5;     //**合并客户
        private static int mHousehold = 6;     //**家庭成员

        private static string mOwnerName = "业主";
        private static string mClientName = "租户";
        private static string mOTherName = "临时";
        private static string mSplitName = "拆分客户";
        private static string mMergerName = "合并客户";
        private static string mHouseholdName = "家庭成员";

        #endregion

        public TWLiveType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID
        #region 业主
        /// <summary>
        /// 业主
        /// </summary>
        public static int Owner
        {
            set
            {
                mOwner = value;
            }
            get
            {
                return mOwner;
            }
        }


        #endregion

        #region 租户
        /// <summary>
        /// 租户
        /// </summary>
        public static int Client
        {
            set
            {
                mClient = value;
            }
            get
            {
                return mClient;
            }
        }


        #endregion

        #region 临时
        /// <summary>
        /// 临时
        /// </summary>
        public static int OTher
        {
            set
            {
                mOTher = value;
            }
            get
            {
                return mOTher;
            }
        }


        #endregion

        #region 拆分客户
        /// <summary>
        /// 拆分客户
        /// </summary>
        public static int Split
        {
            set
            {
                mSplit = value;
            }
            get
            {
                return mSplit;
            }
        }


        #endregion

        #region 合并客户
        /// <summary>
        /// 合并客户
        /// </summary>
        public static int Merger
        {
            set
            {
                mMerger = value;
            }
            get
            {
                return mMerger;
            }
        }


        #endregion

        #region 家庭成员
        /// <summary>
        /// 家庭成员
        /// </summary>
        public static int Household
        {
            set
            {
                mHousehold = value;
            }
            get
            {
                return mHousehold;
            }
        }


        #endregion


        #endregion

        #region 名称

        #region 业主
        /// <summary>
        /// 业主
        /// </summary>
        public static string OwnerName
        {
            set
            {
                mOwnerName = value;
            }
            get
            {
                return mOwnerName;
            }
        }


        #endregion

        #region 租户
        /// <summary>
        /// 租户
        /// </summary>
        public static string ClientName
        {
            set
            {
                mClientName = value;
            }
            get
            {
                return mClientName;
            }
        }


        #endregion

        #region 临时
        /// <summary>
        /// 临时
        /// </summary>
        public static string OTherName
        {
            set
            {
                mOTherName = value;
            }
            get
            {
                return mOTherName;
            }
        }


        #endregion

        #region 拆分客户
        /// <summary>
        /// 拆分客户
        /// </summary>
        public static string SplitName
        {
            set
            {
                mSplitName = value;
            }
            get
            {
                return mSplitName;
            }
        }


        #endregion

        #region 合并客户
        /// <summary>
        /// 合并客户
        /// </summary>
        public static string MergerName
        {
            set
            {
                mMergerName = value;
            }
            get
            {
                return mMergerName;
            }
        }


        #endregion

        #region 家庭成员
        /// <summary>
        /// 家庭成员
        /// </summary>
        public static string HouseholdName
        {
            set
            {
                mHouseholdName = value;
            }
            get
            {
                return mHouseholdName;
            }
        }


        #endregion


        #endregion

        #endregion

    }
    #endregion

    #region 使用状态(1产权人自用、2产权人出租)
    /// <summary>
    /// 使用状态(1产权人自用、2产权人出租)
    /// </summary>
    public sealed class TWUsesState
    {
        #region 私有变量
        private static int mHome = 1;		//**产权人自用
        private static int mRental = 2;		//**产权人出租		

        private static string mHomeName = "产权人自用";
        private static string mRentalName = "产权人出租";

        #endregion

        public TWUsesState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID
        #region 产权人自用
        /// <summary>
        /// 产权人自用
        /// </summary>
        public static int Home
        {
            set
            {
                mHome = value;
            }
            get
            {
                return mHome;
            }
        }


        #endregion

        #region 产权人出租
        /// <summary>
        /// 产权人出租
        /// </summary>
        public static int Rental
        {
            set
            {
                mRental = value;
            }
            get
            {
                return mRental;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 产权人自用
        /// <summary>
        /// 产权人自用
        /// </summary>
        public static string HomeName
        {
            set
            {
                mHomeName = value;
            }
            get
            {
                return mHomeName;
            }
        }


        #endregion

        #region 产权人出租
        /// <summary>
        /// 产权人出租
        /// </summary>
        public static string RentalName
        {
            set
            {
                mRentalName = value;
            }
            get
            {
                return mRentalName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 居住状态(0空置、1自用、2预定、3出租、4出售、5待租、6待售)
    /// <summary>
    /// 居住状态(0空置、1自用、2预定、3出租、4出售、5待租、6待售)
    /// </summary>
    public sealed class TWLiveState
    {

        #region 私有变量
        private static int mEmpty = 0;		//**空置
        private static int mStayed = 1;		//**自用
        private static int mDestine = 2;	//**预定
        private static int mLease = 3;		//**出租
        private static int mSale = 4;		//**出售
        private static int mWtLease = 5;	//**待租
        private static int mWtSale = 6;		//**待售		
        private static int mWtLive = 7;		//**租用

        private static string mEmptyName = "空置";
        private static string mStayedName = "自用";
        private static string mDestineName = "预定";
        private static string mLeaseName = "出租";
        private static string mSaleName = "出售";
        private static string mWtLeaseName = "待租";
        private static string mWtSaleName = "待售";
        private static string mWtLiveName = "租用";

        #endregion

        public TWLiveState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 空置
        /// <summary>
        /// 空置
        /// </summary>
        public static int Empty
        {
            set
            {
                mEmpty = value;
            }
            get
            {
                return mEmpty;
            }
        }


        #endregion

        #region 自用
        /// <summary>
        /// 自用
        /// </summary>
        public static int Stayed
        {
            set
            {
                mStayed = value;
            }
            get
            {
                return mStayed;
            }
        }


        #endregion

        #region 预定
        /// <summary>
        /// 预定
        /// </summary>
        public static int Destine
        {
            set
            {
                mDestine = value;
            }
            get
            {
                return mDestine;
            }
        }


        #endregion

        #region 出租
        /// <summary>
        /// 出租
        /// </summary>
        public static int Lease
        {
            set
            {
                mLease = value;
            }
            get
            {
                return mLease;
            }
        }


        #endregion

        #region 出售
        /// <summary>
        /// 出售
        /// </summary>
        public static int Sale
        {
            set
            {
                mSale = value;
            }
            get
            {
                return mSale;
            }
        }


        #endregion

        #region 待租
        /// <summary>
        /// 待租
        /// </summary>
        public static int WtLease
        {
            set
            {
                mWtLease = value;
            }
            get
            {
                return mWtLease;
            }
        }


        #endregion

        #region 待售
        /// <summary>
        /// 待售
        /// </summary>
        public static int WtSale
        {
            set
            {
                mWtSale = value;
            }
            get
            {
                return mWtSale;
            }
        }


        #endregion

        #region 租用
        /// <summary>
        /// 租用
        /// </summary>
        public static int WtLive
        {
            set
            {
                mWtLive = value;
            }
            get
            {
                return mWtLive;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 空置
        /// <summary>
        /// 空置
        /// </summary>
        public static string EmptyName
        {
            set
            {
                mEmptyName = value;
            }
            get
            {
                return mEmptyName;
            }
        }


        #endregion

        #region 自用
        /// <summary>
        /// 自用
        /// </summary>
        public static string StayedName
        {
            set
            {
                mStayedName = value;
            }
            get
            {
                return mStayedName;
            }
        }


        #endregion

        #region 预定
        /// <summary>
        /// 预定
        /// </summary>
        public static string DestineName
        {
            set
            {
                mDestineName = value;
            }
            get
            {
                return mDestineName;
            }
        }


        #endregion

        #region 出租
        /// <summary>
        /// 出租
        /// </summary>
        public static string LeaseName
        {
            set
            {
                mLeaseName = value;
            }
            get
            {
                return mLeaseName;
            }
        }


        #endregion

        #region 出售
        /// <summary>
        /// 出售
        /// </summary>
        public static string SaleName
        {
            set
            {
                mSaleName = value;
            }
            get
            {
                return mSaleName;
            }
        }


        #endregion

        #region 待租
        /// <summary>
        /// 待租
        /// </summary>
        public static string WtLeaseName
        {
            set
            {
                mWtLeaseName = value;
            }
            get
            {
                return mWtLeaseName;
            }
        }


        #endregion

        #region 待售
        /// <summary>
        /// 待售
        /// </summary>
        public static string WtSaleName
        {
            set
            {
                mWtSaleName = value;
            }
            get
            {
                return mWtSaleName;
            }
        }


        #endregion

        #region 租用
        /// <summary>
        /// 租用
        /// </summary>
        public static string WtLiveName
        {
            set
            {
                mWtLiveName = value;
            }
            get
            {
                return mWtLiveName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 房屋状态(1未交房、2未装修、3未入住、4已入住)
    /// <summary>
    /// 房屋状态(1未交房、2未装修、3未入住、4已入住)
    /// </summary>
    public sealed class TWRoomState
    {
        #region 私有变量
        private static int mNoSubmit = 1;		//**未交房
        private static int mNoFitting = 2;		//**未装修
        private static int mNoStay = 3;		//**未入住
        private static int mStayed = 4;		//**已入住

        private static string mNoSubmitName = "未交房";
        private static string mNoFittingName = "未装修";
        private static string mNoStayName = "未入住";
        private static string mStayedName = "已入住";

        #endregion

        public TWRoomState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 未交房
        /// <summary>
        /// 未交房
        /// </summary>
        public static int NoSubmit
        {
            set
            {
                mNoSubmit = value;
            }
            get
            {
                return mNoSubmit;
            }
        }


        #endregion

        #region 未装修
        /// <summary>
        /// 未装修
        /// </summary>
        public static int NoFitting
        {
            set
            {
                mNoFitting = value;
            }
            get
            {
                return mNoFitting;
            }
        }


        #endregion

        #region 未入住
        /// <summary>
        /// 未入住
        /// </summary>
        public static int NoStay
        {
            set
            {
                mNoStay = value;
            }
            get
            {
                return mNoStay;
            }
        }


        #endregion

        #region 已入住
        /// <summary>
        /// 已入住
        /// </summary>
        public static int Stayed
        {
            set
            {
                mStayed = value;
            }
            get
            {
                return mStayed;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 未交房
        /// <summary>
        /// 未交房
        /// </summary>
        public static string NoSubmitName
        {
            set
            {
                mNoSubmitName = value;
            }
            get
            {
                return mNoSubmitName;
            }
        }


        #endregion

        #region 未装修
        /// <summary>
        /// 未装修
        /// </summary>
        public static string NoFittingName
        {
            set
            {
                mNoFittingName = value;
            }
            get
            {
                return mNoFittingName;
            }
        }


        #endregion

        #region 未入住
        /// <summary>
        /// 未入住
        /// </summary>
        public static string NoStayName
        {
            set
            {
                mNoStayName = value;
            }
            get
            {
                return mNoStayName;
            }
        }


        #endregion

        #region 已入住
        /// <summary>
        /// 已入住
        /// </summary>
        public static string StayedName
        {
            set
            {
                mStayedName = value;
            }
            get
            {
                return mStayedName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 仪表类型(1水、2电、3气、4暖气)
    /// <summary>
    /// 仪表类型(1水、2电、3气、4暖气)
    /// </summary>
    public sealed class TWMeterType
    {

        #region 私有变量
        private static int mWater = 1;			//**水
        private static int mEletricity = 2;		//**电
        private static int mGas = 3;			//**气
        private static int mHeating = 4;		//**暖气

        private static string mWaterName = "水";			//**水
        private static string mEletricityName = "电";		//**电
        private static string mGasName = "气";				//**气
        private static string mHeatingName = "暖气";		//**暖气


        #endregion

        public TWMeterType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID
        #region 水
        /// <summary>
        /// 水
        /// </summary>
        public static int Water
        {
            set
            {
                mWater = value;
            }
            get
            {
                return mWater;
            }
        }


        #endregion

        #region 电
        /// <summary>
        /// 电
        /// </summary>
        public static int Eletricity
        {
            set
            {
                mEletricity = value;
            }
            get
            {
                return mEletricity;
            }
        }


        #endregion

        #region 气
        /// <summary>
        /// 气
        /// </summary>
        public static int Gas
        {
            set
            {
                mGas = value;
            }
            get
            {
                return mGas;
            }
        }


        #endregion

        #region 暖气
        /// <summary>
        /// 暖气
        /// </summary>
        public static int Heating
        {
            set
            {
                mHeating = value;
            }
            get
            {
                return mHeating;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 水
        /// <summary>
        /// 水
        /// </summary>
        public static string WaterName
        {
            set
            {
                mWaterName = value;
            }
            get
            {
                return mWaterName;
            }
        }


        #endregion

        #region 电
        /// <summary>
        /// 电
        /// </summary>
        public static string EletricityName
        {
            set
            {
                mEletricityName = value;
            }
            get
            {
                return mEletricityName;
            }
        }


        #endregion

        #region 气
        /// <summary>
        /// 气
        /// </summary>
        public static string GasName
        {
            set
            {
                mGasName = value;
            }
            get
            {
                return mGasName;
            }
        }


        #endregion

        #region 暖气
        /// <summary>
        /// 暖气
        /// </summary>
        public static string HeatingName
        {
            set
            {
                mHeatingName = value;
            }
            get
            {
                return mHeatingName;
            }
        }
        #endregion

        #endregion

        #endregion

    }
    #endregion

    #region 分摊方式(1按客户数量均摊、2按房屋数量均摊、3按建筑面积分摊、4按表计用量分摊、5按客户车位均摊、6按套内面积分摊)
    /// <summary>
    /// 分摊方式(1按客户数量均摊、2按房屋数量均摊、3按建筑面积分摊、4按表计用量分摊、5按客户车位均摊、6按套内面积分摊)
    /// </summary>
    public sealed class TWMeterPoolType
    {
        #region 私有变量
        private static int mCust = 1;		//**按客户数量均摊
        private static int mRoom = 2;		//**按房屋数量均摊
        private static int mBuildArea = 3;	//**按建筑面积分摊
        private static int mDosage = 4;		//**按表计用量分摊
        private static int mParking = 5;	//**按客户车位均摊
        private static int mInteriorArea = 6;	//**按套内面积分摊

        private static string mCustName = "按客户数量均摊";
        private static string mRoomName = "按房屋数量均摊";
        private static string mBuildAreaName = "按建筑面积分摊";
        private static string mDosageName = "按表计用量分摊";
        private static string mParkingName = "按客户车位均摊";
        private static string mInteriorAreaName = "按套内面积分摊";

        #endregion

        public TWMeterPoolType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID
        #region 按客户数量均摊
        /// <summary>
        /// 按客户数量均摊
        /// </summary>
        public static int Cust
        {
            set
            {
                mCust = value;
            }
            get
            {
                return mCust;
            }
        }


        #endregion

        #region 按房屋数量均摊
        /// <summary>
        /// 按房屋数量均摊
        /// </summary>
        public static int Room
        {
            set
            {
                mRoom = value;
            }
            get
            {
                return mRoom;
            }
        }


        #endregion

        #region 按建筑面积分摊
        /// <summary>
        /// 按建筑面积分摊
        /// </summary>
        public static int BuildArea
        {
            set
            {
                mBuildArea = value;
            }
            get
            {
                return mBuildArea;
            }
        }


        #endregion

        #region 按表计用量分摊
        /// <summary>
        /// 按表计用量分摊
        /// </summary>
        public static int Dosage
        {
            set
            {
                mDosage = value;
            }
            get
            {
                return mDosage;
            }
        }


        #endregion

        #region 按客户车位均摊
        /// <summary>
        /// 按客户车位均摊
        /// </summary>
        public static int Parking
        {
            set
            {
                mParking = value;
            }
            get
            {
                return mParking;
            }
        }


        #endregion

        #region 按套内面积分摊
        /// <summary>
        /// 按套内面积分摊
        /// </summary>
        public static int InteriorArea
        {
            set
            {
                mInteriorArea = value;
            }
            get
            {
                return mInteriorArea;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 按客户数量均摊
        /// <summary>
        /// 按客户数量均摊
        /// </summary>
        public static string CustName
        {
            set
            {
                mCustName = value;
            }
            get
            {
                return mCustName;
            }
        }


        #endregion

        #region 按房屋数量均摊
        /// <summary>
        /// 按房屋数量均摊
        /// </summary>
        public static string RoomName
        {
            set
            {
                mRoomName = value;
            }
            get
            {
                return mRoomName;
            }
        }


        #endregion

        #region 按建筑面积分摊
        /// <summary>
        /// 按建筑面积分摊
        /// </summary>
        public static string BuildAreaName
        {
            set
            {
                mBuildAreaName = value;
            }
            get
            {
                return mBuildAreaName;
            }
        }


        #endregion

        #region 按表计用量分摊
        /// <summary>
        /// 按表计用量分摊
        /// </summary>
        public static string DosageName
        {
            set
            {
                mDosageName = value;
            }
            get
            {
                return mDosageName;
            }
        }
        #endregion

        #region 按客户车位均摊
        /// <summary>
        /// 按客户车位均摊
        /// </summary>
        public static string ParkingName
        {
            set
            {
                mParkingName = value;
            }
            get
            {
                return mParkingName;
            }
        }
        #endregion

        #region 按套内面积分摊
        /// <summary>
        /// 按套内面积分摊
        /// </summary>
        public static string InteriorAreaName
        {
            set
            {
                mInteriorAreaName = value;
            }
            get
            {
                return mInteriorAreaName;
            }
        }
        #endregion

        #endregion

        #endregion
    }
    #endregion

    #region 费用类型(1收入、2代收、3暂收、9支出)
    /// <summary>
    /// 费用类型(1收入、2代收、3暂收、9支出)
    /// </summary>
    public sealed class TWCostType
    {
        #region 私有变量
        private static int mIncome = 1;		//**收入
        private static int mCollection = 2;		//**代收
        private static int mProvisional = 3;	//**暂收
        private static int mPayout = 9;		//**支出

        private static string mIncomeName = "收入";
        private static string mCollectionName = "代收";
        private static string mProvisionalName = "暂收";
        private static string mPayoutName = "支出";

        #endregion

        public TWCostType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID
        #region 收入
        /// <summary>
        /// 收入
        /// </summary>
        public static int Income
        {
            set
            {
                mIncome = value;
            }
            get
            {
                return mIncome;
            }
        }


        #endregion

        #region 代收
        /// <summary>
        /// 代收
        /// </summary>
        public static int Collection
        {
            set
            {
                mCollection = value;
            }
            get
            {
                return mCollection;
            }
        }


        #endregion

        #region 暂收
        /// <summary>
        /// 暂收
        /// </summary>
        public static int Provisional
        {
            set
            {
                mProvisional = value;
            }
            get
            {
                return mProvisional;
            }
        }


        #endregion

        #region 支出
        /// <summary>
        /// 支出
        /// </summary>
        public static int Payout
        {
            set
            {
                mPayout = value;
            }
            get
            {
                return mPayout;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 收入
        /// <summary>
        /// 收入
        /// </summary>
        public static string IncomeName
        {
            set
            {
                mIncomeName = value;
            }
            get
            {
                return mIncomeName;
            }
        }


        #endregion

        #region 代收
        /// <summary>
        /// 代收
        /// </summary>
        public static string CollectionName
        {
            set
            {
                mCollectionName = value;
            }
            get
            {
                return mCollectionName;
            }
        }


        #endregion

        #region 暂收
        /// <summary>
        /// 暂收
        /// </summary>
        public static string ProvisionalName
        {
            set
            {
                mProvisionalName = value;
            }
            get
            {
                return mProvisionalName;
            }
        }


        #endregion

        #region 支出
        /// <summary>
        /// 支出
        /// </summary>
        public static string PayoutName
        {
            set
            {
                mPayoutName = value;
            }
            get
            {
                return mPayoutName;
            }
        }
        #endregion

        #endregion

        #endregion
    }
    #endregion

    #region 费用生成类型(1否、2是)是否允许输入
    /// <summary>
    /// 费用生成类型(1否、2是)是否允许输入
    /// </summary>
    public sealed class TWCostGeneType
    {
        #region 私有变量
        private static int mCalculate = 1;		//**否
        private static int mSporadic = 2;		//**是		

        private static string mCalculateName = "否";
        private static string mSporadicName = "是";

        #endregion

        public TWCostGeneType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID
        #region 否
        /// <summary>
        /// 否
        /// </summary>
        public static int Calculate
        {
            set
            {
                mCalculate = value;
            }
            get
            {
                return mCalculate;
            }
        }


        #endregion

        #region 是
        /// <summary>
        /// 是
        /// </summary>
        public static int Sporadic
        {
            set
            {
                mSporadic = value;
            }
            get
            {
                return mSporadic;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 否
        /// <summary>
        /// 否
        /// </summary>
        public static string CalculateName
        {
            set
            {
                mCalculateName = value;
            }
            get
            {
                return mCalculateName;
            }
        }


        #endregion

        #region 是
        /// <summary>
        /// 是
        /// </summary>
        public static string SporadicName
        {
            set
            {
                mSporadicName = value;
            }
            get
            {
                return mSporadicName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 计算方式(1按定额每月计算、2按建筑面积*单价每月计算、3按车位面积*单价每月计算、4按用量*单价计算,5按套内面积*单价每月计算,6按数量*单价计算,7按天数*单价计算,8按实际发生额计算,9按花园面积*单价每月计算,10按产权面积*单价每月计算,11按数量*建筑面积*单价计算,12按天数*建筑面积*单价计算,13按建筑面积*单价计算,20按定额每季计算,21按建筑面积*单价每季计算,22按套内面积*单价每季计算,23按花园面积*单价每季计算,24按车位面积*单价每季计算)
    /// <summary>
    /// 计算方式(1按定额每月计算、2按建筑面积*单价每月计算、3按车位面积*单价每月计算、4按用量*单价计算,5按套内面积*单价每月计算,6按数量*单价计算,7按天数*单价计算,8按实际发生额计算,9按花园面积*单价每月计算,10按产权面积*单价每月计算,11按数量*建筑面积*单价计算,12按天数*建筑面积*单价计算,13按建筑面积*单价计算,20按定额每季计算,21按建筑面积*单价每季计算,22按套内面积*单价每季计算,23按花园面积*单价每季计算,24按车位面积*单价每季计算)
    /// </summary>
    public sealed class TWStanFormula
    {
        #region 私有变量

        private static string mFixed = "1";				//**按定额每月计算
        private static string mBuildArea = "2";			//**按建筑面积*单价每月计算
        private static string mParkArea = "3";			//**按车位面积*单价每月计算
        private static string mDosage = "4";			//**按用量*单价计算
        private static string mInteriorArea = "5";		//**按套内面积*单价每月计算
        private static string mPriceAmount = "6";		//**按数量*单价计算
        private static string mPriceDays = "7";			//**按天数*单价计算
        private static string mManageFees = "8";		//**按实际发生额计算
        private static string mGardenArea = "9";		//**按花园面积*单价每月计算
        private static string mPropertyArea = "10";		//**按产权面积*单价每月计算
        private static string mBuildAreaAmount = "11";	//**按数量*建筑面积*单价计算
        private static string mBuildAreaDay = "12";		//**按天数*建筑面积*单价计算
        private static string mBuildAreaOnly = "13";	//**按建筑面积*单价计算

        private static string mQuarterFixed = "20";				//**按定额每季计算
        private static string mQuarterBuildArea = "21";			//**按建筑面积*单价每季计算
        private static string mQuarterInteriorArea = "22";		//**按套内面积*单价每季计算
        private static string mQuarterGardenArea = "23";		//**按花园面积*单价每季计算
        private static string mQuarterParkArea = "24";			//**按车位面积*单价每季计算

        private static string mFixedName = "按定额每月计算";
        private static string mBuildAreaName = "按建筑面积*单价每月计算";
        private static string mParkAreaName = "按车位面积*单价每月计算";
        private static string mDosageName = "按用量*单价计算";
        private static string mInteriorAreaName = "按套内面积*单价每月计算";
        private static string mPriceAmountName = "按数量*单价计算";
        private static string mPriceDaysName = "按天数*单价计算";
        private static string mManageFeesName = "按实际发生额计算";
        private static string mGardenAreaName = "按花园面积*单价每月计算";
        private static string mPropertyAreaName = "按产权面积*单价每月计算";
        private static string mBuildAreaAmountName = "按数量*建筑面积*单价计算";
        private static string mBuildAreaDayName = "按天数*建筑面积*单价计算";
        private static string mBuildAreaOnlyName = "按建筑面积*单价计算";

        private static string mQuarterFixedName = "按定额每季计算";
        private static string mQuarterBuildAreaName = "按建筑面积*单价每季计算";
        private static string mQuarterInteriorAreaName = "按套内面积*单价每季计算";
        private static string mQuarterGardenAreaName = "按花园面积*单价每季计算";
        private static string mQuarterParkAreaName = "按车位面积*单价每季计算";

        #endregion

        public TWStanFormula()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 按定额每月计算
        /// <summary>
        /// 按定额每月计算
        /// </summary>
        public static string Fixed
        {
            set
            {
                mFixed = value;
            }
            get
            {
                return mFixed;
            }
        }


        #endregion

        #region 按建筑面积*单价每月计算
        /// <summary>
        /// 按建筑面积*单价每月计算
        /// </summary>
        public static string BuildArea
        {
            set
            {
                mBuildArea = value;
            }
            get
            {
                return mBuildArea;
            }
        }


        #endregion

        #region 按车位面积*单价每月计算
        /// <summary>
        /// 按车位面积*单价每月计算
        /// </summary>
        public static string ParkArea
        {
            set
            {
                mParkArea = value;
            }
            get
            {
                return mParkArea;
            }
        }


        #endregion

        #region 按用量*单价计算
        /// <summary>
        /// 按用量*单价计算
        /// </summary>
        public static string Dosage
        {
            set
            {
                mDosage = value;
            }
            get
            {
                return mDosage;
            }
        }


        #endregion

        #region 按套内面积*单价每月计算
        /// <summary>
        /// 按套内面积*单价每月计算
        /// </summary>
        public static string InteriorArea
        {
            set
            {
                mInteriorArea = value;
            }
            get
            {
                return mInteriorArea;
            }
        }


        #endregion

        #region 按数量*单价计算
        /// <summary>
        /// 按数量*单价计算
        /// </summary>
        public static string PriceAmount
        {
            set
            {
                mPriceAmount = value;
            }
            get
            {
                return mPriceAmount;
            }
        }


        #endregion

        #region 按天数*单价计算
        /// <summary>
        /// 按天数*单价计算
        /// </summary>
        public static string PriceDays
        {
            set
            {
                mPriceDays = value;
            }
            get
            {
                return mPriceDays;
            }
        }


        #endregion

        #region 按实际发生额计算
        /// <summary>
        /// 按实际发生额计算
        /// </summary>
        public static string ManageFees
        {
            set
            {
                mManageFees = value;
            }
            get
            {
                return mManageFees;
            }
        }


        #endregion

        #region 按花园面积*单价每月计算
        /// <summary>
        /// 按花园面积*单价每月计算
        /// </summary>
        public static string GardenArea
        {
            set
            {
                mGardenArea = value;
            }
            get
            {
                return mGardenArea;
            }
        }


        #endregion

        #region 按产权面积*单价每月计算
        /// <summary>
        /// 按产权面积*单价每月计算
        /// </summary>
        public static string PropertyArea
        {
            set
            {
                mPropertyArea = value;
            }
            get
            {
                return mPropertyArea;
            }
        }


        #endregion

        #region 按数量*建筑面积*单价计算
        /// <summary>
        /// 按数量*建筑面积*单价计算
        /// </summary>
        public static string BuildAreaAmount
        {
            set
            {
                mBuildAreaAmount = value;
            }
            get
            {
                return mBuildAreaAmount;
            }
        }


        #endregion

        #region 按天数*建筑面积*单价计算
        /// <summary>
        /// 按数量*建筑面积*单价计算
        /// </summary>
        public static string BuildAreaDay
        {
            set
            {
                mBuildAreaDay = value;
            }
            get
            {
                return mBuildAreaDay;
            }
        }


        #endregion

        #region 按建筑面积*单价计算
        /// <summary>
        /// 按建筑面积*单价计算
        /// </summary>
        public static string BuildAreaOnly
        {
            set
            {
                mBuildAreaOnly = value;
            }
            get
            {
                return mBuildAreaOnly;
            }
        }

        #endregion

        #region 按定额每季计算
        /// <summary>
        /// 按定额每季计算
        /// </summary>
        public static string QuarterFixed
        {
            set
            {
                mQuarterFixed = value;
            }
            get
            {
                return mQuarterFixed;
            }
        }


        #endregion

        #region 按建筑面积*单价每季计算
        /// <summary>
        /// 按建筑面积*单价每季计算
        /// </summary>
        public static string QuarterBuildArea
        {
            set
            {
                mQuarterBuildArea = value;
            }
            get
            {
                return mQuarterBuildArea;
            }
        }


        #endregion

        #region 按套内面积*单价每季计算
        /// <summary>
        /// 按套内面积*单价每季计算
        /// </summary>
        public static string QuarterInteriorArea
        {
            set
            {
                mQuarterInteriorArea = value;
            }
            get
            {
                return mQuarterInteriorArea;
            }
        }


        #endregion

        #region 按花园面积*单价每季计算
        /// <summary>
        /// 按花园面积*单价每季计算
        /// </summary>
        public static string QuarterGardenArea
        {
            set
            {
                mQuarterGardenArea = value;
            }
            get
            {
                return mQuarterGardenArea;
            }
        }


        #endregion

        #region 按车位面积*单价每季计算
        /// <summary>
        /// 按车位面积*单价每季计算
        /// </summary>
        public static string QuarterParkArea
        {
            set
            {
                mQuarterParkArea = value;
            }
            get
            {
                return mQuarterParkArea;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 按定额每月计算
        /// <summary>
        /// 按定额每月计算
        /// </summary>
        public static string FixedName
        {
            set
            {
                mFixedName = value;
            }
            get
            {
                return mFixedName;
            }
        }


        #endregion

        #region 按建筑面积*单价每月计算
        /// <summary>
        /// 按建筑面积*单价每月计算
        /// </summary>
        public static string BuildAreaName
        {
            set
            {
                mBuildAreaName = value;
            }
            get
            {
                return mBuildAreaName;
            }
        }


        #endregion

        #region 按车位面积*单价每月计算
        /// <summary>
        /// 按车位面积*单价每月计算
        /// </summary>
        public static string ParkAreaName
        {
            set
            {
                mParkAreaName = value;
            }
            get
            {
                return mParkAreaName;
            }
        }


        #endregion

        #region 按用量*单价计算
        /// <summary>
        /// 按用量*单价计算
        /// </summary>
        public static string DosageName
        {
            set
            {
                mDosageName = value;
            }
            get
            {
                return mDosageName;
            }
        }


        #endregion

        #region 按套内面积*单价每月计算
        /// <summary>
        /// 按套内面积*单价每月计算
        /// </summary>
        public static string InteriorAreaName
        {
            set
            {
                mInteriorAreaName = value;
            }
            get
            {
                return mInteriorAreaName;
            }
        }


        #endregion

        #region 按数量*单价计算
        /// <summary>
        /// 按数量*单价计算
        /// </summary>
        public static string PriceAmountName
        {
            set
            {
                mPriceAmountName = value;
            }
            get
            {
                return mPriceAmountName;
            }
        }


        #endregion

        #region 按天数*单价计算
        /// <summary>
        /// 按天数*单价计算
        /// </summary>
        public static string PriceDaysName
        {
            set
            {
                mPriceDaysName = value;
            }
            get
            {
                return mPriceDaysName;
            }
        }


        #endregion

        #region 按实际发生额计算
        /// <summary>
        /// 按实际发生额计算
        /// </summary>
        public static string ManageFeesName
        {
            set
            {
                mManageFeesName = value;
            }
            get
            {
                return mManageFeesName;
            }
        }


        #endregion

        #region 按花园面积*单价每月计算
        /// <summary>
        /// 按花园面积*单价每月计算
        /// </summary>
        public static string GardenAreaName
        {
            set
            {
                mGardenAreaName = value;
            }
            get
            {
                return mGardenAreaName;
            }
        }


        #endregion

        #region 按产权面积*单价每月计算
        /// <summary>
        /// 按产权面积*单价每月计算
        /// </summary>
        public static string PropertyAreaName
        {
            set
            {
                mPropertyAreaName = value;
            }
            get
            {
                return mPropertyAreaName;
            }
        }


        #endregion

        #region 按数量*建筑面积*单价计算
        /// <summary>
        /// 按数量*建筑面积*单价计算
        /// </summary>
        public static string BuildAreaAmountName
        {
            set
            {
                mBuildAreaAmountName = value;
            }
            get
            {
                return mBuildAreaAmountName;
            }
        }


        #endregion

        #region 按天数*建筑面积*单价计算
        /// <summary>
        /// 按天数*建筑面积*单价计算
        /// </summary>
        public static string BuildAreaDayName
        {
            set
            {
                mBuildAreaDayName = value;
            }
            get
            {
                return mBuildAreaDayName;
            }
        }


        #endregion

        #region 按建筑面积*单价计算
        /// <summary>
        /// 按建筑面积*单价计算
        /// </summary>
        public static string BuildAreaOnlyName
        {
            set
            {
                mBuildAreaOnlyName = value;
            }
            get
            {
                return mBuildAreaOnlyName;
            }
        }

        #endregion

        #region 按定额每季计算
        /// <summary>
        /// 按定额每季计算
        /// </summary>
        public static string QuarterFixedName
        {
            set
            {
                mQuarterFixedName = value;
            }
            get
            {
                return mQuarterFixedName;
            }
        }


        #endregion

        #region 按建筑面积*单价每季计算
        /// <summary>
        /// 按建筑面积*单价每季计算
        /// </summary>
        public static string QuarterBuildAreaName
        {
            set
            {
                mQuarterBuildAreaName = value;
            }
            get
            {
                return mQuarterBuildAreaName;
            }
        }


        #endregion

        #region 按套内面积*单价每季计算
        /// <summary>
        /// 按套内面积*单价每季计算
        /// </summary>
        public static string QuarterInteriorAreaName
        {
            set
            {
                mQuarterInteriorAreaName = value;
            }
            get
            {
                return mQuarterInteriorAreaName;
            }
        }


        #endregion

        #region 按花园面积*单价每季计算
        /// <summary>
        /// 按花园面积*单价每季计算
        /// </summary>
        public static string QuarterGardenAreaName
        {
            set
            {
                mQuarterGardenAreaName = value;
            }
            get
            {
                return mQuarterGardenAreaName;
            }
        }


        #endregion

        #region 按车位面积*单价每季计算
        /// <summary>
        /// 按车位面积*单价每季计算
        /// </summary>
        public static string QuarterParkAreaName
        {
            set
            {
                mQuarterParkAreaName = value;
            }
            get
            {
                return mQuarterParkAreaName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 计算条件(BuildArea建筑面积、ParkArea车位面积、Dosage表计用量、Floor楼层层数、InteriorArea套内面积、PropertyArea产权面积、GardenArea花园面积)
    /// <summary>
    /// 计算方式(BuildArea建筑面积、ParkArea车位面积、Dosage表计用量、Floor楼层层数、InteriorArea套内面积、Amount数量、PropertyArea产权面积、GardenArea花园面积)
    /// </summary>
    public sealed class TWConditionField
    {
        #region 私有变量
        private static string mBuildArea = "BuildArea";		//**建筑面积
        private static string mParkArea = "ParkArea";		//**车位面积
        private static string mDosage = "Dosage";		//**表计用量
        private static string mFloor = "Floor";			//**楼层层数
        private static string mInteriorArea = "InteriorArea";//**套内面积
        private static string mGardenArea = "GardenArea";//**花园面积
        private static string mPropertyArea = "PropertyArea";//**产权面积
        private static string mAmount = "Amount";//**数量

        private static string mBuildAreaName = "建筑面积";
        private static string mParkAreaName = "车位面积";
        private static string mDosageName = "表计用量";
        private static string mFloorName = "楼层层数";
        private static string mInteriorAreaName = "套内面积";
        private static string mGardenAreaName = "花园面积";
        private static string mPropertyAreaName = "产权面积";
        private static string mAmountName = "数量";


        #endregion

        public TWConditionField()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 建筑面积
        /// <summary>
        /// 建筑面积
        /// </summary>
        public static string BuildArea
        {
            set
            {
                mBuildArea = value;
            }
            get
            {
                return mBuildArea;
            }
        }


        #endregion

        #region 车位面积
        /// <summary>
        /// 车位面积
        /// </summary>
        public static string ParkArea
        {
            set
            {
                mParkArea = value;
            }
            get
            {
                return mParkArea;
            }
        }


        #endregion

        #region 表计用量
        /// <summary>
        /// 表计用量
        /// </summary>
        public static string Dosage
        {
            set
            {
                mDosage = value;
            }
            get
            {
                return mDosage;
            }
        }


        #endregion

        #region 楼层层数
        /// <summary>
        /// 楼层层数
        /// </summary>
        public static string Floor
        {
            set
            {
                mFloor = value;
            }
            get
            {
                return mFloor;
            }
        }


        #endregion

        #region 套内面积
        /// <summary>
        /// 套内面积
        /// </summary>
        public static string InteriorArea
        {
            set
            {
                mInteriorArea = value;
            }
            get
            {
                return mInteriorArea;
            }
        }


        #endregion

        #region 花园面积
        /// <summary>
        /// 花园面积
        /// </summary>
        public static string GardenArea
        {
            set
            {
                mGardenArea = value;
            }
            get
            {
                return mGardenArea;
            }
        }


        #endregion

        #region 产权面积
        /// <summary>
        /// 产权面积
        /// </summary>
        public static string PropertyArea
        {
            set
            {
                mPropertyArea = value;
            }
            get
            {
                return mPropertyArea;
            }
        }


        #endregion

        #region 数量
        /// <summary>
        /// 数量
        /// </summary>
        public static string Amount
        {
            set
            {
                mAmount = value;
            }
            get
            {
                return mAmount;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 建筑面积
        /// <summary>
        /// 建筑面积
        /// </summary>
        public static string BuildAreaName
        {
            set
            {
                mBuildAreaName = value;
            }
            get
            {
                return mBuildAreaName;
            }
        }


        #endregion

        #region 车位面积
        /// <summary>
        /// 车位面积
        /// </summary>
        public static string ParkAreaName
        {
            set
            {
                mParkAreaName = value;
            }
            get
            {
                return mParkAreaName;
            }
        }


        #endregion

        #region 表计用量
        /// <summary>
        /// 表计用量
        /// </summary>
        public static string DosageName
        {
            set
            {
                mDosageName = value;
            }
            get
            {
                return mDosageName;
            }
        }


        #endregion

        #region 楼层层数
        /// <summary>
        /// 楼层层数
        /// </summary>
        public static string FloorName
        {
            set
            {
                mFloorName = value;
            }
            get
            {
                return mFloorName;
            }
        }


        #endregion

        #region 套内面积
        /// <summary>
        /// 套内面积
        /// </summary>
        public static string InteriorAreaName
        {
            set
            {
                mInteriorAreaName = value;
            }
            get
            {
                return mInteriorAreaName;
            }
        }


        #endregion

        #region 花园面积
        /// <summary>
        /// 花园面积
        /// </summary>
        public static string GardenAreaName
        {
            set
            {
                mGardenAreaName = value;
            }
            get
            {
                return mGardenAreaName;
            }
        }


        #endregion

        #region 产权面积
        /// <summary>
        /// 产权面积
        /// </summary>
        public static string PropertyAreaName
        {
            set
            {
                mPropertyAreaName = value;
            }
            get
            {
                return mPropertyAreaName;
            }
        }


        #endregion

        #region 数量
        /// <summary>
        /// 数量
        /// </summary>
        public static string AmountName
        {
            set
            {
                mAmountName = value;
            }
            get
            {
                return mAmountName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 费用入账标志(100零星费用)其他说明：15导入的实收押金，25导入的垫付，99导入的欠费
    /// <summary>
    /// 费用入账标志(100零星费用)15导入的实收押金，25导入的垫付，99导入的欠费
    /// </summary>
    public sealed class TWAccountType
    {
        #region 私有变量
        private static int mSporadic = 100;		//**零星费用
        private static int mAccounts = 0;		//**入账费用

        private static string mSporadicName = "零星费用";
        private static string mAccountsName = "入账费用";

        #endregion

        public TWAccountType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 零星费用
        /// <summary>
        /// 零星费用
        /// </summary>
        public static int Sporadic
        {
            set
            {
                mSporadic = value;
            }
            get
            {
                return mSporadic;
            }
        }
        #endregion

        #region 入账费用
        /// <summary>
        /// 入账费用
        /// </summary>
        public static int Accounts
        {
            set
            {
                mAccounts = value;
            }
            get
            {
                return mAccounts;
            }
        }
        #endregion

        #endregion

        #region 名称

        #region 零星费用
        /// <summary>
        /// 零星费用
        /// </summary>
        public static string SporadicName
        {
            set
            {
                mSporadicName = value;
            }
            get
            {
                return mSporadicName;
            }
        }


        #endregion

        #region 入账费用
        /// <summary>
        /// 入账费用
        /// </summary>
        public static string AccountsName
        {
            set
            {
                mAccountsName = value;
            }
            get
            {
                return mAccountsName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 费用收费标志(1收费、3预交收款、5退款处理、7预交余额转移、8预交余额退款、9零头结转)
    /// <summary>
    /// 费用收费标志(1收费、3预交收款、5退款处理、7预交余额转移、8预交余额退款、9零头结转)
    /// </summary>
    public sealed class TWAccountWay
    {
        #region 私有变量
        private static int mChgFee = 1;			//**收费		
        private static int mPreCosts = 3;		//**预交收款	
        private static int mRefund = 5;			//**退款处理
        private static int mPreTransfer = 7;	//**预交余额转移
        private static int mPreRefund = 8;		//**预交余额退款
        private static int mSurplus = 9;		//**零头结转

        #endregion

        public TWAccountWay()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 收费
        /// <summary>
        /// 收费
        /// </summary>
        public static int ChgFee
        {
            set
            {
                mChgFee = value;
            }
            get
            {
                return mChgFee;
            }
        }


        #endregion

        #region 预交收款
        /// <summary>
        /// 预交收款
        /// </summary>
        public static int PreCosts
        {
            set
            {
                mPreCosts = value;
            }
            get
            {
                return mPreCosts;
            }
        }


        #endregion

        #region 退款处理
        /// <summary>
        /// 退款处理
        /// </summary>
        public static int Refund
        {
            set
            {
                mRefund = value;
            }
            get
            {
                return mRefund;
            }
        }


        #endregion

        #region 预交余额转移
        /// <summary>
        /// 预交余额转移
        /// </summary>
        public static int PreTransfer
        {
            set
            {
                mPreTransfer = value;
            }
            get
            {
                return mPreTransfer;
            }
        }


        #endregion

        #region 预交余额退款
        /// <summary>
        /// 预交余额退款
        /// </summary>
        public static int PreRefund
        {
            set
            {
                mPreRefund = value;
            }
            get
            {
                return mPreRefund;
            }
        }


        #endregion

        #region 零头结转
        /// <summary>
        /// 零头结转
        /// </summary>
        public static int Surplus
        {
            set
            {
                mSurplus = value;
            }
            get
            {
                return mSurplus;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

    #region 入账类别(1全部费用入账、2单户费用入账、3滞纳金的减免、4退款生成、5预交费用冲抵、6合同费用审核、7费用减免冲抵、8单户减免冲抵、9费用减免登记、10退款办理申请、11票据撤销申请)
    /// <summary>
    /// 入账类别(1全部费用入账、2单户费用入账、3滞纳金的减免、4退款生成、5预交费用冲抵、6合同费用审核、7费用减免冲抵、8单户减免冲抵、9费用减免登记、10退款办理申请、11票据撤销申请)
    /// </summary>
    public sealed class TWAuditType
    {
        #region 私有变量
        private static int mAllFees = 1;		//**全部费用入账
        private static int mOneFees = 2;		//**单户费用入账
        private static int mLateFee = 3;		//**滞纳金的减免
        private static int mRefund = 4;			//**退款生成

        private static int mAllPreOffset = 5;		//**预交费用冲抵
        private static int mContract = 6;			//**合同费用审核
        private static int mAllWaivOffset = 7;		//**费用减免冲抵
        //		private static int mOneWaivOffset = 8;		//**单户减免冲抵

        private static int mAuditWaiv = 9;				//**费用减免登记
        private static int mAuditRefund = 10;			//**实收/预收退款办理申请
        private static int mAuditBillCancel = 11;		//**票据撤销申请

        private static int mPartPreOffset = 12;			//**预存部分冲抵
        private static int mAuditRoomState = 13;		//**交房状态审核

        private static int mAuditPrecRefund = 15;			//**预存退款办理申请

        private static int mPaymentApply = 16;			//**交款申请按人


        private static string mAllFeesName = "全部费用入账";
        private static string mOneFeesName = "单户费用入账";
        private static string mLateFeeName = "滞纳金的减免";
        private static string mRefundName = "退款生成";

        private static string mAllPreOffsetName = "预交费用冲抵";
        private static string mContractName = "合同费用审核";
        private static string mAllWaivOffsetName = "费用减免冲抵";
        //		private static string mOneWaivOffsetName = "单户减免冲抵";

        private static string mAuditWaivName = "费用减免登记";
        private static string mAuditRefundName = "实收/预收退款办理申请";
        private static string mAuditBillCancelName = "票据撤销申请";

        private static string mPartPreOffsetName = "预存部分冲抵";
        private static string mAuditRoomStateName = "交房状态审核";

        private static string mAuditPrecRefundName = "预存退款办理申请";

        private static string mPaymentApplyName = "交款申请按人";


        #endregion

        public TWAuditType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 全部费用入账
        /// <summary>
        /// 全部费用入账
        /// </summary>
        public static int AllFees
        {
            set
            {
                mAllFees = value;
            }
            get
            {
                return mAllFees;
            }
        }


        #endregion

        #region 单户费用入账
        /// <summary>
        /// 单户费用入账
        /// </summary>
        public static int OneFees
        {
            set
            {
                mOneFees = value;
            }
            get
            {
                return mOneFees;
            }
        }


        #endregion

        #region 滞纳金的减免
        /// <summary>
        /// 滞纳金的减免
        /// </summary>
        public static int LateFee
        {
            set
            {
                mLateFee = value;
            }
            get
            {
                return mLateFee;
            }
        }


        #endregion

        #region 退款生成
        /// <summary>
        /// 退款生成
        /// </summary>
        public static int Refund
        {
            set
            {
                mRefund = value;
            }
            get
            {
                return mRefund;
            }
        }


        #endregion

        #region 预交费用冲抵
        /// <summary>
        /// 预交费用冲抵
        /// </summary>
        public static int AllPreOffset
        {
            set
            {
                mAllPreOffset = value;
            }
            get
            {
                return mAllPreOffset;
            }
        }


        #endregion

        #region 合同费用审核
        /// <summary>
        /// 合同费用审核
        /// </summary>
        public static int Contract
        {
            set
            {
                mContract = value;
            }
            get
            {
                return mContract;
            }
        }


        #endregion

        #region 费用减免冲抵
        /// <summary>
        /// 费用减免冲抵
        /// </summary>
        public static int AllWaivOffset
        {
            set
            {
                mAllWaivOffset = value;
            }
            get
            {
                return mAllWaivOffset;
            }
        }


        #endregion

        #region 单户减免冲抵
        //		/// <summary>
        //		/// 单户减免冲抵
        //		/// </summary>
        //		public static int OneWaivOffset
        //		{
        //			set
        //			{
        //				mOneWaivOffset=value;
        //			}
        //			get
        //			{
        //				return mOneWaivOffset;
        //			}
        //		}


        #endregion

        #region 费用减免登记
        /// <summary>
        /// 费用减免登记
        /// </summary>
        public static int AuditWaiv
        {
            set
            {
                mAuditWaiv = value;
            }
            get
            {
                return mAuditWaiv;
            }
        }


        #endregion

        #region 实收/预收退款办理申请
        /// <summary>
        /// 实收/预收退款办理申请
        /// </summary>
        public static int AuditRefund
        {
            set
            {
                mAuditRefund = value;
            }
            get
            {
                return mAuditRefund;
            }
        }
        #endregion

        #region  预存退款办理申请
        /// <summary>
        /// 预存退款办理申请
        /// </summary>
        public static int AuditPrecRefund
        {
            set
            {
                mAuditPrecRefund = value;
            }
            get
            {
                return mAuditPrecRefund;
            }
        }
        #endregion

        #region 票据撤销申请
        /// <summary>
        /// 票据撤销申请
        /// </summary>
        public static int AuditBillCancel
        {
            set
            {
                mAuditBillCancel = value;
            }
            get
            {
                return mAuditBillCancel;
            }
        }


        #endregion

        #region 预存部分冲抵
        /// <summary>
        /// 预存部分冲抵
        /// </summary>
        public static int PartPreOffset
        {
            set
            {
                mPartPreOffset = value;
            }
            get
            {
                return mPartPreOffset;
            }
        }


        #endregion

        #region 交房状态审核
        /// <summary>
        /// 交房状态审核
        /// </summary>
        public static int AuditRoomState
        {
            set
            {
                mAuditRoomState = value;
            }
            get
            {
                return mAuditRoomState;
            }
        }


        #endregion

        #region 交款申请按人
        /// <summary>
        /// 交款申请按人
        /// </summary>
        public static int PaymentApply
        {
            set
            {
                mPaymentApply = value;
            }
            get
            {
                return mPaymentApply;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 全部费用入账
        /// <summary>
        /// 全部费用入账
        /// </summary>
        public static string AllFeesName
        {
            set
            {
                mAllFeesName = value;
            }
            get
            {
                return mAllFeesName;
            }
        }


        #endregion

        #region 单户费用入账
        /// <summary>
        /// 单户费用入账
        /// </summary>
        public static string OneFeesName
        {
            set
            {
                mOneFeesName = value;
            }
            get
            {
                return mOneFeesName;
            }
        }


        #endregion

        #region 滞纳金的减免
        /// <summary>
        /// 滞纳金的减免
        /// </summary>
        public static string LateFeeName
        {
            set
            {
                mLateFeeName = value;
            }
            get
            {
                return mLateFeeName;
            }
        }


        #endregion

        #region 退款生成
        /// <summary>
        /// 退款生成
        /// </summary>
        public static string RefundName
        {
            set
            {
                mRefundName = value;
            }
            get
            {
                return mRefundName;
            }
        }


        #endregion

        #region 预交费用冲抵
        /// <summary>
        /// 预交费用冲抵
        /// </summary>
        public static string AllPreOffsetName
        {
            set
            {
                mAllPreOffsetName = value;
            }
            get
            {
                return mAllPreOffsetName;
            }
        }


        #endregion

        #region 合同费用审核
        /// <summary>
        /// 合同费用审核
        /// </summary>
        public static string ContractName
        {
            set
            {
                mContractName = value;
            }
            get
            {
                return mContractName;
            }
        }


        #endregion

        #region 费用减免冲抵
        /// <summary>
        /// 费用减免冲抵
        /// </summary>
        public static string AllWaivOffsetName
        {
            set
            {
                mAllWaivOffsetName = value;
            }
            get
            {
                return mAllWaivOffsetName;
            }
        }


        #endregion

        #region 单户减免冲抵
        //		/// <summary>
        //		/// 单户减免冲抵
        //		/// </summary>
        //		public static string OneWaivOffsetName
        //		{
        //			set
        //			{
        //				mOneWaivOffsetName=value;
        //			}
        //			get
        //			{
        //				return mOneWaivOffsetName;
        //			}
        //		}


        #endregion

        #region 费用减免登记
        /// <summary>
        /// 费用减免登记
        /// </summary>
        public static string AuditWaivName
        {
            set
            {
                mAuditWaivName = value;
            }
            get
            {
                return mAuditWaivName;
            }
        }


        #endregion

        #region 退款办理申请
        /// <summary>
        /// 退款办理申请
        /// </summary>
        public static string AuditRefundName
        {
            set
            {
                mAuditRefundName = value;
            }
            get
            {
                return mAuditRefundName;
            }
        }


        #endregion

        #region 票据撤销申请
        /// <summary>
        /// 票据撤销申请
        /// </summary>
        public static string AuditBillCancelName
        {
            set
            {
                mAuditBillCancelName = value;
            }
            get
            {
                return mAuditBillCancelName;
            }
        }


        #endregion

        #region 预存部分冲抵
        /// <summary>
        /// 预存部分冲抵
        /// </summary>
        public static string PartPreOffsetName
        {
            set
            {
                mPartPreOffsetName = value;
            }
            get
            {
                return mPartPreOffsetName;
            }
        }


        #endregion

        #region 交房状态审核
        /// <summary>
        /// 交房状态审核
        /// </summary>
        public static string AuditRoomStateName
        {
            set
            {
                mAuditRoomStateName = value;
            }
            get
            {
                return mAuditRoomStateName;
            }
        }


        #endregion

        #region 预存退款办理申请
        /// <summary>
        /// 预存退款办理申请
        /// </summary>
        public static string AuditPrecRefundName
        {
            set
            {
                mAuditPrecRefundName = value;
            }
            get
            {
                return mAuditPrecRefundName;
            }
        }


        #endregion

        #region 交款申请按人
        /// <summary>
        /// 交款申请按人
        /// </summary>
        public static string PaymentApplyName
        {
            set
            {
                mPaymentApplyName = value;
            }
            get
            {
                return mPaymentApplyName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 组织机构(1公司-管理处、2总公司-分公司-管理处)
    /// <summary>
    /// 组织机构(1公司-管理处、2总公司-分公司-管理处)
    /// </summary>
    public sealed class TWRegMode
    {
        #region 私有变量
        private static string mNoBranch = "1";		//**公司-管理处
        private static string mHaveBranch = "2";		//**总公司-分公司-管理处

        private static string mNoBranchName = "公司-管理处";		//**公司-管理处
        private static string mHaveBranchName = "总公司-分公司-管理处";		//**总公司-分公司-管理处

        #endregion

        public TWRegMode()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 公司-管理处
        /// <summary>
        /// 公司-管理处
        /// </summary>
        public static string NoBranch
        {
            set
            {
                mNoBranch = value;
            }
            get
            {
                return mNoBranch;
            }
        }


        #endregion

        #region 总公司-分公司-管理处
        /// <summary>
        /// 总公司-分公司-管理处
        /// </summary>
        public static string HaveBranch
        {
            set
            {
                mHaveBranch = value;
            }
            get
            {
                return mHaveBranch;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 公司-管理处
        /// <summary>
        /// 公司-管理处
        /// </summary>
        public static string NoBranchName
        {
            set
            {
                mNoBranchName = value;
            }
            get
            {
                return mNoBranchName;
            }
        }


        #endregion

        #region 总公司-分公司-管理处
        /// <summary>
        /// 总公司-分公司-管理处
        /// </summary>
        public static string HaveBranchName
        {
            set
            {
                mHaveBranchName = value;
            }
            get
            {
                return mHaveBranchName;
            }
        }


        #endregion

        #endregion



        #endregion

    }
    #endregion

    #region 登录系统(1总公司、3分公司、5管理处)
    /// <summary>
    /// 登录系统(1总公司、3分公司、5管理处)
    /// </summary>
    public sealed class TWEntryType
    {
        #region 私有变量
        private static string mParentCompany = "1";		//**总公司
        private static string mFilialeCompany = "3";	//**分公司
        private static string mManagementOffice = "5";	//**管理处

        private static string mParentCompanyName = "总公司";		//**总公司
        private static string mFilialeCompanyName = "分公司";	//**分公司
        private static string mManagementOfficeName = "管理处";	//**管理处

        #endregion

        public TWEntryType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 总公司
        /// <summary>
        /// 总公司
        /// </summary>
        public static string ParentCompany
        {
            set
            {
                mParentCompany = value;
            }
            get
            {
                return mParentCompany;
            }
        }


        #endregion

        #region 分公司
        /// <summary>
        /// 分公司
        /// </summary>
        public static string FilialeCompany
        {
            set
            {
                mFilialeCompany = value;
            }
            get
            {
                return mFilialeCompany;
            }
        }


        #endregion

        #region 管理处
        /// <summary>
        /// 管理处
        /// </summary>
        public static string ManagementOffice
        {
            set
            {
                mManagementOffice = value;
            }
            get
            {
                return mManagementOffice;
            }
        }


        #endregion

        #endregion

        #region 名称
        #region 总公司
        /// <summary>
        /// 总公司
        /// </summary>
        public static string ParentCompanyName
        {
            set
            {
                mParentCompanyName = value;
            }
            get
            {
                return mParentCompanyName;
            }
        }


        #endregion

        #region 分公司
        /// <summary>
        /// 分公司
        /// </summary>
        public static string FilialeCompanyName
        {
            set
            {
                mFilialeCompanyName = value;
            }
            get
            {
                return mFilialeCompanyName;
            }
        }


        #endregion

        #region 管理处
        /// <summary>
        /// 管理处
        /// </summary>
        public static string ManagementOfficeName
        {
            set
            {
                mManagementOfficeName = value;
            }
            get
            {
                return mManagementOfficeName;
            }
        }


        #endregion

        #endregion




        #endregion

    }
    #endregion

    #region 注册类型(1物管、2商家、3银行)
    /// <summary>
    /// 注册类型(1物管、2商家、3银行)
    /// </summary>
    public sealed class TWRegType
    {
        #region 私有变量
        private static string mProperty = "1";	//**物管
        private static string mMerchant = "2";	//**商家
        private static string mBank = "3";	//**银行

        private static string mPropertyName = "物管";		//**物管
        private static string mMerchantName = "商家";	//**商家
        private static string mBankName = "银行";	//**银行

        #endregion

        public TWRegType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 物管
        /// <summary>
        /// 物管
        /// </summary>
        public static string Property
        {
            set
            {
                mProperty = value;
            }
            get
            {
                return mProperty;
            }
        }


        #endregion

        #region 商家
        /// <summary>
        /// 商家
        /// </summary>
        public static string Merchant
        {
            set
            {
                mMerchant = value;
            }
            get
            {
                return mMerchant;
            }
        }


        #endregion

        #region 银行
        /// <summary>
        /// 银行
        /// </summary>
        public static string Bank
        {
            set
            {
                mBank = value;
            }
            get
            {
                return mBank;
            }
        }


        #endregion

        #endregion

        #region 名称
        #region 物管
        /// <summary>
        /// 物管
        /// </summary>
        public static string PropertyName
        {
            set
            {
                mPropertyName = value;
            }
            get
            {
                return mPropertyName;
            }
        }


        #endregion

        #region 商家
        /// <summary>
        /// 商家
        /// </summary>
        public static string MerchantName
        {
            set
            {
                mMerchantName = value;
            }
            get
            {
                return mMerchantName;
            }
        }


        #endregion

        #region 银行
        /// <summary>
        /// 银行
        /// </summary>
        public static string BankName
        {
            set
            {
                mBankName = value;
            }
            get
            {
                return mBankName;
            }
        }


        #endregion

        #endregion




        #endregion

    }
    #endregion

    #region 票据用途(0收费预交公用、1收费专用、2预交专用、)
    /// <summary>
    /// 票据用途(0收费预交公用、1收费专用、2预交专用、)
    /// </summary>
    public sealed class TWBillPurpose
    {
        #region 私有变量
        private static string mPublic = "0";	//**收费预交公用
        private static string mPaid = "1";	//**收费专用
        private static string mPrec = "2";	//**预交专用

        private static string mPublicName = "收费预交公用";	//**收费预交公用
        private static string mPaidName = "收费专用";		//**收费专用
        private static string mPrecName = "预交专用";	//**预交专用


        #endregion

        public TWBillPurpose()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 收费预交公用
        /// <summary>
        /// 收费预交公用
        /// </summary>
        public static string Public
        {
            set
            {
                mPublic = value;
            }
            get
            {
                return mPublic;
            }
        }


        #endregion

        #region 收费专用
        /// <summary>
        /// 收费专用
        /// </summary>
        public static string Paid
        {
            set
            {
                mPaid = value;
            }
            get
            {
                return mPaid;
            }
        }


        #endregion

        #region 预交专用
        /// <summary>
        /// 预交专用
        /// </summary>
        public static string Prec
        {
            set
            {
                mPrec = value;
            }
            get
            {
                return mPrec;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 收费预交公用
        /// <summary>
        /// 收费预交公用
        /// </summary>
        public static string PublicName
        {
            set
            {
                mPublicName = value;
            }
            get
            {
                return mPublicName;
            }
        }


        #endregion

        #region 收费专用
        /// <summary>
        /// 收费专用
        /// </summary>
        public static string PaidName
        {
            set
            {
                mPaidName = value;
            }
            get
            {
                return mPaidName;
            }
        }


        #endregion

        #region 预交专用
        /// <summary>
        /// 预交专用
        /// </summary>
        public static string PrecName
        {
            set
            {
                mPrecName = value;
            }
            get
            {
                return mPrecName;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

    #region 系统费用项目(B0001物业管理、B0002车位物管、B0003出租租金、B0004特约服务、B0005产权税费、G0001水公摊、G0002电公摊、G0003气公摊、G0004暖公摊、S1000水、S2000电、S3000气、S4000暖气、K0000宿管类)
    /// <summary>
    /// 系统费用项目(B0001物业管理、B0002车位物管、B0003出租租金、B0004特约服务、B0005产权税费、G0001水公摊、G0002电公摊、G0003气公摊、G0004暖公摊、S1000水、S2000电、S3000气、S4000暖气、K0000宿管类)
    /// </summary>
    public sealed class TWSysCost
    {
        //Proc_HSPR_Fees_SearchCost
        //Proc_HSPR_AuditingFeesDetail_Calculate
        #region 私有变量
        private static string mPropFee = "B0001";	//**物业管理
        private static string mCarFee = "B0002";	//**车位物管
        private static string mLeaseFee = "B0003";	//**出租租金
        private static string mIncidentFee = "B0004";	//**特约服务
        private static string mPropertyRightTaxFee = "B0005";	//**产权税费

        private static string mPropFeeName = "物管类";	//**物业管理
        private static string mCarFeeName = "车位类";		//**车位物管
        private static string mLeaseFeeName = "房屋租金";		//**出租租金
        private static string mIncidentFeeName = "服务类";	//**特约服务
        private static string mPropertyRightTaxFeeName = "产权类";	//**产权税费

        private static string mWaterPublic = "G0001";	//**水公摊
        private static string mElectPublic = "G0002";	//**电公摊
        private static string mGasPublic = "G0003";		//**气公摊
        private static string mWarmPublic = "G0004";	//**暖公摊

        private static string mWaterPublicName = "水公摊";		//**水公摊
        private static string mElectPublicName = "电公摊";		//**电公摊
        private static string mGasPublicName = "气公摊";		//**气公摊
        private static string mWarmPublicName = "暖公摊";		//**暖公摊

        private static string mWater = "S1000";			//**水
        private static string mEletricity = "S2000";	//**电
        private static string mGas = "S3000";			//**气
        private static string mHeating = "S4000";		//**暖气

        private static string mWaterName = "水";			//**水
        private static string mEletricityName = "电";		//**电
        private static string mGasName = "气";				//**气
        private static string mHeatingName = "暖";		//**暖气

        private static string mHostelFee = "K0000";			//**宿管类
        private static string mHostelFeeName = "宿管类";	//**宿管类

        #endregion

        public TWSysCost()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 物业管理
        /// <summary>
        /// 物业管理
        /// </summary>
        public static string PropFee
        {
            set
            {
                mPropFee = value;
            }
            get
            {
                return mPropFee;
            }
        }


        #endregion

        #region 车位物管
        /// <summary>
        /// 车位物管
        /// </summary>
        public static string CarFee
        {
            set
            {
                mCarFee = value;
            }
            get
            {
                return mCarFee;
            }
        }


        #endregion

        #region 房屋租金
        /// <summary>
        /// 出租租金
        /// </summary>
        public static string LeaseFee
        {
            set
            {
                mLeaseFee = value;
            }
            get
            {
                return mLeaseFee;
            }
        }


        #endregion

        #region 特约服务
        /// <summary>
        /// 特约服务
        /// </summary>
        public static string IncidentFee
        {
            set
            {
                mIncidentFee = value;
            }
            get
            {
                return mIncidentFee;
            }
        }


        #endregion

        #region 产权税费
        /// <summary>
        /// 产权税费
        /// </summary>
        public static string PropertyRightTaxFee
        {
            set
            {
                mPropertyRightTaxFee = value;
            }
            get
            {
                return mPropertyRightTaxFee;
            }
        }


        #endregion

        #region 水公摊
        /// <summary>
        /// 水公摊
        /// </summary>
        public static string WaterPublic
        {
            set
            {
                mWaterPublic = value;
            }
            get
            {
                return mWaterPublic;
            }
        }


        #endregion

        #region 电公摊
        /// <summary>
        /// 电公摊
        /// </summary>
        public static string ElectPublic
        {
            set
            {
                mElectPublic = value;
            }
            get
            {
                return mElectPublic;
            }
        }


        #endregion

        #region 气公摊
        /// <summary>
        /// 气公摊
        /// </summary>
        public static string GasPublic
        {
            set
            {
                mGasPublic = value;
            }
            get
            {
                return mGasPublic;
            }
        }


        #endregion

        #region 暖公摊
        /// <summary>
        /// 暖公摊
        /// </summary>
        public static string WarmPublic
        {
            set
            {
                mWarmPublic = value;
            }
            get
            {
                return mWarmPublic;
            }
        }


        #endregion

        #region 水
        /// <summary>
        /// 水
        /// </summary>
        public static string Water
        {
            set
            {
                mWater = value;
            }
            get
            {
                return mWater;
            }
        }


        #endregion

        #region 电
        /// <summary>
        /// 电
        /// </summary>
        public static string Eletricity
        {
            set
            {
                mEletricity = value;
            }
            get
            {
                return mEletricity;
            }
        }


        #endregion

        #region 气
        /// <summary>
        /// 气
        /// </summary>
        public static string Gas
        {
            set
            {
                mGas = value;
            }
            get
            {
                return mGas;
            }
        }


        #endregion

        #region 暖
        /// <summary>
        /// 暖气
        /// </summary>
        public static string Heating
        {
            set
            {
                mHeating = value;
            }
            get
            {
                return mHeating;
            }
        }


        #endregion

        #region 宿管类
        /// <summary>
        /// 宿管类
        /// </summary>
        public static string HostelFee
        {
            set
            {
                mHostelFee = value;
            }
            get
            {
                return mHostelFee;
            }
        }
        #endregion

        #endregion

        #region 名称

        #region 物业管理
        /// <summary>
        /// 物业管理
        /// </summary>
        public static string PropFeeName
        {
            set
            {
                mPropFeeName = value;
            }
            get
            {
                return mPropFeeName;
            }
        }


        #endregion

        #region 车位物管
        /// <summary>
        /// 车位物管
        /// </summary>
        public static string CarFeeName
        {
            set
            {
                mCarFeeName = value;
            }
            get
            {
                return mCarFeeName;
            }
        }


        #endregion

        #region 房屋租金
        /// <summary>
        /// 出租租金
        /// </summary>
        public static string LeaseFeeName
        {
            set
            {
                mLeaseFeeName = value;
            }
            get
            {
                return mLeaseFeeName;
            }
        }


        #endregion

        #region 特约服务
        /// <summary>
        /// 特约服务
        /// </summary>
        public static string IncidentFeeName
        {
            set
            {
                mIncidentFeeName = value;
            }
            get
            {
                return mIncidentFeeName;
            }
        }


        #endregion

        #region 产权税费
        /// <summary>
        /// 产权税费
        /// </summary>
        public static string PropertyRightTaxFeeName
        {
            set
            {
                mPropertyRightTaxFeeName = value;
            }
            get
            {
                return mPropertyRightTaxFeeName;
            }
        }


        #endregion

        #region 水公摊
        /// <summary>
        /// 水公摊
        /// </summary>
        public static string WaterPublicName
        {
            set
            {
                mWaterPublicName = value;
            }
            get
            {
                return mWaterPublicName;
            }
        }


        #endregion

        #region 电公摊
        /// <summary>
        /// 电公摊
        /// </summary>
        public static string ElectPublicName
        {
            set
            {
                mElectPublicName = value;
            }
            get
            {
                return mElectPublicName;
            }
        }


        #endregion

        #region 气公摊
        /// <summary>
        /// 气公摊
        /// </summary>
        public static string GasPublicName
        {
            set
            {
                mGasPublicName = value;
            }
            get
            {
                return mGasPublicName;
            }
        }


        #endregion

        #region 暖公摊
        /// <summary>
        /// 暖公摊
        /// </summary>
        public static string WarmPublicName
        {
            set
            {
                mWarmPublicName = value;
            }
            get
            {
                return mWarmPublicName;
            }
        }


        #endregion

        #region 水
        /// <summary>
        /// 水
        /// </summary>
        public static string WaterName
        {
            set
            {
                mWaterName = value;
            }
            get
            {
                return mWaterName;
            }
        }


        #endregion

        #region 电
        /// <summary>
        /// 电
        /// </summary>
        public static string EletricityName
        {
            set
            {
                mEletricityName = value;
            }
            get
            {
                return mEletricityName;
            }
        }


        #endregion

        #region 气
        /// <summary>
        /// 气
        /// </summary>
        public static string GasName
        {
            set
            {
                mGasName = value;
            }
            get
            {
                return mGasName;
            }
        }


        #endregion

        #region 暖
        /// <summary>
        /// 暖
        /// </summary>
        public static string HeatingName
        {
            set
            {
                mHeatingName = value;
            }
            get
            {
                return mHeatingName;
            }
        }
        #endregion

        #region 宿管类
        /// <summary>
        /// 宿管类
        /// </summary>
        public static string HostelFeeName
        {
            set
            {
                mHostelFeeName = value;
            }
            get
            {
                return mHostelFeeName;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

    #region 银行状态(0等待新工作、1无新工作,本日完成、2生成数据完成，等待处理等)
    /// <summary>
    /// 银行状态(0等待新工作、1无新工作,本日完成、2生成数据完成，等待处理等)
    /// </summary>
    public sealed class TWBankState
    {
        #region 私有变量

        private static int mWaitNew = 0;		//**等待新工作
        private static int mWaitNo = 1;			//**无新工作,本日完成

        private static int mDoneWait = 2;		//**生成数据完成，等待处理
        private static int mDoneConWait = 3;		//**无处理结果，继续等待

        private static int mCheckingNew = 11;		//**检查有无新工作中
        private static int mCheckingResult = 12;	//**检查有无处理结果中

        private static int mDoingCreate = 13;		//**有新工作，生成数据中
        private static int mDoingDeal = 14;		//**有处理结果，处理中

        #endregion

        public TWBankState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region 等待新工作
        /// <summary>
        /// 等待新工作
        /// </summary>
        public static int WaitNew
        {
            set
            {
                mWaitNew = value;
            }
            get
            {
                return mWaitNew;
            }
        }


        #endregion

        #region 无新工作,本日完成
        /// <summary>
        /// 无新工作,本日完成
        /// </summary>
        public static int WaitNo
        {
            set
            {
                mWaitNo = value;
            }
            get
            {
                return mWaitNo;
            }
        }


        #endregion

        #region 生成数据完成，等待处理
        /// <summary>
        /// 生成数据完成，等待处理
        /// </summary>
        public static int DoneWait
        {
            set
            {
                mDoneWait = value;
            }
            get
            {
                return mDoneWait;
            }
        }


        #endregion

        #region 无处理结果，继续等待
        /// <summary>
        /// 无处理结果，继续等待
        /// </summary>
        public static int DoneConWait
        {
            set
            {
                mDoneConWait = value;
            }
            get
            {
                return mDoneConWait;
            }
        }


        #endregion

        #region 检查有无新工作中
        /// <summary>
        /// 检查有无新工作中
        /// </summary>
        public static int CheckingNew
        {
            set
            {
                mCheckingNew = value;
            }
            get
            {
                return mCheckingNew;
            }
        }


        #endregion

        #region 检查有无处理结果中
        /// <summary>
        /// 检查有无处理结果中
        /// </summary>
        public static int CheckingResult
        {
            set
            {
                mCheckingResult = value;
            }
            get
            {
                return mCheckingResult;
            }
        }


        #endregion

        #region 有新工作，生成数据中
        /// <summary>
        /// 有新工作，生成数据中
        /// </summary>
        public static int DoingCreate
        {
            set
            {
                mDoingCreate = value;
            }
            get
            {
                return mDoingCreate;
            }
        }


        #endregion

        #region 有处理结果，处理中
        /// <summary>
        /// 有处理结果，处理中
        /// </summary>
        public static int DoingDeal
        {
            set
            {
                mDoingDeal = value;
            }
            get
            {
                return mDoingDeal;
            }
        }


        #endregion

        #endregion

    }
    #endregion

    #region 代收状态(1等待处理、2处理中、3处理完成)
    /// <summary>
    /// 代收状态(1等待处理、2处理中、3处理完成)
    /// </summary>
    public sealed class TWSurrState
    {
        #region 私有变量
        private static int mWait = 1;		//**等待处理
        private static int mDoing = 2;		//**处理中
        private static int mDone = 3;		//**处理完成

        #endregion

        public TWSurrState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region 等待处理
        /// <summary>
        /// 等待处理
        /// </summary>
        public static int Wait
        {
            set
            {
                mWait = value;
            }
            get
            {
                return mWait;
            }
        }


        #endregion

        #region 处理中
        /// <summary>
        /// 处理中
        /// </summary>
        public static int Doing
        {
            set
            {
                mDoing = value;
            }
            get
            {
                return mDoing;
            }
        }


        #endregion

        #region 处理完成
        /// <summary>
        /// 处理完成
        /// </summary>
        public static int Done
        {
            set
            {
                mDone = value;
            }
            get
            {
                return mDone;
            }
        }


        #endregion

        #endregion

    }
    #endregion

    #region 房屋合并拆分状态(0未处理,1拆分之前、2拆分以后、3合并之前、4合并之后)

    /// <summary>
    /// 合并拆分状态(0未处理,1拆分之前、2拆分以后、3合并之前、4合并之后)
    /// </summary>
    public sealed class TWRoomSplitUnite
    {
        #region 私有变量

        /// <summary> 0未处理
        ///0未处理
        /// </summary>
        private static int mDeal = 0;               //**未处理

        private static int mBeforeSplit = 1;		//**拆分之前
        private static int mAfterSplit = 2;		//**拆分之后
        private static int mBeforeUnite = 3;		//**合并之前
        private static int mAfterUnite = 4;		//**合并以后

        #endregion

        public TWRoomSplitUnite()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region 未处理
        /// <summary> 0未处理
        ///0未处理
        /// </summary>
        public static int Deal
        {
            set
            {
                mDeal = value;
            }
            get
            {
                return mDeal;
            }
        }


        #endregion

        #region 拆分之前
        /// <summary>
        /// 拆分之前
        /// </summary>
        public static int BeforeSplit
        {
            set
            {
                mBeforeSplit = value;
            }
            get
            {
                return mBeforeSplit;
            }
        }


        #endregion

        #region 拆分之后
        /// <summary>
        /// 拆分之后
        /// </summary>
        public static int AfterSplit
        {
            set
            {
                mAfterSplit = value;
            }
            get
            {
                return mAfterSplit;
            }
        }


        #endregion

        #region 合并之前
        /// <summary>
        /// 合并之前
        /// </summary>
        public static int BeforeUnite
        {
            set
            {
                mBeforeUnite = value;
            }
            get
            {
                return mBeforeUnite;
            }
        }


        #endregion

        #region 合并之后
        /// <summary>
        /// 合并之后
        /// </summary>
        public static int AfterUnite
        {
            set
            {
                mAfterUnite = value;
            }
            get
            {
                return mAfterUnite;
            }
        }


        #endregion





        #endregion

    }

    #endregion

    #region 交款类型(1自付、2垫付、3代付)
    /// <summary>
    /// 交款类型(1自付、2垫付、3代付)
    /// </summary>
    public sealed class TWRenderType
    {
        #region 私有变量

        private static int mSelf = 1;		//**自付
        private static int mAdvance = 2;		//**垫付
        private static int mInstead = 3;		//**代付

        private static string mSelfName = "自付";
        private static string mAdvanceName = "垫付";
        private static string mInsteadName = "代付";

        #endregion

        public TWRenderType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID
        #region 自付
        /// <summary>
        /// 自付
        /// </summary>
        public static int Self
        {
            set
            {
                mSelf = value;
            }
            get
            {
                return mSelf;
            }
        }


        #endregion

        #region 垫付
        /// <summary>
        /// 垫付
        /// </summary>
        public static int Advance
        {
            set
            {
                mAdvance = value;
            }
            get
            {
                return mAdvance;
            }
        }


        #endregion

        #region 代付
        /// <summary>
        /// 代付
        /// </summary>
        public static int Instead
        {
            set
            {
                mInstead = value;
            }
            get
            {
                return mInstead;
            }
        }


        #endregion




        #endregion

        #region 名称

        #region 自付
        /// <summary>
        /// 自付
        /// </summary>
        public static string SelfName
        {
            set
            {
                mSelfName = value;
            }
            get
            {
                return mSelfName;
            }
        }


        #endregion

        #region 垫付
        /// <summary>
        /// 垫付
        /// </summary>
        public static string AdvanceName
        {
            set
            {
                mAdvanceName = value;
            }
            get
            {
                return mAdvanceName;
            }
        }


        #endregion

        #region 代付
        /// <summary>
        /// 代付
        /// </summary>
        public static string InsteadName
        {
            set
            {
                mInsteadName = value;
            }
            get
            {
                return mInsteadName;
            }
        }


        #endregion




        #endregion

        #endregion

    }
    #endregion

    #region 票据种类(0发票、1收据、2退款)
    /// <summary>
    /// 票据种类(0发票、1收据、2退款)
    /// </summary>
    public sealed class TWBillKind
    {
        #region 私有变量

        private static int mInvoice = 0;		//**发票
        private static int mReceipt = 1;		//**收据
        private static int mProof = 2;			//**退款凭据

        private static string mInvoiceName = "发票";
        private static string mReceiptName = "收据";
        private static string mProofName = "凭据";

        #endregion

        public TWBillKind()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 发票
        /// <summary>
        /// 发票
        /// </summary>
        public static int Invoice
        {
            set
            {
                mInvoice = value;
            }
            get
            {
                return mInvoice;
            }
        }


        #endregion

        #region 收据
        /// <summary>
        /// 收据
        /// </summary>
        public static int Receipt
        {
            set
            {
                mReceipt = value;
            }
            get
            {
                return mReceipt;
            }
        }
        #endregion

        #region 退款凭据
        /// <summary>
        /// 退款凭据
        /// </summary>
        public static int Proof
        {
            set
            {
                mProof = value;
            }
            get
            {
                return mProof;
            }
        }
        #endregion

        #endregion

        #region 名称

        #region 发票
        /// <summary>
        /// 发票
        /// </summary>
        public static string InvoiceName
        {
            set
            {
                mInvoiceName = value;
            }
            get
            {
                return mInvoiceName;
            }
        }


        #endregion

        #region 收据
        /// <summary>
        /// 收据
        /// </summary>
        public static string ReceiptName
        {
            set
            {
                mReceiptName = value;
            }
            get
            {
                return mReceiptName;
            }
        }


        #endregion

        #region 退款凭据
        /// <summary>
        /// 退款凭据
        /// </summary>
        public static string ProofName
        {
            set
            {
                mProofName = value;
            }
            get
            {
                return mProofName;
            }
        }


        #endregion



        #endregion

        #endregion

    }
    #endregion

    #region 票据使用说明(1收费票据、2预交票据、3其它票据、4换票票据、5垫付票据、6托收票据、7退款凭据、8预交退款凭据、-1作废票据)
    /// <summary>
    /// 票据使用说明(1收费票据、2预交票据、3其它票据、4换票票据、5垫付票据、6托收票据、7退款凭据、8预交退款凭据、-1作废票据)
    /// </summary>
    public sealed class TWBillUseCase
    {
        #region 私有变量
        private static int mChgFee = 1;			//**收费票据
        private static int mPreCosts = 2;		//**预交票据
        private static int mOther = 3;			//**其它票据
        private static int mInstead = 4;		//**换票票据
        private static int mAdvance = 5;		//**垫付票据
        private static int mConSign = 6;		//**托收票据
        private static int mRefund = 7;			//**退款凭据
        private static int mPrecRefund = 8;		//**预交退款凭据
        private static int mCancel = -1;		//**作废票据

        private static string mChgFeeName = "收费票据";
        private static string mPreCostsName = "预交票据";
        private static string mOtherName = "其它票据";
        private static string mInsteadName = "换票票据";
        private static string mAdvanceName = "垫付票据";
        private static string mConSignName = "托收票据";
        private static string mRefundName = "退款凭据";
        private static string mPrecRefundName = "预交退款凭据";
        private static string mCancelName = "作废票据";

        #endregion

        public TWBillUseCase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 收费票据
        /// <summary>
        /// 收费票据
        /// </summary>
        public static int ChgFee
        {
            set
            {
                mChgFee = value;
            }
            get
            {
                return mChgFee;
            }
        }


        #endregion

        #region 预交票据
        /// <summary>
        /// 预交票据
        /// </summary>
        public static int PreCosts
        {
            set
            {
                mPreCosts = value;
            }
            get
            {
                return mPreCosts;
            }
        }


        #endregion

        #region 其它票据
        /// <summary>
        /// 其它票据
        /// </summary>
        public static int Other
        {
            set
            {
                mOther = value;
            }
            get
            {
                return mOther;
            }
        }


        #endregion

        #region 换票票据
        /// <summary>
        /// 换票票据
        /// </summary>
        public static int Instead
        {
            set
            {
                mInstead = value;
            }
            get
            {
                return mInstead;
            }
        }


        #endregion

        #region 垫付票据
        /// <summary>
        /// 垫付票据
        /// </summary>
        public static int Advance
        {
            set
            {
                mAdvance = value;
            }
            get
            {
                return mAdvance;
            }
        }


        #endregion

        #region 托收票据
        /// <summary>
        /// 托收票据
        /// </summary>
        public static int ConSign
        {
            set
            {
                mConSign = value;
            }
            get
            {
                return mConSign;
            }
        }


        #endregion

        #region 退款凭据
        /// <summary>
        /// 退款凭据
        /// </summary>
        public static int Refund
        {
            set
            {
                mRefund = value;
            }
            get
            {
                return mRefund;
            }
        }


        #endregion

        #region 预交退款凭据
        /// <summary>
        /// 预交退款凭据
        /// </summary>
        public static int PrecRefund
        {
            set
            {
                mPrecRefund = value;
            }
            get
            {
                return mPrecRefund;
            }
        }


        #endregion

        #region 作废票据
        /// <summary>
        /// 作废票据
        /// </summary>
        public static int Cancel
        {
            set
            {
                mCancel = value;
            }
            get
            {
                return mCancel;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 收费票据
        /// <summary>
        /// 收费票据
        /// </summary>
        public static string ChgFeeName
        {
            set
            {
                mChgFeeName = value;
            }
            get
            {
                return mChgFeeName;
            }
        }


        #endregion

        #region 预交票据
        /// <summary>
        /// 预交票据
        /// </summary>
        public static string PreCostsName
        {
            set
            {
                mPreCostsName = value;
            }
            get
            {
                return mPreCostsName;
            }
        }


        #endregion

        #region 其它票据
        /// <summary>
        /// 其它票据
        /// </summary>
        public static string OtherName
        {
            set
            {
                mOtherName = value;
            }
            get
            {
                return mOtherName;
            }
        }


        #endregion

        #region 换票票据
        /// <summary>
        /// 换票票据
        /// </summary>
        public static string InsteadName
        {
            set
            {
                mInsteadName = value;
            }
            get
            {
                return mInsteadName;
            }
        }


        #endregion

        #region 垫付票据
        /// <summary>
        /// 垫付票据
        /// </summary>
        public static string AdvanceName
        {
            set
            {
                mAdvanceName = value;
            }
            get
            {
                return mAdvanceName;
            }
        }


        #endregion

        #region 托收票据
        /// <summary>
        /// 托收票据
        /// </summary>
        public static string ConSignName
        {
            set
            {
                mConSignName = value;
            }
            get
            {
                return mConSignName;
            }
        }


        #endregion

        #region 退款凭据
        /// <summary>
        /// 退款凭据
        /// </summary>
        public static string RefundName
        {
            set
            {
                mRefundName = value;
            }
            get
            {
                return mRefundName;
            }
        }


        #endregion

        #region 预交退款凭据
        /// <summary>
        /// 预交退款凭据
        /// </summary>
        public static string PrecRefundName
        {
            set
            {
                mPrecRefundName = value;
            }
            get
            {
                return mPrecRefundName;
            }
        }


        #endregion

        #region 作废票据
        /// <summary>
        /// 作废票据
        /// </summary>
        public static string CancelName
        {
            set
            {
                mCancelName = value;
            }
            get
            {
                return mCancelName;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

    #region 集团编码(01集团)
    /// <summary>
    /// 集团编码(01集团)
    /// </summary>
    public sealed class TWSysNodeCode
    {
        //Proc_HSPR_Fees_SearchCost
        //Proc_HSPR_AuditingFeesDetail_Calculate
        #region 私有变量
        private static string mBlocCode = "01";	//**集团
        #endregion

        public TWSysNodeCode()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 集团
        /// <summary>
        /// 集团
        /// </summary>
        public static string BlocCode
        {
            set
            {
                mBlocCode = value;
            }
            get
            {
                return mBlocCode;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

    #region 报事类型(1书面派工、2口头派工、3返修处理、4投诉处理)
    /// <summary>
    /// 报事类型(1书面派工、2口头派工、3返修处理、4投诉处理)
    /// </summary>
    public sealed class TWIncidentClass
    {
        #region 私有变量

        private static int mPaper = 1;		//**书面派工
        private static int mOral = 2;		//**口头派工
        private static int mRework = 3;		//**返修处理
        private static int mComplaints = 4;		//**投诉处理

        private static string mPaperName = "书面派工";
        private static string mOralName = "口头派工";
        private static string mReworkName = "返修处理";
        private static string mComplaintsName = "投诉处理";

        #endregion

        public TWIncidentClass()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 书面派工
        /// <summary>
        /// 自付
        /// </summary>
        public static int Paper
        {
            set
            {
                mPaper = value;
            }
            get
            {
                return mPaper;
            }
        }


        #endregion

        #region 口头派工
        /// <summary>
        /// 口头派工
        /// </summary>
        public static int Oral
        {
            set
            {
                mOral = value;
            }
            get
            {
                return mOral;
            }
        }


        #endregion

        #region 返修处理
        /// <summary>
        /// 返修处理
        /// </summary>
        public static int Rework
        {
            set
            {
                mRework = value;
            }
            get
            {
                return mRework;
            }
        }


        #endregion

        #region 投诉处理
        /// <summary>
        /// 投诉处理
        /// </summary>
        public static int Complaints
        {
            set
            {
                mComplaints = value;
            }
            get
            {
                return mComplaints;
            }
        }


        #endregion


        #endregion

        #region 名称

        #region 书面派工
        /// <summary>
        /// 书面派工
        /// </summary>
        public static string PaperName
        {
            set
            {
                mPaperName = value;
            }
            get
            {
                return mPaperName;
            }
        }


        #endregion

        #region 口头派工
        /// <summary>
        /// 口头派工
        /// </summary>
        public static string OralName
        {
            set
            {
                mOralName = value;
            }
            get
            {
                return mOralName;
            }
        }


        #endregion

        #region 返修处理
        /// <summary>
        /// 返修处理
        /// </summary>
        public static string ReworkName
        {
            set
            {
                mReworkName = value;
            }
            get
            {
                return mReworkName;
            }
        }


        #endregion

        #region 投诉处理
        /// <summary>
        /// 投诉处理
        /// </summary>
        public static string ComplaintsName
        {
            set
            {
                mComplaintsName = value;
            }
            get
            {
                return mComplaintsName;
            }
        }


        #endregion


        #endregion

        #endregion

    }
    #endregion

    #region 报事派工类型(1派工单派工、2协调单派工、3口头派工、4返修处理、5投诉处理)
    /// <summary>
    /// 报事派工类型(1派工单派工、2协调单派工、3口头派工、4返修处理、5投诉处理)
    /// </summary>
    public sealed class TWIncidentDispType
    {
        #region 私有变量

        private static int mDispatching = 1;		//**派工单派工
        private static int mCoordination = 2;		//**协调单派工
        private static int mOralDispatching = 3;		//**口头派工
        private static int mReworkDispatching = 4;		//**返修处理
        private static int mComplaintsDispatching = 5;		//**投诉处理

        private static string mDispatchingName = "派工单派工";
        private static string mCoordinationName = "协调单派工";
        private static string mOralDispatchingName = "口头派工";
        private static string mReworkDispatchingName = "返修处理";
        private static string mComplaintsDispatchingName = "投诉处理";

        #endregion

        public TWIncidentDispType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 派工单派工
        /// <summary>
        /// 派工单派工
        /// </summary>
        public static int Dispatching
        {
            set
            {
                mDispatching = value;
            }
            get
            {
                return mDispatching;
            }
        }


        #endregion

        #region 协调单派工
        /// <summary>
        /// 协调单派工
        /// </summary>
        public static int Coordination
        {
            set
            {
                mCoordination = value;
            }
            get
            {
                return mCoordination;
            }
        }


        #endregion

        #region 口头派工
        /// <summary>
        /// 口头派工
        /// </summary>
        public static int OralDispatching
        {
            set
            {
                mOralDispatching = value;
            }
            get
            {
                return mOralDispatching;
            }
        }


        #endregion

        #region 返修处理
        /// <summary>
        /// 返修处理
        /// </summary>
        public static int ReworkDispatching
        {
            set
            {
                mReworkDispatching = value;
            }
            get
            {
                return mReworkDispatching;
            }
        }


        #endregion

        #region 投诉处理
        /// <summary>
        /// 投诉处理
        /// </summary>
        public static int ComplaintsDispatching
        {
            set
            {
                mComplaintsDispatching = value;
            }
            get
            {
                return mComplaintsDispatching;
            }
        }


        #endregion


        #endregion

        #region 名称

        #region 派工单派工
        /// <summary>
        /// 派工单派工
        /// </summary>
        public static string DispatchingName
        {
            set
            {
                mDispatchingName = value;
            }
            get
            {
                return mDispatchingName;
            }
        }


        #endregion

        #region 协调单派工
        /// <summary>
        /// 协调单派工
        /// </summary>
        public static string CoordinationName
        {
            set
            {
                mCoordinationName = value;
            }
            get
            {
                return mCoordinationName;
            }
        }


        #endregion

        #region 口头派工
        /// <summary>
        /// 口头派工
        /// </summary>
        public static string OralDispatchingName
        {
            set
            {
                mOralDispatchingName = value;
            }
            get
            {
                return mOralDispatchingName;
            }
        }


        #endregion

        #region 返修处理
        /// <summary>
        /// 返修处理
        /// </summary>
        public static string ReworkDispatchingName
        {
            set
            {
                mReworkDispatchingName = value;
            }
            get
            {
                return mReworkDispatchingName;
            }
        }


        #endregion

        #region 投诉处理
        /// <summary>
        /// 投诉处理
        /// </summary>
        public static string ComplaintsDispatchingName
        {
            set
            {
                mComplaintsDispatchingName = value;
            }
            get
            {
                return mComplaintsDispatchingName;
            }
        }


        #endregion


        #endregion

        #endregion

    }
    #endregion

    #region 项目类型(1物业项目、2会所项目、3宿舍项目)
    /// <summary>
    /// 项目类型(1物业项目、2会所项目、3宿舍项目)
    /// </summary>
    public sealed class TWCommType
    {
        #region 私有变量
        private static int mProperty = 1;	//**物业项目
        private static int mClub = 2;		//**会所项目
        private static int mHostel = 3;		//**宿舍项目

        private static string mPropertyName = "物业项目";
        private static string mClubName = "会所项目";
        private static string mHostelName = "宿舍项目";

        #endregion

        public TWCommType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID
        #region 物业项目
        /// <summary>
        /// 物业项目
        /// </summary>
        public static int Club
        {
            set
            {
                mClub = value;
            }
            get
            {
                return mClub;
            }
        }


        #endregion

        #region 会所项目
        /// <summary>
        /// 会所项目
        /// </summary>
        public static int Property
        {
            set
            {
                mProperty = value;
            }
            get
            {
                return mProperty;
            }
        }


        #endregion

        #region 宿舍项目
        /// <summary>
        /// 宿舍项目
        /// </summary>
        public static int Hostel
        {
            set
            {
                mHostel = value;
            }
            get
            {
                return mHostel;
            }
        }
        #endregion

        #endregion

        #region 名称

        #region 物业项目
        /// <summary>
        /// 物业项目
        /// </summary>
        public static string ClubName
        {
            set
            {
                mClubName = value;
            }
            get
            {
                return mClubName;
            }
        }


        #endregion

        #region 会所项目
        /// <summary>
        /// 会所项目
        /// </summary>
        public static string PropertyName
        {
            set
            {
                mPropertyName = value;
            }
            get
            {
                return mPropertyName;
            }
        }


        #endregion

        #region 宿舍项目
        /// <summary>
        /// 宿舍项目
        /// </summary>
        public static string HostelName
        {
            set
            {
                mHostelName = value;
            }
            get
            {
                return mHostelName;
            }
        }
        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 宿管系统

    #region 床位状态(0空置、1入住、2停用)
    /// <summary>
    /// 报事类型(0空置、1入住、2已坏、3停止使用)
    /// </summary>
    public sealed class HostelBedState
    {
        #region 私有变量

        private static int mVacant = 0;		//**空置
        private static int mCheck = 1;		//**入住
        //		private static int mBad = 3;		//**停用
        private static int mStopUs = 2;		//**停止使用
        private static int mLayout = 3;		//**安排

        private static string mVacantName = "空置";
        private static string mCheckName = "入住";
        //		private static string mBadName = "停用";
        private static string mStopUsName = "停用";
        private static string mLayoutName = "安排";


        #endregion

        public HostelBedState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 空置
        /// <summary>
        /// 空置
        /// </summary>
        public static int Vacant
        {
            set
            {
                mVacant = value;
            }
            get
            {
                return mVacant;
            }
        }


        #endregion

        #region 入住
        /// <summary>
        /// 入住
        /// </summary>
        public static int Check
        {
            set
            {
                mCheck = value;
            }
            get
            {
                return mCheck;
            }
        }


        #endregion

        #region 损坏
        //		/// <summary>
        //		/// 已坏
        //		/// </summary>
        //		public static int Bad
        //		{
        //			set
        //			{
        //				mBad=value;
        //			}
        //			get
        //			{
        //				return mBad;
        //			}
        //		}


        #endregion

        #region 停用
        /// <summary>
        /// 停止使用
        /// </summary>
        public static int StopUs
        {
            set
            {
                mStopUs = value;
            }
            get
            {
                return mStopUs;
            }
        }


        #endregion

        #region 安排
        /// <summary>
        /// 安排
        /// </summary>
        public static int Layout
        {
            set
            {
                mLayout = value;
            }
            get
            {
                return mLayout;
            }
        }


        #endregion


        #endregion

        #region 名称

        #region 空置
        /// <summary>
        /// 空置
        /// </summary>
        public static string VacantName
        {
            set
            {
                mVacantName = value;
            }
            get
            {
                return mVacantName;
            }
        }


        #endregion

        #region 入住
        /// <summary>
        /// 入住
        /// </summary>
        public static string CheckName
        {
            set
            {
                mCheckName = value;
            }
            get
            {
                return mCheckName;
            }
        }


        #endregion

        #region 损坏
        //		/// <summary>
        //		/// 已坏
        //		/// </summary>
        //		public static string BadName
        //		{
        //			set
        //			{
        //				mBadName=value;
        //			}
        //			get
        //			{
        //				return mBadName;
        //			}
        //		}


        #endregion

        #region 停用
        /// <summary>
        /// 停止使用
        /// </summary>
        public static string StopUsName
        {
            set
            {
                mStopUsName = value;
            }
            get
            {
                return mStopUsName;
            }
        }


        #endregion


        #region 安排
        /// <summary>
        /// 安排
        /// </summary>
        public static string LayoutName
        {
            set
            {
                mLayoutName = value;
            }
            get
            {
                return mLayoutName;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

    #endregion

    #region 会员卡状态(0可用、1停用、2挂失、3转让、4退卡)
    /// <summary>
    /// 会员卡状态(0可用、1停用、2挂失、3转让、4退卡)
    /// </summary>
    public sealed class TWCardState
    {
        #region 私有变量
        private static int mCanUse = 0;		//**可用
        private static int mStopUse = 1;	//**停用
        private static int mLoss = 2;		//**挂失
        private static int mTransfer = 3;	//**转让
        private static int mRefund = 4;		//**退卡

        private static string mCanUseName = "可用";
        private static string mStopUseName = "停用";
        private static string mLossName = "挂失";
        private static string mTransferName = "转让";
        private static string mRefundName = "退卡";

        #endregion

        public TWCardState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 可用
        /// <summary>
        /// 可用
        /// </summary>
        public static int CanUse
        {
            set
            {
                mCanUse = value;
            }
            get
            {
                return mCanUse;
            }
        }


        #endregion

        #region 停用
        /// <summary>
        /// 停用
        /// </summary>
        public static int StopUse
        {
            set
            {
                mStopUse = value;
            }
            get
            {
                return mStopUse;
            }
        }


        #endregion

        #region 挂失
        /// <summary>
        /// 挂失
        /// </summary>
        public static int Loss
        {
            set
            {
                mLoss = value;
            }
            get
            {
                return mLoss;
            }
        }


        #endregion

        #region 转让
        /// <summary>
        /// 转让
        /// </summary>
        public static int Transfer
        {
            set
            {
                mTransfer = value;
            }
            get
            {
                return mTransfer;
            }
        }


        #endregion

        #region 退卡
        /// <summary>
        /// 退卡
        /// </summary>
        public static int Refund
        {
            set
            {
                mRefund = value;
            }
            get
            {
                return mRefund;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 可用
        /// <summary>
        /// 可用
        /// </summary>
        public static string CanUseName
        {
            set
            {
                mCanUseName = value;
            }
            get
            {
                return mCanUseName;
            }
        }


        #endregion

        #region 停用
        /// <summary>
        /// 停用
        /// </summary>
        public static string StopUseName
        {
            set
            {
                mStopUseName = value;
            }
            get
            {
                return mStopUseName;
            }
        }


        #endregion

        #region 挂失
        /// <summary>
        /// 挂失
        /// </summary>
        public static string LossName
        {
            set
            {
                mLossName = value;
            }
            get
            {
                return mLossName;
            }
        }


        #endregion

        #region 转让
        /// <summary>
        /// 转让
        /// </summary>
        public static string TransferName
        {
            set
            {
                mTransferName = value;
            }
            get
            {
                return mTransferName;
            }
        }


        #endregion

        #region 退卡
        /// <summary>
        /// 退卡
        /// </summary>
        public static string RefundName
        {
            set
            {
                mRefundName = value;
            }
            get
            {
                return mRefundName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 会所客户类别(1会员、10非会员-业主、11非会员-非业主、20挂账客户-地产签单客户、21挂账客户-物业部门经理)
    /// <summary>
    /// 会所客户类别(1会员、10非会员-业主、11非会员-非业主、20挂账客户-地产签单客户、21挂账客户-物业部门经理)
    /// </summary>
    public sealed class TWConsumeType
    {
        #region 私有变量
        private static int mMember = 1;				//**会员
        private static int mNoMemberOwner = 10;		//**非会员-业主
        private static int mNoMemberNoOwner = 11;	//**非会员-非业主
        private static int mInternalEstate = 20;	//**挂账客户-地产签单客户
        private static int mInternalManager = 21;	//**挂账客户-物业部门经理

        private static string mMemberName = "会员";
        private static string mNoMemberOwnerName = "非会员-业主";
        private static string mNoMemberNoOwnerName = "非会员-非业主";
        private static string mInternalEstateName = "挂账客户-地产签单客户";
        private static string mInternalManagerName = "挂账客户-物业部门经理";


        #endregion

        public TWConsumeType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 会员
        /// <summary>
        /// 会员
        /// </summary>
        public static int Member
        {
            set
            {
                mMember = value;
            }
            get
            {
                return mMember;
            }
        }


        #endregion

        #region 非会员-业主
        /// <summary>
        /// 非会员-业主
        /// </summary>
        public static int NoMemberOwner
        {
            set
            {
                mNoMemberOwner = value;
            }
            get
            {
                return mNoMemberOwner;
            }
        }
        #endregion

        #region 非会员-非业主
        /// <summary>
        /// 非会员-非业主
        /// </summary>
        public static int NoMemberNoOwner
        {
            set
            {
                mNoMemberNoOwner = value;
            }
            get
            {
                return mNoMemberNoOwner;
            }
        }
        #endregion

        #region 挂账客户-地产签单客户
        /// <summary>
        /// 挂账客户-地产签单客户
        /// </summary>
        public static int InternalEstate
        {
            set
            {
                mInternalEstate = value;
            }
            get
            {
                return mInternalEstate;
            }
        }
        #endregion

        #region 挂账客户-物业部门经理
        /// <summary>
        /// 挂账客户-物业部门经理
        /// </summary>
        public static int InternalManager
        {
            set
            {
                mInternalManager = value;
            }
            get
            {
                return mInternalManager;
            }
        }
        #endregion

        #endregion

        #region 名称

        #region 会员
        /// <summary>
        /// 会员
        /// </summary>
        public static string MemberName
        {
            set
            {
                mMemberName = value;
            }
            get
            {
                return mMemberName;
            }
        }
        #endregion

        #region 非会员-业主
        /// <summary>
        /// 非会员-业主
        /// </summary>
        public static string NoMemberOwnerName
        {
            set
            {
                mNoMemberOwnerName = value;
            }
            get
            {
                return mNoMemberOwnerName;
            }
        }
        #endregion

        #region 非会员-非业主
        /// <summary>
        /// 非会员-非业主
        /// </summary>
        public static string NoMemberNoOwnerName
        {
            set
            {
                mNoMemberNoOwnerName = value;
            }
            get
            {
                return mNoMemberNoOwnerName;
            }
        }
        #endregion

        #region 挂账客户-地产签单客户
        /// <summary>
        /// 挂账客户-地产签单客户
        /// </summary>
        public static string InternalEstateName
        {
            set
            {
                mInternalEstateName = value;
            }
            get
            {
                return mInternalEstateName;
            }
        }
        #endregion

        #region 挂账客户-物业部门经理
        /// <summary>
        /// 挂账客户-物业部门经理
        /// </summary>
        public static string InternalManagerName
        {
            set
            {
                mInternalManagerName = value;
            }
            get
            {
                return mInternalManagerName;
            }
        }
        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 营业类型(1场馆、2客房、3物品、4清吧、5商品)
    /// <summary>
    /// 营业类型(1场馆、2客房、3物品、4清吧、5商品)
    /// </summary>
    public sealed class TWBusinessType
    {
        #region 私有变量
        private static int mVenues = 1;		//**场馆
        private static int mRooms = 2;		//**客房
        private static int mMaterials = 3;	//**物品
        private static int mBar = 4;		//**清吧
        private static int mMerch = 5;		//**商品

        private static string mVenuesName = "场馆";
        private static string mRoomsName = "客房";
        private static string mMaterialsName = "物品";
        private static string mBarName = "清吧";
        private static string mMerchName = "商品";

        #endregion

        public TWBusinessType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 场馆
        /// <summary>
        /// 场馆
        /// </summary>
        public static int Venues
        {
            set
            {
                mVenues = value;
            }
            get
            {
                return mVenues;
            }
        }


        #endregion

        #region 客房
        /// <summary>
        /// 客房
        /// </summary>
        public static int Rooms
        {
            set
            {
                mRooms = value;
            }
            get
            {
                return mRooms;
            }
        }


        #endregion

        #region 物品
        /// <summary>
        /// 物品
        /// </summary>
        public static int Materials
        {
            set
            {
                mMaterials = value;
            }
            get
            {
                return mMaterials;
            }
        }


        #endregion

        #region 清吧
        /// <summary>
        /// 清吧
        /// </summary>
        public static int Bar
        {
            set
            {
                mBar = value;
            }
            get
            {
                return mBar;
            }
        }


        #endregion

        #region 商品
        /// <summary>
        /// 商品
        /// </summary>
        public static int Merch
        {
            set
            {
                mMerch = value;
            }
            get
            {
                return mMerch;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 场馆
        /// <summary>
        /// 场馆
        /// </summary>
        public static string VenuesName
        {
            set
            {
                mVenuesName = value;
            }
            get
            {
                return mVenuesName;
            }
        }


        #endregion

        #region 客房
        /// <summary>
        /// 客房
        /// </summary>
        public static string RoomsName
        {
            set
            {
                mRoomsName = value;
            }
            get
            {
                return mRoomsName;
            }
        }


        #endregion

        #region 物品
        /// <summary>
        /// 物品
        /// </summary>
        public static string MaterialsName
        {
            set
            {
                mMaterialsName = value;
            }
            get
            {
                return mMaterialsName;
            }
        }


        #endregion

        #region 清吧
        /// <summary>
        /// 清吧
        /// </summary>
        public static string BarName
        {
            set
            {
                mBarName = value;
            }
            get
            {
                return mBarName;
            }
        }


        #endregion

        #region 商品
        /// <summary>
        /// 商品
        /// </summary>
        public static string MerchName
        {
            set
            {
                mMerchName = value;
            }
            get
            {
                return mMerchName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 商品类别性质(1场馆、2客房、3清吧、4代销、5租赁、6会员卡、7普通卡)
    /// <summary>
    /// 商品类别性质(1场馆、2客房、3清吧、4代销、5租赁、6会员卡、7普通卡)
    /// </summary>
    public sealed class TWMerchTypeKind
    {
        #region 私有变量
        private static int mVenues = 1;		//**场馆
        private static int mRooms = 2;		//**客房
        private static int mMaterial = 3;		//**物品
        //private static int mConsign = 4;	//**代销
        private static int mLease = 5;		//**租赁		
        private static int mMemberCard = 6;		//**会员卡
        private static int mGeneralCard = 7;	//**普通卡

        private static string mVenuesName = "场馆";
        private static string mRoomsName = "客房";
        private static string mMaterialName = "库房物资";
        //private static string mConsignName = "代销";
        private static string mLeaseName = "租赁物资";
        private static string mMemberCardName = "会员卡";
        private static string mGeneralCardName = "普通卡";

        #endregion

        public TWMerchTypeKind()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 场馆
        /// <summary>
        /// 场馆
        /// </summary>
        public static int Venues
        {
            set
            {
                mVenues = value;
            }
            get
            {
                return mVenues;
            }
        }


        #endregion

        #region 客房
        /// <summary>
        /// 客房
        /// </summary>
        public static int Rooms
        {
            set
            {
                mRooms = value;
            }
            get
            {
                return mRooms;
            }
        }


        #endregion

        #region 物品
        /// <summary>
        /// 清吧
        /// </summary>
        public static int Material
        {
            set
            {
                mMaterial = value;
            }
            get
            {
                return mMaterial;
            }
        }


        #endregion

        //		#region 代销
        //		/// <summary>
        //		/// 代销
        //		/// </summary>
        //		public static int Consign
        //		{
        //			set
        //			{
        //				mConsign=value;
        //			}
        //			get
        //			{
        //				return mConsign;
        //			}
        //		}
        //
        //
        //		#endregion

        #region 租赁物资
        /// <summary>
        /// 租赁
        /// </summary>
        public static int Lease
        {
            set
            {
                mLease = value;
            }
            get
            {
                return mLease;
            }
        }


        #endregion

        #region 会员卡
        /// <summary>
        /// 会员卡
        /// </summary>
        public static int MemberCard
        {
            set
            {
                mMemberCard = value;
            }
            get
            {
                return mMemberCard;
            }
        }


        #endregion

        #region 普通卡
        /// <summary>
        /// 普通卡
        /// </summary>
        public static int GeneralCard
        {
            set
            {
                mGeneralCard = value;
            }
            get
            {
                return mGeneralCard;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 场馆
        /// <summary>
        /// 场馆
        /// </summary>
        public static string VenuesName
        {
            set
            {
                mVenuesName = value;
            }
            get
            {
                return mVenuesName;
            }
        }


        #endregion

        #region 客房
        /// <summary>
        /// 客房
        /// </summary>
        public static string RoomsName
        {
            set
            {
                mRoomsName = value;
            }
            get
            {
                return mRoomsName;
            }
        }


        #endregion

        #region 库房物资
        /// <summary>
        /// 清吧
        /// </summary>
        public static string MaterialName
        {
            set
            {
                mMaterialName = value;
            }
            get
            {
                return mMaterialName;
            }
        }


        #endregion

        #region 租赁物资
        /// <summary>
        /// 租赁
        /// </summary>
        public static string LeaseName
        {
            set
            {
                mLeaseName = value;
            }
            get
            {
                return mLeaseName;
            }
        }


        #endregion

        #region 会员卡
        /// <summary>
        /// 会员卡
        /// </summary>
        public static string MemberCardName
        {
            set
            {
                mMemberCardName = value;
            }
            get
            {
                return mMemberCardName;
            }
        }


        #endregion

        #region 普通卡
        /// <summary>
        /// 普通卡
        /// </summary>
        public static string GeneralCardName
        {
            set
            {
                mGeneralCardName = value;
            }
            get
            {
                return mGeneralCardName;
            }
        }


        #endregion

        #endregion


        #endregion

    }
    #endregion

    #region 商品状态(0可用、1占用、2停用、3预订)
    /// <summary>
    /// 商品状态(0可用、1占用、2停用、3预订)
    /// </summary>
    public sealed class TWMerchState
    {

        #region 私有变量
        private static int mMayUse = 0;		//**可用
        private static int mUsed = 1;		//**占用
        private static int mStopped = 2;	//**停用
        private static int mReserve = 3;	//**预订


        private static string mMayUseName = "可用";
        private static string mUsedName = "占用";
        private static string mStoppedName = "停用";
        private static string mReserveName = "预订";


        #endregion

        public TWMerchState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 可用
        /// <summary>
        /// 可用
        /// </summary>
        public static int MayUse
        {
            set
            {
                mMayUse = value;
            }
            get
            {
                return mMayUse;
            }
        }


        #endregion

        #region 占用
        /// <summary>
        /// 占用
        /// </summary>
        public static int Used
        {
            set
            {
                mUsed = value;
            }
            get
            {
                return mUsed;
            }
        }


        #endregion

        #region 停用
        /// <summary>
        /// 停用
        /// </summary>
        public static int Stopped
        {
            set
            {
                mStopped = value;
            }
            get
            {
                return mStopped;
            }
        }


        #endregion

        #region 预订
        /// <summary>
        /// 预订
        /// </summary>
        public static int Reserve
        {
            set
            {
                mReserve = value;
            }
            get
            {
                return mReserve;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 可用
        /// <summary>
        /// 可用
        /// </summary>
        public static string MayUseName
        {
            set
            {
                mMayUseName = value;
            }
            get
            {
                return mMayUseName;
            }
        }


        #endregion

        #region 占用
        /// <summary>
        /// 占用
        /// </summary>
        public static string UsedName
        {
            set
            {
                mUsedName = value;
            }
            get
            {
                return mUsedName;
            }
        }


        #endregion

        #region 停用
        /// <summary>
        /// 停用
        /// </summary>
        public static string StoppedName
        {
            set
            {
                mStoppedName = value;
            }
            get
            {
                return mStoppedName;
            }
        }


        #endregion

        #region 预订
        /// <summary>
        /// 预订
        /// </summary>
        public static string ReserveName
        {
            set
            {
                mReserveName = value;
            }
            get
            {
                return mReserveName;
            }
        }


        #endregion


        #endregion


        #endregion

    }
    #endregion

    #region 版本小类(basic基础版、standard标准版、completely完全版、group集团版)
    /// <summary>
    /// 版本小类(basic基础版、standard标准版、completely完全版、group集团版)
    /// </summary>
    public sealed class TWSysVersion
    {
        #region 私有变量
        private static string mBasic = "basic";//基础版
        private static string mStandard = "standard";//标准版
        private static string mCompletely = "completely";//完全版
        private static string mGroup = "group";//集团版

        private static string mBasicName = "基础版";//基础版
        private static string mStandardName = "标准版";//标准版
        private static string mCompletelyName = "完全版";//完全版
        private static string mGroupName = "集团版";//集团版

        #endregion

        public TWSysVersion()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 基础版
        /// <summary>
        /// 基础版
        /// </summary>
        public static string Basic
        {
            set
            {
                mBasic = value;
            }
            get
            {
                return mBasic;
            }
        }


        #endregion

        #region 标准版
        /// <summary>
        /// 标准版
        /// </summary>
        public static string Standard
        {
            set
            {
                mStandard = value;
            }
            get
            {
                return mStandard;
            }
        }


        #endregion

        #region 完全版
        /// <summary>
        /// 完全版
        /// </summary>
        public static string Completely
        {
            set
            {
                mCompletely = value;
            }
            get
            {
                return mCompletely;
            }
        }


        #endregion

        #region 集团版
        /// <summary>
        /// 集团版
        /// </summary>
        public static string Group
        {
            set
            {
                mGroup = value;
            }
            get
            {
                return mGroup;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 基础版
        /// <summary>
        /// 基础版
        /// </summary>
        public static string BasicName
        {
            set
            {
                mBasicName = value;
            }
            get
            {
                return mBasicName;
            }
        }


        #endregion

        #region 标准版
        /// <summary>
        /// 标准版
        /// </summary>
        public static string StandardName
        {
            set
            {
                mStandardName = value;
            }
            get
            {
                return mStandardName;
            }
        }


        #endregion

        #region 完全版
        /// <summary>
        /// 完全版
        /// </summary>
        public static string CompletelyName
        {
            set
            {
                mCompletelyName = value;
            }
            get
            {
                return mCompletelyName;
            }
        }


        #endregion

        #region 集团版
        /// <summary>
        /// 集团版
        /// </summary>
        public static string GroupName
        {
            set
            {
                mGroupName = value;
            }
            get
            {
                return mGroupName;
            }
        }

        #endregion

        #endregion

        #endregion

    }
    #endregion


    #region 保险类型(养老保险、医疗保险、失业保险、工伤保险、生育保险、综合保险、公积金)
    /// <summary>
    /// 保险类型(养老保险、医疗保险、失业保险、工伤保险、生育保险、综合保险)
    /// </summary>
    public sealed class TInsuranceType
    {
        #region 私有变量

        private static string mAged = "养老保险";
        private static string mMedical = "医疗保险";
        private static string mIdleness = "失业保险";
        private static string mCompo = "工伤保险";
        private static string mBearing = "生育保险";
        private static string mColligate = "综合保险";
        private static string mAccumulation = "公积金";

        #endregion

        public TInsuranceType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region 名称

        #region 养老保险
        /// <summary>
        /// 养老保险
        /// </summary>
        public static string Aged
        {
            set
            {
                mAged = value;
            }
            get
            {
                return mAged;
            }
        }


        #endregion

        #region 医疗保险
        /// <summary>
        /// 医疗保险
        /// </summary>
        public static string Medical
        {
            set
            {
                mMedical = value;
            }
            get
            {
                return mMedical;
            }
        }


        #endregion

        #region 失业保险
        /// <summary>
        /// 失业保险
        /// </summary>
        public static string Idleness
        {
            set
            {
                mIdleness = value;
            }
            get
            {
                return mIdleness;
            }
        }


        #endregion

        #region 工伤保险
        /// <summary>
        /// 工伤保险
        /// </summary>
        public static string Compo
        {
            set
            {
                mCompo = value;
            }
            get
            {
                return mCompo;
            }
        }


        #endregion

        #region 生育保险
        /// <summary>
        /// 生育保险
        /// </summary>
        public static string Bearing
        {
            set
            {
                mBearing = value;
            }
            get
            {
                return mBearing;
            }
        }


        #endregion

        #region 综合保险
        /// <summary>
        /// 综合保险
        /// </summary>
        public static string Colligate
        {
            set
            {
                mColligate = value;
            }
            get
            {
                return mColligate;
            }
        }


        #endregion

        #region 公积金
        /// <summary>
        /// 综合保险
        /// </summary>
        public static string Accumulation
        {
            set
            {
                mAccumulation = value;
            }
            get
            {
                return mAccumulation;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

    #region 人员档案状态(0未录用、1已录用未入职、2已入职未转正、3已转正、4已离职)
    /// <summary>
    /// 人员档案状态(0储备人员、1试用人员、2正式人员、3离职人员、4实习人员)
    /// </summary>
    public sealed class TArchiveState
    {
        #region 私有变量
        private static int mNew = 0;				//**未录用
        private static int mExercitation = 1;		//**已录用未入职
        private static int mTryOut = 2;				//**已入职未转正
        private static int mFormal = 3;				//**已转正
        private static int mDimission = 4;			//**已离职


        private static string mNewName = "未录用";
        private static string mExercitationName = "已录用未入职";
        private static string mTryOutName = "已入职未转正";
        private static string mFormalName = "已转正";
        private static string mDimissionName = "已离职";

        #endregion

        public TArchiveState()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 未录用
        /// <summary>
        /// 未录用
        /// </summary>
        public static int New
        {
            set
            {
                mNew = value;
            }
            get
            {
                return mNew;
            }
        }


        #endregion

        #region 已录用未入职
        /// <summary>
        /// 实习人员
        /// </summary>
        public static int Exercitation
        {
            set
            {
                mExercitation = value;
            }
            get
            {
                return mExercitation;
            }
        }

        #endregion

        #region 已入职未转正
        /// <summary>
        /// 试用人员
        /// </summary>
        public static int TryOut
        {
            set
            {
                mTryOut = value;
            }
            get
            {
                return mTryOut;
            }
        }


        #endregion

        #region 已转正
        /// <summary>
        /// 录用人员
        /// </summary>
        public static int Formal
        {
            set
            {
                mFormal = value;
            }
            get
            {
                return mFormal;
            }
        }


        #endregion

        #region 已离职
        /// <summary>
        /// 离职人员
        /// </summary>
        public static int Dimission
        {
            set
            {
                mDimission = value;
            }
            get
            {
                return mDimission;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 未录用
        /// <summary>
        /// 未录用
        /// </summary>
        public static string NewName
        {
            set
            {
                mNewName = value;
            }
            get
            {
                return mNewName;
            }
        }


        #endregion

        #region 已录用未入职
        /// <summary>
        /// 已录用未入职
        /// </summary>
        public static string ExercitationName
        {
            set
            {
                mExercitationName = value;
            }
            get
            {
                return mExercitationName;
            }
        }
        #endregion

        #region 已入职未转正
        /// <summary>
        /// 已入职未转正
        /// </summary>
        public static string TryOutName
        {
            set
            {
                mTryOutName = value;
            }
            get
            {
                return mTryOutName;
            }
        }


        #endregion

        #region 已转正
        /// <summary>
        /// 已转正
        /// </summary>
        public static string FormalName
        {
            set
            {
                mFormalName = value;
            }
            get
            {
                return mFormalName;
            }
        }


        #endregion

        #region 已离职
        /// <summary>
        /// 已离职
        /// </summary>
        public static string DimissionName
        {
            set
            {
                mDimissionName = value;
            }
            get
            {
                return mDimissionName;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

    #region 工资类型定时工作(1定时工作制、0非定时工作制)
    /// <summary>
    /// 工资类型定时工作(1定时工作制、0非定时工作制)
    /// </summary>
    public sealed class TPayTimeType
    {
        #region 私有变量
        private static int mOnTime = 1;		//**定时工作制
        private static int mNoTime = 0;		//**非定时工作制		

        private static string mOnTimeName = "定时工作制";
        private static string mNoTimeName = "非定时工作制";

        #endregion

        public TPayTimeType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 定时工作制
        /// <summary>
        /// 定时工作制
        /// </summary>
        public static int OnTime
        {
            set
            {
                mOnTime = value;
            }
            get
            {
                return mOnTime;
            }
        }


        #endregion

        #region 非定时工作制
        /// <summary>
        /// 非定时工作制
        /// </summary>
        public static int NoTime
        {
            set
            {
                mNoTime = value;
            }
            get
            {
                return mNoTime;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 定时工作制
        /// <summary>
        /// 定时工作制
        /// </summary>
        public static string OnTimeName
        {
            set
            {
                mOnTimeName = value;
            }
            get
            {
                return mOnTimeName;
            }
        }


        #endregion

        #region 非定时工作制
        /// <summary>
        /// 非定时工作制
        /// </summary>
        public static string NoTimeName
        {
            set
            {
                mNoTimeName = value;
            }
            get
            {
                return mNoTimeName;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

    #region 工资类型计件工资(1计件工资制、0非计件工资制)
    /// <summary>
    /// 工资类型计件工资(1计件工资制、0非计件工资制)
    /// </summary>
    public sealed class TPayByJobType
    {
        #region 私有变量
        private static int mByJob = 1;		//**计件工资制
        private static int mNoByJob = 0;		//**非计件工资制		

        private static string mByJobName = "计件工资制";
        private static string mNoByJobName = "非计件工资制";

        #endregion

        public TPayByJobType()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 公共属性

        #region ID

        #region 计件工资制
        /// <summary>
        /// 计件工资制
        /// </summary>
        public static int ByJob
        {
            set
            {
                mByJob = value;
            }
            get
            {
                return mByJob;
            }
        }


        #endregion

        #region 非计件工资制
        /// <summary>
        /// 非计件工资制
        /// </summary>
        public static int NoByJob
        {
            set
            {
                mNoByJob = value;
            }
            get
            {
                return mNoByJob;
            }
        }


        #endregion

        #endregion

        #region 名称

        #region 计件工资制
        /// <summary>
        /// 计件工资制
        /// </summary>
        public static string ByJobName
        {
            set
            {
                mByJobName = value;
            }
            get
            {
                return mByJobName;
            }
        }


        #endregion

        #region 非计件工资制
        /// <summary>
        /// 非计件工资制
        /// </summary>
        public static string NoByJobName
        {
            set
            {
                mNoByJobName = value;
            }
            get
            {
                return mNoByJobName;
            }
        }


        #endregion

        #endregion

        #endregion

    }
    #endregion

}
