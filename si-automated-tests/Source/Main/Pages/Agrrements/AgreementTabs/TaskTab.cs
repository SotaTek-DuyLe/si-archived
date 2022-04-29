using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs
{
    public class TaskTab : BasePage
    {
        private readonly By deleteItemBtn = By.XPath("//button[text()='Delete Item']");
        private readonly By bulkUpdateItemBtn = By.XPath("//button[text()='Bulk Update']");

        private readonly By taskType = By.XPath("//div[@class='slick-cell l11 r11']");
        private readonly By taskDueDate = By.XPath("//div[@class='slick-cell l13 r13']");

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
        private string retiredTaskWithId = "//div[text()='{0}']/parent::div/parent::div[contains(@class,'retired')]";
        public TaskTab VerifyFirstTaskType(string expected)
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            Assert.AreEqual(expected, GetElementText(listTaskType[0]));
            return this;
        }
        public TaskTab VerifyFirstTaskDueDate(string expected)
        {
            IList<IWebElement> listTaskDueDate = WaitUtil.WaitForAllElementsVisible(taskDueDate);
            Assert.IsTrue(GetElementText(listTaskDueDate[0]).Contains(expected));
            return this;
        }
        public TaskTab VerifySecondTaskType(string expected)
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            Assert.AreEqual(expected, GetElementText(listTaskType[1]));
            return this;
        }
        public TaskTab VerifySecondTaskDueDate(string expected)
        {
            IList<IWebElement> listTaskDueDate = WaitUtil.WaitForAllElementsVisible(taskDueDate);
            Assert.IsTrue(GetElementText(listTaskDueDate[1]).Contains(expected));
            return this;
        }
        public TaskTab OpenFirstTask()
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            DoubleClickOnElement(listTaskType[0]);
            return this;
        }
        public TaskTab OpenSecondTask()
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            DoubleClickOnElement(listTaskType[1]);
            return this;
        }

        public TaskTab ClickTaskTabBtn()
        {
            ClickOnElement(taskTabBtn);
            return this;
        }
        public TaskTab VerifyRetiredTask(int num)
        {
            this.WaitForLoadingIconToDisappear();
            int i = 5;

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
        public TaskTab VerifyTwoNewTaskAppear()
        {
            this.WaitForLoadingIconToDisappear();
            int i = 10;
            while (i > 0)
            {
                if (GetElementText(firstTask).Equals("Deliver Commercial Bin") && GetElementText(secondTask).Equals("Deliver Commercial Bin"))
                {
                    Assert.AreEqual(GetElementText(firstTask), "Deliver Commercial Bin");
                    Assert.AreEqual(GetElementText(secondTask), "Deliver Commercial Bin");
                    String tomorrowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1).Replace('-', '/');
                    String firstDueDate = GetElementText(firstTask + dueDateColumn).Substring(0, 10);
                    String secondDueDate = GetElementText(secondTask + dueDateColumn).Substring(0, 10);
                    //verify created date is tommorrow
                    Assert.AreEqual(tomorrowDate, firstDueDate);
                    Assert.AreEqual(tomorrowDate, secondDueDate);
                    break;
                }
                else
                {
                    ClickOnElement(refreshBtn);
                    this.WaitForLoadingIconToDisappear();
                    Thread.Sleep(20000);
                    i--;
                }
            }

            return this;
        }

        public List<IWebElement> VerifyNewDeliverCommercialBin(String dueDate, int num)
        {
            this.WaitForLoadingIconToDisappear();
            int i = 10;
            deliverCommercialBinWithDateRows = String.Format(deliverCommercialBinWithDateRows, dueDate);
            
            while(i > 0)
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
                    break;
                }
            }
            List<IWebElement> taskList = GetAllElements(deliverCommercialBinWithDateRows);
            return taskList;
        }
        public List<IWebElement> VerifyNewRemovedCommercialBin(String dueDate, int num)
        {
            this.WaitForLoadingIconToDisappear();
            int i = 5;
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
        public AgreementTaskDetailsPage GoToATask(IWebElement e)
        {
            SleepTimeInMiliseconds(1000);
            DoubleClickOnElement(e);
            return new AgreementTaskDetailsPage();
        }
        public AgreementTaskDetailsPage GoToATaskById(int id)
        {
            SleepTimeInMiliseconds(1000);
            DoubleClickOnElement(taskId, id.ToString());
            return new AgreementTaskDetailsPage();
        }
        public TaskTab GoToFirstTask()
        {
            DoubleClickOnElement(firstTask);
            return this;
        }

        public TaskTab GoToSecondTask()
        {
            DoubleClickOnElement(secondTask);
            return this;
        }
        

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
        public List<IWebElement> VerifyNewTaskAppearWithNum(int num, string taskState, string taskType, string dueDate, string completedDate)
        {
            ClickOnElement(refreshBtn);
            WaitForLoadingIconToDisappear();
            Thread.Sleep(5000);
            List<IWebElement> availableRow = new List<IWebElement>();
            int i = 5;
            while (i > 0)
            {
                availableRow = GetTasksAppear(taskState, taskType, dueDate, completedDate);
                if (availableRow.Count == num)
                {
                    return availableRow; 
                }
                else
                {
                    availableRow.Clear();
                    ClickOnElement(refreshBtn);
                    WaitForLoadingIconToDisappear();
                    Thread.Sleep(10000);
                    i--;
                }
            }
            Assert.AreEqual(availableRow.Count, num);
            return availableRow;
        }
        
        public int GetAllTaskNum()
        {
            IList<IWebElement> all = WaitUtil.WaitForAllElementsVisible(allRows);
            return all.Count;
        }

        public TaskTab VerifyNoNewTaskAppear()
        {
            Assert.IsTrue(IsControlUnDisplayed(allRows));
            return this;
        }
        public TaskTab VerifyTaskNumUnchange(int before, int after)
        {
            Assert.AreEqual(before, after);
            return this;
        }
        
        //Task
        public TaskTab VerifyTaskAppearWithID(int id)
        {
            Assert.IsTrue(IsControlDisplayed(taskId, id.ToString()));
            return this;
        }
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
        public TaskTab SelectATask(int id)
        {
            ClickOnElement(taskId, id.ToString());
            return this;
        }
        public TaskTab SelectMultipleTask(int[] id)
        {
            foreach(int task in id)
            {
                ClickOnElement(taskIdCheckBox, task.ToString());
            }
            return this;
        }

        //Delete Item
        public RemoveTaskPage ClickDeleteItem()
        {
            ClickOnElement(deleteItemBtn);
            return new RemoveTaskPage();
        }
        //Bulk Update
        public BulkUpdatePage ClickBulkUpdateItem()
        {
            ClickOnElement(bulkUpdateItemBtn);
            return new BulkUpdatePage();
        }

        //Verify Retired Task
        public TaskTab VerifyRetiredTaskWithId(int id)
        {
            WaitUtil.WaitForElementVisible(retiredTaskWithId, id.ToString());
            Assert.IsTrue(IsControlDisplayed(retiredTaskWithId, id.ToString()));
            return this;
        }
        public TaskTab VerifyRetiredTaskWithIds(int[] id)
        {
            foreach(int i in id)
            {
                this.VerifyRetiredTaskWithId(i);
            }
            return this;
        }
        public TaskTab VerifyTaskStateWithIds(int[] idList, string state)
        {
            List<AgreementTaskModel> listTasks = this.GetAllTaskInList();
            int n = 0;
            for(int j = 0; j < idList.Length; j++)
            { 
                
                int id = idList[j];
                for(int i = 0; i < listTasks.Count; i++)
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
    }

}
