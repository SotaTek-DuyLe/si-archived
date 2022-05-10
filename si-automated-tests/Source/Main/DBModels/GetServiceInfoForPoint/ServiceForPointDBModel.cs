using System;
namespace si_automated_tests.Source.Main.DBModels.GetServiceInfoForPoint
{
    public class ServiceForPointDBModel
    {
        public int serviceID { get; set; }
        public int roundID { get; set; }
        public string next { get; set; }
        public string service { get; set; }
        public int serviceunitID { get; set; }
        public string serviceunit { get; set; }
        public int servicetaskscheduleID { get; set; }
        public string statusdesc { get; set; }
        public string tasktype { get; set; }
        public string servicedesc { get; set; }
        public string roundgroup { get; set; }
    }
}
