using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskReAllocationModel
    {
        public int taskID { get; set; }
        public int roundinstanceID { get; set; }
        public int roundID { get; set; }
        public int reason_resolutioncodeID { get; set; }
    }
}
