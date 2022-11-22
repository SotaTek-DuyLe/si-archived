using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Resources
{
    public class RoundModel
    {
        private string serviceName;
        private string roundName;

        public string ServiceName { get => serviceName; set => serviceName = value; }
        public string RoundName { get => roundName; set => roundName = value; }

        public override string ToString()
        {
            return ServiceName + "|" + RoundName;
        }
    }
}
