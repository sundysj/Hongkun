using System;
namespace MobileSoft.Model.HSPR
{
	/// <summary>
	/// 实体类Tb_HSPR_Room 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Tb_HSPR_Room
	{
		public Tb_HSPR_Room()
		{}
		#region Model
		private long _roomid;
		private int? _commid;
		private string _roomsign;
		private string _roomname;
		private int? _regionsnum;
		private int? _buildsnum;
		private int? _unitsnum;
		private int? _floorsnum;
		private int? _roomsnum;
		private string _roommodel;
		private string _roomtype;
		private string _propertyrights;
		private string _roomtowards;
		private decimal? _buildarea;
		private decimal? _interiorarea;
		private decimal? _commonarea;
		private string _rightssign;
		private string _propertyuses;
		private int? _roomstate;
		private long _chargetypeid;
		private int? _usesstate;
		private int? _isdelete;
		private string _floorheight;
		private string _buildstructure;
		private decimal? _poolratio;
		private string _bearparameters;
		private string _renovation;
		private string _configuration;
		private string _advertising;
		private int? _issplitunite;
		private decimal? _gardenarea;
		private decimal? _propertyarea;
		private int? _areatype;
		private decimal? _yardarea;
		private string _deluser;
		private DateTime? _deldate;
		private int? _residetype;
		private Guid _roomsynchcode;
		private int? _synchflag;
		private long _bedtypeid;
		private int? _usetype;
		private int? _isfrozen;
		/// <summary>
		/// 
		/// </summary>
		public long RoomID
		{
			set{ _roomid=value;}
			get{return _roomid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CommID
		{
			set{ _commid=value;}
			get{return _commid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoomSign
		{
			set{ _roomsign=value;}
			get{return _roomsign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoomName
		{
			set{ _roomname=value;}
			get{return _roomname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RegionSNum
		{
			set{ _regionsnum=value;}
			get{return _regionsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BuildSNum
		{
			set{ _buildsnum=value;}
			get{return _buildsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UnitSNum
		{
			set{ _unitsnum=value;}
			get{return _unitsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FloorSNum
		{
			set{ _floorsnum=value;}
			get{return _floorsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomSNum
		{
			set{ _roomsnum=value;}
			get{return _roomsnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoomModel
		{
			set{ _roommodel=value;}
			get{return _roommodel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoomType
		{
			set{ _roomtype=value;}
			get{return _roomtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropertyRights
		{
			set{ _propertyrights=value;}
			get{return _propertyrights;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoomTowards
		{
			set{ _roomtowards=value;}
			get{return _roomtowards;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BuildArea
		{
			set{ _buildarea=value;}
			get{return _buildarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? InteriorArea
		{
			set{ _interiorarea=value;}
			get{return _interiorarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CommonArea
		{
			set{ _commonarea=value;}
			get{return _commonarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RightsSign
		{
			set{ _rightssign=value;}
			get{return _rightssign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PropertyUses
		{
			set{ _propertyuses=value;}
			get{return _propertyuses;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoomState
		{
			set{ _roomstate=value;}
			get{return _roomstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long ChargeTypeID
		{
			set{ _chargetypeid=value;}
			get{return _chargetypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UsesState
		{
			set{ _usesstate=value;}
			get{return _usesstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FloorHeight
		{
			set{ _floorheight=value;}
			get{return _floorheight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuildStructure
		{
			set{ _buildstructure=value;}
			get{return _buildstructure;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PoolRatio
		{
			set{ _poolratio=value;}
			get{return _poolratio;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BearParameters
		{
			set{ _bearparameters=value;}
			get{return _bearparameters;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Renovation
		{
			set{ _renovation=value;}
			get{return _renovation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Configuration
		{
			set{ _configuration=value;}
			get{return _configuration;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Advertising
		{
			set{ _advertising=value;}
			get{return _advertising;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsSplitUnite
		{
			set{ _issplitunite=value;}
			get{return _issplitunite;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? GardenArea
		{
			set{ _gardenarea=value;}
			get{return _gardenarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PropertyArea
		{
			set{ _propertyarea=value;}
			get{return _propertyarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AreaType
		{
			set{ _areatype=value;}
			get{return _areatype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? YardArea
		{
			set{ _yardarea=value;}
			get{return _yardarea;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DelUser
		{
			set{ _deluser=value;}
			get{return _deluser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? DelDate
		{
			set{ _deldate=value;}
			get{return _deldate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ResideType
		{
			set{ _residetype=value;}
			get{return _residetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid RoomSynchCode
		{
			set{ _roomsynchcode=value;}
			get{return _roomsynchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SynchFlag
		{
			set{ _synchflag=value;}
			get{return _synchflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long BedTypeID
		{
			set{ _bedtypeid=value;}
			get{return _bedtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UseType
		{
			set{ _usetype=value;}
			get{return _usetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsFrozen
		{
			set{ _isfrozen=value;}
			get{return _isfrozen;}
		}
		#endregion Model

	}
}

