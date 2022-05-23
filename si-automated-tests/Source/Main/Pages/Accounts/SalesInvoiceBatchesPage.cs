using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SalesInvoiceBatchesPage : BasePage
    {
        private readonly By invoiceRows = By.XPath("//div[@class='slick-viewport']//div[@class='grid-canvas']//div[contains(@class,'ui-widget-content')]");
        private readonly By postBtn = By.XPath("//button[contains(string(), 'Post')]");

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

        public SalesInvoiceBatchesPage ClickPost()
        {
            ClickOnElement(postBtn);
            return this;
        }
    }
}
