using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class LinesTab : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By deleteItemBtn = By.XPath("//button[text()='Delete Item']");
        private readonly By numberOfLines = By.XPath("//div[contains(@class,'ui-widget-content slick-row')]");
        
        [AllureStep]
        public LinesTab IsOnLinesTab()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(addNewItemBtn);
            WaitUtil.WaitForElementVisible(deleteItemBtn);
            return this;
        }
        [AllureStep]
        public LinesTab ClickAddNewItem()
        {
            ClickOnElement(addNewItemBtn);
            return this;
        }
        [AllureStep]
        public LinesTab VerifyLineInfo(string _site, string _product, string _description, string _quantity, string _price)
        {
            CommonBrowsePage browsePage = PageFactoryManager.Get<CommonBrowsePage>();
            browsePage.VerifyFirstResultValue("Site", _site);
            browsePage.VerifyFirstResultValue("Product", _product);
            browsePage.VerifyFirstResultValue("Description", _description);
            browsePage.VerifyFirstResultValue("Quantity", _quantity);
            browsePage.VerifyFirstResultValue("Price", "£" + _price);
            browsePage.VerifyFirstResultValue("Posted Status", "NEW");
            return this;
        }
        [AllureStep]
        public LinesTab VerifyLineInfo(string _site, string _product, string _description, string _quantity, string vatRate, string _price)
        {
            var vatRateValue = Double.Parse(vatRate);
            var priceValue = Double.Parse(_price);
            var vatValue = priceValue * vatRateValue / 100;
            var totalValue = priceValue + vatValue;
            CommonBrowsePage browsePage = PageFactoryManager.Get<CommonBrowsePage>();
            browsePage.VerifyFirstResultValue("Site", _site);
            browsePage.VerifyFirstResultValue("Product", _product);
            browsePage.VerifyFirstResultValue("Description", _description);
            browsePage.VerifyFirstResultValue("Quantity", _quantity);
            browsePage.VerifyFirstResultValue("Price", "£" + _price);
            browsePage.VerifyFirstResultValue("VAT", "£" + vatValue.ToString() + ".00");
            browsePage.VerifyFirstResultValue("Total", "£" + totalValue.ToString() + ".00");
            browsePage.VerifyFirstResultValue("Posted Status", "NEW");
            return this;
        }
        [AllureStep]
        public LinesTab VerifyNumberOfLineIsOne()
        {
            Assert.AreEqual(WaitUtil.WaitForAllElementsVisible(numberOfLines).Count, 1);
            return this;
        }
    }
}
