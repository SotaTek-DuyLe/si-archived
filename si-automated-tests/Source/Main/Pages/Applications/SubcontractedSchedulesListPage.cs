using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class SubcontractedSchedulesListPage : BasePage
    {
        private readonly By allSubconrtactedSchedulesRow = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        [AllureStep]
        public SubcontractedSchedulesListPage IsSubcontractedSchedulesLoaded()
        {
            WaitUtil.WaitForAllElementsVisible(allSubconrtactedSchedulesRow);
            return this;
        }

        [AllureStep]
        public SubcontractedSchedulesListPage VerifyDisplayVerticalScrollBarSubcontractedSchedulesPage()
        {
            VerifyDisplayVerticalScrollBar(containerPage);
            return this;
        }
    }
}
