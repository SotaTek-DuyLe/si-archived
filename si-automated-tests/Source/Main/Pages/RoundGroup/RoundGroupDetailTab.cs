using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Round
{
    public class RoundGroupDetailTab : BasePage
    {
        private readonly By roundGroupInput = By.XPath("//input[@name='roundGroup']");

        public RoundGroupDetailTab IsOnDetailTab()
        {
            WaitUtil.WaitForElementVisible(roundGroupInput);
            return this;
        }
    }
}
