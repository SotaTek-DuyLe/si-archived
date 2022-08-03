using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskDBModel
    {
        public int taskId { get; set; }
        public int tasktypeID { get; set; }
        public string task { get; set; }
        public string tasknotes { get; set; }
        public DateTime taskcompleteddate { get; set; }
        public DateTime taskcreateddate { get; set; }
        public DateTime taskduedate { get; set; }
        public DateTime taskenddate { get; set; }

        public TaskDBModel()
        {
        }
    }
}
