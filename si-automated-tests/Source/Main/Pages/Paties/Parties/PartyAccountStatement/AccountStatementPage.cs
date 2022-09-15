using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccountStatement
{
    public class AccountStatementPage : BasePage
    {
        private string buttonNamed = "//button[text()='{0}']";
        public AccountStatementPage()
        {
            WaitUtil.WaitForElementVisible(buttonNamed, "Create Credit Note");
            WaitUtil.WaitForElementVisible(buttonNamed, "Take Payment");
            WaitUtil.WaitForElementVisible(buttonNamed, "Create Sales Invoice");
        }
        private AccountStatementPage ClickOnButtonNamed(string value)
        {
            ClickOnElement(buttonNamed, value);
            return this;
        }
        public AccountStatementPage ClickCreateCreditNote()
        {
            ClickOnButtonNamed("Create Credit Note");
            return this;
        }
        public AccountStatementPage ClickTakePayment()
        {
            ClickOnButtonNamed("Take Payment");
            return this;
        }
        public AccountStatementPage ClickCreateSaleInvoice()
        {
            ClickOnButtonNamed("Create Sales Invoice");
            return this;
        }

    }
}
