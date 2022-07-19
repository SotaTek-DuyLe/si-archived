using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class ServiceUnitDBModel
    {
        public int serviceunitID { get; set; }
        public string serviceunit { get; set; }
        public int serviceID { get; set; }
        public DateTime startdate { get; set; }
        public DateTime endDate { get; set; }
        public int serviceunittypeID { get; set; }
        public int streetID { get; set; }
        public int pointsegmentID { get; set; }
        public string clientreference { get; set; }
        public int islocked { get; set; }
        public int servicelevelID { get; set; }

        public ServiceUnitDBModel()
        {
        }
    }
}
