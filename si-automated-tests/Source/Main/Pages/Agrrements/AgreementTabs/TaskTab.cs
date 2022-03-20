using System;
using System.Collections.Generic;
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
        private string retiredTasks = "//div[@class='grid-canvas']/div[contains(@class,'retired')]";


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

        public TaskTab VerifyTwoNewTaskAppearRemove()
        {

            return this;
        }

        public List<IWebElement> VerifyNewDeliverCommercialBin(String dueDate, int num)
        {
            this.WaitForLoadingIconToDisappear();
            int i = 10;
            deliverCommercialBinWithDateRows = String.Format(deliverCommercialBinWithDateRows, dueDate);
            List<IWebElement> taskList = new List<IWebElement>();
            taskList = GetAllElements(deliverCommercialBinWithDateRows);
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
                    taskList = GetAllElements(deliverCommercialBinWithDateRows);
                    i--;
                }
            }
            return taskList;
        }
        public AgreementTaskDetailsPage GoToATask(IWebElement e)
        {
            DoubleClickOnElement(e);
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
                string taskState = GetElementText(taskStates[i]);
                string taskType = GetElementText(taskTypes[i]);
                string description = GetElementText(descriptions[i]);
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
                AgreementTaskModel task = new AgreementTaskModel(taskState, taskType, description, dueDate, completedDate);
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
            List<IWebElement> availableRow = new List<IWebElement>();
            int i = 3;
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
                    Thread.Sleep(5000);
                    i--;
                }
            }
            Assert.AreEqual(availableRow.Count, num);
            return availableRow;
        }
        

    }
}
