using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

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
    }
}
