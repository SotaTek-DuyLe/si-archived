using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Resources.Tabs
{
    public class ResourceShiftExceptionTab : BasePage
    {
        private readonly By stateSelect = By.Id("resource-state");
        private readonly By startDate = By.Id("start-date");
        private readonly By endDate = By.Id("end-date");
        private readonly By createBtn = By.XPath("//button[text()='Create Exception']");

        [AllureStep]
        public ResourceShiftExceptionTab IsOnShiftExceptionTab()
        {
            WaitUtil.WaitForElementVisible(stateSelect);
            WaitUtil.WaitForElementVisible(startDate);
            WaitUtil.WaitForElementVisible(endDate);
            WaitUtil.WaitForElementVisible(createBtn);
            return this;
        }
        [AllureStep]
        public ResourceShiftExceptionTab SelectState(string state)
        {
            SelectTextFromDropDown(stateSelect, state);
            return this;
        }
        [AllureStep]
        public ResourceShiftExceptionTab SetEndDate(string _endDate)
        {
            SendKeys(endDate, _endDate);
            return this;
        }
        [AllureStep]
        public ResourceShiftExceptionTab ClickCreateException()
        {
            ClickOnElement(createBtn);
            return this;
        }
    }
}
