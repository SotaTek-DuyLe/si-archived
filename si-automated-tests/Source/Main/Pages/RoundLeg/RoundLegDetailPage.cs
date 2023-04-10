using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.RoundLeg
{
    public class RoundLegDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Round Leg']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By risksTab = By.CssSelector("a[aria-controls='risks-tab']");
        private readonly By attributeTab = By.CssSelector("a[aria-controls='attributes-tab']");

        [AllureStep]
        public RoundLegDetailPage IsRoundLegDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            WaitUtil.WaitForElementVisible(risksTab);
            WaitUtil.WaitForElementVisible(attributeTab);
            return this;
        }

        [AllureStep]
        public RoundLegDetailPage ClickOnRisksTab()
        {
            ClickOnElement(risksTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public RoundLegDetailPage ClickOnDetailsTab()
        {
            ClickOnElement(detailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public RoundLegDetailPage ClickOnAttributesTab()
        {
            ClickOnElement(attributeTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
    }
}
