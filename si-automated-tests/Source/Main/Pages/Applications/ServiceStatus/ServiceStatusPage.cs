using System;
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

        public ServiceStatusPage FilterServiceStatusById(string id)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, id.ToString());
            WaitForLoadingIconToDisappear();
            return this;
        }

        public RoundInstanceForm OpenFirstResult()
        {
            ClickOnElement(selectAllCheckbox);
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<RoundInstanceForm>();
        }

    }
}
