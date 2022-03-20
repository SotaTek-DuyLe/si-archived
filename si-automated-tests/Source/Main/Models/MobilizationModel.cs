using System;
namespace si_automated_tests.Source.Main.Models
{
    public class MobilizationModel
    {
        private string index;
        private string taskType;
        private string[] invoiceSchedule;
        private string[] invoiceAddress;
        private string startEndDate;
        private string taskLineType;
        private string assetType;
        private string assetQuantity;
        private string minAssetQty;
        private string maxAssetQty;
        private string product;
        private string amountOfProduct;
        private string minProductQty;
        private string maxProductQty;
        private string unit;
        private string startDateCover;
        private string endDateCover;

        public MobilizationModel(string indexM, string taskTypeM, string[] invoiceScheduleM, string[] invoiceAddressM, string startEndDateM, string taskLineTypeM, string assetTypeM, string assetQtyM, string minAssetM, string maxAssetM, string productM, string amountOfProductM, string minProdQtyM, string maxProdQtyM, string unitM, string startDateM, string endDateM)
        {
            this.Index = indexM;
            this.taskType = taskTypeM;
            this.invoiceSchedule = invoiceScheduleM;
            this.invoiceAddress = invoiceAddressM;
            this.startEndDate = startEndDateM;
            this.taskLineType = taskLineTypeM;
            this.assetType = assetTypeM;
            this.assetQuantity = assetQtyM;
            this.minAssetQty = minAssetM;
            this.maxAssetQty = maxAssetM;
            this.product = productM;
            this.amountOfProduct = amountOfProductM;
            this.minProductQty = minProdQtyM;
            this.maxProductQty = maxProdQtyM;
            this.unit = unitM;
            this.startDateCover = startDateM;
            this.endDateCover = endDateM;
        }
        public MobilizationModel(string taskLineTypeM, string assetTypeM, string assetQtyM, string productM, string amountOfProductM, string unitM, string startDateM)
        {
            this.taskLineType = taskLineTypeM;
            this.assetType = assetTypeM;
            this.assetQuantity = assetQtyM;
            this.product = productM;
            this.amountOfProduct = amountOfProductM;
            this.unit = unitM;
            this.startDateCover = startDateM;
        }

        public string TaskType { get => taskType; set => taskType = value; }
        public string[] InvoiceSchedule { get => invoiceSchedule; set => invoiceSchedule = value; }
        public string[] InvoiceAddress { get => invoiceAddress; set => invoiceAddress = value; }
        public string StartEndDate { get => startEndDate; set => startEndDate = value; }
        public string TaskLineType { get => taskLineType; set => taskLineType = value; }
        public string AssetType { get => assetType; set => assetType = value; }
        public string AssetQuantity { get => assetQuantity; set => assetQuantity = value; }
        public string MinAssetQty { get => minAssetQty; set => minAssetQty = value; }
        public string MaxAssetQty { get => maxAssetQty; set => maxAssetQty = value; }
        public string Product { get => product; set => product = value; }
        public string AmountOfProduct { get => amountOfProduct; set => amountOfProduct = value; }
        public string MinProductQty { get => minProductQty; set => minProductQty = value; }
        public string MaxProductQty { get => maxProductQty; set => maxProductQty = value; }
        public string Unit { get => unit; set => unit = value; }
        public string StartDateCover { get => startDateCover; set => startDateCover = value; }
        public string EndDateCover { get => endDateCover; set => endDateCover = value; }
        public string Index { get => index; set => index = value; }
    }
}
