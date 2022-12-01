using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Adhoc
{
    public class TaskLinesModel
    {
        public string Order { get; set; }

        public string Type { get; set; }

        public string AssetType { get; set; }

        public string ScheduledAssetQty { get; set; }

        public string Product { get; set; }

        public string ScheduledProductQuantity { get; set; }

        public string Unit { get; set; }

        public string State { get; set; }
    }
}
