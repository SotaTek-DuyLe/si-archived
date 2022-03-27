using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class LeaveEntryPage : BasePage
    {
        private readonly By leaveType = By.Id("state");
        private readonly By leaveReason = By.Id("resolution-code");
        private readonly By saveBtn = By.XPath("//button[text()='Save']");
        private readonly By approveBtn = By.XPath("//button[text()='Approve']");
        private readonly By confirmApprovalBtn = By.XPath("//button[text()='Yes, Approve']");

        public LeaveEntryPage IsOnLeaveEntryPage()
        {
            WaitUtil.WaitForElementVisible(leaveType);
            WaitUtil.WaitForElementVisible(leaveReason);
            //WaitUtil.WaitForElementVisible(saveBtn);
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
        public LeaveEntryPage SaveLeaveEntry()
        {
            ClickOnElement(saveBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage ApproveLeaveEntry()
        {
            ClickOnElement(approveBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public LeaveEntryPage ConfirmApprovalLeaveEntry()
        {
            ClickOnElement(confirmApprovalBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
    }
}
