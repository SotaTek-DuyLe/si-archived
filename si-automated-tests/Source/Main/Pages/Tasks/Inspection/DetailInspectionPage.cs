using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;

namespace si_automated_tests.Source.Main.Pages.Tasks.Inspection
{
    public class DetailInspectionPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='INSPECTION']");
        private readonly By detailTitle = By.XPath("//p[text()='Site Inspection']");
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

        //DATA TAB
        private readonly By dataTab = By.XPath("//a[text()='Data']");

        //HISTORY TAB
        private readonly By historyTab = By.XPath("//a[text()='History']");
        private readonly By userNameText = By.XPath("//strong[text()='User: ']/following-sibling::span");

        //DYNAMIC
        private const string inspectionAddress = "//p[text()='{0}']";
        private const string historyItem = "//span[text()='{0}']/following-sibling::span[1]";

        public DetailInspectionPage WaitForInspectionDetailDisplayed()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTitle);
            return this;
        }

        public DetailInspectionPage IsDetailInspectionPage(string allocatedUnitValue, string assignedUserValue, string noteValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(allocatedUnitDd), allocatedUnitValue);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), assignedUserValue);
            Assert.AreEqual(GetAttributeValue(noteInput, "value"), noteValue);
            Assert.IsTrue(IsControlEnabled(completeBtn));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            return this;
        }

        public DetailInspectionPage VerifyAllFieldsInPopupAndDisabled(string allocatedUnitValue, string assignedUserValue, string noteValue, string validFromValue, string validToValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(allocatedUnitDd), allocatedUnitValue);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), assignedUserValue);
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), validFromValue);
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), validToValue);
            Assert.AreEqual(GetAttributeValue(noteInput, "value"), noteValue);
            Assert.IsTrue(IsControlEnabled(cancelBtnDisabled));
            Assert.IsTrue(IsControlEnabled(completeBtnDisabled));
            return this;
        }

        public DetailInspectionPage VerifyStateInspection(string stateExpected)
        {
            Assert.AreEqual(GetElementText(inspectionState), stateExpected);
            return this;
        }

        public DetailInspectionPage VerifyInspectionAddress(string address)
        {
            Assert.IsTrue(IsControlDisplayed(inspectionAddress, address));
            return this;
        }

        public DetailInspectionPage ClickAddressLinkAndVerify(string address, string sourceId)
        {
            ClickOnElement(inspectionAddress, address);
            //Verify
            SwitchToLastWindow();
            WaitUtil.WaitForElementVisible("//span[text()='Service Task']");
            string currentUrl = GetCurrentUrl();
            Assert.AreEqual(currentUrl, WebUrl.MainPageUrl + "service-tasks/" + sourceId);
            ClickCloseBtn();
            SwitchToChildWindow(3);
            return this;
        }

        public DetailInspectionPage VerifyValidFromValidToAndOtherDateField()
        {
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT) + " 00:00");
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1) + " 00:00");
            Assert.AreEqual(GetAttributeValue(startDateInput, "value"), "");
            Assert.AreEqual(GetAttributeValue(endDateInput, "value"), "");
            Assert.AreEqual(GetAttributeValue(cancelledDateInput, "value"), "");
            return this;
        }

        public DetailInspectionPage ClickOnDataTab()
        {
            ClickOnElement(dataTab);
            return this;
        }

        public DetailInspectionPage VerifyDataDisplayedWithDB(InspectionDBModel inspection, string note, int contractUnitId, int instance, int userId)
        {
            Assert.AreEqual(inspection.note, note);
            Assert.AreEqual(inspection.contractunitID, contractUnitId);
            Assert.AreEqual(inspection.inspectioninstance, instance);
            Assert.AreEqual(inspection.userID, userId);
            Assert.AreEqual(inspection.inspectionvaliddate, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT) + " 00:00:00.000");
            Assert.AreEqual(inspection.inspectionexpirydate, CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1) + " 00:00:00.000");
            return this;
        }

        //HISTORY TAB
        public DetailInspectionPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            return this;
        }

        public DetailInspectionPage VerifyDataInHistoryTab(string userName, string noteValue, string contractUnit, string userValue, string instanceValue)
        {
            Assert.AreEqual(userName, GetElementText(userNameText));
            Assert.AreEqual(noteValue + ".", GetElementText(historyItem, "Notes"));
            Assert.AreEqual(contractUnit + ".", GetElementText(historyItem, "Contract unit"));
            Assert.AreEqual(userValue + ".", GetElementText(historyItem, "User"));
            Assert.AreEqual(instanceValue + ".", GetElementText(historyItem, "Instance"));
            //Date
            Assert.AreEqual(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT) + " 00:00.", GetElementText(historyItem, "Inspection valid date"));
            Assert.AreEqual(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1) + " 00:00.", GetElementText(historyItem, "Inspection expiry date"));
            return this;
        }
    }
}
