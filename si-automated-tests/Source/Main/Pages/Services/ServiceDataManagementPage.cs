using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages.PointAddress;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceDataManagementPage : BasePage
    {
        private readonly By serviceLocationTypeTitle = By.XPath("//label[text()='Service Location Type']");
        private readonly By refreshPageBtn = By.XPath("//label[text()='Service Location Type']/parent::div/preceding-sibling::div//button[@title='Refresh']");
        private readonly By showInformationBtn = By.XPath("//label[text()='Service Location Type']/parent::div/preceding-sibling::div//button[@title='Show Information']");
        private readonly By popOutBtn = By.XPath("//label[text()='Service Location Type']/parent::div/preceding-sibling::div//button[@title='Pop out']");
        private readonly By inputServicesTree = By.XPath("//label[text()='Services']/following-sibling::input");
        private readonly By selectTypeDd = By.CssSelector("select[id='type']");
        private readonly By selectAndDeselectBtn = By.CssSelector("div[title='Select/Deselect All']");
        private readonly By nextBtn = By.CssSelector("button[id='next-button']");
        private readonly By firstRowWithServiceTaskSchedule = By.XPath("(//img[@data-bind='visible: serviceTask.isAssured']/parent::span)[1]");
        private readonly By firstRowWithServiceTaskScheduleAndNotAllocated = By.XPath("(//img[@data-bind='visible: serviceTask.isAssured' and contains(@style, 'display: none;')]/parent::span)[1]");
        private readonly By secondRowWithServiceTaskScheduleAndNotAllocated = By.XPath("(//img[@data-bind='visible: serviceTask.isAssured' and contains(@style, 'display: none;')]/parent::span)[2]");
        private readonly By firstRowWithoutServiceTaskSchedule = By.XPath("(//tbody/tr[1]/td[contains(@data-bind, 'retiredPoint') and not(span)])[1]");
        private readonly By applyFiltersBtn = By.CssSelector("button[id='filter-button']");
        private readonly By firstMultipleRowWithServiceTaskSchedule = By.XPath("(//tbody/tr/td[contains(@data-bind, 'retiredPoint')]//button[@class='toggle'])[1]");
        private readonly By firstRowInMultipleWithServiceTaskSchedule = By.XPath("(//tbody/tr/td[contains(@data-bind, 'retiredPoint')]//button[@class='toggle']/parent::div/following-sibling::div/span)[1]");
        private readonly By secondRowInMultipleWithServiceTaskSchedule = By.XPath("(//tbody/tr/td[contains(@data-bind, 'retiredPoint')]//button[@class='toggle']/parent::div/following-sibling::div/span)[2]");
        private readonly By firstMultipleRowWithoutServiceTaskSchedule = By.XPath("(//tbody/tr[1]/td[contains(@data-bind, 'retiredPoint') and not(span)])[1]");
        private readonly By secondMultipleRowWithoutServiceTaskSchedule = By.XPath("(//tbody/tr[2]/td[contains(@data-bind, 'retiredPoint') and not(span)])[1]");
        private readonly By thridMultipleRowWithoutServiceTaskSchedule = By.XPath("(//tbody/tr[1]/td[contains(@data-bind, 'retiredPoint')and not(span)])[3]");
        private readonly By forthMultipleRowWithoutServiceTaskSchedule = By.XPath("(//tbody/tr[2]/td[contains(@data-bind, 'retiredPoint')and not(span)])[3]");
        private readonly By fifthMultipleRowWithoutServiceTaskSchedule = By.XPath("(//tbody/tr[3]/td[contains(@data-bind, 'retiredPoint')and not(span)])[3]");
        private readonly By firstCellWithServiceUnit = By.XPath("(//tbody/tr[1]/td[contains(@class, 'unit-cell')])[1]");
        private readonly By firstCellWithMultipleServiceUnitPoint = By.XPath("(//img[@src='content/style/images/service-unit.png']/following-sibling::img)[1]/parent::span");
        private readonly By totalRow = By.XPath("//span[contains(text(), 'Total = ')]");
        private readonly By firstRedRow = By.XPath("(//table[@id='description-table']//td[contains(@class, 'no-service-definition')])[1]");
        private readonly By secondRedRow = By.XPath("(//table[@id='description-table']//td[contains(@class, 'no-service-definition')])[2]");
        private readonly By referenceIdInput = By.XPath("//div[@id='point-grid']//div[contains(@class, 'l1')]//input");
        private readonly By totalRecord = By.XPath("//span[contains(text(),'Total =')]");


        //WARINING POPUP
        private readonly By warningTitle = By.XPath("//h4[text()='Warning']");
        private readonly By warningContent = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//div[text()='Please Note – Any previous row selections will be lost once filters are applied']");
        private readonly By checkboxMessage = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//label[text()='Do not show this message again']");
        private readonly By okBtn = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//button[text()='OK']");
        private readonly By cancelBtn = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//button[text()='Cancel']");

        //DYNAMIC
        private readonly string serviceTypeOption = "//select[@id='type']/option[text()='{0}']";
        private readonly string actionOption = "//div[@class='action-container']/button[text()='{0}']";
        private readonly string anyServicesGroupByContract = "//li[contains(@class, 'serviceGroups')]//a[text()='{0}']/i[1]";
        private readonly string expandServiceGroup = "//li[contains(@class, 'serviceGroups')]//a[text()='{0}']/preceding-sibling::i";
        private readonly string firstLocatorWithDescRedRow = "(//tbody/tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]/td[contains(@data-bind, 'retiredPoint')]/span/parent::td)[1]";
        private readonly string roundDate = "//table[@id='master-table']//tr[contains(@class, 'round-row')]/td[count(//tbody/tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//span/parent::td[contains(@data-bind, 'retiredPoint')]/preceding-sibling::td) + 1]";
        private readonly string columnRoundByRoundName = "//tbody[contains(@class, 'ui-droppable')]/tr[1]/td[count(//td[contains(@title, '{0}')]/preceding-sibling::td) + 1]";
        private readonly string firstColumnRoundByRoundNameContantSTS = "//tbody[contains(@class, 'ui-droppable')]/tr[1]/td[count(//td[contains(@title, '{0}')]/preceding-sibling::td) + 1]/span";
        private readonly string secondColumnRoundByRoundNameContantSTS = "//tbody[contains(@class, 'ui-droppable')]/tr[2]/td[count(//td[contains(@title, '{0}')]/preceding-sibling::td) + 1]/span";
        private readonly string checkboxByRefId = "//div[text()='{0}']/preceding-sibling::div/input";

        private readonly By ServiceUnitPointC = By.XPath("((//table[@id='master-table']//tbody//tr[4])//td[@class='unit-cell'])[5]");
        private readonly By SchedulePointC = By.XPath("((//table[@id='master-table']//tbody//tr[4])//td)[12]");
        private readonly By ServiceUnitPointA = By.XPath("(//table[@id='master-table']//tbody//tr[5]//td)[12]");
        private readonly By DescriptionPointC = By.XPath("//table[@id='description-table']//tbody//tr[4]//td[contains(@class, 'data-cell')]");

        [AllureStep]
        public ServiceDataManagementPage DoubleClickPointC()
        {
            DoubleClickOnElement(DescriptionPointC);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DragServiceUnitPointCToServicePointA()
        {
            IWebElement schedulePointC = GetElement(SchedulePointC);
            //var shedulePointCContentEle = schedulePointC.FindElements(By.XPath("./span[contains(@class, 'existing-schedule')]"));
            //Assert.IsTrue(shedulePointCContentEle.Count != 0);
            DragAndDrop(SchedulePointC, ServiceUnitPointA);
            SleepTimeInMiliseconds(300);
            schedulePointC = GetElement(SchedulePointC);
            //shedulePointCContentEle = schedulePointC.FindElements(By.XPath("./span[contains(@class, 'existing-schedule')]"));
            //Assert.IsTrue(shedulePointCContentEle.Count == 0);
            IWebElement imgServiceUnitPointC = GetElement(By.XPath("((//table[@id='master-table']//tbody//tr[4])//td)[12]//img[@class='action-image'][1]"));
            IWebElement imgMergeUnitPointC = GetElement(By.XPath("((//table[@id='master-table']//tbody//tr[4])//td)[12]//img[@class='action-image'][2]"));
            Assert.IsTrue(GetAttributeValue(imgServiceUnitPointC, "src").Contains("service-unit.png"));
            Assert.IsTrue(GetAttributeValue(imgMergeUnitPointC, "src").Contains("merged-unit.png"));
            IWebElement imgServiceUnitPointA = GetElement(By.XPath("((//table[@id='master-table']//tbody//tr[5])//td)[12]//img[@class='action-image'][1]"));
            IWebElement imgMergeUnitPointA = GetElement(By.XPath("((//table[@id='master-table']//tbody//tr[5])//td)[12]//img[@class='action-image'][2]"));
            Assert.IsTrue(GetAttributeValue(imgServiceUnitPointA, "src").Contains("service-unit.png"));
            Assert.IsTrue(GetAttributeValue(imgMergeUnitPointA, "src").Contains("merged-unit.png"));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage IsServiceDataManagementPage()
        {
            WaitUtil.WaitForElementVisible(serviceLocationTypeTitle);
            Assert.IsTrue(IsControlDisplayed(serviceLocationTypeTitle));
            Assert.IsTrue(IsControlDisplayed(refreshPageBtn));
            Assert.IsTrue(IsControlDisplayed(showInformationBtn));
            Assert.IsTrue(IsControlDisplayed(popOutBtn));
            return this;
        }

        [AllureStep]
        public int GetRoundDate(string descName)
        {
            string roundName = GetElementText(roundDate, descName);
            if (roundName.Contains("Monday"))
            {
                return 2;
            } else if (roundName.Contains("Tuesday"))
            {
                return 3;
            }
            else if (roundName.Contains("Tuesday"))
            {
                return 3;
            }
            else if (roundName.Contains("Wednesday"))
            {
                return 4;
            }
            else if (roundName.Contains("Thursday"))
            {
                return 5;
            }
            else if (roundName.Contains("Friday"))
            {
                return 6;
            }
            else if (roundName.Contains("Saturday"))
            {
                return 7;
            }
            return 8;
        }

        [AllureStep]
        public ServiceDataManagementPage ClickServiceLocationTypeDdAndSelectOption(string typeOptionValue)
        {
            ClickOnElement(selectTypeDd);
            //Select value
            ClickOnElement(serviceTypeOption, typeOptionValue);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickOnServicesAndSelectGroupInTree(string serviceGroupName)
        {
            ClickOnElement(inputServicesTree);
            ClickOnElement(anyServicesGroupByContract, serviceGroupName);
            return this;
        }


        [AllureStep]
        public ServiceDataManagementPage ClickOnServicesAndSelectGroupInTree(string contract, string serviceGroupName, string childServiceGroup)
        {
            ClickOnElement(inputServicesTree);
            ClickOnElement(expandServiceGroup, serviceGroupName);
            ClickOnElement(anyServicesGroupByContract, childServiceGroup);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickOnApplyFiltersBtn()
        {
            ClickOnElement(applyFiltersBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage VerifyWarningPopupDisplayed()
        {
            WaitUtil.WaitForElementVisible(warningTitle);
            Assert.IsTrue(IsControlDisplayed(warningTitle));
            Assert.IsTrue(IsControlDisplayed(warningContent));
            Assert.IsTrue(IsControlDisplayed(checkboxMessage));
            Assert.IsTrue(IsControlEnabled(okBtn));
            Assert.IsTrue(IsControlEnabled(cancelBtn));
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickOnOkBtn()
        {
            ClickOnElement(okBtn);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage FilterReferenceById(string refId)
        {
            SendKeys(referenceIdInput, refId);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage SelectCheckboxByReferenceId(string refId)
        {
            ScrollDownToElement(checkboxByRefId, refId);
            ClickOnElement(checkboxByRefId, refId);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage ClickOnSelectAndDeselectBtn()
        {
            ClickOnElement(selectAndDeselectBtn);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage ClickOnNextBtn()
        {
            ClickOnElement(nextBtn);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage RightClickOnFirstRowWithServiceTaskSchedule()
        {
            RightClickOnElement(firstRowWithServiceTaskSchedule);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnFirstRowUnAllocated()
        {
            RightClickOnElement(firstRowWithServiceTaskScheduleAndNotAllocated);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnSecondRowUnAllocated()
        {
            RightClickOnElement(secondRowWithServiceTaskScheduleAndNotAllocated);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnSecondRowUnAllocated(string roundNameDisplayed)
        {
            RightClickOnElement(string.Format(columnRoundByRoundName, roundNameDisplayed));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage SelectAndRightClickOnMultipleRowsUnAllocated(string firstDescRedName, string secondDescRedName)
        {
            List<string> locators = new List<string>();
            locators.Add(string.Format(firstLocatorWithDescRedRow, firstDescRedName));
            locators.Add(string.Format(firstLocatorWithDescRedRow, secondDescRedName));
            HoldKeyDownWhileClickOnElement(locators);
            RightClickOnElement(string.Format(firstLocatorWithDescRedRow, firstDescRedName));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnFirstRowUnAllocated()
        {
            DoubleClickOnElement(firstRowWithServiceTaskScheduleAndNotAllocated);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnFirstRowUnAllocated(string firstDescRedName)
        {
            DoubleClickOnElement(firstLocatorWithDescRedRow, firstDescRedName);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnSecondRowUnAllocated(string secondDescRedName)
        {
            DoubleClickOnElement(firstLocatorWithDescRedRow, secondDescRedName);
            return this;
        }
        [AllureStep]
        public ServiceDataManagementPage RightClickOnFirstRowWithServiceTaskSchedule(string descValue)
        {
            RightClickOnElement(string.Format(firstLocatorWithDescRedRow, descValue));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnFirstRowWithServiceTaskSchedule(string descValue)
        {
            DoubleClickOnElement(string.Format(firstLocatorWithDescRedRow, descValue));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnAnyRowWithServiceTaskSchedule(string roundNameDisplayed)
        {
            DoubleClickOnElement(string.Format(columnRoundByRoundName, roundNameDisplayed));
            return this;
        }

        [AllureStep]
        public string GetFirstDescWithRedColor()
        {
            return GetElementText(firstRedRow);
        }

        [AllureStep]
        public string GetSecondDescWithRedColor()
        {
            return GetElementText(secondRedRow);
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnFirstRowWithServiceTaskSchedule()
        {
            DoubleClickOnElement(firstRowWithServiceTaskSchedule);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyActionMenuDisplayedWithActions()
        {
            foreach (string action in CommonConstants.ActionMenuSDM)
            {
                Assert.IsTrue(IsControlDisplayed(actionOption, action), action + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage ClickOnAnyOptionInActions(string actionName)
        {
            ClickOnElement(actionOption, actionName);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyActionMenuDisplayWithServiceUnit()
        {
            SleepTimeInMiliseconds(1000);
            foreach (string action in CommonConstants.ActionMenuSU)
            {
                Assert.IsTrue(IsControlDisplayed(actionOption, action), action + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyActionInActionMenuDisabled(string[] nameActions)
        {
            foreach (string action in nameActions)
            {
                Assert.AreEqual("true", GetAttributeValue(string.Format(actionOption, action), "disabled"), action + " is not disabaled");
            }
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnFirstRowWithoutServiceTaskSchedule()
        {
            RightClickOnElement(firstRowWithoutServiceTaskSchedule);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage ClickOnMultipleRowsWithServiceTaskSchedule()
        {
            ScrollDownToElement(firstMultipleRowWithServiceTaskSchedule);
            ClickOnElement(firstMultipleRowWithServiceTaskSchedule);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage SelectMultipleRowsWithServiceTaskSchedule()
        {

            HoldKeyDownWhileClickOnElement(new List<By> { firstRowInMultipleWithServiceTaskSchedule, secondRowInMultipleWithServiceTaskSchedule });
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnMultipleRowWithServiceTaskSchedule()
        {
            RightClickOnElement(firstRowInMultipleWithServiceTaskSchedule);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage SelectMultipleRowsWithoutServiceTaskSchedule()
        {
            HoldKeyDownWhileClickOnElement(new List<By> { firstMultipleRowWithoutServiceTaskSchedule, secondMultipleRowWithoutServiceTaskSchedule });
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnMultipleRowWithoutServiceTaskSchedule()
        {
            RightClickOnElement(firstMultipleRowWithoutServiceTaskSchedule);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage ClickOnTotalRecord()
        {
            ClickOnElement(totalRecord);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnFirstCellWithServiceUnit()
        {
            RightClickOnElement(firstCellWithServiceUnit);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnCellWithMutipleServiceUnitPoint()
        {
            ScrollDownToElement(firstCellWithMultipleServiceUnitPoint);
            RightClickOnElement(firstCellWithMultipleServiceUnitPoint);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyActionInActionMenuEnabled(string[] nameActions)
        {
            foreach (string action in nameActions)
            {
                Assert.IsTrue(IsControlEnabled(string.Format(actionOption, action)), action + " is not enabled");
            }
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnMultipleServiceTaskScheduleSegment()
        {
            RightClickOnElement(thridMultipleRowWithoutServiceTaskSchedule);
            RightClickOnElement(forthMultipleRowWithoutServiceTaskSchedule);
            RightClickOnElement(fifthMultipleRowWithoutServiceTaskSchedule);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage ClickOutOfAction()
        {
            ClickOnElement(totalRow);
            return this;
        }

        //SET ASSURED
        private readonly By setEndDateLabel = By.XPath("//label[text()='Set End Date']");
        private readonly By inputEndDate = By.XPath("//input[@id='assured-end-date']");
        private readonly By applyAtBottomBtn = By.XPath("//button[text()='Apply']");
        private readonly By addAmendCrewNotesBtn = By.XPath("//button[text()='Add/Amend Crew Notes']");
        private readonly By setAssuredBtn = By.XPath("//button[text()='Set Assured']");
        private readonly By textareaInputNotes = By.CssSelector("textarea[id='crew-notes']");
        private readonly By saveBtn = By.XPath("//button[text()='Save']");

        [AllureStep]
        public ServiceDataManagementPage VerifySetAssuredAfterClick()
        {
            Assert.IsTrue(IsControlDisplayed(setEndDateLabel));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage InputDateInSetEndDate(string endDateValue)
        {
            InputCalendarDate(inputEndDate, endDateValue);
            ClickOnElement(setEndDateLabel);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage InputAddAmendCrewNotes(string noteValue)
        {
            SendKeys(textareaInputNotes, noteValue);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage ClickOnSaveBtn()
        {
            ClickOnElement(saveBtn);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage ClickOnApplyAtBottomBtn()
        {
            ClickOnElement(applyAtBottomBtn);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyWhiteColourAppearInCellNoSTSPresent(string roundNameValue)
        {
            Assert.IsTrue(IsControlUnDisplayed(firstColumnRoundByRoundNameContantSTS, roundNameValue));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyWhiteColourAppearInSecondCellNoSTSPresent(string roundNameValue)
        {
            Assert.IsTrue(IsControlUnDisplayed(secondColumnRoundByRoundNameContantSTS, roundNameValue));
            return this;
        }

        #region
        //Step and locator for TC-132/step 16
        private readonly By firstRowNotContaintServiceUnitStep16 = By.XPath("(//tbody/tr/td[contains(@data-bind, 'retiredPoint') and not(span)])[1]");
        private readonly By secondRowNotConstaintServiceUnitStep16 = By.XPath("(//tbody/tr/td[contains(@data-bind, 'retiredPoint') and not(span)])[2]");
        //DYNAMIC
        private readonly string firstCellWithServiceTaskScheduleByRoundName = "//tbody[contains(@class, 'ui-droppable')]/tr[1]/td[count(//td[contains(@title, '{0}')]/preceding-sibling::td) + 1]";
        private readonly string secondCellWithServiceTaskScheduleByRoundName = "//tbody[contains(@class, 'ui-droppable')]/tr[2]/td[count(//td[contains(@title, '{0}')]/preceding-sibling::td) + 1]";
        private readonly string thirdCellWithServiceTaskScheduleByRoundName = "//tbody[contains(@class, 'ui-droppable')]/tr[3]/td[count(//td[contains(@title, '{0}')]/preceding-sibling::td) + 1]";

        [AllureStep]
        public ServiceDataManagementPage SelectMultipleCellWithServiceTaskSchedule(string roundName)
        {
            HoldKeyDownWhileClickOnElement(new List<string> { string.Format(firstCellWithServiceTaskScheduleByRoundName, roundName), string.Format(secondCellWithServiceTaskScheduleByRoundName, roundName) });
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnMultipleRowWithServiceTaskSchedule(string roundName)
        {
            RightClickOnElement(string.Format(firstCellWithServiceTaskScheduleByRoundName, roundName));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage SelectMultipleCellWithNoServiceTaskSchedule()
        {
            HoldKeyDownWhileClickOnElement(new List<By> { firstRowNotContaintServiceUnitStep16, secondRowNotConstaintServiceUnitStep16 });
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnMultipleCellWithMoServiceTaskSchedule()
        {
            RightClickOnElement(secondRowNotConstaintServiceUnitStep16);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnThirdCellWithServiceTaskSchedule(string roundName)
        {
            RightClickOnElement(string.Format(firstCellWithServiceTaskScheduleByRoundName, roundName));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnForthCellWithServiceTaskSchedule(string roundName)
        {
            RightClickOnElement(string.Format(secondCellWithServiceTaskScheduleByRoundName, roundName));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnFifthCellWithServiceTaskSchedule(string roundName)
        {
            RightClickOnElement(string.Format(firstCellWithServiceTaskScheduleByRoundName, roundName));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnFirstServiceTasSchedule(string roundName)
        {
            DoubleClickOnElement(secondCellWithServiceTaskScheduleByRoundName, roundName);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnSecondServiceTasSchedule(string roundName)
        {
            DoubleClickOnElement(thirdCellWithServiceTaskScheduleByRoundName, roundName);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnSetAssuredCell16(string roundName)
        {
            DoubleClickOnElement(firstCellWithServiceTaskScheduleByRoundName, roundName);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnSetProximityAlert16(string roundName)
        {
            DoubleClickOnElement(secondCellWithServiceTaskScheduleByRoundName, roundName);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnAddAmendCrewNoteAndSetAssured16(string roundName)
        {
            DoubleClickOnElement(firstCellWithServiceTaskScheduleByRoundName, roundName);
            return this;
        }
        #endregion

        #region
        //Step and locator for Step 19
        private readonly string serviceUnitPointByNameStep19 = "(//tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//img[@src='content/style/images/service-unit.png']/parent::span)[1]";
        private readonly string serviceUnitPointWithMultipleIcon = "(//tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//img[@src='content/style/images/merged-unit.png']/preceding-sibling::img[@src='content/style/images/service-unit.png'])[1]";
        //private readonly string serviceTaskScheduleWithMultipleIcon = "//tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//img[@src='content/style/images/merged-unit.png' and not(contains(@style, 'display: none;'))]/parent::span[contains(@data-bind, 'serviceTaskSchedule: { serviceTaskSchedule: $data }')]";
        private readonly string anyPointWithDesc = "//table[@id='description-table']//td[text()='{0}']";

        [AllureStep]
        public ServiceDataManagementPage DragServiceUnitPointToServiceUnitStep19(string serviceUnitNamePointA, string serviceUnitNamePointB)
        {
            IWebElement schedulePointA = GetElement(serviceUnitPointByNameStep19, serviceUnitNamePointA);
            IWebElement schedulePointB = GetElement(serviceUnitPointByNameStep19, serviceUnitNamePointB);
            ScrollDownToElement(schedulePointB);
            DragAndDrop(schedulePointA, schedulePointB);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnServiceUnitPointStep19(string serviceUnitNamePointA)
        {
            RightClickOnElement(serviceUnitPointByNameStep19, serviceUnitNamePointA);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyDisplayMultipleIconInServiceUnit(string serviceUnitNamePointA, string serviceUnitNamePointB)
        {
            Assert.IsTrue(IsControlDisplayed(serviceUnitPointWithMultipleIcon, serviceUnitNamePointA));
            Assert.IsTrue(IsControlDisplayed(serviceUnitPointWithMultipleIcon, serviceUnitNamePointB));
            //Assert.IsTrue(IsControlDisplayed(serviceTaskScheduleWithMultipleIcon, serviceUnitNamePointA));
            //Assert.IsTrue(IsControlDisplayed(serviceTaskScheduleWithMultipleIcon, serviceUnitNamePointB));
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyUnDisplayMultipleIconInServiceUnit(string serviceUnitNamePointA, string serviceUnitNamePointB)
        {
            Assert.IsTrue(IsControlUnDisplayed(serviceUnitPointWithMultipleIcon, serviceUnitNamePointA));
            Assert.IsTrue(IsControlUnDisplayed(serviceUnitPointWithMultipleIcon, serviceUnitNamePointB));
            //Assert.IsTrue(IsControlUnDisplayed(serviceTaskScheduleWithMultipleIcon, serviceUnitNamePointA));
            //Assert.IsTrue(IsControlUnDisplayed(serviceTaskScheduleWithMultipleIcon, serviceUnitNamePointB));
            return this;
        }

        [AllureStep]
        public PointAddressDetailPage DoubleClickAtAnyPointStep19(string serviceUnitName)
        {
            DoubleClickOnElement(anyPointWithDesc, serviceUnitName);
            return PageFactoryManager.Get<PointAddressDetailPage>();
        }
        #endregion

        #region locators and steps for Step 21
        private readonly string serviceUnitWithMultipleServiceUnit = "(//tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//button[text()='Multiple']/ancestor::td/preceding-sibling::td[1])[1]";
        private readonly string serviceUnitIconAtServiceUnit = "//table[@id='master-table']//tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//td[8]//img[@src='content/style/images/service-unit.png']";
        private readonly string mergedUnitIconAtServiceUnit = "//table[@id='master-table']//tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//td[8]//img[@src='content/style/images/merged-unit.png']";
        private readonly string multipleBtnAtServiceUnit = "//table[@id='master-table']//tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//td[9]//button[text()='Multiple']";
        private readonly string toggleMultipleBtnAtServiceUnit = "//table[@id='master-table']//tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//td[9]//button[@class='toggle']";

        [AllureStep]
        public ServiceDataManagementPage RightClickOnServiceUnitWithMultipleServiceUnitStep19(string serviceUnitName)
        {
            RightClickOnElement(serviceUnitWithMultipleServiceUnit, serviceUnitName);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyUnDisplayIconForServiceUnitAndLinkedIcon(string serviceUnitName)
        {
            Assert.IsTrue(IsControlUnDisplayed(serviceUnitIconAtServiceUnit, serviceUnitName));
            Assert.IsTrue(IsControlUnDisplayed(mergedUnitIconAtServiceUnit, serviceUnitName));
            Assert.IsTrue(IsControlUnDisplayed(multipleBtnAtServiceUnit, serviceUnitName));
            Assert.IsTrue(IsControlUnDisplayed(toggleMultipleBtnAtServiceUnit, serviceUnitName));
            return this;
        }
        #endregion

        #region locators and steps for Step [Reallocate]
        private readonly By tuesdayEveryWeekNotAllocated = By.XPath("//table[@id='master-table']//tbody/tr[1]/td[count(//thead/tr[@class='round-row']/td[@title='REC1-AM:Tuesday']/preceding-sibling::td) + 1]");
        private readonly By wednesdayEveryWeekNotAllocated = By.XPath("//table[@id='master-table']//tbody/tr[1]/td[count(//thead/tr[@class='round-row']/td[@title='REF1-AM:Wednesday']/preceding-sibling::td) + 1]");

        [AllureStep]
        public ServiceDataManagementPage RightClickOnTuesdayEveryWeek()
        {
            RightClickOnElement(tuesdayEveryWeekNotAllocated);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DragTuesDayToWednesdayToTestReallocate()
        {
            DragAndDrop(tuesdayEveryWeekNotAllocated, wednesdayEveryWeekNotAllocated);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DragWednesdayToTuesDayToTestReallocate()
        {
            DragAndDrop(wednesdayEveryWeekNotAllocated, tuesdayEveryWeekNotAllocated);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnTuesdayAfterReallocate()
        {
            DoubleClickOnElement(tuesdayEveryWeekNotAllocated);
            return this;
        }

        #endregion

        #region locators and steps for Step [2]
        private readonly string firstRoundByRoundNameStep2 = "//tbody[contains(@class, 'ui-droppable')]/tr[1]/td[count((//td[text()='{0}'])[1]/preceding-sibling::td) + 1]";
        private readonly string firstRoundByRoundNameWithServiceUnitStep2 = "//tbody[contains(@class, 'ui-droppable')]/tr[1]/td[count(//td[text()='{0}']/preceding-sibling::td) + 1]/preceding-sibling::td[1]//img[@src='content/style/images/service-unit.png']";
        private readonly string firstRoundByRoundNameAfterAddServiceTaskScheduleStep2 = "//tbody[contains(@class, 'ui-droppable')]/tr[1]/td[count(//td[text()='{0}']/preceding-sibling::td) + 1]/span";

        [AllureStep]
        public ServiceDataManagementPage RightClickOnFirstRowWithNoServiceUnitStep2(string roundName)
        {
            RightClickOnElement(firstRoundByRoundNameStep2, roundName);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyDisplayServiceUnitAfterAddServiceTaskSchedule(string roundName)
        {
            Assert.IsTrue(IsControlDisplayed(firstRoundByRoundNameWithServiceUnitStep2, roundName), "Service Unit icon is not displayed after [Add Service Task Schedule");
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage VerifyDisplayGreenBorderAfterAddServiceTaskSchedule(string roundName)
        {
            Assert.IsTrue(IsControlDisplayed(firstRoundByRoundNameAfterAddServiceTaskScheduleStep2, roundName), "Green border is not displayed after [Add Service Task Schedule");
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnFirstServiceTaskScheduleByRoundStep2(string roundName)
        {
            DoubleClickOnElement(firstRoundByRoundNameStep2, roundName);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage DoubleClickOnFirstUnitServiceByRoundStep2(string roundName)
        {
            DoubleClickOnElement(firstRoundByRoundNameWithServiceUnitStep2, roundName);
            return this;
        }

        #endregion
    }
}
