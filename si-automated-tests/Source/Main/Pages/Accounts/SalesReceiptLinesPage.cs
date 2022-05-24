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
    public class SalesReceiptLinesPage : BasePage
    {
        private readonly By objectType = By.XPath("//select[@id='echo-type']");
        private readonly By objectTypeOpt = By.XPath("//select[@id='echo-type']//option");
        private readonly By inputInvoice = By.XPath("//input[@id='echo-id']");
        private readonly By netPrice = By.XPath("//input[@id='net-value']");
        private readonly By vatPrice = By.XPath("//input[@id='vat-charge']");
        private readonly By grossPrice = By.XPath("//input[@id='gross-value']");
        private readonly By valuePrice = By.XPath("//input[@id='value']");
        private readonly By amountOwedPrice = By.XPath("//input[@id='amount-owed']");
        private readonly By saveBtn = By.XPath("//button[@title='Save']");
        private readonly By receiptValue = By.XPath("//h5[@title='Receipt Value']");

        public SalesReceiptLinesPage ClickObjectTypeAndVerifyListType()
        {
            ClickOnElement(objectType);
            Thread.Sleep(100);
            List<string> types = GetAllElements(objectTypeOpt).Select(x => GetElementText(x)).Where(x => x != "Select...").ToList();
            Assert.AreEqual(new List<string>() { "Weighbridge Ticket", "Sales Invoice" }, types);
            return this;
        }

        public SalesReceiptLinesPage SelectObjectType(string option)
        {
            SelectTextFromDropDown(objectType, option);
            return this;
        }

        public SalesReceiptLinesPage InputInvoice(string value)
        {
            SendKeys(inputInvoice, value);
            return this;
        }

        public SalesReceiptLinesPage NetPriceHasValue()
        {

            int i = 5;
            while (i > 0)
            {
                if (GetInputValue(netPrice).Equals(""))
                {
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else { break; }
            }
            Assert.IsNotEmpty(GetInputValue(netPrice));
            return this;
        }

        public SalesReceiptLinesPage VatPriceHasValue()
        {
            int i = 5;
            while (i > 0)
            {
                if (GetInputValue(vatPrice).Equals(""))
                {
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else { break; }
            }
            Assert.IsNotEmpty(GetInputValue(vatPrice));
            return this;
        }

        public SalesReceiptLinesPage GrossPriceHasValue()
        {
            int i = 5;
            while (i > 0)
            {
                if (GetInputValue(grossPrice).Equals(""))
                {
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else { break; }
            }
            Assert.IsNotEmpty(GetInputValue(grossPrice));
            return this;
        }

        public SalesReceiptLinesPage ValuePriceContainValue(string value)
        {
            Assert.IsTrue(GetInputValue(valuePrice) == value);
            return this;
        }

        public SalesReceiptLinesPage InputValuePrice(string value)
        {
            SendKeys(valuePrice, value);
            return this;
        }

        public SalesReceiptLinesPage IsReceiptValueDisplay()
        {
            WaitUtil.WaitForElementVisible(receiptValue);
            Assert.IsTrue(IsControlDisplayed(receiptValue));
            return this;
        }

        public SalesReceiptLinesPage VerifyAmountOwned(string value)
        {
            Assert.IsTrue(GetInputValue(amountOwedPrice) == value);
            return this;
        }

        public SalesReceiptLinesPage ClickOnSaveBtn()
        {
            ClickOnElement(saveBtn);
            return this;
        }
    }
}
