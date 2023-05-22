using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs
{
    public class TaskTab : BasePageCommonActions
    {
        private readonly By deleteItemBtn = By.XPath("//button[text()='Delete Item']");
        private readonly By bulkUpdateItemBtn = By.XPath("//button[text()='Bulk Update']");

        private readonly By taskType = By.XPath("//div[@class='slick-cell l11 r11']");
        private readonly By taskDueDate = By.XPath("//div[@class='slick-cell l13 r13']");
        private readonly By containerTasksTab = By.XPath("//div[@id='tasks-tab']//div[@class='grid-canvas']");

        private readonly By taskTabBtn = By.XPath("//a[@aria-controls='tasks-tab']");
        private readonly By refreshBtn = By.XPath("//button[@title='Refresh']");
        private string firstTask = "(//div[text()='Deliver Commercial Bin'])[2]";
        private string secondTask = "(//div[text()='Deliver Commercial Bin'])[1]";
        private string dueDateColumn = "/following-sibling::div[2]";
        private string allColumnTitle = "//div[contains(@class, 'slick-header-columns')]/div/span[1]";
        private string eachColumn = "//div[@class='grid-canvas']/div/div[{0}]";
        private string allRows = "//div[@class='grid-canvas']/div";

        private string deliverCommercialBinWithDateRows = "//div[@class='grid-canvas']/div[contains(.,'Deliver Commercial Bin') and contains(.,'{0}')]";
        private string removeCommercialBinWithDateRows = "//div[text()='Remove Commercial Bin']/following-sibling::div[contains(@class, 'r13') and contains(.,'{0}')]";

        private string retiredTasks = "//div[@class='grid-canvas']/div[contains(@class,'retired')]";

        private string taskId = "//div[contains(@class,'r5')]/div[text()='{0}']";
        private string taskIdCheckBox = "//div[text()='{0}']/parent::div/preceding-sibling::div[contains(@class,'r0')]/input";
        private string retiredTaskWithId = "//div[text()='{0}']/parent::div/parent::div[contains(@class,'retired')]//div[text()='Completed']";

        private string firstTaskId = "(//div[contains(@class, 'r5')])[4]/div";
        private string secondTaskId = "(//div[contains(@class, 'r5')])[5]/div";
        private string thirdTaskId = "(//div[contains(@class, 'r5')])[6]/div";
        private string fourthTaskId = "(//div[contains(@class, 'r5')])[7]/div";

        private string TaskTable = "//div[@id='tasks-tab']//div[@class='grid-canvas']";
        private string TaskRow = "./div[contains(@class, 'slick-row')]";
        private string TaskCheckboxCell = "./div[contains(@class, 'l0')]//input[@type='checkbox']";
        private string TaskULCell = "./div[contains(@class, 'l1')]//img";
        private string TaskStateImgCell = "./div[contains(@class, 'l2')]//img";
        private string TaskCreateDateCell = "./div[contains(@class, 'l3')]";
        private string TaskPartyCell = "./div[contains(@class, 'l4')]";
        private string TaskIDCell = "./div[contains(@class, 'l5')]";
        private string TaskStateCell = "./div[contains(@class, 'l6')]";
        private string TaskTypeCell = "./div[contains(@class, 'l11')]";
        private string TaskDescriptionCell = "./div[contains(@class, 'l12')]";
        private string TaskDueDateCell = "./div[contains(@class, 'l13')]";

        public readonly By TaskTypeSearch = By.XPath("//div[@id='tasks-tab']//div[contains(@class, 'slick-headerrow-column l11 r1')]//input");
        public readonly By ApplyBtn = By.XPath("//div[@id='tasks-tab']//button[@title='Apply Filters']");

        //DYNAMIC
        public readonly string firstTaskAfterGenerated = "(//div[contains(text(), '{0}')]/preceding-sibling::div/div[contains(text(), '{1}')]/parent::div/following-sibling::div[contains(@class, 'l9')]/a[text()='ServiceUnit'])[1]";

        private TableElement taskTableEle;
        public TableElement TaskTableEle
        {
            get => taskTableEle;
        }

        public TaskTab()
        {
            taskTableEle = new TableElement(TaskTable, TaskRow, new List<string>() { TaskCheckboxCell, TaskULCell, TaskStateImgCell, TaskCreateDateCell, TaskPartyCell, TaskIDCell, TaskStateCell, TaskTypeCell, TaskDescriptionCell, TaskDueDateCell });
            taskTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        [AllureStep]
        public TaskTab VerifyOnStopTaskState()
        {
            int count = TaskTableEle.GetRows().Count;
            for (int i = 0; i < count; i++)
            {
                string taskImageUrl = TaskTableEle.GetCell(i, TaskTableEle.GetCellIndex(TaskStateImgCell)).GetAttribute("background-image");
                if (taskImageUrl.Contains("task-onhold.png"))
                {
                    return this;
                }
            }
            return this;
        }

        [AllureStep]
        public TaskTab VerifyFirstTaskType(string expected)
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            Assert.AreEqual(expected, GetElementText(listTaskType[0]));
            return this;
        }
        [AllureStep]
        public TaskTab VerifyFirstTaskDueDate(string expected)
        {
            IList<IWebElement> listTaskDueDate = WaitUtil.WaitForAllElementsVisible(taskDueDate);
            Assert.IsTrue(GetElementText(listTaskDueDate[0]).Contains(expected), "Incorrect due date. Expected " + listTaskDueDate[0].Text + " to contain " + expected);
            return this;
        }
        [AllureStep]
        public TaskTab VerifySecondTaskType(string expected)
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            Assert.AreEqual(expected, GetElementText(listTaskType[1]));
            return this;
        }
        [AllureStep]
        public TaskTab VerifySecondTaskDueDate(string expected)
        {
            IList<IWebElement> listTaskDueDate = WaitUtil.WaitForAllElementsVisible(taskDueDate);
            Assert.IsTrue(GetElementText(listTaskDueDate[1]).Contains(expected));
            return this;
        }
        [AllureStep]
        public TaskTab OpenFirstTask()
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            DoubleClickOnElement(listTaskType[0]);
            return this;
        }
        [AllureStep]
        public TaskTab OpenSecondTask()
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            DoubleClickOnElement(listTaskType[1]);
            return this;
        }
        [AllureStep]
        public TaskTab ClickTaskTabBtn()
        {
            ClickOnElement(taskTabBtn);
            return this;
        }
        [AllureStep]
        public TaskTab VerifyRetiredTask(int num)
        {
            this.WaitForLoadingIconToDisappear();
            int i = 15;

            List<IWebElement> taskList = new List<IWebElement>();
            taskList = GetAllElements(retiredTasks);
            while (i > 0)
            {
                if (taskList.Count == num)
                {
                    Assert.AreEqual(taskList.Count, num);
                    break;
                }
                else
                {
                    ClickOnElement(refreshBtn);
                    this.WaitForLoadingIconToDisappear();
                    Thread.Sleep(5000);
                    taskList.Clear();
                    taskList = GetAllElements(retiredTasks);
                    i--;
                }
            }
            return this;
        }
        
        [AllureStep]
        public List<IWebElement> VerifyNewDeliverCommercialBin(String dueDate, int num)
        {
            this.WaitForLoadingIconToDisappear();
            int i = 15;
            deliverCommercialBinWithDateRows = String.Format(deliverCommercialBinWithDateRows, dueDate);
            
            while (i > 0)
            {
                if (IsControlUnDisplayed(deliverCommercialBinWithDateRows))
                {
                    ClickOnElement(refreshBtn);
                    this.WaitForLoadingIconToDisappear();
                    Thread.Sleep(5000);
                    i--;
                }
                else
                {
                    if(GetAllElements(deliverCommercialBinWithDateRows).Count == num) {
                        break;
                    }
                    else {
                        ClickOnElement(refreshBtn);
                        this.WaitForLoadingIconToDisappear();
                        Thread.Sleep(5000);
                        i--;
                    }
                }
            }
            List<IWebElement> taskList = GetAllElements(deliverCommercialBinWithDateRows);
            Assert.AreEqual(taskList.Count, num);
            return taskList;
        }
        [AllureStep]
        public List<IWebElement> VerifyNewRemovedCommercialBin(String dueDate, int num)
        {
            this.WaitForLoadingIconToDisappear();
            int i = 15;
            removeCommercialBinWithDateRows = String.Format(removeCommercialBinWithDateRows, dueDate);
            while (i > 0)
            {
                if (IsControlUnDisplayed(removeCommercialBinWithDateRows))
                {
                    ClickOnElement(refreshBtn);
                    this.WaitForLoadingIconToDisappear();
                    Thread.Sleep(5000);
                    i--;
                }
                else
                {
                    break;
                }
            }
            List<IWebElement> taskList = GetAllElements(removeCommercialBinWithDateRows);
            return taskList;
        }
        [AllureStep]
        public AgreementTaskDetailsPage GoToATask(IWebElement e)
        {
            SleepTimeInMiliseconds(1000);
            DoubleClickOnElement(e);
            return new AgreementTaskDetailsPage();
        }
        [AllureStep]
        public AgreementTaskDetailsPage GoToATaskById(int id)
        {
            SleepTimeInMiliseconds(1000);
            DoubleClickOnElement(taskId, id.ToString());
            return new AgreementTaskDetailsPage();
        }
        [AllureStep]
        public TaskTab GoToFirstTask()
        {
            DoubleClickOnElement(firstTask);
            return this;
        }
        [AllureStep]
        public TaskTab GoToSecondTask()
        {
            DoubleClickOnElement(secondTask);
            return this;
        }

        [AllureStep]
        public int GetColumnIndexByColumnName(string name)
        {
            List<IWebElement> allTitles = GetAllElements(allColumnTitle);
            for (int i = 0; i < allTitles.Count; i++)
            {
                if (GetElementText(allTitles[i]) == name)
                {
                    return i + 1;
                }
            }
            return 0;
        }
        [AllureStep]
        public List<AgreementTaskModel> GetAllTaskInList()
        {
            List<AgreementTaskModel> list = new List<AgreementTaskModel>();

            List<IWebElement> allRow = GetAllElements(allRows);

            int createdDateIndex = this.GetColumnIndexByColumnName("Created Date");
            List<IWebElement> createdDates = GetAllElements(String.Format(eachColumn, createdDateIndex) + "/div");

            int stateIndex = this.GetColumnIndexByColumnName("State");
            List<IWebElement> states = GetAllElements(String.Format(eachColumn, stateIndex) + "//img");

            int idIndex = this.GetColumnIndexByColumnName("ID");
            List<IWebElement> ids = GetAllElements(String.Format(eachColumn, idIndex) + "/div");

            int taskStateIndex = this.GetColumnIndexByColumnName("Task State");
            List<IWebElement> taskStates = GetAllElements(String.Format(eachColumn, taskStateIndex));

            int taskTypeIndex = this.GetColumnIndexByColumnName("Task Type");
            List<IWebElement> taskTypes = GetAllElements(String.Format(eachColumn, taskTypeIndex));

            int descriptionIndex = this.GetColumnIndexByColumnName("Description");
            List<IWebElement> descriptions = GetAllElements(String.Format(eachColumn, descriptionIndex));

            int dueDateIndex = this.GetColumnIndexByColumnName("Due Date");
            List<IWebElement> dueDates = GetAllElements(String.Format(eachColumn, dueDateIndex));

            int completedDateIndex = this.GetColumnIndexByColumnName("Completed Date");
            List<IWebElement> completedDates = GetAllElements(String.Format(eachColumn, completedDateIndex));

            for (int i = 0; i < allRow.Count; i++)
            {
                string state = GetAttributeValue(states[i], "title");
                string id = GetElementText(ids[i]);
                string taskState = GetElementText(taskStates[i]);
                string taskType = GetElementText(taskTypes[i]);
                string description = GetElementText(descriptions[i]);
                string createdDate;
                if (GetElementText(createdDates[i]).Length > 10)
                {
                    createdDate = GetElementText(createdDates[i]).Substring(0, 10);
                }
                else { createdDate = GetElementText(createdDates[i]); }
                string dueDate;
                if (GetElementText(dueDates[i]).Length > 10)
                {
                    dueDate = GetElementText(dueDates[i]).Substring(0, 10);
                }
                else { dueDate = GetElementText(dueDates[i]); }
                string completedDate;
                if (GetElementText(completedDates[i]).Length > 10)
                {
                    completedDate = GetElementText(completedDates[i]).Substring(0, 10);
                }
                else { completedDate = GetElementText(completedDates[i]); }
                AgreementTaskModel task = new AgreementTaskModel(createdDate, state, id, taskState, taskType, description, dueDate, completedDate);
                list.Add(task);
            }
            return list;
        }
        [AllureStep]
        public List<IWebElement> GetTasksAppear(string taskType, string dueDate)
        {
            List<AgreementTaskModel> list = this.GetAllTaskInList();
            List<IWebElement> allRow = GetAllElements(allRows);
            List<IWebElement> availableRow = new List<IWebElement>();

            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].TaskType == taskType && list[i].DueDate == dueDate)
                {
                    num++;
                    availableRow.Add(allRow[i]);
                }
            }
            return availableRow;
        }
        [AllureStep]
        public List<IWebElement> GetTasksAppear(string taskState, string createdDate, string dueDate)
        {
            List<AgreementTaskModel> list = this.GetAllTaskInList();
            List<IWebElement> allRow = GetAllElements(allRows);
            List<IWebElement> availableRow = new List<IWebElement>();

            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].TaskState == taskState && list[i].DueDate == dueDate && list[i].CreatedDate == createdDate)
                {
                    num++;
                    availableRow.Add(allRow[i]);
                }
            }
            return availableRow;
        }
        [AllureStep]
        public List<IWebElement> GetTasksAppear(string taskState, string taskType, string dueDate, string completedDate)
        {
            List<AgreementTaskModel> list = this.GetAllTaskInList();

            List<IWebElement> allRow = GetAllElements(allRows);
            List<IWebElement> availableRow = new List<IWebElement>();
            int num = 0;
            for (int i = 0; i < allRow.Count; i++)
            {
                if (list[i].TaskType == taskType && list[i].DueDate == dueDate
                    && list[i].TaskState == taskState && list[i].CompletedDate == completedDate)
                {
                    num++;
                    availableRow.Add(allRow[i]);
                }
            }
            return availableRow;
        }

        [AllureStep]
        public ServiceUnitDetailPage ClickOnFirstServiceUnitAfterGenerated(string taskTypeValue, string createdDateValue)
        {
            ClickOnElement(string.Format(firstTaskAfterGenerated, taskTypeValue, createdDateValue));
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            return PageFactoryManager.Get<ServiceUnitDetailPage>();
        }

        [AllureStep]
        public List<IWebElement> VerifyNewTaskAppearWithNum(int num, string taskState, string taskType, string dueDate, string completedDate)
        {
            ClickOnElement(refreshBtn);
            WaitForLoadingIconToDisappear();
            Thread.Sleep(10000);
            int i = 10;
            while (i > 0)
            {
                
                if (GetTasksAppear(taskState, taskType, dueDate, completedDate).Count == num)
                {
                    break; 
                }
                else
                {
                    ClickOnElement(refreshBtn);
                    WaitForLoadingIconToDisappear();
                    Thread.Sleep(10000);
                    i--;
                }
            }
            List<IWebElement> availableRow = GetTasksAppear(taskState, taskType, dueDate, completedDate);
            Assert.AreEqual(availableRow.Count, num);
            return availableRow;
        }
        [AllureStep]
        public int GetAllTaskNum()
        {
            IList<IWebElement> all = WaitUtil.WaitForAllElementsVisible(allRows);
            return all.Count;
        }
        [AllureStep]
        public TaskTab VerifyNoNewTaskAppear()
        {
            Assert.IsTrue(IsControlUnDisplayed(allRows));
            return this;
        }
        [AllureStep]
        public TaskTab VerifyTaskNumUnchange(int before, int after)
        {
            Assert.AreEqual(before, after);
            return this;
        }

        //Task
        [AllureStep]
        public TaskTab VerifyTaskAppearWithID(int id)
        {
            Assert.IsTrue(IsControlDisplayed(taskId, id.ToString()));
            return this;
        }
        [AllureStep]
        public TaskTab VerifyTaskDisappearWithID(int id)
        {
            int i = 5;
            while(i > 0)
            {
                if(IsControlUnDisplayed(taskId, id.ToString()))
                {
                    break;
                }
                else
                {
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
            }
            Assert.IsTrue(IsControlUnDisplayed(taskId, id.ToString()));
            return this;
        }
        [AllureStep]
        public TaskTab SelectATask(int id)
        {
            ClickOnElement(taskId, id.ToString());
            return this;
        }
        [AllureStep]
        public TaskTab SelectMultipleTask(int[] id)
        {
            foreach(int task in id)
            {
                ClickOnElement(taskIdCheckBox, task.ToString());
            }
            return this;
        }
        //Bulk Update
        [AllureStep]
        public BulkUpdatePage ClickBulkUpdateItem()
        {
            ClickOnElement(bulkUpdateItemBtn);
            return new BulkUpdatePage();
        }

        //Verify Retired Task
        [AllureStep]
        public TaskTab VerifyRetiredTaskWithId(int id)
        {
            int i = 5;
            while (i > 0)
            {
                if(IsControlUnDisplayed(retiredTaskWithId, id.ToString())){
                    ClickRefreshBtn();
                    WaitForLoadingIconToDisappear();
                    SleepTimeInMiliseconds(7000);
                    i--;
                }
                else
                {
                    break;
                }
            }
            Assert.IsTrue(IsControlDisplayed(retiredTaskWithId, id.ToString()));
            return this;
        }
        [AllureStep]
        public TaskTab VerifyRetiredTaskWithIds(int[] id)
        {
            foreach(int i in id)
            {
                this.VerifyRetiredTaskWithId(i);
            }
            return this;
        }
        [AllureStep]
        public TaskTab VerifyTaskStateWithIds(int[] idList, string state)
        {
            List<AgreementTaskModel> listTasks = this.GetAllTaskInList();
            int n = 0;
            for (int j = 0; j < idList.Length; j++)
            {

                int id = idList[j];
                for (int i = 0; i < listTasks.Count; i++)
                {
                    if ((listTasks[i].Id).Equals(id.ToString()))
                    {
                        Assert.AreEqual(listTasks[i].Id, id.ToString());
                        Assert.AreEqual(listTasks[i].State, state);
                        n++;
                        break;
                    }
                }
            }
            Assert.AreEqual(n, idList.Length);
            return this;
        }
        [AllureStep]
        public int getFirstTaskId()
        {
            int firstId = int.Parse(GetElementText(firstTaskId));
            return firstId;
        }
        [AllureStep]
        public int getSecondTaskId()
        {
            int firstId = int.Parse(GetElementText(secondTaskId));
            return firstId;
        }
        [AllureStep]

        public TaskTab VerifyTaskDataType(string taskType)
        {
            List<IWebElement> rows = TaskTableEle.GetRows();
            for (int i = 0; i < rows.Count; i++)
            {
                VerifyCellValue(TaskTableEle, i, 7, taskType);
            }
            return this;
        }
        [AllureStep]
        public TaskTab VerifyTaskDueDate(string dueDate)
        {
            List<IWebElement> rows = TaskTableEle.GetRows();
            for (int i = 0; i < rows.Count; i++)
            {
                VerifyCellValue(TaskTableEle, i, 9, dueDate);
            }
            return this;
        }
        [AllureStep]
        public TaskTab DoubleClickTask(int rowIdx)
        {
            TaskTableEle.DoubleClickRow(rowIdx);
            return this;
        }
        [AllureStep]
        public int getThirdTaskId()
        {
            int firstId = int.Parse(GetElementText(thirdTaskId));
            return firstId;
        }
        [AllureStep]
        public int getFourthTaskId()
        {
            int firstId = int.Parse(GetElementText(fourthTaskId));
            return firstId;
        }

        [AllureStep]
        public TaskTab IsTaskTabLoaded()
        {
            WaitUtil.WaitForAllElementsVisible("//div[@id='tasks-tab']//div[@class='grid-canvas']/div");
            return this;
        }


        [AllureStep]
        public TaskTab VerifyDisplayVerticalScrollBarTasksTab()
        {
            VerifyDisplayVerticalScrollBar(containerTasksTab);
            return this;
        }
    }

}
