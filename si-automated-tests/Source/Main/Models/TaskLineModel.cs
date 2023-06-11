using System;
namespace si_automated_tests.Source.Main.Models
{
    public class TaskLineModel
    {
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
        public string clientRef { get; set; }

        public TaskLineModel()
        {
        }

        public TaskLineModel(string order, string assetType, string minAssetQty, string scheduledAssetQty, string minProductQty, string scheduledProductQty, string destinationSite, string maxAssetQty, string actualAssetQty, string maxProductQty, string actualProductQty, string state, string siteProduct, string clientRef, string product, string unit, string resolutionCode)
        {
            this.order = order;
            this.assetType = assetType;
            this.minAssetQty = minAssetQty;
            this.scheduledAssetQty = scheduledAssetQty;
            this.minProductQty = minProductQty;
            this.scheduledProductQuantity = scheduledProductQty;
            this.destinationSite = destinationSite;
            this.maxAssetQty = maxAssetQty;
            this.actualAssetQuantity = actualAssetQty;
            this.maxProductQty = maxProductQty;
            this.actualProductQuantity = actualProductQty;
            this.state = state;
            this.siteProduct = siteProduct;
            this.clientRef = clientRef;
            this.product = product;
            this.unit = unit;
            this.resolutionCode = resolutionCode;
        }

        public TaskLineModel(string order, string type, string assetType, string scheduleAssetQty, string actualAssetQuantity, string product, string scheduledProductQuantity, string actualProductQuantity, string unit, string destinationSite, string siteProduct, string state, string resolutionCode)
        {
            this.order = order;
            this.type = type;
            this.assetType = assetType;
            this.scheduledAssetQty = scheduleAssetQty;
            this.actualAssetQuantity = actualAssetQuantity;
            this.product = product;
            this.scheduledProductQuantity = scheduledProductQuantity;
            this.actualProductQuantity = actualProductQuantity;
            this.unit = unit;
            this.destinationSite = destinationSite;
            this.siteProduct = siteProduct;
            this.state = state;
            this.resolutionCode = resolutionCode;
        }
    }
}
