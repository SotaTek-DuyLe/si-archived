using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Pages.PointAddress;

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

        //DATA TAB
        private readonly By dataTab = By.XPath("//a[text()='Data']");

        //HISTORY TAB
        private readonly By historyTab = By.XPath("//a[text()='History']");
        private readonly By userNameText = By.XPath("//strong[text()='User: ']/following-sibling::span");

        //DYNAMIC
        private const string inspectionAddress = "//p[text()='{0}']";
        private const string historyItem = "//span[text()='{0}']/following-sibling::span[1]";
        private const string inspectionType = "//p[text()='{0}']";

        public DetailInspectionPage WaitForInspectionDetailDisplayed(string inspectionTypeValue)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(inspectionType, inspectionTypeValue);
            return this;
        }

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

        public DetailInspectionPage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            return this;
        }

        public DetailInspectionPage VerifyAllFieldsInPopupAndDisabled(string allocatedUnitValue, string assignedUserValue, string noteValue, string validFromValue, string validToValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(allocatedUnitDd), allocatedUnitValue);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), assignedUserValue);
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), validFromValue);
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), validToValue);
            Assert.AreEqual(GetAttributeValue(noteInput, "value"), noteValue);
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
            Assert.AreEqual(currentUrl, WebUrl.MainPageUrl + "web/service-tasks/" + sourceId);
            ClickCloseBtn();
            SwitchToChildWindow(3);
            return this;
        }

        public PointAddressDetailPage ClickAddressLink(string address)
        {
            ClickOnElement(inspectionAddress, address);
            
            return PageFactoryManager.Get<PointAddressDetailPage>();
        }

        public DetailInspectionPage VerifyValidFromValidToAndOtherDateField(string validFromValue, string validToValue)
        {
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), validFromValue + " 00:00");
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), validToValue + " 00:00");
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

        public DetailInspectionPage VerifyDataDisplayedWithDB(InspectionDBModel inspection, string note, int contractUnitId, int instance, int userId, string validDateValue, string expDateValue)
        {
            Assert.AreEqual(inspection.note, note);
            Assert.AreEqual(inspection.contractunitID, contractUnitId);
            Assert.AreEqual(inspection.inspectioninstance, instance);
            Assert.AreEqual(inspection.userID, userId);
            Assert.AreEqual(inspection.inspectionvaliddate.ToString().Replace("-", "/"), validDateValue + " 00:00:00");
            Assert.AreEqual(inspection.inspectionexpirydate.ToString().Replace("-", "/"), expDateValue + " 00:00:00");
            return this;
        }

        public DetailInspectionPage VerifyInspectionId(string inspectionIdValue)
        {
            string idActual = GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/inspections/", "");
            Assert.AreEqual(inspectionIdValue, idActual);
            return this;
        }

        //HISTORY TAB
        public DetailInspectionPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            return this;
        }

        public DetailInspectionPage VerifyDataInHistoryTab(string userName, string noteValue, string contractUnit, string userValue, string instanceValue, string validDate, string expDate)
        {
            Assert.AreEqual(userName, GetElementText(userNameText));
            Assert.AreEqual(noteValue + ".", GetElementText(historyItem, "Notes"));
            Assert.AreEqual(contractUnit + ".", GetElementText(historyItem, "Contract unit"));
            Assert.AreEqual(userValue + ".", GetElementText(historyItem, "User"));
            Assert.AreEqual(instanceValue + ".", GetElementText(historyItem, "Instance"));
            //Date
            Assert.AreEqual(validDate + " 00:00.", GetElementText(historyItem, "Inspection valid date"));
            Assert.AreEqual(expDate + " 00:00.", GetElementText(historyItem, "Inspection expiry date"));
            return this;
        }

    }
}
