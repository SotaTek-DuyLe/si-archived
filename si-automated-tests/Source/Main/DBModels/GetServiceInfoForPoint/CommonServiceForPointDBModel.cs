using System;
namespace si_automated_tests.Source.Main.DBModels.GetServiceInfoForPoint
{
    public class CommonServiceForPointDBModel
    {
        public int serviceID { get; set; }
        public int serviceeventtypeID { get; set; }
        public int eventtypeID { get; set; }
        public string eventtype { get; set; }
        public string prefix { get; set; }
    }
}
