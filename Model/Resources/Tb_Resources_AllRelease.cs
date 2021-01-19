using System;
using System.Collections.Generic;
using System.Text;
using MobileSoft.Model.Resources;

namespace MobileSoft.Model.Resources
{
    public class Tb_Resources_AllRelease
    {
        public Tb_Resources_Release MoRelease=new Tb_Resources_Release ();
        public Tb_Resources_ReleaseImages MoReleaseImages=new Tb_Resources_ReleaseImages ();
        public Tb_Resources_ReleaseWare MoReleaseWare=new Tb_Resources_ReleaseWare ();
        public Tb_Resources_Details MoResourcesDetails=new Tb_Resources_Details ();
        public TB_Resources_ReleaseService MoReleaseService = new TB_Resources_ReleaseService();
        public Tb_Resources_ReleaseRooms MoReleaseRooms = new Tb_Resources_ReleaseRooms();
        public Tb_Resources_ReleaseVenues MoReleaseVenues = new Tb_Resources_ReleaseVenues();
        public Tb_Resources_ReleaseTrain MoReleaseTrain = new Tb_Resources_ReleaseTrain();
        public Tb_Resources_ReleaseHotel MoReleaseHotel = new Tb_Resources_ReleaseHotel();
        public Tb_Resources_ReleaseDeduction MoReleaseDeduction = new Tb_Resources_ReleaseDeduction();
        public Tb_Resources_ReleaseMember MoReleaseMember = new Tb_Resources_ReleaseMember();
        public Tb_Resources_ReleaseTraveling MoReleaseTraveling = new Tb_Resources_ReleaseTraveling();
        public Tb_Resources_ReleaseCarrental MoReleaseCarrental = new Tb_Resources_ReleaseCarrental();
    }
}
