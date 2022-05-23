using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.DetailReceipt
{
    public class DetailReceiptModel
    {
        public string Id { get; set; }

        public string Party { get; set; }

        public string PaymentMethod { get; set; }

        public string PaymentReference { get; set; }

        public string Notes { get; set; }

        public string Value { get; set; }
    }
}
