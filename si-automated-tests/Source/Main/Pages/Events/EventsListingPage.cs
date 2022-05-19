using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Events
{
    public class EventsListingPage : BasePage
    {
        private readonly By firstRecord = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))]");
        private readonly By addNewEventItem = By.XPath("//button[text()='Add New Item']");
        private readonly By filterInputById = By.XPath("//div[contains(@class, 'l6 r6')]/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By linkedIcon = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))]/div[contains(@class, 'l3 r3')]");
        private readonly By clearBtn = By.XPath("//button[@title='Clear Filters']");
        private readonly By deleteEventItemBtn = By.XPath("//button[text()='Delete Item']");

        public EventsListingPage FilterByEventId(String eventId)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(addNewEventItem);
            SendKeys(filterInputById, eventId);
            ClickOnElement(applyBtn);
            return this;
        }

        public EventDetailPage ClickOnFirstRecord()
        {
            DoubleClickOnElement(firstRecord);
            return PageFactoryManager.Get<EventDetailPage>();
        }

        public EventDetailPage ClickRowWithIcon()
        {
            DoubleClickOnElement(linkedIcon);
            return PageFactoryManager.Get<EventDetailPage>();
        }

        public EventsListingPage ClickClearBtn()
        {
            ClickOnElement(clearBtn);
            return this;
        }

        public EventsListingPage ClickDeleteBtn()
        {
            ClickOnElement(deleteEventItemBtn);
            return this;
        }
    }
}
