using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class ServiceGroupDBModel
    {
        public int servicegroupID { get; set; }
        public string servicegroup { get; set; }
        public int contractID { get; set; }

        public ServiceGroupDBModel()
        {
        }
    }
}
