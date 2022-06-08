using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.UserAndRole
{
    public class UserDetailPage : BasePage
    {
        private readonly By userNameBox = By.XPath("//input[@mandmsg='User Name']");
        private readonly By displayNameBox = By.XPath("//input[@mandmsg='Display Name']");
        private readonly By emailAddressBox = By.XPath("//input[@mandmsg='Email Address']");

        private readonly By saveBtn = By.XPath("//img[@title='Save']");
        private readonly By saveCloseBtn = By.XPath("//img[@title='Save and close']");
        private readonly By detailTab = By.XPath("//a[text()='Details']");
        private readonly By accessRoleTab = By.XPath("//a[text()='Data Access Roles']");
        private readonly By adminRoleTab = By.XPath("//a[text()='Admin Roles']");
        private readonly By resetPasswordBtn = By.XPath("//span[text()='Reset Password']");


        private readonly By accessRoleOption = By.XPath("//span[text()='North Star Commercial']/ancestor::td/following-sibling::td"); 
        private const string adminRoleOption = "(//span[contains(text(), '{0}')])[2]/parent::td/following-sibling::td/input";

        private readonly By rightFrame = By.XPath("//iframe[@id='RightFrame']");

        public UserDetailPage IsOnUserDetailPage()
        {
            WaitUtil.WaitForElementVisible(saveBtn);
            WaitUtil.WaitForElementVisible(detailTab);
            WaitUtil.WaitForElementVisible(accessRoleTab);
            WaitUtil.WaitForElementVisible(adminRoleTab);
            return this;
        }
        public UserDetailPage SwitchToRightFrame()
        {
            SwitchToFrame(rightFrame);
            return this;
        }
        public UserDetailPage EnterUserName(string userName)
        {
            SendKeys(userNameBox, userName);
            return this;
        }
        public UserDetailPage EnterDisplayName(string displayName)
        {
            SendKeys(displayNameBox, displayName);
            return this;
        }
        public UserDetailPage EnterEmail(string email)
        {
            SendKeys(emailAddressBox, email);
            return this;
        }
        public UserDetailPage EnterUserDetails(string userName, string displayName, string email)
        {
            EnterUserName(userName)
            .EnterDisplayName(displayName)
            .EnterEmail(email);
            return this;
        }
        public UserDetailPage ClickDataAccessRoles()
        {
            ClickOnElement(accessRoleTab);
            return this;
        }
        public UserDetailPage ChooseAccessRole(String role)
        {
            ClickOnElement(accessRoleOption);
            return this;
        }
        public UserDetailPage ClickAdminRoles()
        {
            ClickOnElement(adminRoleTab);
            return this;
        }
        public UserDetailPage ChooseAdminRole(string role)
        {
            ClickOnElement(adminRoleOption, role);
            return this;
        }
        public UserDetailPage ClickSave()
        {
            ClickOnElement(saveBtn);
            return this;
        }
        public UserPage ClickSaveAndClose()
        {
            ClickOnElement(saveCloseBtn);
            return PageFactoryManager.Get<UserPage>();
        }
        public UserDetailPage ClickResetPassword()
        {
            ClickOnElement(resetPasswordBtn);
            return this;
        }

    }
}
