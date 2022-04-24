using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class LeaveEntryPage : BasePage
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

        public LeaveEntryPage IsOnLeaveEntryPage()
        {
            WaitUtil.WaitForElementVisible(leaveType);
            WaitUtil.WaitForElementVisible(leaveReason);
            //WaitUtil.WaitForElementVisible(saveBtn);
            return this;
        }
        public LeaveEntryPage SelectLeaveResource(string _resourceName)
        {
            ClickOnElement(selectBtn);
            ClickOnElement(resourceOption, _resourceName);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage SelectLeaveType(string _leaveType)
        {
            SelectTextFromDropDown(leaveType, _leaveType);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage SelectLeaveReason(string _leaveReason)
        {
            SelectTextFromDropDown(leaveReason, _leaveReason);
            return this;
        }
        public LeaveEntryPage EnterDates(string _date)
        {
            SendKeys(startDate, _date);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage EnterDetails(string _details)
        {
            SendKeys(details, _details);
            return this;
        }
        public LeaveEntryPage SaveLeaveEntry()
        {
            WaitForLoadingIconToDisappear();
            ClickOnElement(saveBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage VerifyNewButtonsDisplayed()
        {
            WaitUtil.WaitForElementVisible(deleteBtn);
            WaitUtil.WaitForElementVisible(approveBtn);
            WaitUtil.WaitForElementVisible(declineBtn);
            return this;
        }
        public LeaveEntryPage ApproveLeaveEntry()
        {
            ClickOnElement(approveBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage DeclineLeaveEntry()
        {
            ClickOnElement(declineBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage DeleteLeaveEntry()
        {
            ClickOnElement(deleteBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage ConfirmApprovalLeaveEntry()
        {
            ClickOnElement(confirmApprovalBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage ConfirmDeclineLeaveEntry()
        {
            ClickOnElement(confirmDeclineBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage ConfirmDeleteLeaveEntry()
        {
            ClickOnElement(confirmDeleteBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage VerifyDateIsHighlighted(string date)
        {
            var xpath = String.Format(currentDateRow, date);
            Assert.AreEqual(GetAttributeValue(xpath, "style"), hightlightStyle);
            return this;
        }
        public LeaveEntryPage VerifyResourceNamesArePresent(string[] names)
        {
            List<IWebElement> headers = GetAllElements(tableHeaders);
            for(int i = 2; i < headers.Count; i++)
            {
                Assert.AreEqual(names[i-2], GetElementText(headers[i]));
            }
            return this;
        }
        public LeaveEntryPage VerifyTotalUnavailableNumberIs(int num)
        {
            Assert.AreEqual(num.ToString(), GetElementText(totalUnavailableAM));
            return this;
        }
    }
}
