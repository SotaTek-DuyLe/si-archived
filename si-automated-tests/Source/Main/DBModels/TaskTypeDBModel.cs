using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskTypeDBModel
    {
        public int? Allcompletetasklines_taskstateID { get; set; }
        public int? Allcompletetasklines_rescodeID { get; set; }
        public int? Allfailedtasklines_taskstateID { get; set; }
        public int? Allfailedtasklines_rescodeID { get; set; }
        public int? Allclosedtasklines_taskstateID { get; set; }
        public int? Allclosedtasklines_rescodeID { get; set; }

        public TaskTypeDBModel()
        {
        }
    }
}
