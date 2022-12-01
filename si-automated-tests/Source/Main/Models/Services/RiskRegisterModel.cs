using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Services
{
    public class RiskRegisterModel
    {
        public string Risk { get; set; }

        public string Level { get; set; }

        public string Type { get; set; }

        public bool ProximityType { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
