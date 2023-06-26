using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class SaleInvoicePriceLinesDBModel
    {
        public int salesinvoicepricelineID { get; set; }
        public int salesinvoicelineID { get; set; }
        public int salesinvoicebatchID  { get; set; }
        public SaleInvoicePriceLinesDBModel()
        {
        }
    }
}
