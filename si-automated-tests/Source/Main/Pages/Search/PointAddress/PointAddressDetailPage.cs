using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Events;

namespace si_automated_tests.Source.Main.Pages.PointAddress
{
    public class PointAddressDetailPage : BasePage
    {
        private readonly By titleDetail = By.XPath("//h4[text()='Point Address']");
        private readonly By pointAddressName = By.XPath("//span[@class='object-name']");
        private readonly By inspectBtn = By.CssSelector("button[title='Inspect']");

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
        private readonly By allDueDate = By.XPath("//div[@class='slick-cell l7 r7']");

        //ACTIVE SERVICES TAB
        private readonly By activeServiceTab = By.CssSelector("a[aria-controls='activeServices-tab']");
        private readonly By allActiveServiceWithServiceUnitRow = By.XPath("//div[@class='parent-row']//span[@title='Open Service Task']");
        private readonly By allActiveServiceRows = By.XPath("//span[@title='Open Service Task' or @title='0']");

        private const string eventDynamicLocator = "//div[@class='parent-row'][{0}]//div[text()='Event']";
        private const string serviceUnitDynamic = "//div[@class='parent-row'][{0}]//div[@title='Open Service Unit']/span";
        private const string serviceWithServiceUnitDynamic = "//div[@class='parent-row'][{0}]//span[@title='Open Service Task']";
        private const string allserviceUnitDynamic = "//div[@class='parent-row'][{0}]//span[@title='Open Service Task' or @title='0']";

        private const string eventOptions = "//div[@id='create-event-dropdown']//li[text()='{0}']";
        private readonly By allEventOptions = By.CssSelector("ul#create-event-opts>li");

        //DYNAMIC LOCATOR
        private const string inspectionTypeOption = "//div[@id='inspection-modal']//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()=' Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//div[@id='inspection-modal']//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";

        public PointAddressDetailPage WaitForPointAddressDetailDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(titleDetail);
            return this;
        }

        public string GetPointAddressName()
        {
            return GetElementText(pointAddressName);
        }

        public PointAddressDetailPage ClickInspectBtn()
        {
            ClickOnElement(inspectBtn);
            return this;
        }

        public PointAddressDetailPage VerifyPointAddressId(string idExpected)
        {
            string idActual = GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/point-addresses/", "");
            Assert.AreEqual(idExpected, idActual);
            return this;
        }

        //INSPECTION MODEL
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

        public PointAddressDetailPage VerifyDefaultSourceDd(string sourceValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceValue);
            return this;
        }

        public PointAddressDetailPage ClickAndSelectInspectionType(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }

        public PointAddressDetailPage ClickAndSelectAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }

        public PointAddressDetailPage ClickAndSelectAssignedUser(string assignedUserValue)
        {
            ClickOnElement(assignedUserDd);
            ClickOnElement(assignedUserOption, assignedUserValue);
            return this;
        }

        public PointAddressDetailPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }

        public PointAddressDetailPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }

        public PointAddressDetailPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }

        public PointAddressDetailPage ClickOnInspectionCreatedLink()
        {
            ClickOnElement("//a[@id='echo-notify-success-link']");
            return this;
        }

        //POINT HISTORY TAB
        public PointAddressDetailPage ClickPointHistoryTab()
        {
            ClickOnElement(pointHistoryTab);
            return this;
        }

        public List<PointHistoryModel> GetAllPointHistory()
        {
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
                string dueDate = GetElementText(GetAllElements(allDueDate)[i]);
                string state = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[7])[i]);
                string resolution = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[8])[i]);
                allModel.Add(new PointHistoryModel(desc, ID, type, service, address, date, dueDate, state, resolution));
            }
            return allModel;
        }

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

        public PointAddressDetailPage FilterByPointHistoryId(string pointHistoryId)
        {
            SendKeys(filterInputById, pointHistoryId);
            SendKeys(filterInputById, Keys.Enter);
            return this;
        }

        //ACTIVE SERVICES TAB
        public PointAddressDetailPage ClickOnActiveServicesTab()
        {
            ClickOnElement(activeServiceTab);
            return this;
        }

        public List<ActiveSeviceModel> GetAllServiceWithServiceUnitModel()
        {
            List<ActiveSeviceModel> activeSeviceModels = new List<ActiveSeviceModel>();
            List<IWebElement> allActiveRow = GetAllElements(allActiveServiceWithServiceUnitRow);
            for(int i = 0; i < allActiveRow.Count; i++)
            {
                string eventLocator = string.Format(eventDynamicLocator, (i + 1).ToString());
                string serviceUnitValue = GetElementText(serviceUnitDynamic, (i + 1).ToString());
                string serviceValue = GetElementText(serviceWithServiceUnitDynamic, (i + 1).ToString());
                activeSeviceModels.Add(new ActiveSeviceModel(eventLocator, serviceUnitValue, serviceValue));
            }
            return activeSeviceModels;
        }

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

        public ActiveSeviceModel GetActiveServiceWithSkipService(List<ActiveSeviceModel> allActiveServicesInServiceTab)
        {
            return allActiveServicesInServiceTab.FirstOrDefault(x => x.service.Equals("Skips"));
        }


        public ActiveSeviceModel GetFirstActiveServiceModel()
        {
            string eventLocator = string.Format(eventDynamicLocator, "1");
            string serviceUnitValue = GetElementText(serviceUnitDynamic, "1");
            string serviceValue = GetElementText(serviceWithServiceUnitDynamic, "1");
            return new ActiveSeviceModel(eventLocator, serviceUnitValue, serviceValue);
        }

        public PointAddressDetailPage ClickFirstEventInFirstServiceRow()
        {
            ClickOnElement(eventDynamicLocator, "1");
            return this;
        }

        public PointAddressDetailPage ClickAnyEventInActiveServiceRow(string eventLocator)
        {
            ClickOnElement(eventLocator);
            return this;
        }

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

        public EventDetailPage ClickAnyEventOption(string eventName)
        {
            ClickOnElement(eventOptions, eventName);
            return PageFactoryManager.Get<EventDetailPage>();
        }

       
    }
}
