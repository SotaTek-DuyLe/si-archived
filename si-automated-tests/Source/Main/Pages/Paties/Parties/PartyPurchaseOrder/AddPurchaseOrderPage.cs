using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyPurchaseOrder
{
    public class AddPurchaseOrderPage : BasePage
    {
        private readonly By PONumberInput = By.Id("number");
        private readonly By firstDayInput = By.Id("start-date");
        private readonly By lastDayInput = By.Id("end-date");
        private readonly By agreementInput = By.Id("agreement");

        //Dynamic locator 
        private string agreementOptions = "//select[@id='agreement']//option[text()='{0}']";

        public AddPurchaseOrderPage IsOnAddPurchaseOrderPage()
        {
            WaitUtil.WaitForElementVisible(PONumberInput);
            Assert.IsTrue(IsControlDisplayed(PONumberInput));
            Assert.IsTrue(IsControlDisplayed(firstDayInput));
            Assert.IsTrue(IsControlDisplayed(lastDayInput));
            Assert.IsTrue(IsControlDisplayed(agreementInput));
            return this;
        }

        public AddPurchaseOrderPage InputPONumber(string num)
        {
            SendKeys(PONumberInput, num);
            return this;
        }
        public AddPurchaseOrderPage InputFirstDay(string date)
        {
            SendKeys(firstDayInput, date);
            return this;
        }
        public AddPurchaseOrderPage InputLastDay(string date)
        {
            SendKeys(lastDayInput, date);
            return this;
        }
        public AddPurchaseOrderPage SelectAgreement(string agr)
        {
            WaitUtil.WaitForElementVisible(agreementOptions, agr);
            ClickOnElement(agreementInput);
            ClickOnElement(agreementOptions, agr);
            return this;
        }
    }
}
