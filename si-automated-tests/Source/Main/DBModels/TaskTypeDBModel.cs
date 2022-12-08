using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskTypeDBModel
    {
        public int allcompletetasklines_taskstateID { get; set; }
        public int allcompletetasklines_rescodeID { get; set; }
        public int allfailedtasklines_taskstateID { get; set; }
        public int allfailedtasklines_rescodeID { get; set; }
        public int allclosedtasklines_taskstateID { get; set; }
        public int allclosedtasklines_rescodeID { get; set; }

        public TaskTypeDBModel()
        {
        }
    }
}
