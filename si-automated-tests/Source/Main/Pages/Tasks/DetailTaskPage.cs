using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class DetailTaskPage : BasePage
    {
        private readonly By taskTitle = By.XPath("//span[text()='Task']");
        private readonly By inspectionBtn = By.XPath("//button[@title='Inspect']");
        private readonly By locationName = By.CssSelector("a[class='typeUrl']");
        private readonly By serviceName = By.XPath("//div[text()='Service']/following-sibling::div");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly By verdictTab = By.CssSelector("a[aria-controls='verdict-tab']");

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
        private readonly By taskStateDd = By.CssSelector("select[id='taskState.id']");
        private readonly By completionDateInput = By.CssSelector("input[id='completionDate.id']");
        private readonly By endDateInput = By.CssSelector("input[id='endDate.id']");
        private readonly By resolutionCode = By.CssSelector("select[id='resolutionCode.id']");

        //DYNAMIC LOCATOR
        private const string sourceName = "//select[@id='source']/option[text()='{0}']";
        private const string inspectionTypeOption = "//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//select[@id='allocated-unit']/option[text()='{0}']";
        private const string allocatedUserOption = "//select[@id='allocated-user']/option[text()='{0}']";

        public DetailTaskPage IsDetailTaskPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(taskTitle);
            return this;
        }

        public string GetLocationName()
        {
            return GetElementText(locationName);
        }

        public DetailTaskPage ClickOnInspectionBtn()
        {
            ClickOnElement(inspectionBtn);
            return this;
        }

        public string GetServiceName()
        {
            return GetElementText(serviceName);
        }

        //INSPECTION POPUP
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

        public DetailTaskPage VerifyDefaultValue(string sourceName)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceName);
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }

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

        public DetailTaskPage ClickInspectionTypeDdAndSelectValue(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }

        public DetailTaskPage ClickAllocatedUnitAndSelectValue(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }

        public DetailTaskPage ClickAllocatedUserAndSelectValue(string allocatedUserValue)
        {
            ClickOnElement(allocatedUserDd);
            ClickOnElement(allocatedUserOption, allocatedUserValue);
            return this;
        }

        public DetailTaskPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }

        public DetailTaskPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }

        public DetailTaskPage InputValidFrom(string validFromValue)
        {
            SendKeys(validFromInput, validFromValue);
            return this;
        }

        public DetailTaskPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }

        //INSPECTION TAB
        public DetailTaskPage ClickInspectionTab()
        {
            ClickOnElement(inspectionTab);
            return this;
        }

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

        public DetailTaskPage ClickAddNewInspectionItem()
        {
            ClickOnElement(addNewItemInSpectionBtn);
            return this;
        }

        public DetailInspectionPage ClickOnFirstInspection()
        {
            DoubleClickOnElement(firstInspectionRow);
            return PageFactoryManager.Get<DetailInspectionPage>();
        }

        //DETAIL TAB
        public DetailTaskPage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public DetailTaskPage VerifyFieldAfterBulkUpdate(string topNoteValue, string commonNoteValue, string endDateValue, string taskStateValue, string completionDateValue, string resolutionCodeValue)
        {
            string firstNote = GetAttributeValue(taskNotesInput, "value").Split(Environment.NewLine)[0];
            string commonNote = GetAttributeValue(taskNotesInput, "value").Split(Environment.NewLine)[1];
            Assert.AreEqual(topNoteValue, firstNote);
            Assert.AreEqual(commonNoteValue, commonNote);
            Assert.AreEqual(endDateValue, GetAttributeValue(endDateInput, "value"));
            Assert.AreEqual(taskStateValue, GetFirstSelectedItemInDropdown(taskStateDd));
            Assert.AreEqual(completionDateValue, GetAttributeValue(completionDateInput, "value"));
            Assert.AreEqual(resolutionCodeValue, GetFirstSelectedItemInDropdown(resolutionCode));
            return this;
        }

        public DetailTaskPage VerifyFieldAfterBulkUpdate(string topNoteValue, string endDateValue, string taskStateValue, string completionDateValue, string resolutionCodeValue)
        {
            Assert.AreEqual(topNoteValue, GetAttributeValue(taskNotesInput, "value"));
            Assert.AreEqual(endDateValue, GetAttributeValue(endDateInput, "value"));
            Assert.AreEqual(taskStateValue, GetFirstSelectedItemInDropdown(taskStateDd));
            Assert.AreEqual(completionDateValue, GetAttributeValue(completionDateInput, "value"));
            Assert.AreEqual(resolutionCodeValue, GetFirstSelectedItemInDropdown(resolutionCode));
            return this;
        }

        //HISTORY TAB
        private readonly By titleTaskLineFirstServiceUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[1]");
        private readonly By titleTaskLineSecondServiceUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[2]");
        private readonly By userFirstServiceUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[1]/parent::div/following-sibling::div/strong[1]");
        private readonly By timeFirstServiceUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[1]/parent::div/following-sibling::div/strong[2]");

        private readonly By userSecondServiceUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[2]/parent::div/following-sibling::div/strong[1]");
        private readonly By timeSecondServiceUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')])[2]/parent::div/following-sibling::div/strong[2]");

        private readonly By contentFirstServiceUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')]/following-sibling::div)[1]");
        private readonly By contentSecondServiceUpdate = By.XPath("(//strong[contains(text(), 'Task Line') and contains(text(), 'Service Update')]/following-sibling::div)[2]");
        private readonly By updateTitle = By.XPath("(//strong[text()='Update'])[1]");
        private readonly By userUpdate = By.XPath("(//strong[text()='Update']/parent::div/following-sibling::div/strong[1])[1]");
        private readonly By timeUpdate = By.XPath("(//strong[text()='Update']/parent::div/following-sibling::div/strong[2])[1]");
        private readonly By contentUpdate = By.XPath("(//strong[text()='Update']/following-sibling::div)[1]");

        public DetailTaskPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public DetailTaskPage VerifyTitleTaskLineFirstServiceUpdate()
        {
            Assert.IsTrue(IsControlDisplayed(titleTaskLineFirstServiceUpdate));
            return this;
        }

        public DetailTaskPage VerifyTitleTaskLineSecondServiceUpdate()
        {
            Assert.IsTrue(IsControlDisplayed(titleTaskLineSecondServiceUpdate));
            return this;
        }

        public DetailTaskPage VerifyTitleUpdate()
        {
            Assert.IsTrue(IsControlDisplayed(updateTitle));
            return this;
        }

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

        public DetailTaskPage ClickOnVerdictTab()
        {
            ClickOnElement(verdictTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        //==> Verdict tab -> Information
        public DetailTaskPage ClickOnTaskInformation()
        {
            ClickOnElement(taskInformationVerdictTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public DetailTaskPage VerifyTaskInformationAfterBulkUpdating(string completionDateExp, string taskStateExp, string resolutionCodeExp, string confirmationMethodExp)
        {
            Assert.AreEqual(completionDateExp, GetAttributeValue(completionDateVerdictInput, "value"), "Completion Date is not correct");
            Assert.AreEqual(taskStateExp, GetAttributeValue(taskStateVerdictInput, "value"), "Task State is not correct");
            Assert.AreEqual(resolutionCodeExp, GetAttributeValue(resolutionCodeVerdictInput, "value"), "Resolution Code is not correct");
            Assert.AreEqual(confirmationMethodExp, GetAttributeValue(confirmationMethodVerdictInput, "value"), "Confirmation method is not correct");
            return this;
        }

        //==> Verdict tab -> Task line
        public DetailTaskPage ClickOnTaskLineVerdictTab()
        {
            ClickOnElement(taskLineVerdictTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public DetailTaskPage VerifyFirstTaskLineStateVerdictTab(string dateTimeExp, string stateExp, string confirmMethodExp, string productExp)
        {
            Assert.AreEqual(dateTimeExp, GetElementText(dateTimeFirstLineVerdictTab));
            Assert.AreEqual(stateExp, GetElementText(tasklineStateFirstVerdictTab));
            Assert.AreEqual(confirmMethodExp, GetElementText(confirmationMethodFirstVerdictTab));
            Assert.AreEqual(productExp, GetElementText(productFirstVerdictTab));
            return this;
        }

        public DetailTaskPage VerifySecondTaskLineStateVerdictTab(string dateTimeExp, string stateExp, string confirmMethodExp, string productExp)
        {
            Assert.AreEqual(dateTimeExp, GetElementText(dateTimeSecondLineVerdictTab));
            Assert.AreEqual(stateExp, GetElementText(tasklineStateSecondVerdictTab));
            Assert.AreEqual(confirmMethodExp, GetElementText(confirmationMethodSecondVerdictTab));
            Assert.AreEqual(productExp, GetElementText(productSecondVerdictTab));
            return this;
        }

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
        private readonly By firstStateTaskLine = By.CssSelector("tbody>tr:nth-child(1) select[id='itemState.id']");
        private readonly By firstProductTaskLine = By.XPath("//tbody/tr[1]//echo-select[contains(@params, 'name: product')]/select");
        private readonly By firstResolutionCodeTaskLine = By.CssSelector("tbody>tr:nth-child(1) select[id='resCode.id']");
        private readonly By secondStateTaskLine = By.CssSelector("tbody>tr:nth-child(2) select[id='itemState.id']");
        private readonly By secondProductTaskLine = By.XPath("//tbody/tr[2]//echo-select[contains(@params, 'name: product')]/select");
        private readonly By secondResolutionCodeTaskLine = By.CssSelector("tbody>tr:nth-child(2) select[id='resCode.id']");

        public DetailTaskPage ClickOnTaskLineTab()
        {
            ClickOnElement(taskLineTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

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

    }
}
