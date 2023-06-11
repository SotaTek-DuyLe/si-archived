using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services.ServiceTask
{
    public class ServiceTaskDetailPage : BasePage
    {
        private readonly By titleServiceTaskDetail = By.XPath("//span[text()='Service Task']");

        public ServiceTaskDetailPage WaitForServiceTaskDetailPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(titleServiceTaskDetail);
            return this;
        }

        public ServiceTaskDetailPage VerifyCurrentUrlServiceTaskDetail(string serviceTaskIdExp)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/service-tasks/" + serviceTaskIdExp, GetCurrentUrl());
            return this;
        }
    }
}
