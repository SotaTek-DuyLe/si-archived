using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class PurchaseOderListVDBModel
    {
        public int purchaseorderID { get; set; }
        public string number { get; set; }
        public string customername { get; set; }
        public int partyID { get; set; }

        public PurchaseOderListVDBModel()
        {
        }
    }
}
