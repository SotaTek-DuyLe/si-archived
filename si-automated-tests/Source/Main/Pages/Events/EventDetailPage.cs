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
        private const string columnInRowPointHistoryTab = "//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private readonly By firstRowPointHistory = By.XPath("//div[@id='pointHistory-tab']//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");

        //DYNAMIC
        private const string eventType = "//span[text()='{0}']";
        private const string urlType = "//a[text()='{0}']";
        private const string sourceOption = "//select[@id='source']/option[text()='{0}']";
        private const string inspectionTypeOption = "//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()='Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";


        public EventDetailPage WaitForEventDetailDisplayed(string eventTypeValue, string location)
        {
            WaitUtil.WaitForElementVisible(eventTitle);
            WaitUtil.WaitForElementVisible(eventType, eventTypeValue);
            WaitUtil.WaitForElementVisible(urlType, location);
            return this;
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

        //POPUP CREATE INSPECTION
        public EventDetailPage IsCreateInspectionPopup(bool isIcon)
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
    }
}
