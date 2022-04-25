using System;

namespace si_automated_tests.Source.Main.DBModels
{
    public class InspectionDBModel
    {
        public int inspectionID { get; set; }
        public int inspectioncreateduserID { get; set; }
        public int userID { get; set; }
        public int echoID { get; set; }
        public int echotypeID { get; set; }
        public int inspectionstateID { get; set; }
        public string inspectiontypedesc { get; set; }
        public string inspectionobjectdesc { get; set; }
        public int inspectiontypeID { get; set; }
        public string note { get; set; }
        public DateTime inspectionvaliddate { get; set; }
        public DateTime inspectionexpirydate { get; set; }
        public int inspectioninstance { get; set; }
        public int contractunitID { get; set; }

        public InspectionDBModel()
        {
        }
    }
}
