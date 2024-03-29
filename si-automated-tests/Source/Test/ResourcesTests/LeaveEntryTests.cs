﻿using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using System;
using System.Collections.Generic;
using System.Text;

using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class LeaveEntryTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO DEFAULT ALLOCATION
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser21.UserName, AutoUser21.Password)
                .IsOnHomePage(AutoUser21);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Default Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Commercial)
                .SelectBusinessUnit(Contract.Commercial)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);

        }

        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_67_68_create_and_approve_leave_entry_descision_false()
        {
            string currentDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string details = CommonUtil.GetRandomString(5);
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";
            string leaveType = "Holiday";
            string leaveReason = "Paid";

            //Create new default resource
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.CollectionRecycling)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Leave Entry")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickButton("Create Leave Entry Record")
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .SelectLeaveResource(resourceName)
                .SelectLeaveType(leaveType)
                .SelectLeaveReason(leaveReason)
                .EnterDates(currentDate)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<LeaveEntryPage>()
                .VerifyNewButtonsDisplayed()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Pending")
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //TC-68: APPROVE LEAVE ENTRY
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .ApproveLeaveEntry()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LeaveEntryPage>()
                .ConfirmApprovalLeaveEntry()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(1000) //wait for window to be automatically closed
                .SwitchToLastWindow();
            PageFactoryManager.Get<LeaveEntryPage>()
               .IsOnLeaveEntryPage()
               .CloseCurrentWindow()
               .SwitchToLastWindow()
               .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemBy("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Approved");
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_69_deny_leave_entry()
        {
            string currentDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string details = CommonUtil.GetRandomString(5);
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";
            string leaveType = "Holiday";
            string leaveReason = "Paid";

            //CREATE NEW RESOURCE
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.CollectionRecycling)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Leave Entry")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickButton("Create Leave Entry Record")
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .SelectLeaveResource(resourceName)
                .SelectLeaveType(leaveType)
                .SelectLeaveReason(leaveReason)
                .EnterDates(currentDate)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<LeaveEntryPage>()
                .VerifyNewButtonsDisplayed()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemBy("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Pending")
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //TC-69: DECLINE LEAVE ENTRY
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .DeclineLeaveEntry()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LeaveEntryPage>()
                .ConfirmDeclineLeaveEntry()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2500) //wait for window to be automatically closed
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemBy("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Declined");
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_70_delete_leave_entry()
        {
            string currentDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string details = CommonUtil.GetRandomString(5);
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";
            string leaveType = "Holiday";
            string leaveReason = "Paid";

            //CREATE NEW RESOURCE
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.CollectionRecycling)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Leave Entry")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickButton("Create Leave Entry Record")
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .SelectLeaveResource(resourceName)
                .SelectLeaveType(leaveType)
                .SelectLeaveReason(leaveReason)
                .EnterDates(currentDate)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<LeaveEntryPage>()
                .VerifyNewButtonsDisplayed()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemBy("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Pending")
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();

            //TC-69: DELETE LEAVE ENTRY
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .DeleteLeaveEntry()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LeaveEntryPage>()
                .ConfirmDeleteLeaveEntry()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2500) //wait for window to be automatically closed
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemBy("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Cancelled");
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_71_create_leave_entry_decision_false()
        {
            string currentDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string details = CommonUtil.GetRandomString(5);
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";
            string leaveType = "Jury Service";

            //CREATE NEW RESOURCE
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.CollectionRecycling)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Leave Entry")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickButton("Create Leave Entry Record")
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .SelectLeaveResource(resourceName)
                .SelectLeaveType(leaveType)
                .EnterDates(currentDate)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemBy("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "N/A")
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();

            //VERIFY LEAVE ENTRY CAN BE DELETED
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .DeleteLeaveEntry()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LeaveEntryPage>()
                .ConfirmDeleteLeaveEntry()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2500) //wait for window to be automatically closed
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemBy("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Cancelled");
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_77_default_resource_count_leave_entry()
        {
            string currentDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string details = CommonUtil.GetRandomString(5);
            string resourceNameA = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceNameB = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string[] resourceNames = { resourceNameA, resourceNameB };
            string resourceType = "Driver";
            string leaveType = "Holiday";
            string leaveReason = "Paid";


            //CREATE NEW RESOURCE A
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceNameA)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.CollectionRecycling)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame();

            //CREATE NEW RESOURCE B
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow() 
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceNameB)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.CollectionRecycling)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");

            //ALLOCATE RESOURCES
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceNameA)
                .VerifyFirstResultValue("Resource", resourceNameA)
                .DragAndDropFirstResultToRoundGroup(2)
                .VerifyToastMessage("Default Resource Set");

            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceNameB)
                .VerifyFirstResultValue("Resource", resourceNameB)
                .DragAndDropFirstResultToRoundGroup(2)
                .VerifyToastMessage("Default Resource Set")

            //CREATE LEAVE ENTRY
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Leave Entry")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickButton("Create Leave Entry Record")
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .SelectLeaveResource(resourceNameA)
                .SelectLeaveType(leaveType)
                .SelectLeaveReason(leaveReason)
                .EnterDates(currentDate)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //VERIFY
            PageFactoryManager.Get<LeaveEntryPage>()
                .VerifyNewButtonsDisplayed()
                .VerifyDateIsHighlighted(currentDate)
                .VerifyResourceNamesArePresent(resourceNames)
                .SwitchToTab("All Resources");
            PageFactoryManager.Get<LeaveEntryPage>()
                .VerifyTotalUnavailableNumberIs(0)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();

            //CREATE ANOTHER LEAVE ENTRY
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickButton("Create Leave Entry Record")
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .SelectLeaveResource(resourceNameB)
                .SelectLeaveType(leaveType)
                .SelectLeaveReason(leaveReason)
                .EnterDates(currentDate)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            //VERIFY
            PageFactoryManager.Get<LeaveEntryPage>()
                .VerifyNewButtonsDisplayed()
                .VerifyDateIsHighlighted(currentDate)
                .VerifyResourceNamesArePresent(resourceNames)
                .SwitchToTab("All Resources");
            PageFactoryManager.Get<LeaveEntryPage>()
                .VerifyTotalUnavailableNumberIs(0)

            //DEALLOCATE RESOURCE TO MAINTAIN TEST
                .CloseCurrentWindow()
                .SwitchToLastWindow();

            //PageFactoryManager.Get<NavigationBase>()
            //   .ClickMainOption(MainOption.Resources)
            //   .OpenOption("Default Allocation")
            //   .SwitchNewIFrame();
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .SelectContract(Contract.NSC)
            //    .SelectBusinessUnit(Contract.NSC)
            //    .SelectShift("AM")
            //    .ClickGo()
            //    .WaitForLoadingIconToDisappear()
            //    .SleepTimeInMiliseconds(2000);
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .DeallocateResourceFromRoundGroup(2, resourceNameA)
            //    .VerifyToastMessage("Default resource cleared")
            //    .WaitUntilToastMessageInvisible("Default resource cleared");
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .DeallocateResourceFromRoundGroup(2, resourceNameB)
            //    .VerifyToastMessage("Default resource cleared")
            //    .WaitUntilToastMessageInvisible("Default resource cleared");
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .DeallocateResourceFromRoundGroup(2, resourceType)
            //    .VerifyToastMessage("Default resource-type cleared")
            //    .WaitUntilToastMessageInvisible("Default resource-type cleared");
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .DeallocateResourceFromRoundGroup(2, resourceType)
            //    .VerifyToastMessage("Default resource-type cleared")
            //    .WaitUntilToastMessageInvisible("Default resource-type cleared");
        }

        [Category("Resources")]
        [Category("Huong")]
        [Test]
        public void TC_264_Leave_Entry_Rescode()
        {
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Leave Entry")
                .SwitchNewIFrame();
            LeaveEntryPage leaveEntryPage = PageFactoryManager.Get<LeaveEntryPage>();
            leaveEntryPage.WaitForLoadingIconToDisappear();
            leaveEntryPage.ClickOnElement(leaveEntryPage.CreateLeaveEntryButton);
            leaveEntryPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            CreateLeaveEntryPage createLeaveEntryPage = PageFactoryManager.Get<CreateLeaveEntryPage>();
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            //Verify whether when user selects a resource state in the Leave Type dropdown that has mandatoryrescode = TRUE, show the Reason field in red to denote it is a mandatory field
            createLeaveEntryPage.ClickOnElement(createLeaveEntryPage.ResourceDropdown);
            createLeaveEntryPage.SelectByDisplayValueOnUlElement(createLeaveEntryPage.OpenDropDown, "Margaret Knight (E1556)");
            createLeaveEntryPage.SelectTextFromDropDown(createLeaveEntryPage.LeaveTypeDropdown, "Jury Service")
                .SleepTimeInMiliseconds(200);
            createLeaveEntryPage.VerifyElementIsMandatory(createLeaveEntryPage.ReasonDropdown, false);
            createLeaveEntryPage.SelectTextFromDropDown(createLeaveEntryPage.LeaveTypeDropdown, "Holiday")
                .SleepTimeInMiliseconds(200);
            createLeaveEntryPage.VerifyElementIsMandatory(createLeaveEntryPage.ReasonDropdown, true);
            createLeaveEntryPage.SendKeys(createLeaveEntryPage.FromDateInput, londonCurrentDate.ToString("dd/MM/yyyy"));
            createLeaveEntryPage.WaitForLoadingIconToDisappear();
            createLeaveEntryPage.ClickOnElement(createLeaveEntryPage.ToDateInput);
            createLeaveEntryPage.WaitForLoadingIconToDisappear();
            //Verify whether save is still enabled, and if user selects the Save button the error displays
            createLeaveEntryPage.VerifyElementEnable(createLeaveEntryPage.SaveButton, true);
            createLeaveEntryPage.ClickOnElement(createLeaveEntryPage.SaveButton);
            createLeaveEntryPage.VerifyToastMessage("Reason is required");
            
        }

        [Category("Resources")]
        [Category("Huong")]
        [Test]
        public void TC_267_Leave_Entry_Active_Resources()
        {
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Leave Entry")
                .SwitchNewIFrame();
            LeaveEntryPage leaveEntryPage = PageFactoryManager.Get<LeaveEntryPage>();
            leaveEntryPage.WaitForLoadingIconToDisappear();
            //Verify whether user not able to see retired resources in the leave entry dropdown list
            leaveEntryPage.OpenResource(false)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            CreateLeaveEntryPage createLeaveEntryPage = PageFactoryManager.Get<CreateLeaveEntryPage>();
            createLeaveEntryPage.VerifyResourceIsDisable(true)
                .CloseCurrentWindow()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            //Verify whether user able to view active or resources starting in future
            leaveEntryPage.ClickOnElement(leaveEntryPage.CreateLeaveEntryButton);
            leaveEntryPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            createLeaveEntryPage.VerifyResourceIsDisable(false)
                .ClickOnElement(createLeaveEntryPage.ResourceDropdown);
            createLeaveEntryPage.VerifyResourceHasValue()
                .CloseCurrentWindow()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            //Verify whether user able to view retired resources in existing records leave entry form
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Resources)
               .ExpandOption(Contract.Commercial)
               .OpenOption("Leave Entry")
               .SwitchNewIFrame();
            leaveEntryPage.WaitForLoadingIconToDisappear();
            leaveEntryPage.VerifyRetiredResourceAreExisting();
        }

        [Category("Resources")]
        [Category("Huong")]
        [Category("Huong_2")]
        [Test]
        public void TC_265_Leave_Entry_All_Resources_tab()
        {
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Leave Entry")
                .SwitchNewIFrame();
            LeaveEntryPage leaveEntryPage = PageFactoryManager.Get<LeaveEntryPage>();
            leaveEntryPage.WaitForLoadingIconToDisappear();
            leaveEntryPage.OpenResource(0)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            CreateLeaveEntryPage createLeaveEntryPage = PageFactoryManager.Get<CreateLeaveEntryPage>();
            createLeaveEntryPage.ClickOnElement(createLeaveEntryPage.AllResourceTab);
            createLeaveEntryPage.WaitForLoadingIconToDisappear();
            createLeaveEntryPage.ClickOnElement(createLeaveEntryPage.BUSelect);
            createLeaveEntryPage.VerifyBUSelectValues(new List<string>() { "Select...", "Collections - Recycling", "Collections - Refuse" });
        }
    }
}
