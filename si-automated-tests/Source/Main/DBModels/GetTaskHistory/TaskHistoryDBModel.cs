using System;
namespace si_automated_tests.Source.Main.DBModels.GetTaskHistory
{
    public class TaskHistoryDBModel
    {
        public string action { get; set; }
        public string createdbyuser { get; set; }
        public DateTime createddate { get; set; }
        public string changes { get; set; }
        public string type { get; set; }

        public TaskHistoryDBModel()
        {
        }
    }
}
