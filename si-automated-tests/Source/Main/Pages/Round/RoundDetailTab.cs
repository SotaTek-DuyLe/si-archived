using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Round
{
    public class RoundDetailTab : BasePage
    {
        private readonly By roundGroupInput = By.XPath("//input[@name='round']");

        [AllureStep]
        public RoundDetailTab IsOnDetailTab()
        {
            WaitUtil.WaitForElementVisible(roundGroupInput);
            return this;
        }
    }
}
