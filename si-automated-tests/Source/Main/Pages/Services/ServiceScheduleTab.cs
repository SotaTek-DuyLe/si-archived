using NUnit.Allure.Attributes;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceScheduleTab : BasePage
    {
        private readonly string startDate = "//div[@id='schedules-tab']//td[contains(@data-bind,'startDate')]";
        private readonly string endDate = "//div[@id='schedules-tab']//td[contains(@data-bind,'endDate')]";

        [AllureStep]
        public ServiceScheduleTab verifyScheduleStartDate(string date)
        {
            string startdate = GetElementText(startDate);
            Assert.AreEqual(startdate, date);
            return this;
        }
        [AllureStep]
        public ServiceScheduleTab verifyScheduleEndDate(string date)
        {
            string enddate = GetElementText(endDate);
            Assert.AreEqual(enddate, date);
            return this;
        }
    }
}
