using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class DailyAllocationTests : BaseTest
    {
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_41_42_43_Create_Resource_And_Daily_Allocation()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string currentDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string resourceType = "Driver";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser22.UserName, AutoUser22.Password)
                .IsOnHomePage(AutoUser22);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.RM)
                .SelectBusinessUnit(Contract.RM)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");
            //ALLOCATE FOR CURRENT DATE
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName)
                .ClickAllocatedResource(resourceName)
                .VerifyPresenceOption("IN/OUT")
                .ClickPresenceOption();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "green")
                .ClickAllocatedResource(resourceName)
                .VerifyPresenceOption("IN/OUT")
                .ClickPresenceOption();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "white")
                .InsertDate(dateInFutre + Keys.Enter)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SwitchToTab("All Resources");
            //ALLOCATE FOR FUTURE DATE
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName)
                .ClickAllocatedResource(resourceName)
                .VerifyPresenceOption("PRE-CONFIRM/UN-CONFIRM")
                .ClickPresenceOption();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "purple")
                .ClickAllocatedResource(resourceName)
                .VerifyPresenceOption("PRE-CONFIRM/UN-CONFIRM")
                .ClickPresenceOption();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "white")
            //TC-43
            //Deallocate future resource
                .DeallocateResource(resourceName)
                .RefreshGrid()
                .FilterResource("Resource", resourceName)
                .VerifyResourceDeallocated(resourceName)
                .VerifyFirstResultValue("Status", "Available")
            //Deallocate current-date resource
                .InsertDate(currentDate + Keys.Enter)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickAllocatedResource(resourceName)
                .ClickViewShiftDetail();
            PageFactoryManager.Get<ShiftDetailPage>()
                .IsOnShiftDetailPage()
                .RemoveAllocation()
                .SaveDetail()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .RefreshGrid()
                .FilterResource("Resource", resourceName)
                .VerifyResourceDeallocated(resourceName)
                .VerifyFirstResultValue("Status", "Available");
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_44_1_Create_Resource_And_Daily_Allocation()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string vehicleResourceName = "Van " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";
            string vehicleResourceType = "Van";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser22.UserName, AutoUser22.Password)
                .IsOnHomePage(AutoUser22);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.RM)
                .SelectBusinessUnit(Contract.RM)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            //Create driver
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Create vehicle
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(vehicleResourceName)
                .SelectResourceType(vehicleResourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("All Resources");
            //Verify Driver
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName)
                .ClickAllocatedResource(resourceName)
                .SelectResourceState("SICK")
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "greenish")
                .VerifyStateAbbreviation(resourceName, "S")
                .VerifyFirstResultValue("Status", "Sick");
            //Verify Vehicle
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", vehicleResourceName)
                .VerifyFirstResultValue("Resource", vehicleResourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(vehicleResourceName)
                .ClickAllocatedResource(vehicleResourceName)
                .SelectResourceState("MAINTENANCE")
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(vehicleResourceName, "red")
                .VerifyStateAbbreviation(vehicleResourceName, "M")
                //.FilterResource("Resource", vehicleResourceName)
                .VerifyFirstResultValue("Status", "Maintenance");
            //Select state for resource
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName)
                .ClickAllocatedResource(resourceName)
                .ClickViewShiftDetail();
            PageFactoryManager.Get<ShiftDetailPage>()
                .IsOnShiftDetailPage()
                .SelectState("Training")
                .SelectResolutionCode("Paid")
                .SaveDetail()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyStateAbbreviation(resourceName, "T")
                .VerifyBackgroundColor(resourceName, "red2")
                .FilterResource("Resource", resourceName);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyFirstResultValue("Status", "Training");
            //Select state for vehicle
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(vehicleResourceName)
                .ClickAllocatedResource(vehicleResourceName)
                .ClickViewShiftDetail();
            PageFactoryManager.Get<ShiftDetailPage>()
                .IsOnShiftDetailPage()
                .SelectState("Vehicle Off Road")
                .SaveDetail()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyStateAbbreviation(vehicleResourceName, "V")
                .VerifyBackgroundColor(vehicleResourceName, "red")
                .FilterResource("Resource", vehicleResourceName);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyFirstResultValue("Status", "Vehicle Off Road");

        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_44_2_Create_Resource_And_Daily_Allocation()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string vehicleResourceName = "Van " + CommonUtil.GetRandomNumber(5);
            string currentDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string today = CommonUtil.GetLocalTimeNow("dd");
            string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string resourceType = "Driver";
            string vehicleResourceType = "Van";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser22.UserName, AutoUser22.Password)
                .IsOnHomePage(AutoUser22);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.RM)
                .SelectBusinessUnit(Contract.RM)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            //Create driver
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Create vehicle
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(vehicleResourceName)
                .SelectResourceType(vehicleResourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");

            //View Resource Detail of human resource
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName)
                .ClickAllocatedResource(resourceName)
                .ClickResourceDetail()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .SwitchToTab("Calendar");
            PageFactoryManager.Get<ResourceCalendarTab>()
                .SwitchDateView("Year")
                .OpenTodayDateInYearView()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LeaveEntryPage>()
                .IsOnLeaveEntryPage()
                .SelectLeaveType("Holiday")
                .SelectLeaveReason("Paid")
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<LeaveEntryPage>()
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
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceCalendarTab>()
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Status", "Holiday");
            //View Resource Detail of vehicle resource
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", vehicleResourceName)
                .VerifyFirstResultValue("Resource", vehicleResourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(vehicleResourceName)
                .ClickAllocatedResource(vehicleResourceName)
                .ClickResourceDetail()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .SwitchToTab("Shift Exceptions");
            PageFactoryManager.Get<ResourceShiftExceptionTab>()
                .IsOnShiftExceptionTab()
                .SelectState("Maintenance")
                .SetEndDate(dateInFutre)
                .ClickCreateException()
                .VerifyToastMessage("Success")
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .InsertDate(dateInFutre + Keys.Enter)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", vehicleResourceName)
                .VerifyFirstResultValue("Status", "Maintenance");
        }
    }
}
