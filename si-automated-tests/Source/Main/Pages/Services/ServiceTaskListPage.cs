using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceTaskListPage : BasePage
    {
        private readonly By allServiceTasksRows = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        [AllureStep]
        public ServiceTaskListPage IsServiceTaskListPageLoaded()
        {
            WaitUtil.WaitForAllElementsVisible(allServiceTasksRows);
            return this;
        }

        [AllureStep]
        public ServiceTaskListPage VerifyDisplayVerticalScrollBarServiceTaskPage()
        {
            VerifyDisplayVerticalScrollBar(containerPage);
            return this;
        }
    }
}
