using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class SubcontractedTasksListPage : BasePage
    {
        private readonly By allSubconrtactedRow = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        [AllureStep]
        public SubcontractedTasksListPage IsSubcontractedTasksLoaded()
        {
            WaitUtil.WaitForAllElementsVisible(allSubconrtactedRow);
            return this;
        }

        [AllureStep]
        public SubcontractedTasksListPage VerifyDisplayVerticalScrollBarSubcontractedTasksPage()
        {
            VerifyDisplayVerticalScrollBar(containerPage);
            return this;
        }

    }
}
