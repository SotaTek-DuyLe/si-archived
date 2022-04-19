using System;
namespace si_automated_tests.Source.Main.Models
{
    public class InspectionModel
    {
        public string ID { get; set; }
        public string inspectionType { get; set; }
        public string createdDate { get; set; }
        public string createdByUser { get; set; }
        public string assignedUser { get; set; }
        public string allocatedUser { get; set; }
        public string allocatedUnit { get; set; }
        public string status { get; set; }
        public string validFrom { get; set; }
        public string validTo { get; set; }
        public string completionDate { get; set; }
        public string cancelledDate { get; set; }

        public InspectionModel()
        {
        }

        public InspectionModel(string id, string inspectionType, string createdDate, string createdByUser, string assignedUser, string allocatedUser, string status, string validFrom, string validTo, string completionDate, string cancelledDate)
        {
            ID = id;
            this.inspectionType = inspectionType;
            this.createdDate = createdDate;
            this.createdByUser = createdByUser;
            this.assignedUser = assignedUser;
            this.allocatedUser = allocatedUser;
            this.status = status;
            this.validFrom = validFrom;
            this.validTo = validTo;
            this.completionDate = completionDate;
            this.cancelledDate = cancelledDate;
        }
    }
}
