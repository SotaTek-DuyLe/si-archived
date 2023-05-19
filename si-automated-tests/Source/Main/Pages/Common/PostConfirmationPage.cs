using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Common
{
    public class PostConfirmationPage : BasePage
    {
        private readonly By textInfo = By.XPath("//div[@class='text-info']");
        private readonly By confirmBtn = By.XPath("//button[@data-bb-handler='Confirm']");
        private readonly By cancelBtn = By.XPath("//button[@data-bb-handler='Cancel']");

        public PostConfirmationPage()
        {
            SwitchToLastWindow();
            IsOnPage();
        }
        [AllureStep]
        private void IsOnPage()
        {
            WaitUtil.WaitForElementVisible(textInfo);
            WaitUtil.WaitForElementVisible(confirmBtn);
            WaitUtil.WaitForElementVisible(cancelBtn);
        }
        [AllureStep]
        public PostConfirmationPage VerifyInfoMessage(string expected)
        {
            Assert.AreEqual(expected, GetElementText(textInfo));
            return this;
        }
        [AllureStep]
        public PostConfirmationPage ClickConfirm()
        {
            ClickOnElement(confirmBtn);
            return this;
        }
        [AllureStep]
        public PostConfirmationPage ClickCancel()
        {
            ClickOnElement(cancelBtn);
            return this;
        }
    }
}
