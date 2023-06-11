using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskAndTaskTypeByTaskIdDBModel
    {
        public string task { get; set; }
        public int taskID { get; set; }
        public string tasktype { get; set; }
        public string tasktypedescription { get; set; }

        public TaskAndTaskTypeByTaskIdDBModel()
        {
        }
    }
}
