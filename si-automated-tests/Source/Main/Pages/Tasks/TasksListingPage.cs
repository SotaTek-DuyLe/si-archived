using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class TasksListingPage : BasePage
    {
        private readonly By firstRecordRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By deleteItem = By.XPath("//button[text()='Delete Item']");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By filterInputById = By.XPath("//div[contains(@class, 'l5 r5')]/descendant::input");

        public TasksListingPage WaitForTaskListinPageDisplayed()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(deleteItem);
            return this;
        }

        public TasksListingPage FilterByTaskId(string taskId)
        {
            SendKeys(filterInputById, taskId);
            ClickOnElement(applyBtn);
            return this;
        }

        public DetailTaskPage ClickOnFirstRecord()
        {
            DoubleClickOnElement(firstRecordRow);
            return PageFactoryManager.Get<DetailTaskPage>();
        }

    }
}
