using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class ServiceUnitModel
    {
        public string serviceunit { get; set; }
        public int serviceID { get; set; }
        public DateTime startdate { get; set; }
        public DateTime endDate { get; set; }
        public int serviceunittypeID { get; set; }
        public int streetID { get; set; }
        public int pointsegmentID { get; set; }
        public string clientreference { get; set; }
        public int islocked { get; set; }
        public string lockedreference { get; set; }
    }
}
