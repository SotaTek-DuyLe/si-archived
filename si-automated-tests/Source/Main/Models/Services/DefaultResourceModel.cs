using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Services
{
    public class DefaultResourceModel
    {
        public string Type { get; set; }
        public string Quantity { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public DetailDefaultResourceModel Detail { get; set; }
    }
}
