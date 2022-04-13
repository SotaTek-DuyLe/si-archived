using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService
{
    public class PriceTab : AddServicePage
    {
        private readonly By closeBtns = By.XPath("//tr[contains(@data-bind,'placeholderText') and not(@style='display: none;')]/descendant::button[@title='Retire/Remove']");
        private readonly By Page4PricesText = By.XPath("//span[text()='4']/following-sibling::p[text()='Prices']");

        //fix locator for tc 016 017 
        private string removePriceBtn = "(//div[contains(@data-bind,'priceEditor')]//button[@title='Retire/Remove'])[3]";
        private string allPrices17 = "(//tr[@class='heading']/following-sibling::tr[1])[1]//button[@title='Retire/Remove']";
        
        private string redundantPrice = "(//div[@id='step-4']//input[@placeholder='Price £' and contains(@class, 'has-error')])[1]/parent::td/following-sibling::td//button[@title='Retire/Remove']";
        private string redundantPriceAll = "//div[@id='step-4']//input[@placeholder='Price £' and contains(@class, 'has-error')]";
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

        public PriceTab RemoveAllRedundantPrice17()
        {
            int i = 3;
            while (i > 0)
            {
                if (!IsControlUnDisplayed(allPrices17))
                {
                    ClickOnElement(allPrices17);
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

        public int GetRedundantPricesNum()
        {
            List<IWebElement> all = GetAllElements(redundantPriceAll);
            return all.Count;
        }
        public PriceTab RemoveAllRedundantPrices(int num)
        {
            int i = num;
            while (i > 0)
            {
                if (!IsControlUnDisplayed(redundantPrice))
                {
                    ClickOnElement(redundantPrice);
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
