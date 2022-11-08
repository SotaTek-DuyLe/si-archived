using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;


namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RoundGroupPage : BasePageCommonActions
    {
        private readonly By roundGroupInput = By.XPath("//div[@id='details-tab']//input[@name='roundGroup']");
        private readonly By sortOrderInput = By.XPath("//div[@id='details-tab']//input[@name='sortOrder']");
        private readonly By dispatchSiteSelect = By.XPath("//div[@id='details-tab']//select[@id='dispatchSite.id']");
        private readonly By dispatchSiteSelectOpt = By.XPath("//div[@id='details-tab']//select[@id='dispatchSite.id']//option");
        private readonly By generationOfRoundInstanceSelect = By.XPath("//div[@id='details-tab']//select[@id='generationOfRoundInstance.id']");
        private readonly By defaultWorkSheetSelect = By.XPath("//div[@id='details-tab']//select[@id='defaultWorkSheet.id']");
        private readonly By recordWorkingTimeSelect = By.XPath("//div[@id='details-tab']//select[@id='recordWorkingTime.id']");
        private readonly By copyBtn = By.XPath("//button[@title='Copy']");
        private readonly By nextMonthBtn = By.XPath("//button[@title='Next month']");
        private readonly By optimiseBtn = By.XPath("//button[@title='Optimise']");
        private readonly By roundTab = By.XPath("//a[@aria-controls='rounds-tab']");
        private readonly By sitesTab = By.XPath("//a[@aria-controls='sites-tab']");
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        private readonly By defaultResourcesTab = By.XPath("//a[@aria-controls='defaultResources-tab']");
        private readonly By calendarTab = By.XPath("//a[@aria-controls='calendar-tab']");
        private readonly By addNewItemBtnOnRoundTab = By.XPath("//div[@id='rounds-tab']//button");
        private readonly By addNewItemBtnOnResourceTab = By.XPath("//div[@id='defaultResources-tab']//button[contains(string(), 'Add New Item')]");
        private readonly By syncRoundResourceOnResourceTab = By.XPath("//div[@id='defaultResources-tab']//button[contains(string(), 'Sync Round Resource')]");
        private readonly By roundRows = By.XPath("//div[@id='rounds-tab']//table//tbody//tr");
        private readonly By resourceRows = By.XPath("//div[@id='defaultResources-tab']//table//tbody//tr[contains(@data-bind, 'with: $data.getFields()')][not(ancestor::tr)]");
        private readonly string resourceDetailContainerRows = "//div[@id='defaultResources-tab']//table//tbody//tr[contains(@id, 'child-target{0}')]";
        private readonly string resourceDetailRows = "//div[@id='defaultResources-tab']//table//tbody//tr[contains(@id, 'child-target{0}')]//tr[@data-bind='with: $data.getFields()']";
        private readonly By typeSelect = By.XPath("./td//select[@id='type.id']");
        private readonly By resourceSelect = By.XPath("./td//select[@id='resource.id']");
        private readonly By resourceSelectOpt = By.XPath("./td//select[@id='resource.id']//option");
        private readonly By quantityInput = By.XPath("./td//input[@id='quantity.id']");
        private readonly By startDateInput = By.XPath("./td//input[@id='startDate.id']");
        private readonly By endDateInput = By.XPath("./td//input[@id='endDate.id']");
        private readonly By retireBtn = By.XPath("./td//button[@title='Retire']");
        private readonly By editBtn = By.XPath("./td//button[@title='Edit']");
        private readonly By toggleBtn = By.XPath("./td//div[@id='toggle-actions']");
        private readonly By addResourceBtn = By.XPath("./td//button[contains(string(), 'Add Resource')]");
        private readonly By hasScheduleCheckbox = By.XPath("./td//input[@type='checkbox']");
        private readonly By scheduleInput = By.XPath("./td//input[@id='schedule.id']");
        private readonly By patternStartDateInput = By.XPath("//div[@id='defaultResources-tab']//div[@id='rightPanel']//input[@id='patternStartDate.id']");
        private readonly By rightPanelTitle = By.XPath("//div[@id='defaultResources-tab']//label[@class='text-muted']");
        private readonly By periodTimeButtons = By.XPath("//div[@id='defaultResources-tab']//div[@id='rightPanel']//div[@role='group']//button");
        private readonly By weeklyFrequencySelect = By.XPath("//div[@id='defaultResources-tab']//div[@id='rightPanel']//select[@id='weekly-frequency']");
        private readonly By dailyFrequencySelect = By.XPath("//div[@id='defaultResources-tab']//div[@id='rightPanel']//select[@id='daily-frequency']");
        private readonly By dateHeaderCells = By.XPath("//div[@id='calendar-tab']//table//tbody//div[contains(@class, 'fc-row')]//div[@class='fc-bg']//tbody//td");
        private readonly By dateDetailCells = By.XPath("//div[@id='calendar-tab']//table//tbody//div[contains(@class, 'fc-row')]//div[@class='fc-content-skeleton']//tbody//td");
        private readonly By leftSiteColumn = By.XPath("//div[@id='sites-tab']//ul[@data-bind='foreach: allSites']");
        private readonly By rightSiteColumn = By.XPath("//div[@id='sites-tab']//ul[@data-bind='foreach: addedSites']");
        private readonly By addSiteButton = By.XPath("//div[@id='sites-tab']//button[contains(@data-bind, 'click: addSites')]");
        private readonly By removeSiteButton = By.XPath("//div[@id='sites-tab']//button[contains(@data-bind, 'click: removeSites')]");

        #region Schedule Tab
        public readonly By ScheduleTab = By.XPath("//a[@aria-controls='schedules-tab']");
        public readonly string AddNewScheduleBtn = "//div[@id='schedules-tab']//button[contains(string(), ' Add New Schedule')]";
        private readonly string ScheduleTable = "//div[@id='schedules-tab']//table";
        private readonly string ScheduleRows = "./tbody//tr";
        private readonly string ScheduleIdCell = "./td[1]";
        private readonly string ScheduleDetailCell = "./td//a";
        private readonly string ScheduleStartDateCell = "./td//input[@id='startDate.id']";
        private readonly string ScheduleEndDateCell = "./td//input[@id='endDate.id']";
        private readonly string ScheduleSeasonCell = "./td//select[@id='season.id']";
        private readonly string ScheduleEditCell = "./td//button[contains(string(), 'Edit')]";

        public TableElement ScheduleTableElement
        {
            get => new TableElement(ScheduleTable, ScheduleRows, new List<string>() { ScheduleIdCell, ScheduleDetailCell, ScheduleStartDateCell, ScheduleEndDateCell, ScheduleSeasonCell, ScheduleEditCell });
        }
        [AllureStep]
        public RoundGroupPage ClickScheduleDetail(string id)
        {
            ScheduleTableElement.ClickCellOnCellValue(1, 0, id);
            return this;
        }
        [AllureStep]
        public RoundGroupPage EditPatternEnd(string id, string patternEnd)
        {
            int rowIdx = ScheduleTableElement.GetRows().IndexOf(ScheduleTableElement.GetRowByCellValue(0, id));
            ScheduleTableElement.SetCellValue(rowIdx, 3, patternEnd);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyPatternEnd(string id, string patternEnd)
        {
            int rowIdx = ScheduleTableElement.GetRows().IndexOf(ScheduleTableElement.GetRowByCellValue(0, id));
            VerifyCellValue(ScheduleTableElement, rowIdx, 3, patternEnd);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyNewSchedule(string scheduleDetail, string startDate, string endDate)
        {
            int rowCount = ScheduleTableElement.GetRows().Count;
            List<object> rowValues = ScheduleTableElement.GetRowValue(rowCount - 1);
            Assert.IsTrue(rowValues[1].AsString() == scheduleDetail);
            Assert.IsTrue(rowValues[2].AsString() == startDate);
            Assert.IsTrue(rowValues[3].AsString() == endDate);
            return this;
        }

        [AllureStep]
        public RoundGroupPage VerifyScheduleDetail(string scheduleDetail)
        {
            int rowCount = ScheduleTableElement.GetRows().Count;
            List<object> rowValues = ScheduleTableElement.GetRowValue(rowCount - 1);
            Assert.IsTrue(rowValues[1].AsString() == scheduleDetail);
            return this;
        }

        [AllureStep]
        public RoundGroupPage ClickEditSchedule()
        {
            ScheduleTableElement.ClickCell(0, 5);
            return this;
        }
        #endregion

        [AllureStep]
        public RoundGroupPage VerifyDefaultDataOnAddForm()
        {
            Assert.IsEmpty(GetElement(roundGroupInput).GetAttribute("value"));
            Assert.IsEmpty(GetElement(sortOrderInput).GetAttribute("value"));
            Assert.IsEmpty(GetFirstSelectedItemInDropdown(dispatchSiteSelect));
            Assert.IsTrue(GetFirstSelectedItemInDropdown(generationOfRoundInstanceSelect) == "Default");
            Assert.IsTrue(GetFirstSelectedItemInDropdown(defaultWorkSheetSelect) == "Round Map (Round Legs)");
            Assert.IsTrue(GetFirstSelectedItemInDropdown(recordWorkingTimeSelect) == "Default");
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyRoundGroup(string value)
        {
            Assert.IsTrue(GetElement(roundGroupInput).GetAttribute("value") == value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickOnDispatchSiteAndVerifyData()
        {
            ClickOnElement(dispatchSiteSelect);
            List<string> options = GetAllElements(dispatchSiteSelectOpt).Select(x => x.Text).Where(x => !string.IsNullOrEmpty(x)).ToList();
            Assert.AreEqual(new List<string>() { "Kingston Tip", "Townmead Tip & Depot (East)", "Townmead Weighbridge" }, options);
            return this;
        }
        [AllureStep]
        public RoundGroupPage SelectDispatchSite(string value)
        {
            SelectTextFromDropDown(dispatchSiteSelect, value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage EnterRoundGroupValue(string value)
        {
            SendKeys(roundGroupInput, value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyServiceButtonsVisible()
        {
            WaitUtil.WaitForElementVisible(copyBtn);
            Assert.IsTrue(IsControlDisplayed(copyBtn));
            Assert.IsTrue(IsControlDisplayed(optimiseBtn));
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickRoundTab()
        {
            ClickOnElement(roundTab);
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickDefaultResourceTab()
        {
            ClickOnElement(defaultResourcesTab);
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickCalendarTab()
        {
            ClickOnElement(calendarTab);
            return this;
        }
        [AllureStep]
        public RoundGroupPage IsRoundTab()
        {
            Assert.IsTrue(IsControlDisplayed(roundTab));
            Assert.IsTrue(IsControlDisplayed(roundRows));
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickAddNewItemOnRoundTab()
        {
            ClickOnElement(addNewItemBtnOnRoundTab);
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickAddNewItemOnResourceTab()
        {
            ClickOnElement(addNewItemBtnOnResourceTab);
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickSyncRoundResourceOnResourceTab()
        {
            ClickOnElement(syncRoundResourceOnResourceTab);
            return this;
        }
        [AllureStep]
        public int GetIndexNewRoundRow()
        {
            return GetAllElements(roundRows).Count - 1;
        }
        [AllureStep]
        public int GetIndexNewResourceRow()
        {
            return GetAllElements(resourceRows).Count - 1;
        }
        [AllureStep]
        public RoundGroupPage VerifyDefaultResourceRowsIsVisible()
        {
            Assert.IsTrue(IsControlDisplayed(resourceRows));
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyDropDownTypeIsPresent(int rowIdx)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            Assert.IsTrue(row.FindElement(typeSelect).Displayed);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyDropDownTypeIsDisable(int rowIdx)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            Assert.IsFalse(row.FindElement(typeSelect).Enabled);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyStartDateInputIsDisable(int rowIdx)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            Assert.IsFalse(row.FindElement(startDateInput).Enabled);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyInputQuantityIsPresent(int rowIdx)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            Assert.IsTrue(row.FindElement(quantityInput).Displayed);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyRetireButtonIsPresent(int rowIdx)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            Assert.IsTrue(row.FindElement(retireBtn).Displayed);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyStartDateInput(int rowIdx, string expectedValue)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            Assert.IsTrue(row.FindElement(startDateInput).GetAttribute("value") == expectedValue);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyEndDateInput(int rowIdx, string expectedValue)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            Assert.IsTrue(row.FindElement(endDateInput).GetAttribute("value") == expectedValue);
            return this;
        }
        [AllureStep]
        public RoundGroupPage SelectType(int rowIdx, string value)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            IWebElement select = row.FindElement(typeSelect);
            SelectTextFromDropDown(select, value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage EnterQuantity(int rowIdx, string value)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            IWebElement input = row.FindElement(quantityInput);
            SendKeys(input, value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage EnterStartDate(int rowIdx, string value)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            IWebElement input = row.FindElement(startDateInput);
            SendKeys(input, value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage EnterEndDate(int rowIdx, string value)
        {
            IWebElement row = GetAllElements(resourceRows)[rowIdx];
            IWebElement input = row.FindElement(endDateInput);
            SendKeys(input, value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage EnterRoundValue(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            SendKeys(webElement.FindElement(By.XPath("./td/input[@id='round.id']")), value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickExpandButton(int rowIdx)
        {
            IWebElement webElement = GetAllElements(resourceRows)[rowIdx];
            ClickOnElement(webElement.FindElement(toggleBtn));
            return this;
        }
        [AllureStep]
        public int GetIndexResourceRowByType(string type)
        {
            List<IWebElement> webElements = GetAllElements(resourceRows);
            for (int i = 0; i < webElements.Count; i++)
            {
                if (GetFirstSelectedItemInDropdown(webElements[i].FindElement(typeSelect)) == type)
                {
                    return i;
                }
            }
            return 0;
        }
        [AllureStep]
        public RoundGroupPage ClickAddResource(int rowIdx)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailContainerRows, rowIdx));
            IWebElement webElement = this.driver.FindElement(resourceDetailXPath);
            ClickOnElement(webElement.FindElement(addResourceBtn));
            return this;
        }
        [AllureStep]
        public RoundGroupPage SelectResourceType(int resourceRowIdx, int resourceDetailRowIdx, int index)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            IWebElement webElement = this.driver.FindElements(resourceDetailXPath)[resourceDetailRowIdx];
            SelectIndexFromDropDown(webElement.FindElement(resourceSelect), index);
            return this;
        }
        [AllureStep]
        public RoundGroupPage SelectResourceType(int resourceRowIdx, int resourceDetailRowIdx, string value)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            IWebElement webElement = this.driver.FindElements(resourceDetailXPath)[resourceDetailRowIdx];
            SelectTextFromDropDown(webElement.FindElement(resourceSelect), value);
            return this;
        }
        [AllureStep]
        public int GetResourceOptionCount(int resourceRowIdx, int resourceDetailRowIdx)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            IWebElement webElement = this.driver.FindElements(resourceDetailXPath)[resourceDetailRowIdx];
            return webElement.FindElements(resourceSelectOpt).Count;
        }
        [AllureStep]
        public RoundGroupPage ClickHasSchedule(int resourceRowIdx, int resourceDetailRowIdx)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            IWebElement webElement = this.driver.FindElements(resourceDetailXPath)[resourceDetailRowIdx];
            ClickOnElement(webElement.FindElement(hasScheduleCheckbox));
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyPatternStartDateContainString(string value)
        {
            IWebElement webElement = GetElement(patternStartDateInput);
            Assert.IsTrue(webElement.GetAttribute("value") == value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyRightPanelTitle(string value)
        {
            IWebElement webElement = GetElement(rightPanelTitle);
            Assert.IsTrue(GetElementText(webElement).Trim() == value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyAllPeriodTimeOptions(List<string> options)
        {
            List<string> presentOptions = GetAllElements(periodTimeButtons).Select(x => x.Text).ToList();
            Assert.AreEqual(options, presentOptions);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyResourceDetailRow(int resourceRowIdx, int resourceDetailRowIdx, int resourceSelectedIdx, bool hasSchedule, string schedule, bool isVisibleRetireBtn, bool isVisibleEditBtn)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            IWebElement row = this.driver.FindElements(resourceDetailXPath)[resourceDetailRowIdx];
            IWebElement select = row.FindElement(resourceSelect);
            List<string> options = row.FindElements(resourceSelectOpt).Select(x => x.Text).ToList();
            string selectedValue = GetFirstSelectedItemInDropdown(select);
            Assert.IsTrue(options.IndexOf(selectedValue) == resourceSelectedIdx);
            IWebElement checkbox = row.FindElement(hasScheduleCheckbox);
            Assert.IsTrue(checkbox.Selected == hasSchedule);
            IWebElement input = row.FindElement(scheduleInput);
            Assert.IsTrue(input.GetAttribute("value") == schedule);
            IWebElement retireButton = row.FindElement(retireBtn);
            Assert.IsTrue(retireButton.Displayed == isVisibleRetireBtn);
            IWebElement editButton = row.FindElement(editBtn);
            Assert.IsTrue(editButton.Displayed == isVisibleEditBtn);
            return this;
        }

        [AllureStep]
        public RoundGroupPage VerifyResourceDetailRow(int resourceRowIdx, int resourceDetailRowIdx, string resourceSelected, bool hasSchedule, string schedule, bool isVisibleRetireBtn)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            IWebElement row = this.driver.FindElements(resourceDetailXPath)[resourceDetailRowIdx];
            IWebElement select = row.FindElement(resourceSelect);
            string selectedValue = GetFirstSelectedItemInDropdown(select);
            Assert.IsTrue(selectedValue == resourceSelected);
            IWebElement checkbox = row.FindElement(hasScheduleCheckbox);
            Assert.IsTrue(checkbox.Selected == hasSchedule);
            IWebElement input = row.FindElement(scheduleInput);
            Assert.IsTrue(input.GetAttribute("value") == schedule);
            IWebElement retireButton = row.FindElement(retireBtn);
            Assert.IsTrue(retireButton.Displayed == isVisibleRetireBtn);
            return this;
        }

        [AllureStep]
        public RoundGroupPage VerifyResourceDetailRow(int resourceRowIdx, int resourceDetailRowIdx, string resourceSelected, bool hasSchedule, string schedule, string startDateValue, string endDateValue, bool isVisibleRetireBtn, bool checkEnable)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            IWebElement row = this.driver.FindElements(resourceDetailXPath)[resourceDetailRowIdx];
            IWebElement select = row.FindElement(resourceSelect);
            string selectedValue = GetFirstSelectedItemInDropdown(select);
            Assert.IsTrue(selectedValue == resourceSelected);
            IWebElement checkbox = row.FindElement(hasScheduleCheckbox);
            Assert.IsTrue(checkbox.Selected == hasSchedule);
            IWebElement input = row.FindElement(scheduleInput);
            Console.WriteLine(input.GetAttribute("value"));
            Assert.IsTrue(input.GetAttribute("value") == schedule);
            IWebElement startDate = row.FindElement(startDateInput);
            Assert.IsTrue(startDate.GetAttribute("value") == startDateValue);
            IWebElement endDate = row.FindElement(endDateInput);
            Assert.IsTrue(endDate.GetAttribute("value") == endDateValue);
            IWebElement retireButton = row.FindElement(retireBtn);
            Assert.IsTrue(retireButton.Displayed == isVisibleRetireBtn);
            if (checkEnable)
            {
                Assert.IsFalse(select.Enabled);
                Assert.IsFalse(checkbox.Enabled);
                Assert.IsFalse(input.Enabled);
                Assert.IsFalse(startDate.Enabled);
                Assert.IsTrue(endDate.Enabled);
            }
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickEditButton(int resourceRowIdx, int resourceDetailRowIdx)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            IWebElement row = this.driver.FindElements(resourceDetailXPath)[resourceDetailRowIdx];
            IWebElement editButton = row.FindElement(editBtn);
            ClickOnElement(editButton);
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickPeriodTimeButton(string period)
        {
            SleepTimeInMiliseconds(500);
            IWebElement webElement = GetAllElements(periodTimeButtons).FirstOrDefault(x => x.Text.Contains(period));
            ClickOnElement(webElement);
            return this;
        }
        [AllureStep]
        public RoundGroupPage IsPeriodButtonSelected(string period)
        {
            IWebElement webElement = GetAllElements(periodTimeButtons).FirstOrDefault(x => x.Text.Contains(period));
            Assert.IsTrue(webElement.GetAttribute("class").Contains("btn-primary"));
            return this;
        }
        [AllureStep]
        public RoundGroupPage SelectWeeklyFrequency(string value)
        {
            SelectTextFromDropDown(weeklyFrequencySelect, value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage SelectDailyFrequency(string value)
        {
            SelectTextFromDropDown(dailyFrequencySelect, value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifySelectWeeklyFrequency(string value)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(weeklyFrequencySelect) == value); ;
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickDayButtonOnWeekly(string day)
        {
            string xpath = $"//div[@id='defaultResources-tab']//div[@id='rightPanel']//div[contains(@data-bind, 'foreach: dayButtons')]//button[contains(string(), '{day}')]";
            ClickOnElement(By.XPath(xpath));
            Thread.Sleep(200);
            return this;
        }
        [AllureStep]
        public RoundGroupPage IsDayButtonOnWeeklySelected(string day)
        {
            string xpath = $"//div[@id='defaultResources-tab']//div[@id='rightPanel']//div[@data-bind='foreach: dayButtons']//button[contains(string(), '{day}')]";
            IWebElement webElement = GetElement(By.XPath(xpath));
            Assert.IsTrue(webElement.GetAttribute("class").Contains("btn-primary"));
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyRightPanelIsInVisible()
        {
            Assert.IsTrue(IsControlUnDisplayed(rightPanelTitle));
            return this;
        }

        [AllureStep]
        public int GetIndexNewRowDetail(int resourceRowIdx)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            return this.driver.FindElements(resourceDetailXPath).Count - 1;
        }

        [AllureStep]
        public RoundGroupPage VerifyRowDetailIsVisible(int resourceRowIdx, int resourceDetailRowIdx)
        {
            By resourceDetailXPath = By.XPath(string.Format(resourceDetailRows, resourceRowIdx));
            IWebElement webElement = this.driver.FindElements(resourceDetailXPath)[resourceDetailRowIdx];
            Assert.IsTrue(webElement.Displayed);
            return this;
        }
        [AllureStep]
        public RoundGroupPage EnterRoundTypeValue(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            IWebElement select = webElement.FindElement(By.XPath("./td/select[@id='roundType.id']"));
            SelectElement selectedValue = new SelectElement(select);
            selectedValue.SelectByText(value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage EnterDispatchSiteValue(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            IWebElement select = webElement.FindElement(By.XPath("./td/select[@id='dispatchSite.id']"));
            SelectElement selectedValue = new SelectElement(select);
            selectedValue.SelectByText(value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage EnterShiftValue(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            IWebElement select = webElement.FindElement(By.XPath("./td/select[@id='shift.id']"));
            SelectElement selectedValue = new SelectElement(select);
            selectedValue.SelectByText(value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyRoundColor(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            IWebElement input = webElement.FindElement(By.XPath("./td/input[@name='color']"));
            Assert.IsTrue(input.GetAttribute("value") == value);
            return this;
        }
        [AllureStep]
        public RoundGroupPage DoubleClickRound(int rowIdx)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            DoubleClickOnElement(webElement);
            return this;
        }
        [AllureStep]
        public RoundGroupPage DoubleClickRound(string roundType)
        {
            List<IWebElement> webElements = GetAllElements(roundRows);
            foreach (var webElement in webElements)
            {
                IWebElement input = webElement.FindElement(By.XPath("./td/input[@id='round.id']"));
                if (input.GetAttribute("value") != roundType) continue;
                DoubleClickOnElement(webElement);
                break;
            }
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickRetireButton(string driverType)
        {
            List<IWebElement> webElements = GetAllElements(resourceRows);
            for (int i = 0; i < webElements.Count; i++)
            {
                if (GetFirstSelectedItemInDropdown(webElements[i].FindElement(typeSelect)) == driverType)
                {
                    ClickOnElement(webElements[i].FindElement(retireBtn));
                    break;
                }
            }
            return this;
        }
        [AllureStep]

        public RoundGroupPage ClickRetireDefaultResourceButton(string resource)
        {
            List<IWebElement> webElements = new List<IWebElement>();
            for (int i = 0; i < webElements.Count; i++)
            {
                if (GetFirstSelectedItemInDropdown(webElements[i].FindElement(resourceSelect)) == resource)
                {
                    ClickOnElement(webElements[i].FindElement(retireBtn));
                    break;
                }
            }
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyDefaultResourceIsInVisible(string driverType)
        {
            List<IWebElement> webElements = GetAllElements(resourceRows);
            for (int i = 0; i < webElements.Count; i++)
            {
                Assert.IsFalse(GetFirstSelectedItemInDropdown(webElements[i].FindElement(typeSelect)) == driverType);
            }
            return this;
        }
        [AllureStep]
        public RoundGroupPage VerifyDetailDefaultResourceIsInVisible(string driverType, string resource)
        {
            List<IWebElement> webElements = GetAllElements(resourceRows);
            List<IWebElement> detailWebElements = new List<IWebElement>();
            for (int i = 0; i < webElements.Count; i++)
            {
                if (GetFirstSelectedItemInDropdown(webElements[i].FindElement(typeSelect)) == driverType)
                {
                    bool containDetail = detailWebElements[i].FindElements(resourceSelect).Count != 0;
                    if (containDetail)
                    {
                        if (!detailWebElements[i].Displayed)
                        {
                            ClickExpandButton(i);
                            Thread.Sleep(300);
                        }
                        Assert.IsFalse(GetFirstSelectedItemInDropdown(detailWebElements[i].FindElement(resourceSelect)) == resource);
                    }
                }
            }
            return this;
        }
        [AllureStep]
        public List<DefaultResourceModel> GetAllDefaultResourceModels()
        {
            List<DefaultResourceModel> defaultResources = new List<DefaultResourceModel>();
            List<IWebElement> webElements = GetAllElements(resourceRows);
            List<IWebElement> webDetailElements = new List<IWebElement>();
            for (int i = 0; i < webElements.Count; i++)
            {
                DefaultResourceModel defaultResource = new DefaultResourceModel();
                defaultResource.Type = GetFirstSelectedItemInDropdown(webElements[i].FindElement(typeSelect));
                defaultResource.Quantity = webElements[i].FindElement(quantityInput).GetAttribute("value");
                bool containDetail = webDetailElements[i].FindElements(resourceSelect).Count != 0;
                if (containDetail)
                {
                    if (!webDetailElements[i].Displayed)
                    {
                        ClickExpandButton(i);
                        Thread.Sleep(300);
                    }

                    defaultResource.Detail = new DetailDefaultResourceModel()
                    {
                        Resource = GetFirstSelectedItemInDropdown(webDetailElements[i].FindElement(resourceSelect)),
                        HasSchedule = webDetailElements[i].FindElement(hasScheduleCheckbox).Selected,
                        Schedule = webDetailElements[i].FindElement(scheduleInput).GetAttribute("value"),
                    };
                }
                defaultResources.Add(defaultResource);
            }
            return defaultResources;
        }


        /// <summary>
        /// DoubleClickRoundGroup
        /// </summary>
        /// <param name="startDate">ddMMyyyy</param>
        /// <returns></returns>
        /// 
        [AllureStep]
        public RoundGroupPage DoubleClickRoundGroup(DateTime startDate, DateTime endDate, List<DayOfWeek> dayOfWeeks)
        {
            while (true)
            {
                List<IWebElement> headerCells = GetAllElements(dateHeaderCells);
                List<IWebElement> detailCells = GetAllElements(dateDetailCells);
                DateTime maxDate = DateTime.MinValue;
                for (int i = 0; i < headerCells.Count; i++)
                {
                    DateTime dateTime = DateTime.ParseExact(headerCells[i].GetAttribute("data-date"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    maxDate = dateTime;
                    if (startDate >= dateTime || !dayOfWeeks.Contains(dateTime.DayOfWeek)) continue;
                    IWebElement webElement = detailCells[i];
                    bool isRoundGroup = webElement.FindElements(By.XPath("./a")).Count != 0;
                    if (!isRoundGroup) continue;
                    DoubleClickOnElement(detailCells[i]);
                    return this;
                }
                if(maxDate >= endDate)
                {
                    break;
                }
                ClickOnElement(nextMonthBtn);
                WaitForLoadingIconToDisappear();
                Thread.Sleep(300);
            }
            return this;
        }
        [AllureStep]
        public RoundGroupPage RoundInstancesNotDisplayAfterEnddate(DateTime endDate)
        {
            int retry = 0;
            while (retry < 2)
            {
                List<IWebElement> headerCells = GetAllElements(dateHeaderCells);
                List<IWebElement> detailCells = this.driver.FindElements(dateDetailCells).ToList();
                for (int i = 0; i < headerCells.Count; i++)
                {
                    DateTime dateTime = DateTime.ParseExact(headerCells[i].GetAttribute("data-date"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    IWebElement webElement = detailCells[i];
                    bool isRoundGroup = webElement.FindElements(By.XPath("./a")).Count != 0;
                    if(dateTime > endDate) Assert.IsTrue(!isRoundGroup);
                }
                retry++;
                ClickOnElement(nextMonthBtn);
                WaitForLoadingIconToDisappear();
                Thread.Sleep(300);
            }
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickSiteTab()
        {
            ClickOnElement(sitesTab);
            return this;
        }
        [AllureStep]
        public RoundGroupPage IsOnSiteTab()
        {
            VerifyElementVisibility(leftSiteColumn, true);
            VerifyElementVisibility(rightSiteColumn, true);
            VerifyElementVisibility(addSiteButton, true);
            VerifyElementVisibility(removeSiteButton, true);
            return this;
        }
        [AllureStep]
        public RoundGroupPage CheckRightSiteVisibility(string selectSite, bool isVisible)
        {
            IWebElement webElement = GetElement(rightSiteColumn);
            List<IWebElement> options = webElement.FindElements(By.XPath("./li")).ToList();
            Assert.IsTrue(isVisible ? options.FirstOrDefault(x => x.Text == selectSite) != null: options.FirstOrDefault(x => x.Text == selectSite) == null);
            return this;
        }
        [AllureStep]
        public RoundGroupPage CheckLeftSiteVisibility(string selectSite, bool isVisible)
        {
            IWebElement webElement = GetElement(leftSiteColumn);
            List<IWebElement> options = webElement.FindElements(By.XPath("./li")).ToList();
            Assert.IsTrue(isVisible ? options.FirstOrDefault(x => x.Text == selectSite) != null : options.FirstOrDefault(x => x.Text == selectSite) == null);
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickRemoveRightSite(string selectSite)
        {
            IWebElement webElement = GetElement(rightSiteColumn);
            List<IWebElement> options = webElement.FindElements(By.XPath("./li")).ToList();
            foreach (var item in options)
            {
                if (item.Text == selectSite)
                {
                    ClickOnElement(item);
                    break;
                }
            }
            ClickOnElement(removeSiteButton);
            return this;
        }
        [AllureStep]
        public RoundGroupPage ClickAddLeftSite(string selectSite)
        {
            IWebElement webElement = GetElement(leftSiteColumn);
            List<IWebElement> options = webElement.FindElements(By.XPath("./li")).ToList();
            foreach (var item in options)
            {
                if (item.Text == selectSite)
                {
                    ClickOnElement(item);
                    break;
                }
            }
            ClickOnElement(addSiteButton);
            return this;
        }
    }
}
