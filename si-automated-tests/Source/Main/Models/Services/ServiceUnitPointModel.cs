using System;
namespace si_automated_tests.Source.Main.Models.Services
{
    public class ServiceUnitPointModel
    {
        public string serviceUnitPointID {get; set;}
        public string pointID { get; set; }
        public string desc { get; set; }
        public string type { get; set; }
        public string qualifier { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

        public ServiceUnitPointModel()
        {
        }

        public ServiceUnitPointModel(string id, string pointId, string desc, string type, string qualifier, string startDate, string endDate)
        {
            this.serviceUnitPointID = id;
            this.pointID = pointID;
            this.desc = desc;
            this.type = type;
            this.qualifier = qualifier;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
