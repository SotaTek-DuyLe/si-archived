using System;
namespace si_automated_tests.Source.Main.Models
{
    public class TaskLineModel
    {
        private string scheduleAssetQty { get; set; }

        public string order { get; set; }
        public string type { get; set; }
        public string assetType { get; set; }
        public string scheduledAssetQty { get; set; }
        public string actualAssetQuantity { get; set; }
        public string minAssetQty { get; set; }
        public string maxAssetQty { get; set; }
        public string product { get; set; }
        public string scheduledProductQuantity { get; set; }
        public string actualProductQuantity { get; set; }
        public string minProductQty { get; set; }
        public string maxProductQty { get; set; }
        public string unit { get; set; }
        public string serialised { get; set; }
        public string destinationSite { get; set; }
        public string siteProduct { get; set; }
        public string asset { get; set; }
        public string state { get; set; }
        public string resolutionCode { get; set; }

        public TaskLineModel()
        {
        }

        public TaskLineModel(string order, string type, string assetType, string scheduleAssetQty, string actualAssetQuantity, string minAssetQty, string maxAssetQty, string product, string scheduledProductQuantity, string actualProductQuantity, string minProductQty, string maxProductQty, string unit, string destinationSite, string siteProduct, string asset, string state, string resolutionCode)
        {
            this.order = order;
            this.type = type;
            this.assetType = assetType;
            this.scheduleAssetQty = scheduleAssetQty;
            this.actualAssetQuantity = actualAssetQuantity;
            this.minAssetQty = minAssetQty;
            this.maxAssetQty = maxAssetQty;
            this.product = product;
            this.scheduledProductQuantity = scheduledProductQuantity;
            this.actualProductQuantity = actualProductQuantity;
            this.minProductQty = minProductQty;
            this.maxProductQty = maxProductQty;
            this.unit = unit;
            this.destinationSite = destinationSite;
            this.siteProduct = siteProduct;
            this.asset = asset;
            this.state = state;
            this.resolutionCode = resolutionCode;
        }
    }
}
