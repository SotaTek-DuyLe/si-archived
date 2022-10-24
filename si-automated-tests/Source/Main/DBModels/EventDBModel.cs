using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class EventDBModel
    {
        public string eventobjectdesc { get; set; }
        public string echodisplayname { get; set; }
        public string eventstatedesc { get; set; }
        public string basestatedesc { get; set; }
        public DateTime lastupdated { get; set; }
        public DateTime eventdate { get; set; }
        public DateTime eventcreateddate { get; set; }
        public DateTime eventduedate { get; set; }
        public int eventpointID { get; set; }
        public int eventcreatedbyuserID { get; set; }
        public int serviceID { get; set; }
        public int echoID { get; set; }
        public int lastupdateduserID { get; set; }

        public EventDBModel()
        {
        }
    }
}
