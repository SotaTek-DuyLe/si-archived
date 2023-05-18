using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundInstanceEventNewForm : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Round Instance Event']");
        private readonly By detailTab = By.XPath("//a[text()='Details']");
        private readonly By roundEventTypeDd = By.CssSelector("select[id='event-type']");
        private readonly By resourceDd = By.CssSelector("select[id='resource']");

        [AllureStep]
        public RoundInstanceEventNewForm IsRoundInstanceEventNewForm()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventNewForm SelectAnyRoundEventType(string roundEventTypeName)
        {
            SelectTextFromDropDown(roundEventTypeDd, roundEventTypeName);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventNewForm SelectAnyResource(string resourceName)
        {
            SelectTextFromDropDown(resourceDd, resourceName);
            return this;
        }
    }
}
