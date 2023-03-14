using System;
using System.Collections.Generic;
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

namespace si_automated_tests.Source.Main.Pages.Search.PointSegment
{
    public class PointSegmentDetailPage : BasePageCommonActions
    {
        public readonly By SubscriptionTab = By.XPath("//a[@aria-controls='subscriptions-tab']");
        private readonly By titleDetail = By.XPath("//h4[text()='Point Segment']");
        private readonly By inspectBtn = By.CssSelector("button[title='Inspect']");
        private readonly By segmentName = By.XPath("//p[@class='object-name']");
        private readonly By allAservicesTab = By.CssSelector("a[aria-controls='allServices-tab']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");

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
        public PointSegmentDetailPage VerifyNewSubscription(string id, string firstName, string lastName, string mobile, string subjectDescription)
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
        public PointSegmentDetailPage VerifyColumnsDisplay(List<string> columnNames)
        {
            var headerEles = GetAllElements(By.XPath("//div[contains(@class, 'slick-header-columns')]//span[@class='slick-column-name']"));
            foreach (var item in headerEles)
            {
                Assert.IsTrue(columnNames.Contains(item.Text));
            }
            return this;
        }
        #endregion

        #region Risk tab
        public readonly By RiskTab = By.XPath("//a[@aria-controls='risks-tab']");
        public readonly By RiskIframe = By.XPath("//div[@id='risks-tab']//iframe");
        public readonly By BulkCreateButton = By.XPath("//button[@title='Add risk register(s)']");
        private readonly string riskTable = "//div[@id='risk-grid']//div[@class='grid-canvas']";
        private readonly string riskRow = "./div[contains(@class,'slick-row')]";
        private readonly string riskCheckboxCell = "./div[@class='slick-cell l0 r0']//input";
        private readonly string riskNameCell = "./div[@class='slick-cell l2 r2']";
        private readonly string riskStartDateCell = "./div[@class='slick-cell l9 r9']";
        private readonly string riskEndDateCell = "./div[@class='slick-cell l10 r10']";
        public TableElement RiskTableEle
        {
            get => new TableElement(riskTable, riskRow, new List<string>() { riskCheckboxCell, riskNameCell, riskStartDateCell, riskEndDateCell });
        }

        [AllureStep]
        public PointSegmentDetailPage VerifyRiskSelect(string riskName, string startdate, string endDate)
        {
            Assert.IsNotNull(RiskTableEle.GetCellByCellValues(0, new Dictionary<int, object>()
            {
                { RiskTableEle.GetCellIndex(riskNameCell), riskName },
                { RiskTableEle.GetCellIndex(riskStartDateCell), startdate },
                { RiskTableEle.GetCellIndex(riskEndDateCell), endDate },
            }));
            return this;
        }
        #endregion

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
        private readonly By activeServicesTab = By.CssSelector("a[aria-controls='activeServices-tab']");
        private readonly By serviceWithoutServiceUnit = By.XPath("//div[@class='parent-row']//span[@title='0']");
        private readonly By serviceUnitAtFirstRow = By.XPath("//div[@class='parent-row'][1]//div[@title='Open Service Unit']/span");

        private readonly By allActiveServiceParentRow = By.CssSelector("div.parent-row");
        private readonly By serviceUnitParent = By.XPath("//div[@class='parent-row']//div[@title='Open Service Unit']");
        private readonly By serviceParent = By.XPath("//div[@class='parent-row']//span[@title='Open Service Task']");
        private readonly By scheduleParent = By.XPath("//div[@data-bind=\"template: { name: 'service-grid-schedule' }\"]");
        private readonly By lastParent = By.XPath("//div[@id='activeServices-tab']//div[@class='parent-row']//span[@data-bind='text: ew.formatDateForUser($data.lastDate)']");
        private readonly By nextPrent = By.XPath("//div[@id='activeServices-tab']//div[@class='parent-row']//span[@data-bind='text: ew.formatDateForUser($data.nextDate)']");
        private readonly By assetTypeParent = By.XPath("//div[@id='activeServices-tab']//div[@class='parent-row']//div[@data-bind='foreach: $data.asset']");
        private readonly By allocationParent = By.XPath("//div[@id='activeServices-tab']//div[@class='parent-row']//span[contains(@data-bind, 'text: $parents[0].getParentAllocationText($data)')]");
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
        private const string pointSegmentName = "//p[@class='object-name' and text()='{0}']";

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
        [AllureStep]
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
        public PointSegmentDetailPage WaitForPointSegmentDetailPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(titleDetail);
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage WaitForPointSegmentDetailPageDisplayed(string pointSegmentNameValue)
        {
            WaitUtil.WaitForElementVisible(titleDetail);
            WaitUtil.WaitForElementVisible(pointSegmentName, pointSegmentNameValue);
            WaitUtil.WaitForElementVisible(detailTab);
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage ClickInspectBtn()
        {
            ClickOnElement(inspectBtn);
            return this;
        }
        [AllureStep]
        public string GetPointSegmentName()
        {
            return GetElementText(segmentName);
        }
        [AllureStep]
        public PointSegmentDetailPage VerifyPointSegmentId(string idExpected)
        {
            string idActual = GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/point-segments/", "");
            Assert.AreEqual(idExpected, idActual);
            return this;
        }

        #region ACTIVE SERVICE TAB
        [AllureStep]
        public PointSegmentDetailPage ClickOnActiveServiceTab()
        {
            ClickOnElement(activeServicesTab);
            return this;
        }
        [AllureStep]
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
        [AllureStep]
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
                //if (i == 1)
                //{
                //    //Allocation child
                //    Assert.AreEqual(serviceForPointDB[i].roundgroup + " - " + serviceForPointDB[i].round.Trim(), activeSeviceModelsDisplayed[1].listChildSchedule[0].allocationRound);
                //    //Schedule child
                //    if (serviceForPointDB[i].patterndesc.Contains("every week"))
                //    {
                //        Assert.AreEqual("Every " + serviceForPointDB[i].patterndesc.Replace("every week", "").Trim(), activeSeviceModelsDisplayed[1].listChildSchedule[0].scheduleRound);
                //    }
                //    else
                //    {
                //        Assert.AreEqual(serviceForPointDB[i].patterndesc, activeSeviceModelsDisplayed[i].listChildSchedule[0].scheduleRound);
                //    }
                //    //Last child
                //    if (serviceForPointDB[i].last.Equals("Today"))
                //    {
                //        Assert.AreEqual(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), activeSeviceModelsDisplayed[i].listChildSchedule[0].lastRound);
                //    }
                //    else
                //    {
                //        Assert.AreEqual(serviceForPointDB[i].last.Replace("-", "/"), activeSeviceModelsDisplayed[i].listChildSchedule[0].lastRound);
                //    }
                //    //Next child
                //    //if (serviceForPointDB[i].next.Equals("Tomorrow"))
                //    //{
                //    //    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), activeSeviceModelsDisplayed[i].nextService);
                //    //}
                //    //else
                //    //{
                //    //    Assert.AreEqual(serviceForPointDB[i].next.Replace("-", "/"), activeSeviceModelsDisplayed[i].nextService);
                //    //}

                //    //Schedule parent
                //    Assert.AreEqual("Multiple", activeSeviceModelsDisplayed[1].schedule);
                //}
                //else
                //{
                //    //Allocation parent
                //    Assert.AreEqual(serviceForPointDB[i].roundgroup + " - " + serviceForPointDB[i].round.Trim(), activeSeviceModelsDisplayed[i].allocationService);
                //    if (serviceForPointDB[i].patterndesc.Contains("every week"))
                //    {
                //        Assert.AreEqual("Every " + serviceForPointDB[i].patterndesc.Replace("every week", "").Trim(), activeSeviceModelsDisplayed[i].schedule);
                //    }
                //    else
                //    {
                //        Assert.AreEqual(serviceForPointDB[i].patterndesc, activeSeviceModelsDisplayed[i].schedule);
                //    }
                //}

                //Assert.AreEqual(serviceForPointDB[i].roundgroup + " - " + serviceForPointDB[i].round.Trim(), activeSeviceModelsDisplayed[i].allocationService);
                if (serviceForPointDB[i].patterndesc.Contains("every week"))
                {
                    Assert.AreEqual("Every " + serviceForPointDB[i].patterndesc.Replace("every week", "").Trim(), activeSeviceModelsDisplayed[i].schedule);
                }
                else
                {
                    Assert.AreEqual(serviceForPointDB[i].patterndesc, activeSeviceModelsDisplayed[i].schedule);
                }

            }
            return this;
        }

        [AllureStep]
        public List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId(List<CommonServiceForPointDBModel> commonService, int serviceIdExpected)
        {
            return commonService.FindAll(x => x.serviceID == serviceIdExpected);
        }
        [AllureStep]
        public PointSegmentDetailPage VerifyEventTypeWhenClickEventBtn(List<CommonServiceForPointDBModel> FilterCommonServiceForPointWithServiceId)
        {
            foreach (CommonServiceForPointDBModel common in FilterCommonServiceForPointWithServiceId)
            {
                Assert.IsTrue(IsControlDisplayed(eventOptions, common.prefix + " - " + common.eventtype));
            }
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage ClickFirstEventInFirstServiceRow()
        {
            ClickOnElement(eventDynamicLocator, "1");
            return this;
        }
        [AllureStep]
        public EventDetailPage ClickAnyEventOption(string eventName)
        {
            ClickOnElement(eventOptions, eventName);
            return PageFactoryManager.Get<EventDetailPage>();
        }
        [AllureStep]
        public PointSegmentDetailPage VerifyActiveServiceWithoutServiceUnitDisplayedWithDB(List<ActiveSeviceModel> activeSeviceWithoutServiceUnitModelsDisplayed, List<ServiceForPointDBModel> serviceForPointDB)
        {
            for (int i = 0; i < activeSeviceWithoutServiceUnitModelsDisplayed.Count; i++)
            {
                Assert.AreEqual(activeSeviceWithoutServiceUnitModelsDisplayed[i].serviceUnit, serviceForPointDB[i].serviceunit);
                Assert.AreEqual(activeSeviceWithoutServiceUnitModelsDisplayed[i].service, serviceForPointDB[i].service);
            }
            return this;
        }

        [AllureStep]
        public PointSegmentDetailPage ClickOnFirstServiceUnit()
        {
            ClickOnElement(serviceUnitAtFirstRow);
            return this;
        }
        #endregion

        #region INSPECTION MODEL
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
        public PointSegmentDetailPage VerifyDefaultSourceDd(string sourceValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceValue);
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage ClickAndSelectInspectionType(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage ClickAndSelectAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage ClickAndSelectAssignedUser(string assignedUserValue)
        {
            ClickOnElement(assignedUserDd);
            ClickOnElement(assignedUserOption, assignedUserValue);
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage ClickOnInspectionCreatedLink()
        {
            ClickOnElement("//a[@id='echo-notify-success-link']");
            return this;
        }
        #endregion

        //POINT HISTORY TAB
        [AllureStep]
        public PointSegmentDetailPage ClickPointHistoryTab()
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
                return allModel;
            }
            return allModel;
            
        }
        [AllureStep]
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
        [AllureStep]
        public PointSegmentDetailPage FilterByPointHistoryId(string pointHistoryId)
        {
            SendKeys(filterInputById, pointHistoryId);
            ClickOnElement(titleDetail);
            WaitUtil.WaitForPageLoaded();
            return this;
        }

        //Click on the [All Services] tab
        [AllureStep]
        public PointSegmentDetailPage ClickOnAllServicesTab()
        {
            ClickOnElement(allAservicesTab);
            return this;
        }

        //Click on any [Action]
        [AllureStep]
        public PointSegmentDetailPage ClickOnAnyActionBtn(int index)
        {
            ClickOnElement(actionBtnAtRow, index.ToString());
            return this;
        }

        //Click on any [Add Service Unit] btn
        [AllureStep]
        public PointSegmentDetailPage ClickOnAnyAddServiceUnitBtn(int index)
        {
            ClickOnElement(addServiceUnitBtnAtRow, index.ToString());
            return this;
        }

        //Click on any [Find Service Unit] btn
        [AllureStep]
        public PointSegmentDetailPage ClickOnAnyFindServiceUnitBtn(int index)
        {
            ClickOnElement(findServiceUnitBtnAtRow, index.ToString());
            return this;
        }
        [AllureStep]
        public PointSegmentDetailPage VerifyServiceRowAfterRefreshing(string atRow, string serviceUnitAdded, string taskCountExp, string scheduleCountExp, string statusExp)
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

        #region MAP TAB
        private readonly By mapTab = By.CssSelector("a[aria-controls='map-tab']");
        private readonly By segmentDescInMapTab = By.XPath("//td[text()='Segment']/following-sibling::td");

        [AllureStep]
        public PointSegmentDetailPage ClickOnMapTab()
        {
            ClickOnElement(mapTab);
            return this;
        }

        [AllureStep]
        public string GetDescInMapTab()
        {
            return GetElementText(segmentDescInMapTab);
        }

        #endregion
    }
}
