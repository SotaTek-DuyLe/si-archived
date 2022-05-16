
namespace si_automated_tests.Source.Main.DBModels
{
    public class ServiceDBModel
    {
        public int serviceID { get; set; }
        public string service { get; set; }
        public int servicegroupID { get; set; }
        public int servicetypeID { get; set; }
        public string servicetype { get; set; }

        public ServiceDBModel()
        {
        }
    }
}
