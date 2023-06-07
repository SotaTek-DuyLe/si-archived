using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class SubcontractedTasksListPage : BasePageCommonActions
    {
        private readonly By allSubconrtactedRow = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");

        [AllureStep]
        public SubcontractedTasksListPage FilterTaskId(string mode, string value)
        {
            ClickOnElement(By.XPath("//div[contains(@class, 'l1 r1')]/descendant::button"));
            SelectByDisplayValueOnUlElement(By.XPath("//ul[@aria-expanded='true']"), mode);
            SleepTimeInMiliseconds(100);
            SendKeys(By.XPath("//div[contains(@class, 'l1 r1')]/descendant::input"), value);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public SubcontractedTasksListPage VerifyTaskExists()
        {
            Assert.IsTrue(IsControlDisplayed(By.XPath("//div[@class='grid-canvas']//div[@class='slick-cell l0 r0']")));
            return this;
        }

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
