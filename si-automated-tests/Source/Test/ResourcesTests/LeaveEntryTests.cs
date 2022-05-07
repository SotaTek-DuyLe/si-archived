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
                .ClickMainOption("Resources")
                .OpenOption("Default Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract("North Star Commercial")
                .SelectBusinessUnit("North Star Commercial")
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);

        }

        [Category("Resources")]
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
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .ExpandOption("North Star Commercial")
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
                .VerifyToastMessage("Successfully saved Leave Entry");
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
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Approved");
        }
        [Category("Resources")]
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
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .ExpandOption("North Star Commercial")
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
                .VerifyToastMessage("Successfully saved Leave Entry");
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
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Declined");
        }
        [Category("Resources")]
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
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .ExpandOption("North Star Commercial")
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
                .VerifyToastMessage("Successfully saved Leave Entry");
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
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Cancelled");
        }
        [Category("Resources")]
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
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .ExpandOption("North Star Commercial")
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
                .VerifyToastMessage("Successfully saved Leave Entry")
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
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
                .VerifyFirstResultValue("Resource", resourceName)
                .VerifyFirstResultValue("Verdict", "Cancelled");
        }
        [Category("Resources")]
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
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
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
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");

            //ALLOCATE RESOURCES
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceNameA)
                .VerifyFirstResultValue("Resource", resourceNameA)
                .DragAndDropFirstResultToRound(2)
                .VerifyToastMessage("Default Resource Set");

            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceNameB)
                .VerifyFirstResultValue("Resource", resourceNameB)
                .DragAndDropFirstResultToRound(2)
                .VerifyToastMessage("Default Resource Set")

            //CREATE LEAVE ENTRY
                .SwitchToDefaultContent();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .ExpandOption("North Star Commercial")
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
                .VerifyToastMessage("Successfully saved Leave Entry");
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
                .VerifyToastMessage("Successfully saved Leave Entry");
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

            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption("Resources")
               .OpenOption("Default Allocation")
               .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract("North Star Commercial")
                .SelectBusinessUnit("North Star Commercial")
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceNameA)
                .VerifyToastMessage("Default resource cleared");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceType)
                .VerifyToastMessage("Default resource-type cleared");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceNameB)
                .VerifyToastMessage("Default resource cleared");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceType)
                .VerifyToastMessage("Default resource-type cleared");
        }
    }
}
