using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService
{
    public class PriceTab : AddServicePage
    {
        private readonly By closeBtns = By.XPath("//tr[contains(@data-bind,'placeholderText') and not(@style='display: none;')]/descendant::button[@title='Retire/Remove' and not(@disabled)]");
        private readonly By Page4PricesText = By.XPath("//span[text()='4']/following-sibling::p[text()='Prices']");

        //fix locator for tc 016 017 
        
        private string redundantPriceAll = "//div[@id='step-4']//input[@placeholder='Price £' and contains(@class, 'has-error')]";
        public readonly By PriceTable = By.XPath("(//div[@id='step-4']//table)[1]/tbody");

        [AllureStep]
        public PriceTab ClosePriceRecords()
        {
            SleepTimeInSeconds(1);
            if (IsControlDisplayedNotThrowEx(closeBtns))
            {
                var _closeBtns = GetAllElements(closeBtns);
                foreach (var item in _closeBtns)
                {
                    ClickOnElement(item);
                    SleepTimeInMiliseconds(300);
                }
            }
            return this;
        }
        [AllureStep]
        public PriceTab IsOnPriceTab()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForAllElementsVisible(Page4PricesText);
            Assert.IsTrue(IsControlDisplayed(Page4PricesText));
            return this;
        }

        [AllureStep]
        public int GetRedundantPricesNum()
        {
            List<IWebElement> all = GetAllElements(redundantPriceAll);
            return all.Count;
        }
        [AllureStep]
        public PriceTab ClickOnRemoveButton(List<string> commercialCustomers)
        {
            foreach (var commercialCustomer in commercialCustomers)
            {
                IWebElement table = GetElement(PriceTable);
                List<IWebElement> rows = table.FindElements(By.XPath("./tr")).ToList();
                for (int i = 0; i < rows.Count; i++)
                {
                    IWebElement title = rows[i].FindElements(By.XPath("./td//div[@class='price-list']//span[@data-bind='text: name']")).FirstOrDefault();
                    if (title == null || title.Text != commercialCustomer) continue;
                    if(i + 2 < rows.Count)
                    {
                        IWebElement detailRow = rows[i + 2];
                        IWebElement buttonCell = detailRow.FindElement(By.XPath("./td[@class='buttons']"));
                        IWebElement removeBtn = buttonCell.FindElement(By.XPath("./button[@title='Retire/Remove']"));
                        WaitUtil.WaitForElementClickable(removeBtn).Click();
                        SleepTimeInMiliseconds(300);
                        break;
                    }
                }
            }
            return this;
        }

        [AllureStep]
        public PriceTab InputPrices(List<(string title, string value)> commercialCustomers)
        {
            foreach (var commercialCustomer in commercialCustomers)
            {
                IWebElement table = GetElement(PriceTable);
                List<IWebElement> rows = table.FindElements(By.XPath("./tr")).ToList();
                for (int i = 0; i < rows.Count; i++)
                {
                    IWebElement title = rows[i].FindElements(By.XPath("./td//div[@class='price-list']//span[@data-bind='text: name']")).FirstOrDefault();
                    if (title == null || title.Text != commercialCustomer.title) continue;
                    if (i + 2 < rows.Count)
                    {
                        IWebElement detailRow = rows[i + 2];
                        IWebElement priceInputCell = detailRow.FindElement(By.XPath("./td//input[@placeholder='Price £']"));
                        if(priceInputCell != null)
                        {
                            SendKeys(priceInputCell, commercialCustomer.value);
                        }
                        break;
                    }
                }
            }
            return this;
        }

        [AllureStep]
        public PriceTab ClickPrice(string commercialCustomer)
        {
            IWebElement table = GetElement(PriceTable);
            List<IWebElement> rows = table.FindElements(By.XPath("./tr")).ToList();
            for (int i = 0; i < rows.Count; i++)
            {
                IWebElement title = rows[i].FindElements(By.XPath("./td//div[@class='price-list']//span[@data-bind='text: name']")).FirstOrDefault();
                if (title == null || title.Text != commercialCustomer) continue;
                if (i + 2 < rows.Count)
                {
                    IWebElement detailRow = rows[i + 2];
                    IWebElement priceInputCell = detailRow.FindElement(By.XPath("./td//input[@placeholder='Price £']"));
                    ClickOnElement(priceInputCell);
                    break;
                }
            }
            return this;
        }
    }
}
