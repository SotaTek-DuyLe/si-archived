using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskLineDBModel
    {
        public int tasklinestateID { get; set; }
        public int resolutioncodeID { get; set; }
        public string tasklinestate { get; set; }

        public TaskLineDBModel()
        {
        }
    }
}
