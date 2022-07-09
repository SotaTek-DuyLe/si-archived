using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Events
{
    public class DeleteEventPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Warning']");
        private readonly By content = By.XPath("//div[text()='Are you sure you want to delete this Event?']");
        private readonly By yesBtn = By.CssSelector("button[data-bb-handler='Confirm']");
        private readonly By noBtn = By.CssSelector("button[data-bb-handler='Cancel']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");

        public DeleteEventPage IsWarningPopup()
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(title));
            Assert.IsTrue(IsControlDisplayed(content));
            Assert.IsTrue(IsControlEnabled(yesBtn));
            Assert.IsTrue(IsControlEnabled(noBtn));
            Assert.IsTrue(IsControlDisplayed(closeBtn));
            return this;
        }

        public EventsListingPage ClickNoBtn()
        {
            ClickOnElement(noBtn);
            return PageFactoryManager.Get< EventsListingPage >();
        }

        public EventsListingPage ClickClosePopupBtn()
        {
            ClickOnElement(closeBtn);
            return PageFactoryManager.Get<EventsListingPage>();
        }

        public EventsListingPage ClickYesBtn()
        {
            ClickOnElement(yesBtn);
            return PageFactoryManager.Get<EventsListingPage>();
        }
    }
}
