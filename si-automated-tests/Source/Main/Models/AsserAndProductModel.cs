using System;
namespace si_automated_tests.Source.Main.Models
{
    public class AsserAndProductModel
    {
        private string assetType;
        private string quantity1;
        private string product;
        private string ewcCode;
        private string productQuantity;
        private string unit;
        private string tenure;
        private string[] invoiceSchedule;
        private string[] invoiceAddress;
        private string startDate;
        private string endDate;

        public AsserAndProductModel(string assertTypeM, string quantity1M, string productM, string ewcCodeM, string productQuantityM, string unitM, string tenureM, string[] invoiceScheduleM, string[] invoiceAddressM, string startDateM, string endDateM)
        {
            this.assetType = assertTypeM;
            this.quantity1 = quantity1M;
            this.product = productM;
            this.ewcCode = ewcCodeM;
            this.ProductQuantity = productQuantityM;
            this.unit = unitM;
            this.tenure = tenureM;
            this.invoiceSchedule = invoiceScheduleM;
            this.invoiceAddress = invoiceAddressM;
            this.startDate = startDateM;
            this.endDate = endDateM;
        }

        public string Quantity1 { get => quantity1; set => quantity1 = value; }
        public string Product { get => product; set => product = value; }
        public string EwcCode { get => ewcCode; set => ewcCode = value; }
        public string Unit { get => unit; set => unit = value; }
        public string Tenure { get => tenure; set => tenure = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string EndDate { get => endDate; set => endDate = value; }
        public string[] InvoiceSchedule { get => invoiceSchedule; set => invoiceSchedule = value; }
        public string[] InvoiceAddress { get => invoiceAddress; set => invoiceAddress = value; }
        public string AssetType { get => assetType; set => assetType = value; }
        public string ProductQuantity { get => productQuantity; set => productQuantity = value; }
    }
}
