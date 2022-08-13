using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class OutstandingTaskModel
    {
        public int ID { get; set; }
        public int servicetaskID { get; set; }
        public int servicetaskscheduleID { get; set; }
        public string task { get; set; }
        public string description { get; set; }
    }
}
