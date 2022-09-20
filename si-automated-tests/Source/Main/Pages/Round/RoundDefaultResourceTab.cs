using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Round
{
    public class RoundDefaultResourceTab : BasePage
    {
        private readonly By table = By.Id("default-resources");
        private readonly By headers = By.XPath("//div[@id='default-resources']/table/thead//th");
        private readonly By endDates = By.XPath("//tr[@data-bind='with: $data.getFields()']//input[@id='endDate.id']/following-sibling::span");
        private readonly By subEndDates = By.XPath("//tr[@class='child-container-row accordian-body collapse in']//input[@id='endDate.id']/following-sibling::span");
        private readonly By activeMonthYear = By.XPath("//div[contains(@class,'bootstrap-datetimepicker-widget dropdown-menu picker-open') and contains(@style,'display: block;')]//div[@class='datepicker-days']//th[@class='picker-switch']");
        private readonly By activeDay = By.XPath("//div[contains(@class,'bootstrap-datetimepicker-widget dropdown-menu picker-open') and contains(@style,'display: block;')]//div[@class='datepicker-days']//td[@class='day active']");
        private readonly By expandBtn = By.XPath("//div[@id='toggle-actions']");

        [AllureStep]
        public RoundDefaultResourceTab IsOnDefaultResourceTab()
        {
            WaitUtil.WaitForElementVisible(table);
            return this;
        }
        [AllureStep]
        public RoundDefaultResourceTab ClickOnEndDate(int whichRow)
        {
            IList<IWebElement> _endDates = WaitUtil.WaitForAllElementsVisible(endDates);
            ClickOnElement(_endDates[whichRow - 1]);
            return this;
        }
        [AllureStep]
        public RoundDefaultResourceTab ClickOnLastSubEndDate()
        {
            SleepTimeInMiliseconds(500);
            IList<IWebElement> _subEndDates = WaitUtil.WaitForAllElementsVisible(subEndDates);
            //ClickOnElement(_subEndDates[whichOneInOrder - 1]);
            ClickOnElement(_subEndDates[_subEndDates.Count - 1]);
            return this;
        }
        [AllureStep]
        public RoundDefaultResourceTab ClickOnSecondLastSubEndDate()
        {
            SleepTimeInMiliseconds(500);
            IList<IWebElement> _subEndDates = WaitUtil.WaitForAllElementsVisible(subEndDates);
            //ClickOnElement(_subEndDates[whichOneInOrder - 1]);
            ClickOnElement(_subEndDates[_subEndDates.Count - 1]);
            return this;
        }
        [AllureStep]
        public RoundDefaultResourceTab VerifyEndDateIsDefault()
        {
            Assert.AreEqual("January 2050", GetElementText(activeMonthYear));
            Assert.AreEqual("1", GetElementText(activeDay));
            return this;
        }
        [AllureStep]
        public RoundDefaultResourceTab VerifyEndDateIs(string monthAndYear, string dayOfMonth)
        {
            if (dayOfMonth.StartsWith("0"))
            {
                dayOfMonth = dayOfMonth.Substring(1);
            }
            Assert.AreEqual(monthAndYear, GetElementText(activeMonthYear));
            Assert.AreEqual(dayOfMonth, GetElementText(activeDay));
            return this;
        }
        [AllureStep]
        public RoundDefaultResourceTab ExpandOption(int whichRow)
        {
            IList<IWebElement> expandBtns = WaitUtil.WaitForAllElementsVisible(expandBtn);
            ClickOnElement(expandBtns[whichRow-1]);
            return this;
        }
    }
}
