using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using static si_automated_tests.Source.Main.Pages.Resources.ResourceAllocationPage;

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
                .SelectContract(Contract.Municipal)
                .SelectBusinessUnit(Contract.Municipal)
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
                .SelectBusinessUnit(BusinessUnit.EastCollections)
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
                .SelectContract(Contract.Municipal)
                .SelectBusinessUnit(Contract.Municipal)
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
                .SelectBusinessUnit(BusinessUnit.EastCollections)
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
                .SelectBusinessUnit(BusinessUnit.EastCollections)
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
                .IsReasonPopupDisplayed()
                .SelectReason(ResourceReason.Paid)
                .ClickConfirmButton()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "greenish")
                .VerifyStateAbbreviation(resourceName, "S")
                .RefreshGrid()
                .FilterResource("Resource", resourceName)
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
                .SelectContract(Contract.Municipal)
                .SelectBusinessUnit(Contract.Municipal)
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
                .SelectBusinessUnit(BusinessUnit.EastCollections)
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
                .SelectBusinessUnit(BusinessUnit.EastCollections)
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
                .RefreshGrid()
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

        [Category("Resources")]
        [Category("Huong")]
        [Test]
        public void TC_192_Unable_to_navigate_to_round_instance_from_resource_calendar()
        {
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
            var resourceAllocationPage = PageFactoryManager.Get<ResourceAllocationPage>();
            resourceAllocationPage.SelectContract(Contract.Commercial);
            resourceAllocationPage.SelectShift("AM");
            resourceAllocationPage.ClickOnElement(resourceAllocationPage.BusinessUnitInput);
            resourceAllocationPage.ExpandRoundNode(Contract.Commercial)
                .SelectRoundNode("Collections")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
               .ClickFirstResouceDetail()
               .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>().SwitchToTab("Calendar");
            var resourceCalendarTab = PageFactoryManager.Get<ResourceCalendarTab>();
            resourceCalendarTab
                .SwitchDateView("Month")
                .WaitForLoadingIconToDisappear();
            resourceCalendarTab
                .ClickRoundIntansceDetail(0)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            var roundinstanceDetailPage = PageFactoryManager.Get<RoundInstanceDetailPage>();
            roundinstanceDetailPage.VerifyElementVisibility(roundinstanceDetailPage.OpenRoundTitle, true)
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            resourceCalendarTab
                .ClickRoundIntansceDetail(2)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            roundinstanceDetailPage.VerifyElementVisibility(roundinstanceDetailPage.OpenRoundTitle, true)
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_222_verify_color_of_resource_when_hovered()
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
                .SelectContract(Contract.Municipal)
                .SelectBusinessUnit(Contract.Municipal)
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
                .SelectBusinessUnit(BusinessUnit.EastCollections)
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
                .SelectBusinessUnit(BusinessUnit.EastCollections)
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
                .HoverAndVerifyBackgroundColor(resourceName, "light blue")
                .ClickAllocatedResource(resourceName)
                .SelectResourceState("SICK")
                .IsReasonPopupDisplayed()
                .SelectReason(ResourceReason.Paid)
                .ClickConfirmButton()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .HoverAndVerifyBackgroundColor(resourceName, "darker green");

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
                .HoverAndVerifyBackgroundColor(vehicleResourceName, "darker red");
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_223_verify_daily_and_default_allocation_page_ui_changes()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";
            string contractUnit = "Ancillary";
            string dispatchSite = "Langhorn Road Depot (West)";
            string shift = "AM : 05.00 - 14.00";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser32.UserName, AutoUser32.Password)
                .IsOnHomePage(AutoUser32);
            //Verify on Default Allocation
            string dResourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string dResourceType = "Driver";

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Default Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Commercial)
                .VerifyUnassignedBusinessUnitIsDisplayed()
                .SelectBusinessUnit(Contract.Commercial)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            //Create new default resource
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(dResourceName)
                .SelectResourceType(dResourceType)
                .SelectBusinessUnit(BusinessUnit.CollectionRecycling)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");
            //Verify Default Allocation changes
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", dResourceName);
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValueInTab("Business Unit", BusinessUnit.CollectionRecycling);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .RefreshGrid()
                .FilterResource("Business Unit", BusinessUnit.CollectionRecycling);
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValueInTab("Business Unit", BusinessUnit.CollectionRecycling);
            
            //Verify on Daily Allocation
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyRoundFilterButtonEnabled(false)
                .SelectContract(Contract.Municipal)
                .VerifyBusinessUnitIsOptional()
                .SelectBusinessUnit(BusinessUnit.Unassigned)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyOnlyAllResourceTabIsDisplayed();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectBusinessUnit(Contract.Municipal)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickRoundFilterBtn()
                .ClearFilterOptionIfAny()
                .ClickApplyBtn()
                .WaitForLoadingIconToDisappear();
            //Create driver
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyRoundFilterButtonEnabled(true)
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.EastCollections)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("All Resources");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName);
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValueInTab("Business Unit", BusinessUnit.EastCollections);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .RefreshGrid()
                .FilterResource("Business Unit", BusinessUnit.EastCollections);
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValueInTab("Business Unit", BusinessUnit.EastCollections);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .OpenFirstRoundInstance()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .OpenRound()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundDetailPage>()
                .ClickRoundGroupHyperLink()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string bu = PageFactoryManager.Get<RoundGroupPage>()
                .GetBusinessUnit();
            Assert.AreEqual(BusinessUnit.EastCollections, bu);
            //Assert.AreEqual("An", contractUnit);
            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();

            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickRoundFilterBtn()
                .SelectContractUnit(contractUnit)
                .SelectDisapatchSite(dispatchSite)
                //.SelectServiceUnit("Ancillary")
                .SelectShiftFilter(shift)
                .ClickRememberOption()
                .ClickApplyBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyNumberOfFilter(3)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyNumberOfFilter(3)
                //.VerifyFirstRoundService("CLINICAL WASTE")
                .OpenFirstRoundInstance()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .OpenRound()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(5000);
            PageFactoryManager.Get<RoundDetailPage>()
                .VerifyContractUnit(contractUnit)
                .VerifyDispatchSite(dispatchSite)
                .VerifyShift(shift);

        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_220_verify_round_sort_order()
        {
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
                .SelectContract(Contract.Municipal)
                .SelectBusinessUnit(Contract.Municipal)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifySortOrderOfRoundInstances();
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_221_adding_adhoc_round()
        {
            string roundName = "adhoc autotest round " + CommonUtil.GetRandomNumber(5);
            int templateNo = 1;
            string reason = "Service Recovery";
            string note = "adhoc autotest not " + CommonUtil.GetRandomNumber(5);
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
                .SelectContract(Contract.Municipal)
                .SelectBusinessUnit(Contract.Municipal)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickAddAdhocRoundBtn()
                .IsOnAddAdhocRoundPage()
                .InputAdhocRoundDetails(templateNo, reason, note, roundName)
                .ClickCreateBtn()
                .VerifyFirstRoundName("(Adhoc) " + roundName)
                .ClickAddAdhocRoundBtn()
                .IsOnAddAdhocRoundPage()
                .InputAdhocRoundDetails(1, reason, note);
            string templateValue = PageFactoryManager.Get<AddAdhocRoundPopup>()
                .GetSelectedTemplate();
            PageFactoryManager.Get<AddAdhocRoundPopup>()
                .ClickCreateBtn()
                .VerifyFirstRoundName("(Adhoc) " + templateValue);
        }

        [Category("Resources")]
        [Category("Huong")]
        [Test]
        public void TC_273_Daily_Allocation_Available_color_change()
        {
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
            var resourceAllocationPage = PageFactoryManager.Get<ResourceAllocationPage>();
            resourceAllocationPage.SelectContract(Contract.Commercial);
            resourceAllocationPage.SelectShift("AM");
            resourceAllocationPage.ClickOnElement(resourceAllocationPage.BusinessUnitInput);
            resourceAllocationPage.ExpandRoundNode(Contract.Commercial)
                .SelectRoundNode("Collections")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);

            resourceAllocationPage.ClickOnElement(resourceAllocationPage.AllResourceTab);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Driver");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SelectTextFromDropDown(resourceAllocationPage.ThirdPartyHeaderInput, "false");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            int rowIdx = 0;
            resourceAllocationPage.ClickType(rowIdx)
                .ClickLeftResourceMenu("SICK")
                .SelectTextFromDropDown(resourceAllocationPage.LeftMenuReasonSelect, "Paid")
                .ClickOnElement(resourceAllocationPage.LeftMenuConfirmReasonButton);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SleepTimeInMiliseconds(5000);
            //Refresh grid
            resourceAllocationPage.ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            resourceAllocationPage.ClickOnElement(resourceAllocationPage.AllResourceTab);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Driver");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SelectTextFromDropDown(resourceAllocationPage.ThirdPartyHeaderInput, "false");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            //Verify
            resourceAllocationPage.VerifyResourceRowHasGreenBackground(rowIdx);
            resourceAllocationPage.ClickType(rowIdx)
                .ClickLeftResourceMenu("VIEW SHIFT DETAILS")
                .WaitForLoadingIconToDisappear();
            resourceAllocationPage.SelectTextFromDropDown(resourceAllocationPage.ResourceStateSelect, "Available");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.ClickOnElement(resourceAllocationPage.ResourceShiftInstanceButton);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SleepTimeInMiliseconds(2000);
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Driver");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.VerifyResourceRowHasWhiteBackground(rowIdx);
        }

        [Category("Resources")]
        [Category("Huong")]
        [Test]
        public void TC_272_Daily_Allocation_Dates_Overlap()
        {
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
            var resourceAllocationPage = PageFactoryManager.Get<ResourceAllocationPage>();
            resourceAllocationPage.SelectContract(Contract.Commercial);
            resourceAllocationPage.SelectShift("AM");
            resourceAllocationPage.ClickOnElement(resourceAllocationPage.BusinessUnitInput);
            resourceAllocationPage.ExpandRoundNode(Contract.Commercial)
                .SelectRoundNode("Collections")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);

            resourceAllocationPage.ClickOnElement(resourceAllocationPage.AllResourceTab);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Driver");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            int rowIdx = 0;
            resourceAllocationPage.ClickType(rowIdx)
                .ClickLeftResourceMenu("SICK")
                .SelectTextFromDropDown(resourceAllocationPage.LeftMenuReasonSelect, "Paid")
                .ClickOnElement(resourceAllocationPage.LeftMenuConfirmReasonButton);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SleepTimeInMiliseconds(5000);
            //Refresh grid
            resourceAllocationPage.ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            resourceAllocationPage.ClickOnElement(resourceAllocationPage.AllResourceTab);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Driver");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            //Verify
            resourceAllocationPage.VerifyResourceRowHasGreenBackground(rowIdx);
            resourceAllocationPage.ClickType(rowIdx)
                .ClickLeftResourceMenu("VIEW SHIFT DETAILS")
                .WaitForLoadingIconToDisappear();
            resourceAllocationPage.SelectTextFromDropDown(resourceAllocationPage.ResourceStateSelect, "Available");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.ClickOnElement(resourceAllocationPage.ResourceShiftInstanceButton);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SleepTimeInMiliseconds(2000);
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Driver");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            string resource = resourceAllocationPage.GetResourceName(rowIdx);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            resourceAllocationPage.ClickType(rowIdx)
                .ClickLeftResourceMenu("TRAINING")
                .VerifyToastMessage($"Unable to log request. {resource} has Sick request for {londonCurrentDate.ToString("dd/MM/yyyy")} - {londonCurrentDate.ToString("dd/MM/yyyy")}");
        }
    }
}
