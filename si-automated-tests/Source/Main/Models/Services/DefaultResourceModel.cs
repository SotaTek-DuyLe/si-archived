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

        public override bool Equals(object obj)
        {
            return this.Type == (obj as DefaultResourceModel).Type && 
                this.Quantity == (obj as DefaultResourceModel).Quantity &&
                this.StartDate == (obj as DefaultResourceModel).StartDate &&
                this.EndDate == (obj as DefaultResourceModel).EndDate;
        }
    }
}
