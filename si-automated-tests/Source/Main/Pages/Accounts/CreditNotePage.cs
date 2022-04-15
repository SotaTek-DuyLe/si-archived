using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class CreditNotePage :BasePage
    {
        private readonly By partyInput = By.XPath("//div[@id='party-name']//input");
        private readonly string partySelectOption = "//div[@id='party-name']//li[text()='{0}']";
        private readonly By creditDateInput = By.Id("credit-date");
        private readonly By noteInput = By.Id("notes");

        //New tabs
        private readonly By lineTab = By.Id("tbd");
        private readonly By noteTab = By.Id("tbd");



        public CreditNotePage IsOnCreditNotePage()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(partyInput);
            WaitUtil.WaitForElementVisible(creditDateInput);
            WaitUtil.WaitForElementVisible(noteInput);
            return this;
        }
        public CreditNotePage SearchForParty(string partyName)
        {
            SendKeys(partyInput, partyName);
            SleepTimeInMiliseconds(1000);
            ClickOnElement(partySelectOption, partyName);
            return this;
        }
        public CreditNotePage VerifyNewTabsArePresent()
        {
            
            return this;
        }
    }
}
