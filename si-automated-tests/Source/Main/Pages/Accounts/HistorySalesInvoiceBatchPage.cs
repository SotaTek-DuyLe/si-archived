using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class HistorySalesInvoiceBatchPage : BasePage
    {
        private readonly By idColumn = By.XPath("//th[text()='ID']");
        private readonly By expandToggleFirstRow = By.XPath("//tbody[@data-bind='foreach: visibleHistoryItems']/tr[1]/td[@data-toggle='collapse']");
        private readonly By propertyChangesAtFirstToggle = By.XPath("(//tbody[@data-bind='foreach: changes'])[1]//td[1]");
        private readonly By newTimeChangesAtFirstToggle = By.XPath("(//tbody[@data-bind='foreach: changes'])[1]//td[2]");
        private readonly By updateTypeInput = By.XPath("(//input[@id='update-type'])[1]");
        private readonly By createdDateInput = By.XPath("(//input[@id='created-date'])[1]");

        [AllureStep]
        public HistorySalesInvoiceBatchPage ClickOnExpandToggleAtFirstRow()
        {
            WaitUtil.WaitForElementVisible(idColumn);
            ClickOnElement(expandToggleFirstRow);
            return this;
        }

        [AllureStep]
        public HistorySalesInvoiceBatchPage VerifyValueInHistoryTableAfterUpdated(string propertyValue, string timeUpdatedNew, string updateTypeValue)
        {
            Assert.AreEqual(propertyValue, GetElementText(propertyChangesAtFirstToggle));
            Assert.IsTrue(GetElementText(newTimeChangesAtFirstToggle).Contains(timeUpdatedNew));
            Assert.AreEqual(updateTypeValue, GetAttributeValue(updateTypeInput, "value"));
            Assert.AreEqual("true", GetAttributeValue(updateTypeInput, "readonly"));
            Assert.IsTrue(GetAttributeValue(createdDateInput, "value").Contains(timeUpdatedNew));
            Assert.AreEqual("true", GetAttributeValue(createdDateInput, "readonly"));
            return this;
        }
    }
}
