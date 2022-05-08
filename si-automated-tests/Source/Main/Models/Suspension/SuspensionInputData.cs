using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Suspension
{
    public class SuspensionInputData
    {
        public List<string> Sites { get; set; }
        public List<string> Services { get; set; }
        public string FromDate { get; set; }
        public string LastDate { get; set; }
        public string SuspensedDay { get; set; }
    }
}
