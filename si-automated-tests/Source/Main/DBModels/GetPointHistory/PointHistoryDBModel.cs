using System;
namespace si_automated_tests.Source.Main.DBModels.GetPointHistory
{
    public class PointHistoryDBModel
    {
        public int echoID { get; set; }
        public int echotypeID { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string statedesc { get; set; }
        public string typename { get; set; }
        public string service { get; set; }
        public DateTime itemdate { get; set; }
        public DateTime duedate { get; set; }

        public PointHistoryDBModel()
        {
        }
    }
}
