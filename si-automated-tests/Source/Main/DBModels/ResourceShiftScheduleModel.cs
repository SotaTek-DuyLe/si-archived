using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class ResourceShiftScheduleModel
    {
        public int resourceshiftscheduleID { get; set; }
        public int resourceID { get; set; }
        public int scheduleID { get; set; }
    }
}
