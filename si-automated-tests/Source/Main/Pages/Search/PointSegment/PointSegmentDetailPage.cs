using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels.GetServiceInfoForPoint;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Events;

namespace si_automated_tests.Source.Main.Pages.Search.PointSegment
{
    public class PointSegmentDetailPage : BasePage
    {
        private readonly By titleDetail = By.XPath("//h4[text()='Point Segment']");
        private readonly By inspectBtn = By.CssSelector("button[title='Inspect']");
        private readonly By segmentName = By.XPath("//p[@class='object-name']");

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
        private readonly By allActiveServiceRow = By.CssSelector("div.parent-row");
        private readonly By serviceUnit = By.XPath("//div[@class='parent-row']//div[@title='Open Service Unit']");
        private readonly By serviceWithoutServiceUnit = By.XPath("//div[@class='parent-row']//span[@title='0']");
        private readonly By service = By.XPath("//div[@class='parent-row']//span[@title='Open Service Task']");
        private readonly By schedule = By.CssSelector("div.parent-row div[data-bind='text: $data']");
        private readonly By last = By.CssSelector("div.parent-row span[data-bind='text: ew.formatDateForUser($data.lastDate)']");
        private readonly By next = By.CssSelector("div.parent-row span[data-bind='text: ew.formatDateForUser($data.nextDate)']");
        private readonly By assetType = By.CssSelector("div.parent-row div[data-bind='foreach: $data.asset']");
        private readonly By allocation = By.XPath("//div[@class='parent-row']//span[contains(@data-bind, 'text: $parents[0].getParentAllocationText($data)')]");
        private const string eventDynamicLocator = "//div[@class='parent-row'][{0}]//div[text()='Event']";
        private const string eventOptions = "//div[@id='create-event-dropdown']//li[text()='{0}']";

        //DYNAMIC LOCATOR
        private const string inspectionTypeOption = "//div[@id='inspection-modal']//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()=' Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//div[@id='inspection-modal']//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";

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

        public List<ActiveSeviceModel> GetAllActiveServiceInTab()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allRow = GetAllElements(allActiveServiceRow);
            for(int i = 0; i < allRow.Count; i++)
            {
                string serviceUnitValue = GetElementText(GetAllElements(serviceUnit)[i]);
                string serviceValue = GetElementText(GetAllElements(service)[i]);
                string scheduleValue = GetElementText(GetAllElements(schedule)[i]);
                string lastValue = GetElementText(GetAllElements(last)[i]);
                string nextValue = GetElementText(GetAllElements(next)[i]);
                string assetTypeValue = GetElementText(GetAllElements(assetType)[i]);
                string allocationValue = GetElementText(GetAllElements(allocation)[i]);
                activeSeviceModels.Add(new ActiveSeviceModel(serviceUnitValue, serviceValue, scheduleValue, lastValue, nextValue, assetTypeValue, allocationValue));
            }
            return activeSeviceModels;
        }

        public List<ActiveSeviceModel> GetAllActiveServiceWithoutServiceUnit()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allRow = GetAllElements(allActiveServiceRow);
            for (int i = 0; i < allRow.Count; i++)
            {
                string serviceUnitValue = GetElementText(GetAllElements(serviceUnit)[i]);
                string serviceValue = GetElementText(GetAllElements(serviceWithoutServiceUnit)[i]);
                activeSeviceModels.Add(new ActiveSeviceModel(serviceUnitValue, serviceValue));
            }
            return activeSeviceModels;
        }

        public PointSegmentDetailPage VerifyActiveServiceDisplayedWithDB(List<ActiveSeviceModel> activeSeviceModelsDisplayed, List<ServiceForPointDBModel> serviceForPointDB)
        {
            for(int i = 0; i < activeSeviceModelsDisplayed.Count; i++)
            {
                Assert.AreEqual(serviceForPointDB[i].service, activeSeviceModelsDisplayed[i].service);
                Assert.AreEqual(serviceForPointDB[i].serviceunit, activeSeviceModelsDisplayed[i].serviceUnit);
                if (serviceForPointDB[i].patterndesc.Contains("every week"))
                {
                    Assert.AreEqual("Every " + serviceForPointDB[i].patterndesc.Replace("every week", "").Trim(), activeSeviceModelsDisplayed[i].schedule);
                } else {
                    Assert.AreEqual(serviceForPointDB[i].patterndesc, activeSeviceModelsDisplayed[i].schedule);
                }
                if(serviceForPointDB[i].next.Equals("Tomorrow"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), activeSeviceModelsDisplayed[i].nextService);
                } else
                {
                    Assert.AreEqual(serviceForPointDB[i].next.Replace("-", "/"), activeSeviceModelsDisplayed[i].nextService);
                }
                if(serviceForPointDB[i].last.Equals("Today"))
                {
                    Assert.AreEqual(CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), activeSeviceModelsDisplayed[i].lastService);
                } else
                {
                    Assert.AreEqual(serviceForPointDB[i].last.Replace("-", "/"), activeSeviceModelsDisplayed[i].lastService);
                }
                Assert.AreEqual(serviceForPointDB[i].roundgroup + " - " + serviceForPointDB[i].round.Trim(), activeSeviceModelsDisplayed[i].allocationService);
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
    }
}
