using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Events
{
    public class EventBulkUpdatePage : BasePage
    {
        private readonly By bulkUpdateTitle = By.XPath("//h5[text()=' Bulk Update ']");
        private readonly By selectedEventText = By.XPath("//h4[text()=' Selected Events  ']");
        private readonly By allEventRows = By.XPath("//tbody/tr");
        private readonly By actionSelectDd = By.CssSelector("select[id='action']");
        private readonly By notesTextbox = By.XPath("//label[text()='Notes']/following-sibling::textarea");

        //DYNAMIC
        private const string eventIdColumn = "//tr[{0}]//td[@data-bind='text: $data.eventId']";
        private const string descriptionColumn = "//tr[{0}]//td[@data-bind='text: $data.description']";
        private const string eventDateColumn = "//tr[{0}]//td[@data-bind='text: $data.eventDate']";
        private const string serviceColumn = "//tr[{0}]//td[@data-bind='text: $data.service']";
        private const string addressCoumn = "//tr[{0}]//td[@data-bind='text: $data.address']";
        private const string anyActionOption = "//select[@id='action']/option[text()='{0}']";

        [AllureStep]
        public EventBulkUpdatePage IsEventBulkUpdatePage()
        {
            WaitUtil.WaitForElementVisible(bulkUpdateTitle);
            Assert.IsTrue(IsControlDisplayed(bulkUpdateTitle));
            Assert.IsTrue(IsControlDisplayed(selectedEventText));
            return this;
        }
        [AllureStep]
        public List<EventInBulkUpdateEventModel> GetAllEventInPage()
        {
            List<EventInBulkUpdateEventModel> eventInBulkUpdateEventModels = new List<EventInBulkUpdateEventModel>();
            List<IWebElement> allRowsEvent = GetAllElements(allEventRows);
            for(int i = 0; i < allRowsEvent.Count; i++)
            {
                string eventId = GetElementText(eventIdColumn, (i + 1).ToString());
                string description = GetElementText(descriptionColumn, (i + 1).ToString());
                string eventDate = GetElementText(eventDateColumn, (i + 1).ToString());
                string service = GetElementText(serviceColumn, (i + 1).ToString());
                string address = GetElementText(addressCoumn, (i + 1).ToString());
                eventInBulkUpdateEventModels.Add(new EventInBulkUpdateEventModel(eventId, description, eventDate, service, address));
            }
            return eventInBulkUpdateEventModels;
        }
        [AllureStep]
        public EventBulkUpdatePage VerifyRecordInfoInEventBulkUpdatePage(List<EventInBulkUpdateEventModel> eventInBulkUpdateEventModels, string[] eventIds, string[] eventTypes, string[] eventServices, string[] eventAddresses)
        {
            for(int i = 0; i < eventInBulkUpdateEventModels.Count; i++)
            {
                Assert.AreEqual(eventIds[i], eventInBulkUpdateEventModels[i].eventId, "EventId is not correct");
                Assert.AreEqual(eventTypes[i], eventInBulkUpdateEventModels[i].description, "Description is not correct");
                Assert.AreEqual(eventServices[i], eventInBulkUpdateEventModels[i].service, "Service is not correct");
                Assert.AreEqual(eventAddresses[i], eventInBulkUpdateEventModels[i].address, "Address is not correct");
            }
            return this;
        }
        [AllureStep]
        public EventBulkUpdatePage ClickActionDdAndVerify()
        {
            ClickOnElement(actionSelectDd);
            //Verify
            foreach(string action in CommonConstants.ActionEventBulkUpdate)
            {
                Assert.IsTrue(IsControlDisplayed(anyActionOption, action));
            }
            return this;
        }
        [AllureStep]
        public EventBulkUpdatePage SelectAnyAction(string actionOptionValue)
        {
            ClickOnElement(anyActionOption, actionOptionValue);
            return this;
        }
        [AllureStep]
        public EventBulkUpdatePage AddNotes(string noteValue)
        {
            WaitUtil.WaitForElementVisible(notesTextbox);
            SendKeys(notesTextbox, noteValue);
            return this;
        }
    }
}
