using System;
namespace si_automated_tests.Source.Main.Models
{
    public class RegularModel
    {
        private string index;
        private string taskType;
        private string[] invoiceSchedule;
        private string[] invoiceAddress;
        private string frequency;
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
        private string destinationSite;
        private string siteProduct;
        private string startDateCover;
        private string endDateCover;

        public RegularModel(string indexR, string taskTypeR, string[] invoiceScheduleR, string[] invoiceAddressR, string frequencyR, string startEndDateR, string taskLineTypeR, string assetTypeR, string assetQtyR, string minAssetR, string maxAssetR, string productR, string amountOfProductR, string minProdQtyR, string maxProdQtyR, string unitR, string destinationSiteR, string siteProductR, string startDateCoverR, string endDateCoverR)
        {
            this.index = indexR;
            this.taskType = taskTypeR;
            this.invoiceSchedule = invoiceScheduleR;
            this.invoiceAddress = invoiceAddressR;
            this.frequency = frequencyR;
            this.startEndDate = startEndDateR;
            this.taskLineType = taskLineTypeR;
            this.assetType = assetTypeR;
            this.assetQuantity = assetQtyR;
            this.minAssetQty = minAssetR;
            this.maxAssetQty = maxAssetR;
            this.product = productR;
            this.amountOfProduct = amountOfProductR;
            this.minProductQty = minProdQtyR;
            this.maxProductQty = maxProdQtyR;
            this.unit = unitR;
            this.destinationSite = destinationSiteR;
            this.siteProduct = siteProductR;
            this.startDateCover = startDateCoverR;
            this.endDateCover = endDateCoverR;
        }

        public string Index { get => index; set => index = value; }
        public string TaskType { get => taskType; set => taskType = value; }
        public string[] InvoiceSchedule { get => invoiceSchedule; set => invoiceSchedule = value; }
        public string[] InvoiceAddress { get => invoiceAddress; set => invoiceAddress = value; }
        public string Frequency { get => frequency; set => frequency = value; }
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
        public string DestinationSite { get => destinationSite; set => destinationSite = value; }
        public string SiteProduct { get => siteProduct; set => siteProduct = value; }
        public string StartDateCover { get => startDateCover; set => startDateCover = value; }
        public string EndDateCover { get => endDateCover; set => endDateCover = value; }
       
    }
}
