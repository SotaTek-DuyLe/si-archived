using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceTaskLineTab : BasePage
    {
        private readonly string startDate = "//div[@id='tasklines-tab']//td[contains(@data-bind,'startDate')]";
        private readonly string endDate = "//div[@id='tasklines-tab']//td[contains(@data-bind,'endDate')]";

        public ServiceTaskLineTab verifyTaskLineStartDate(string date)
        {
            string startdate = GetElementText(startDate);
            Assert.AreEqual(startdate, date);
            return this;
        }
        public ServiceTaskLineTab verifyTaskLineEndDate(string date)
        {
            string startdate = GetElementText(endDate);
            Assert.AreEqual(endDate, date);
            return this;
        }
    }
}
