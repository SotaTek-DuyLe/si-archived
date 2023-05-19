using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System.Collections.Generic;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SalesInvoiceBatchesPage : BasePage
    {
        private readonly By invoiceRows = By.XPath("//div[@class='slick-viewport']//div[@class='grid-canvas']//div[contains(@class,'ui-widget-content')]");
        private readonly By createSaleInvoiceBtn = By.XPath("//button[text()='Create']");
        private readonly By generateSaleInvoiceBtn = By.XPath("//button[text()='Generate']");
        private readonly By postSaleInvoiceBtn = By.XPath("//button[text()='Post']");
        private readonly By filterInputById = By.XPath("//div[contains(@class, 'l1 r1')]/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstRecordRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By firstCheckboxAtRow = By.XPath("//div[@class='grid-canvas']//input");
        private readonly By firstStatusAtRow = By.XPath("//div[@class='grid-canvas']//div[contains(@class, 'l9 r9')]");
        private readonly By firstRegenerateBatchBtn = By.XPath("//div[@class='grid-canvas']//button[text()='Regenerate Batch']");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");
        //DYNAMIC
        private readonly string firstRecordById = "//div[@class='grid-canvas']/div[1]//div[contains(@class, 'r1')]/div[text()='{0}']";

        [AllureStep]
        public SalesInvoiceBatchesPage ClickSalesInvoiceBatches(int invoiceID)
        {
            List<IWebElement> rows = GetAllElements(invoiceRows);
            foreach (var row in rows)
            {
                IWebElement idCell = row.FindElement(By.XPath("./div[contains(@class,'l1')]"));
                int id = GetElementText(idCell).AsInteger();
                if (invoiceID == id)
                {
                    ClickOnElement(row);
                    break;
                }
            }
            return this;
        }
        [AllureStep]
        public SalesInvoiceBatchesPage VerifySalesInvoiceBatchesIsPosted(int invoiceID)
        {
            List<IWebElement> rows = GetAllElements(invoiceRows);
            foreach (var row in rows)
            {
                IWebElement idCell = row.FindElement(By.XPath("./div[contains(@class,'l1')]"));
                int id = GetElementText(idCell).AsInteger();
                if (invoiceID == id)
                {
                    IWebElement statusCell = row.FindElement(By.XPath("./div[contains(@class,'l9')]"));
                    Assert.IsTrue(GetElementText(statusCell) == "POSTED");
                    break;
                }
            }
            return this;
        }
        [AllureStep]
        public SalesInvoiceBatchesPage ClickPost()
        {
            ClickOnElement(postSaleInvoiceBtn);
            return this;
        }
        [AllureStep]
        public SalesInvoiceBatchesPage IsSalesInvoiceBatchesPage()
        {
            WaitUtil.WaitForElementVisible(createSaleInvoiceBtn);
            WaitUtil.WaitForElementVisible(generateSaleInvoiceBtn);
            Assert.IsTrue(IsControlDisplayed(postSaleInvoiceBtn));
            return this;
        }
        [AllureStep]
        public SalesInvoiceBatchesPage FilterBySaleInvoiceBatchId(string id)
        {
            SendKeys(filterInputById, id);
            SendKeys(filterInputById, Keys.Enter);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(firstRecordById, id);
            return this;
        }
        [AllureStep]
        public SalesInvoiceBatchesDetailPage ClickOnFirstRecord()
        {
            DoubleClickOnElement(firstRecordRow);
            return PageFactoryManager.Get<SalesInvoiceBatchesDetailPage>();
        }
        [AllureStep]
        public SalesInvoiceBatchesPage ClickOnFirstCheckboxAtRow()
        {
            ClickOnElement(firstCheckboxAtRow);
            return this;
        }
        [AllureStep]
        public SalesInvoiceBatchesDetailPage ClickOnPostBtn()
        {
            ClickOnElement(postSaleInvoiceBtn);
            return PageFactoryManager.Get< SalesInvoiceBatchesDetailPage>();
        }
        [AllureStep]
        public SalesInvoiceBatchesPage VerifyStatusOfFirstRecord(string statusExp)
        {
            Assert.AreEqual(statusExp, GetElementText(firstStatusAtRow));
            return this;
        }

        [AllureStep]
        public SalesInvoiceBatchesPage FilterId(string id)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, id);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToAppear();
            return this;
        }

        [AllureStep]
        public SalesInvoiceBatchesPage ClickOnFirstRegenerateBatchBtn()
        {
            ClickOnElement(firstRegenerateBatchBtn);
            return this;
        }

        [AllureStep]
        public SalesInvoiceBatchesPage VerifyDisplayVerticalScrollBarSiteListPage()
        {
            VerifyDisplayVerticalScrollBar(containerPage);
            return this;
        }

    }
}
