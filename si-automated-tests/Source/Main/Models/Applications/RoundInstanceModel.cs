using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Applications
{
    public class RoundInstanceModel
    {
        public string Description { get; set; }

        public string Service { get; set; }

        public override bool Equals(object obj)
        {
            return this.Description == (obj as RoundInstanceModel).Description;
        }
    }
}
