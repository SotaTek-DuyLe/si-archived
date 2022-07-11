using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Applications
{
    public class RoundCalendarModel
    {
        public DateTime Day { get; set; }

        public List<RoundInstanceSchedule> Instances { get; set; }

        public IWebElement ButtonMore { get; set; }
    }

    public class RoundInstanceSchedule
    {
        public IWebElement Instance { get; set; }

        public bool IsGrey { get; set; }
        public bool IsGreen { get; set; }
    }
}
