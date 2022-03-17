using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Resources.Tabs
{
    public class ResourceCalendarTab : BasePage
    {
        private readonly By workPattern = By.XPath("//span[@class='fc-title']");

        public ResourceCalendarTab VerifyWorkPatternIsSet(string _pattern)
        {
            IList<IWebElement> patternList = WaitUtil.WaitForAllElementsVisible(workPattern);
            foreach(var pattern in patternList)
            {
                Assert.AreEqual(_pattern, pattern.Text);
            }
            return this;
        }
        public ResourceCalendarTab VerifyWorkPatternNotSet()
        {
            WaitUtil.WaitForElementInvisible(workPattern);
            return this;
        }
    }
}
