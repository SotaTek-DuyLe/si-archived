using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class ServiceUnitPointDBModel
    {
        public int serviceunitpointID { get; set; }
        public int serviceunitID { get; set; }
        public int pointtypeID { get; set; }
        public int pointID { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int pointqualifierID { get; set; }
        public int serviceunitpointtype { get; set; }
    }
}
