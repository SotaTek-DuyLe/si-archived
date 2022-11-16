using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;

namespace si_automated_tests.Source.Main.Pages.Tasks.Inspection
{
    public class DetailInspectionPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='INSPECTION']");
        private readonly By allocatedUnitDd = By.CssSelector("select#allocated-unit");
        private readonly By assignedUserDd = By.CssSelector("select#allocated-user");
        private readonly By noteInput = By.CssSelector("textarea#note");
        private readonly By inspectionState = By.XPath("//div[@title='Inspection State']");
        private readonly By validFromInput = By.CssSelector("input#valid-from");
        private readonly By validToInput = By.CssSelector("input#valid-to");
        private readonly By startDateInput = By.CssSelector("input#start-date-and-time");
        private readonly By endDateInput = By.CssSelector("input#end-date-and-time");
        private readonly By cancelledDateInput = By.CssSelector("input#cancelled-date");
        private readonly By completeBtn = By.XPath("//div[text()='Complete']");
        private readonly By cancelBtn = By.XPath("//div[text()='Cancel']");
        private readonly By cancelBtnDisabled = By.XPath("//div[contains(@class, 'disabled') and text()='Cancel']");
        private readonly By completeBtnDisabled = By.XPath("//div[contains(@class, 'disabled') and text()='Complete']");
        private readonly By allocatedUnitLabel = By.XPath("//label[text()='Allocated Unit']");

        //DETAIL TAB
        private readonly By detailTab = By.XPath("//a[text()='Details']");
        private readonly By endDateAndTimeCalender = By.XPath("//input[@id='end-date-and-time']/following-sibling::span");
        private readonly By cancelledDateCalender = By.XPath("//input[@id='cancelled-date']/following-sibling::span");

        //DATA TAB
        private readonly By dataTab = By.XPath("//a[text()='Data']");
        private readonly By notesInputInDataTab = By.XPath("//label[text()='Notes']/following-sibling::input");
        private readonly By issueFoundCheckbox = By.XPath("//label[text()='Issue found?']/following-sibling::input");
        private readonly By addNewBtnImage = By.XPath("//label[text()='Image']/following-sibling::div/button");
        private readonly By imageData = By.XPath("//label[text()='Image']/following-sibling::div");
        private readonly By imgThumbnailTag = By.XPath("//div[contains(@class, 'img-thumbnail')]");
        private readonly By closeImgBtn = By.XPath("//div[contains(@class, 'img-thumbnail')]/img[@aria-label='Close']");
        private readonly By inputImage = By.CssSelector("input[type='file']");
        private readonly By streetGradeDd = By.XPath("//label[text()='Street Grade']/following-sibling::select");
        private readonly By accessPointInputInDataTab = By.XPath("//label[text()='Access Point']/following-sibling::input");

        //HISTORY TAB
        private readonly By historyTab = By.XPath("//a[text()='History']");
        private readonly By userNameText = By.XPath("//strong[text()='User: ']/following-sibling::span");
        private readonly By actionUpdateTextFirstRow = By.XPath("(//strong[text()='Action: ']/following-sibling::span[text()='Update - Inspection'])[1]");
        private readonly By actionUpdateTextSecondRow = By.XPath("(//strong[text()='Action: ']/following-sibling::span[text()='Update - Inspection'])[2]");
        private readonly By streetGradeFirstRow = By.XPath("//span[text()='Street Grade']/following-sibling::span[1]");
        private readonly By userFirstRow = By.XPath("//div[contains(@class, 'panel-default')][1]//strong[text()='User: ']/following-sibling::span[1]");
        private readonly By userSecondRow = By.XPath("//div[contains(@class, 'panel-default')][2]//strong[text()='User: ']/following-sibling::span[1]");

        private readonly By completedDate = By.XPath("//span[text()='Completed date']/following-sibling::span[1]");
        private readonly By cancelledDate = By.XPath("//span[text()='Cancelled date']/following-sibling::span[1]");
        private readonly By expiredDate = By.XPath("//span[text()='Update - Inspection']/parent::div/parent::div/following-sibling::div//span[text()='Inspection expiry date']/following-sibling::span[1]");
        private readonly By issueFound = By.XPath("//span[text()='Issue found?']/following-sibling::span[1]");

        //DYNAMIC
        private const string inspectionAddress = "//p[text()='{0}']";
        private const string historyItem = "//span[text()='{0}']/following-sibling::span[1]";
        private const string inspectionType = "//p[text()='{0}']";
        private const string streetGradeOption = "//label[text()='Street Grade']/following-sibling::select/option[text()='{0}']";
        private const string dataFirstRow = "//div[@id='history-tab']//div[contains(@class, 'panel-default')][1]//span[text()='{0}']";
        private const string dataSecondRow = "//div[@id='history-tab']//div[contains(@class, 'panel-default')][2]//span[text()='{0}']";

        [AllureStep]
        public DetailInspectionPage WaitForInspectionDetailDisplayed(string inspectionTypeValue)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(inspectionType, inspectionTypeValue);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage WaitForInspectionDetailDisplayed()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(title);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage IsDetailInspectionPage(string allocatedUnitValue, string assignedUserValue, string noteValue)
        {
            WaitUtil.WaitForElementVisible(allocatedUnitLabel);
            Assert.IsTrue(IsControlEnabled(completeBtn));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            Assert.AreEqual(GetFirstSelectedItemInDropdown(allocatedUnitDd), allocatedUnitValue);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), assignedUserValue);
            Assert.AreEqual(GetAttributeValue(noteInput, "value"), noteValue);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage JustClickCalenderEndDateAndTime()
        {
            ClickOnElement(endDateAndTimeCalender);
            ClickOnElement(title);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage JustClickCalenderCancelledDate()
        {
            ClickOnElement(cancelledDateCalender);
            ClickOnElement(title);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyValueInCancelledDate(string cancelledDateValue)
        {
            Assert.AreEqual(cancelledDateValue, GetAttributeValue(cancelledDateInput, "value"));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyAllFieldsInPopupAndDisabled(string allocatedUnitValue, string assignedUserValue, string noteValue, string validFromValue, string validToValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(allocatedUnitDd), allocatedUnitValue);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), assignedUserValue);
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), validFromValue);
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), validToValue);
            Assert.AreEqual(GetAttributeValue(noteInput, "value"), noteValue);
            //Disabled
            //Assert.AreEqual(GetAttributeValue(allocatedUnitDd, "disabled"), "true");
            //Assert.AreEqual(GetAttributeValue(assignedUserDd, "disabled"), "true");
            //Assert.AreEqual(GetAttributeValue(validFromInput, "disabled"), "true");
            //Assert.AreEqual(GetAttributeValue(validToInput, "disabled"), "true");
            //Assert.AreEqual(GetAttributeValue(startDateInput, "disabled"), "true");
            //Assert.AreEqual(GetAttributeValue(endDateInput, "disabled"), "true");
            //Assert.AreEqual(GetAttributeValue(noteInput, "disabled"), "true");
            //Assert.AreEqual(GetAttributeValue(cancelledDateInput, "disabled"), "true");
            //Assert.IsTrue(IsControlDisplayed(cancelBtnDisabled));
            //Assert.IsTrue(IsControlDisplayed(completeBtnDisabled));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyStateInspection(string stateExpected)
        {
            Assert.AreEqual(GetElementText(inspectionState), stateExpected);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyNoteValue(string noteValueExp)
        {
            Assert.AreEqual(GetAttributeValue(noteInput, "value"), noteValueExp);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyInspectionAddress(string address)
        {
            Assert.IsTrue(IsControlDisplayed(inspectionAddress, address));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage ClickServiceUnitLinkAndVerify(string address, string serviceUnitId)
        {
            ClickOnElement(inspectionAddress, address);
            //Verify
            SwitchToLastWindow();
            WaitUtil.WaitForElementVisible("//span[text()='Service Unit']");
            string currentUrl = GetCurrentUrl();
            Assert.AreEqual(currentUrl, WebUrl.MainPageUrl + "web/service-units/" + serviceUnitId);
            ClickCloseBtn();
            return this;
        }
        [AllureStep]
        public DetailInspectionPage ClickAddressLinkAndVerify(string address, string sourceId)
        {
            ClickOnElement(inspectionAddress, address);
            //Verify
            SwitchToLastWindow();
            WaitUtil.WaitForElementVisible("//span[text()='Service Task']");
            string currentUrl = GetCurrentUrl();
            Assert.AreEqual(currentUrl, WebUrl.MainPageUrl + "web/service-tasks/" + sourceId);
            ClickCloseBtn();
            SwitchToChildWindow(3);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickAddressLink(string address)
        {
            ClickOnElement(inspectionAddress, address);

            return PageFactoryManager.Get<PointAddressDetailPage>();
        }
        [AllureStep]
        public DetailInspectionPage VerifyValidFromValidToAndOtherDateField(string validFromValue, string validToValue)
        {
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), validFromValue + " 00:00");
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), validToValue + " 00:00");
            Assert.AreEqual(GetAttributeValue(startDateInput, "value"), "");
            Assert.AreEqual(GetAttributeValue(endDateInput, "value"), "");
            Assert.AreEqual(GetAttributeValue(cancelledDateInput, "value"), "");
            return this;
        }
        [AllureStep]
        public DetailInspectionPage ClickOnDataTab()
        {
            ClickOnElement(dataTab);
            return this;
        }

        [AllureStep]
        public DetailInspectionPage VerifyStreetGradeMandatory()
        {
            Assert.AreEqual(GetCssValue(streetGradeDd, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyDataDisplayedWithDB(InspectionDBModel inspection, string note, int contractUnitId, int instance, int userId, string validDateValue, string expDateValue, string validDateWithNewFormat, string expDateWithNewFormat)
        {
            Assert.AreEqual(inspection.note, note);
            Assert.AreEqual(inspection.contractunitID, contractUnitId);
            Assert.AreEqual(inspection.inspectioninstance, instance);
            Assert.AreEqual(inspection.userID, userId);
            Assert.IsTrue(inspection.inspectionvaliddate.ToString().Replace("-", "/").Contains(validDateValue + " 00:00:00") || inspection.inspectionvaliddate.ToString().Replace("-", "/").Contains(validDateWithNewFormat + " 00:00:00"), "Wrong inpsection valid Date");
            Assert.IsTrue(inspection.inspectionexpirydate.ToString().Replace("-", "/").Contains(expDateValue + " 00:00:00") ||
              inspection.inspectionexpirydate.ToString().Replace("-", "/").Contains(expDateWithNewFormat + " 00:00:00"), "Wrong inpsection expiry Date");
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyDataDisplayedWithDB(InspectionQueryModel inspection, string note, string contractUnitName, int instance, string userNameCreatedInspec, string validDateValue, string expDateValue, string allocatedUserInModel, string allocatedUserDisplayed)
        {
            Assert.AreEqual(inspection.note, note);
            Assert.AreEqual(inspection.contractunit, contractUnitName);
            Assert.AreEqual(inspection.inspectioninstance, instance);
            Assert.AreEqual(inspection.username, userNameCreatedInspec);
            Assert.AreEqual(allocatedUserInModel, allocatedUserDisplayed);
            Assert.AreEqual(inspection.inspectionvaliddate.ToString(CommonConstants.DATE_MM_DD_YYYY_FORMAT), validDateValue, "Wrong inpsection valid Date");
            Assert.AreEqual(inspection.inspectionexpirydate.ToString(CommonConstants.DATE_MM_DD_YYYY_FORMAT), expDateValue, "Wrong inpsection expiry Date");
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyInspectionId(string inspectionIdValue)
        {
            string idActual = GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/inspections/", "");
            Assert.AreEqual(inspectionIdValue, idActual);
            return this;
        }

        //HISTORY TAB
        [AllureStep]
        public DetailInspectionPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyDataInHistoryTab(string userName, string noteValue, string contractUnit, string userValue, string instanceValue, string validDate, string expDate)
        {
            Assert.AreEqual(userName, GetElementText(userNameText));
            Assert.AreEqual(noteValue + ".", GetElementText(historyItem, "Notes"));
            Assert.AreEqual(contractUnit + ".", GetElementText(historyItem, "Contract unit"));
            Assert.AreEqual(userValue + ".", GetElementText(historyItem, "User"));
            Assert.AreEqual(instanceValue + ".", GetElementText(historyItem, "Instance"));
            //Date
            Assert.AreEqual(validDate + " 00:00.", GetElementText(historyItem, "Inspection valid date"), "Wrong inpsection valid Date in History tab");
            Assert.AreEqual(expDate + " 00:00.", GetElementText(historyItem, "Inspection expiry date"), "Wrong inpsection expiry Date in History tab");
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage ClickAddressLinkShowPointSegmentDetail(string address)
        {
            ClickOnElement(inspectionAddress, address);

            return PageFactoryManager.Get<PointSegmentDetailPage>();
        }
        [AllureStep]
        public DetailInspectionPage VerifyRecordAfterUpdateAction(string userValue, string secondNote, string accessPoint)
        {
            Assert.IsTrue(IsControlDisplayed(actionUpdateTextFirstRow));
            Assert.IsTrue(IsControlDisplayed(actionUpdateTextSecondRow));
            //verify value
            Assert.AreEqual(GetElementText(userFirstRow), userValue);
            Assert.IsTrue(IsControlDisplayed(dataFirstRow, accessPoint + "."));
            Assert.AreEqual(GetElementText(userSecondRow), userValue);
            Assert.IsTrue(IsControlDisplayed(dataSecondRow, secondNote + "."));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage InputValidTo(string validToValue)
        {
            SendKeys(validToInput, validToValue);
            return this;
        }

        //DATA TAB
        [AllureStep]
        public DetailInspectionPage AddNotesInDataTab(string notesInput)
        {
            SendKeys(notesInputInDataTab, notesInput);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyValueInNoteInputDataTab(string noteValueExp)
        {
            Assert.AreEqual(noteValueExp, GetAttributeValue(notesInputInDataTab, "value"));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage ClickIssueFoundCheckbox()
        {
            ClickOnElement(issueFoundCheckbox);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyIssueFoundCheckboxChecked()
        {
            Assert.IsTrue(IsCheckboxChecked(issueFoundCheckbox));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage UploadImage(string urlImage)
        {
            ClickOnElement(addNewBtnImage);
            SendKeysWithUrl(inputImage, urlImage);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage SelectStreetGrade(string streetGradeValue)
        {
            ClickOnElement(streetGradeDd);
            ClickOnElement(streetGradeOption, streetGradeValue);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage AddAccessPointInDataTab(string accessPointValue)
        {
            SendKeys(accessPointInputInDataTab, accessPointValue);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyValueInAccessPointInput(string accessPointValue)
        {
            Assert.AreEqual(accessPointValue, GetAttributeValue(accessPointInputInDataTab, "value"));
            return this;
        }

        [AllureStep]
        public DetailInspectionPage VerifyValueInStreetGradeDd(string optionSelected)
        {
            Assert.AreEqual(optionSelected, GetFirstSelectedItemInDropdown(streetGradeDd));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyFieldsInDataTabDisabled(string streetGradeExp)
        {
            Assert.AreEqual(GetAttributeValue(addNewBtnImage, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(issueFoundCheckbox, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(streetGradeDd, "disabled"), "true");
            Assert.AreEqual(GetFirstSelectedItemInDropdown(streetGradeDd), streetGradeExp);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyNotesFieldInDataTabReadOnly()
        {
            Assert.AreEqual(GetAttributeValue(notesInputInDataTab, "disabled"), "true");
            return this;
        }

        //COMPLETE
        [AllureStep]
        public DetailInspectionPage ClickCompleteBtn()
        {
            ClickOnElement(completeBtn);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyAllFieldsInPopupDisabled()
        {
            //Disabled
            Assert.AreEqual(GetAttributeValue(allocatedUnitDd, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(assignedUserDd, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(validFromInput, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(validToInput, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(startDateInput, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(endDateInput, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(noteInput, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(cancelledDateInput, "disabled"), "true");
            Assert.IsTrue(IsControlDisplayed(cancelBtnDisabled));
            Assert.IsTrue(IsControlDisplayed(completeBtnDisabled));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyAllFieldsInDataTabDisabled()
        {
            Assert.AreEqual(GetAttributeValue(addNewBtnImage, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(accessPointInputInDataTab, "disabled"), "true");
            Assert.AreEqual(GetAttributeValue(notesInputInDataTab, "disabled"), "true");
            return this;
        }

        [AllureStep]
        public DetailInspectionPage VerifyTheImageIsReadOnly()
        {
            Assert.IsTrue(GetAttributeValue(imgThumbnailTag, "class").Contains("disabled"));
            Assert.IsTrue(GetAttributeValue(closeImgBtn, "class").Contains("disabled"));
            Assert.AreEqual(GetAttributeValue(inputImage, "disabled"), "disabled");
            return this;
        }

        [AllureStep]
        public DetailInspectionPage VerifyHistoryAfterCompleted(string userName, string timeCompleted)
        {
            Assert.IsTrue(IsControlDisplayed(actionUpdateTextFirstRow));
            Assert.AreEqual(timeCompleted + ".", GetElementText(completedDate));
            Assert.AreEqual(userName, GetElementText(userFirstRow));
            return this;

        }
        [AllureStep]
        public DetailInspectionPage VerifyStreetGradeInHistory(string streetGradeValue)
        {
            Assert.AreEqual(streetGradeValue + ".", GetElementText(streetGradeFirstRow));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyIssueFound(string issueFoundValue)
        {
            Assert.AreEqual(issueFoundValue + ".", GetElementText(issueFound));
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyFirstNoteInHistoryTab(string noteExp)
        {
            Assert.IsTrue(IsControlDisplayed(dataFirstRow, noteExp + "."));
            return this;
        }

        [AllureStep]
        public DetailInspectionPage VerifyTimeInEndDateAndTimeField(string timeNow)
        {
            Assert.AreEqual(timeNow, GetAttributeValue(endDateInput, "value"));
            return this;
        }

        //CANCEL
        [AllureStep]
        public DetailInspectionPage ClickCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage VerifyHistoryAfterCancelled(string userName, string timeCancelled)
        {
            Assert.IsTrue(IsControlDisplayed(actionUpdateTextFirstRow));
            Assert.AreEqual(timeCancelled + ".", GetElementText(cancelledDate));
            Assert.AreEqual(userName, GetElementText(userFirstRow));
            return this;

        }

        //EXPIRE
        [AllureStep]
        public DetailInspectionPage VerifyHistoryAfterExpired(string userName, string timeExpired)
        {
            Assert.IsTrue(IsControlDisplayed(actionUpdateTextFirstRow));
            Assert.AreEqual(timeExpired + ".", GetElementText(expiredDate));
            Assert.AreEqual(userName, GetElementText(userFirstRow));
            return this;

        }
        [AllureStep]
        public DetailInspectionPage VerifyValueInValidToField(string validToValue)
        {
            Assert.AreEqual(validToValue, GetAttributeValue(validToInput, "value"));
            return this;
        }

        //Open inpectionDetail with Id
        [AllureStep]
        public DetailInspectionPage OpenDetailInspectionWithId(string inspectionId)
        {
            GoToURL(WebUrl.MainPageUrl + "web/inspections/" + inspectionId);
            return PageFactoryManager.Get<DetailInspectionPage>();
        }
    }
}
