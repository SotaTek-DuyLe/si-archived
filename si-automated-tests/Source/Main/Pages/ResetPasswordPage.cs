using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages
{
    public class ResetPasswordPage: BasePage
    {
        private readonly By newPasswordInput = By.Id("new-password");
        private readonly By confirmNewPasswordInput = By.Id("confirm-new-password");
        private readonly By setBtn = By.XPath("//button[@type='submit']");
        private readonly By successMessage = By.XPath("//div[text()='You have successfully changed your password.']");
        private readonly By gotoLoginBtn = By.XPath("//a[text()='Go to Login']");
        
        public ResetPasswordPage IsOnResetPasswordPage()
        {
            WaitUtil.WaitForElementVisible(newPasswordInput);
            WaitUtil.WaitForElementVisible(confirmNewPasswordInput);
            WaitUtil.WaitForElementVisible(setBtn);
            return this;
        }
        public ResetPasswordPage ResetPassword(string _newPassword)
        {
            SendKeys(newPasswordInput, _newPassword);
            SendKeys(confirmNewPasswordInput, _newPassword);
            ClickOnElement(setBtn);
            return this;
        }
        public ResetPasswordPage VerifyResetSuccessfully()
        {
            WaitUtil.WaitForElementVisible(successMessage);
            return this;
        }
        public LoginPage ClickGoToLogin()
        {
            ClickOnElement(gotoLoginBtn);
            return PageFactoryManager.Get<LoginPage>();
        }
    }
}
