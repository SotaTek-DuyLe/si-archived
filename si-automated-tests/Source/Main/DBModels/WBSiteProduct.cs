
using System;

namespace si_automated_tests.Source.Main.DBModels
{
    public class WBSiteProduct
    {
        public int siteproductID { get; set; }
        public int siteID { get; set; }
        public int productID { get; set; }
        public int tickettypeID { get; set; }
        public int defaultlocationID { get; set; }
        public bool islocationmandatory { get; set; }
        public bool isrestrictlocation { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string siteproduct { get; set; }
        public string clientreference { get; set; }

        public WBSiteProduct() { }
    }
}
