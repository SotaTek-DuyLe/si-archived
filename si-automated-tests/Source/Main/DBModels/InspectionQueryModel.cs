using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class InspectionQueryModel
    {
        public string username { get; set; }
        public string contractunit { get; set; }
        public string note { get; set; }
        public int inspectioninstance { get; set; }
        public DateTime inspectionvaliddate { get; set; }
        public DateTime inspectionexpirydate { get; set; }

        public InspectionQueryModel()
        {
        }
    }
}
