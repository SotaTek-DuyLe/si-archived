using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Contract
{
    public class ContractDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Contract']");
        private readonly By detailsTab = By.XPath("//span[text()='Contract']");
        private readonly By pidRetentionDayInput = By.CssSelector("input[id='pidRetentionDays.id']");

        [AllureStep]
        public ContractDetailPage IsContractDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailsTab);
            return this;
        }

        [AllureStep]
        public ContractDetailPage InputPIDRententionDays(string value)
        {
            SendKeys(pidRetentionDayInput, value);
            return this;
        }

        [AllureStep]
        public ContractDetailPage VerifyValuePIDRententionDays(string value)
        {
            Assert.AreEqual(value, GetAttributeValue(pidRetentionDayInput, "value"));
            return this;
        }

        [AllureStep]
        public string GetValueInPIDRententionDays()
        {
            return GetAttributeValue(pidRetentionDayInput, "value");
        }
    }
}
