using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
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

namespace si_automated_tests.Source.Main.Pages.Search.PointNodes
{
    public class PointNodeDetailPage : BasePage
    {
        private readonly By titleDetail = By.XPath("//h4[text()='Point Node']");
        private readonly By inspectBtn = By.CssSelector("button[title='Inspect']");
        private readonly By pointNodeName = By.XPath("//p[@class='object-name']");
        private readonly By allAservicesTab = By.CssSelector("a[aria-controls='allServices-tab']");

        //DETAIL TAB
        private readonly By description = By.Id("description");
        private readonly By latitude = By.Id("latitude");
        private readonly By longitude = By.Id("longitude");

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

        //ACTIVE SERVICES TAB
        private readonly By activeServiceTab = By.CssSelector("a[aria-controls='activeServices-tab']");
        private readonly By allActiveServiceRow = By.CssSelector("div.parent-row");
        private readonly By serviceUnit = By.XPath("//div[@class='parent-row']//div[@title='Open Service Unit']");
        private readonly By service = By.XPath("//div[@class='parent-row']//span[@title='0' or @title='Open Service Task']");
        private readonly By schedule = By.XPath("//div[@data-bind=\"template: { name: 'service-grid-schedule' }\"]");
        private readonly By last = By.CssSelector("div.parent-row span[data-bind='text: ew.formatDateForUser($data.lastDate)']");
        private readonly By next = By.CssSelector("div.parent-row span[data-bind='text: ew.formatDateForUser($data.nextDate)']");
        private readonly By assetType = By.CssSelector("div.parent-row div[data-bind='foreach: $data.asset']");
        private readonly By allocation = By.XPath("//div[@class='parent-row']//span[contains(@data-bind, 'text: $parents[0].getParentAllocationText($data)')]");
        private const string eventDynamicLocator = "//div[@class='parent-row'][{0}]//div[text()='Event']";
        private const string eventOptions = "//div[@id='create-event-dropdown']//li[text()='{0}']";

        //CHRILD
        private readonly By scheduleChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//div[@data-bind='text: $data']");
        private readonly By lastChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//span[@data-bind='text: ew.formatDateForUser($data.lastDate)']");
        private readonly By nextChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//div[@data-bind='text: $data.next']");
        private readonly By allocationChildRow = By.XPath("//div[@class='services-grid--root']/div[@class='services-grid--row']//div[@class='child-row' and not(contains(@style, 'display: none'))]//span[contains(@data-bind,'text: $data.allocation')]");

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

        [AllureStep]
        public PointNodeDetailPage ClickOnActiveServicesTab()
        {
            ClickOnElement(activeServiceTab);
            return this;
        }
        [AllureStep]
        public string GetPointNodeName()
        {
            return GetElementText(pointNodeName);
        }
        [AllureStep]
        public List<ActiveSeviceModel> GetAllServiceWithServiceUnitModel()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allRow = GetAllElements(allActiveServiceRow);
            for (int i = 0; i < allRow.Count; i++)
            {
                string serviceUnitValue = GetElementText(GetAllElements(serviceUnit)[i]);
                string serviceValue = GetElementText(GetAllElements(service)[i]);
                string scheduleValue = GetElementText(GetAllElements(schedule)[i]);
                string lastValue = GetElementText(GetAllElements(last)[i]);
                string nextValue = GetElementText(GetAllElements(next)[i]);
                string assetTypeValue = GetElementText(GetAllElements(assetType)[i]);
                string allocationValue = GetElementText(GetAllElements(allocation)[i]);
                List<ChildSchedule> listSchedule = new List<ChildSchedule>();
                string scheludeChild = GetElementText(scheduleChildRow);
                string lastChild = GetElementText(lastChildRow);
                string nextChild = GetElementText(nextChildRow);
                string allocationChild = GetElementText(allocationChildRow);
                listSchedule.Add(new ChildSchedule(scheludeChild, lastChild, nextChild, allocationChild));
                activeSeviceModels.Add(new ActiveSeviceModel(serviceUnitValue, serviceValue, scheduleValue, lastValue, nextValue, assetTypeValue, allocationValue, listSchedule));
            }
            return activeSeviceModels;
        }
        [AllureStep]
        public PointNodeDetailPage VerifyActiveServiceDisplayedWithDB(List<ActiveSeviceModel> activeSeviceModelsDisplayed, List<ServiceForPointDBModel> serviceForPointDB, List<ServiceTaskForPointDBModel> serviceTaskForPointDBModels)
        {
            for (int i = 0; i < activeSeviceModelsDisplayed.Count; i++)
            {
                Assert.AreEqual(serviceForPointDB[i].service, activeSeviceModelsDisplayed[i].service);
                Assert.AreEqual(serviceForPointDB[i].serviceunit, activeSeviceModelsDisplayed[i].serviceUnit);
                //Parent - Schedule
                Assert.AreEqual("Multiple", activeSeviceModelsDisplayed[i].schedule);
                if (serviceForPointDB[i].next.Equals("Tomorrow"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), activeSeviceModelsDisplayed[i].nextService);
                }
                else
                {
                    Assert.AreEqual(serviceForPointDB[i].next.Replace("-", "/"), activeSeviceModelsDisplayed[i].nextService);
                }
                if (serviceForPointDB[i].last.Equals("Today"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), activeSeviceModelsDisplayed[i].lastService);
                }
                else
                {
                    Assert.AreEqual(serviceForPointDB[i].last.Replace("-", "/"), activeSeviceModelsDisplayed[i].lastService);
                }
                Assert.AreEqual(serviceForPointDB[i].assets, activeSeviceModelsDisplayed[i].assetTypeService);

                List<ServiceTaskForPointDBModel> allSTForPointWithSameAssetType = GetServiceTaskForPointWithSameAssetType(serviceTaskForPointDBModels, serviceForPointDB[i].assets);

                //allocation - Child
                Assert.AreEqual(allSTForPointWithSameAssetType[0].roundgroup + " - " + allSTForPointWithSameAssetType[0].round.Trim(), activeSeviceModelsDisplayed[i].listChildSchedule[0].allocationRound);
                //CHILD - ROW
                Assert.AreEqual(serviceForPointDB[i].roundgroup + " - " + serviceForPointDB[i].round.Trim(), activeSeviceModelsDisplayed[i].listChildSchedule[0].allocationRound);
                //Schedule child
                Assert.AreEqual(allSTForPointWithSameAssetType[0].patterndesc, activeSeviceModelsDisplayed[i].listChildSchedule[0].scheduleRound);
                //Last child
                Assert.AreEqual(CommonUtil.ParseDateTimeToFormat(allSTForPointWithSameAssetType[0].lastdate, CommonConstants.DATE_DD_MM_YYYY_FORMAT), activeSeviceModelsDisplayed[i].listChildSchedule[0].lastRound);
                //Next child
                Assert.AreEqual(allSTForPointWithSameAssetType[0].next, activeSeviceModelsDisplayed[i].listChildSchedule[0].nextRound);
            }
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage ClickFirstEventInFirstServiceRow()
        {
            ClickOnElement(eventDynamicLocator, "1");
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage VerifyEventTypeWhenClickEventBtn(List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId)
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

        //DB
        [AllureStep]
        public List<ServiceTaskForPointDBModel> GetServiceTaskForPointWithSameAssetType(List<ServiceTaskForPointDBModel> serviceTaskForPoint, string assetType)
        {
            List<ServiceTaskForPointDBModel> result = new List<ServiceTaskForPointDBModel>();
            foreach (ServiceTaskForPointDBModel services in serviceTaskForPoint)
            {
                if (services.assets.Equals(assetType) && services.roundscheduleID != 0)
                {
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

        //DYNAMIC LOCATOR
        private const string inspectionTypeOption = "//div[@id='inspection-modal']//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()=' Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//div[@id='inspection-modal']//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";

        [AllureStep]
        public PointNodeDetailPage WaitForPointNodeDetailDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(titleDetail);
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage ClickInspectBtn()
        {
            ClickOnElement(inspectBtn);
            return this;
        }

        //INSPECTION MODEL
        [AllureStep]
        public PointNodeDetailPage IsCreateInspectionPopup()
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
        public PointNodeDetailPage VerifyDefaulValue()
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
        public PointNodeDetailPage VerifyDefaultSourceDd(string sourceValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceValue);
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage ClickAndSelectInspectionType(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage ClickAndSelectAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage ClickAndSelectAssignedUser(string assignedUserValue)
        {
            ClickOnElement(assignedUserDd);
            ClickOnElement(assignedUserOption, assignedUserValue);
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage ClickOnInspectionCreatedLink()
        {
            ClickOnElement("//a[@id='echo-notify-success-link']");
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage VerifyPointNodeId(string idExpected)
        {
            string idActual = GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/point-nodes/", "");
            Assert.AreEqual(idExpected, idActual);
            return this;
        }

        //POINT HISTORY TAB
        [AllureStep]
        public PointNodeDetailPage ClickPointHistoryTab()
        {
            ClickOnElement(pointHistoryTab);
            return this;
        }
        [AllureStep]
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
            }
            return allModel;

        }
        [AllureStep]
        public PointNodeDetailPage VerifyPointHistory(PointHistoryModel pointHistoryModelActual, string desc, string id, string type, string address, string date, string dueDate, string state)
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
        public PointNodeDetailPage FilterByPointHistoryId(string pointHistoryId)
        {
            SendKeys(filterInputById, pointHistoryId);
            ClickOnElement(titleDetail);
            WaitUtil.WaitForPageLoaded();
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage InputPointNodeDetails(string _des, string _lat, string _long)
        {
            SendKeys(description, _des);
            SendKeys(latitude, _lat);
            SendKeys(longitude, _long);
            return this;
        }

        //Click on the [All Services] tab
        [AllureStep]
        public PointNodeDetailPage ClickOnAllServicesTab()
        {
            ClickOnElement(allAservicesTab);
            return this;
        }

        //Click on any [Action]
        [AllureStep]
        public PointNodeDetailPage ClickOnAnyActionBtn(int index)
        {
            ClickOnElement(actionBtnAtRow, index.ToString());
            return this;
        }

        //Click on any [Add Service Unit] btn
        [AllureStep]
        public PointNodeDetailPage ClickOnAnyAddServiceUnitBtn(int index)
        {
            ClickOnElement(addServiceUnitBtnAtRow, index.ToString());
            return this;
        }

        //Click on any [Find Service Unit] btn
        [AllureStep]
        public PointNodeDetailPage ClickOnAnyFindServiceUnitBtn(int index)
        {
            ClickOnElement(findServiceUnitBtnAtRow, index.ToString());
            return this;
        }
        [AllureStep]
        public PointNodeDetailPage VerifyServiceRowAfterRefreshing(string atRow, string serviceUnitAdded, string taskCountExp, string scheduleCountExp, string statusExp)
        {
            Assert.AreEqual(GetElementText(serviceUnitAtRow, atRow), serviceUnitAdded);
            Assert.AreEqual(GetElementText(taskCountAtRow, atRow), taskCountExp);
            Assert.AreEqual(GetElementText(scheduleCountAtRow, atRow), scheduleCountExp);
            Assert.AreEqual(GetElementText(statusActiveAtRow, atRow), statusExp);
            return this;

        }
        [AllureStep]
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
        [AllureStep]
        public ServiceUnitDetailPage ClickServiceUnitLinkAdded(string locatorToDetail)
        {
            ClickOnElement(locatorToDetail);
            return PageFactoryManager.Get<ServiceUnitDetailPage>();
        }
        [AllureStep]
        public PointNodeDetailPage VerifyDBWithUI(List<AllServiceInPointAddressModel> allServiceInPointAddresses, List<ServiceForPoint2DBModel> serviceForPoint2DBModels)
        {
            for (int i = 0; i < allServiceInPointAddresses.Count; i++)
            {
                Assert.AreEqual(serviceForPoint2DBModels[i].Contract, allServiceInPointAddresses[i].contract, "Wrong Contract");
                Assert.AreEqual(serviceForPoint2DBModels[i].Service, allServiceInPointAddresses[i].service, "Wrong Service");
                if (serviceForPoint2DBModels[i].ServiceUnit == null)
                {
                    Assert.AreEqual("", allServiceInPointAddresses[i].serviceUnit, "Wrong Service Unit");
                }
                else
                {
                    Assert.AreEqual(serviceForPoint2DBModels[i].ServiceUnit, allServiceInPointAddresses[i].serviceUnit, "Wrong Service Unit");
                }
                Assert.AreEqual(serviceForPoint2DBModels[i].STCount.ToString(), allServiceInPointAddresses[i].taskCount, "Wrong task count");
                Assert.AreEqual(serviceForPoint2DBModels[i].STSCount.ToString(), allServiceInPointAddresses[i].scheduleCount, "Wrong schedule count");
                if (serviceForPoint2DBModels[i].ActiveState == 0)
                {
                    Assert.AreEqual("", allServiceInPointAddresses[i].status, "Wrong task count");
                }
                else
                {
                    Assert.AreEqual("Active", allServiceInPointAddresses[i].status, "Wrong state");
                }
            }

            return this;
        }

    }
}
