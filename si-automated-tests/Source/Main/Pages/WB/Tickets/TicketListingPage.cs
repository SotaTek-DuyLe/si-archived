using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.WB.Tickets
{
    public class TicketListingPage : BasePage
    {
        private readonly By addNewTicketBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By filterInputById = By.XPath("//div[contains(@class, 'l1 r1')]//input");
        private readonly By selectAllCheckbox = By.XPath("//div[@title='Select/Deselect All']//input");
        private readonly By firstResult = By.XPath("//div[@class='grid-canvas']/div[1]");

        public TicketListingPage ClickAddNewTicketBtn()
        {
            ClickOnElement(addNewTicketBtn);
            return this;
        }

        public TicketListingPage FilterTicketById(string ticketId)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, ticketId);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public WeighbridgeTicketDetailPage OpenFirstResult()
        {
            ClickOnElement(selectAllCheckbox);
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<WeighbridgeTicketDetailPage>();
        }

    }
}
