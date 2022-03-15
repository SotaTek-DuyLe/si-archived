using System;
using System.Collections.Generic;
using System.Text;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class CommonActiveServicesTaskPage : BasePage
    {
        private string tribeYarnsWithDate = "//div[text()='Tribe Yarns']/following-sibling::div/div[text()='{0}']";

        public CommonActiveServicesTaskPage OpenTribleYarnsWithDate(string date)
        {
            WaitUtil.WaitForElementVisible(tribeYarnsWithDate, date);
            DoubleClickOnElement(tribeYarnsWithDate, date);
            return this;
        }
    }
}
