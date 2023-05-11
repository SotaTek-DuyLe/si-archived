using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.CostRules
{
    public class CostRuleDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Cost Rule']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By costElementDd = By.XPath("//label[contains(string(), 'Cost Element')]/following-sibling::select");

        [AllureStep]
        public CostRuleDetailPage IsCostRuleDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            return this;
        }

        [AllureStep]
        public CostRuleDetailPage VerifyDisplayCostElement()
        {
            Assert.IsTrue(IsControlDisplayed(costElementDd));
            return this;
        }
    }
}
