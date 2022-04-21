using System;
namespace si_automated_tests.Source.Main.Models
{
    public class InspectionModel
    {
        public string ID { get; set; }
        public string point { get; set; }
        public string inspectionType { get; set; }
        public string createdDate { get; set; }
        public string createdByUser { get; set; }
        public string inspectionScheme { get; set; }
        public string assignedUser { get; set; }
        public string allocatedUnit { get; set; }
        public string status { get; set; }
        public string clientReference { get; set; }
        public string contract { get; set; }
        public string validFrom { get; set; }
        public string validTo { get; set; }
        public string completionDate { get; set; }
        public string lastUpdated { get; set; }
        public string cancelledDate { get; set; }
        public string gpsDescription {get; set; }
        public string source { get; set; }
        public string service { get; set; }

        public InspectionModel()
        {
        }

        public InspectionModel(string id, string inspectionType, string createdDate, string createdByUser, string assignedUser, string allocatedUnit, string status, string validFrom, string validTo, string completionDate, string cancelledDate)
        {
            ID = id;
            this.inspectionType = inspectionType;
            this.createdDate = createdDate;
            this.createdByUser = createdByUser;
            this.assignedUser = assignedUser;
            this.allocatedUnit = allocatedUnit;
            this.status = status;
            this.validFrom = validFrom;
            this.validTo = validTo;
            this.completionDate = completionDate;
            this.cancelledDate = cancelledDate;
        }

        public InspectionModel(string id, string point, string inspectionType, string createdDate, string createdByUser, string inspectionSchema, string assignedUser, string allocatedUnit, string status, string clientRef, string contract, string validFrom, string validTo, string completionDate, string lastUpdated, string cancelledDate, string gps, string source, string service)
        {
            ID = id;
            this.point = point;
            this.inspectionType = inspectionType;
            this.createdDate = createdDate;
            this.createdByUser = createdByUser;
            this.inspectionScheme = inspectionSchema;
            this.assignedUser = assignedUser;
            this.allocatedUnit = allocatedUnit;
            this.status = status;
            this.clientReference = clientRef;
            this.validFrom = validFrom;
            this.validTo = validTo;
            this.completionDate = completionDate;
            this.cancelledDate = cancelledDate;
            this.lastUpdated = lastUpdated;
            this.gpsDescription = gps;
            this.source = source;
            this.service = service;
            this.contract = contract;
        }

    }
}
