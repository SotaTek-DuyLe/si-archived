using System;
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
    }
}
