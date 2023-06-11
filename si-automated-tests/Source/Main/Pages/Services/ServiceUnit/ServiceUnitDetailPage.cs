using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services.ServiceUnit
{
    public class ServiceUnitDetailPage : BasePage
    {
        private readonly By serviceUnitTitle = By.XPath("//h4/span[text()='Service Unit']");

        public ServiceUnitDetailPage WaitForServiceUnitDetailPageDisplayed()
        {
            WaitUtil.WaitForAllElementsVisible(serviceUnitTitle);
            return this;
        }

        public ServiceUnitDetailPage VerifyCurrentUrlServiceTaskDetail(string serviceUnitIdExp)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/service-units/" + serviceUnitIdExp, GetCurrentUrl());
            return this;
        }
    }
}
