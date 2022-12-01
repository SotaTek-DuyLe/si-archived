using System;
using NUnit.Allure.Attributes;
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

        //Uninvoiced popup
        private readonly By confirmBtn = By.XPath("//button[text()='Confirm']");

        //PRICE LINES TAB
        private readonly By filterPriceLineInputById = By.XPath("//div[@id='priceLines-tab']//div[contains(@class, 'l0 r0')]/descendant::input");
        private readonly By applyPriceLineBtn = By.XPath("//div[@id='priceLines-tab']//button[@type='button' and @title='Apply Filters']");
        private readonly By firstPriceLineRecordRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");

        //LINES TAB
        private readonly By filterLinesInputById = By.XPath("//div[@id='salesInvoiceLines-tab']//div[contains(@class, 'l1 r1')]/descendant::input");
        private readonly By applyLinesBtn = By.XPath("//div[@id='salesInvoiceLines-tab']//button[@type='button' and @title='Apply Filters']");
        private readonly By firstLinesRecordRow = By.XPath("//div[@id='salesInvoiceLines-tab']//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        
        [AllureStep]
        public SalesInvoiceDetailPage IsSaleInvoiceDetailPage(string statusValue, string idValue)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(saleInvoiceBatchTitle);
            Assert.AreEqual(statusValue, GetElementText(status));
            Assert.AreEqual(idValue, GetElementText(id));
            return this;
        }
        [AllureStep]
        public SalesInvoiceDetailPage ClickOnPriceLineTab()
        {
            ClickOnElement(priceLineTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public SalesInvoiceDetailPage FilterByPriceLineId(string id)
        {
            SendKeys(filterPriceLineInputById, id);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(applyPriceLineBtn);
            ClickOnElement(applyPriceLineBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PriceLineDetailPage ClickOnFirstPriceLineRecord()
        {
            DoubleClickOnElement(firstPriceLineRecordRow);
            return PageFactoryManager.Get<PriceLineDetailPage>();
        }

        //LINES TAB
        [AllureStep]
        public SalesInvoiceDetailPage ClickOnLinesTab()
        {
            ClickOnElement(linesTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]

        public SalesInvoiceDetailPage FilterByLinesId(string id)
        {
            SendKeys(filterLinesInputById, id);
            SendKeys(filterLinesInputById, Keys.Enter);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public SaleInvoiceLinePage ClickOnFirstLinesRecord()
        {
            DoubleClickOnElement(firstLinesRecordRow);
            return PageFactoryManager.Get<SaleInvoiceLinePage>();
        }
        [AllureStep]
        public SalesInvoiceDetailPage SelectFirstUninvoicedItem()
        {
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickFirstItem();
            ClickOnElement(confirmBtn);
            return PageFactoryManager.Get<SalesInvoiceDetailPage>();
        }
    }
}
