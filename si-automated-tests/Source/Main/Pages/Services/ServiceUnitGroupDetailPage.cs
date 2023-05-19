using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceUnitGroupDetailPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='SERVICE UNIT GROUP']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");

        [AllureStep]
        public ServiceUnitGroupDetailPage IsServiceUnitGroupDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            return this;
        }

        [AllureStep]
        public ServiceUnitGroupDetailPage VerifyCurrentUrl()
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/service-unit-groups/new?contractId=1", GetCurrentUrl());
            return this;
        }
    }
}
