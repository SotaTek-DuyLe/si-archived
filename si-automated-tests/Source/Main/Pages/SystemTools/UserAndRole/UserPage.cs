using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.UserAndRole
{
    public class UserPage : BasePage
    {
        private readonly By actionBtn = By.XPath("//span[contains(text(),'Actions')]");
        private readonly By newAction = By.XPath("//td[@id='mo_0']");
        private readonly By rightFrame = By.XPath("//iframe[@id='RightFrame']");

        public UserPage IsOnUserScreen()
        {
            SwitchToFirstWindow();
            SwitchToFrame(rightFrame);
            WaitUtil.WaitForElementVisible(actionBtn);
            return this;
        }
        public UserPage ClickAction()
        {
            ClickOnElement(actionBtn);
            return this;
        }
        public UserPage ClickNew()
        {
            ClickOnElement(newAction);
            return this;
        }
    }
}
