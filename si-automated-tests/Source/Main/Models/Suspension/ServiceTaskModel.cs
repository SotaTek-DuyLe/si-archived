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
        public DateTime Date { get; set; }
        public DateTime DateTime
        {
            get => Date;
        }

        public string Content { get; set; }

        public string ImagePath { get; set; }
    }
}
