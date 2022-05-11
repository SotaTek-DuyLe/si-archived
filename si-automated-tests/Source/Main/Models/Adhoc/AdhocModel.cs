using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Adhoc
{
    public class AdhocModel
    {
        public string Site { get; set; }
        public string Address { get; set; }
        public string Service { get; set; }
        public string TaskType { get; set; }
        public string TaskLines { get; set; }
        public IWebElement CreateAdHocBtn { get; set; } 
    }
}
