using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class ServiceUnitAssetsDBModel
    {
        public int partyID { get; set; }
        public int agreementID { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
}
