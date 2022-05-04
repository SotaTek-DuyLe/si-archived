using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Events
{
    public class EventDetailPage : BasePage
    {
        private readonly By eventTitle = By.XPath("//span[text()='Event']");
        private readonly By inspectionBtn = By.XPath("//button[@title='Inspect']");
        private readonly By locationName = By.CssSelector("a.typeUrl");
        private readonly By eventType = By.XPath("//span[text()='Event']/following-sibling::span");
        private readonly By serviceGroup = By.XPath("//div[text()='Service Group']/following-sibling::div");
        private readonly By service = By.XPath("//div[text()='Service']/following-sibling::div");
        private readonly By detailToggle = By.CssSelector("div#details-content-tab>div#toggle-actions");
        private readonly By detailLoactorExpanded = By.XPath("//div[@id='toggle-actions' and @aria-expanded='true']");
        private readonly By FrameMessage = By.XPath("//div[@class='notifyjs-corner']/div");

        //DETAIL - Expanded
        private readonly By sourceInput = By.CssSelector("div#details-content input#source");
        private readonly By statusDd = By.CssSelector("div#details-content select#status");
        private readonly By eventDateInput = By.CssSelector("div#details-content input#event-date");
        private readonly By allocatedUnitDetailDd = By.CssSelector("div#details-content select#allocated-unit");
        private readonly By resolutionCodeDd = By.CssSelector("div#details-content select#resolution-code");
        private readonly By assignedUserDetailDd = By.CssSelector("div#details-content select#allocated-user");
        private readonly By dueDateInput = By.CssSelector("div#details-content input#due-date");
        private readonly By resolvedDateInput = By.CssSelector("div#details-content input#resolved-date");
        private readonly By endDateInput = By.CssSelector("div#details-content input#end-date");
        private readonly By clientRefInput = By.CssSelector("div#details-content input#client-reference");

        //DATA TAB
        private readonly By allActiveServiceRow = By.CssSelector("//div[@class='parent-row']/div[1]");
        private const string eventDynamicLocator = "//div[@class='parent-row'][{0}]//div[text()='Event']";
        private const string serviceUnitDynamic = "//div[@class='parent-row'][{0}]//div[@title='Open Service Unit']/span";
        private const string serviceDynamic = "//div[@class='parent-row'][{0}]//span[@title='0']";

        //POPUP
        private readonly By createTitle = By.XPath("//h4[text()='Create ']");
        private readonly By sourceDd = By.CssSelector("select#source");
        private readonly By inspectionTypeDd = By.CssSelector("select#inspection-type");
        private readonly By validFromInput = By.CssSelector("input#valid-from");
        private readonly By validToInput = By.CssSelector("input#valid-to");
        private readonly By allocatedUnitDd = By.XPath("//label[text()='Allocated Unit']/following-sibling::div/select");
        private readonly By assignedUserDd = By.XPath("//label[text()='Assigned User']/following-sibling::div/select");
        private readonly By noteInput = By.CssSelector("textarea#note");
        private readonly By cancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By createBtn = By.XPath("//button[text()='Create']");
        private readonly By closeBtn = By.XPath("//h4[text()='Create ']/parent::div/following-sibling::div/button[@aria-label='Close']");
        private readonly By refreshBtn = By.XPath("//button[@title='Refresh' and @data-placement='bottom']");

        //Point History tab
        private readonly By pointHistoryBtn = By.CssSelector("a[aria-controls='pointHistory-tab']");
        private readonly By allRowInPointHistoryTabel = By.XPath("//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div");
        private const string columnInRowPointHistoryTab = "//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div/div[count(//div[@id='pointHistory-tab']//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private readonly By firstRowPointHistory = By.XPath("//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By descriptionColumn = By.XPath("//div[@id='pointHistory-tab']//span[text()='Description']");
        private readonly By filterInputById = By.XPath("//div[@id='pointHistory-tab']//div[contains(@class, 'l2 r2')]/descendant::input");

        //DYNAMIC
        private const string urlType = "//a[text()='{0}']";
        private const string sourceOption = "//select[@id='source']/option[text()='{0}']";
        private const string inspectionTypeOption = "//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()='Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";
        private const string anyTab = "//a[@aria-controls='{0}']";


        public EventDetailPage WaitForEventDetailDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(eventTitle);
            return this;
        }

        public string GetLocationName()
        {
            return GetElementText(locationName);
        }

        public EventDetailPage ClickInspectionBtn()
        {
            ClickOnElement(inspectionBtn);
            return this;
        }

        public ServiceUnitDetailPage ClickOnSourceHyperlink(string sourceName)
        {
            ClickOnElement(urlType, sourceName);
            return PageFactoryManager.Get<ServiceUnitDetailPage>();
        }

        public string GetEventTypeName()
        {
            return GetElementText(eventType);
        }

        public EventDetailPage VerifyEventType(string eventTypeValueExpected)
        {
            Assert.AreEqual(eventTypeValueExpected, GetElementText(eventType).Replace("- ", ""));
            return this;
        }

        public EventDetailPage VerifyServiceGroupAndService(string serviceGroupExp, string serviceExp)
        {
            Assert.AreEqual(serviceGroupExp, GetElementText(serviceGroup));
            Assert.AreEqual(serviceExp, GetElementText(service));
            return this;
        }

        public EventDetailPage ExpandDetailToggle()
        {
            ClickOnElement(detailToggle);
            WaitUtil.WaitForAllElementsVisible(detailLoactorExpanded);
            return this;
        }

        //DETAIL - Expanded
        public EventDetailPage VerifyValueInSubDetailInformation(string sourceExp, string statusExp)
        {
            Assert.AreEqual(sourceExp, GetElementText(sourceInput));
            Assert.AreEqual(statusExp, GetFirstSelectedItemInDropdown(statusDd));
            Assert.IsTrue(GetAttributeValue(eventDateInput, "value").Contains(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT)));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(allocatedUnitDetailDd));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(resolutionCodeDd));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(assignedUserDetailDd));
            Assert.AreEqual("", GetAttributeValue(dueDateInput, "value"));
            Assert.AreEqual("", GetAttributeValue(resolvedDateInput, "value"));
            Assert.AreEqual("", GetAttributeValue(endDateInput, "value"));
            Assert.AreEqual("", GetAttributeValue(clientRefInput, "value"));
            return this;
        }

        public EventDetailPage VerifyDueDate(string dueDateValue)
        {
            Assert.IsTrue(GetAttributeValue(dueDateInput, "value").Contains(dueDateValue));
            return this;
        }

        public EventDetailPage VerifyDisplayTabsAfterSaveEvent()
        {
            Assert.IsTrue(IsControlDisplayed(anyTab, "data-tab"));
            Assert.IsTrue(IsControlDisplayed(anyTab, "history-tab"));
            Assert.IsTrue(IsControlDisplayed(anyTab, "services-tab"));
            Assert.IsTrue(IsControlDisplayed(anyTab, "pointHistory-tab"));
            Assert.IsTrue(IsControlDisplayed(anyTab, "outstanding-tab"));
            Assert.IsTrue(IsControlDisplayed(anyTab, "history-tab"));
            Assert.IsTrue(IsControlDisplayed(anyTab, "tasks-tab"));
            Assert.IsTrue(IsControlDisplayed(anyTab, "map-tab"));
            return this;
        }

        public EventDetailPage ClickHistoryTab()
        {
            ClickOnElement(anyTab, "history-tab");
            return this;
        }

        public EventDetailPage ClickDataSubTab()
        {
            ClickOnElement(anyTab, "data-tab");
            return this;
        }

        public EventDetailPage VerifyNotDisplayErrorMessage()
        {
            Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
            return this;
        }

        public EventDetailPage ClickServicesSubTab()
        {
            ClickOnElement(anyTab, "services-tab");
            return this;
        }

        public EventDetailPage ClickOutstandingSubTab()
        {
            ClickOnElement(anyTab, "outstanding-tab");
            return this;
        }

        public EventDetailPage ClickPointHistorySubTab()
        {
            ClickOnElement(anyTab, "pointHistory-tab");
            return this;
        }

        public EventDetailPage VerifyDataInServiceSubTab(List<ActiveSeviceModel> allActiveServicesInServiceTab, List<ActiveSeviceModel> allActiveServicesSubTab)
        {
            Assert.AreEqual(allActiveServicesInServiceTab, allActiveServicesSubTab);
            return this;
        }

        public List<ActiveSeviceModel> GetAllActiveServiceModel()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allActiveRow = GetAllElements(allActiveServiceRow);
            for (int i = 0; i < allActiveRow.Count; i++)
            {
                string eventLocator = string.Format(eventDynamicLocator, i.ToString());
                string serviceUnitValue = GetElementText(serviceUnitDynamic, i.ToString());
                string serviceValue = GetElementText(serviceDynamic, i.ToString());
                activeSeviceModels.Add(new ActiveSeviceModel(eventLocator, serviceUnitValue, serviceValue));
            }
            return activeSeviceModels;
        }

        public EventDetailPage VerifyPointHistoryInSubTab(List<PointHistoryModel> pointHistoryModelsInDetail, List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab)
        {
            Assert.AreEqual(pointHistoryModelsInDetail, pointHistoryModelsInPointHistorySubTab);
            return this;
        }

        //POPUP CREATE INSPECTION
        public EventDetailPage IsCreateInspectionPopup(bool isIcon)
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(createTitle);
            Assert.IsTrue(IsControlDisplayed(sourceDd));
            Assert.IsTrue(IsControlDisplayed(inspectionTypeDd));
            Assert.IsTrue(IsControlDisplayed(validFromInput));
            Assert.IsTrue(IsControlDisplayed(validToInput));
            Assert.IsTrue(IsControlDisplayed(allocatedUnitDd));
            Assert.IsTrue(IsControlDisplayed(assignedUserDd));
            Assert.IsTrue(IsControlDisplayed(noteInput));
            Assert.IsTrue(IsControlDisplayed(closeBtn));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            //Disabled
            Assert.AreEqual(GetAttributeValue(createBtn, "disabled"), "true");
            //Mandatory field
            if (isIcon)
            {
                Assert.AreEqual(GetCssValue(sourceDd, "border-color"), CommonConstants.BoderColorMandatory);
            }
            Assert.AreEqual(GetCssValue(inspectionTypeDd, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(allocatedUnitDd, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }

        public EventDetailPage VerifyDefaulValue(bool isIcon)
        {
            if (isIcon)
            {
                Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), "Select...");
            }
            Assert.AreEqual(GetFirstSelectedItemInDropdown(inspectionTypeDd), "Select...");
            Assert.AreEqual(GetFirstSelectedItemInDropdown(allocatedUnitDd), "Select...");
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), "Select...");
            //Date
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }

        public EventDetailPage ClickSourceDdAndVerify(string[] sourceValues)
        {
            ClickOnElement(sourceDd);
            //Verify
            foreach (string source in sourceValues)
            {
                Assert.IsTrue(IsControlDisplayed(sourceOption, source));
            }
            return this;
        }

        public EventDetailPage VerifyDefaultSourceDd(string sourceValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceValue);
            return this;
        }

        public EventDetailPage ClickCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        public EventDetailPage VerifyPopupDisappears()
        {
            Assert.IsTrue(IsControlUnDisplayed(createBtn));
            Assert.IsTrue(IsControlUnDisplayed(sourceDd));
            return this;
        }

        public EventDetailPage ClickAndSelectInspectionType(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }

        public EventDetailPage ClickAndSelectAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }

        public EventDetailPage ClickAndSelectAssignedUser(string assignedUserValue)
        {
            ClickOnElement(assignedUserDd);
            ClickOnElement(assignedUserOption, assignedUserValue);
            return this;
        }

        public EventDetailPage InputValidFrom(string validFromValue)
        {
            SendKeys(validFromInput, validFromValue);
            return this;
        }

        public EventDetailPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }

        public EventDetailPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }

        public EventDetailPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }


        public EventDetailPage ClickRefreshEventDetailBtn()
        {
            ClickOnElement(refreshBtn);
            return this;
        }

        //POINT HISTORY
        public EventDetailPage ClickPointHistoryTab()
        {
            ClickOnElement(pointHistoryBtn);
            return this;
        }

        public List<PointHistoryModel> GetAllPointHistory()
        {
            WaitUtil.WaitForElementVisible(descriptionColumn);
            List<PointHistoryModel> allModel = new List<PointHistoryModel>();
            List<IWebElement> allRow = GetAllElements(allRowInPointHistoryTabel);

            for (int i = 0; i < allRow.Count; i++)
            {
                string desc = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[0])[i]);
                string ID = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[1])[i]);
                string type = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[2])[i]);
                string service = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[3])[i]);
                string address = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[4])[i]);
                string date = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[5])[i]);
                string dueDate = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[6])[i]);
                string state = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[7])[i]);
                string resolution = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[8])[i]);
                allModel.Add(new PointHistoryModel(desc, ID, type, service, address, date, dueDate, state, resolution));
            }
            return allModel;
        }

        public EventDetailPage VerifyPointHistory(PointHistoryModel pointHistoryModelActual, string desc, string id, string type, string service, string address, string date, string dueDate, string state)
        {
            Assert.AreEqual(desc, pointHistoryModelActual.description);
            Assert.AreEqual(id, pointHistoryModelActual.ID);
            Assert.AreEqual(type, pointHistoryModelActual.type);
            Assert.AreEqual(service, pointHistoryModelActual.service);
            Assert.AreEqual(address, pointHistoryModelActual.address);
            Assert.AreEqual(date, pointHistoryModelActual.date);
            Assert.AreEqual(dueDate, pointHistoryModelActual.dueDate);
            Assert.AreEqual(state, pointHistoryModelActual.state);
            return this;

        }

        public EventDetailPage DoubleClickOnCreatedInspection()
        {
            DoubleClickOnElement(firstRowPointHistory);
            return this;
        }

        public EventDetailPage FilterByPointHistoryId(string pointHistoryId)
        {
            SendKeys(filterInputById, pointHistoryId);
            SendKeys(filterInputById, Keys.Enter);
            return this;
        }

        public EventDetailPage VerifyCurrentEventUrl()
        {
            Assert.IsTrue(GetCurrentUrl().Contains(WebUrl.MainPageUrl + "web/events/"));
            return this;
        }

        public string GetEventId()
        {
            return GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/events/", "");
        }
    }
}
