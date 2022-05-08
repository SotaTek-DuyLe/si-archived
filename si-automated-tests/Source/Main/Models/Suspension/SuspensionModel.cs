using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models
{
    public class SuspensionModel
    {
        public string Sites { get; set; }
        public string Services { get; set; }
        public string FromDate { get; set; }
        public string LastDate { get; set; }
        public string SuspensedDay { get; set; }
    }
}
