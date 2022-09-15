using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class ResourceStateModel
    {
        public int resourcestateID { get; set; }
        public string resourcestate { get; set; }
        public int resourceclassID { get; set; }
    }
}
