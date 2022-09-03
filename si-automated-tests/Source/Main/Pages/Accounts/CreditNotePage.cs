using System;
using NUnit.Framework;
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
        private readonly By accountRefInput = By.Id("account-ref");
        private readonly By yesBtn = By.XPath("//button[text()='Yes']");

        //New tabs
        private readonly By lineTab = By.XPath("//a[@aria-controls='creditNoteLines-tab']");
        private readonly By noteTab = By.XPath("//a[@aria-controls='notes-tab']");


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
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(lineTab);
            WaitUtil.WaitForElementVisible(noteTab);
            return this;
        }
        public CreditNotePage ClickYesBtn()
        {
            ClickOnElement(yesBtn);
            return this;
        }
        public CreditNotePage VerifyAccountReferenceIsReadonly()
        {
            Assert.AreEqual("true", GetAttributeValue(accountRefInput, "readonly"));
            return this;
        }

    }
}
