namespace si_automated_tests.Source.Main.Models
{
    public class PointSegmentModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pointSegmentType { get; set; }
        public string pointSegmentClass { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }

        public PointSegmentModel()
        {
        }

        public PointSegmentModel(string id, string name, string pointSegmentType, string pointSegmentClass, string startDate, string endDate)
        {
            this.id = id;
            this.name = name;
            this.pointSegmentType = pointSegmentType;
            this.pointSegmentClass = pointSegmentClass;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
