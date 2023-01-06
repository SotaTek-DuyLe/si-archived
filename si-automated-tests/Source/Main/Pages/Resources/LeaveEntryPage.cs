using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class LeaveEntryPage : BasePageCommonActions
    {
        private readonly By selectBtn = By.XPath("//button[@data-id='resource']");
        private readonly string resourceOption = "//option[text()='{0}']";
        private readonly By leaveType = By.Id("state");
        private readonly By leaveReason = By.Id("resolution-code");
        private readonly By startDate = By.Id("startDate");
        private readonly By endDate = By.Id("endDate");
        private readonly By details = By.Id("details");
        private readonly By saveBtn = By.XPath("//button[text()='Save']");
        private readonly By deleteBtn = By.XPath("//button[text()='Delete']");
        private readonly By approveBtn = By.XPath("//button[text()='Approve']");
        private readonly By declineBtn = By.XPath("//button[text()='Decline']");
        private readonly By confirmApprovalBtn = By.XPath("//button[text()='Yes, Approve']");
        private readonly By confirmDeclineBtn = By.XPath("//button[text()='Yes, Decline']");
        private readonly By confirmDeleteBtn = By.XPath("//button[text()='Yes']");

        //RIGHT PANEL
        //DEFAULT ALLOCATION TAB
        private readonly string hightlightStyle = "background-color: skyblue;";
        private readonly string currentDateRow = "//td[text()='{0}']/parent::tr";
        private readonly By tableHeaders = By.XPath("//div[@class='tab-pane active']//tr[@class='spaced-header']/th");

        //ALL RESOURCES TAB
        private readonly By totalUnavailableAM = By.XPath("((//div[@class='tab-pane active']//tr[@style='background-color: skyblue;'])[2]//span)[2]");
        public readonly By CreateLeaveEntryButton = By.XPath("//button[text()='Create Leave Entry Record']");

        private string ResourceTable = "//div[@class='grid-canvas']";
        private string ResourceRow = "./div[contains(@class, 'slick-row')]";
        private string ResourceStartDateCell = "./div[@class='slick-cell l8 r8']"; 
        private string ResourceEndDateCell = "./div[@class='slick-cell l9 r9']";
        private readonly By EndDateFilterInput = By.XPath("//div[@class='ui-state-default slick-headerrow-column l9 r9']//input");
        private readonly By EndDateFilterButton = By.XPath("//div[@class='ui-state-default slick-headerrow-column l9 r9']//button");
        private readonly By FilterDropDown = By.XPath("//ul[@role='listbox' and @aria-expanded='true']");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");

        public TableElement ResourceTableEle
        {
            get => new TableElement(ResourceTable, ResourceRow, new List<string>() { ResourceStartDateCell, ResourceEndDateCell });
        }

        [AllureStep]
        public LeaveEntryPage OpenResource(bool isActive)
        {
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            ClickOnElement(EndDateFilterButton);
            SelectByDisplayValueOnUlElement(FilterDropDown, isActive ? "Greater than" : "Less than");
            SendKeys(EndDateFilterInput, londonCurrentDate.ToString("dd/MM/yyyy"));
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            ResourceTableEle.DoubleClickRow(0);
            return this;
        }

        [AllureStep]
        public LeaveEntryPage OpenResource(int rowIdx)
        {
            ResourceTableEle.DoubleClickRow(rowIdx);
            return this;
        }

        [AllureStep]
        public LeaveEntryPage VerifyRetiredResourceAreExisting()
        {
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            ClickOnElement(EndDateFilterButton);
            SelectByDisplayValueOnUlElement(FilterDropDown, "Less than");
            SendKeys(EndDateFilterInput, londonCurrentDate.ToString("dd/MM/yyyy"));
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            string endDate = ResourceTableEle.GetCellValue(0, ResourceTableEle.GetCellIndex(ResourceEndDateCell)).AsString();
            Assert.IsTrue(CommonUtil.StringToDateTime(endDate, "dd/MM/yyyy") < londonCurrentDate);
            return this;
        }

        [AllureStep]
        public LeaveEntryPage IsOnLeaveEntryPage()
        {
            WaitUtil.WaitForElementVisible(leaveType);
            WaitUtil.WaitForElementVisible(leaveReason);
            //WaitUtil.WaitForElementVisible(saveBtn);
            return this;
        }
        [AllureStep]
        public LeaveEntryPage SelectLeaveResource(string _resourceName)
        {
            ClickOnElement(selectBtn);
            ClickOnElement(resourceOption, _resourceName);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage SelectLeaveType(string _leaveType)
        {
            SelectTextFromDropDown(leaveType, _leaveType);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage SelectLeaveReason(string _leaveReason)
        {
            SelectTextFromDropDown(leaveReason, _leaveReason);
            return this;
        }
        [AllureStep]
        public LeaveEntryPage EnterDates(string _date)
        {
            SendKeys(startDate, _date);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage EnterDetails(string _details)
        {
            SendKeys(details, _details);
            return this;
        }
        [AllureStep]
        public LeaveEntryPage SaveLeaveEntry()
        {
            WaitForLoadingIconToDisappear();
            ClickOnElement(saveBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage VerifyNewButtonsDisplayed()
        {
            WaitUtil.WaitForElementVisible(deleteBtn);
            WaitUtil.WaitForElementVisible(approveBtn);
            WaitUtil.WaitForElementVisible(declineBtn);
            return this;
        }
        [AllureStep]
        public LeaveEntryPage ApproveLeaveEntry()
        {
            ClickOnElement(approveBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage DeclineLeaveEntry()
        {
            ClickOnElement(declineBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage DeleteLeaveEntry()
        {
            ClickOnElement(deleteBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage ConfirmApprovalLeaveEntry()
        {
            ClickOnElement(confirmApprovalBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage ConfirmDeclineLeaveEntry()
        {
            ClickOnElement(confirmDeclineBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage ConfirmDeleteLeaveEntry()
        {
            ClickOnElement(confirmDeleteBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public LeaveEntryPage VerifyDateIsHighlighted(string date)
        {
            var xpath = String.Format(currentDateRow, date);
            Assert.AreEqual(GetAttributeValue(xpath, "style"), hightlightStyle);
            return this;
        }
        [AllureStep]
        public LeaveEntryPage VerifyResourceNamesArePresent(string[] names)
        {
            List<IWebElement> headers = GetAllElements(tableHeaders);
            List<String> allNames = new List<String>();
            for(int i = 2; i < headers.Count; i++)
            {
                allNames.Add(GetElementText(headers[i]));
            }
            bool isMatch = names.All(name => allNames.Contains(name));
            Assert.IsTrue(isMatch, "Not every name are present");
            return this;
        }
        [AllureStep]
        public LeaveEntryPage VerifyTotalUnavailableNumberIs(int num)
        {
            Assert.AreEqual(num.ToString(), GetElementText(totalUnavailableAM));
            return this;
        }
    }
}
