using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models.ServiceStatus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundInstanceForm : BasePageCommonActions
    {
        public RoundInstanceForm()
        {
            roundInstanceEventTableEle = new TableElement("//div[@id='roundInstanceEvents-tab']//div[@class='grid-canvas']", RoundInstanceEventRow, new List<string>() { RoundInstanceEventCheckbox, RoundInstanceEventId, RoundInstanceEventType, RoundInstanceEventResource });
            roundInstanceEventTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        public readonly By DropDownStatusButton = By.XPath("//button[@data-id='status']");
        public readonly By DropDownSelect = By.XPath("//ul[@class='dropdown-menu inner']");
        public readonly By WorksheetTab = By.XPath("//a[@aria-controls='worksheet-tab']");
        private readonly By containerWorksheetTab = By.XPath("//*[@id='grid']//div[@class='slick-viewport']");
        public readonly By EventsTab = By.XPath("//a[@aria-controls='roundInstanceEvents-tab']");
        public readonly By WorkSheetIframe = By.XPath("//iframe[@id='worksheet-tab']");
        private readonly By title = By.XPath("//h4[text()='Round Instance']");
        private readonly By idService = By.CssSelector("h4[title='Id']");
        private readonly By taskName = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-group') and not(contains(@style, 'display: none;'))]");


        #region WorkSheetTab
        public readonly By WorkSheetIFrame = By.XPath("//iframe[@id='worksheet-tab']");
        public readonly By ToggleRoundButton = By.XPath("//button[@id='t-toggle-rounds']");
        public readonly By ReallocateButton = By.XPath("//button[@id='t-bulk-reallocate']");
        private readonly By expandRoundBtn = By.XPath("//span[text()='Expand Rounds']/parent::button");
        private readonly By bulkUpdateBtn = By.XPath("//button[@id='t-bulk-confirm']");
        private readonly By firstCheckedBox = By.XPath("(//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured')]/div[contains(@class, 'selected')]/input)[1]");
        private readonly By selectAndUnSelectInput = By.XPath("//div[@title='Select/Deselect All']//input");

        private readonly string RLITable = "//div[@id='grid']//div[@class='grid-canvas']";
        private readonly string RLIRow = "./div[contains(@class, 'slick-row') and not(contains(@class,'slick-group'))]";
        private readonly string RLICheckboxCell = "./div[contains(@class, 'l0')]//input";
        private readonly string RLIIdCell = "./div[contains(@class, 'l3')]";
        private readonly string RLIDescriptionCell = "./div[contains(@class, 'l4')]";
        private readonly string RLIServiceCell = "./div[contains(@class, 'l5')]";
        private readonly By allTaskRows = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))]");
        private readonly By firstTaskRow = By.XPath("(//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))])[1]");
        private readonly string allIdRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l3 r3')]";
        private readonly string desciptionRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l4 r4')]";
        private readonly string serviceRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l5 r5')]";
        private readonly string partyRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l6 r6')]";
        private readonly string siteRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l7 r7')]";
        private readonly string streetRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l8 r8')]";
        private readonly string townRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l9 r9')]";
        private readonly string postcodeRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l10 r10')]";
        private readonly By idSearch = By.XPath("//div[contains(@id, 'grid')]//div[contains(@class, 'ui-state-default')]//div[contains(@class, 'l3')]//input[contains(@class, 'value')]");
        private readonly By AllFilteredHeaderInput = By.XPath("//div[@class='slick-headerrow-columns']//input");
        private readonly By statusFilterInputWSTab = By.XPath("//div[@id='grid']//div[contains(@class, 'l19')]/input");
        //DYNAMIC
        private readonly string checkboxRow = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured')][{0}]/div[contains(@class, 'l0 r0')]/input";
        private readonly string anyCheckedBox = "(//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured')]/div[contains(@class, 'selected')]/input)[{0}]";


        public TableElement RLITableEle
        {
            get => new TableElement(RLITable, RLIRow, new List<string>() { RLICheckboxCell, RLIIdCell, RLIDescriptionCell, RLIServiceCell });
        }

        [AllureStep]
        public List<string> SelectRLI(List<string> RLIIDs)
        {
            List<string> descriptions = new List<string>();
            foreach (var RLIID in RLIIDs)
            {
                IWebElement descriptionCell = RLITableEle.GetCellByCellValues(2, new Dictionary<int, object>() { { 1, RLIID } });
                descriptions.Add(descriptionCell.Text);
                RLITableEle.ClickCellOnCellValue(0, 1, RLIID);
            }
            return descriptions;
        }

        [AllureStep]
        public RoundInstanceForm VerifyRITableVisible()
        {
            VerifyElementVisibility(By.XPath(RLITable), true);
            return this;
        }

        public RoundInstanceForm VerifyNoFilterOnRITable()
        {
            var inputEles = driver.FindElements(AllFilteredHeaderInput);
            foreach (var inputEle in inputEles)
            {
                VerifyInputValue(inputEle, "");
            }
            return this;
        }
        #endregion

        #region Event Tab
        public readonly By AddNewEventItemButton = By.XPath("//div[@id='roundInstanceEvents-tab']//button[text()='Add New Item']");
        public readonly By RefreshButton = By.XPath("//div[@id='roundInstanceEvents-tab']//button[@title='Refresh']");
        public readonly string RoundInstanceEventRow = "./div[contains(@class, 'slick-row')]";
        public readonly string RoundInstanceEventCheckbox = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        public readonly string RoundInstanceEventId = "./div[contains(@class, 'slick-cell l1 r1')]";
        public readonly string RoundInstanceEventType = "./div[contains(@class, 'slick-cell l2 r2')]";
        public readonly string RoundInstanceEventResource = "./div[contains(@class, 'slick-cell l3 r3')]";

        private TableElement roundInstanceEventTableEle;
        public TableElement RoundInstanceEventTableEle
        {
            get => roundInstanceEventTableEle;
        }

        [AllureStep]
        public RoundInstanceForm VerifyNewRoundInstanceEventData(string eventType, string resource)
        {
            VerifyCellValue(RoundInstanceEventTableEle, 0, 2, eventType);
            VerifyCellValue(RoundInstanceEventTableEle, 0, 3, resource);
            return this;
        }
        #endregion

        [AllureStep]
        public RoundInstanceForm SelectStatus(string status)
        {
            SleepTimeInMiliseconds(300);
            IWebElement webElement = this.driver.FindElements(DropDownSelect).FirstOrDefault(x => x.Displayed);
            SelectByDisplayValueOnUlElement(webElement, status);
            return this;
        }

        [AllureStep]
        public RoundInstanceForm IsRoundInstanceForm(string serviceId)
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.AreEqual(serviceId, GetElementText(idService));
            return this;
        }

        [AllureStep]
        public RoundInstanceForm IsRoundInstanceForm()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            return this;
        }

        [AllureStep]
        public RoundInstanceForm ClickOnWorksheetTab()
        {
            ClickOnElement(WorksheetTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public RoundInstanceForm VerifyDisplayVerticalScrollBarWorksheetTab()
        {
            VerifyDisplayVerticalScrollBar(containerWorksheetTab);
            return this;
        }

        [AllureStep]
        public RoundInstanceForm ClickOnExpandRoundBtn()
        {
            ClickOnElement(expandRoundBtn);
            WaitUtil.WaitForPageLoaded();
            return this;
        }

        [AllureStep]
        public RoundInstanceForm WaitForAllTasksVisibled()
        {
            WaitUtil.WaitForAllElementsVisible(allTaskRows);
            return this;
        }
        [AllureStep]
        public List<TaskInWorksheetModel> GetAllTaskInWorksheetTab(int numberOfTasks)
        {
            List<TaskInWorksheetModel> taskInWorksheetModels = new List<TaskInWorksheetModel>();

            for(int i = 0; i < numberOfTasks; i++)
            {
                string checkboxLocator = string.Format(checkboxRow, (i + 1));
                string id = GetElementText(string.Format(allIdRows, (i + 1)));
                string desc = GetElementText(string.Format(desciptionRows, (i + 1)));
                string service = GetElementText(string.Format(serviceRows, (i + 1)));
                string party = GetElementText(string.Format(partyRows, (i + 1)));
                string site = GetElementText(string.Format(siteRows, (i + 1)));
                string street = GetElementText(string.Format(streetRows, (i + 1)));
                string town = GetElementText(string.Format(townRows, (i + 1)));
                string postcode = GetElementText(string.Format(postcodeRows, (i + 1)));
                taskInWorksheetModels.Add(new TaskInWorksheetModel(checkboxLocator, id, desc, service, party, site, street, town, postcode));
            }
            return taskInWorksheetModels;
        }
        [AllureStep]
        public RoundInstanceForm SelectTwoTaskInGrid(string firstTask, string secondTask)
        {
            ClickOnElement(firstTask);
            ClickOnElement(secondTask);
            return this;
        }
        [AllureStep]
        public RoundInstanceForm SelectOneTaskInGrid(string firstTask)
        {
            ClickOnElement(firstTask);
            return this;
        }
        [AllureStep]
        public RoundInstanceForm DoubleClickOneTaskInGrid(string firstTask)
        {
            DoubleClickOnElement(firstTask);
            return this;
        }
        [AllureStep]
        public RoundInstanceForm DoubleClickOnFirstTaskInGrid()
        {
            DoubleClickOnElement(firstTaskRow);
            return this;
        }
        [AllureStep]
        public RoundInstanceForm SrollLeftToCheckboxTask()
        {
            ScrollMaxToTheLeft(By.XPath("//div[@class='slick-viewport']"));
            return this;
        }
        [AllureStep]
        public RoundInstanceForm UncheckedFirstTask()
        {
            ClickOnElement(firstCheckedBox);
            return this;
        }
        [AllureStep]
        public RoundInstanceForm UncheckedAnyTask(string index)
        {
            ClickOnElement(anyCheckedBox, index);
            WaitUtil.WaitForPageLoaded();
            return this;
        }
        [AllureStep]
        public RoundInstanceForm ClickOnSelectAllBtn()
        {
            ClickOnElement(selectAndUnSelectInput);
            return this;
        }
        [AllureStep]
        public RoundInstanceForm ClickReallocateBtn()
        {
            ClickOnElement(ReallocateButton);
            return this;
        }
        [AllureStep]
        public RoundInstanceForm ClickOnBulkUpdateBtn()
        {
            ClickOnElement(bulkUpdateBtn);
            return this;
        }

        [AllureStep]
        public string GetTaskName()
        {
            string taskNameDisplayed = GetElementText(taskName);
            return taskNameDisplayed.Split(Environment.NewLine)[0].Replace(":", " ").Split("(")[0].Trim();
        }
        [AllureStep]
        public RoundInstanceForm SendKeyInId(string taskId)
        {
            SendKeys(idSearch, taskId);
            return this;
        }
        [AllureStep]
        public RoundInstanceForm VerifyTaskNoLongerDisplayedInGrid()
        {
            Assert.IsTrue(IsControlUnDisplayed(allTaskRows));
            return this;
        }

        [AllureStep]
        public RoundInstanceForm FilterTaskByStatusInWorksheetTab(string statusValue)
        {
            ScrollRightOffset(By.XPath("//div[@class='slick-viewport']"));

            SendKeys(statusFilterInputWSTab, statusValue);
            return this;
        }

        //DETAIL TAB
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By noteInput = By.CssSelector("textarea[id='notes']");
        private readonly By statusDd = By.XPath("//label[text()='Status']/following-sibling::div//button");
        private readonly By statusValue = By.XPath("//label[text()='Status']/following-sibling::div//button/span[1]");
        private readonly string anyStatusOption = "//span[text()='{0}']/ancestor::li";
        public readonly By DebriefButton = By.XPath("//button[@title='Debrief']");

        [AllureStep]
        public RoundInstanceForm ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public RoundInstanceForm AddNotes(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }
        [AllureStep]
        public RoundInstanceForm ClickOnStatusDdAndSelectValue(string statusValue)
        {
            ClickOnElement(statusDd);
            ClickOnElement(anyStatusOption, statusValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceForm VerifyStatusDetailTab(string statusValueExp)
        {
            Assert.AreEqual(statusValueExp, GetElementText(statusValue));
            return this;
        }


        [AllureStep]
        public RoundInstanceForm ClickOnDebriefBtn()
        {
            ClickOnElement(DebriefButton);
            return this;
        }

        //HISTORY TAB
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly By updatedByUser = By.XPath("(//strong[text()='Update']/parent::div/following-sibling::div/strong[1])[1]");
        private readonly By updatedTime = By.XPath("//strong[text()='Update']/parent::div/following-sibling::div/strong[2]");
        private readonly By contentUpdated = By.XPath("(//strong[text()='Update']/following-sibling::div)[1]");

        [AllureStep]
        public RoundInstanceForm ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }


        [AllureStep]
        public RoundInstanceForm VerifyFirstHistoryRow(string notesValueExp, string updatedByUserExp)
        {
            Assert.AreEqual(notesValueExp, GetElementText(contentUpdated));
            Assert.AreEqual(updatedByUserExp, GetElementText(updatedByUser));
            return this;
        }

        [AllureStep]
        public RoundInstanceForm VerifyFirstHistoryRow(string[] valueUpdated, string[] labels, string updatedByUserExp)
        {
            string[] valueNoteActual = GetElementText(contentUpdated).Split(Environment.NewLine);

            Assert.AreEqual(labels[0]+ ": " + valueUpdated[0] + ".", valueNoteActual[0]);
            Assert.AreEqual(updatedByUserExp, GetElementText(updatedByUser));
            return this;
        }

        //EVENTS TAB
        private readonly By eventsTab = By.CssSelector("a[aria-controls='roundInstanceEvents-tab']");
        private readonly By addNewItemBtn = By.XPath("//div[@id='roundInstanceEvents-tab']//button[text()='Add New Item']");
        private readonly By loadingIconAtEventsTab = By.XPath("//div[@id='roundInstanceEvents-tab']//div[@class='loading-data']");

        [AllureStep]
        public RoundInstanceForm ClickOnEventsTab()
        {
            ClickOnElement(eventsTab);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(loadingIconAtEventsTab);
            WaitUtil.WaitForElementInvisible(loadingIconAtEventsTab);
            return this;
        }

        [AllureStep]
        public RoundInstanceForm ClickOnAddNewItemBtn()
        {
            ClickOnElement(addNewItemBtn);
            return this;
        }

        //TASKS TAB
        private readonly By tasksTab = By.CssSelector("a[aria-controls='tasks-tab']");
        private readonly By allTasks = By.XPath("//div[@id='tasks-tab']//div[@class='grid-canvas']/div");
        private readonly By containerTasksTab = By.XPath("//div[@id='tasks-tab']//div[@class='slick-viewport']");

        [AllureStep]
        public RoundInstanceForm ClickOnTasksTab()
        {
            ClickOnElement(tasksTab);
            return this;
        }

        [AllureStep]
        public RoundInstanceForm IsTasksTabLoaded()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForAllElementsPresent(allTasks);
            return this;
        }


        [AllureStep]
        public RoundInstanceForm VerifyDisplayVerticalScrollBarTasksTab()
        {
            VerifyDisplayVerticalScrollBar(containerTasksTab);
            return this;
        }


        #region
        private readonly By bulkUpdateTitle = By.XPath("//h4[text()='Bulk Update : Page 1 of 1']");
        private readonly By statusBulkUpdateDd = By.XPath("//label[text()='Status']/following-sibling::select[1]");
        private readonly By resolutionCodeOption = By.XPath("//label[text()='Resolution Code']/following-sibling::select[1]");
        private readonly By confirmBulkUpdateBtn = By.XPath("//button[text()='Confirm']");

        //DYNAMIC
        private readonly string statusBulkUpdateOption = "//label[text()='Status']/following-sibling::select[1]/option[text()='{0}']";

        [AllureStep]
        public RoundInstanceForm SelectStatusInBulkUpdateForm(string status)
        {
            WaitUtil.WaitForElementVisible(bulkUpdateTitle);
            ClickOnElement(statusBulkUpdateDd);
            ClickOnElement(statusBulkUpdateOption, status);
            return this;
        }

        [AllureStep]
        public RoundInstanceForm ClickOnConfirmBtnBulkUpdateForm()
        {
            ClickOnElement(confirmBulkUpdateBtn);
            return this;
        }

        #endregion

    }
}
