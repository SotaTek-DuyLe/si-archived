using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
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
        private readonly By clearBtn = By.CssSelector("button[title='Clear Filters']");
        private readonly By deleteBtn = By.XPath("//button[text()='Delete Item']");
        private readonly By taskRow = By.XPath("//div[@class='grid-canvas']");

        [AllureStep]
        public TasksListingPage WaitForTaskListinPageDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(deleteItem);
            return this;
        }
        [AllureStep]
        public TasksListingPage FilterByTaskId(string taskId)
        {
            SendKeys(filterInputById, taskId);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public TasksListingPage FilterMultipleTaskId(string firstTaskId, string secondTaskId)
        {
            SendKeys(filterInputById, firstTaskId + "," + secondTaskId);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public DetailTaskPage ClickOnFirstRecord()
        {
            DoubleClickOnElement(firstRecordRow);
            return PageFactoryManager.Get<DetailTaskPage>();
        }
        [AllureStep]
        public TasksListingPage ClickCheckboxFirstTaskInList()
        {
            ClickOnElement(firstCheckboxTask);
            return this;
        }
        [AllureStep]
        public TasksListingPage ClickCheckboxMultipleTaskInList()
        {
            ClickOnElement(allRecordCheckbox);
            return this;
        }
        [AllureStep]
        public TasksBulkUpdatePage ClickOnBulkUpdateBtn()
        {
            WaitUtil.WaitForElementVisible(bulkUpdateBtn);
            ClickOnElement(bulkUpdateBtn);
            return PageFactoryManager.Get<TasksBulkUpdatePage>();
        }
        [AllureStep]
        public TasksListingPage ClickClearBtn()
        {
            ClickOnElement(clearBtn);
            return this;
        }
        [AllureStep]
        public TasksListingPage ClickDeleteBtn()
        {
            ClickOnElement(deleteBtn);
            return this;
        }
        [AllureStep]
        public TasksListingPage VerifyNoRecordDisplayed()
        {
            Assert.AreEqual("", GetElementText(taskRow));
            return this;
        }

    }
}
