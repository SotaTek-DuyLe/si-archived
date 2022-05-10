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
    }
}
