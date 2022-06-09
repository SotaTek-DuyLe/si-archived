using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskDBModel
    {
        public int taskID { get; set; }
        public int tasktypeID { get; set; }
        public string task { get; set; }
        public int serviceunitID { get; set; }
        public int servicetaskID { get; set; }
        public int contractID { get; set; }

        public TaskDBModel()
        {
        }
    }
}
