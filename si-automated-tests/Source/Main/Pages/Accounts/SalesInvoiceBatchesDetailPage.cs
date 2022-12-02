using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SalesInvoiceBatchesDetailPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Sales Invoice Batch']");
        private readonly By id = By.XPath("//h4[@title='Id']");
        private readonly By invoicesTab = By.CssSelector("a[aria-controls='salesInvoices-tab']");
        private readonly By detailsTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By firstRecordRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By filterInputById = By.XPath("//div[contains(@class, 'l1 r1')]/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstPeriodTo = By.XPath("(//select[contains(@data-bind, 'options: periodToOptions')])[1]");
        private readonly By firstPeriodFrom = By.XPath("(//select[contains(@data-bind, 'options: periodFromOptions')])[1]");
        private readonly By historyBtn = By.CssSelector("button[title='History']");

        //WANRING POPUP
        private readonly By warningTitle = By.XPath("//h4[text()='Warning']");
        private readonly By contentWaring = By.XPath("//div[text()='Are you sure you want to post the selected Sales Invoice Batches?']");
        private readonly By yesBtn = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//button[text()='Yes']");
        private readonly By noBtn = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//button[text()='No']");
        private readonly By closeBtn = By.XPath("//h4[text()='Warning']/preceding-sibling::button");

        //DYNAMIC
        //private readonly string statusSaleInvoice = "//h5[text()='{0}']";

        [AllureStep]
        public SalesInvoiceBatchesDetailPage IsSalesInvoiceBatchesDetailPage(string statusValue, string idValue)
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.AreEqual(GetElementText(id), idValue);
            //Assert.IsTrue(IsControlDisplayed(statusSaleInvoice, statusValue));
            return this;
        }

        [AllureStep]
        public SalesInvoiceBatchesDetailPage ClickOnDetailsTab()
        {
            ClickOnElement(detailsTab);
            return this;
        }

        [AllureStep]
        public string GetFirstPeriodTo()
        {
            return GetFirstSelectedItemInDropdown(firstPeriodTo);
        }

        [AllureStep]
        public string GetFirstPeriodFrom()
        {
            return GetFirstSelectedItemInDropdown(firstPeriodFrom);
        }

        [AllureStep]
        public SalesInvoiceBatchesDetailPage ClickOnInvoiceTab()
        {
            ClickOnElement(invoicesTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]

        public SalesInvoiceBatchesDetailPage FilterByInvoiceId(string id)
        {
            SendKeys(filterInputById, id);
            SendKeys(filterInputById, Keys.Enter);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public SalesInvoiceDetailPage DoubleClickOnAnyInvoiceRow()
        {
            DoubleClickOnElement(firstRecordRow);
            return PageFactoryManager.Get<SalesInvoiceDetailPage>();
        }
        [AllureStep]
        public SalesInvoiceBatchesDetailPage IsWariningSaleInvoiceBatchesPopup()
        {
            WaitUtil.WaitForElementVisible(warningTitle);
            Assert.IsTrue(IsControlDisplayed(warningTitle));
            Assert.IsTrue(IsControlDisplayed(contentWaring));
            Assert.IsTrue(IsControlEnabled(yesBtn));
            Assert.IsTrue(IsControlEnabled(noBtn));
            Assert.IsTrue(IsControlDisplayed(closeBtn));
            return this;
        }
        [AllureStep]
        public SalesInvoiceBatchesPage ClickOnYesOnWarningPopupBtn()
        {
            ClickOnElement(yesBtn);
            VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            return PageFactoryManager.Get<SalesInvoiceBatchesPage>();
        }

        [AllureStep]
        public SalesInvoiceBatchesDetailPage ClickOnHistoryBtn()
        {
            ClickOnElement(historyBtn);
            return this;
        }

    }
}
