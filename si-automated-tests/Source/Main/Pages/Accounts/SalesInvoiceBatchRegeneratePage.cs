using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SalesInvoiceBatchRegeneratePage : BasePage
    {
        private readonly By titlePopup = By.XPath("//h4[text()='Regenerate sales invoice batch']");
        private readonly By salesInvoiceDateLabel = By.XPath("//label[text()='Sales Invoice Date']");
        private readonly By firstPeriodTo = By.XPath("(//select[contains(@data-bind, 'options: periodToOptions')])[1]");
        private readonly By firstPeriodFrom = By.XPath("(//select[contains(@data-bind, 'options: periodFromOptions')])[1]");
        private readonly By yesBtn = By.XPath("//button[not(@disabled) and text()='Yes']");
        private readonly By refreshBtn = By.CssSelector("button[title='Refresh']");

        //DYNAMIC
        private readonly string firstPeriodToOption = "//select[contains(@data-bind, 'options: periodToOptions')]/option[text()='{0}']";
        private readonly string firstPeriodFromOption = "//select[contains(@data-bind, 'options: periodFromOptions')]/option[text()='{0}']";

        [AllureStep]
        public SalesInvoiceBatchRegeneratePage IsSaleInvoiceBatchRegeneratePage()
        {
            WaitUtil.WaitForElementVisible(titlePopup);
            WaitUtil.WaitForElementVisible(salesInvoiceDateLabel);
            return this;
        }

        [AllureStep]
        public SalesInvoiceBatchRegeneratePage ClickAndSelectPeriodTo(string periodToValue)
        {
            ClickOnElement(firstPeriodTo);
            ClickOnElement(firstPeriodToOption, periodToValue);
            return this;
        }

        [AllureStep]
        public SalesInvoiceBatchRegeneratePage ClickAndSelectPeriodFrom(string periodFromValue)
        {
            ClickOnElement(firstPeriodFrom);
            ClickOnElement(firstPeriodFromOption, periodFromValue);
            return this;
        }

        [AllureStep]
        public SalesInvoiceBatchRegeneratePage ClickOnYesBtn()
        {
            ClickOnElement(yesBtn);
            return this;
        }

        [AllureStep]
        public SalesInvoiceBatchRegeneratePage ClickOnRefreshBtn()
        {
            ClickOnElement(refreshBtn);
            return this;
        }
    }
}
