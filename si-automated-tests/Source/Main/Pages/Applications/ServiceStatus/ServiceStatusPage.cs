using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Applications.ServiceStatus
{
    public class ServiceStatusPage : BasePage
    {
        private readonly By filterInputById = By.XPath("//div[contains(@class, 'l1 r1')]//input");
        private readonly By firstResult = By.XPath("//div[@class='grid-canvas']/div[1]");
        private readonly By firstCheckbox = By.XPath("//div[@class='grid-canvas']/div[1]//input");
        private readonly By selectAllCheckbox = By.XPath("//div[@title='Select/Deselect All']//input");
        private readonly By allServiceStatusRow = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By loadingIconFrameTab = By.XPath("//div[@id='form']/following-sibling::div/div[@class='loading-data']");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        [AllureStep]
        public ServiceStatusPage IsServiceStatusLoaded()
        {
            WaitUtil.WaitForAllElementsVisible("//div[@class='grid-canvas']/div");
            return this;
        }

        [AllureStep]
        public ServiceStatusPage FilterServiceStatusById(string id)
        {
            WaitUtil.WaitForElementInvisible(loadingIconFrameTab);
            SendKeys(filterInputById, id.ToString());
            WaitUtil.WaitForElementVisible(loadingIconFrameTab);
            WaitUtil.WaitForElementInvisible(loadingIconFrameTab);
            WaitUtil.WaitForElementVisible(firstResult);
            
            return this;
        }
        [AllureStep]
        public RoundInstanceForm OpenFirstResult()
        {
            ClickOnElement(selectAllCheckbox);
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<RoundInstanceForm>();
        }

        [AllureStep]
        public ServiceStatusPage VerifyDisplayVerticalScrollBarServiceStatusPage()
        {
            VerifyDisplayVerticalScrollBar(containerPage);
            return this;
        }

    }
}
