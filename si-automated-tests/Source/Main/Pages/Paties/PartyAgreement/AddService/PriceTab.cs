using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.PartyAgreement.AddService
{
    public class PriceTab : AddServicePage
    {
        private readonly By closeBtns = By.XPath("//tr[contains(@data-bind,'placeholderText') and not(@style='display: none;')]/descendant::button[@title='Retire/Remove']");

        public PriceTab ClosePriceRecords()
        {
            int count = 0;
            while (count < 3)
            {
                IList<IWebElement> priceRecords = WaitUtil.WaitForAllElementsVisible(closeBtns);
                ClickOnElement(priceRecords[0]);
                Thread.Sleep(500);
                count++;
            }
            return this;
        }
        private readonly By Page4PricesText = By.XPath("//span[text()='4']/following-sibling::p[text()='Prices']");

        //fix locator for arrgrement 27 
        private string removePriceBtn = "(//div[contains(@data-bind,'priceEditor')]//button[@title='Retire/Remove'])[3]";
        public PriceTab IsOnPriceTab()
        {
            WaitUtil.WaitForAllElementsVisible(Page4PricesText);
            Assert.IsTrue(IsControlDisplayed(Page4PricesText));
            return this;
        }
        public PriceTab RemoveAllRedundantPrice()
        {
            int i = 3;
            while (i > 0)
            {
                if (!IsControlUnDisplayed(removePriceBtn))
                {
                    ClickOnElement(removePriceBtn);
                    Thread.Sleep(1000);
                    i--;
                }
                else
                {
                    break;
                }
            }

            return this;
        }

    }
}
