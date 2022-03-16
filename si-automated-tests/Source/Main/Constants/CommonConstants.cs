using System;
namespace si_automated_tests.Source.Main.Constants
{
    public class CommonConstants
    {
        public static readonly string[] InvoiceScheduleAssetAndProduct = {"Use Agreement", "Daily in Arrears", "Monthly in Arrears"};
        public static readonly string[] InvoiceAddressAssetAndProduct = { "Use Agreement", "15 HILL STREET, RICHMOND, TW9 1SX" };
        public static readonly string[] InvoiceScheduleMobilization = { "Use Agreement Line", "Daily in Arrears", "Monthly in Arrears" };
        public static readonly string[] InvoiceAddressMobilization = { "Use Agreement Line", "15 HILL STREET, RICHMOND, TW9 1SX" };
        public static readonly string[] TaskLineType = { "Service" };
        public static readonly string[] AssetType = { "1100L" };
        public static readonly string[] ProductName = { "General Recycling" };
        public static readonly string[] Unit = { "Kilograms" };
        public static readonly string StartDateAgreement = "03/03/2022";
        public static readonly string EndDateAgreement = "01/01/2050";
    }
}
