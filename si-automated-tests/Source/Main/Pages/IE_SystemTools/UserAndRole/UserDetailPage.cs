﻿using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;

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
        private readonly By accessRoleTab = By.XPath("//a[text()='Data Access Roles']/parent::li");
        private readonly By adminRoleTab = By.XPath("//a[text()='Admin Roles']");
        private readonly By resetPasswordBtn = By.XPath("//span[text()='Reset Password']");
        private readonly By loadingIconHidden = By.XPath("//div[@id='Progress' and contains(@style, 'visibility: hidden')]");


        private readonly By accessRoleOption = By.XPath("//span[contains(text(),'North Star Commercial')]/ancestor::td/following-sibling::td");
        private readonly string adminRoleOption = "//span[contains(text(), '{0}')]/parent::td/following-sibling::td/input";
        private readonly By inpectionsAdminRole = By.XPath("(//span[contains(text(), 'Inspections')])[2]/parent::td/following-sibling::td/input");

        private readonly By rightFrame = By.XPath("//iframe[@id='RightFrame']");

        [AllureStep]
        public UserDetailPage IsOnUserDetailPage()
        {
            WaitUtil.WaitForElementVisible(saveBtn);
            WaitUtil.WaitForElementVisible(detailTab);
            WaitUtil.WaitForElementVisible(accessRoleTab);
            WaitUtil.WaitForElementVisible(adminRoleTab);
            return this;
        }
        [AllureStep]
        public UserDetailPage SwitchToRightFrame()
        {
            SwitchToFrame(rightFrame);
            return this;
        }
        [AllureStep]
        public UserDetailPage EnterUserName(string userName)
        {
            SendKeys(userNameBox, userName);
            return this;
        }
        [AllureStep]
        public UserDetailPage EnterDisplayName(string displayName)
        {
            SendKeys(displayNameBox, displayName);
            return this;
        }
        [AllureStep]
        public UserDetailPage EnterEmail(string email)
        {
            SendKeys(emailAddressBox, email);
            return this;
        }
        [AllureStep]
        public UserDetailPage EnterUserDetails(string userName, string displayName, string email)
        {
            EnterUserName(userName)
            .EnterDisplayName(displayName)
            .EnterEmail(email);
            return this;
        }
        [AllureStep]
        public UserDetailPage ClickDataAccessRoles()
        {
            ClickOnElement(accessRoleTab);
            return this;
        }
        [AllureStep]
        public UserDetailPage ChooseNorthStarCommercialAsAccessRole(String role)
        {
            ClickOnElement(accessRoleOption);
            return this;
        }
        [AllureStep]
        public UserDetailPage ClickAdminRoles()
        {
            ClickOnElement(adminRoleTab);
            return this;
        }
        [AllureStep]
        public UserDetailPage ChooseAdminRole(string role)
        {
            ScrollDownInElement(By.Id("RightDef"));
            WebElement target = (WebElement)GetElement(String.Format(adminRoleOption, role));
            if (!target.Selected)
            {
                ClickOnElement(adminRoleOption, role);
            }
            return this;
        }
        [AllureStep]
        public UserDetailPage UntickAdminRole(string role)
        {
            ScrollDownInElement(By.Id("RightDef"));
            WebElement target = (WebElement)GetElement(String.Format(adminRoleOption, role));
            if (target.Selected)
            {
                ClickOnElement(adminRoleOption, role);
            }
            return this;
        }
        [AllureStep]
        public UserDetailPage ChooseInspectionAdminRole()
        {
            ClickOnElement(inpectionsAdminRole);
            return this;
        }
        [AllureStep]
        public UserDetailPage ClickSave()
        {
            ClickOnElement(saveBtn);
            return this;
        }
        [AllureStep]
        public UserPage ClickSaveAndClose()
        {
            ClickOnElement(saveCloseBtn);
            return PageFactoryManager.Get<UserPage>();
        }
        [AllureStep]
        public UserDetailPage ClickResetPassword()
        {
            ClickOnElement(resetPasswordBtn);
            return this;
        }
        [AllureStep]
        public UserDetailPage WaitForLoadingIconDisappear()
        {
            WaitUtil.WaitForAllElementsPresent(loadingIconHidden);
            return this;
        }

    }
}
