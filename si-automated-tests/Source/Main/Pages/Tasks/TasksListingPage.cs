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
        private readonly By firstCheckboxTask = By.XPath("//div[@class='grid-canvas']//div[contains(@class, 'l0 r0')]/input");
        private readonly By bulkUpdateBtn = By.XPath("//button[text()='Bulk Update']");
        private readonly By allRecordCheckbox = By.XPath("//div[@title='Select/Deselect All']//input");

        public TasksListingPage WaitForTaskListinPageDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(deleteItem);
            return this;
        }

        public TasksListingPage FilterByTaskId(string taskId)
        {
            SendKeys(filterInputById, taskId);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public TasksListingPage FilterMultipleTaskId(string firstTaskId, string secondTaskId)
        {
            SendKeys(filterInputById, firstTaskId + "," + secondTaskId);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public DetailTaskPage ClickOnFirstRecord()
        {
            DoubleClickOnElement(firstRecordRow);
            return PageFactoryManager.Get<DetailTaskPage>();
        }

        public TasksListingPage ClickCheckboxFirstTaskInList()
        {
            ClickOnElement(firstCheckboxTask);
            return this;
        }

        public TasksListingPage ClickCheckboxMultipleTaskInList()
        {
            ClickOnElement(allRecordCheckbox);
            return this;
        }

        public TasksBulkUpdatePage ClickOnBulkUpdateBtn()
        {
            WaitUtil.WaitForElementVisible(bulkUpdateBtn);
            ClickOnElement(bulkUpdateBtn);
            return PageFactoryManager.Get<TasksBulkUpdatePage>();
        }

    }
}
