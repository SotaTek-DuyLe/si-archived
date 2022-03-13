using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models
{
    public class TaskLinesModel
    {
        private string order;
        private string type;
        private string assetType;
        private string scheduledAssetQty;
        private string product;
        private string scheduledProductQuantity;
        private string unit;
        private string state;

        public TaskLinesModel(string order, string type, string assetType, string scheduleAssetQty, string product, string scheduleproductQuantiry, string unit, string state)
        {
            this.Order = order;
            this.Type = type;
            this.AssetType = assetType;
            this.ScheduledAssetQty = scheduleAssetQty;
            this.Product = product;
            this.ScheduledProductQuantity = scheduleproductQuantiry;
            this.Unit = unit;
            this.State = state;
        }
        public string Order { get => order; set => order = value; }
        public string Type { get => type; set => type = value; }
        public string AssetType { get => assetType; set => assetType = value; }
        public string ScheduledAssetQty { get => scheduledAssetQty; set => scheduledAssetQty = value; }
        public string Product { get => product; set => product = value; }
        public string ScheduledProductQuantity { get => scheduledProductQuantity; set => scheduledProductQuantity = value; }
        public string Unit { get => unit; set => unit = value; }
        public string State { get => state; set => state = value; }
    }
}
