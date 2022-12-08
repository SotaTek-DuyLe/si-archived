using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages
{
    public class LoginPage : BasePage
    {
        private const string userNameInput = "//label[text()='Username']/following-sibling::input";
        private const string passwordInput = "//label[text()='Password']/following-sibling::input";
        private const string signInBtn = "//button[text()='Sign In']";
        private const string forgotPasswordLink = "//a[text()='Forgot Password']";
        private const string helpLink = "//a[text()='Help']";
        private readonly By nextBtn = By.XPath("//a[text()='Help']");
        private const string ErrorMessage = "//p[text()='Incorrect user name or password']";

        [AllureStep]
        public LoginPage IsOnLoginPage()
        {
            WaitUtil.WaitForElementVisible(userNameInput);
            WaitUtil.WaitForElementVisible(helpLink);
            //WaitUtil.WaitForElementVisible(nextBtn);
            return this;
        }
        [AllureStep]
        public LoginPage SendKeyToUsername(string UserName)
        {
            SendKeys(userNameInput, UserName);
            return this;
        }
        [AllureStep]
        public LoginPage SendKeyToPassword(string Password)
        {
            SendKeys(passwordInput, Password);
            return this;
        }
        [AllureStep]
        public LoginPage ClickOnSignIn()
        {
            ClickOnElement(signInBtn);
            return this;
        }
        [AllureStep]
        public LoginPage VerifyErrorMessageDisplay()
        {
            Assert.IsTrue(IsControlDisplayed(ErrorMessage));
            return this;
        }
        [AllureStep]
        public HomePage Login(string username, string password)
        {
            SendKeyToUsername(username);
            //Click next
            SendKeyToPassword(password);
            ClickOnSignIn();
            return PageFactoryManager.Get<HomePage>();
        }
    }
}
