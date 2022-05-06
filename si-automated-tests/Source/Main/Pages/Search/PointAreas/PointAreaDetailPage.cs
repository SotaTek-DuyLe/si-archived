using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Search.PointAreas
{
    public class PointAreaDetailPage : BasePage
    {
        private readonly By titleDetail = By.XPath("//h4[text()='Point Area']");
        private readonly By inspectBtn = By.CssSelector("button[title='Inspect']");
        private readonly By areaName = By.XPath("//p[@class='object-name']");

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

        //DYNAMIC LOCATOR
        private const string inspectionTypeOption = "//div[@id='inspection-modal']//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//label[text()=' Allocated Unit']/following-sibling::div/select/option[text()='{0}']";
        private const string assignedUserOption = "//div[@id='inspection-modal']//label[text()='Assigned User']/following-sibling::div/select/option[text()='{0}']";

        public PointAreaDetailPage WaitForAreaDetailDisplayed()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(titleDetail);
            return this;
        }

        public PointAreaDetailPage ClickInspectBtn()
        {
            ClickOnElement(inspectBtn);
            return this;
        }

        //INSPECTION MODEL
        public PointAreaDetailPage IsCreateInspectionPopup()
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

        public PointAreaDetailPage VerifyDefaulValue()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(inspectionTypeDd), "Select... ...");
            Assert.IsTrue(GetFirstSelectedItemInDropdown(allocatedUnitDd).Contains("Select..."));
            Assert.AreEqual(GetFirstSelectedItemInDropdown(assignedUserDd), "Select... ...");
            //Date
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }

        public PointAreaDetailPage VerifyDefaultSourceDd(string sourceValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceValue);
            return this;
        }

        public PointAreaDetailPage ClickAndSelectInspectionType(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }

        public PointAreaDetailPage ClickAndSelectAllocatedUnit(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }

        public PointAreaDetailPage ClickAndSelectAssignedUser(string assignedUserValue)
        {
            ClickOnElement(assignedUserDd);
            ClickOnElement(assignedUserOption, assignedUserValue);
            return this;
        }

        public PointAreaDetailPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }

        public PointAreaDetailPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }

        public PointAreaDetailPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }

        public PointAreaDetailPage ClickOnInspectionCreatedLink()
        {
            ClickOnElement("//a[@id='echo-notify-success-link']");
            return this;
        }

        public PointAreaDetailPage VerifyPointAreaId(string idExpected)
        {
            string idActual = GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/point-areas/", "");
            Assert.AreEqual(idExpected, idActual);
            return this;
        }

        //POINT HISTORY TAB
        public PointAreaDetailPage ClickPointHistoryTab()
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
                string dueDate = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[6])[i]);
                string state = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[7])[i]);
                string resolution = GetElementText(GetAllElements(columnInRowPointHistoryTab, CommonConstants.PointHistoryTabColumn[8])[i]);
                allModel.Add(new PointHistoryModel(desc, ID, type, service, address, date, dueDate, state, resolution));
            }
            return allModel;
        }

        public PointAreaDetailPage VerifyPointHistory(PointHistoryModel pointHistoryModelActual, string desc, string id, string type, string address, string date, string dueDate, string state)
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

        public string GetPointAreaName()
        {
            return GetElementText(areaName);
        }

        public PointAreaDetailPage FilterByPointHistoryId(string pointHistoryId)
        {
            SendKeys(filterInputById, pointHistoryId);
            ClickOnElement(titleDetail);
            WaitUtil.WaitForPageLoaded();
            return this;
        }
    }
}
