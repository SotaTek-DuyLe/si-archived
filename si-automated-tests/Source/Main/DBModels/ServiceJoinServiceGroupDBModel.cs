using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class ServiceJoinServiceGroupDBModel
    {
        public int serviceID { get; set; }
        public string service { get; set; }
        public string servicegroup { get; set; }

        public ServiceJoinServiceGroupDBModel()
        {
        }
    }
}
