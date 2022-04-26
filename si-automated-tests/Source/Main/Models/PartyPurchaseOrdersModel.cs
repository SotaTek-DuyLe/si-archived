using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models
{
    public class PartyPurchaseOrdersModel
    {
        public string number { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }

        public PartyPurchaseOrdersModel(string num, string from, string to)
        {
            this.number = num;
            this.fromDate = from;
            this.toDate = to;
        }
    }
}
