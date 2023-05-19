using System;
namespace si_automated_tests.Source.Main.DBModels.GetServiceInfoForPoint
{
    public class ServiceTaskForPointDBModel
    {
        public int servicetaskscheduleID { get; set; }
        public int pointID { get; set; }
        public int serviceID { get; set; }
        public string service { get; set; }
        public int serviceunitID { get; set; }
        public string serviceunit { get; set; }
        public int serviceunitpointID { get; set; }
        public int servicetaskID { get; set; }
        public string assets { get; set; }
        public int roundscheduleID { get; set; }
        public string last { get; set; }
        public string next { get; set; }
        public string round { get; set; }
        public string roundgroup { get; set; }
        public string patterndesc { get; set; }
        public DateTime lastdate { get; set; }
        public DateTime nextdate { get; set; }
    }
}
