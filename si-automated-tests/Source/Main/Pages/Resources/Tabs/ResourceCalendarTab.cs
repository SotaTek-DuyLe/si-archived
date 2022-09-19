using NUnit.Allure.Attributes;
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
        //date view option when in Year tab:
        private readonly string dateViewOptionYear = "//div[@id='calendar-year']//button[text()='{0}']";
        private readonly string hiddenYearDateView = "//div[@class='calendar-year-component' and @style='display: none;']";
        //date view option when in other tab:
        private readonly string dateViewOptionOthers = "//div[contains(@data-bind,'echoCalendar')]//button[text()='{0}']";
        private readonly By todayInYearView = By.XPath("//td[contains(@class,'day today')]");

        [AllureStep]
        public ResourceCalendarTab VerifyWorkPatternIsSet(string _pattern)
        {
            IList<IWebElement> patternList = WaitUtil.WaitForAllElementsVisible(workPattern);
            foreach(var pattern in patternList)
            {
                Assert.AreEqual(_pattern, pattern.Text);
            }
            return this;
        }
        [AllureStep]
        public ResourceCalendarTab VerifyWorkPatternNotSet()
        {
            WaitUtil.WaitForElementInvisible(workPattern);
            return this;
        }
        [AllureStep]
        public bool IsOnYearDateView()
        {
            //if hidden year view is found -> not on year view, not found -> on year view
            return IsControlUnDisplayed(hiddenYearDateView);
        }
        [AllureStep]
        public ResourceCalendarTab SwitchDateView(string viewOption)
        {
            //if is year view -> use other view and vice versa
            if (IsOnYearDateView())
            {
                ClickOnElement(dateViewOptionYear, viewOption);
            }
            else
            {
                ClickOnElement(dateViewOptionOthers, viewOption);
            }
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ResourceCalendarTab OpenTodayDateInYearView()
        {
            DoubleClickOnElement(todayInYearView);
            return this;
        }
    }
}
