using System;
using System.Collections.Generic;
using System.Text;
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
        public AccountTypeDetailPage TickOverrideCheckbox()
        {
            if(!IsElementSelected(overridingcheckbox))
            {
                ClickOnElement(overridingcheckbox);
            }
            return this;
        }
        public AccountTypeDetailPage UntickOverrideCheckbox()
        {
            if(IsElementSelected(overridingcheckbox))
            {
                ClickOnElement(overridingcheckbox);
            }
            return this;
        }
        public AccountTypeDetailPage clickSaveButton()
        {
            ClickOnElement(saveButton);
            return this;
        }
        public AccountTypeDetailPage inputOverrideValue(string value)
        {
            SendKeys(overridingValue, value);
            return this;
        }
    }
}
