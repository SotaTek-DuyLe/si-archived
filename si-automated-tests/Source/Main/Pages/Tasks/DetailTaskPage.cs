﻿using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class DetailTaskPage : BasePageCommonActions
    {
        private readonly By taskTitle = By.XPath("//span[text()='Task']");
        private readonly By inspectionBtn = By.XPath("//button[@title='Inspect']");
        private readonly By locationName = By.CssSelector("a[class='typeUrl']");
        private readonly By serviceName = By.XPath("//div[text()='Service']/following-sibling::div");
        private readonly By serviceGroupName = By.XPath("//div[text()='Service Group']/following-sibling::div");
        private readonly By site = By.XPath("//div[text()='Site']/following-sibling::a");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly By verdictTab = By.CssSelector("a[aria-controls='verdict-tab']");
        public readonly By OnHoldImg = By.XPath("//img[@class='header-status-icon' and @src='/web/content/images/tasks/onHold.ico']");
        private readonly By partyLink = By.XPath("//div[text()='Party']/following-sibling::a");

        //INSPECTION POPUP
        private readonly By inspectionPopupTitle = By.XPath("//h4[text()='Create ']");
        private readonly By sourceDd = By.CssSelector("select#source");
        private readonly By inspectionTypeDd = By.CssSelector("select#inspection-type");
        private readonly By validFromInput = By.CssSelector("input#valid-from");
        private readonly By validToInput = By.CssSelector("input#valid-to");
        private readonly By allocatedUnitDd = By.CssSelector("select#allocated-unit");
        private readonly By allocatedUserDd = By.CssSelector("select#allocated-user");
        private readonly By noteInput = By.CssSelector("textarea#note");
        private readonly By createBtn = By.XPath("//button[text()='Create']");
        private readonly By cancelBtn = By.XPath("//button[text()='Create']/preceding-sibling::button");

        //INSPECTION TAB
        private readonly By inspectionTab = By.CssSelector("a[aria-controls='taskInspections-tab']");
        private readonly By addNewItemInSpectionBtn = By.XPath("//div[@id='taskInspections-tab']//button[text()='Add New Item']");
        private readonly By allRowInInspectionTabel = By.XPath("//div[@id='taskInspections-tab']//div[@class='grid-canvas']/div");
        private readonly By firstInspectionRow = By.XPath("//div[@id='taskInspections-tab']//div[@class='grid-canvas']/div[1]");
        private const string columnInRowInspectionTab = "//div[@id='taskInspections-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";

        //DETAIL TAB
        private readonly By taskNotesInput = By.CssSelector("textarea[id='taskNotes.id']");
        public readonly By taskStateDd = By.CssSelector("select[id='taskState.id']");
        public readonly By ScheduleDateInput = By.CssSelector("input[id='scheduledDate.id']");
        public readonly By completionDateInput = By.CssSelector("input[id='completionDate.id']");
        public readonly By endDateInput = By.CssSelector("input[id='endDate.id']");
        private readonly By resolutionCode = By.CssSelector("select[id='resolutionCode.id']");

        //DYNAMIC LOCATOR
        private const string sourceName = "//select[@id='source']/option[text()='{0}']";
        private const string inspectionTypeOption = "//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//select[@id='allocated-unit']/option[text()='{0}']";
        private const string allocatedUserOption = "//select[@id='allocated-user']/option[text()='{0}']";
        private const string taskStateOption = "//select[@id='taskState.id']/option[text()='{0}']";
        private const string taskStateOptionAndOrder = "//select[@id='taskState.id']/option[{0}]";

        public readonly By IndicatorTab = By.XPath("//a[@aria-controls='objectIndicators-tab']");
        #region IndicatorTab
        public readonly By IndicatorIframe = By.XPath("//iframe[@id='objectIndicators']");
        private string indicatorTable = "//div[@id='object-indicators-grid']//div[@class='grid-canvas']";
        private string indicatorRow = "./div[contains(@class, 'slick-row')]";
        private string idCell = "./div[@class='slick-cell l0 r0']";
        private string indicatorCell = "./div[@class='slick-cell l1 r1']";
        private string inheritedCell = "./div[@class='slick-cell l6 r6']";
        private string retireButtonCell = "./div[@class='slick-cell l7 r7']//button";

        public TableElement IndicatorTableEle
        {
            get => new TableElement(indicatorTable, indicatorRow, new List<string>() { idCell, indicatorCell, inheritedCell, retireButtonCell });
        }

        public DetailTaskPage ClickOnRetireButton(int rowIdx)
        {
            IndicatorTableEle.ClickCell(rowIdx, IndicatorTableEle.GetCellIndex(retireButtonCell));
            return this;
        }
        #endregion

        public readonly By SubscriptionTab = By.XPath("//a[@aria-controls='subscriptions-tab']");
        #region SubscriptionTab
        public readonly By AddNewSubscriptionButton = By.XPath("//button[@data-bind='click: createSubscription']");
        public readonly By SubscriptionIFrame = By.XPath("//div[@id='subscriptions-tab']//iframe");
        private readonly string SubcriptionTable = "//div[@class='grid-canvas']";
        private readonly string SubscriptionRow = "./div[contains(@class, 'slick-row')]";
        private readonly string SubscriptionIdCell = "./div[contains(@class, 'l0')]";
        private readonly string SubscriptionContractIdCell = "./div[contains(@class, 'l1')]";
        private readonly string SubscriptionContractCell = "./div[contains(@class, 'l2')]";
        private readonly string SubscriptionMobileCell = "./div[contains(@class, 'l3')]";
        private readonly string SubscriptionStateCell = "./div[contains(@class, 'l4')]";
        private readonly string SubscriptionStartDateCell = "./div[contains(@class, 'l5')]";
        private readonly string SubscriptionEndDateCell = "./div[contains(@class, 'l6')]";
        private readonly string SubscriptionNotesCell = "./div[contains(@class, 'l7')]";
        private readonly string SubscriptionSubjectCell = "./div[contains(@class, 'l8')]";
        private readonly string SubscriptionSubjectDesCell = "./div[contains(@class, 'l9')]";

        public TableElement SubscriptionTableEle
        {
            get => new TableElement(SubcriptionTable, SubscriptionRow,
                new List<string>() {
                    SubscriptionIdCell, SubscriptionContractIdCell, SubscriptionContractCell,
                    SubscriptionMobileCell, SubscriptionStateCell, SubscriptionStartDateCell,
                    SubscriptionEndDateCell, SubscriptionNotesCell, SubscriptionSubjectCell, SubscriptionSubjectDesCell
                });
        }

        [AllureStep]
        public DetailTaskPage VerifyNewSubscription(string id, string firstName, string lastName, string mobile, string subjectDescription)
        {
            int newIdx = SubscriptionTableEle.GetRows().Count - 1;
            VerifyCellValue(SubscriptionTableEle, newIdx, 0, id);
            VerifyCellValue(SubscriptionTableEle, newIdx, 2, firstName + " " + lastName);
            VerifyCellValue(SubscriptionTableEle, newIdx, 3, mobile);
            string subjectDescriptionCellValue = SubscriptionTableEle.GetCellValue(newIdx, 9).AsString();
            Assert.IsTrue(subjectDescription.Contains(subjectDescriptionCellValue));
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyColumnsDisplay(List<string> columnNames)
        {
            var headerEles = GetAllElements(By.XPath("//div[contains(@class, 'slick-header-columns')]//span[@class='slick-column-name']"));
            foreach (var item in headerEles)
            {
                Assert.IsTrue(columnNames.Contains(item.Text));
            }
            return this;
        }
        #endregion

        [AllureStep]
        public DetailTaskPage IsDetailTaskPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(taskTitle);
            return this;
        }
        [AllureStep]
        public string GetLocationName()
        {
            return GetElementText(locationName);
        }
        [AllureStep]
        public DetailTaskPage ClickOnInspectionBtn()
        {
            ClickOnElement(inspectionBtn);
            return this;
        }

        [AllureStep]
        public string GetTaskId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/tasks/", "").Trim();
        }

        [AllureStep]
        public string GetServiceName()
        {
            return GetElementText(serviceName);
        }
        [AllureStep]
        public string GetServiceGroup()
        {
            return GetElementText(serviceGroupName);
        }
        [AllureStep]
        public string GetSite()
        {
            return GetElementText(site);
        }

        [AllureStep]
        public DetailTaskPage ClickOnPartyLink()
        {
            ClickOnElement(partyLink);
            return this;
        }

        //INSPECTION POPUP
        [AllureStep]
        public DetailTaskPage IsInspectionPopup()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(inspectionPopupTitle);
            Assert.IsTrue(IsControlDisplayed(sourceDd));
            Assert.IsTrue(IsControlDisplayed(inspectionTypeDd));
            Assert.IsTrue(IsControlDisplayed(validFromInput));
            Assert.IsTrue(IsControlDisplayed(validToInput));
            Assert.IsTrue(IsControlDisplayed(allocatedUnitDd));
            Assert.IsTrue(IsControlDisplayed(allocatedUserDd));
            Assert.IsTrue(IsControlDisplayed(noteInput));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            //Create btn disabled
            Assert.AreEqual(GetAttributeValue(createBtn, "disabled"), "true");
            //Mandatory field
            Assert.AreEqual(GetCssValue(inspectionTypeDd, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(allocatedUnitDd, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyDefaultValue(string sourceName)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceName);
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }
        [AllureStep]
        public DetailTaskPage ClickAndVerifySourceDd(string[] sourceNameList)
        {
            ClickOnElement(sourceDd);
            //Verify
            foreach (string source in sourceNameList)
            {
                Assert.IsTrue(IsControlDisplayed(sourceName, source));
            }
            return this;
        }
        [AllureStep]
        public DetailTaskPage ClickInspectionTypeDdAndSelectValue(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }
        [AllureStep]
        public DetailTaskPage ClickAllocatedUnitAndSelectValue(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }
        [AllureStep]
        public DetailTaskPage ClickAllocatedUserAndSelectValue(string allocatedUserValue)
        {
            ClickOnElement(allocatedUserDd);
            ClickOnElement(allocatedUserOption, allocatedUserValue);
            return this;
        }
        [AllureStep]
        public DetailTaskPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }
        [AllureStep]
        public DetailTaskPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }
        [AllureStep]
        public DetailTaskPage InputValidFrom(string validFromValue)
        {
            SendKeys(validFromInput, validFromValue);
            return this;
        }
        [AllureStep]
        public DetailTaskPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }

        //INSPECTION TAB
        [AllureStep]
        public DetailTaskPage ClickInspectionTab()
        {
            ClickOnElement(inspectionTab);
            return this;
        }
        [AllureStep]
        public List<InspectionModel> getAllInspection()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(addNewItemInSpectionBtn);
            List<InspectionModel> allModel = new List<InspectionModel>();
            List<IWebElement> allRow = GetAllElements(allRowInInspectionTabel);
            List<IWebElement> allId = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[0]);
            List<IWebElement> allInspectionType = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[1]);
            List<IWebElement> allCreatedDate = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[2]);
            List<IWebElement> allCreatedByUser = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[3]);
            List<IWebElement> allAssignedUser = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[4]);
            List<IWebElement> allAllocatedUser = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[5]);
            List<IWebElement> allStatus = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[6]);
            List<IWebElement> allValidFrom = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[7]);
            List<IWebElement> allValidTo = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[8]);
            List<IWebElement> allCompletionDate = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[9]);
            List<IWebElement> allCancelledDate = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[10]);

            for (int i = 0; i < allRow.Count; i++)
            {
                string id = GetElementText(allId[i]);
                string inspectionType = GetElementText(allInspectionType[i]);
                string createdDate = GetElementText(allCreatedDate[i]);
                string createdByUser = GetElementText(allCreatedByUser[i]);
                string assignedUser = GetElementText(allAssignedUser[i]);
                string allocatedUnit = GetElementText(allAllocatedUser[i]);
                string status = GetElementText(allStatus[i]);
                string validFrom = GetElementText(allValidFrom[i]);
                string validTo = GetElementText(allValidTo[i]);
                string completionDate = GetElementText(allCompletionDate[i]);
                string cancelledDate = GetElementText(allCancelledDate[i]);
                allModel.Add(new InspectionModel(id, inspectionType, createdDate, createdByUser, assignedUser, allocatedUnit, status, validFrom, validTo, completionDate, cancelledDate));
            }
            return allModel;
        }
        [AllureStep]
        public DetailTaskPage VerifyInspectionCreated(InspectionModel inspectionModelActual, string id, string inspectionType, string createdByUser, string assignedUser, string allocatedUnit, string status, string validFrom, string validTo)
        {
            Assert.AreEqual(id, inspectionModelActual.ID);
            Assert.AreEqual(inspectionType, inspectionModelActual.inspectionType);
            Assert.AreEqual(createdByUser, inspectionModelActual.createdByUser);
            Assert.AreEqual(assignedUser, inspectionModelActual.assignedUser);
            Assert.AreEqual(allocatedUnit, inspectionModelActual.allocatedUnit);
            Assert.AreEqual(status, inspectionModelActual.status);
            Assert.AreEqual(validFrom, inspectionModelActual.validFrom);
            Assert.AreEqual(validTo, inspectionModelActual.validTo);
            return this;
        }
        [AllureStep]
        public DetailTaskPage ClickAddNewInspectionItem()
        {
            ClickOnElement(addNewItemInSpectionBtn);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage ClickOnFirstInspection()
        {
            DoubleClickOnElement(firstInspectionRow);
            return PageFactoryManager.Get<DetailInspectionPage>();
        }

        //DETAIL TAB
        [AllureStep]
        public DetailTaskPage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyFieldAfterBulkUpdate(string topNoteValue, string commonNoteValue, string endDateValue, string taskStateValue, string completionDateValue, string resolutionCodeValue)
        {
            string firstNote = GetAttributeValue(taskNotesInput, "value").Split(Environment.NewLine)[0];
            string commonNote = GetAttributeValue(taskNotesInput, "value").Split(Environment.NewLine)[1];
            Assert.AreEqual(topNoteValue, firstNote);
            Assert.AreEqual(commonNoteValue, commonNote);
            //Assert.AreEqual(endDateValue, GetAttributeValue(endDateInput, "value"));
            Assert.AreEqual(taskStateValue, GetFirstSelectedItemInDropdown(taskStateDd));
            //Assert.AreEqual(completionDateValue, GetAttributeValue(completionDateInput, "value"));
            Assert.AreEqual(resolutionCodeValue, GetFirstSelectedItemInDropdown(resolutionCode));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyFieldAfterBulkUpdate(string topNoteValue, string endDateValue, string taskStateValue, string completionDateValue, string resolutionCodeValue)
        {
            Assert.AreEqual(topNoteValue, GetAttributeValue(taskNotesInput, "value"));
            Assert.AreEqual(endDateValue, GetAttributeValue(endDateInput, "value"));
            Assert.AreEqual(taskStateValue, GetFirstSelectedItemInDropdown(taskStateDd));
            Assert.AreEqual(completionDateValue, GetAttributeValue(completionDateInput, "value"));
            Assert.AreEqual(resolutionCodeValue, GetFirstSelectedItemInDropdown(resolutionCode));
            return this;
        }
        [AllureStep]
        public DetailTaskPage ClickOnTaskStateDd()
        {
            ClickOnElement(taskStateDd);
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyCurrentTaskState(string currentTaskStateValue)
        {
            Assert.AreEqual(currentTaskStateValue, GetFirstSelectedItemInDropdown(taskStateDd));
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyOrderInTaskStateDd(string[] taskStateValues)
        {
            for(int i = 0; i < taskStateValues.Length; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(taskStateOptionAndOrder, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }

        [AllureStep]
        public DetailTaskPage SelectAnyTaskStateInDd(string taskStateValue)
        {
            ClickOnElement(taskStateOption, taskStateValue);
            return this;
        }
        [AllureStep]
        public string GetCompletedDateDisplayed()
        {
            return GetAttributeValue(completionDateInput, "value");
        }
        [AllureStep]
        public string GetEndDateDisplayed()
        {
            return GetAttributeValue(endDateInput, "value");
        }
        [AllureStep]
        public DetailTaskPage VerifyCompletedDateNotEmpty()
        {
            string completedDateDisplayed = GetAttributeValue(completionDateInput, "value");
            Assert.IsFalse(string.IsNullOrEmpty(completedDateDisplayed));
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyTaskNotesValue(string noteValue)
        {
            Assert.AreEqual(noteValue, GetAttributeValue(taskNotesInput, "value"));
            return this;
        }

        //HISTORY TAB
        private readonly By titleTaskLineFirstServiceUpdate = By.XPath("(//strong[text()='Update'])[1]");
        private readonly By titleTaskLineSecondServiceUpdate = By.XPath("//strong[contains(text(), 'Service Update')]");
        private readonly By userFirstServiceUpdate = By.XPath("(//strong[text()='Update'])[1]/parent::div/following-sibling::div/strong[1]");
        private readonly By timeFirstServiceUpdate = By.XPath("(//strong[text()='Update'])[1]/parent::div/following-sibling::div/strong[2]");

        private readonly By userSecondServiceUpdate = By.XPath("(//strong[contains(text(), 'Service Update')])[1]/parent::div/following-sibling::div/strong[1]");
        private readonly By timeSecondServiceUpdate = By.XPath("(//strong[contains(text(), 'Service Update')])[1]/parent::div/following-sibling::div/strong[2]");

        private readonly By contentFirstServiceUpdate = By.XPath("(//strong[contains(text(), 'Service Update')]/following-sibling::div)[1]");
        private readonly By contentSecondServiceUpdate = By.XPath("(//strong[contains(text(), 'Service Update')]/following-sibling::div)[2]");
        private readonly By updateTitle = By.XPath("(//strong[text()='Update'])[1]");
        private readonly By createTitle = By.XPath("(//strong[text()='Create'])[1]");
        private readonly By userUpdate = By.XPath("(//strong[text()='Update']/parent::div/following-sibling::div/strong[1])[1]");
        private readonly By timeUpdate = By.XPath("(//strong[text()='Update']/parent::div/following-sibling::div/strong[2])[1]");
        private readonly By contentUpdate = By.XPath("(//strong[text()='Update']/following-sibling::div)[1]");
        private readonly By completedDateUpdate = By.XPath("//strong[text()='Update']/following-sibling::div/span[text()='Completed date']/following-sibling::span[1]");
        private readonly By stateUpdate = By.XPath("//strong[text()='Update']/following-sibling::div/span[text()='State']/following-sibling::span[1]");
        private readonly By endDateUpdate = By.XPath("//strong[text()='Update']/following-sibling::div/span[text()='End date']/following-sibling::span[1]");
        private readonly By actualAssetQtyTaskLineUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[1]/following-sibling::div//span[text()='ActualAssetQuantity']/following-sibling::span[1]");
        private readonly By actualProductQtyTaskLineUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[1]/following-sibling::div//span[text()='ActualProductQuantity']/following-sibling::span[1]");
        private readonly By stateTaskLineUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[1]/following-sibling::div//span[text()='State']/following-sibling::span[1]");
        private readonly By resolutionCodeTaskLineUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[1]/following-sibling::div//span[text()='Resolution Code']/following-sibling::span[1]");
        private readonly By completedDateTaskLineUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[1]/following-sibling::div//span[text()='Completed Date']/following-sibling::span[1]");
        private readonly By autoConfirmedTaskLineUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[1]/following-sibling::div//span[text()='Auto Confirmed']/following-sibling::span[1]");

        //DYNAMIC
        private readonly string createdValue = "//strong[contains(text(), 'Service Create')]/following-sibling::div//span[text()='{0}']/following-sibling::span[1]";
        private readonly string updatedValue = "//strong[contains(text(), 'Service Update')]/following-sibling::div//span[text()='{0}']/following-sibling::span[1]";

        [AllureStep]
        public DetailTaskPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyTitleTaskLineFirstServiceUpdate()
        {
            Assert.IsTrue(IsControlDisplayed(titleTaskLineFirstServiceUpdate), "Title Task Line is not displayed");
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyTitleTaskLineSecondServiceUpdate()
        {
            Assert.IsTrue(IsControlDisplayed(titleTaskLineSecondServiceUpdate));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyTitleUpdate()
        {
            Assert.IsTrue(IsControlDisplayed(updateTitle));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyTitleCreate()
        {
            Assert.IsTrue(IsControlDisplayed(createTitle));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyHistoryTabFirstAfterBulkUpdating(string userUpdatedExp, string timeUpdatedExp, string[] fieldInServiceUpdate, string[] valueExpected)
        {
            Assert.AreEqual(userUpdatedExp, GetElementText(userFirstServiceUpdate));
            Assert.IsTrue(timeUpdatedExp.Contains(GetElementText(timeFirstServiceUpdate)));
            string[] allInfoDisplayed = GetElementText(contentFirstServiceUpdate).Split(Environment.NewLine);
            for(int i = 0; i < allInfoDisplayed.Length; i++)
            {
                Assert.AreEqual(fieldInServiceUpdate[i] + ": " + valueExpected[i] + ".", allInfoDisplayed[i]);
            }
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyHistoryTabSecondAfterBulkUpdating(string userUpdatedExp, string timeUpdatedExp, string[] fieldInServiceUpdate, string[] valueExpected)
        {
            Assert.AreEqual(userUpdatedExp, GetElementText(userSecondServiceUpdate));
            Assert.AreEqual(timeUpdatedExp, GetElementText(timeSecondServiceUpdate));
            string[] allInfoDisplayed = GetElementText(contentSecondServiceUpdate).Split(Environment.NewLine);
            for (int i = 0; i < allInfoDisplayed.Length; i++)
            {
                Assert.AreEqual(fieldInServiceUpdate[i] + ": " + valueExpected[i] + ".", allInfoDisplayed[i]);
            }
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyHistoryTabUpdate(string userUpdatedExp, string timeUpdatedExp, string[] fieldInServiceUpdate, string[] valueExpected)
        {
            Assert.AreEqual(userUpdatedExp, GetElementText(userUpdate));
            Assert.AreEqual(timeUpdatedExp, GetElementText(timeUpdate));
            string[] allInfoDisplayed = GetElementText(contentUpdate).Split(Environment.NewLine);
            for (int i = 0; i < allInfoDisplayed.Length; i++)
            {
                Assert.AreEqual(fieldInServiceUpdate[i] + ": " + valueExpected[i] + ".", allInfoDisplayed[i]);
            }
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyHistoryTabUpdate(string userUpdatedExp, string timeUpdatedExp, string completedDateExp, string stateExp, string endDateExp)
        {
            Assert.AreEqual(userUpdatedExp, GetElementText(userUpdate));
            Assert.AreEqual(timeUpdatedExp, GetElementText(timeUpdate));
            Assert.AreEqual(completedDateExp + ".", GetElementText(completedDateUpdate));
            Assert.AreEqual(stateExp + ".", GetElementText(stateUpdate));
            Assert.AreEqual(endDateExp + ".", GetElementText(endDateUpdate));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyHistoryTabFirstAfterChangingStatus(string userUpdatedExp, string timeUpdatedExp, string actualAssetExp, string actualProductExp, string stateExp, string resolutionCodeExp, string completedDateExp, string autoConfirmedExp)
        {
            Assert.AreEqual(userUpdatedExp, GetElementText(userFirstServiceUpdate));
            Assert.IsTrue(timeUpdatedExp.Contains(GetElementText(timeFirstServiceUpdate)));
            Assert.AreEqual(actualAssetExp + ".", GetElementText(actualAssetQtyTaskLineUpdate));
            Assert.AreEqual(actualProductExp + ".", GetElementText(actualProductQtyTaskLineUpdate));
            Assert.AreEqual(stateExp + ".", GetElementText(stateTaskLineUpdate));
            //Assert.AreEqual(resolutionCodeExp + ".", GetElementText(resolutionCodeTaskLineUpdate));
            Assert.IsTrue(completedDateExp.Contains(GetElementText(completedDateTaskLineUpdate).Replace(".", "").Trim()), "Expected: " + completedDateExp + " but found: " + GetElementText(completedDateTaskLineUpdate));
            Assert.AreEqual(autoConfirmedExp + ".", GetElementText(autoConfirmedTaskLineUpdate));
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyTaskLineAfterCreatedState(string[] titleValues, string[] expValues)
        {
            for(int i = 0; i < titleValues.Length; i++)
            {
                Assert.AreEqual(expValues[i], GetElementText(createdValue, titleValues[i]), "Value at " + titleValues[i] + " is not correct");
            }
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyTaskLineAfterUpdatedState(string[] titleValues, string[] expValues)
        {
            for (int i = 0; i < titleValues.Length; i++)
            {
                Assert.AreEqual(expValues[i], GetElementText(updatedValue, titleValues[i]), "Value at " + titleValues[i] + " is not correct");
            }
            return this;
        }

        //VERDICT TAB
        private readonly By taskInformationVerdictTab = By.CssSelector("a[aria-controls='verdictInformation-tab']");
        private readonly By completionDateVerdictInput = By.XPath("//label[text()='Completion Date']/following-sibling::textarea");
        private readonly By confirmationMethodVerdictInput = By.XPath("//label[text()='Confirmation Method']/following-sibling::textarea");
        private readonly By taskStateVerdictInput = By.XPath("//label[text()='Task State']/following-sibling::textarea");
        private readonly By resolutionCodeVerdictInput = By.XPath("//label[text()='Resolution Code']/following-sibling::textarea");
        private readonly By taskLineVerdictTab = By.CssSelector("a[aria-controls='verdictTasklines-tab']");
        private readonly By dateTimeFirstLineVerdictTab = By.CssSelector("tbody[data-bind='foreach: verdictTasklines']>tr:nth-child(1)>td:nth-child(1)");
        private readonly By tasklineStateFirstVerdictTab = By.CssSelector("tbody[data-bind='foreach: verdictTasklines']>tr:nth-child(1)>td:nth-child(8)");
        private readonly By confirmationMethodFirstVerdictTab = By.CssSelector("tbody[data-bind='foreach: verdictTasklines']>tr:nth-child(1)>td:nth-child(10)");
        private readonly By productFirstVerdictTab = By.CssSelector("tbody[data-bind='foreach: verdictTasklines']>tr:nth-child(1)>td:nth-child(4)");

        private readonly By dateTimeSecondLineVerdictTab = By.CssSelector("tbody[data-bind='foreach: verdictTasklines']>tr:nth-child(2)>td:nth-child(1)");
        private readonly By tasklineStateSecondVerdictTab = By.CssSelector("tbody[data-bind='foreach: verdictTasklines']>tr:nth-child(2)>td:nth-child(8)");
        private readonly By confirmationMethodSecondVerdictTab = By.CssSelector("tbody[data-bind='foreach: verdictTasklines']>tr:nth-child(2)>td:nth-child(10)");
        private readonly By productSecondVerdictTab = By.CssSelector("tbody[data-bind='foreach: verdictTasklines']>tr:nth-child(2)>td:nth-child(4)");

        [AllureStep]
        public DetailTaskPage ClickOnVerdictTab()
        {
            ClickOnElement(verdictTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        //==> Verdict tab -> Information
        [AllureStep]
        public DetailTaskPage ClickOnTaskInformation()
        {
            ClickOnElement(taskInformationVerdictTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyTaskInformationAfterBulkUpdating(string completionDateExp, string taskStateExp, string resolutionCodeExp, string confirmationMethodExp)
        {
            //Assert.AreEqual(completionDateExp, GetAttributeValue(completionDateVerdictInput, "value"), "Completion Date is not correct");
            Assert.AreEqual(taskStateExp, GetAttributeValue(taskStateVerdictInput, "value"), "Task State is not correct");
            Assert.AreEqual(resolutionCodeExp, GetAttributeValue(resolutionCodeVerdictInput, "value"), "Resolution Code is not correct");
            //Assert.AreEqual(confirmationMethodExp, GetAttributeValue(confirmationMethodVerdictInput, "value"), "Confirmation method is not correct");
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyTaskCompleteDate(string completionDateExp)
        {
            Assert.AreEqual(completionDateExp, GetAttributeValue(completionDateVerdictInput, "value"), "Completion Date is not correct");
            return this;
        }

        //==> Verdict tab -> Task line
        [AllureStep]
        public DetailTaskPage ClickOnTaskLineVerdictTab()
        {
            ClickOnElement(taskLineVerdictTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyFirstTaskLineStateVerdictTab(string dateTimeExp, string stateExp, string confirmMethodExp, string productExp)
        {
            Assert.AreEqual(dateTimeExp, GetElementText(dateTimeFirstLineVerdictTab));
            Assert.AreEqual(stateExp, GetElementText(tasklineStateFirstVerdictTab));
            Assert.AreEqual(confirmMethodExp, GetElementText(confirmationMethodFirstVerdictTab));
            Assert.AreEqual(productExp, GetElementText(productFirstVerdictTab));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifySecondTaskLineStateVerdictTab(string dateTimeExp, string stateExp, string confirmMethodExp, string productExp)
        {
            Assert.AreEqual(dateTimeExp, GetElementText(dateTimeSecondLineVerdictTab));
            Assert.AreEqual(stateExp, GetElementText(tasklineStateSecondVerdictTab));
            Assert.AreEqual(confirmMethodExp, GetElementText(confirmationMethodSecondVerdictTab));
            Assert.AreEqual(productExp, GetElementText(productSecondVerdictTab));
            return this;
        }
        [AllureStep]
        public string CompareDueDateWithTimeNow(TaskDBModel taskDBModel, string timeCompleted)
        {
            DateTime dateTime = CommonUtil.GetLocalTimeNow();
            if(DateTime.Compare(taskDBModel.taskduedate, dateTime) < 0) 
            {
                return taskDBModel.taskduedate.ToString(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            }
            return timeCompleted;
        }

        //TASK LINE TAB
        private readonly By taskLineTab = By.CssSelector("a[aria-controls='taskLines-tab']");
        private readonly By numberOfTaskLine = By.XPath("//tbody//tr[contains(@data-bind,'with: $data.getFields()')]");
        private readonly By addNewItemInTaskLine = By.CssSelector("div[id='taskLines-tab'] button[title='Add New Item']");

        private readonly By firstStateTaskLine = By.CssSelector("tbody>tr:nth-child(1) select[id='itemState.id']");
        private readonly By firstProductTaskLine = By.XPath("//tbody/tr[1]//echo-select[contains(@params, 'name: product')]/select");
        private readonly By allProductFirstTaskLine = By.XPath("//tbody/tr[1]//echo-select[contains(@params, 'name: product')]/select/optgroup/option");
        private readonly By allProductSecondTaskLine = By.XPath("//tbody/tr[2]//echo-select[contains(@params, 'name: product')]/select/optgroup/option");
        private readonly By firstResolutionCodeTaskLine = By.CssSelector("tbody>tr:nth-child(1) select[id='resCode.id']");
        private readonly By firstOrderTaskLine = By.CssSelector("tbody>tr:nth-child(1) input[id='order.id']");
        private readonly By firstTypeTaskLine = By.CssSelector("tbody>tr:nth-child(1) select[id='taskLineType.id']");
        private readonly By firstTypeAssetTaskLine = By.XPath("//tbody/tr[1]//echo-select[contains(@params, 'name: assetType')]/select");
        private readonly By firstActualAssetTaskLine = By.CssSelector("tbody>tr:nth-child(1) input[id='actualAssetQuantity.id']");
        private readonly By firstScheduledAssetTaskLine = By.CssSelector("tbody>tr:nth-child(1) input[id='scheduledAssetQuantity.id']");
        private readonly By firstActualProductTaskLine = By.CssSelector("tbody>tr:nth-child(1) input[id='actualProductQuantity.id']");
        private readonly By firstScheduledProductTaskLine = By.CssSelector("tbody>tr:nth-child(1) input[id='scheduledProductQuantity.id']");
        private readonly By firstUnitProductTaskLine = By.XPath("//tbody/tr[1]//echo-select[contains(@params, 'name: unitOfMeasure')]/select");
        private readonly By firstDestinationTaskLine = By.CssSelector("tbody>tr:nth-child(1) select[id='destinationSite.id']");
        private readonly By firstSiteProductTaskLine = By.CssSelector("tbody>tr:nth-child(1) select[id='siteProduct.id']");
        private readonly By secondStateTaskLine = By.CssSelector("tbody>tr:nth-child(2) select[id='itemState.id']");
        private readonly By secondProductTaskLine = By.XPath("//tbody/tr[2]//echo-select[contains(@params, 'name: product')]/select");
        private readonly By secondResolutionCodeTaskLine = By.CssSelector("tbody>tr:nth-child(2) select[id='resCode.id']");
        private readonly By secondTypeTaskLine = By.CssSelector("tbody>tr:nth-child(2) select[id='taskLineType.id']");
        private readonly By secondTypeAssetTaskLine = By.XPath("//tbody/tr[2]//echo-select[contains(@params, 'name: product')]/select");
        //DYNAMIC
        private readonly string optionInStatusFirstRow = "//tbody/tr[1]//select[@id='itemState.id']/option[{0}]";
        private readonly string TaskLineTable = "//div[@id='taskLines-tab']//table";
        private readonly string TaskLineRow = "./tbody//tr[contains(@data-bind,'with: $data.getFields()')]";
        private readonly string TaskLineOrderCell = "./td//input[@id='order.id']";
        private readonly string TaskLineTypeCell = "./td//select[@id='taskLineType.id']";
        private readonly string AssetTypeCell = "./td//echo-select[contains(@params, 'assetType')]//select";
        private readonly string AssetActualCell = "./td//input[@id='actualAssetQuantity.id']";
        private readonly string AssetScheduleCell = "./td//input[@id='scheduledAssetQuantity.id']";
        private readonly string ProductCell = "./td//echo-select[contains(@params, 'product')]//select";
        private readonly string ProductActualCell = "./td//input[@id='actualProductQuantity.id']";
        private readonly string ProductScheduleCell = "./td//input[@id='scheduledProductQuantity.id']";
        private readonly string UnitCell = "./td//echo-select[contains(@params, 'unitOfMeasure')]//select";
        private readonly string SiteDestinationCell = "./td//select[@id='destinationSite.id']";
        private readonly string SiteProductCell = "./td//select[@id='siteProduct.id']";
        private readonly string StateCell = "./td//select[@id='itemState.id']";
        private readonly string ResolutionCodeCell = "./td//select[@id='resCode.id']";
        private readonly string typeOptionInSecondTaskLineRow = "//tr[2]//select[@id='taskLineType.id']/option[text()='{0}']";
        private readonly string assetTypeOptionInSecondTaskLineRow = "//tbody/tr[2]//echo-select[contains(@params, 'name: assetType')]/select//option[text()='{0}']";
        //private readonly string productOptionInSecondTaskLineRow = "//tbody/tr[2]//echo-select[contains(@params, 'name: assetType')]/select//option[text()='{0}']";

        public TableElement TaskLineTableEle
        {
            get => new TableElement(TaskLineTable, TaskLineRow, new List<string>()
            {
                TaskLineOrderCell, TaskLineTypeCell, AssetTypeCell,
                AssetActualCell, AssetScheduleCell, ProductCell,
                ProductActualCell, ProductScheduleCell, UnitCell,
                SiteDestinationCell, SiteProductCell, StateCell, ResolutionCodeCell
            });
        }

        [AllureStep]
        public DetailTaskPage VerifyTaskLineState(string state)
        {
            VerifyCellValue(TaskLineTableEle, 0, 11, state);
            return this;
        }

        [AllureStep]
        public DetailTaskPage InputTaskLineState(string state)
        {
            TaskLineTableEle.SetCellValue(0, 11, state);
            return this;
        }

        [AllureStep]
        public DetailTaskPage DoubleClickFirstTaskLine()
        {
            TaskLineTableEle.DoubleClickRow(0);
            return this;
        }
        [AllureStep]
        public DetailTaskPage ClickOnTaskLineTab()
        {
            ClickOnElement(taskLineTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyStateOfFirstRowInTaskLineTab(string stateExp)
        {
            Assert.AreEqual(stateExp, GetFirstSelectedItemInDropdown(firstStateTaskLine));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyResoluctionCodeFirstRowInTaskLineTab(string resolutionCodeExp)
        {
            Assert.AreEqual(resolutionCodeExp, GetFirstSelectedItemInDropdown(firstResolutionCodeTaskLine));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyAllColumnInFirstRowDisabled()
        {
            //Disabled
            Assert.AreEqual("true", GetAttributeValue(firstOrderTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstTypeTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstTypeAssetTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstActualAssetTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstScheduledAssetTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstProductTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstActualProductTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstScheduledProductTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstUnitProductTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstDestinationTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstSiteProductTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstStateTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstResolutionCodeTaskLine, "disabled"));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyFirstTaskLineAfterBulkUpdate(string productExp, string stateExp, string resolutionCodeExp)
        {
            Assert.AreEqual(productExp, GetFirstSelectedItemInDropdown(firstProductTaskLine));
            Assert.AreEqual(stateExp, GetFirstSelectedItemInDropdown(firstStateTaskLine));
            Assert.AreEqual(resolutionCodeExp, GetFirstSelectedItemInDropdown(firstResolutionCodeTaskLine));
            //Disabled
            Assert.AreEqual("true", GetAttributeValue(firstProductTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstStateTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstResolutionCodeTaskLine, "disabled"));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyFirstTaskLineAfterUpdateStatus(string assetValue, string stateExp, string productValue)
        {
            Assert.AreEqual(assetValue, GetAttributeValue(firstActualAssetTaskLine, "value"));
            Assert.AreEqual(assetValue, GetAttributeValue(firstScheduledAssetTaskLine, "value"));
            Assert.AreEqual(stateExp, GetFirstSelectedItemInDropdown(firstStateTaskLine));
            Assert.AreEqual(productValue, GetAttributeValue(firstActualProductTaskLine, "value"));
            Assert.AreEqual(productValue, GetAttributeValue(firstScheduledProductTaskLine, "value"));
            //Disabled
            Assert.AreEqual("true", GetAttributeValue(firstProductTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstStateTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(firstResolutionCodeTaskLine, "disabled"));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifySecondTaskLineAfterBulkUpdate(string productExp, string stateExp, string resolutionCodeExp)
        {
            Assert.AreEqual(productExp, GetFirstSelectedItemInDropdown(secondProductTaskLine));
            Assert.AreEqual(stateExp, GetFirstSelectedItemInDropdown(secondStateTaskLine));
            Assert.AreEqual(resolutionCodeExp, GetFirstSelectedItemInDropdown(secondResolutionCodeTaskLine));
            //Disabled
            Assert.AreEqual("true", GetAttributeValue(secondProductTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(secondStateTaskLine, "disabled"));
            Assert.AreEqual("true", GetAttributeValue(secondResolutionCodeTaskLine, "disabled"));
            return this;
        }
        [AllureStep]
        public DetailTaskPage VerifyDisplayNoRecordDisplayed()
        {
            Assert.IsTrue(IsControlUnDisplayed(numberOfTaskLine));
            return this;
        }

        [AllureStep]
        public DetailTaskPage ClickOnStateOnFirstTaskLineRowAndVerifyOrder(string[] taskStateValues)
        {
            ClickOnElement(firstStateTaskLine);
            //Verify
            for (int i = 0; i < taskStateValues.Length; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(optionInStatusFirstRow, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyCurrentUrl(string taskId)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/tasks/" + taskId, GetCurrentUrl());
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyCurrentUrlWithId0(string serviceTaskId)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/tasks/service-task-adhoc?serviceTaskId=" + serviceTaskId, GetCurrentUrl());
            return this;
        }

        [AllureStep]
        public DetailTaskPage ClickOnAddNewItemBtnTaskLinesTab()
        {
            ClickOnElement(addNewItemInTaskLine);
            return this;
        }

        [AllureStep]
        public DetailTaskPage SelectInfoSecondTaskLineRow(string typeValue, string assetTypeValue)
        {
            //Type
            ClickOnElement(secondTypeTaskLine);
            ClickOnElement(typeOptionInSecondTaskLineRow, typeValue);
            //Asset Type
            ClickOnElement(secondTypeAssetTaskLine);
            ClickOnElement(assetTypeOptionInSecondTaskLineRow, assetTypeValue);
            return this;
        }

        [AllureStep]
        public DetailTaskPage VerifyProductOfSecondRowMappingFirstRow()
        {
            ClickOnElement(firstProductTaskLine);
            List<string> firstResult = new List<string>();
            List<IWebElement> allProductFirstLine = GetAllElements(allProductFirstTaskLine);
            foreach(IWebElement webElement in allProductFirstLine)
            {
                firstResult.Add(GetElementText(webElement));
            }
            ClickOnElement(taskTitle);
            SleepTimeInSeconds(2);
            ClickOnElement(secondProductTaskLine);
            //Second product
            List<string> secondResult = new List<string>();
            List<IWebElement> allProductSecondLine = GetAllElements(allProductSecondTaskLine);
            foreach (IWebElement webElement in allProductSecondLine)
            {
                secondResult.Add(GetElementText(webElement));
            }
            Assert.AreEqual(firstResult, secondResult, "Product in two task is not matching");
            return this;
        }

    }
}
