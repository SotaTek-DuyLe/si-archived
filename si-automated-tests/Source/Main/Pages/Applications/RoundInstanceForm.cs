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
        public readonly By DropDownStatusButton = By.XPath("//button[@data-id='status']");
        public readonly By DropDownSelect = By.XPath("//ul[@class='dropdown-menu inner']");
        public readonly By WorksheetTab = By.XPath("//a[@aria-controls='worksheet-tab']");
        public readonly By WorkSheetIframe = By.XPath("//iframe[@id='worksheet-tab']");
        private readonly By title = By.XPath("//h4[text()='Round Instance']");
        private readonly By idService = By.CssSelector("h4[title='Id']");
        private readonly By taskName = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-group') and not(contains(@style, 'display: none;'))]");

        #region WorkSheetTab
        public readonly By ToggleRoundButton = By.XPath("//button[@id='t-toggle-rounds']");
        public readonly By ReallocateButton = By.XPath("//button[@id='t-bulk-reallocate']");
        private readonly By expandRoundBtn = By.XPath("//span[text()='Expand Rounds']/parent::button");

        private readonly string RLITable = "//div[@id='grid']//div[@class='grid-canvas']";
        private readonly string RLIRow = "./div[contains(@class, 'slick-row') and not(contains(@class,'slick-group'))]";
        private readonly string RLICheckboxCell = "./div[contains(@class, 'l0')]//input";
        private readonly string RLIIdCell = "./div[contains(@class, 'l3')]";
        private readonly string RLIDescriptionCell = "./div[contains(@class, 'l4')]";
        private readonly string RLIServiceCell = "./div[contains(@class, 'l5')]";
        private readonly By allTaskRows = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))]");
        private readonly string allIdRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l3 r3')]";
        private readonly string desciptionRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l4 r4')]";
        private readonly string serviceRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l5 r5')]";
        private readonly string partyRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l6 r6')]";
        private readonly string siteRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l7 r7')]";
        private readonly string streetRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l8 r8')]";
        private readonly string townRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l9 r9')]";
        private readonly string postcodeRows = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured') and not(contains(@style, 'display: none;'))][{0}]/div[contains(@class, 'l10 r10')]";
        private readonly By idSearch = By.XPath("//div[contains(@id, 'grid')]//div[contains(@class, 'ui-state-default')]//div[contains(@class, 'l3')]//input[contains(@class, 'value')]");

        //DYNAMIC
        private readonly string checkboxRow = "//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured')][{0}]/div[contains(@class, 'l0 r0')]/input";

        public TableElement RLITableEle
        {
            get => new TableElement(RLITable, RLIRow, new List<string>() { RLICheckboxCell, RLIIdCell, RLIDescriptionCell, RLIServiceCell });
        }

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
        #endregion

        public RoundInstanceForm SelectStatus(string status)
        {
            SleepTimeInMiliseconds(300);
            IWebElement webElement = this.driver.FindElements(DropDownSelect).FirstOrDefault(x => x.Displayed);
            SelectByDisplayValueOnUlElement(webElement, status);
            return this;
        }

        public RoundInstanceForm IsRoundInstanceForm(string serviceId)
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.AreEqual(serviceId, GetElementText(idService));
            return this;
        }

        public RoundInstanceForm ClickOnWorksheetTab()
        {
            ClickOnElement(WorksheetTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public RoundInstanceForm ClickOnExpandRoundBtn()
        {
            ClickOnElement(expandRoundBtn);
            WaitUtil.WaitForPageLoaded();
            return this;
        }

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

        public RoundInstanceForm SelectTwoTaskInGrid(string firstTask, string secondTask)
        {
            ClickOnElement(firstTask);
            ClickOnElement(secondTask);
            return this;
        }

        public RoundInstanceForm ClickReallocateBtn()
        {
            ClickOnElement(ReallocateButton);
            return this;
        }

        public string GetTaskName()
        {
            string taskNameDisplayed = GetElementText(taskName);
            return taskNameDisplayed.Split(Environment.NewLine)[0].Replace(":", " ").Split("(")[0].Trim();
        }

        public RoundInstanceForm SendKeyInId(string taskId)
        {
            SendKeys(idSearch, taskId);
            return this;
        }

        public RoundInstanceForm VerifyTaskNoLongerDisplayedInGrid()
        {
            Assert.IsTrue(IsControlUnDisplayed(allTaskRows));
            return this;
        }

        //DETAIL TAB
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By noteInput = By.CssSelector("textarea[id='notes']");
        private readonly By statusDd = By.XPath("//label[text()='Status']/following-sibling::div//button");
        private readonly string anyStatusOption = "//span[text()='{0}']/ancestor::li";

        public RoundInstanceForm ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public RoundInstanceForm AddNotes(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }

        public RoundInstanceForm ClickOnStatusDdAndSelectValue(string statusValue)
        {
            ClickOnElement(statusDd);
            ClickOnElement(anyStatusOption, statusValue);
            return this;
        }

        //HISTORY TAB
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly By typeInFirstHistoryRow = By.XPath("//div[@id='history-tab']//div[@class='grid-canvas']/div[1]//div[contains(@class, 'l0')]");
        private readonly By actionNameInFirstHistoryRow = By.XPath("//div[@id='history-tab']//div[@class='grid-canvas']/div[1]//div[contains(@class, 'l1')]");
        private readonly By detailInFirstHistoryRow = By.XPath("//div[@id='history-tab']//div[@class='grid-canvas']/div[1]//button");
        private readonly By createdByUserInFirstHistoryRow = By.XPath("//div[@id='history-tab']//div[@class='grid-canvas']/div[1]//div[contains(@class, 'l6')]");
            
        public RoundInstanceForm ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public RoundInstanceForm VerifyFirstHistoryRow(string typeExp, string actionNameExp, string detailValueExp, string createdByUserExp) 
        {
            Assert.AreEqual(typeExp, GetElementText(typeInFirstHistoryRow));
            Assert.AreEqual(actionNameExp, GetElementText(actionNameInFirstHistoryRow));
            Assert.IsTrue(GetElementText(detailInFirstHistoryRow).Trim().Contains(detailValueExp));
            Assert.AreEqual(createdByUserExp, GetElementText(createdByUserInFirstHistoryRow));
            return this;
        }

        //EVENTS TAB
        private readonly By eventsTab = By.CssSelector("a[aria-controls='roundInstanceEvents-tab']");
        private readonly By addNewItemBtn = By.XPath("//div[@id='roundInstanceEvents-tab']//button[text()='Add New Item']");

        public RoundInstanceForm ClickOnEventsTab()
        {
            ClickOnElement(eventsTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public RoundInstanceForm ClickOnAddNewItemBtn()
        {
            ClickOnElement(addNewItemBtn);
            return this;
        }

    }
}
