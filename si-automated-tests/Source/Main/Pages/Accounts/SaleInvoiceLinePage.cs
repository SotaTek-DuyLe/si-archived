using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SaleInvoiceLinePage : BasePage
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
        private readonly By markInvoiceLineForCreditBtn = By.XPath("//button[text()='Mark Invoice Line For Credit']");
        private readonly By title = By.XPath("//h4[text()='Sales Invoice Line']");
        private readonly By unmarkInvoiceLineForCreditBtn = By.XPath("//button[text()='Unmark Invoice Line From Credit']");
        private readonly By id = By.CssSelector("h4[title='Id']");

        [AllureStep]
        public SaleInvoiceLinePage IsOnSaleInvoiceLinePage()
        {
            SwitchToLastWindow();
            WaitUtil.WaitForElementVisible(lineType);
            WaitUtil.WaitForElementVisible(site);
            WaitUtil.WaitForElementVisible(product);
            Assert.IsTrue(IsControlDisplayed(title));
            return this;
        }
        [AllureStep]
        public SaleInvoiceLinePage InputInfo(string _lineType, string _site, string _product, string _priceElement, string _quantity, string _price)
        {
            SelectTextFromDropDown(lineType, _lineType);
            SelectTextFromDropDown(site, _site);
            SelectTextFromDropDown(product, _product);
            SelectTextFromDropDown(priceElement, _priceElement);
            SendKeys(quantity, _quantity);
            SendKeys(price, _price);
            SleepTimeInMiliseconds(1000);
            return this;
        }
        [AllureStep]
        public SaleInvoiceLinePage VerifyDisplayOfMarkInvoiceLineForCreditBtn()
        {
            Assert.IsTrue(IsControlEnabled(markInvoiceLineForCreditBtn));
            return this;
        }
        [AllureStep]
        public SaleInvoiceLinePage ClickOnMarkInvoiceLineForCreditBtn()
        {
            ClickOnElement(markInvoiceLineForCreditBtn);
            return this;
        }
        [AllureStep]
        public SaleInvoiceLinePage VerifyDisplayUnmarkInvoiceLineFromCreditBtn()
        {
            Assert.IsTrue(IsControlEnabled(unmarkInvoiceLineForCreditBtn));
            return this;
        }
    }
}
