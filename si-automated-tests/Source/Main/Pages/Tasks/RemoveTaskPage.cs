using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class RemoveTaskPage : BasePage
    {
        private readonly By titlePopup = By.XPath("//h4[text()='Warning']");
        private readonly By contentPopup = By.XPath("//div[text()='Are you sure you want to delete this Task?']");
        private readonly By yesBtn = By.XPath("//button[text()='Yes']");
        private readonly By noBtn = By.XPath("//button[text()='No']");
        private readonly By closeBtn = By.XPath("//button[contains(@class, 'close')]");

        public RemoveTaskPage IsDeleteTaskPopup()
        {
            WaitUtil.WaitForElementVisible(titlePopup);
            Assert.IsTrue(IsControlDisplayed(titlePopup));
            Assert.IsTrue(IsControlDisplayed(contentPopup));
            Assert.IsTrue(IsControlDisplayed(yesBtn));
            Assert.IsTrue(IsControlDisplayed(noBtn));
            Assert.IsTrue(IsControlDisplayed(closeBtn));
            return this;
        }

        public RemoveTaskPage ClickOnYesDeleteTaskPopupBtn()
        {
            ClickOnElement(yesBtn);
            return this;
        }

        public TasksListingPage ClickOnNoDeleteTaskPopupBtn()
        {
            ClickOnElement(noBtn);
            return PageFactoryManager.Get<TasksListingPage>(); ;
        }

        public TasksListingPage ClickCloseDeleteTaskPopupBtn()
        {
            ClickOnElement(closeBtn);
            return PageFactoryManager.Get<TasksListingPage>();
        }

        public TasksListingPage EnterEscBtn()
        {
            Actions action = new Actions(IWebDriverManager.GetDriver());
            action.SendKeys(Keys.Escape).Perform();
            return PageFactoryManager.Get<TasksListingPage>();
        }
    }
}
