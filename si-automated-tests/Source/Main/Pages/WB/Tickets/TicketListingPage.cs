using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.WB.Tickets
{
    public class TicketListingPage : BasePage
    {
        private readonly By addNewTicketBtn = By.XPath("//button[text()='Add New Item']");
        
        public TicketListingPage ClickAddNewTicketBtn()
        {
            ClickOnElement(addNewTicketBtn);
            return this;
        }

    }
}
