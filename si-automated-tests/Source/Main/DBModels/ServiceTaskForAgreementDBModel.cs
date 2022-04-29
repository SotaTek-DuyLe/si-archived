using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class ServiceTaskForAgreementDBModel
    {
        public int servicetaskID { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int agreementID { get; set; }
    }
}
