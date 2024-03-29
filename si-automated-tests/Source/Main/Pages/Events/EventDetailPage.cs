﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.DBModels.GetPointHistory;
using si_automated_tests.Source.Main.DBModels.GetServiceInfoForPoint;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Events
{
    public class EventDetailPage : BasePage
    {
        public EventDetailPage()
        {
            pointhistoryTableEle = new TableElement(PointHistoryTable, PointHistoryRow, new List<string>()
            {
                PHDescriptionCell, PHIDCell, PHTypeCell, PHServiceCell, PHAddressCell, PHDateCell, PHDueDateCell, PHStateCell, PHResolutionCodeCell
            });
            pointhistoryTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        private readonly By eventTitle = By.XPath("//span[text()='Event']");
        private readonly By inspectionBtn = By.XPath("//button[@title='Inspect']");
        private readonly By locationName = By.CssSelector("a.typeUrl");
        private readonly By eventType = By.XPath("//span[text()='Event']/following-sibling::span");
        private readonly By serviceGroup = By.XPath("//div[text()='Service Group']/following-sibling::div");
        private readonly By service = By.XPath("//div[text()='Service']/following-sibling::div");
        private readonly By detailToggle = By.CssSelector("div#details-content-tab>div#toggle-actions");
        private readonly By detailLoactorExpanded = By.XPath("//div[@id='toggle-actions' and @aria-expanded='true']");
        private readonly By FrameMessage = By.XPath("//div[@class='notifyjs-corner']/div");
        private readonly By blueIcon = By.CssSelector("img[title='Find Service Unit for this location']");
        private readonly By allEventOptions = By.CssSelector("ul#create-event-opts>li");
        private readonly By actionBtn = By.CssSelector("button[title='Actions']");
        private readonly By allOptionsInActionDd = By.XPath("//button[@title='Actions']/following-sibling::ul/li");
        private readonly By allOptionsInEventAction = By.XPath("//div[@id='event-actions-content']//span");

        //DETAIL - Expanded
        private readonly By sourceInput = By.CssSelector("div#details-content input#source");
        private readonly By parentSourceInput = By.XPath("//input[@id='source']");
        private readonly By statusDd = By.CssSelector("div#details-content select#status");
        private readonly By eventDateInput = By.CssSelector("div#details-content input#event-date");
        private readonly By allocatedUnitDetailDd = By.CssSelector("div#details-content select#allocated-unit");
        private readonly By allAllocatedUnitInDetailSubTab = By.CssSelector("div#details-content select#allocated-unit>option");
        private readonly By resolutionCodeDd = By.CssSelector("div#details-content select#resolution-code");
        private readonly By assignedUserDetailDd = By.CssSelector("div#details-content select#allocated-user");
        private readonly By allAssignedUserInDetailSubTab = By.CssSelector("div#details-content select#allocated-user>option");
        private readonly By dueDateInput = By.CssSelector("div#details-content input#due-date");
        private readonly By resolvedDateInput = By.CssSelector("div#details-content input#resolved-date");
        private readonly By endDateInput = By.CssSelector("div#details-content input#end-date");
        private readonly By clientRefInput = By.CssSelector("div#details-content input#client-reference");
        private readonly By firstAllocatedUnitInDd = By.CssSelector("select[id='allocated-unit']>option:nth-child(1)");
        private const string allocatedUnitDetailOption = "//div[@id='details-content']//select[@id='allocated-unit']/option[text()='{0}']";


        //DATA TAB
        private readonly By allActiveServiceRow = By.XPath("//div[@class='parent-row']//span[@title='Open Service Task']");
        private const string eventDynamicLocator = "//div[@class='parent-row'][{0}]//div[text()='Event']";
        private const string serviceUnitDynamic = "//div[@class='parent-row'][{0}]//div[@title='Open Service Unit']/span";
        private const string serviceWithServiceUnitDynamic = "//div[@class='parent-row'][{0}]//span[@title='Open Service Task']";
        private readonly By nameInput = By.XPath("//label[text()='Name']/following-sibling::input");
        private readonly By noteInputInDataTab = By.XPath("//label[text()='Notes']/following-sibling::textarea");
        private readonly By contactNumberInput = By.XPath("//label[text()='Contact Number']/following-sibling::input");
        private readonly By emailInput = By.CssSelector("input[type='email']");
        private readonly By allActiveServiceRows = By.XPath("//span[@title='Open Service Task' or @title='0']");
        private const string allserviceUnitDynamic = "//div[@class='parent-row'][{0}]//span[@title='Open Service Task' or @title='0']";
        private readonly By schedule = By.XPath("//div[@data-bind=\"template: { name: 'service-grid-schedule' }\"]");
        private readonly By last = By.CssSelector("div.parent-row span[data-bind='text: ew.formatDateForUser($data.lastDate)']");
        private readonly By next = By.CssSelector("div.parent-row span[data-bind='text: ew.formatDateForUser($data.nextDate)']");
        private readonly By assetType = By.CssSelector("div.parent-row div[data-bind='foreach: $data.asset']");
        private readonly By allocation = By.XPath("//div[@class='parent-row']//span[contains(@data-bind, 'text: $parents[0].getParentAllocationText($data)')]");
        private readonly By addNewCWActionBinTasklinesBtn = By.XPath("//label[text()='CW Action/Bin/Task Lines']/following-sibling::div/button");
        private readonly By firstActionCWAction = By.XPath("//label[text()='Action']/following-sibling::select");
        private readonly By firstBinCWAction = By.XPath("//label[text()='Bin']/following-sibling::select");
        private readonly string firstActionCWActionOption = "//label[text()='Action']/following-sibling::select/option[text()='{0}']";
        private readonly string firstBinCWActionOption = "//label[text()='Bin']/following-sibling::select/option[text()='{0}']";

        //CHRILD
        private readonly By scheduleChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//div[@data-bind='text: $data']");
        private readonly By lastChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//span[@data-bind='text: ew.formatDateForUser($data.lastDate)']");
        private readonly By nextChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//div[@data-bind='text: $data.next']");
        private readonly By allocationChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//span[contains(@data-bind,'text: $data.allocation')]");

        //MAP TAB
        private readonly By typeInMapTab = By.CssSelector("td[data-bind='text: pointType']");
        private readonly By descInMapTab = By.CssSelector("td[data-bind='text: description']");

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

        #region Point History
        //Point History tab
        private readonly By pointHistoryBtn = By.CssSelector("a[aria-controls='pointHistory-tab']");
        private readonly By allRowInPointHistoryTabel = By.XPath("//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div");
        private const string columnInRowPointHistoryTab = "//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div/div[count(//div[@id='pointHistory-tab']//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private readonly By firstRowPointHistory = By.XPath("//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By descriptionColumn = By.XPath("//div[@id='pointHistory-tab']//span[text()='Description']");
        private readonly By filterInputById = By.XPath("//div[@id='pointHistory-tab']//div[contains(@class, 'l2 r2')]/descendant::input");
        private readonly By allDueDate = By.XPath("//div[@class='slick-cell l7 r7']");

        private string PointHistoryTable = "//div[@id='pointHistory-tab']//div[@class='grid-canvas']";
        private string PointHistoryRow = "./div[contains(@class, 'slick-row')]";
        private string PHDescriptionCell = "./div[@class='slick-cell l1 r1']";
        private string PHIDCell = "./div[@class='slick-cell l2 r2']";
        private string PHTypeCell = "./div[@class='slick-cell l3 r3']";
        private string PHServiceCell = "./div[@class='slick-cell l4 r4']";
        private string PHAddressCell = "./div[@class='slick-cell l5 r5']";
        private string PHDateCell = "./div[@class='slick-cell l6 r6']";
        private string PHDueDateCell = "./div[@class='slick-cell l7 r7']";
        private string PHStateCell = "./div[@class='slick-cell l8 r8']";
        private string PHResolutionCodeCell = "./div[@class='slick-cell l9 r9']";

        private TableElement pointhistoryTableEle;
        public TableElement PointHistoryTableEle
        {
            get => pointhistoryTableEle;
        }
        #endregion

        //HISTORY TAB
        private const string titleHistoryTab = "//strong[text()='{0}']";
        private readonly By stateInHistoryTab = By.XPath("//span[text()='State']/following-sibling::span[@data-bind='text: $data.value'][1]");
        private readonly By stateOfEventInHistoryTab = By.XPath("//strong[text()='Create Event - Event']/following-sibling::div/span[text()='State']/following-sibling::span[@data-bind='text: $data.value'][1]");
        private readonly By eventDateInHistoryTab = By.XPath("//span[text()='Event date']/following-sibling::span[@data-bind='text: $data.value'][1]");
        private readonly By dueDateInHistoryTab = By.XPath("(//span[text()='Due date']/following-sibling::span[@data-bind='text: $data.value'][1])[1]");
        private readonly By createdByUserInHistoryTab = By.XPath("//strong[text()='Create Event - Event']/parent::div/following-sibling::div//strong[@data-bind='text: $data.createdByUser']");
        private readonly By createdDateInHistoryTab = By.XPath("//strong[@data-bind='text: $data.createdDate']");
        private readonly By nameInHistoryTab = By.XPath("//span[text()='Name']/following-sibling::span[1]");
        private readonly By notesInHistoryTab = By.XPath("//span[text()='Notes']/following-sibling::span[1]");
        //HISTORY TAB => NEW RECORD
        private readonly By newStateInHistoryTab = By.XPath(" //strong[text()='Allocate Event - Event']/following-sibling::div/span[text()='State']/following-sibling::span[1]");
        private readonly By newAllocatedUserInHistoryTab = By.XPath("//strong[text()='Allocate Event - Event']/following-sibling::div/span[text()='Allocated user']/following-sibling::span[1]");
        private readonly By newContractUnitInHistoryTab = By.XPath("//strong[text()='Allocate Event - Event']/following-sibling::div/span[text()='Contract unit']/following-sibling::span[1]");
        private readonly By createdByUserNewRecord = By.XPath("//strong[text()='Allocate Event - Event']/parent::div/following-sibling::div/strong[1]");
        private readonly By nameAfterUpdateInHistoryTab = By.XPath("//strong[text()='Accept - Event']/following-sibling::div/span[text()='Name']/following-sibling::span[1]");
        private readonly By createdByUserAfterUpdate = By.XPath("//strong[text()='Update Event - Event']/parent::div/following-sibling::div/strong[1]");
        private readonly By stateAfterAcceptInHistoryTab = By.XPath("//strong[text()='Accept - Event']/following-sibling::div/span[text()='State']/following-sibling::span[1]");
        private readonly By firstNameAfterAccept = By.XPath("//strong[text()='Accept - Event']/following-sibling::div//span[text()='Name']/following-sibling::span[1]");
        private readonly By createdByUserAfterAccept = By.XPath("//strong[text()='Accept - Event']/parent::div/following-sibling::div/strong[1]");
        private readonly By createdByUserAfterAddNote = By.XPath("//strong[text()='Add Note - Event']/parent::div/following-sibling::div/strong[1]");
        private readonly By updatedTimeAfterAddNote = By.XPath("//strong[text()='Add Note - Event']/parent::div/following-sibling::div/strong[2]");
        private readonly By notesInAddNoteEvent = By.XPath("//strong[text()='Add Note - Event']//following-sibling::div/span[text()='Notes']/following-sibling::span[1]");
        private readonly By clientRefAfterUpdateInHistoryTab = By.XPath("//strong[text()='Update Event - Event']/following-sibling::div/span[text()='Client reference']/following-sibling::span[1]");
        private readonly By emailAddressAfterUpdateInHistoryTab = By.XPath("//strong[text()='Update Event - Event']/following-sibling::div/span[text()='Email Address']/following-sibling::span[1]");

        private readonly By stateAfterCancelInHistoryTab = By.XPath("//strong[text()='Cancel - Event']/following-sibling::div/span[text()='State']/following-sibling::span[1]");
        private readonly By endDateAfterCancelInHistoryTab = By.XPath("//strong[text()='Cancel - Event']/following-sibling::div/span[text()='End date']/following-sibling::span[1]");
        private readonly By resolvedDateAfterCancelInHistoryTab = By.XPath("//strong[text()='Cancel - Event']/following-sibling::div/span[text()='Resolved date']/following-sibling::span[1]");
        private readonly By emailAddressAfterCancelInHistoryTab = By.XPath("//strong[text()='Cancel - Event']/following-sibling::div/span[text()='Email Address']/following-sibling::span[1]");

        //DYNAMIC
        private const string urlType = "//a[text()='{0}']";
        private const string sourceOption = "//select[@id='source']/option[text()='{0}']";
        private const string inspectionTypeOption = "//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()='Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";
        private const string anyTab = "//a[@aria-controls='{0}']";
        private const string anyOptionInActionDd = "//button[@title='Actions']/following-sibling::ul/li/a[text()='{0}']";

        //POPUP
        private readonly By linkEventTitle = By.XPath("//div[@id='no-service-unit']//h4[text()='Link event to a service unit']");
        private readonly By searchSection = By.XPath("//div[@id='no-service-unit']//span[text()='Search']");
        private readonly By resultSection = By.XPath("//div[@id='no-service-unit']//span[text()='Result']");
        private readonly By addressSearchCheckbox = By.XPath("//div[@id='no-service-unit']//label[text()='Address']/preceding-sibling::input");
        private readonly By areaSearchCheckbox = By.XPath("//div[@id='no-service-unit']//label[text()='Area']/preceding-sibling::input");
        private readonly By nodeSearchCheckbox = By.XPath("//div[@id='no-service-unit']//label[text()='Node']/preceding-sibling::input");
        private readonly By segmentSearchCheckbox = By.XPath("//div[@id='no-service-unit']//label[text()='Segment']/preceding-sibling::input");
        private readonly By sectorDd = By.XPath("//div[@id='no-service-unit']//label[text()='Sector']/following-sibling::select");
        private readonly By closePopupBtn = By.XPath("//div[@id='no-service-unit']//h4[text()='Link event to a service unit']/following-sibling::button[@aria-label='Close']");
        private readonly By streetInput = By.CssSelector("div#no-service-unit input[placeholder='Start typing for suggestions...']");
        private readonly By houseNumberNameInput = By.XPath("//div[@id='no-service-unit']//label[text()='House Number/Name']/following-sibling::input");
        private readonly By postCodeInput = By.XPath("//div[@id='no-service-unit']//label[text()='Postcode']/following-sibling::input");
        private readonly By searchBtn = By.XPath("//div[@id='no-service-unit']//button[text()='Search']");
        private readonly By clearFilterInput = By.CssSelector("div#no-service-unit input[value='Clear Filters']");
        private readonly By cannotFindAddressBtn = By.XPath("//div[@id='no-service-unit']//button[text()='Cannot find Address']");
        private const string columnInPopupEventTitle = "//div[@id='no-service-unit']//th[text()='{0}']";

        //SERVICES TAB
        private readonly By servicesTab = By.CssSelector("a[aria-controls='services-tab']");
        private readonly By allServiceRowsInServiceTab = By.CssSelector("div.parent-row");
        private readonly By serviceUnitInServiceTab = By.XPath("//div[@class='parent-row']//div[@title='Open Service Unit']");
        private readonly By serviceInServiceTab = By.XPath("//div[@class='parent-row']//span[@title='0' or @title='Open Service Task']");
        private readonly By scheduleInServiceTab = By.CssSelector("div.parent-row div[data-bind=\"template: { name: 'service-grid-schedule' }\"]");
        private readonly By lastInServiceTab = By.CssSelector("div.parent-row div[data-bind=\"template: { name: 'task-round-leg-state', data: $data }\"]");
        private readonly By nextInServiceTab = By.CssSelector("div[data-bind=\"template: { name: 'service-grid-next' }\"]");
        private readonly By assetTypeInServiceTab = By.CssSelector("div.parent-row div[data-bind='foreach: $data.asset']");
        private readonly By allocationInServiceTab = By.XPath("//div[@class='parent-row']//span[contains(@data-bind, 'text: $parents[0].getParentAllocationText($data)')]/parent::div");
        private const string eventOptions = "//div[@id='create-event-dropdown']//li[text()='{0}']";

        [AllureStep]
        public EventDetailPage ClickOnServicesTab()
        {
            ClickOnElement(servicesTab);
            return this;
        }
        [AllureStep]
        public List<ActiveSeviceModel> GetAllServiceWithServiceUnitModel()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allRow = GetAllElements(allServiceRowsInServiceTab);
            for (int i = 0; i < allRow.Count; i++)
            {
                string eventParentLocator = string.Format(eventDynamicLocator, (i + 1).ToString());
                string serviceUnitValue = GetElementText(GetAllElements(serviceUnitInServiceTab)[i]);
                string serviceValue = GetElementText(GetAllElements(serviceInServiceTab)[i]);
                string scheduleValue = GetElementText(GetAllElements(scheduleInServiceTab)[i]);
                string lastValue = GetElementText(GetAllElements(lastInServiceTab)[i]);
                string nextValue = GetElementText(GetAllElements(nextInServiceTab)[i]);
                string assetTypeValue = GetElementText(GetAllElements(assetTypeInServiceTab)[i]);
                string allocationValue = GetElementText(GetAllElements(allocationInServiceTab)[i]);
                activeSeviceModels.Add(new ActiveSeviceModel(serviceUnitValue, serviceValue, scheduleValue, lastValue, nextValue, assetTypeValue, allocationValue, eventParentLocator, ""));
            }
            return activeSeviceModels;
        }
        [AllureStep]
        public List<ActiveSeviceModel> GetAllActiveServiceInTabFullInfo()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allRow = GetAllElements(allActiveServiceRow);
            for (int i = 0; i < allRow.Count; i++)
            {
                string serviceUnitValue = GetElementText(serviceUnitDynamic, (i + 1).ToString());
                string serviceValue = GetElementText(serviceWithServiceUnitDynamic, (i + 1).ToString());
                string scheduleValue = GetElementText(GetAllElements(schedule)[i]);
                string lastValue = GetElementText(GetAllElements(last)[i]);
                string nextValue = GetElementText(GetAllElements(next)[i]);
                string assetTypeValue = GetElementText(GetAllElements(assetType)[i]);
                string allocationValue = GetElementText(GetAllElements(allocation)[i]);
                activeSeviceModels.Add(new ActiveSeviceModel(serviceUnitValue, serviceValue, scheduleValue, lastValue, nextValue, assetTypeValue, allocationValue));
            }
            return activeSeviceModels;
        }
        [AllureStep]
        public List<ActiveSeviceModel> GetAllActiveServiceInTabFullInfo32839()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allRow = GetAllElements(allActiveServiceRow);
            for (int i = 0; i < allRow.Count; i++)
            {
                string serviceUnitValue = GetElementText(serviceUnitDynamic, (i + 1).ToString());
                string serviceValue = GetElementText(serviceWithServiceUnitDynamic, (i+1).ToString());
                string scheduleValue = GetElementText(GetAllElements(schedule)[i]);
                string lastValue = GetElementText(GetAllElementsNotWait(last)[i]);
                string nextValue = GetElementText(GetAllElementsNotWait(next)[i]);
                string assetTypeValue = GetElementText(GetAllElements(assetType)[i]);
                string allocationValue = GetElementText(GetAllElementsNotWait(allocation)[i]);
                //List<ChildSchedule> listSchedule = new List<ChildSchedule>();
                //if (i == 1)
                //{
                //    string scheludeChild = GetElementText(scheduleChildRow);
                //    string lastChild = GetElementText(lastChildRow);
                //    string nextChild = GetElementText(nextChildRow);
                //    string allocationChild = GetElementText(allocationChildRow);
                //    listSchedule.Add(new ChildSchedule(scheludeChild, lastChild, nextChild, allocationChild));
                //}
                //activeSeviceModels.Add(new ActiveSeviceModel(serviceUnitValue, serviceValue, scheduleValue, lastValue, nextValue, assetTypeValue, allocationValue, listSchedule));
                activeSeviceModels.Add(new ActiveSeviceModel(serviceUnitValue, serviceValue, scheduleValue, lastValue, nextValue, assetTypeValue, allocationValue));
            }
            return activeSeviceModels;
        }

        [AllureStep]
        public EventDetailPage VerifyActiveServiceDisplayedWithDB(List<ActiveSeviceModel> activeSeviceModelsDisplayed, List<ServiceForPointDBModel> serviceForPointDB, List<ServiceTaskForPointDBModel> serviceTaskForPointDBModels)
        {
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickFirstEventInFirstServiceRow()
        {
            ClickOnElement(eventDynamicLocator, "1");
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyEventTypeWhenClickEventBtn(List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId)
        {
            foreach (CommonServiceForPointDBModel common in FilterCommonServiceForPointWithServiceId)
            {
                Assert.IsTrue(IsControlDisplayed(eventOptions, common.prefix + " - " + common.eventtype));
            }
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickAnyEventOption(string eventName)
        {
            ClickOnElement(eventOptions, eventName);
            return PageFactoryManager.Get<EventDetailPage>();
        }

        //LOCATION POPUP
        [AllureStep]
        public EventDetailPage VeriryDisplayPopupLinkEventToServiceUnit(string sectorValue)
        {
            WaitUtil.WaitForElementVisible(linkEventTitle);
            Assert.IsTrue(IsControlDisplayed(linkEventTitle));
            Assert.IsTrue(IsControlDisplayed(searchSection));
            Assert.IsTrue(IsControlDisplayed(resultSection));
            //Assert.IsTrue(IsControlDisplayed(areaSearchCheckbox));
            //Assert.IsTrue(IsControlDisplayed(nodeSearchCheckbox));
            //Assert.IsTrue(IsControlDisplayed(segmentSearchCheckbox));
            Assert.IsTrue(IsControlDisplayed(sectorDd));
            Assert.IsTrue(IsControlDisplayed(closePopupBtn));
            Assert.IsTrue(IsControlDisplayed(streetInput));
            Assert.IsTrue(IsControlDisplayed(houseNumberNameInput));
            Assert.IsTrue(IsControlDisplayed(postCodeInput));
            Assert.IsTrue(IsControlDisplayed(searchBtn));
            Assert.IsTrue(IsControlDisplayed(clearFilterInput));
            Assert.IsTrue(IsControlDisplayed(cannotFindAddressBtn));
            //Address checked
           // Assert.AreEqual(GetAttributeValue(addressSearchCheckbox, "checked"), "true");
            //Sector defaul value
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sectorDd), sectorValue);
            foreach(string column in CommonConstants.LinkEventPopupColumn)
            {
                Assert.IsTrue(IsControlDisplayed(columnInPopupEventTitle, column));
            }
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickCloseEventPopupBtn()
        {
            ClickOnElement(closePopupBtn);
            return this;
        }
        [AllureStep]
        public EventDetailPage WaitForEventDetailDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            //WaitUtil.WaitForElementVisible(eventTitle);
            WaitUtil.WaitForElementVisible(locationName);
            WaitUtil.WaitForElementVisible(anyTab, "data-tab");
            return this;
        }
        [AllureStep]
        public string GetLocationName()
        {
            return GetElementText(locationName);
        }
        [AllureStep]
        public EventDetailPage VerifyEventName(string eventNameExp)
        {
            WaitUtil.WaitForElementVisible(locationName);
            Assert.AreEqual(eventNameExp, GetElementText(locationName));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickOnLocation()
        {
            ClickOnElement(locationName);
            return PageFactoryManager.Get<ServiceUnitDetailPage>();
        }
        [AllureStep]
        public EventDetailPage ClickOnLocationShowPopup()
        {
            ClickOnElement(locationName);
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickInspectionBtn()
        {
            ClickOnElement(inspectionBtn);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickOnSourceHyperlink(string sourceName)
        {
            ClickOnElement(urlType, sourceName);
            return PageFactoryManager.Get<ServiceUnitDetailPage>();
        }
        [AllureStep]
        public string GetEventTypeName()
        {
            return GetElementText(eventType);
        }
        [AllureStep]
        public EventDetailPage VerifyEventType(string eventTypeValueExpected)
        {
            Assert.AreEqual(eventTypeValueExpected.ToLower().Replace("standard", "").Replace("-", "").Trim(), GetElementText(eventType).Replace("- ", "").ToLower());
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyEventTypeWithDB(string eventTypeValueExpected)
        {
            Assert.AreEqual(eventTypeValueExpected.ToLower(), GetElementText(eventType).Replace("- ", "").ToLower());
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyServiceGroupAndService(string serviceGroupExp, string serviceExp)
        {
            Assert.AreEqual(serviceGroupExp, GetElementText(serviceGroup));
            Assert.AreEqual(serviceExp, GetElementText(service));
            return this;
        }
        [AllureStep]
        public EventDetailPage ExpandDetailToggle()
        {
            if (IsControlUnDisplayed(detailLoactorExpanded))
            {
                ClickOnElement(detailToggle);
                WaitUtil.WaitForAllElementsVisible(detailLoactorExpanded);
            }
            WaitUtil.WaitForPageLoaded();
            SleepTimeInMiliseconds(2000);
            return this;
        }
        [AllureStep]
        public List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId(List<CommonServiceForPointDBModel> commonService, int serviceIdExpected)
        {
            return commonService.FindAll(x => x.serviceID == serviceIdExpected);
        }

        //DETAIL - Expanded
        [AllureStep]
        public EventDetailPage VerifyValueInSubDetailInformation(EventDBModel eventDBModel)
        {
            Assert.AreEqual(eventDBModel.eventobjectdesc, GetAttributeValue(sourceInput, "value"));
            Assert.AreEqual(eventDBModel.eventstatedesc, GetFirstSelectedItemInDropdown(statusDd));
            Assert.AreEqual(CommonUtil.ParseDateTimeToFormat(eventDBModel.eventdate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT), GetAttributeValue(eventDateInput, "value"));
            Assert.AreEqual(CommonUtil.ParseDateTimeToFormat(eventDBModel.eventduedate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT), GetAttributeValue(dueDateInput, "value"));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(allocatedUnitDetailDd));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(resolutionCodeDd));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(assignedUserDetailDd));
            Assert.AreEqual("", GetAttributeValue(resolvedDateInput, "value"));
            Assert.AreEqual("", GetAttributeValue(endDateInput, "value"));
            Assert.AreEqual("", GetAttributeValue(clientRefInput, "value"));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyValueInSubDetailInformation(string sourceExp, string statusExp)
        {
            Assert.AreEqual(sourceExp, GetAttributeValue(sourceInput, "value"));
            Assert.AreEqual(statusExp, GetFirstSelectedItemInDropdown(statusDd));
            string eventDate = GetAttributeValue(eventDateInput, "value");
            Assert.IsTrue(eventDate.Contains(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT)), "Expected: " + CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT) + " but was " + eventDate);
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(allocatedUnitDetailDd));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(resolutionCodeDd));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(assignedUserDetailDd));
            Assert.AreEqual("", GetAttributeValue(resolvedDateInput, "value"));
            Assert.AreEqual("", GetAttributeValue(endDateInput, "value"));
            Assert.AreEqual("", GetAttributeValue(clientRefInput, "value"));
            return this;
        }

        [AllureStep]
        public EventDetailPage VerifyValueInSubDetailInformation(string sourceExp, string statusExp, string allocatedUnitValue)
        {
            Assert.AreEqual(sourceExp, GetAttributeValue(sourceInput, "value"));
            Assert.AreEqual(statusExp, GetFirstSelectedItemInDropdown(statusDd));
            string eventDate = GetAttributeValue(eventDateInput, "value");
            Assert.IsTrue(eventDate.Contains(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT)));
            Assert.AreEqual(allocatedUnitValue, GetFirstSelectedItemInDropdown(allocatedUnitDetailDd));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(resolutionCodeDd));
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(assignedUserDetailDd));
            Assert.AreEqual("", GetAttributeValue(resolvedDateInput, "value"));
            Assert.AreEqual("", GetAttributeValue(endDateInput, "value"));
            Assert.AreEqual("", GetAttributeValue(clientRefInput, "value"));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyDueDateEmpty()
        {
            Assert.AreEqual("", GetAttributeValue(dueDateInput, "value"));
            return this;
        }

        [AllureStep]
        public EventDetailPage VerifySourceInputReadOnly()
        {
            Assert.AreEqual(GetAttributeValue(parentSourceInput, "disabled"), "true");
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyDueDate(string dueDateValue)
        {
            Assert.IsTrue(GetAttributeValue(dueDateInput, "value").Contains(dueDateValue), "Expected " + GetAttributeValue(dueDateInput, "value") + " to contain " + dueDateValue);
            return this;
        }
        [AllureStep]
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
        [AllureStep]
        public EventDetailPage ClickHistoryTab()
        {
            ClickOnElement(anyTab, "history-tab");
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickDataTab()
        {
            ClickOnElement(anyTab, "data-tab");
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickMapTab()
        {
            ClickOnElement(anyTab, "map-tab");
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyDataInMapTab(string typeExp, string eventTypeExp, string serviceUnitEpx)
        {
            Assert.AreEqual(typeExp, GetElementText(typeInMapTab));
            Assert.AreEqual((eventTypeExp.ToLower().Replace("standard", "").Replace("-", "").Trim() + " > " + serviceUnitEpx).ToLower(), GetElementText(descInMapTab).ToLower());
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyHistoryWithDB(EventDBModel eventDBModel, string displayUserLogin)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.CreateEventEventTitle));
            Assert.AreEqual(GetElementText(stateOfEventInHistoryTab), eventDBModel.basestatedesc + ".");
            Assert.AreEqual(GetElementText(eventDateInHistoryTab), eventDBModel.eventdate.ToString(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT) + ".");
            Assert.AreEqual(GetElementText(dueDateInHistoryTab), eventDBModel.eventduedate.ToString(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT) + ".");
            Assert.AreEqual(GetElementText(createdByUserInHistoryTab), displayUserLogin);
            Assert.AreEqual(54, eventDBModel.eventcreatedbyuserID);
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyHistoryWithDB(EventDBModel eventDBModel, string displayUserLogin, int userId)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.CreateEventEventTitle));
            Assert.AreEqual(GetElementText(stateInHistoryTab), eventDBModel.basestatedesc + ".");
            Assert.AreEqual(GetElementText(eventDateInHistoryTab), eventDBModel.eventdate.ToString(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT) + ".");
            Assert.AreEqual(GetElementText(dueDateInHistoryTab), eventDBModel.eventduedate.ToString(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT) + ".");
            Assert.AreEqual(displayUserLogin, GetElementText(createdByUserInHistoryTab));
            Assert.AreEqual(eventDBModel.eventcreatedbyuserID, userId);
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyHistoryWithDBAfter(EventDBModel eventDBModel, string lastUpdated, int lastUpdatedUserId)
        {
            Assert.AreEqual(eventDBModel.lastupdated.ToString(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT), lastUpdated);
            Assert.AreEqual(eventDBModel.lastupdateduserID, lastUpdatedUserId);
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyHistoryData(string eventDate, string dueDate, string name, string notes, string state, string displayUserLogin)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.CreateEventEventTitle));
            Assert.AreEqual(state + ".", GetElementText(stateInHistoryTab));
            Assert.IsTrue(GetElementText(eventDateInHistoryTab).Contains(eventDate));
            Assert.IsTrue(GetElementText(dueDateInHistoryTab).Contains(dueDate));
            Assert.AreEqual(GetElementText(createdByUserInHistoryTab), displayUserLogin);
            Assert.AreEqual(name + ".", GetElementText(nameInHistoryTab));
            Assert.AreEqual(notes + ".", GetElementText(notesInHistoryTab));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyNewRecordInHistoryTab(string newStateValue, string newAllocatedUserValue, string newContractUnitValue, string user)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.AllocateEventEventTitle));
            Assert.AreEqual(newStateValue + ".", GetElementText(newStateInHistoryTab));
            Assert.AreEqual(newAllocatedUserValue + ".", GetElementText(newAllocatedUserInHistoryTab));
            //Assert.AreEqual(newContractUnitValue + ".", GetElementText(newContractUnitInHistoryTab));
            Assert.AreEqual(user, GetElementText(createdByUserNewRecord));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyNewRecordInHistoryTabAfterAllocate(string newStateValue, string user)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.AllocateEventEventTitle));
            Assert.AreEqual(newStateValue + ".", GetElementText(newStateInHistoryTab));
            Assert.AreEqual(user, GetElementText(createdByUserNewRecord));
            return this;
        }

        [AllureStep]
        public EventDetailPage VerifyRecordInHistoryTabAfterUpdate(string newNameValue, string user)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.UpdateEventTitle));
            Assert.AreEqual(newNameValue + ".", GetElementText(nameAfterUpdateInHistoryTab));
            Assert.AreEqual(user, GetElementText(createdByUserAfterUpdate));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyRecordInHistoryTabAfterCancel(string newState, string endDate, string resolvedDate, string user)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.CancelEventTitle));
            Assert.AreEqual(newState + ".", GetElementText(stateAfterCancelInHistoryTab));
            Assert.IsTrue(GetElementText(endDateAfterCancelInHistoryTab).Contains(endDate));
            Assert.IsTrue(GetElementText(resolvedDateAfterCancelInHistoryTab).Contains(resolvedDate));
            Assert.AreEqual(user, GetElementText(createdByUserAfterUpdate));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyRecordInHistoryTabAfterUpdateClientRefAndEmail(string clientRef, string emailAddress, string user)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.UpdateEventTitle));
            Assert.AreEqual(clientRef + ".", GetElementText(clientRefAfterUpdateInHistoryTab));
            Assert.AreEqual(emailAddress + ".", GetElementText(emailAddressAfterUpdateInHistoryTab));
            Assert.AreEqual(user, GetElementText(createdByUserAfterUpdate));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyRecordInHistoryTabAfterAddNote(string user)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.AddNoteEventTitle));
            Assert.AreEqual(user, GetElementText(createdByUserAfterAddNote));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyNotesAfterAddNote(string notesValue)
        {
            Assert.AreEqual(notesValue + ".", GetElementText(notesInAddNoteEvent));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyRecordInHistoryTabAfterAccept(string newStateValue, string user, string newName)
        {
            Assert.IsTrue(IsControlDisplayed(titleHistoryTab, CommonConstants.AcceptEventTitle));
            Assert.AreEqual(newStateValue, GetElementText(stateAfterAcceptInHistoryTab));
            Assert.AreEqual(newName + ".", GetElementText(firstNameAfterAccept));
            Assert.AreEqual(user, GetElementText(createdByUserAfterAccept));
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickDataSubTab()
        {
            ClickOnElement(anyTab, "data-tab");
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickOnAddNewCWActionBinTasklinesAndAddNewOne(string actionValue, string binValue)
        {
            ClickOnElement(addNewCWActionBinTasklinesBtn);
            //Action
            ClickOnElement(firstActionCWAction);
            ClickOnElement(firstActionCWActionOption, actionValue);
            //Bin
            ClickOnElement(firstBinCWAction);
            ClickOnElement(firstBinCWActionOption, binValue);
            return this;
        }
        [AllureStep]
        public new EventDetailPage VerifyNotDisplayErrorMessage()
        {
            Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickServicesSubTab()
        {
            ClickOnElement(anyTab, "services-tab");
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickOutstandingSubTab()
        {
            ClickOnElement(anyTab, "outstanding-tab");
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickPointHistorySubTab()
        {
            ClickOnElement(anyTab, "pointHistory-tab");
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyValueInStatus(string expectedStatus)
        {
            WaitUtil.WaitForElementVisible(statusDd);
            Assert.AreEqual(expectedStatus, GetFirstSelectedItemInDropdown(statusDd));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyEndDateAndResolvedDate()
        {
            Assert.IsTrue(GetAttributeValue(endDateInput, "value").Contains(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT)));
            Assert.IsTrue(GetAttributeValue(resolvedDateInput, "value").Contains(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT)));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyValueInAllocatedUnit(string expectedAllocatedUnit)
        {
            Assert.AreEqual(expectedAllocatedUnit, GetFirstSelectedItemInDropdown(allocatedUnitDetailDd));
            return this;
        }
        [AllureStep]
        public EventDetailPage InputClientRef(string clientRefValue)
        {
            SendKeys(clientRefInput, clientRefValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyValueInClientRef(string expectedClientRef)
        {
            Assert.AreEqual(expectedClientRef, GetAttributeValue(clientRefInput, "value"));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyValueInAssignedUser(string expectedAlssignedUser)
        {
            Assert.AreEqual(expectedAlssignedUser, GetFirstSelectedItemInDropdown(assignedUserDetailDd));
            return this;
        }

        [AllureStep]
        public EventDetailPage VerifyDataInServiceSubTab(List<ActiveSeviceModel> allActiveServicesSubTab, List<ServiceForPointDBModel> filterServiceWithContract)
        {
            for (int i = 0; i < allActiveServicesSubTab.Count; i++)
            {
                Assert.AreEqual(filterServiceWithContract[i].serviceunit, allActiveServicesSubTab[i].serviceUnit);
                Assert.AreEqual(filterServiceWithContract[i].service, allActiveServicesSubTab[i].service);
            }

            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyDataInServiceSubTab(List<ActiveSeviceModel> allActiveServicesSubTab, List<ActiveSeviceModel> activeSeviceModelsDetailPointSegment)
        {
            for(int i = 0; i < allActiveServicesSubTab.Count; i++)
            {
                Assert.AreEqual(activeSeviceModelsDetailPointSegment[i].service, allActiveServicesSubTab[i].service);
                Assert.AreEqual(activeSeviceModelsDetailPointSegment[i].serviceUnit, allActiveServicesSubTab[i].serviceUnit);
                Assert.AreEqual(activeSeviceModelsDetailPointSegment[i].schedule, allActiveServicesSubTab[i].schedule);
                Assert.AreEqual(activeSeviceModelsDetailPointSegment[i].lastService, allActiveServicesSubTab[i].lastService);
                Assert.AreEqual(activeSeviceModelsDetailPointSegment[i].nextService, allActiveServicesSubTab[i].nextService);
                Assert.AreEqual(activeSeviceModelsDetailPointSegment[i].allocationService, allActiveServicesSubTab[i].allocationService);
                Assert.AreEqual(activeSeviceModelsDetailPointSegment[i].assetTypeService, allActiveServicesSubTab[i].assetTypeService);
            }
           
            return this;
        }

        [AllureStep]
        public List<ActiveSeviceModel> GetAllActiveServiceWithServiceUnitModel()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allActiveRow = GetAllElements(allActiveServiceRow);
            for (int i = 0; i < allActiveRow.Count; i++)
            {
                string eventLocator = string.Format(eventDynamicLocator, (i+1).ToString());
                string serviceUnitValue = GetElementText(serviceUnitDynamic, (i + 1).ToString());
                string serviceValue = GetElementText(serviceWithServiceUnitDynamic, (i + 1).ToString());
                activeSeviceModels.Add(new ActiveSeviceModel(eventLocator, serviceUnitValue, serviceValue));
            }
            return activeSeviceModels;
        }
        [AllureStep]
        public List<ActiveSeviceModel> GetAllServiceInTab()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allActiveRow = GetAllElements(allActiveServiceRows);
            for (int i = 0; i < allActiveRow.Count; i++)
            {
                string eventLocator = string.Format(eventDynamicLocator, (i + 1).ToString());
                string serviceUnitValue = GetElementText(serviceUnitDynamic, (i + 1).ToString());
                string serviceValue = GetElementText(allserviceUnitDynamic, (i + 1).ToString());
                activeSeviceModels.Add(new ActiveSeviceModel(eventLocator, serviceUnitValue, serviceValue));
            }
            return activeSeviceModels;
        }
        [AllureStep]
        public List<ActiveSeviceModel> GetAllServiceWithoutServiceUnitModel(List<ActiveSeviceModel> GetAllServiceInTab)
        {
            List<ActiveSeviceModel> serviceModels = new List<ActiveSeviceModel>();
            foreach (ActiveSeviceModel activeSeviceModel in GetAllServiceInTab)
            {
                if (activeSeviceModel.serviceUnit.Equals("No Service Unit"))
                {
                    serviceModels.Add(activeSeviceModel);
                }
            }
            return serviceModels;
        }
        [AllureStep]
        public EventDetailPage VerifyPointHistoryInSubTab(List<PointHistoryModel> pointHistoryModelsInDetail, List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab)
        {
            for(int i = 0; i < pointHistoryModelsInDetail.Count; i++)
            {
                Assert.AreEqual(pointHistoryModelsInDetail[i].description, pointHistoryModelsInPointHistorySubTab[i].description);
                Assert.AreEqual(pointHistoryModelsInDetail[i].ID, pointHistoryModelsInPointHistorySubTab[i].ID);
                Assert.AreEqual(pointHistoryModelsInDetail[i].type, pointHistoryModelsInPointHistorySubTab[i].type);
                Assert.AreEqual(pointHistoryModelsInDetail[i].service, pointHistoryModelsInPointHistorySubTab[i].service);
                Assert.AreEqual(pointHistoryModelsInDetail[i].address, pointHistoryModelsInPointHistorySubTab[i].address);
                Assert.AreEqual(pointHistoryModelsInDetail[i].date, pointHistoryModelsInPointHistorySubTab[i].date);
                Assert.AreEqual(pointHistoryModelsInDetail[i].dueDate, pointHistoryModelsInPointHistorySubTab[i].dueDate);
                Assert.AreEqual(pointHistoryModelsInDetail[i].state, pointHistoryModelsInPointHistorySubTab[i].state);
            }
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyPointHistoryInSubTab(List<PointHistoryDBModel> pointHistoryDBModels, List<PointHistoryModel> pointHistoryModelsInPointHistorySubTab)
        {
            for (int i = 0; i < pointHistoryDBModels.Count; i++)
            {
                Assert.AreEqual(pointHistoryDBModels[i].description, pointHistoryModelsInPointHistorySubTab[i].description);
                Assert.AreEqual(pointHistoryDBModels[i].echoID, int.Parse(pointHistoryModelsInPointHistorySubTab[i].ID));
                Assert.AreEqual(pointHistoryDBModels[i].typename, pointHistoryModelsInPointHistorySubTab[i].type);
                Assert.AreEqual(pointHistoryDBModels[i].service, pointHistoryModelsInPointHistorySubTab[i].service);
                Assert.AreEqual(pointHistoryDBModels[i].location, pointHistoryModelsInPointHistorySubTab[i].address);
                Assert.AreEqual(CommonUtil.ParseDateTimeToFormat(pointHistoryDBModels[i].itemdate, CommonConstants.DATE_DD_MM_YYYY_FORMAT), pointHistoryModelsInPointHistorySubTab[i].date);
                Assert.AreEqual(CommonUtil.ParseDateTimeToFormat(pointHistoryDBModels[i].duedate, CommonConstants.DATE_DD_MM_YYYY_FORMAT), pointHistoryModelsInPointHistorySubTab[i].dueDate);
                Assert.AreEqual(pointHistoryDBModels[i].statedesc, pointHistoryModelsInPointHistorySubTab[i].state);
            }
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickAnyEventInActiveServiceRow(string eventLocator)
        {
            ClickOnElement(eventLocator);
            return this;
        }
        [AllureStep]
        public EventDetailPage InputNameInDataTab(string nameValue)
        {
            SendKeys(nameInput, nameValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage InputNoteInDataTab(string noteValue)
        {
            SendKeys(noteInputInDataTab, noteValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyValueInNameFieldInDataTab(string expName)
        {
            Assert.AreEqual(expName, GetAttributeValue(nameInput, "value"));
            return this;
        }
        [AllureStep]
        public EventDetailPage InputContactNumber(string numberValue)
        {
            SendKeys(contactNumberInput, numberValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage InputEmailAddress(string emailValue)
        {
            SendKeys(emailInput, emailValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyValueInEmailFieldInDataTab(string expName)
        {
            Assert.AreEqual(expName, GetAttributeValue(emailInput, "value"));
            return this;
        }

        [AllureStep]
        public List<string> GetAllEventTypeInDd()
        {
            List<string> eventTypes = new List<string>();
            List<IWebElement> allEventTypes = GetAllElements(allEventOptions);
            foreach (IWebElement eventLocator in allEventTypes)
            {
                eventTypes.Add(GetElementText(eventLocator));
            }
            return eventTypes;
        }
        [AllureStep]
        public EventDetailPage ClickOnAllocatedUnit()
        {
            ClickOnElement(allocatedUnitDetailDd);
            return this;
        }
        [AllureStep]
        public List<string> GetAllOptionInDetailDd()
        {
            List<string> results = new List<string>();
            List<IWebElement> allActions = GetAllElements(By.XPath("//div[@id='details-content']//select[@id='allocated-unit']/option"));
            foreach (IWebElement e in allActions)
            {
                results.Add(GetElementText(e));
            }
            return results;
        }
        [AllureStep]
        public EventDetailPage VerifySelectValueNotInDetailAllocatedUnit(string value)
        {
            Assert.IsFalse(GetAllOptionInDetailDd().Contains(value));
            return this;
        }
        [AllureStep]
        public EventDetailPage SelectAnyAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDetailOption, allocatedUnitValue);
            SleepTimeInMiliseconds(500);
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickOnFirstAllocatedUnit()
        {
            ClickOnElement(firstAllocatedUnitInDd);
            return this;
        }
        [AllureStep]
        public List<string> GetAllOptionInAllocatedUnitDetailSubTab()
        {
            List<string> results = new List<string>();
            List<IWebElement> allAllocatedOptions = GetAllElements(allAllocatedUnitInDetailSubTab);
            foreach(IWebElement e in allAllocatedOptions)
            {
                if(GetElementText(e) != "")
                {
                    results.Add(GetElementText(e));
                }
            }
            return results;
        }
        [AllureStep]
        public List<string> GetAllOptionInAssignedUserDetailSubTab()
        {
            List<string> results = new List<string>();
            List<IWebElement> allAllocatedOptions = GetAllElements(allAssignedUserInDetailSubTab);
            foreach (IWebElement e in allAllocatedOptions)
            {
                if (GetElementText(e) != "")
                {
                    results.Add(GetElementText(e));
                }
            }
            return results;
        }

        //POPUP CREATE INSPECTION
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
        public EventDetailPage VerifyDefaultSourceDd(string sourceValue)
        {
            Assert.AreEqual(sourceValue, GetFirstSelectedItemInDropdown(sourceDd));
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyPopupDisappears()
        {
            WaitUtil.WaitForElementInvisible(sourceDd);
            Assert.IsTrue(IsControlUnDisplayed(createBtn));
            Assert.IsTrue(IsControlUnDisplayed(sourceDd));
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickAndSelectInspectionType(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickAndVerifyAllocatedUnitNotContainValue(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            Assert.IsFalse(GetAllOptionInAllocateDd().Contains(allocatedUnitValue));
            return this;
        }

        [AllureStep]
        public List<string> GetAllOptionInAllocateDd()
        {
            List<string> results = new List<string>();
            List<IWebElement> allActions = GetAllElements(allocatedUnitDd);
            foreach (IWebElement e in allActions)
            {
                results.Add(GetElementText(e));
            }
            return results;
        }

        [AllureStep]
        public EventDetailPage ClickAndSelectAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickOnAssignedUserInDetailSubTab()
        {
            ClickOnElement(assignedUserDetailDd);
            return this;
        }

        [AllureStep]
        public EventDetailPage ClickAndSelectAssignedUser(string assignedUserValue)
        {
            ClickOnElement(assignedUserDd);
            ClickOnElement(assignedUserOption, assignedUserValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage InputValidFrom(string validFromValue)
        {
            SendKeys(validFromInput, validFromValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }
        [AllureStep]
        public EventDetailPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyValueInNoteField(string expNoteValue)
        {
            Assert.AreEqual(expNoteValue, GetAttributeValue(noteInputInDataTab, "value"));
            return this;
        }

        [AllureStep]
        public EventDetailPage ClickRefreshEventDetailBtn()
        {
            ClickOnElement(refreshBtn);
            return this;
        }

        //POINT HISTORY
        [AllureStep]
        public EventDetailPage ClickPointHistoryTab()
        {
            ClickOnElement(pointHistoryBtn);
            return this;
        }
        [AllureStep]
        public List<PointHistoryModel> GetAllPointHistoryWithScrollDown()
        {
            WaitUtil.WaitForElementVisible(descriptionColumn);
            List<PointHistoryModel> allModel = new List<PointHistoryModel>();
            var tableHeight = PointHistoryTableEle.GetTable().GetCssValue("height").Replace("px", "").AsInteger();
            int rowHeight = 25;
            int maxRow = tableHeight / rowHeight;
            int rowCount = 0;
            int retryCount = 0;
            while (allModel.Count < maxRow && retryCount < 5)
            {
                retryCount++;
                rowCount = PointHistoryTableEle.GetRows().Count;
                for (int i = 0; i < rowCount; i++)
                {
                    List<object> rowValues = PointHistoryTableEle.GetRowValue(i);
                    string desc = rowValues[PointHistoryTableEle.GetCellIndex(PHDescriptionCell)].AsString();
                    string ID = rowValues[PointHistoryTableEle.GetCellIndex(PHIDCell)].AsString();
                    string type = rowValues[PointHistoryTableEle.GetCellIndex(PHTypeCell)].AsString();
                    string service = rowValues[PointHistoryTableEle.GetCellIndex(PHServiceCell)].AsString();
                    string address = rowValues[PointHistoryTableEle.GetCellIndex(PHAddressCell)].AsString();
                    string date = rowValues[PointHistoryTableEle.GetCellIndex(PHDateCell)].AsString();
                    string dueDate = rowValues[PointHistoryTableEle.GetCellIndex(PHDueDateCell)].AsString();
                    string state = rowValues[PointHistoryTableEle.GetCellIndex(PHStateCell)].AsString();
                    string resolution = rowValues[PointHistoryTableEle.GetCellIndex(PHResolutionCodeCell)].AsString();
                    if (!allModel.Any(x => x.ID == ID))
                    {
                        allModel.Add(new PointHistoryModel(desc, ID, type, service, address, date, dueDate, state, resolution));
                    }
                }
                if (allModel.Count < maxRow)
                {
                    var lastRow = PointHistoryTableEle.GetRows().LastOrDefault();
                    if (lastRow == null) return allModel;
                    ScrollDownToElement(lastRow);
                    WaitForLoadingIconToDisappear();
                    WaitForLoadingIconToDisappear();
                    tableHeight = PointHistoryTableEle.GetTable().GetCssValue("height").Replace("px", "").AsInteger();
                    maxRow = tableHeight / rowHeight;
                }
            }
            return allModel;
        }

        [AllureStep]
        public List<PointHistoryModel> GetAllPointHistory()
        {
            WaitUtil.WaitForElementVisible(descriptionColumn);
            List<PointHistoryModel> allModel = new List<PointHistoryModel>();
            if (IsControlDisplayedNotThrowEx(allRowInPointHistoryTabel))
            {
                List<IWebElement> allRow = GetAllElements(allRowInPointHistoryTabel);
                for (int i = 0; i < allRow.Count; i++)
                {
                    string desc = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[0])[i]);
                    string ID = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[1])[i]);
                    string type = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[2])[i]);
                    string service = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[3])[i]);
                    string address = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[4])[i]);
                    string date = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[5])[i]);
                    string dueDate = GetElementText(GetAllElements(allDueDate)[i]);
                    string state = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[7])[i]);
                    string resolution = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[8])[i]);
                    allModel.Add(new PointHistoryModel(desc, ID, type, service, address, date, dueDate, state, resolution));
                }
            }
            return allModel;
        }

        [AllureStep]
        public List<PointHistoryModel> FilterPointHistoryWithId(List<PointHistoryModel> pointHistoryModelsAll, string pointHistoryIdInput)
        {
            return pointHistoryModelsAll.FindAll(x => x.ID.Equals(pointHistoryIdInput));
        }
        [AllureStep]
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
        [AllureStep]
        public EventDetailPage DoubleClickOnCreatedInspection()
        {
            DoubleClickOnElement(firstRowPointHistory);
            return this;
        }
        [AllureStep]
        public EventDetailPage FilterByPointHistoryId(string pointHistoryId)
        {
            SendKeys(filterInputById, pointHistoryId);
            ClickOnElement(eventTitle);
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyCurrentEventUrl()
        {
            Assert.IsTrue(GetCurrentUrl().Contains(WebUrl.MainPageUrl + "web/events/"));
            return this;
        }
        [AllureStep]
        public EventDetailPage VerifyDisplayBlueIcon()
        {
            Assert.IsTrue(IsControlDisplayed(blueIcon));
            return this;
        }
        [AllureStep]
        public string GetEventId()
        {
            return GetCurrentUrl()
                .Replace(WebUrl.MainPageUrl + "web/events/", "");
        }
        [AllureStep]
        public EventDetailPage ClickActionBtn()
        {
            ClickOnElement(actionBtn);
            return this;
        }
        [AllureStep]
        public List<string> GetAllOptionInActionDd()
        {
            List<string> results = new List<string>();
            List<IWebElement> allActions = GetAllElements(allOptionsInActionDd);
            foreach(IWebElement e in allActions)
            {
                results.Add(GetElementText(e));
            }
            return results;
        }
        [AllureStep]
        public EventDetailPage ClickAnyOptionInActionDd(string actionName)
        {
            ClickOnElement(anyOptionInActionDd, actionName);
            return this;
        }
        [AllureStep]
        public List<string> GetAllOptionInEventActions()
        {
            List<string> results = new List<string>();
            List<IWebElement> allActions = GetAllElements(allOptionsInEventAction);
            foreach (IWebElement e in allActions)
            {
                results.Add(GetElementText(e));
            }
            return results;
        }
        [AllureStep]
        public EventDetailPage VerifyActionAreTheSame(List<string> actions, List<string> eventActions)
        {
            Assert.IsTrue(actions.SequenceEqual(eventActions));
            return this;
        }


        //DB
        [AllureStep]
        public List<ServiceForPointDBModel> FilterServicePointWithServiceID(List<ServiceForPointDBModel> allServices, List<ServiceDBModel> serviceDBModels)
        {
            List<ServiceForPointDBModel> result = new List<ServiceForPointDBModel>();
            for (int i = 0; i < serviceDBModels.Count; i++)
            {
                foreach (ServiceForPointDBModel model in allServices)
                {
                    if (model.serviceID != serviceDBModels[i].serviceID)
                    {
                        result.Add(model);
                    }
                }
            }
            return result;

        }

        //Event Actions
        private readonly By allocateEventBtnInEventActions = By.XPath("//span[text()='Allocate Event']/parent::button/parent::li");
        private readonly By acceptBtnInEventActions = By.XPath("//span[text()='Accept']/parent::button/parent::li");
        private readonly By addNoteBtnInEventActions = By.XPath("//span[text()='Add Note']/parent::button/parent::li");
        private readonly By cancelBtnInEventActions = By.XPath("//span[text()='Cancel']/parent::button/parent::li");

        [AllureStep]
        public EventDetailPage ClickAllocateEventInEventActionsPanel()
        {
            ClickOnElement(allocateEventBtnInEventActions);
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickAcceptInEventActionsPanel()
        {
            ClickOnElement(acceptBtnInEventActions);
            return this;
        }
        [AllureStep]
        public EventActionPage ClickAddNoteInEventsActionsPanel()
        {
            ClickOnElement(addNoteBtnInEventActions);
            return PageFactoryManager.Get< EventActionPage>();
        }
        [AllureStep]
        public EventDetailPage ClickCancelInEventsActionsPanel()
        {
            ClickOnElement(cancelBtnInEventActions);
            return this;
        }

    }
}
