using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Suspension
{
    public class ServiceTaskModel
    {
        /// <summary>
        /// ddMMyyyy
        /// </summary>
        public int Date { get; set; }

        public DateTime DateTime
        {
            get => DateTime.ParseExact(Date.ToString(), "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
        }

        public string Content { get; set; }

        public string ImagePath { get; set; }
    }
}
