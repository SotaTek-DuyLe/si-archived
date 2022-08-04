using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccount
{
    public class PartyAccountPage : BasePage
    {
        private readonly By accountTypeInput = By.Id("account-type");
        private readonly By accountRefInput = By.Id("account-ref");

        private string accountCheckBox = "//label[text()='{0}']/following-sibling::div/input";

        public PartyAccountPage IsOnAccountPage()
        {
            WaitUtil.WaitForElementVisible(accountTypeInput);
            WaitUtil.WaitForElementVisible(accountRefInput);
            Assert.IsTrue(IsControlDisplayed(accountTypeInput));
            Assert.IsTrue(IsControlDisplayed(accountRefInput));
            return this;
        }
        public PartyAccountPage CheckOnAccountType(string account)
        {
            if(!IsElementSelected(accountCheckBox, account))
            {
                ClickOnElement(accountCheckBox, account);
            }
            return this;
        }
        public PartyAccountPage UncheckOnAccountType(string account)
        {
            if (IsElementSelected(accountCheckBox, account))
            {
                ClickOnElement(accountCheckBox, account);
            }
            return this;
        }
        public PartyAccountPage VerifyAccountTypeChecked(string account)
        {
            Assert.IsTrue(IsElementSelected(accountCheckBox, account));
            return this;
        }
        public PartyAccountPage VerifyAccountTypeUnchecked(string account)
        {
            Assert.IsFalse(IsElementSelected(accountCheckBox, account));
            return this;
        }
        public PartyAccountPage VerifyAccountReferenceEnabled(bool isEnabled)
        {
            Assert.AreEqual(isEnabled, GetElement(accountRefInput).Enabled);
            return this;
        }
        public PartyAccountPage SelectAccountType(string accountType)
        {
            SelectTextFromDropDown(accountTypeInput, accountType);
            return this;
        }
    }
}
