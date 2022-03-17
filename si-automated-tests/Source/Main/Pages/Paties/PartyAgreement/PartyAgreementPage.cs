using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.PartyAgreement.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.PartyAgreement
{
    //Party Agreement Detail Page
    //Can be opened through Party Detail Page -> Agreement Tab -> Double click one agreement
    //Or Agreements Main Page -> Double click one agreement
    public class PartyAgreementPage : BasePage
    {
        private readonly By detailsTabBtn = By.XPath("//a[@aria-controls='details-tab']");
        private readonly By saveBtn = By.XPath("//button[@title='Save']");
        private readonly By saveAndCloseBtn = By.XPath("//button[@title='Save and Close']");
        private readonly By closeWithoutSavingBtn = By.XPath("//a[@aria-controls='details-tab']/ancestor::body//button[@title='Close Without Saving']");
        private readonly By closeBtn = By.XPath("//button[@title='Close Without Saving']");
        private readonly By status = By.XPath("//div[@title='Agreement Status']");
        private string agreementStatus = "//div[@title='Agreement Status']//span[text()='{0}']";

        private readonly By agreementTypeInput = By.Id("agreement-type");
        private readonly By startDateInput = By.Id("start-date");
        private readonly By endDateInput = By.Id("end-date");
        private readonly By primaryContract = By.Id("primary-contact");
        private readonly By invoiceContact = By.Id("invoice-contact");
        private readonly By correspondenceAddressInput = By.Id("correspondence-address");
        private readonly By invoiceAddressInput = By.Id("invoice-address");
        private readonly By invoiceScheduleInput = By.Id("invoice-schedule");
        private readonly By paymentMethodInput = By.Id("payment-method");
        private readonly By vatCodeInput = By.Id("vat-code");
        private readonly By serviceInput = By.Id("search");

        private readonly By addServiceBtn = By.XPath("//button[text()='Add Services']");
        private readonly By approveBtn = By.XPath("//button[text()='Approve']");
        private readonly By cancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By confirmApproveBtn = By.XPath("//button[@data-bb-handler='confirm']");

        //Agreement line panel
        private readonly By serviceAgreementPanel = By.XPath("//div[@class='panel panel-default' and contains(@data-bind,'agreementLines')]");
        private readonly By serviceStartDate = By.XPath("//div[@class='panel panel-default' and contains(@data-bind,'agreementLines')]/descendant::span[contains(@data-bind,'displayStartDate')]");
        private readonly By panelSiteAddress = By.XPath("//p[contains(@data-bind,'siteAddress')]");
        private readonly By expandBtn = By.XPath("//button[@title='Expand/close agreement line']");
        private readonly By subExpandBtns = By.XPath("//div[contains(@class,'panel-heading clickable')]");
        private readonly By editBtn = By.XPath("//button[text()='Edit']");
        private readonly By assetAndProductAssetTypeStartDate = By.XPath("//tbody[contains(@data-bind,'assetProducts')]//span[@title='Start Date']");
        private readonly By regularAssertTypeStartDate = By.XPath("//span[text()='Regular']/ancestor::div[1]/following-sibling::div//span[contains(@data-bind,'displayStartDate')]");
        private readonly By serviceTaskLineTypeStartDates = By.XPath("//th[text()='Task Line Type']/ancestor::thead[1]/following-sibling::tbody//span[@title='Start Date']");

        //Summary title
        private readonly By startDate = By.XPath("//span[@title='Start Date']");
        private readonly By endDate = By.XPath("//span[@title='End Date']");

        //Tabs
        private readonly By taskTab = By.XPath("//a[text()='Tasks']");

        //Task Tab locator 
        private readonly By taskTabBtn = By.XPath("//a[@aria-controls='tasks-tab']");
        private readonly By refreshBtn = By.XPath("//button[@title='Refresh']");
        private string firstTask = "(//div[text()='Deliver Commercial Bin'])[2]";
        private string secondTask = "(//div[text()='Deliver Commercial Bin'])[1]";
        private string dueDateColumn = "/following-sibling::div[2]";
        private string taskTypeColumn = "//div[contains(@class,'r11')]";
        private string allColumnTitle = "//div[contains(@class, 'slick-header-columns')]/div/span[1]";
        private string eachColumn = "//div[@class='grid-canvas']/div/div[{0}]";
        private string allRows = "//div[@class='grid-canvas']/div";
        private string deliverCommercialBinWithDateRows = "//div[@class='grid-canvas']/div[contains(.,'Deliver Commercial Bin') and contains(.,'{0}')]";
        private string retiredTasks = "//div[@class='grid-canvas']/div[contains(@class,'retired')]";

        private readonly By createAdhocBtn = By.XPath("//button[text()='Create Ad-Hoc Task']");

        //Agreement Tab locator
        private readonly By agreementTabBtn = By.XPath("//a[@aria-controls='agreements-tab']");
        private string agreementWithDate = "//div[text()='{0}']";

        public PartyAgreementPage ClickOnDetailsTab()
        {
            WaitUtil.WaitForElementClickable(detailsTabBtn);
            ClickOnElement(detailsTabBtn);
            return this;
        }
        public PartyAgreementPage SleepForLongTime()
        {
            Thread.Sleep(10000);
            return this;
        }
        public PartyAgreementPage CloseWithoutSaving()
        {
            WaitUtil.WaitForElementClickable(closeWithoutSavingBtn);
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }
        public PartyAgreementPage IsOnPartyAgreementPage()
        {
            WaitUtil.WaitForElementVisible(agreementTypeInput);
            WaitUtil.WaitForElementVisible(startDateInput);
            WaitUtil.WaitForElementVisible(endDateInput);
            WaitUtil.WaitForElementVisible(primaryContract);
            WaitUtil.WaitForElementVisible(invoiceContact);
            WaitUtil.WaitForElementVisible(correspondenceAddressInput);
            WaitUtil.WaitForElementVisible(invoiceAddressInput);
            WaitUtil.WaitForElementVisible(invoiceScheduleInput);
            WaitUtil.WaitForElementVisible(paymentMethodInput);
            WaitUtil.WaitForElementVisible(vatCodeInput);
            WaitUtil.WaitForElementVisible(serviceInput);
            return this;
        }
        public PartyAgreementPage SelectAgreementType(string text)
        {
            SelectTextFromDropDown(agreementTypeInput, text);
            return this;
        }
        public PartyAgreementPage VerifyStartDateIsCurrentDate()
        {
            Assert.AreEqual(CommonUtil.GetLocalTimeNow("dd/MM/yyyy"), WaitUtil.WaitForElementVisible(startDate).Text);
            return this;
        }
        public PartyAgreementPage VerifyStartDateIs(string date)
        {
            Assert.AreEqual(date, WaitUtil.WaitForElementVisible(startDate).Text);
            return this;
        }
        public PartyAgreementPage VerifyAllStartDate(string date)
        {
            IList<IWebElement> startDates = WaitUtil.WaitForAllElementsVisible(startDate);
            foreach(var startDate in startDates)
            {
                Assert.AreEqual(date, startDate.Text);
            }
            
            return this;
        }
        public PartyAgreementPage VerifyEndDateIsPredefined()
        {
            Assert.AreEqual("01/01/2050", WaitUtil.WaitForElementVisible(endDate).Text);
            return this;
        }
        public PartyAgreementPage EnterStartDate(string startDate)
        {
            SendKeys(startDateInput, startDate);
            return this;
        }
        public string GetAgreementStatus()
        {
            return WaitUtil.WaitForElementVisible(status).Text;
        }
        public PartyAgreementPage VerifyAgreementStatus(string _status)
        {
            WaitUtil.WaitForTextVisibleInElement(status, _status);
            return this;
        }
        public PartyAgreementPage VerifyAgreementStatusWithText(string _status)
        {
            Thread.Sleep(10000);
            WaitUtil.WaitForElementInvisible(agreementStatus, _status);
            Assert.IsTrue(IsControlDisplayed(agreementStatus, _status));
            return this;
        }
        public PartyAgreementPage VerifyNewOptionsAvailable()
        {
            WaitUtil.WaitForElementVisible(addServiceBtn);
            WaitUtil.WaitForElementVisible(approveBtn);
            WaitUtil.WaitForElementVisible(cancelBtn);
            return this;
        }
        public DetailPartyPage ClosePartyAgreementPage()
        {
            ClickOnElement(closeBtn);
            return PageFactoryManager.Get<DetailPartyPage>();
        }
        public PartyAgreementPage ClickApproveAgreement()
        {
            ScrollDownToElement(approveBtn);
            ClickOnElement(approveBtn);
            return this;
        }
        public PartyAgreementPage ConfirmApproveBtn()
        {
            WaitUtil.WaitForElementClickable(confirmApproveBtn);
            ClickOnElement(confirmApproveBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public AddServicePage ClickAddService()
        {
            ClickOnElement(addServiceBtn);
            return PageFactoryManager.Get<AddServicePage>();
        }
        public PartyAgreementPage VerifyServicePanelPresent()
        {
            WaitUtil.WaitForElementVisible(serviceAgreementPanel);
            return this;
        }
        public PartyAgreementPage VerifyServiceSiteAddres(string add)
        {
            Assert.IsTrue(add.Contains(GetElementText(panelSiteAddress)));
            return this;
        }
        public PartyAgreementPage VerifyServiceStartDate(string startDate)
        {
            Assert.AreEqual(startDate, GetElementText(serviceStartDate));
            return this;
        }
        public PartyAgreementPage VerifyTaskLineTypeStartDates(string startDate)
        {
            Assert.AreEqual(startDate, GetElementText(serviceTaskLineTypeStartDates));
            IList<IWebElement> elements = WaitUtil.WaitForAllElementsVisible(serviceTaskLineTypeStartDates);
            foreach (IWebElement element in elements)
            {
                Assert.AreEqual(startDate, GetElementText(element));
            }
            return this;
        }
        public PartyAgreementPage VerifyRegularAssetTypeStartDate(string startDate)
        {
            ScrollDownToElement(regularAssertTypeStartDate);
            Assert.AreEqual(startDate, GetElementText(regularAssertTypeStartDate));
            return this;
        }
        public PartyAgreementPage VerifyAssetAndProductAssetTypeStartDate(string startDate)
        {
            
            Assert.AreEqual(startDate, GetElementText(assetAndProductAssetTypeStartDate));
            return this;
        }
        public PartyAgreementPage ExpandAllAgreementFields()
        {
            IList<IWebElement> fields = WaitUtil.WaitForAllElementsVisible(subExpandBtns);
            foreach(var field in fields)
            {
                Thread.Sleep(300);
                field.Click();
            }
            return this;
        }
        public PartyAgreementPage ExpandAgreementLine()
        {
            ClickOnElement(expandBtn);
            return this;
        }
        public TaskTab OpenTaskTab()
        {
            ClickOnElement(taskTab);
            return PageFactoryManager.Get<TaskTab>();
        }

        public PartyAgreementPage ClickEditAgreementBtn()
        {
            ScrollDownToElement(editBtn);
            ClickOnElement(editBtn);
            return this;
        }

        //Task tab
        public PartyAgreementPage ClickTaskTabBtn()
        {
            WaitUtil.WaitForElementClickable(taskTabBtn);
            ClickOnElement(taskTabBtn);
            return this;
        }
        public PartyAgreementPage VerifyRetiredTask(int num)
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
        public PartyAgreementPage VerifyTwoNewTaskAppear()
        {
            this.WaitForLoadingIconToDisappear();
            int i = 10;
            while (i > 0)
            {
                if(GetElementText(firstTask).Equals("Deliver Commercial Bin") && GetElementText(secondTask).Equals("Deliver Commercial Bin"))
                {
                    Assert.AreEqual(GetElementText(firstTask), "Deliver Commercial Bin");
                    Assert.AreEqual(GetElementText(secondTask), "Deliver Commercial Bin");
                    String tomorrowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1).Replace('-', '/');
                    String firstDueDate = GetElementText(firstTask + dueDateColumn).Substring(0,10);
                    String secondDueDate = GetElementText(secondTask + dueDateColumn).Substring(0,10);
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

        public PartyAgreementPage VerifyTwoNewTaskAppearRemove()
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
        public PartyAgreementPage GoToFirstTask()
        {
            DoubleClickOnElement(firstTask);
            return this;
        }

        public PartyAgreementPage GoToSecondTask()
        {
            DoubleClickOnElement(secondTask);
            return this;
        }
        public PartyAgreementPage VerifyCreateAdhocButtonsAreDisabled()
        {
            IList<IWebElement> createAdhocBtns = WaitUtil.WaitForAllElementsVisible(createAdhocBtn);
            foreach(var btn in createAdhocBtns)
            {
                Assert.AreEqual(false, btn.Enabled);
            }
            return this;
        }

        public int GetColumnIndexByColumnName(string name)
        {
            List <IWebElement> allTitles = GetAllElements(allColumnTitle);
            for(int i = 0 ; i< allTitles.Count; i++)
            {
                if(GetElementText(allTitles[i]) == name)
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
                if(GetElementText(dueDates[i]).Length > 10)
                {
                    dueDate = GetElementText(dueDates[i]).Substring(0,10);
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
            for (int i = 0; i < list.Count; i++)
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
        //public List<IWebElement> VerifyNewTaskAppearWithNum(string taskType, string dueDate, int num)
        //{
        //    ClickOnElement(refreshBtn);
        //    WaitForLoadingIconToDisappear();
        //    int i = 5;
        //    int n;
        //    while(i > 0)
        //    {
        //        n = GetTasksAppear(taskType, dueDate);
        //        if (n == num)
        //        {
        //            Assert.AreEqual(n, num);
        //            break;
        //        }
        //        else
        //        {
        //            ClickOnElement(refreshBtn);
        //            WaitForLoadingIconToDisappear();
        //            Thread.Sleep(5000);
        //        }
        //    }
        //    return this;
        //}
        public List<IWebElement> VerifyNewTaskAppearWithNum(int num, string taskState, string taskType, string dueDate, string completedDate)
        {
            ClickOnElement(refreshBtn);
            WaitForLoadingIconToDisappear();
            List<IWebElement> availableRow = GetTasksAppear(taskState, taskType, dueDate, completedDate);
            int i = 5;
            while (i > 0)
            {
                if (availableRow.Count == num)
                {
                    Assert.AreEqual(availableRow.Count, num);
                    break;
                }
                else
                {
                    availableRow.Clear();
                    ClickOnElement(refreshBtn);
                    WaitForLoadingIconToDisappear();
                    Thread.Sleep(5000);
                    availableRow = GetTasksAppear(taskState, taskType, dueDate, completedDate);
                    i--;
                }
            }
            return availableRow;
        }
        public List<IWebElement> GetNewTasklRowList()
        {
            List<IWebElement> newTasks = new List<IWebElement>();
            return newTasks;
        }
        //Agreement Tab
        public PartyAgreementPage ClickToAgreementTab()
        {
            ClickOnElement(agreementTabBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public PartyAgreementPage OpenAnAgreementWithDate(string date)
        {
            DoubleClickOnElement(agreementWithDate, date);
            return this;
        }
    }
}
