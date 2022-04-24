using System;
namespace si_automated_tests.Source.Main.Models
{
    public class PointAddressModel
    {
        public PointAddressModel(string id, string name, string uprn, string postcode, string propertyName, string toProperty, string subBuilding, string propertySuff, string pointSegmentId, string pointAddressType, string startDate, string endDate)
        {
            this.id = id;
            this.name = name;
            this.uprn = uprn;
            this.postcode = postcode;
            this.propertyName = propertyName;
            this.toProperty = toProperty;
            this.subBuilding = subBuilding;
            this.propertySuff = propertySuff;
            this.pointSegmentId = pointSegmentId;
            this.pointAddressType = pointAddressType;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public string id { get; set; }
        public string name { get; set; }
        public string uprn { get; set; }
        public string postcode { get; set; }
        public string propertyName { get; set; }
        public string toProperty { get; set; }
        public string subBuilding { get; set; }
        public string propertySuff { get; set; }
        public string pointSegmentId { get; set; }
        public string pointAddressType { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
