using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels.GetAllServicesForPoint2;
using si_automated_tests.Source.Main.DBModels.GetServiceInfoForPoint;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Services;
using si_automated_tests.Source.Main.Pages.Events;
using static si_automated_tests.Source.Main.Models.ActiveSeviceModel;

namespace si_automated_tests.Source.Main.Pages.Search.PointSegment
{
    public class PointSegmentDetailPage : BasePage
    {
        private readonly By titleDetail = By.XPath("//h4[text()='Point Segment']");
        private readonly By inspectBtn = By.CssSelector("button[title='Inspect']");
        private readonly By segmentName = By.XPath("//p[@class='object-name']");
        private readonly By allAservicesTab = By.CssSelector("a[aria-controls='allServices-tab']");

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
        private readonly By pointHistoryTab = By.CssSelector("a[aria-controls='pointHistory-tab']");
        private readonly By allRowInPointHistoryTabel = By.XPath("//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div");
        private const string columnInRowPointHistoryTab = "//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private readonly By filterInputById = By.XPath("//div[@id='pointHistory-tab']//div[contains(@class, 'l2 r2')]/descendant::input");

        //ALL SERVICES TAB
        private readonly By activeServicesTab = By.CssSelector("a[aria-controls='activeServices-tab']");
        private readonly By serviceWithoutServiceUnit = By.XPath("//div[@class='parent-row']//span[@title='0']");

        private readonly By allActiveServiceParentRow = By.CssSelector("div.parent-row");
        private readonly By serviceUnitParent = By.XPath("//div[@class='parent-row']//div[@title='Open Service Unit']");
        private readonly By serviceParent = By.XPath("//div[@class='parent-row']//span[@title='Open Service Task']");
        private readonly By scheduleParent = By.XPath("//div[@data-bind=\"template: { name: 'service-grid-schedule' }\"]");
        private readonly By lastParent = By.CssSelector("div.parent-row span[data-bind='text: ew.formatDateForUser($data.lastDate)']");
        private readonly By nextPrent = By.CssSelector("div.parent-row span[data-bind='text: ew.formatDateForUser($data.nextDate)']");
        private readonly By assetTypeParent = By.CssSelector("div.parent-row div[data-bind='foreach: $data.asset']");
        private readonly By allocationParent = By.XPath("//div[@class='parent-row']//span[contains(@data-bind, 'text: $parents[0].getParentAllocationText($data)')]");
        //CHRILD
        private readonly By scheduleChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//div[@data-bind='text: $data']");
        private readonly By lastChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//span[@data-bind='text: ew.formatDateForUser($data.lastDate)']");
        private readonly By nextChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//div[@data-bind='text: $data.next']");
        private readonly By allocationChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//span[contains(@data-bind,'text: $data.allocation')]");

        //DYNAMIC LOCATOR
        private const string inspectionTypeOption = "//div[@id='inspection-modal']//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()=' Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//div[@id='inspection-modal']//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";
        private const string eventDynamicLocator = "//div[@class='parent-row'][{0}]//div[text()='Event']";
        private const string eventOptions = "//div[@id='create-event-dropdown']//li[text()='{0}']";

        //ACTION
        private const string actionBtnAtRow = "//tr[{0}]//label[@id='btndropdown']";
        private const string addServiceUnitBtnAtRow = "//tr[{0}]//button[contains(string(), 'Add Service Unit')]";
        private const string findServiceUnitBtnAtRow = "//tr[{0}]//button[contains(string(), 'Find Service Unit')]";
        private const string serviceUnitAtRow = "//tr[{0}]//a[@title='Open Service Unit']";
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

        //Get all active service with service unit
        public List<ActiveSeviceModel> GetAllActiveServiceInTab32839()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allRow = GetAllElements(allActiveServiceParentRow);
            for (int i = 0; i < allRow.Count; i++)
            {
                string serviceUnitValue = GetElementText(GetAllElements(serviceUnitParent)[i]);
                string serviceValue = GetElementText(GetAllElements(serviceParent)[i]);
                string scheduleValue = GetElementText(GetAllElements(scheduleParent)[i]);
                string lastValue = GetElementText(GetAllElements(lastParent)[i]);
                string nextValue = GetElementText(GetAllElements(nextPrent)[i]);
                string assetTypeValue = GetElementText(GetAllElements(assetTypeParent)[i]);
                string allocationValue = GetElementText(GetAllElements(allocationParent)[i]);
                List<ChildSchedule> listSchedule = new List<ChildSchedule>();
                if (i == 1)
                {
                    string scheludeChild = GetElementText(scheduleChildRow);
                    string lastChild = GetElementText(lastChildRow);
                    string nextChild = GetElementText(nextChildRow);
                    string allocationChild = GetElementText(allocationChildRow);
                    listSchedule.Add(new ChildSchedule(scheludeChild, lastChild, nextChild, allocationChild));
                }
                activeSeviceModels.Add(new ActiveSeviceModel(serviceUnitValue, serviceValue, scheduleValue, lastValue, nextValue, assetTypeValue, allocationValue, listSchedule));
            }
            return activeSeviceModels;
        }

        public PointSegmentDetailPage WaitForPointSegmentDetailPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(titleDetail);
            return this;
        }

        public PointSegmentDetailPage ClickInspectBtn()
        {
            ClickOnElement(inspectBtn);
            return this;
        }

        public string GetPointSegmentName()
        {
            return GetElementText(segmentName);
        }

        public PointSegmentDetailPage VerifyPointSegmentId(string idExpected)
        {
            string idActual = GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/point-segments/", "");
            Assert.AreEqual(idExpected, idActual);
            return this;
        }

        //ACTIVE SERVICE TAB
        public PointSegmentDetailPage ClickOnActiveServiceTab()
        {
            ClickOnElement(activeServicesTab);
            return this;
        }

        public List<ActiveSeviceModel> GetAllActiveServiceWithoutServiceUnit()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allRow = GetAllElements(allActiveServiceParentRow);
            for (int i = 0; i < allRow.Count; i++)
            {
                string serviceUnitValue = GetElementText(GetAllElements(serviceUnitParent)[i]);
                string serviceValue = GetElementText(GetAllElements(serviceWithoutServiceUnit)[i]);
                activeSeviceModels.Add(new ActiveSeviceModel(serviceUnitValue, serviceValue));
            }
            return activeSeviceModels;
        }

        public PointSegmentDetailPage VerifyActiveServiceDisplayedWithDB(List<ActiveSeviceModel> activeSeviceModelsDisplayed, List<ServiceForPointDBModel> serviceForPointDB)
        {
            for(int i = 0; i < activeSeviceModelsDisplayed.Count; i++)
            {
                //Service
                Assert.AreEqual(serviceForPointDB[i].service, activeSeviceModelsDisplayed[i].service);
                //Service Unit
                Assert.AreEqual(serviceForPointDB[i].serviceunit, activeSeviceModelsDisplayed[i].serviceUnit);
                //Next parent
                if(serviceForPointDB[i].next.Equals("Tomorrow"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), activeSeviceModelsDisplayed[i].nextService);
                } else
                {
                    Assert.AreEqual(serviceForPointDB[i].next.Replace("-", "/"), activeSeviceModelsDisplayed[i].nextService);
                }
                //Last parent
                if (serviceForPointDB[i].last.Equals("Today"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), activeSeviceModelsDisplayed[i].lastService);
                }
                else
                {
                    Assert.AreEqual(serviceForPointDB[i].last.Replace("-", "/"), activeSeviceModelsDisplayed[i].lastService);
                }
                if (i == 1)
                {
                    //Allocation child
                    Assert.AreEqual(serviceForPointDB[i].roundgroup + " - " + serviceForPointDB[i].round.Trim(), activeSeviceModelsDisplayed[1].listChildSchedule[0].allocationRound);
                    //Schedule child
                    if (serviceForPointDB[i].patterndesc.Contains("every week"))
                    {
                        Assert.AreEqual("Every " + serviceForPointDB[i].patterndesc.Replace("every week", "").Trim(), activeSeviceModelsDisplayed[1].listChildSchedule[0].scheduleRound);
                    }
                    else
                    {
                        Assert.AreEqual(serviceForPointDB[i].patterndesc, activeSeviceModelsDisplayed[i].listChildSchedule[0].scheduleRound);
                    }
                    //Last child
                    if (serviceForPointDB[i].last.Equals("Today"))
                    {
                        Assert.AreEqual(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), activeSeviceModelsDisplayed[i].listChildSchedule[0].lastRound);
                    }
                    else
                    {
                        Assert.AreEqual(serviceForPointDB[i].last.Replace("-", "/"), activeSeviceModelsDisplayed[i].listChildSchedule[0].lastRound);
                    }
                    //Next child
                    //if (serviceForPointDB[i].next.Equals("Tomorrow"))
                    //{
                    //    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), activeSeviceModelsDisplayed[i].nextService);
                    //}
                    //else
                    //{
                    //    Assert.AreEqual(serviceForPointDB[i].next.Replace("-", "/"), activeSeviceModelsDisplayed[i].nextService);
                    //}

                    //Schedule parent
                    Assert.AreEqual("Multiple", activeSeviceModelsDisplayed[1].schedule);
                }
                else
                {
                    //Allocation parent
                    Assert.AreEqual(serviceForPointDB[i].roundgroup + " - " + serviceForPointDB[i].round.Trim(), activeSeviceModelsDisplayed[i].allocationService);
                    if (serviceForPointDB[i].patterndesc.Contains("every week"))
                    {
                        Assert.AreEqual("Every " + serviceForPointDB[i].patterndesc.Replace("every week", "").Trim(), activeSeviceModelsDisplayed[i].schedule);
                    }
                    else
                    {
                        Assert.AreEqual(serviceForPointDB[i].patterndesc, activeSeviceModelsDisplayed[i].schedule);
                    }
                }
                
            }
            return this;
        }


        public List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId(List<CommonServiceForPointDBModel> commonService, int serviceIdExpected)
        {
            return commonService.FindAll(x => x.serviceID == serviceIdExpected);
        }

        public PointSegmentDetailPage VerifyEventTypeWhenClickEventBtn(List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId)
        {
            foreach (CommonServiceForPointDBModel common in FilterCommonServiceForPointWithServiceId)
            {
                Assert.IsTrue(IsControlDisplayed(eventOptions, common.prefix + " - " + common.eventtype));
            }
            return this;
        }

        public PointSegmentDetailPage ClickFirstEventInFirstServiceRow()
        {
            ClickOnElement(eventDynamicLocator, "1");
            return this;
        }

        public EventDetailPage ClickAnyEventOption(string eventName)
        {
            ClickOnElement(eventOptions, eventName);
            return PageFactoryManager.Get<EventDetailPage>();
        }

        public PointSegmentDetailPage VerifyActiveServiceWithoutServiceUnitDisplayedWithDB(List<ActiveSeviceModel> activeSeviceWithoutServiceUnitModelsDisplayed, List<ServiceForPointDBModel> serviceForPointDB)
        {
            for (int i = 0; i < activeSeviceWithoutServiceUnitModelsDisplayed.Count; i++)
            {
                Assert.AreEqual(activeSeviceWithoutServiceUnitModelsDisplayed[i].serviceUnit, serviceForPointDB[i].serviceunit);
                Assert.AreEqual(activeSeviceWithoutServiceUnitModelsDisplayed[i].service, serviceForPointDB[i].service);
            }
            return this;
        }

                //INSPECTION MODEL
        public PointSegmentDetailPage IsCreateInspectionPopup()
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

        public PointSegmentDetailPage VerifyDefaulValue()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(inspectionTypeDd), "Select... ...");
            Assert.IsTrue(GetFirstSelectedItemInDropdown(allocatedUnitDd).Contains("Select..."));
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), "Select... ...");
            //Date
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }

        public PointSegmentDetailPage VerifyDefaultSourceDd(string sourceValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceValue);
            return this;
        }

        public PointSegmentDetailPage ClickAndSelectInspectionType(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }

        public PointSegmentDetailPage ClickAndSelectAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }

        public PointSegmentDetailPage ClickAndSelectAssignedUser(string assignedUserValue)
        {
            ClickOnElement(assignedUserDd);
            ClickOnElement(assignedUserOption, assignedUserValue);
            return this;
        }

        public PointSegmentDetailPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }

        public PointSegmentDetailPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }

        public PointSegmentDetailPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }

        public PointSegmentDetailPage ClickOnInspectionCreatedLink()
        {
            ClickOnElement("//a[@id='echo-notify-success-link']");
            return this;
        }

        //POINT HISTORY TAB
        public PointSegmentDetailPage ClickPointHistoryTab()
        {
            ClickOnElement(pointHistoryTab);
            return this;
        }

        public List<PointHistoryModel> GetAllPointHistory()
        {
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
                    string dueDate = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[6])[i]);
                    string state = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[7])[i]);
                    string resolution = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[8])[i]);
                    allModel.Add(new PointHistoryModel(desc, ID, type, service, address, date, dueDate, state, resolution));
                }
                return allModel;
            }
            return allModel;
            
        }

        public PointSegmentDetailPage VerifyPointHistory(PointHistoryModel pointHistoryModelActual, string desc, string id, string type, string address, string date, string dueDate, string state)
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

        public PointSegmentDetailPage FilterByPointHistoryId(string pointHistoryId)
        {
            SendKeys(filterInputById, pointHistoryId);
            ClickOnElement(titleDetail);
            WaitUtil.WaitForPageLoaded();
            return this;
        }

        //Click on the [All Services] tab
        public PointSegmentDetailPage ClickOnAllServicesTab()
        {
            ClickOnElement(allAservicesTab);
            return this;
        }

        //Click on any [Action]
        public PointSegmentDetailPage ClickOnAnyActionBtn(int index)
        {
            ClickOnElement(actionBtnAtRow, index.ToString());
            return this;
        }

        //Click on any [Add Service Unit] btn
        public PointSegmentDetailPage ClickOnAnyAddServiceUnitBtn(int index)
        {
            ClickOnElement(addServiceUnitBtnAtRow, index.ToString());
            return this;
        }

        //Click on any [Find Service Unit] btn
        public PointSegmentDetailPage ClickOnAnyFindServiceUnitBtn(int index)
        {
            ClickOnElement(findServiceUnitBtnAtRow, index.ToString());
            return this;
        }

        public PointSegmentDetailPage VerifyServiceRowAfterRefreshing(string atRow, string serviceUnitAdded, string taskCountExp, string scheduleCountExp, string statusExp)
        {
            Assert.AreEqual(GetElementText(serviceUnitAtRow, atRow), serviceUnitAdded);
            Assert.AreEqual(GetElementText(taskCountAtRow, atRow), taskCountExp);
            Assert.AreEqual(GetElementText(scheduleCountAtRow, atRow), scheduleCountExp);
            Assert.AreEqual(GetElementText(statusActiveAtRow, atRow), statusExp);
            return this;

        }

        public List<AllServiceInPointAddressModel> GetAllServicesInAllServicesTab()
        {
            WaitUtil.WaitForAllElementsPresent(totalServicesRows);
            List<AllServiceInPointAddressModel> result = new List<AllServiceInPointAddressModel>();
            List<IWebElement> allRows = GetAllElements(totalServicesRows);
            for (int i = 0; i < allRows.Count; i++)
            {
                string contract = GetElementText(GetAllElements(allContractRows)[i]);
                string service = GetElementText(GetAllElements(allServiceRows)[i]);
                string serviceUnit = GetElementText(GetAllElements(allServiceUnitRows)[i]);
                string taskCount = GetElementText(GetAllElements(allTaskCountRows)[i]);
                string scheduleCount = GetElementText(GetAllElements(allScheduledCountRows)[i]);
                string status = GetElementText(GetAllElements(allStatusRows)[i]);
                string serviceUnitLinkToDetail = "";
                if (IsControlDisplayedNotThrowEx(string.Format(serviceUnitLink, (i + 1).ToString())))
                {
                    serviceUnitLinkToDetail = string.Format(serviceUnitLink, (i + 1).ToString());
                }
                result.Add(new AllServiceInPointAddressModel(contract, service, serviceUnit, taskCount, scheduleCount, status, serviceUnitLinkToDetail));
            }
            return result;
        }

        public ServiceUnitDetailPage ClickServiceUnitLinkAdded(string locatorToDetail)
        {
            ClickOnElement(locatorToDetail);
            return PageFactoryManager.Get<ServiceUnitDetailPage>();
        }

        public PointSegmentDetailPage VerifyDBWithUI(List<AllServiceInPointAddressModel> allServiceInPointSegments, List<ServiceForPoint2DBModel> serviceForPoint2DBModels)
        {
            for (int i = 0; i < allServiceInPointSegments.Count; i++)
            {
                Assert.AreEqual(serviceForPoint2DBModels[i].Contract, allServiceInPointSegments[i].contract, "Wrong Contract");
                Assert.AreEqual(serviceForPoint2DBModels[i].Service, allServiceInPointSegments[i].service, "Wrong Service");
                if (serviceForPoint2DBModels[i].ServiceUnit == null)
                {
                    Assert.AreEqual("", allServiceInPointSegments[i].serviceUnit, "Wrong Service Unit");
                }
                else
                {
                    Assert.AreEqual(serviceForPoint2DBModels[i].ServiceUnit, allServiceInPointSegments[i].serviceUnit, "Wrong Service Unit");
                }
                Assert.AreEqual(serviceForPoint2DBModels[i].STCount.ToString(), allServiceInPointSegments[i].taskCount, "Wrong task count");
                Assert.AreEqual(serviceForPoint2DBModels[i].STSCount.ToString(), allServiceInPointSegments[i].scheduleCount, "Wrong schedule count");
                if (serviceForPoint2DBModels[i].ActiveState == 0)
                {
                    Assert.AreEqual("", allServiceInPointSegments[i].status, "Wrong task count");
                }
                else
                {
                    Assert.AreEqual("Active", allServiceInPointSegments[i].status, "Wrong state");
                }
            }

            return this;
        }
    }
}
