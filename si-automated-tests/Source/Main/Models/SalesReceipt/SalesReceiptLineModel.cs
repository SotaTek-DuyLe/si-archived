using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.SalesReceipt
{
    public class SalesReceiptLineModel
    {
        public string Id { get; set; }

        public string TargetType { get; set; }

        public string TargetId { get; set; }

        public string Site { get; set; }

        public string Value { get; set; }
    }
}
