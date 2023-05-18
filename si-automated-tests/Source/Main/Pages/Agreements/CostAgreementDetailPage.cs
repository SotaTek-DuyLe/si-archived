using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Agreements
{
    public class CostAgreementDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Cost Agreement']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By costBookTab = By.CssSelector("a[aria-controls='costBooks-tab']");

        [AllureStep]
        public CostAgreementDetailPage IsCostAgreementDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            return this;
        }

        [AllureStep]
        public CostAgreementDetailPage ClickOnCostBookTab()
        {
            ClickOnElement(costBookTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
    }
}
