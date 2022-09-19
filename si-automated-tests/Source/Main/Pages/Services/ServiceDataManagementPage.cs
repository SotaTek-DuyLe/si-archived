﻿using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceDataManagementPage : BasePage
    {
        private readonly By serviceLocationTypeTitle = By.XPath("//label[text()='Service Location Type']");
        private readonly By refreshPageBtn = By.XPath("//label[text()='Service Location Type']/parent::div/following-sibling::div//button[@title='Refresh']");
        private readonly By showInformationBtn = By.XPath("//label[text()='Service Location Type']/parent::div/following-sibling::div//button[@title='Show Information']");
        private readonly By popOutBtn = By.XPath("//label[text()='Service Location Type']/parent::div/following-sibling::div//button[@title='Pop out']");
        private readonly By inputServicesTree = By.XPath("//label[text()='Services']/following-sibling::input");
        private readonly By selectTypeDd = By.CssSelector("select[id='type']");
        private readonly By selectAndDeselectBtn = By.CssSelector("div[title='Select/Deselect All']");
        private readonly By nextBtn = By.CssSelector("button[id='next-button']");
        private readonly By firstRowWithServiceTaskSchedule = By.XPath("(//tbody/tr[1]/td[contains(@data-bind, 'retiredPoint')]/span)[1]");
        private readonly By firstRowWithServiceTaskScheduleAndNotAllocated = By.XPath("(//img[@data-bind='visible: serviceTask.isAssured' and contains(@style, 'display: none;')]/parent::span)[1]");
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
        private readonly By referenceIdInput = By.XPath("//div[@id='point-grid']//div[contains(@class, 'l1')]//input");

        //WARINING POPUP
        private readonly By warningTitle = By.XPath("//h4[text()='Warning']");
        private readonly By warningContent = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//div[text()='Please Note – Any previous row selections will be lost once filters are applied']");
        private readonly By checkboxMessage = By.XPath("//h4[text()='Warning']/parent::div/following-sibling::div//label[text()='Do not show this message again']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/following-sibling::button");

        //DYNAMIC
        private readonly string serviceTypeOption = "//select[@id='type']/option[text()='{0}']";
        private readonly string actionOption = "//div[@class='action-container']/button[text()='{0}']";
        private readonly string anyServicesGroupByContract = "//li[contains(@class, 'serviceGroups')]//a[text()='{0}']/i[1]";
        private readonly string firstLocatorWithDescRedRow = "(//tbody/tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]/td[contains(@data-bind, 'retiredPoint')]/span)[1]";
        private readonly string roundDate = "//table[@id='master-table']//tr[contains(@class, 'round-row')]/td[count(//tbody/tr[count(//td[text()='{0}']/parent::tr/preceding-sibling::tr) + 1]//span/parent::td[contains(@data-bind, 'retiredPoint')]/preceding-sibling::td) + 1]";

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
            if(roundName.Contains("Monday"))
            {
                return 2;
            } else if(roundName.Contains("Tuesday"))
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
        public ServiceDataManagementPage DoubleClickOnFirstRowUnAllocated()
        {
            DoubleClickOnElement(firstRowWithServiceTaskScheduleAndNotAllocated);
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
        public string GetFirstDescWithRedColor()
        {
            return GetElementText(firstRedRow);
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
            foreach(string action in CommonConstants.ActionMenuSDM)
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
            foreach(string action in nameActions)
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
            HoldKeyDownWhileClickOnElement(firstRowInMultipleWithServiceTaskSchedule);
            HoldKeyDownWhileClickOnElement(secondRowInMultipleWithServiceTaskSchedule);
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
            HoldKeyDownWhileClickOnElement(firstMultipleRowWithoutServiceTaskSchedule);
            HoldKeyDownWhileClickOnElement(secondMultipleRowWithoutServiceTaskSchedule);
            return this;
        }

        [AllureStep]
        public ServiceDataManagementPage RightClickOnMultipleRowWithoutServiceTaskSchedule()
        {
            RightClickOnElement(firstMultipleRowWithoutServiceTaskSchedule);
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
        public ServiceDataManagementPage ClickOnApplyAtBottomBtn()
        {
            ClickOnElement(applyAtBottomBtn);
            return this;
        }
    }
}
