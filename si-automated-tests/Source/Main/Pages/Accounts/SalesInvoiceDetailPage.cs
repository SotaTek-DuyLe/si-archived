using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SalesInvoiceDetailPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Sales Invoice']");
        private readonly By saleInvoiceBatchTitle = By.XPath("//h5[contains(text(), 'Sales Invoice Batch #: ')]");
        private readonly By status = By.XPath("//h5[@title='Invoice Status']");
        private readonly By id = By.XPath("//h4[@title='Id']");
        private readonly By priceLineTab = By.CssSelector("a[aria-controls='priceLines-tab']");
        private readonly By linesTab = By.CssSelector("a[aria-controls='salesInvoiceLines-tab']");

        //PRICE LINES TAB
        private readonly By filterPriceLineInputById = By.XPath("//div[@id='priceLines-tab']//div[contains(@class, 'l0 r0')]/descendant::input");
        private readonly By applyPriceLineBtn = By.XPath("//div[@id='priceLines-tab']//button[@type='button' and @title='Apply Filters']");
        private readonly By firstPriceLineRecordRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");

        //LINES TAB
        private readonly By filterLinesInputById = By.XPath("//div[@id='salesInvoiceLines-tab']//div[contains(@class, 'l1 r1')]/descendant::input");
        private readonly By applyLinesBtn = By.XPath("//div[@id='salesInvoiceLines-tab']//button[@type='button' and @title='Apply Filters']");
        private readonly By firstLinesRecordRow = By.XPath("//div[@id='salesInvoiceLines-tab']//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");

        public SalesInvoiceDetailPage IsSaleInvoiceDetailPage(string statusValue, string idValue)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(saleInvoiceBatchTitle);
            Assert.AreEqual(statusValue, GetElementText(status));
            Assert.AreEqual(idValue, GetElementText(id));
            return this;
        }

        public SalesInvoiceDetailPage ClickOnPriceLineTab()
        {
            ClickOnElement(priceLineTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public SalesInvoiceDetailPage FilterByPriceLineId(string id)
        {
            SendKeys(filterPriceLineInputById, id);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(applyPriceLineBtn);
            ClickOnElement(applyPriceLineBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public PriceLineDetailPage ClickOnFirstPriceLineRecord()
        {
            DoubleClickOnElement(firstPriceLineRecordRow);
            return PageFactoryManager.Get<PriceLineDetailPage>();
        }

        //LINES TAB
        public SalesInvoiceDetailPage ClickOnLinesTab()
        {
            ClickOnElement(linesTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public SalesInvoiceDetailPage FilterByLinesId(string id)
        {
            SendKeys(filterLinesInputById, id);
            SendKeys(filterLinesInputById, Keys.Enter);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public SaleInvoiceLinePage ClickOnFirstLinesRecord()
        {
            DoubleClickOnElement(firstLinesRecordRow);
            return PageFactoryManager.Get<SaleInvoiceLinePage>();
        }
    }
}
