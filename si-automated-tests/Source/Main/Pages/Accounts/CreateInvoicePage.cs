using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class CreateInvoicePage: BasePage
    {
        private readonly By partyInput = By.XPath("//input[@type='search']");
        private readonly string partySelectOption = "//li[@class='list-group-item' and text()='{0}']";

        //new tabs
        private readonly By lineTab = By.XPath("//a[@aria-controls='salesInvoiceLines-tab']");
        private readonly By priceLine = By.XPath("//a[@aria-controls='priceLines-tab']");

        public CreateInvoicePage()
        {
        }

        public CreateInvoicePage IsOnCreateInvoicePage()
        {
            SwitchToLastWindow();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(partyInput);
            return this;
        }
        public CreateInvoicePage SearchForParty(string partyName)
        {
            SendKeys(partyInput, partyName);
            SleepTimeInMiliseconds(1000);
            ClickOnElement(partySelectOption, partyName);
            return this;
        }
        public CreateInvoicePage VerifyNewTabsArePresent()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(lineTab);
            WaitUtil.WaitForElementVisible(priceLine);
            return this;
        }
    }
}
