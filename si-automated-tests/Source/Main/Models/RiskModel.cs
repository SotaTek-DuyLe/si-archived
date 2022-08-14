using System;
namespace si_automated_tests.Source.Main.Models
{
    public class RiskModel
    {
        public string id { get; set; }
        public string riskName { get; set; }
        public string riskLevel { get; set; }
        public string riskType { get; set; }
        public string contract { get; set; }
        public string service { get; set; }
        public string target { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string alert { get; set; }

        public RiskModel()
        {
        }

        public RiskModel(string id, string riskName, string riskLevel, string riskType, string contract, string service, string target, string startDate, string endDate)
        {
            this.id = id;
            this.riskName = riskName;
            this.riskLevel = riskLevel;
            this.riskType = riskType;
            this.contract = contract;
            this.service = service;
            this.target = target;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
