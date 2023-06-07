using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class TasksListingPage : BasePageCommonActions
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
        private string HeaderNameXPath = "//div[@class='echo-grid']//div[contains(@class, 'slick-header-column')]//span[@class='slick-column-name' and text()='{0}']";
        private string TaskTable = "//div[@class='grid-canvas']";
        private string TaskRow = "./div[contains(@class, 'slick-row')]";
        private string TaskPriorityCell = "./div[contains(@class, 'slick-cell l23 r23')]";
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        private TableElement taskTableEle;
        public TableElement TaskTableEle
        {
            get => taskTableEle;
        } 

        public TasksListingPage()
        {
            taskTableEle = new TableElement(TaskTable, TaskRow, new List<string>() { TaskPriorityCell });
            taskTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        [AllureStep]
        public TasksListingPage FilterPriority(string mode, string value)
        {
            ClickOnElement(By.XPath("//div[contains(@class, 'l23 r23')]/descendant::button"));
            SelectByDisplayValueOnUlElement(By.XPath("//ul[@aria-expanded='true']"), mode);
            SleepTimeInMiliseconds(100);
            SendKeys(By.XPath("//div[contains(@class, 'l23 r23')]/descendant::input"), value);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public TasksListingPage FilterTaskState(string mode, string value)
        {
            ClickOnElement(By.XPath("//div[contains(@class, 'l6 r6')]/descendant::button"));
            SelectByDisplayValueOnUlElement(By.XPath("//ul[@aria-expanded='true']"), mode);
            SleepTimeInMiliseconds(100);
            SendKeys(By.XPath("//div[contains(@class, 'l6 r6')]/descendant::input"), value);
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public TasksListingPage VerifyPriority(string value)
        {
            VerifyCellValue(TaskTableEle, 0, TaskTableEle.GetCellIndex(TaskPriorityCell), value);
            return this;
        }

        [AllureStep]
        public TasksListingPage VerifyHeadersVisible(List<string> headerNames)
        {
            foreach (var item in headerNames)
            {
                VerifyElementVisibility(By.XPath(string.Format(HeaderNameXPath, item)), true);
            }
            return this;
        }

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
