using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.IE_Configuration
{
    public class AccountTypeDetailPage : BasePage
    {
        private By overridingValue = By.XPath("//span[text()='Override Value']/parent::td/following-sibling::td//input");
        private By overridingcheckbox = By.XPath("//span[text()='Override Accounting Reference']/parent::td/following-sibling::td//input");
        private By saveButton = By.XPath("//img[@title='Save']");
        public AccountTypeDetailPage()
        {
            WaitUtil.WaitForElementVisible(overridingValue);
            WaitUtil.WaitForElementVisible(overridingcheckbox);
        }
        [AllureStep]
        public AccountTypeDetailPage TickOverrideCheckbox()
        {
            if(!IsElementSelected(overridingcheckbox))
            {
                ClickOnElement(overridingcheckbox);
            }
            return this;
        }
        [AllureStep]
        public AccountTypeDetailPage UntickOverrideCheckbox()
        {
            if(IsElementSelected(overridingcheckbox))
            {
                ClickOnElement(overridingcheckbox);
            }
            return this;
        }
        [AllureStep]
        public AccountTypeDetailPage clickSaveButton()
        {
            ClickOnElement(saveButton);
            return this;
        }
        [AllureStep]
        public AccountTypeDetailPage inputOverrideValue(string value)
        {
            SendKeys(overridingValue, value);
            return this;
        }
    }
}
