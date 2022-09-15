using System;
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

        public ServiceDataManagementPage IsServiceDataManagementPage()
        {
            WaitUtil.WaitForElementVisible(serviceLocationTypeTitle);
            Assert.IsTrue(IsControlDisplayed(serviceLocationTypeTitle));
            Assert.IsTrue(IsControlDisplayed(refreshPageBtn));
            Assert.IsTrue(IsControlDisplayed(showInformationBtn));
            Assert.IsTrue(IsControlDisplayed(popOutBtn));
            return this;
        }

        public ServiceDataManagementPage ClickServiceLocationTypeDdAndSelectOption(string typeOptionValue)
        {
            ClickOnElement(selectTypeDd);
            //Select value
            ClickOnElement(serviceTypeOption, typeOptionValue);
            return this;
        }

        public ServiceDataManagementPage ClickOnServicesAndSelectGroupInTree(string serviceGroupName)
        {
            ClickOnElement(inputServicesTree);
            ClickOnElement(anyServicesGroupByContract, serviceGroupName);
            return this;
        }

        public ServiceDataManagementPage ClickOnApplyFiltersBtn()
        {
            ClickOnElement(applyFiltersBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

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

        public ServiceDataManagementPage ClickOnOkBtn()
        {
            ClickOnElement(okBtn);
            return this;
        }

        public ServiceDataManagementPage ClickOnSelectAndDeselectBtn()
        {
            ClickOnElement(selectAndDeselectBtn);
            return this;
        }

        public ServiceDataManagementPage ClickOnNextBtn()
        {
            ClickOnElement(nextBtn);
            return this;
        }

        public ServiceDataManagementPage RightClickOnFirstRowWithServiceTaskSchedule()
        {
            RightClickOnElement(firstRowWithServiceTaskSchedule);
            return this;
        }

        public ServiceDataManagementPage RightClickOnFirstRowWithServiceTaskSchedule(string descValue)
        {
            RightClickOnElement(string.Format(firstLocatorWithDescRedRow, descValue));
            return this;
        }

        public ServiceDataManagementPage DoubleClickOnFirstRowWithServiceTaskSchedule(string descValue)
        {
            DoubleClickOnElement(string.Format(firstLocatorWithDescRedRow, descValue));
            return this;
        }

        public string GetFirstDescWithRedColor()
        {
            return GetElementText(firstRedRow);
        }

        public ServiceDataManagementPage DoubleClickOnFirstRowWithServiceTaskSchedule()
        {
            DoubleClickOnElement(firstRowWithServiceTaskSchedule);
            return this;
        }

        public ServiceDataManagementPage VerifyActionMenuDisplayedWithActions()
        {
            foreach(string action in CommonConstants.ActionMenuSDM)
            {
                Assert.IsTrue(IsControlDisplayed(actionOption, action), action + " is not displayed");
            }
            return this;
        }

        public ServiceDataManagementPage ClickOnAnyOptionInActions(string actionName)
        {
            ClickOnElement(actionOption, actionName);
            return this;
        }

        public ServiceDataManagementPage VerifyActionMenuDisplayWithServiceUnit()
        {
            SleepTimeInMiliseconds(1000);
            foreach (string action in CommonConstants.ActionMenuSU)
            {
                Assert.IsTrue(IsControlDisplayed(actionOption, action), action + " is not displayed");
            }
            return this;
        }

        public ServiceDataManagementPage VerifyActionInActionMenuDisabled(string[] nameActions)
        {
            foreach(string action in nameActions)
            {
                Assert.AreEqual("true", GetAttributeValue(string.Format(actionOption, action), "disabled"), action + " is not disabaled");
            }
            return this;
        }

        public ServiceDataManagementPage RightClickOnFirstRowWithoutServiceTaskSchedule()
        {
            RightClickOnElement(firstRowWithoutServiceTaskSchedule);
            return this;
        }
        public ServiceDataManagementPage ClickOnMultipleRowsWithServiceTaskSchedule()
        {
            ScrollDownToElement(firstMultipleRowWithServiceTaskSchedule);
            ClickOnElement(firstMultipleRowWithServiceTaskSchedule);
            return this;
        }
        public ServiceDataManagementPage SelectMultipleRowsWithServiceTaskSchedule()
        {
            HoldKeyDownWhileClickOnElement(firstRowInMultipleWithServiceTaskSchedule);
            HoldKeyDownWhileClickOnElement(secondRowInMultipleWithServiceTaskSchedule);
            return this;
        }
        public ServiceDataManagementPage RightClickOnMultipleRowWithServiceTaskSchedule()
        {
            RightClickOnElement(firstRowInMultipleWithServiceTaskSchedule);
            return this;
        }
        public ServiceDataManagementPage SelectMultipleRowsWithoutServiceTaskSchedule()
        {
            HoldKeyDownWhileClickOnElement(firstMultipleRowWithoutServiceTaskSchedule);
            HoldKeyDownWhileClickOnElement(secondMultipleRowWithoutServiceTaskSchedule);
            return this;
        }
        public ServiceDataManagementPage RightClickOnMultipleRowWithoutServiceTaskSchedule()
        {
            RightClickOnElement(firstMultipleRowWithoutServiceTaskSchedule);
            return this;
        }
        public ServiceDataManagementPage RightClickOnFirstCellWithServiceUnit()
        {
            RightClickOnElement(firstCellWithServiceUnit);
            return this;
        }
        public ServiceDataManagementPage RightClickOnCellWithMutipleServiceUnitPoint()
        {
            ScrollDownToElement(firstCellWithMultipleServiceUnitPoint);
            RightClickOnElement(firstCellWithMultipleServiceUnitPoint);
            return this;
        }
        public ServiceDataManagementPage VerifyActionInActionMenuEnabled(string[] nameActions)
        {
            foreach (string action in nameActions)
            {
                Assert.IsTrue(IsControlEnabled(string.Format(actionOption, action)), action + " is not enabled");
            }
            return this;
        }

        public ServiceDataManagementPage RightClickOnMultipleServiceTaskScheduleSegment()
        {
            RightClickOnElement(thridMultipleRowWithoutServiceTaskSchedule);
            RightClickOnElement(forthMultipleRowWithoutServiceTaskSchedule);
            RightClickOnElement(fifthMultipleRowWithoutServiceTaskSchedule);
            return this;
        }

        public ServiceDataManagementPage ClickOutOfAction()
        {
            ClickOnElement(totalRow);
            return this;
        }

        //SET ASSURED
        private readonly By setEndDateLabel = By.XPath("//label[text()='Set End Date']");
        private readonly By inputEndDate = By.XPath("//input[@id='assured-end-date']");
        private readonly By applyAtBottomBtn = By.XPath("//button[text()='Apply']");

        public ServiceDataManagementPage VerifySetAssuredAfterClick()
        {
            Assert.IsTrue(IsControlDisplayed(setEndDateLabel));
            return this;
        }

        public ServiceDataManagementPage InputDateInSetEndDate(string endDateValue)
        {
            InputCalendarDate(inputEndDate, endDateValue);
            ClickOnElement(setEndDateLabel);
            return this;
        }

        public ServiceDataManagementPage ClickOnApplyAtBottomBtn()
        {
            ClickOnElement(applyAtBottomBtn);
            return this;
        }



    }
}
