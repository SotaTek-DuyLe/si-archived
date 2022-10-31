using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.IE_Configuration
{
    public class ResolutionCodeDetailPage : BasePage
    {
        private By resolutionNameInput = By.XPath("//input[@mandmsg='Resolution Code']");
        private By clientReferenceInput = By.XPath("//span[text()='Client Reference']/parent::td/following-sibling::td//input");
        private By saveBtn = By.XPath("//img[@title='Save']/parent::td");
        private By resolutionCodeId = By.XPath("//span[contains(text(),'ResolutionCode')]/following-sibling::span[1]");

        [AllureStep]
        public ResolutionCodeDetailPage IsOnResolutionCodeDetailPage()
        {
            WaitUtil.WaitForElementVisible(resolutionNameInput);
            WaitUtil.WaitForElementVisible(clientReferenceInput);
            return this;
        }

        [AllureStep]
        public ResolutionCodeDetailPage VerifyNoIdIsGenerated()
        {
            Assert.AreEqual("-1", GetResolutionCodeId());
            return this;
        }

        [AllureStep]
        public ResolutionCodeDetailPage InputResolutionCodeDetails(string name, string clientReference)
        {
            SendKeys(resolutionNameInput, name);
            SendKeys(clientReferenceInput, clientReference);
            return this;
        }
        [AllureStep]
        public ResolutionCodeDetailPage SaveResolutionCode()
        {
            ClickOnElement(saveBtn);
            return this;
        }
        private String GetResolutionCodeId()
        {
            return WaitUtil.WaitForElementVisible(resolutionCodeId).Text.Replace(":","").Replace("(", "").Replace(")", "").Trim();
        }
    }
}
