using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class CreditNoteLinePage : BasePage
    {
        private readonly By lineType = By.Id("price-element");
        private readonly By site = By.Id("site");
        private readonly By product = By.Id("product");
        private readonly By priceElement = By.XPath("//*[@id='price-element' and contains(@data-bind,'priceElement')]");
        private readonly By nominalCode = By.XPath("//input[@list='nominal-code']");
        private readonly By nominalCodeValue = By.XPath("//datalist[@id='nominal-code']/option");
        private readonly By description = By.Id("description");
        private readonly By quantity = By.Id("quantity");
        private readonly By price = By.Id("price");
        private readonly By netValue = By.Id("net-value");
        private readonly By vatRate = By.Id("vat-rate");
        private readonly By poNumber = By.Id("po-number");

        public CreditNoteLinePage IsOnCreditNoteLinePage()
        {
            WaitUtil.WaitForElementVisible(lineType);
            WaitUtil.WaitForElementVisible(site);
            WaitUtil.WaitForElementVisible(product);
            WaitUtil.WaitForElementVisible(priceElement);
            WaitUtil.WaitForElementVisible(nominalCode);
            WaitUtil.WaitForElementVisible(description);
            WaitUtil.WaitForElementVisible(quantity);
            WaitUtil.WaitForElementVisible(price);
            WaitUtil.WaitForElementVisible(netValue);
            WaitUtil.WaitForElementVisible(vatRate);
            WaitUtil.WaitForElementVisible(poNumber);
            return this;
        }
        public CreditNoteLinePage InputInfo(string _lineType, string _site, string _product, string _priceElement, string _description, string _quantity, string _price)
        {
            SelectTextFromDropDown(lineType, _lineType);
            SelectTextFromDropDown(site, _site);
            SelectTextFromDropDown(product, _product);
            SelectTextFromDropDown(priceElement, _priceElement);
            SendKeys(description, _description);
            SendKeys(quantity, _quantity);
            SendKeys(price, _price);
            SleepTimeInMiliseconds(1000);
            return this;
        }
    }
}
