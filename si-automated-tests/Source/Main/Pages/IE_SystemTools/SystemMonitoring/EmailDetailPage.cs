using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.SystemTools.SystemMonitoring
{
    public class EmailDetailPage: BasePage
    {
        private readonly By mailBody = By.XPath("//a[contains(text(),'Mail Body')]");
        private readonly By bodyView = By.XPath("//a[contains(text(),'Body View')]");
        private readonly By attributes = By.XPath("//a[contains(text(),'Attribute')]");


        private readonly By resetPasswordLink = By.XPath("//div[contains(@id,'C_View')]/descendant::a");


        [AllureStep]
        public EmailDetailPage IsOnEmailDetailPage()
        {
            Thread.Sleep(1500);
            SwitchToLastWindow();
            WaitUtil.WaitForElementVisible(mailBody);
            WaitUtil.WaitForElementVisible(bodyView);
            WaitUtil.WaitForElementVisible(attributes);
            return this;
        }
        [AllureStep]
        public EmailDetailPage ClickBodyView()
        {
            ClickOnElement(bodyView);
            return this;
        }
        [AllureStep]
        public String GetPasswordResetLink()
        {
            return GetElementText(resetPasswordLink);
        }
    }
}
