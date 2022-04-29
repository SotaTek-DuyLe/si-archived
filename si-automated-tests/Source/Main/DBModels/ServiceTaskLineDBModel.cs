using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.DBModels
{
    public class ServiceTaskLineDBModel
    {
        public int scheduledassetquantity { get; set; }
        public string assettype { get; set; }
        public int scheduledproductquantity { get; set; }
        public string unit { get; set; }
        public string product { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
    }
}
