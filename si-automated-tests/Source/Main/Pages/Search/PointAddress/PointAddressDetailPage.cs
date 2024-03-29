﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels.GetAllServicesForPoint2;
using si_automated_tests.Source.Main.DBModels.GetServiceInfoForPoint;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Services;
using si_automated_tests.Source.Main.Pages.Events;
using static si_automated_tests.Source.Main.Models.ActiveSeviceModel;

namespace si_automated_tests.Source.Main.Pages.PointAddress
{
    public class PointAddressDetailPage : BasePageCommonActions
    {
        public readonly By SubscriptionTab = By.XPath("//a[@aria-controls='subscriptions-tab']/parent::li");
        private readonly By titleDetail = By.XPath("//h4[text()='Point Address']");
        private readonly By pointAddressName = By.XPath("//span[@class='object-name']");
        private readonly By inspectBtn = By.CssSelector("button[title='Inspect']");
        private readonly By allAservicesTab = By.XPath("//a[@aria-controls='allServices-tab']/parent::li");
        private readonly By dataTab = By.XPath("//a[@aria-controls='data-tab']/parent::li");
        private readonly By announcementsTab = By.XPath("//a[@aria-controls='announcements-tab']/parent::li");
        private readonly By mapTab = By.XPath("//a[@aria-controls='map-tab']/parent::li");
        private readonly By risksTab = By.XPath("//a[@aria-controls='risks-tab']/parent::li");
        private readonly By sectorsTab = By.XPath("//a[@aria-controls='sectors-tab']/parent::li");
        private readonly By notificationTab = By.XPath("//a[@aria-controls='notifications-tab']/parent::li");

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
        public PointAddressDetailPage VerifyNewSubscription(string id, string firstName, string lastName, string mobile, string subjectDescription)
        {
            int newIdx = SubscriptionTableEle.GetRows().Count - 1;
            VerifyCellValue(SubscriptionTableEle, newIdx, 0, id);
            VerifyCellValue(SubscriptionTableEle, newIdx, 2, firstName + " " + lastName);
            VerifyCellValue(SubscriptionTableEle, newIdx, 3, mobile);
            //string subjectDescriptionCellValue = SubscriptionTableEle.GetCellValue(newIdx, 9).AsString().ToLower();
            //Assert.IsTrue(subjectDescription.ToLower().Contains(subjectDescriptionCellValue));
            return this;
        }

        [AllureStep]
        public string GetNewContractId()
        {
            int newIdx = SubscriptionTableEle.GetRows().Count - 1;
            return SubscriptionTableEle.GetCellValue(newIdx, 1).AsString(); 
        }
        #endregion

        //DETAILS TAB
        private readonly By detailTab = By.XPath("//a[@aria-controls='details-tab']/parent::li");
        //private readonly By propertyName = By.Id("propertyName");
        //private readonly By property = By.Id("property");
        //private readonly By toProperty = By.Id("toProperty");
        //private readonly By pointSegment = By.Id("point-segment");
        private readonly By pointAddressTypeSelect = By.Id("point-address-type");
        public readonly By ClientRefInput = By.Id("client-ref");
        public readonly By DescriptionInput = By.Id("description");
        public readonly By LatitudeInput = By.Id("latitude");
        public readonly By LongitudeInput = By.Id("longitude");

        //POPUP
        private readonly By createTitle = By.XPath("//div[@id='inspection-modal']//h4[text()='Create ']");
        private readonly By sourceDd = By.CssSelector("div#inspection-modal select#source");
        private readonly By inspectionTypeDd = By.CssSelector("div#inspection-modal select#inspection-type");
        private readonly By validFromInput = By.CssSelector("div#inspection-modal input#valid-from");
        private readonly By validToInput = By.CssSelector("div#inspection-modal input#valid-to");
        private readonly By allocatedUnitDd = By.XPath("//label[text()=' Allocated Unit']/following-sibling::div/select");
        private readonly By assignedUserDd = By.XPath("//div[@id='inspection-modal']//label[text()='Assigned User']/following-sibling::div/select");
        private readonly By noteInput = By.CssSelector("div#inspection-modal textarea#note");
        private readonly By cancelBtn = By.XPath("//div[@id='inspection-modal']//button[text()='Cancel']");
        private readonly By createBtn = By.XPath("//div[@id='inspection-modal']//button[text()='Create']");
        private readonly By closeBtn = By.XPath("//div[@id='inspection-modal']//h4[text()='Create ']/parent::div/following-sibling::div/button[@aria-label='Close']");

        //POINT HISTORY TAB
        private readonly By pointHistoryTab = By.XPath("//a[@aria-controls='pointHistory-tab']/parent::li");
        private readonly By descriptionColumn = By.XPath("//span[text()='Description']");
        private readonly By allRowInPointHistoryTabel = By.XPath("//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div");
        private const string columnInRowPointHistoryTab = "//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private readonly By filterInputById = By.XPath("//div[@id='pointHistory-tab']//div[contains(@class, 'l2 r2')]/descendant::input");
        private readonly By allDueDate = By.XPath("//div[@class='slick-cell l7 r7']");

        //ACTIVE SERVICES TAB
        private readonly By activeServiceTab = By.XPath("//a[@aria-controls='activeServices-tab']/parent::li");
        private readonly By allActiveServiceWithServiceUnitRow = By.XPath("//div[@class='parent-row']//span[@title='Open Service Task']");
        private readonly By allActiveServiceRows = By.XPath("//span[@title='Open Service Task' or @title='0' or contains(@data-bind, 'Open Service Task')]");

        private const string eventDynamicLocator = "//div[@class='parent-row'][{0}]//div[text()='Event']";
        private const string serviceUnitDynamic = "//div[@class='parent-row'][{0}]//div[@title='Open Service Unit']/span";
        private const string serviceWithServiceUnitDynamic = "//div[@class='parent-row'][{0}]//span[@title='Open Service Task']";
        private const string allserviceUnitDynamic = "//div[@class='parent-row'][{0}]//div[contains(@data-bind, 'service-grid-service')]";
        private const string statusDescParentRow = "//div[@class='parent-row'][{0}]//b";
        private const string scheduleParentRow = "//div[@class='service-text']//div[@data-bind='text: $data']";
        private const string scheduleRow = "//div[@class='service-text']/div[@data-bind='text: $data']";
        private const string lastParentRow = "//div[@class='parent-row'][{0}]//div[@title='Open Service Unit']//following-sibling::div//span[@data-bind='text: ew.formatDateForUser($data.lastDate)']";
        private const string nextParentRow = "//div[@class='parent-row'][{0}]//div[@title='Open Service Unit']//following-sibling::div//span[@data-bind='text: ew.formatDateForUser($data.nextDate)']";
        private const string assetTypeParentRow = "//div[@class='parent-row'][{0}]//div[@data-bind='foreach: $data.asset']//div[@data-bind='text: $data']";
        private readonly By allocationRow = By.XPath("//div[@class='parent-row']//div[@data-bind='foreach: $data.asset']/following-sibling::div[1]");
        //Chrild row
        private const string numberOfChirdRow = "//div[@class='parent-row'][{0}]//div[@class='services-grid--row'][2]";
        private const string roundLegChirdRow = "//div[@class='parent-row'][{0}]//div[@class='services-grid--row'][2]//span[text()='Round Leg: ']/following-sibling::span[1]";
        private const string lastChirdRow = "//div[@class='services-grid--row'][{0}]//div[@class='child-row' and not(contains(@style,'display: none;'))]//span[@data-bind='text: ew.formatDateForUser($data.lastDate)']";
        private const string nextChrirdRow = "//div[@class='services-grid--row'][{0}]//div[@class='child-row' and not(contains(@style,'display: none;'))]//div[@data-bind='text: $data.next']";
        private const string assetTypeChildRow = "//div[@class='services-grid--row'][{0}]//div[@class='child-row' and not(contains(@style,'display: none;'))]//div[@data-bind='$data']";
        private const string allocationChildRow = "//div[@class='services-grid--row'][{0}]//div[@class='child-row' and not(contains(@style,'display: none;'))]//span[contains(@data-bind, '$data.allocation')]";
        private const string resolutionChirdRow = "//div[@class='parent-row'][{0}]//div[@class='services-grid--row'][2]//span[text()='Round Leg: ']/following-sibling::b";

        //ACTION
        private const string actionBtnAtRow = "//tr[{0}]//label[@id='btndropdown']";
        private const string addServiceUnitBtnAtRow = "//tr[{0}]//button[contains(string(), 'Add Service Unit')]";
        private const string findServiceUnitBtnAtRow = "//tr[{0}]//button[contains(string(), 'Find Service Unit')]";
        private const string serviceUnitAtRow = "//tr[{0}]//a[@title='Open Service Unit']";
        private const string serviceUnitAtRowWithServiceName = "//tr/td[contains(text(), '{0}')]/following-sibling::td/a[@title='Open Service Unit' and text()='{1}']";
        private const string serviceUnitAtRowWithServiceNameAndActive = "//tr/td[contains(text(), '{0}')]/following-sibling::td/div[not(contains(@style, 'display: none;')) and text()='Active']/parent::td/preceding-sibling::td//a[@title='Open Service Unit' and text()='{1}']";
        private const string serviceUnitAtRowWithServiceNameAndNoneStatus = "//tr/td[contains(text(), '{0}')]/following-sibling::td/div[contains(@style, 'display: none;') and text()='Active']/parent::td/preceding-sibling::td//a[@title='Open Service Unit' and text()='{1}']";
        private const string statusActiveAtRow = "//tr[{0}]//div[@data-bind='visible: $data.active']";
        private const string taskCountAtRow = "//tr[{0}]//td[@data-bind='text: $data.taskCount']";
        private const string scheduleCountAtRow = "//tr[{0}]//td[@data-bind='text: $data.scheduleCount']";

        //ALL SERVICES
        private readonly By totalServicesRows = By.CssSelector("tbody[data-bind='foreach: allServices']>tr");
        private readonly By allContractRows = By.CssSelector("tbody td[data-bind='text: $data.contract']");
        private readonly By allServiceRows = By.CssSelector("tbody td[data-bind='text: $data.service']");
        private readonly By allServiceUnitRows = By.CssSelector("tbody[data-bind='foreach: allServices']>tr>td:nth-child(3)");
        private readonly By allTaskCountRows = By.CssSelector("tbody td[data-bind='text: $data.taskCount']");
        private readonly By allScheduledCountRows = By.CssSelector("tbody[data-bind='foreach: allServices'] td[data-bind='text: $data.scheduleCount']");
        private readonly By allStatusRows = By.CssSelector("tbody[data-bind='foreach: allServices'] td:nth-child(6)");
        private const string serviceUnitLink = "//tbody/tr[{0}]//a[@title='Open Service Unit' and not(contains(@style, 'display: none;'))]";
        private readonly By ServiceRows = By.XPath("//div[@id='activeServices-tab']//div[@class='parent-row']//div[@data-bind='foreach: $data.asset']");

        #region Active Service Tab
        public readonly By ServiceTab = By.XPath("//a[@aria-controls='activeServices-tab']");
        public readonly By AssetTypeColumn = By.XPath("//div[@class='services-grid--root']//div[text()='Asset Type (Product)']");
        #endregion

        [AllureStep]
        public List<AllServiceInPointAddressModel> GetAllServicesInAllServicesTab()
        {
            WaitUtil.WaitForAllElementsPresent(totalServicesRows);
            List<AllServiceInPointAddressModel> result = new List<AllServiceInPointAddressModel>();
            List<IWebElement> allRows = GetAllElements(totalServicesRows);
            for(int i = 0; i < allRows.Count; i++)
            {
                string contract = GetElementText(GetAllElements(allContractRows)[i]);
                string service = GetElementText(GetAllElements(allServiceRows)[i]);
                string serviceUnit = GetElementText(GetAllElements(allServiceUnitRows)[i]);
                string taskCount = GetElementText(GetAllElements(allTaskCountRows)[i]);
                string scheduleCount = GetElementText(GetAllElements(allScheduledCountRows)[i]);
                string status = GetElementText(GetAllElements(allStatusRows)[i]);
                string serviceUnitLinkToDetail = "";
                if (IsControlDisplayedNotThrowEx(string.Format(serviceUnitLink, (i + 1).ToString()))) {
                    serviceUnitLinkToDetail = string.Format(serviceUnitLink, (i + 1).ToString());
                }
                result.Add(new AllServiceInPointAddressModel(contract, service, serviceUnit, taskCount, scheduleCount, status, serviceUnitLinkToDetail));
            }
            return result;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickServiceUnitLinkAdded(string locatorToDetail)
        {
            ClickOnElement(locatorToDetail);
            return PageFactoryManager.Get<ServiceUnitDetailPage>();
        }
        [AllureStep]
        public PointAddressDetailPage VerifyDBWithUI(List<AllServiceInPointAddressModel> allServiceInPointAddresses, List<ServiceForPoint2DBModel> serviceForPoint2DBModels)
        {
            for(int i = 0; i < allServiceInPointAddresses.Count; i++)
            {
                Assert.AreEqual(serviceForPoint2DBModels[i].Contract, allServiceInPointAddresses[i].contract, "Wrong Contract");
                Assert.AreEqual(serviceForPoint2DBModels[i].Service, allServiceInPointAddresses[i].service, "Wrong Service");
                if(serviceForPoint2DBModels[i].ServiceUnit == null)
                {
                    Assert.AreEqual("", allServiceInPointAddresses[i].serviceUnit, "Wrong Service Unit");
                } else
                {
                    Assert.AreEqual(serviceForPoint2DBModels[i].ServiceUnit, allServiceInPointAddresses[i].serviceUnit, "Wrong Service Unit");
                }
                Assert.AreEqual(serviceForPoint2DBModels[i].STCount.ToString(), allServiceInPointAddresses[i].taskCount, "Wrong task count");
                Assert.AreEqual(serviceForPoint2DBModels[i].STSCount.ToString(), allServiceInPointAddresses[i].scheduleCount, "Wrong schedule count");
                if(serviceForPoint2DBModels[i].ActiveState == 0)
                {
                    Assert.AreEqual("", allServiceInPointAddresses[i].status, "Wrong task count");
                } else
                {
                    Assert.AreEqual("Active", allServiceInPointAddresses[i].status, "Wrong state");
                }
            }

            return this;
        }

        [AllureStep]
        public List<ActiveSeviceModel> GetAllServiceWithServiceUnitModel363256()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();

            List<IWebElement> allScheduleParentRow = GetAllElements(scheduleParentRow);
            List<IWebElement> allAllocationRow = GetAllElements(allocationRow);
            List<IWebElement> allActiveRow = GetAllElements(allActiveServiceWithServiceUnitRow);
            for (int i = 0; i < allActiveRow.Count; i++)
            {
                string eventLocator = string.Format(eventDynamicLocator, (i + 1).ToString());
                string serviceUnitValue = GetElementText(serviceUnitDynamic, (i + 1).ToString());
                string serviceValue = GetElementText(serviceWithServiceUnitDynamic, (i + 1).ToString());
                string statusDescParentValue = GetElementText(statusDescParentRow, (i + 1).ToString());
                string scheduleParentValue = GetElementText(allScheduleParentRow[i]);
                string lastParentValue = GetElementText(lastParentRow, (i + 1).ToString());
                string nextParentValue = GetElementText(nextParentRow, (i + 1).ToString());
                string assetTypeParentValue = "";
                List<IWebElement> allAssetTypes = GetAllElements(string.Format(assetTypeParentRow, (i + 1).ToString()));
                if (allAssetTypes.Count > 1)
                {
                    for (int k = 0; k < allAssetTypes.Count; k++)
                    {
                        assetTypeParentValue += " " + GetElementText(allAssetTypes[k]);
                    }
                }
                else if (allAssetTypes.Count == 1)
                {
                    assetTypeParentValue = GetElementText(allAssetTypes[0]);
                }
                string allocation = GetElementText(allAllocationRow[i]);
                //Get child row info
                List<ChildSchedule> listSchedule = new List<ChildSchedule>();
                List<IWebElement> allChildRows = GetAllElements(numberOfChirdRow, (i + 1).ToString());
                for (int j = 0; j < allChildRows.Count; j++)
                {
                    string roundLeg = GetElementText(GetAllElements(roundLegChirdRow, (i + 1).ToString())[j]);
                    string resolutionCode = GetElementText(GetAllElements(resolutionChirdRow, (i + 1).ToString())[j]);
                    listSchedule.Add(new ChildSchedule(roundLeg, resolutionCode));
                }

                activeSeviceModels.Add(new ActiveSeviceModel(eventLocator, serviceUnitValue, serviceValue, statusDescParentValue, scheduleParentValue, lastParentValue, nextParentValue, assetTypeParentValue.Trim(), allocation, listSchedule));
            }
            return activeSeviceModels;
        }

        [AllureStep]
        public List<ActiveSeviceModel> GetAllActiveService483995()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();

            List<IWebElement> allScheduleParentRow = GetAllElements(scheduleRow);
            List<IWebElement> allActiveRow = GetAllElements(allActiveServiceWithServiceUnitRow);
            for (int i = 0; i < allActiveRow.Count; i++)
            {
                string eventLocator = string.Format(eventDynamicLocator, (i + 1).ToString());
                string serviceUnitValue = GetElementText(serviceUnitDynamic, (i + 1).ToString());
                string serviceValue = GetElementText(serviceWithServiceUnitDynamic, (i + 1).ToString());
                string statusDescParentValue = GetElementText(statusDescParentRow, (i + 1).ToString());
                string scheduleParentValue = GetElementText(allScheduleParentRow[i]);
                string lastParentValue = GetElementText(lastParentRow, (i + 1).ToString());
                string nextParentValue = GetElementText(nextParentRow, (i + 1).ToString());
                string assetTypeParentValue = GetElementText(assetTypeParentRow, (i + 1).ToString());
                string allocationValue = GetElementText(GetAllElements(allocationRow)[i]);
                ActiveSeviceModel activeSeviceModel = new ActiveSeviceModel(serviceUnitValue, serviceValue, scheduleParentValue, lastParentValue, nextParentValue, assetTypeParentValue, allocationValue);
                activeSeviceModel.status = statusDescParentValue;
                activeSeviceModel.eventLocator = eventLocator;

                activeSeviceModels.Add(activeSeviceModel);
            }
            return activeSeviceModels;
        }
        [AllureStep]
        public List<ServiceForPointDBModel> GetServiceWithoutServiceUnitDB(List<ServiceForPointDBModel> serviceForPoint)
        {
            return serviceForPoint.FindAll(x => x.serviceunit.Equals("No Service Unit"));
        }
        [AllureStep]
        public PointAddressDetailPage VerifyDataInActiveServicesTab(List<ActiveSeviceModel> activeSeviceWithoutServiceUnitModels, List<ServiceForPointDBModel> serviceForPoint)
        {
            //DB get service without service unit
            List<ServiceForPointDBModel> allServiceWithoutServiceUnitDB = GetServiceWithoutServiceUnitDB(serviceForPoint);
            for (int i = 0; i < activeSeviceWithoutServiceUnitModels.Count; i++)
            {
                Assert.AreEqual(allServiceWithoutServiceUnitDB[i].serviceunit, activeSeviceWithoutServiceUnitModels[i].serviceUnit);
                Assert.AreEqual(allServiceWithoutServiceUnitDB[i].service, activeSeviceWithoutServiceUnitModels[i].service);
            }
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage VerifyDataInActiveServicesTab363256(List<ActiveSeviceModel> activeSeviceWithServiceUnitModels, List<ServiceForPointDBModel> serviceForPoint)
        {
            for (int i = 0; i < activeSeviceWithServiceUnitModels.Count; i++)
            {
                Assert.AreEqual(serviceForPoint[i].serviceunit, activeSeviceWithServiceUnitModels[i].serviceUnit, "Wrong service unit " + i);
                Assert.AreEqual(serviceForPoint[i].statusdesc, activeSeviceWithServiceUnitModels[i].status, "Wrong status " + i);
                Assert.AreEqual(serviceForPoint[i].service, activeSeviceWithServiceUnitModels[i].service, "Wrong service " + i);
                Assert.AreEqual("Every " + serviceForPoint[i].round, activeSeviceWithServiceUnitModels[i].schedule, "Wrong schedule " + i);
                if (serviceForPoint[i].next.Equals("Tomorrow"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, 1), activeSeviceWithServiceUnitModels[i].nextService, "Wrong next service " + i);
                }
                else if (serviceForPoint[i].next.Equals("Yesterday"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, -1), activeSeviceWithServiceUnitModels[i].nextService, "Wrong next service " + i);
                }
                else
                {
                    Assert.AreEqual(serviceForPoint[i].next.Replace("-", "/"), activeSeviceWithServiceUnitModels[i].nextService, "Wrong next service " + i);
                }
                if (serviceForPoint[i].last.Equals("Tomorrow"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, 1), activeSeviceWithServiceUnitModels[i].lastService, "Wrong last service " + i);
                }
                else if (serviceForPoint[i].last.Equals("Today"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB), activeSeviceWithServiceUnitModels[i].lastService, "Wrong last service " + i);
                }
                else if (serviceForPoint[i].last.Equals("Yesterday"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, -1), activeSeviceWithServiceUnitModels[i].lastService, "Wrong last service " + i);
                }
                else
                {
                    Assert.AreEqual(serviceForPoint[i].last.Replace("-", "/"), activeSeviceWithServiceUnitModels[i].lastService, "Wrong last service " + i);
                }

                Assert.AreEqual(serviceForPoint[i].assets, activeSeviceWithServiceUnitModels[i].assetTypeService, "Wrong asset type service " + i);
                Assert.AreEqual(serviceForPoint[i].statusdesc, activeSeviceWithServiceUnitModels[i].listChildSchedule[0].resolutionCode, "Wrong Status desc " + i);
                Assert.AreEqual(serviceForPoint[i].roundlegdesc + ":", activeSeviceWithServiceUnitModels[i].listChildSchedule[0].round, "Wrong Round Leg " + i);
                Assert.AreEqual(serviceForPoint[i].roundgroup + " - " + serviceForPoint[i].round, activeSeviceWithServiceUnitModels[i].allocationService, "Wrong [Allocation] " + i);
            }
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage VerifyDataInActiveServicesTab(List<ActiveSeviceModel> activeSeviceWithServiceUnitModels, List<ServiceForPointDBModel> serviceForPoint, List<ServiceTaskForPointDBModel> serviceTaskForPoint)
        {
            for (int i = 0; i < activeSeviceWithServiceUnitModels.Count; i++)
            {
                Assert.AreEqual(serviceForPoint[i].serviceunit, activeSeviceWithServiceUnitModels[i].serviceUnit);
                Assert.AreEqual(serviceForPoint[i].statusdesc, activeSeviceWithServiceUnitModels[i].status);
                Assert.AreEqual(serviceForPoint[i].service, activeSeviceWithServiceUnitModels[i].service);
                Assert.AreEqual("Multiple", activeSeviceWithServiceUnitModels[i].schedule);
                if(serviceForPoint[i].next.Equals("Tomorrow")) {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, 1), activeSeviceWithServiceUnitModels[i].nextService);
                }
                else if(serviceForPoint[i].next.Equals("Yesterday"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, -1), activeSeviceWithServiceUnitModels[i].nextService);
                }
                else
                {
                    Assert.AreEqual(serviceForPoint[i].next.Replace("-", "/"), activeSeviceWithServiceUnitModels[i].nextService);
                }
                if(serviceForPoint[i].last.Equals("Tomorrow"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, 1), activeSeviceWithServiceUnitModels[i].lastService);
                }
                else if (serviceForPoint[i].last.Equals("Today"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB), activeSeviceWithServiceUnitModels[i].lastService);
                }
                else if (serviceForPoint[i].last.Equals("Yesterday"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, -1), activeSeviceWithServiceUnitModels[i].lastService);
                }
                else
                {
                    Assert.AreEqual(serviceForPoint[i].last.Replace("-", "/"), activeSeviceWithServiceUnitModels[i].lastService);
                }

                Assert.AreEqual(serviceForPoint[i].assets, activeSeviceWithServiceUnitModels[i].assetTypeService);

                //VERIFY CHIRLD ROWs
                List<ServiceTaskForPointDBModel> allSTForPointWithSameAssetType = GetServiceTaskForPointWithSameAssetType(serviceTaskForPoint, serviceForPoint[i].assets);
                List<ChildSchedule> childSchedules = activeSeviceWithServiceUnitModels[i].listChildSchedule;
                for (int j = 0; j < childSchedules.Count; j++)
                {
                    Assert.AreEqual("Every " + allSTForPointWithSameAssetType[j].round.Trim(), childSchedules[j].round.Trim());
                    if(allSTForPointWithSameAssetType[j].last.Equals("Today")) {
                        Assert.AreEqual(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB), childSchedules[j].lastRound);
                    }
                    else if (allSTForPointWithSameAssetType[j].last.Equals("Yesterday"))
                    {
                        Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, -1), childSchedules[j].lastRound);
                    }
                    else
                    {
                        Assert.AreEqual(allSTForPointWithSameAssetType[j].last.Replace("-", "/"), childSchedules[j].lastRound);
                    }

                    if (allSTForPointWithSameAssetType[j].next.Equals("Tomorrow"))
                    {
                        Assert.AreEqual(allSTForPointWithSameAssetType[j].next, childSchedules[j].nextRound);
                    }
                    else if (allSTForPointWithSameAssetType[j].next.Equals("Yesterday"))
                    {
                        Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB, -1), childSchedules[j].nextRound);
                    }
                    else
                    {
                        //Assert.AreEqual(allSTForPointWithSameAssetType[j].next, CommonUtil.ParseDateTimeStringToNewFormat(childSchedules[j].nextRound, CommonConstants.DATE_DD_MM_YYYY_FORMAT_DB));
                    }
                    Assert.AreEqual(allSTForPointWithSameAssetType[j].roundgroup + " - " + allSTForPointWithSameAssetType[j].round.Trim(), childSchedules[j].allocationRound);
                }
            }
            return this;
        }
        [AllureStep]
        public List<ActiveSeviceModel> GetAllServiceInTab()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allActiveRow = GetAllElements(allActiveServiceRows);
            for (int i = 0; i < allActiveRow.Count; i++)
            {
                string eventParentLocator = string.Format(eventDynamicLocator, (i + 1).ToString());
                string serviceParentUnitValue = GetElementText(serviceUnitDynamic, (i + 1).ToString());
                string serviceParentValue = GetElementText(allserviceUnitDynamic, (i + 1).ToString());

                activeSeviceModels.Add(new ActiveSeviceModel(eventParentLocator, serviceParentUnitValue, serviceParentValue));
            }
            return activeSeviceModels;
        }

        //DB
        [AllureStep]
        public List<ServiceTaskForPointDBModel> GetServiceTaskForPointWithSameAssetType(List<ServiceTaskForPointDBModel> serviceTaskForPoint, string assetType)
        {
            List<ServiceTaskForPointDBModel> result = new List<ServiceTaskForPointDBModel>();
            foreach (ServiceTaskForPointDBModel services in serviceTaskForPoint)
            {
                if(services.assets.Equals(assetType) && services.roundscheduleID != 0) {
                    result.Add(services);
                }
            }
            //Sort with next date
            
            return result.OrderBy(x => x.nextdate).ToList();
        }
        [AllureStep]
        public List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId(List<CommonServiceForPointDBModel> commonService, int serviceIdExpected)
        {
            return commonService.FindAll(x => x.serviceID == serviceIdExpected);
        }

        private const string eventOptions = "//div[@id='create-event-dropdown']//li[text()='{0}']";
        private readonly By allEventOptions = By.CssSelector("ul#create-event-opts>li");

        //DYNAMIC LOCATOR
        private const string inspectionTypeOption = "//div[@id='inspection-modal']//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()=' Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//div[@id='inspection-modal']//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";

        [AllureStep]
        public PointAddressDetailPage WaitForPointAddressDetailDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(titleDetail);
            WaitUtil.WaitForElementVisible(pointHistoryTab);
            return this;
        }
        [AllureStep]
        public string GetPointAddressName()
        {
            return GetElementText(pointAddressName);
        }
        [AllureStep]
        public PointAddressDetailPage ClickInspectBtn()
        {
            ClickOnElement(inspectBtn);
            return this;
        }
        [AllureStep]
        public String GetPointAddressId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/point-addresses/", "");
        }
        [AllureStep]
        public PointAddressDetailPage VerifyPointAddressId(string idExpected)
        {
            Assert.AreEqual(idExpected, GetPointAddressId());
            return this;
        }

        //INSPECTION MODEL
        [AllureStep]
        public PointAddressDetailPage IsCreateInspectionPopup()
        {
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
            Assert.AreEqual(GetCssValue(inspectionTypeDd, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(allocatedUnitDd, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage VerifyDefaulValue()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(inspectionTypeDd), "Select... ...");
            Assert.IsTrue(GetFirstSelectedItemInDropdown(allocatedUnitDd).Contains("Select..."));
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), "Select... ...");
            //Date
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage VerifyDefaultSourceDd(string sourceValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceValue);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickAndSelectInspectionType(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickAndSelectAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickAndSelectAssignedUser(string assignedUserValue)
        {
            ClickOnElement(assignedUserDd);
            ClickOnElement(assignedUserOption, assignedUserValue);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnInspectionCreatedLink()
        {
            ClickOnElement("//a[@id='echo-notify-success-link']");
            return this;
        }

        //POINT HISTORY TAB
        [AllureStep]
        public PointAddressDetailPage ClickPointHistoryTab()
        {
            ClickOnElement(pointHistoryTab);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsPointHistoryTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(pointHistoryTab, "class"));
            return this;
        }
        [AllureStep]
        public List<PointHistoryModel> GetAllPointHistory()
        {
            WaitUtil.WaitForElementVisible(descriptionColumn);
            List<PointHistoryModel> allModel = new List<PointHistoryModel>();
            if(IsControlDisplayedNotThrowEx(allRowInPointHistoryTabel))
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
        public PointAddressDetailPage VerifyPointHistory(PointHistoryModel pointHistoryModelActual, string desc, string id, string type, string address, string date, string dueDate, string state)
        {
            Assert.AreEqual(desc, pointHistoryModelActual.description);
            Assert.AreEqual(id, pointHistoryModelActual.ID);
            Assert.AreEqual(type, pointHistoryModelActual.type);
            Assert.AreEqual(address, pointHistoryModelActual.address);
            Assert.AreEqual(date, pointHistoryModelActual.date);
            Assert.AreEqual(dueDate, pointHistoryModelActual.dueDate);
            Assert.AreEqual(state, pointHistoryModelActual.state);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage FilterByPointHistoryId(string pointHistoryId)
        {
            SendKeys(filterInputById, pointHistoryId);
            ClickOnElement(titleDetail);
            WaitUtil.WaitForPageLoaded();
            return this;
        }

        //ACTIVE SERVICES TAB
        [AllureStep]
        public PointAddressDetailPage ClickOnActiveServicesTab()
        {
            ClickOnElement(activeServiceTab);
            return this;
        }

        [AllureStep]
        public PointAddressDetailPage VerifyPointAddress(List<string> assetTypes)
        {
            var serviceRows = GetAllElements(ServiceRows);
            for (int i = 0; i < serviceRows.Count; i++)
            {
                string[] serviceAssetTypes = serviceRows[i].FindElements(By.XPath("./div[@data-bind='text: $data']")).Select(x => x.Text).ToArray();
                if (serviceAssetTypes.Length == 6)
                {
                    foreach (var assetType in assetTypes)
                    {
                        Assert.IsTrue(serviceAssetTypes.Any(x => x.Contains(assetType)));
                    }
                }
            }
            return this;
        }

        [AllureStep]
        public List<ActiveSeviceModel> GetAllServiceWithoutServiceUnitModel(List<ActiveSeviceModel> GetAllServiceInTab)
        {
            List<ActiveSeviceModel> serviceModels = new List<ActiveSeviceModel>();
            foreach (ActiveSeviceModel activeSeviceModel in GetAllServiceInTab)
            {
                if(activeSeviceModel.serviceUnit.Equals("No Service Unit"))
                {
                    serviceModels.Add(activeSeviceModel);
                }
            }
            return serviceModels;
        }
        [AllureStep]
        public ActiveSeviceModel GetActiveServiceWithSkipService(List<ActiveSeviceModel> allActiveServicesInServiceTab)
        {
            return allActiveServicesInServiceTab.FirstOrDefault(x => x.service.Equals("Skips"));
        }

        [AllureStep]
        public ActiveSeviceModel GetFirstActiveServiceModel()
        {
            string eventLocator = string.Format(eventDynamicLocator, "1");
            string serviceUnitValue = GetElementText(serviceUnitDynamic, "1");
            string serviceValue = GetElementText(serviceWithServiceUnitDynamic, "1");
            return new ActiveSeviceModel(eventLocator, serviceUnitValue, serviceValue);
        }
        [AllureStep]
        public PointAddressDetailPage ClickFirstEventInFirstServiceRow()
        {
            ClickOnElement(eventDynamicLocator, "1");
            return this;
        }
        [AllureStep]

        public PointAddressDetailPage ClickAnyEventInActiveServiceRow(string eventLocator)
        {
            ClickOnElement(eventLocator);
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage VerifyEventTypeWhenClickEventBtn(List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId)
        {
            foreach(CommonServiceForPointDBModel common in FilterCommonServiceForPointWithServiceId)
            {
                Assert.IsTrue(IsControlDisplayed(eventOptions, common.prefix + " - " + common.eventtype));
            }
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
        public EventDetailPage ClickAnyEventOption(string eventName)
        {
            ClickOnElement(eventOptions, eventName);
            return PageFactoryManager.Get<EventDetailPage>();
        }
        [AllureStep]
        public PointAddressDetailPage VerifyDataInActiveServiceWithSp(List<ActiveSeviceModel> allActiveServices, List<ServiceForPointDBModel> serviceForPoint)
        {
            for(int i = 0; i < allActiveServices.Count; i++)
            {
                Assert.AreEqual(serviceForPoint[i].serviceunit, allActiveServices[i].serviceUnit);
                Assert.AreEqual(serviceForPoint[i].service, allActiveServices[i].service);
            }
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage VerifyDetailsInDetailsTab(string _propertyName, string _property, string _toProperty, string _pointSegment, string _pointAddType)
        {
            Assert.True(GetPointAddressName().Contains(_propertyName));
            Assert.True(GetPointAddressName().Contains(_property + "-" + _toProperty));
            Assert.True(GetPointAddressName().Replace(",","").Contains(_pointSegment.Replace(",","")));
            Assert.AreEqual(_pointAddType, GetFirstSelectedItemInDropdown(pointAddressTypeSelect));
            return this;
        }

        //Click on the [All Services] tab
        [AllureStep]
        public PointAddressDetailPage ClickOnAllServicesTab()
        {
            ClickOnElement(allAservicesTab);
            return this;
        }

        //Click on any [Action]
        [AllureStep]
        public PointAddressDetailPage ClickOnAnyActionBtn(int index)
        {
            ClickOnElement(actionBtnAtRow, index.ToString());
            return this;
        }

        //Click on any [Add Service Unit] btn
        [AllureStep]
        public PointAddressDetailPage ClickOnAnyAddServiceUnitBtn(int index)
        {
            ClickOnElement(addServiceUnitBtnAtRow, index.ToString());
            return this;
        }

        //Click on any [Find Service Unit] btn
        [AllureStep]
        public PointAddressDetailPage ClickOnAnyFindServiceUnitBtn(int index)
        {
            ClickOnElement(findServiceUnitBtnAtRow, index.ToString());
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage VerifyServiceRowAfterRefreshing(string atRow, string serviceUnitAdded, string taskCountExp, string scheduleCountExp, string statusExp)
        {
            Assert.AreEqual(GetElementText(serviceUnitAtRow, atRow), serviceUnitAdded);
            Assert.AreEqual(GetElementText(taskCountAtRow, atRow), taskCountExp);
            Assert.AreEqual(GetElementText(scheduleCountAtRow, atRow), scheduleCountExp);
            Assert.AreEqual(GetElementText(statusActiveAtRow, atRow), statusExp);
            return this;
        
        }
        [AllureStep]
        public PointAddressDetailPage ClickServiceUnit(int index)
        {
            ClickOnElement(serviceUnitAtRow, index.ToString());
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnServiceUnitWithServiceName(string serviceName, string serviceUnitName)
        {
            ClickOnElement(string.Format(serviceUnitAtRowWithServiceName, serviceName, serviceUnitName));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnServiceUnitWithServiceNameAndActive(string serviceName, string serviceUnitName)
        {
            ClickOnElement(string.Format(serviceUnitAtRowWithServiceNameAndActive, serviceName, serviceUnitName));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnServiceUnitWithServiceNameAndNonActive(string serviceName, string serviceUnitName)
        {
            ClickOnElement(string.Format(serviceUnitAtRowWithServiceNameAndNoneStatus, serviceName, serviceUnitName));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsDetailTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(detailTab, "class"));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnDataTab()
        {
            ClickOnElement(dataTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsDataTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(dataTab, "class"));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnAnnouncementTab()
        {
            ClickOnElement(announcementsTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsAnnouncementTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(announcementsTab, "class"));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnMapTab()
        {
            ClickOnElement(mapTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsMapTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(mapTab, "class"));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnRisksTab()
        {
            ClickOnElement(risksTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsRisksTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(risksTab, "class"));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnSectorsTab()
        {
            ClickOnElement(sectorsTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsSectorTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(sectorsTab, "class"));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnSubscriptionsTab()
        {
            ClickOnElement(SubscriptionTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsSubscriptionTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(SubscriptionTab, "class"));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage ClickOnNotificationsTab()
        {
            ClickOnElement(notificationTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsNotificationsTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(notificationTab, "class"));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsAllServicesTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(allAservicesTab, "class"));
            return this;
        }
        [AllureStep]
        public PointAddressDetailPage IsActiveServicesTabActive()
        {
            Assert.AreEqual("active", GetAttributeValue(activeServiceTab, "class"));
            return this;
        }
    }
}
