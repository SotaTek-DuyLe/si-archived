using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages
{
    public class LoginPage : BasePage
    {
        private const string UserNameInput = "//label[text()='Username']/following-sibling::input";
        private const string PasswordInput = "//label[text()='Password']/following-sibling::input";
        private const string SignInBtn = "//button[text()='Sign In']";
        private const string ForgotPasswordLink = "//a[text()='Forgot Password']";
        private const string HelpLink = "//a[text()='Help']";

        private const string ErrorMessage = "//p[text()='Incorrect user name or password']";
        [AllureStep]
        public LoginPage IsOnLoginPage()
        {
            WaitUtil.WaitForElementVisible(ForgotPasswordLink);
            Assert.IsTrue(IsControlDisplayed(ForgotPasswordLink));
            Assert.IsTrue(IsControlDisplayed(HelpLink));
            Assert.IsTrue(IsControlDisplayed(SignInBtn));
            return this;
        }
        [AllureStep]
        public LoginPage SendKeyToUsername(string UserName)
        {
            SendKeys(UserNameInput, UserName);
            return this;
        }
        public LoginPage SendKeyToPassword(string Password)
        {
            SendKeys(PasswordInput, Password);
            return this;
        }
        public LoginPage ClickOnSignIn()
        {
            ClickOnElement(SignInBtn);
            return this;
        }
        public LoginPage VerifyErrorMessageDisplay()
        {
            Assert.IsTrue(IsControlDisplayed(ErrorMessage));
            return this;
        }
        public HomePage Login(string username, string password)
        {
            SendKeyToUsername(username);
            SendKeyToPassword(password);
            ClickOnSignIn();
            return PageFactoryManager.Get<HomePage>();
        }
    }
}
