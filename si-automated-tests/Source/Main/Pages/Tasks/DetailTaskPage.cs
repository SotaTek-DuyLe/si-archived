using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class DetailTaskPage : BasePage
    {
        private readonly By taskTitle = By.XPath("//span[text()='Task']");
        private readonly By inspectionBtn = By.XPath("//button[@title='Inspect']");
        private readonly By locationName = By.CssSelector("a[class='typeUrl']");

        //INSPECTION POPUP
        private readonly By inspectionPopupTitle = By.XPath("//h4[text()='Create ']");
        private readonly By sourceDd = By.CssSelector("select#source");
        private readonly By inspectionTypeDd = By.CssSelector("select#inspection-type");
        private readonly By validFromInput = By.CssSelector("input#valid-from");
        private readonly By validToInput = By.CssSelector("input#valid-to");
        private readonly By allocatedUnitDd = By.CssSelector("select#allocated-unit");
        private readonly By allocatedUserDd = By.CssSelector("select#allocated-user");
        private readonly By noteInput = By.CssSelector("textarea#note");
        private readonly By createBtn = By.XPath("//button[text()='Create']");
        private readonly By cancelBtn = By.XPath("//button[text()='Create']/preceding-sibling::button");

        //INSPECTION TAB
        private readonly By inspectionTab = By.CssSelector("a[aria-controls='taskInspections-tab']");
        private readonly By addNewItemInSpectionBtn = By.XPath("//div[@id='taskInspections-tab']//button[text()='Add New Item']");
        private readonly By allRowInInspectionTabel = By.XPath("//div[@id='taskInspections-tab']//div[@class='grid-canvas']/div");
        private readonly By firstInspectionRow = By.XPath("//div[@id='taskInspections-tab']//div[@class='grid-canvas']/div[1]");
        private const string columnInRowInspectionTab = "//div[@id='taskInspections-tab']//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";

        //DYNAMIC LOCATOR
        private const string sourceName = "//select[@id='source']/option[text()='{0}']";
        private const string inspectionTypeOption = "//select[@id='inspection-type']/option[text()='{0}']";
        private const string allocatedUnitOption = "//select[@id='allocated-unit']/option[text()='{0}']";
        private const string allocatedUserOption = "//select[@id='allocated-user']/option[text()='{0}']";

        public DetailTaskPage IsDetailTaskPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(taskTitle);
            return this;
        }

        public string GetLocationName()
        {
            return GetElementText(locationName);
        }

        public DetailTaskPage ClickOnInspectionBtn()
        {
            ClickOnElement(inspectionBtn);
            return this;
        }

        //INSPECTION POPUP
        public DetailTaskPage IsInspectionPopup()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(inspectionPopupTitle);
            Assert.IsTrue(IsControlDisplayed(sourceDd));
            Assert.IsTrue(IsControlDisplayed(inspectionTypeDd));
            Assert.IsTrue(IsControlDisplayed(validFromInput));
            Assert.IsTrue(IsControlDisplayed(validToInput));
            Assert.IsTrue(IsControlDisplayed(allocatedUnitDd));
            Assert.IsTrue(IsControlDisplayed(allocatedUserDd));
            Assert.IsTrue(IsControlDisplayed(noteInput));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            //Create btn disabled
            Assert.AreEqual(GetAttributeValue(createBtn, "disabled"), "true");
            //Mandatory field
            Assert.AreEqual(GetCssValue(inspectionTypeDd, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(allocatedUnitDd, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }

        public DetailTaskPage VerifyDefaultValue(string sourceName)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(sourceDd), sourceName);
            Assert.AreEqual(GetAttributeValue(validFromInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(validToInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }

        public DetailTaskPage ClickAndVerifySourceDd(string[] sourceNameList)
        {
            ClickOnElement(sourceDd);
            //Verify
            foreach (string source in sourceNameList)
            {
                Assert.IsTrue(IsControlDisplayed(sourceName, source));
            }
            return this;
        }

        public DetailTaskPage ClickInspectionTypeDdAndSelectValue(string inspectionTypeValue)
        {
            ClickOnElement(inspectionTypeDd);
            ClickOnElement(inspectionTypeOption, inspectionTypeValue);
            return this;
        }

        public DetailTaskPage ClickAllocatedUnitAndSelectValue(string allocatedUnitValue)
        {
            ClickOnElement(allocatedUnitDd);
            ClickOnElement(allocatedUnitOption, allocatedUnitValue);
            return this;
        }

        public DetailTaskPage ClickAllocatedUserAndSelectValue(string allocatedUserValue)
        {
            ClickOnElement(allocatedUserDd);
            ClickOnElement(allocatedUserOption, allocatedUserValue);
            return this;
        }

        public DetailTaskPage InputNote(string noteValue)
        {
            SendKeys(noteInput, noteValue);
            return this;
        }

        public DetailTaskPage ClickCreateBtn()
        {
            ClickOnElement(createBtn);
            return this;
        }

        public DetailTaskPage InputValidFrom(string validFromValue)
        {
            SendKeys(validFromInput, validFromValue);
            return this;
        }

        public DetailTaskPage InputValidTo(string validFromTo)
        {
            SendKeys(validToInput, validFromTo);
            return this;
        }

        //INSPECTION TAB
        public DetailTaskPage ClickInspectionTab()
        {
            ClickOnElement(inspectionTab);
            return this;
        }

        public List<InspectionModel> getAllInspection()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(addNewItemInSpectionBtn);
            List<InspectionModel> allModel = new List<InspectionModel>();
            List<IWebElement> allRow = GetAllElements(allRowInInspectionTabel);
            List<IWebElement> allId = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[0]);
            List<IWebElement> allInspectionType = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[1]);
            List<IWebElement> allCreatedDate = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[2]);
            List<IWebElement> allCreatedByUser = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[3]);
            List<IWebElement> allAssignedUser = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[4]);
            List<IWebElement> allAllocatedUser = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[5]);
            List<IWebElement> allStatus = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[6]);
            List<IWebElement> allValidFrom = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[7]);
            List<IWebElement> allValidTo = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[8]);
            List<IWebElement> allCompletionDate = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[9]);
            List<IWebElement> allCancelledDate = GetAllElements(columnInRowInspectionTab, CommonConstants.InspectionTabColumn[10]);

            for (int i = 0; i < allRow.Count; i++)
            {
                string id = GetElementText(allId[i]);
                string inspectionType = GetElementText(allInspectionType[i]);
                string createdDate = GetElementText(allCreatedDate[i]);
                string createdByUser = GetElementText(allCreatedByUser[i]);
                string assignedUser = GetElementText(allAssignedUser[i]);
                string allocatedUnit = GetElementText(allAllocatedUser[i]);
                string status = GetElementText(allStatus[i]);
                string validFrom = GetElementText(allValidFrom[i]);
                string validTo = GetElementText(allValidTo[i]);
                string completionDate = GetElementText(allCompletionDate[i]);
                string cancelledDate = GetElementText(allCancelledDate[i]);
                allModel.Add(new InspectionModel(id, inspectionType, createdDate, createdByUser, assignedUser, allocatedUnit, status, validFrom, validTo, completionDate, cancelledDate));
            }
            return allModel;
        }

        public DetailTaskPage VerifyInspectionCreated(InspectionModel inspectionModelActual, string id, string inspectionType, string createdByUser, string assignedUser, string allocatedUnit, string status, string validFrom, string validTo)
        {
            Assert.AreEqual(id, inspectionModelActual.ID);
            Assert.AreEqual(inspectionType, inspectionModelActual.inspectionType);
            Assert.AreEqual(createdByUser, inspectionModelActual.createdByUser);
            Assert.AreEqual(assignedUser, inspectionModelActual.assignedUser);
            Assert.AreEqual(allocatedUnit, inspectionModelActual.allocatedUnit);
            Assert.AreEqual(status, inspectionModelActual.status);
            Assert.AreEqual(validFrom, inspectionModelActual.validFrom);
            Assert.AreEqual(validTo, inspectionModelActual.validTo);
            return this;
        }

        public DetailTaskPage ClickAddNewInspectionItem()
        {
            ClickOnElement(addNewItemInSpectionBtn);
            return this;
        }

        public DetailInspectionPage ClickOnFirstInspection()
        {
            DoubleClickOnElement(firstInspectionRow);
            return PageFactoryManager.Get<DetailInspectionPage>();
        }
    }
}
