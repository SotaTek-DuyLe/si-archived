using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.Common;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using System.Collections.Generic;
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
                .InsertDate(dateInFutre)
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
                .VerifyBackgroundColor(resourceName, "green")
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
                .InsertDate(currentDate)
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
            string resourceName2 = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string vehicleResourceName2 = "Van " + CommonUtil.GetRandomNumber(5);
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

            //Create driver 2
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName2)
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

            //Create vehicle 2
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(vehicleResourceName2)
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
                //.VerifyBackgroundColor(resourceName, "greenish")
                //.VerifyStateAbbreviation(resourceName, "S")
                .RefreshGrid()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResoultBackground("green")
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
                //.VerifyBackgroundColor(vehicleResourceName, "red")
                //.VerifyStateAbbreviation(vehicleResourceName, "M")
                //.FilterResource("Resource", vehicleResourceName)
                .VerifyFirstResoultBackground("red")
                .VerifyFirstResultValue("Status", "Maintenance");
            //Select state for resource
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName2)
                .VerifyFirstResultValue("Resource", resourceName2)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName2)
                .ClickAllocatedResource(resourceName2)
                .ClickViewShiftDetail();
            PageFactoryManager.Get<ShiftDetailPage>()
                .IsOnShiftDetailPage()
                .SelectState("Training")
                .SelectResolutionCode("Paid")
                .SaveDetail();
            if(PageFactoryManager.Get<BasePage>().GetAlertText().Equals("Ensure course and location are noted"))
            {
                PageFactoryManager.Get<BasePage>().AcceptAlertIfAny();
            }
            PageFactoryManager.Get<BasePage>().WaitForLoadingIconToDisappear();
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .VerifyStateAbbreviation(resourceName2, "T")
            //    .VerifyBackgroundColor(resourceName2, "red2")
            //    .FilterResource("Resource", resourceName2);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName2)
                .VerifyFirstResoultBackground("yellow")
                .VerifyFirstResultValue("Status", "Training");
            //Select state for vehicle
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", vehicleResourceName2)
                .VerifyFirstResultValue("Resource", vehicleResourceName2)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(vehicleResourceName2)
                .ClickAllocatedResource(vehicleResourceName2)
                .ClickViewShiftDetail();
            PageFactoryManager.Get<ShiftDetailPage>()
                .IsOnShiftDetailPage()
                .SelectState("Vehicle Off Road")
                .SaveDetail()
                .WaitForLoadingIconToDisappear();
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .VerifyStateAbbreviation(vehicleResourceName, "V")
            //    .VerifyBackgroundColor(vehicleResourceName, "red")
            //    .FilterResource("Resource", vehicleResourceName);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", vehicleResourceName2)
                .VerifyFirstResoultBackground("red")
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
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
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
                .InsertDate(dateInFutre)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", vehicleResourceName)
                .VerifyFirstResultValue("Status", "Maintenance");
        }

        [Category("Resources")]
        [Category("Huong")]
        [Category("Huong_2")]
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
        //[Category("Resources")]
        //[Category("Dee")]
        //[Test]
        //[Ignore("Ignore due to Ashna's request: Duplicated after modifying")]
        //public void TC_222_verify_color_of_resource_when_hovered()
        //{
        //    string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
        //    string vehicleResourceName = "Van " + CommonUtil.GetRandomNumber(5);
        //    string resourceType = "Driver";
        //    string vehicleResourceType = "Van";

        //    PageFactoryManager.Get<LoginPage>()
        //        .GoToURL(WebUrl.MainPageUrl);
        //    PageFactoryManager.Get<LoginPage>()
        //        .IsOnLoginPage()
        //        .Login(AutoUser22.UserName, AutoUser22.Password)
        //        .IsOnHomePage(AutoUser22);
        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption(MainOption.Resources)
        //        .OpenOption("Daily Allocation")
        //        .SwitchNewIFrame();
        //    PageFactoryManager.Get<ResourceAllocationPage>()
        //        .SelectContract(Contract.Municipal)
        //        .SelectBusinessUnit(Contract.Municipal)
        //        .SelectShift("AM")
        //        .ClickGo()
        //        .WaitForLoadingIconToDisappear()
        //        .SleepTimeInMiliseconds(2000);
        //    //Create driver
        //    PageFactoryManager.Get<ResourceAllocationPage>()
        //        .ClickCreateResource()
        //        .SwitchToLastWindow();
        //    PageFactoryManager.Get<ResourceDetailTab>()
        //        .IsOnDetailTab()
        //        .InputResourceName(resourceName)
        //        .SelectResourceType(resourceType)
        //        .SelectBusinessUnit(BusinessUnit.EastCollections)
        //        .TickContractRoam()
        //        .ClickSaveBtn()
        //        .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
        //        .ClickCloseBtn()
        //        .SwitchToLastWindow()
        //        .SwitchNewIFrame();
        //    //Create vehicle
        //    PageFactoryManager.Get<ResourceAllocationPage>()
        //        .ClickCreateResource()
        //        .SwitchToLastWindow();
        //    PageFactoryManager.Get<ResourceDetailTab>()
        //        .IsOnDetailTab()
        //        .InputResourceName(vehicleResourceName)
        //        .SelectResourceType(vehicleResourceType)
        //        .SelectBusinessUnit(BusinessUnit.EastCollections)
        //        .TickContractRoam()
        //        .ClickSaveBtn()
        //        .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
        //        .ClickCloseBtn()
        //        .SwitchToLastWindow()
        //        .SwitchNewIFrame()
        //        .WaitForLoadingIconToDisappear()
        //        .SwitchToTab("All Resources");
        //    //Verify Driver
        //    PageFactoryManager.Get<ResourceAllocationPage>()
        //        .FilterResource("Resource", resourceName)
        //        .VerifyFirstResultValue("Resource", resourceName)
        //        .DragAndDropFirstResourceToFirstRound()
        //        .WaitForLoadingIconToDisappear();
        //    PageFactoryManager.Get<ResourceAllocationPage>()
        //        .VerifyAllocatedResourceName(resourceName)
        //        .HoverAndVerifyBackgroundColor(resourceName, "light blue")
        //        .ClickAllocatedResource(resourceName)
        //        .SelectResourceState("SICK")
        //        .IsReasonPopupDisplayed()
        //        .SelectReason(ResourceReason.Paid)
        //        .ClickConfirmButton()
        //        .WaitForLoadingIconToDisappear();
        //    Thread.Sleep(500);
        //    PageFactoryManager.Get<ResourceAllocationPage>()
        //        .RefreshGrid()
        //        .FilterResource("Resource", resourceName)
        //        .HoverAndVerifyBackgroundColor(resourceName, "darker green");

        //    //Verify Vehicle
        //    PageFactoryManager.Get<ResourceAllocationPage>()
        //        .FilterResource("Resource", vehicleResourceName)
        //        .VerifyFirstResultValue("Resource", vehicleResourceName)
        //        .DragAndDropFirstResourceToFirstRound()
        //        .WaitForLoadingIconToDisappear();
        //    PageFactoryManager.Get<ResourceAllocationPage>()
        //        .VerifyAllocatedResourceName(vehicleResourceName)
        //        .ClickAllocatedResource(vehicleResourceName)
        //        .SelectResourceState("MAINTENANCE")
        //        .WaitForLoadingIconToDisappear();
        //    Thread.Sleep(500);
        //    PageFactoryManager.Get<ResourceAllocationPage>()
        //        .RefreshGrid()
        //        .FilterResource("Resource", vehicleResourceName)
        //        .HoverAndVerifyBackgroundColor(vehicleResourceName, "darker red");
        //}
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
                .VerifyRoundNameIsIncluded("(Adhoc) " + roundName)
                .ClickAddAdhocRoundBtn()
                .IsOnAddAdhocRoundPage()
                .InputAdhocRoundDetails(1, reason, note);
            string templateValue = PageFactoryManager.Get<AddAdhocRoundPopup>()
                .GetSelectedTemplate();
            PageFactoryManager.Get<AddAdhocRoundPopup>()
                .ClickCreateBtn()
                .VerifyRoundNameIsIncluded("(Adhoc) " + templateValue);
        }

        [Category("Resources")]
        [Category("Huong")]
        [Category("Huong_2")]
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

            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";

            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Commercial)
                .SelectBusinessUnit(Contract.Commercial)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            //Create driver
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            var resourceDetailTab = PageFactoryManager.Get<ResourceDetailTab>();
            resourceDetailTab
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit("Collections - Recycling")
                .TickContractRoam();

            PageFactoryManager.Get<ResourceDetailTab>()
               .ClickSaveBtn()
               .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            resourceDetailTab.ClickOnElement(resourceDetailTab.ShiftScheduleTab);
            resourceDetailTab.WaitForLoadingIconToDisappear();
            resourceDetailTab.WaitForLoadingIconToDisappear();
            resourceDetailTab.ClickOnElement(resourceDetailTab.AddNewShiftScheduleButton);
            resourceDetailTab.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();

            ShiftSchedulePage shiftSchedulePage = PageFactoryManager.Get<ShiftSchedulePage>();
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.ShiftDropdown);
            shiftSchedulePage.SelectByDisplayValueOnUlElement(shiftSchedulePage.ShiftMenu, "06.00 - 14.30 AM");
            //Click 'Save' on Shift Schedule form
            shiftSchedulePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            shiftSchedulePage.ClickCloseBtn()
                .SwitchToChildWindow(2);

            PageFactoryManager.Get<ResourceDetailTab>()
                .ClickCloseBtn()
                .SwitchToLastWindow()
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
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceHeaderInput, resourceName);
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
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceHeaderInput, resourceName);
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
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceHeaderInput, resourceName);
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Driver");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.VerifyResourceRowHasWhiteBackground(rowIdx);
        }

        [Category("Resources")]
        [Category("Huong")]
        [Category("Huong_2")]
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

            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";

            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Commercial)
                .SelectBusinessUnit(Contract.Commercial)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            //Create driver
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            var resourceDetailTab = PageFactoryManager.Get<ResourceDetailTab>();
            resourceDetailTab
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit("Collections - Recycling")
                .TickContractRoam();

            PageFactoryManager.Get<ResourceDetailTab>()
               .ClickSaveBtn()
               .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            resourceDetailTab.ClickOnElement(resourceDetailTab.ShiftScheduleTab);
            resourceDetailTab.WaitForLoadingIconToDisappear();
            resourceDetailTab.WaitForLoadingIconToDisappear();
            resourceDetailTab.ClickOnElement(resourceDetailTab.AddNewShiftScheduleButton);
            resourceDetailTab.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();

            ShiftSchedulePage shiftSchedulePage = PageFactoryManager.Get<ShiftSchedulePage>();
            shiftSchedulePage.ClickOnElement(shiftSchedulePage.ShiftDropdown);
            shiftSchedulePage.SelectByDisplayValueOnUlElement(shiftSchedulePage.ShiftMenu, "06.00 - 14.30 AM");
            //Click 'Save' on Shift Schedule form
            shiftSchedulePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            shiftSchedulePage.ClickCloseBtn()
                .SwitchToChildWindow(2);

            PageFactoryManager.Get<ResourceDetailTab>()
                .ClickCloseBtn()
                .SwitchToLastWindow()
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
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceHeaderInput, resourceName);
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
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceHeaderInput, resourceName);
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
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceHeaderInput, resourceName);
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Driver");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            string resource = resourceAllocationPage.GetResourceName(rowIdx);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            resourceAllocationPage.ClickType(rowIdx)
                .ClickLeftResourceMenu("TRAINING")
                .VerifyToastMessage($"Unable to log request. {resource} has Sick request for {londonCurrentDate.ToString("dd/MM/yyyy")} - {londonCurrentDate.ToString("dd/MM/yyyy")}");
        }

        //[Category("Resources")]
        //[Category("Huong")]
        //[Test]
        //[Ignore("Ignore due to George's request")]
        //public void TC_275_Translation_DA()
        //{
        //    var loginPage = PageFactoryManager.Get<LoginPage>();
        //    var resourceAllocationPage = PageFactoryManager.Get<ResourceAllocationPage>();
        //    loginPage.GoToURL(WebUrl.MainPageUrl);
        //    loginPage.IsOnLoginPage()
        //        .Login(AutoUser80.UserName, AutoUser80.Password)
        //        .IsOnHomePageWithoutWaitSearchBtn(AutoUser80);
        //    loginPage.ClickOnElement(loginPage.GetToogleButton(AutoUser80.DisplayName));
        //    resourceAllocationPage.OpenLocaleLanguage()
        //        .SwitchToChildWindow(2);
        //    LocalLanguagePage localLanguagePage = PageFactoryManager.Get<LocalLanguagePage>();
        //    localLanguagePage.SelectTextFromDropDown(localLanguagePage.LanguageSelect, "French")
        //        .ClickOnElement(localLanguagePage.SaveButton);
        //    localLanguagePage.SwitchToFirstWindow();
        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption(TextTranslation.ToString(MainOption.Resources, "French"))
        //        .OpenOption(TextTranslation.ToString("Daily Allocation", "French"))
        //        .SwitchNewIFrame();
        //    resourceAllocationPage.SelectContract(Contract.Commercial);
        //    resourceAllocationPage.SelectShift("AM");
        //    resourceAllocationPage.ClickOnElement(resourceAllocationPage.BusinessUnitInput);
        //    resourceAllocationPage.ExpandRoundNode(Contract.Commercial)
        //        .SelectRoundNode("Collections")
        //        .ClickOK()
        //        .WaitForLoadingIconToDisappear()
        //        .SleepTimeInMiliseconds(2000);
        //    int rowIdx = 0;
        //    //Verify whether the IN/OUT should read "PRÉSENT/NOT PRÉSENT" (when on the same day)
        //    //Verify whether the PRE-CONFIRM/UN-CONFIRM should read "PRÉ-CONFIRMER/REFUSER" (for date in future)
        //    resourceAllocationPage.ClickType(rowIdx)
        //        .VerifyResourceTranslation("IN/OUT", "French")
        //        .ClickOutSideMenu();

        //    string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
        //    resourceAllocationPage.InsertDate(dateInFutre)
        //        .ClickOK()
        //        .WaitForLoadingIconToDisappear()
        //        .SleepTimeInMiliseconds(2000);
        //    resourceAllocationPage.ClickType(rowIdx)
        //        .VerifyResourceTranslation("PRE-CONFIRM/UN-CONFIRM", "French")
        //        .ClickOutSideMenu();

        //    resourceAllocationPage.SwitchToDefaultContent();
        //    //Back to default localization
        //    loginPage.ClickOnElement(loginPage.GetToogleButton(AutoUser80.DisplayName));
        //    resourceAllocationPage.OpenLocaleLanguage()
        //        .SwitchToChildWindow(2);
        //    localLanguagePage.SelectTextFromDropDown(localLanguagePage.LanguageSelect, "English")
        //        .ClickOnElement(localLanguagePage.SaveButton);
        //    localLanguagePage.SwitchToFirstWindow();
        //}

        [Category("Resources")]
        [Category("Huong")]
        [Test]
        public void TC_301_Single_Sign_On()
        {
            //Verify that Echo user can login with email address or username
            var loginPage = PageFactoryManager.Get<LoginPage>();
            loginPage.GoToURL(WebUrl.MainPageUrl);
            loginPage.IsOnLoginPage()
                .Login(AutoUser22.UserName, AutoUser22.Password)
                .IsOnHomePageWithoutWaitSearchBtn(AutoUser22);
            loginPage.ClickOnElement(loginPage.GetToogleButton(AutoUser22.DisplayName));
            loginPage.OpenSettings()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            UserSettingPage userSettingPage = PageFactoryManager.Get<UserSettingPage>();
            userSettingPage.ClickOnElement(userSettingPage.DetailTab);
            userSettingPage.WaitForLoadingIconToDisappear();
            string userEmail = userSettingPage.GetInputValue(userSettingPage.EmailInput);
            userSettingPage.ClickCloseBtn()
                .SwitchToFirstWindow();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser22)
                .ClickUserNameDd()
                .ClickLogoutBtn();
            loginPage.IsOnLoginPage()
                .Login(userEmail, AutoUser22.Password)
                .IsOnHomePageWithoutWaitSearchBtn(AutoUser22);
            loginPage.ClickOnElement(loginPage.GetToogleButton(AutoUser22.DisplayName));
            loginPage.OpenSettings()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            userSettingPage.ClickOnElement(userSettingPage.DetailTab);
            userSettingPage.WaitForLoadingIconToDisappear();
            userSettingPage.VerifyInputValue(userSettingPage.EmailInput, userEmail);
            userSettingPage.ClickCloseBtn()
                .SwitchToFirstWindow();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser22)
                .ClickUserNameDd()
                .ClickLogoutBtn();
            //Verify that if multiple users have the same email address then they need to use username to login to Echo
            string sameUserEmail = "josie@selectedinterventions.com";
            loginPage.IsOnLoginPage()
                .Login(sameUserEmail, AutoUser23.Password);
            loginPage.WaitForLoadingIconToDisappear();
            loginPage.VerifyErrorMessageDisplay()
                .ClickChangeLoginButton();
            loginPage.IsOnLoginPage()
                .Login(AutoUser23.UserName, AutoUser23.Password)
                .IsOnHomePageWithoutWaitSearchBtn(AutoUser23);
            loginPage.ClickOnElement(loginPage.GetToogleButton(AutoUser23.DisplayName));
            loginPage.OpenSettings()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            userSettingPage.ClickOnElement(userSettingPage.DetailTab);
            userSettingPage.WaitForLoadingIconToDisappear();
            userSettingPage.VerifyInputValue(userSettingPage.EmailInput, sameUserEmail);
        }

        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_278_Resource_Substitution_Whole_Absence()
        {
            var resourceName = "Thomas Edison";
            var clientReference = " (E0456)";
            var substitutionName = "Samuel Morse";
            var leaveType = "Holiday";
            var leaveReason = "Paid";
            string startDate;
            string endDate;
            DateTime today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 4);
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 8);

            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 5);
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 8);
            }
            else if(today.DayOfWeek == DayOfWeek.Friday)
            {
                startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 5);
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 7);

            }
            else if(today.DayOfWeek == DayOfWeek.Saturday)
            {
                startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 4);
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 6);
            }
            else
            {
                startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 6);
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 8);
            }
            var details = CommonUtil.GetRandomString(5);
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser22.UserName, AutoUser22.Password)
                .IsOnHomePage(AutoUser22);
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
                .SelectLeaveResource(resourceName + clientReference)
                .SelectLeaveType(leaveType)
                .SelectLeaveReason(leaveReason)
                .EnterDates(startDate)
                .EnterEndDate(endDate + Keys.Tab)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Resources)
               .OpenOption("Daily Allocation")
               .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Commercial)
                .SelectBusinessUnit(Contract.Commercial)
                .SelectShift("AM")
                .InsertDate(startDate)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000)
                .SwitchToTab("All Resources");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", substitutionName)
                .DragAndDropFirstResultToResourceInRound(resourceName)
                .IsSubstitutionResourcePopupDisplayed()
                .ClickWholeAbsenceBtn()
                .ClickConfirmSubstitution()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);

            CommonFinder finder = new CommonFinder(DbContext);
            List<ResourceAllocationModel> list = finder.GetResourceAllocation(116);
            var convertedStartDate = CommonUtil.StringToDateTime(startDate, "dd/MM/yyyy");
            var convertedEndDate = CommonUtil.StringToDateTime(endDate, "dd/MM/yyyy");
            Assert.AreEqual(convertedEndDate.Day, list[0].startdate.Day);
            Assert.AreEqual(convertedEndDate.Month, list[0].startdate.Month);
            Assert.AreEqual(convertedEndDate.Year, list[0].startdate.Year);

            Assert.AreEqual(convertedStartDate.Day, list[2].startdate.Day);
            Assert.AreEqual(convertedStartDate.Month, list[2].startdate.Month);
            Assert.AreEqual(convertedStartDate.Year, list[2].startdate.Year);
        }

        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_279_Resource_Substitution_Just_Today()
        {
            var resourceName = "Thomas Edison";
            var clientReference = " (E0456)";
            var substitutionName = "Samuel Morse";
            var leaveType = "Holiday";
            var leaveReason = "Paid";
            string startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 14);
            string endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 16);
            string middleDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 15);

            DateTime temp = DateTime.ParseExact(startDate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 15);
                middleDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 16);
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 17);
            }
            else if (temp.DayOfWeek == DayOfWeek.Saturday)
            {
                startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 17);
                middleDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 18);
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 19);
            }
            else if (temp.DayOfWeek == DayOfWeek.Friday)
            {
                middleDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 19);
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 20);
            }
            else if (temp.DayOfWeek == DayOfWeek.Thursday)
            {
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 21);
            }



            var details = CommonUtil.GetRandomString(5);
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser22.UserName, AutoUser22.Password)
                .IsOnHomePage(AutoUser22);
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
                .SelectLeaveResource(resourceName + clientReference)
                .SelectLeaveType(leaveType)
                .SelectLeaveReason(leaveReason)
                .EnterDates(startDate)
                .EnterEndDate(endDate + Keys.Tab)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Resources)
               .OpenOption("Daily Allocation")
               .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Commercial)
                .SelectBusinessUnit(Contract.Commercial)
                .SelectShift("AM")
                .InsertDate(middleDate)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000)
                .SwitchToTab("All Resources");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", substitutionName)
                .DragAndDropFirstResultToResourceInRound(resourceName)
                .IsSubstitutionResourcePopupDisplayed()
                .ClickJustTodayBtn()
                .ClickConfirmSubstitution()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);


            PageFactoryManager.Get<ResourceAllocationPage>()
               .SelectContract(Contract.Commercial)
               .SelectShift("AM")
               .InsertDate(startDate)
               .ClickGo()
               .WaitForLoadingIconToDisappear()
               .SleepTimeInMiliseconds(2000);

            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName);

            PageFactoryManager.Get<ResourceAllocationPage>()
               .SelectContract(Contract.Commercial)
               .SelectShift("AM")
               .InsertDate(endDate)
               .ClickGo()
               .WaitForLoadingIconToDisappear()
               .SleepTimeInMiliseconds(2000);

            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName);



            CommonFinder finder = new CommonFinder(DbContext);
            List<ResourceAllocationModel> list = finder.GetResourceAllocation(116);
            var convertedEndDate = CommonUtil.StringToDateTime(middleDate, "dd/MM/yyyy");
            Assert.AreEqual(convertedEndDate.Day, list[0].startdate.Day);
            Assert.AreEqual(convertedEndDate.Month, list[0].startdate.Month);
            Assert.AreEqual(convertedEndDate.Year, list[0].startdate.Year);
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_280_Resource_Substitution_Custom_Date()
        {
            var resourceName = "Thomas Edison";
            var clientReference = " (E0456)";
            var substitutionName = "Samuel Morse";
            var leaveType = "Holiday";
            var leaveReason = "Paid";
            string startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 21);
            DateTime temp = DateTime.ParseExact(startDate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 22);
            }
            else if (temp.DayOfWeek == DayOfWeek.Saturday)
            {
                startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 23);
            }

            string middleDate1 = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 22);
            string middleDate2 = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 23);

            temp = DateTime.ParseExact(middleDate1, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                middleDate1 = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 23);
                middleDate2 = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 24);
            }
            else if (temp.DayOfWeek == DayOfWeek.Saturday)
            {
                middleDate1 = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 24);
                middleDate2 = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 25);
            }


            string endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 28);
            temp = DateTime.ParseExact(endDate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 29);
            }
            else if (temp.DayOfWeek == DayOfWeek.Saturday)
            {
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 30);
            }

            var details = CommonUtil.GetRandomString(5);
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser22.UserName, AutoUser22.Password)
                .IsOnHomePage(AutoUser22);
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
                .SelectLeaveResource(resourceName + clientReference)
                .SelectLeaveType(leaveType)
                .SelectLeaveReason(leaveReason)
                .EnterDates(startDate)
                .EnterEndDate(endDate + Keys.Tab)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Resources)
               .OpenOption("Daily Allocation")
               .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Commercial)
                .SelectBusinessUnit(Contract.Commercial)
                .SelectShift("AM")
                .InsertDate(middleDate1)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000)
                .SwitchToTab("All Resources");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", substitutionName)
                .DragAndDropFirstResultToResourceInRound(resourceName)
                .IsSubstitutionResourcePopupDisplayed()
                .ClickCustomDatesBtn()
                .InputToDate(middleDate2)
                .ClickConfirmSubstitution()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);


            PageFactoryManager.Get<ResourceAllocationPage>()
               .SelectContract(Contract.Commercial)
               .SelectShift("AM")
               .InsertDate(startDate)
               .ClickGo()
               .WaitForLoadingIconToDisappear()
               .SleepTimeInMiliseconds(2000);

            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName);

            PageFactoryManager.Get<ResourceAllocationPage>()
               .SelectContract(Contract.Commercial)
               .SelectShift("AM")
               .InsertDate(endDate)
               .ClickGo()
               .WaitForLoadingIconToDisappear()
               .SleepTimeInMiliseconds(2000);

            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName);



            CommonFinder finder = new CommonFinder(DbContext);
            List<ResourceAllocationModel> list = finder.GetResourceAllocation(116);
            var convertedAllocationDate1 = CommonUtil.StringToDateTime(middleDate1, "dd/MM/yyyy");
            var convertedAllocationDate2 = CommonUtil.StringToDateTime(middleDate2, "dd/MM/yyyy");
            Assert.AreEqual(convertedAllocationDate2.Day, list[0].startdate.Day);
            Assert.AreEqual(convertedAllocationDate2.Month, list[0].startdate.Month);
            Assert.AreEqual(convertedAllocationDate2.Year, list[0].startdate.Year);
            Assert.AreEqual(convertedAllocationDate1.Day, list[1].startdate.Day);
            Assert.AreEqual(convertedAllocationDate1.Month, list[1].startdate.Month);
            Assert.AreEqual(convertedAllocationDate1.Year, list[1].startdate.Year);
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_281_Resource_Substitution_Last_Date()
        {
            var resourceName = "Thomas Edison";
            var clientReference = " (E0456)";
            var substitutionName = "Samuel Morse";
            var leaveType = "Holiday";
            var leaveReason = "Paid";
            string startDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 35);
            string endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 37);
            DateTime temp = DateTime.ParseExact(endDate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 38);
            }
            else if (temp.DayOfWeek == DayOfWeek.Saturday)
            {
                endDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 39);
            }


            var details = CommonUtil.GetRandomString(5);
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser22.UserName, AutoUser22.Password)
                .IsOnHomePage(AutoUser22);
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
                .SelectLeaveResource(resourceName + clientReference)
                .SelectLeaveType(leaveType)
                .SelectLeaveReason(leaveReason)
                .EnterDates(startDate)
                .EnterEndDate(endDate + Keys.Tab)
                .EnterDetails(details)
                .SaveLeaveEntry()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Resources)
               .OpenOption("Daily Allocation")
               .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Commercial)
                .SelectBusinessUnit(Contract.Commercial)
                .SelectShift("AM")
                .InsertDate(endDate)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000)
                .SwitchToTab("All Resources");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", substitutionName)
                .DragAndDropFirstResultToResourceInRound(resourceName)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(substitutionName);
        }

        [Category("Resources")]
        [Category("Huong")]
        [Test]
        public void TC_328_Adding_default_resource_and_original_type_columns_in_default_resource_grid()
        {
            //Verify that a column ‘Default' is added after ‘Resource’ column which displays the name of default resource which the resource has got swapped for
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
                .InsertDate("07/06/2023")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);

            var resourceAllocationPage = PageFactoryManager.Get<ResourceAllocationPage>();
            resourceAllocationPage.ClickOnElement(resourceAllocationPage.AllResourceTab);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Loader");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            int rowIdx = resourceAllocationPage.DragResourceToDriverCell();
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.ClickViewRoundInstanceOnDroppedCell(rowIdx)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstanceDetailPage roundInstanceDetailPage = PageFactoryManager.Get<RoundInstanceDetailPage>();
            roundInstanceDetailPage.ClickOnElement(roundInstanceDetailPage.AllocatedResourceTab);
            roundInstanceDetailPage.WaitForLoadingIconToDisappear();
            //Verify a column ‘Original Type' is added after ‘Type’ column which displays the type of the resource which has been swapped for the default resource
            roundInstanceDetailPage.VerifyAllocatedRoundInstance("Driver", "Loader");

            //Verify if the default resource on round is working on round instance then display '-' in Default column
            roundInstanceDetailPage.VerifyDefaultResourceIsWorkingOnRI();

            ResoureDetailPage resoureDetailPage = PageFactoryManager.Get<ResoureDetailPage>();
            //Verify the resource name should be hyperlinked to the resource form
            roundInstanceDetailPage.ClickDefaultResource("Driver", "Loader")
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            resoureDetailPage.IsResourceDetailPage()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);

            //Verify if the allocated resource can be opened by clicking on name itself anyway
            roundInstanceDetailPage.ClickResource("Driver", "Loader")
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            resoureDetailPage.IsResourceDetailPage()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
        }

        [Category("Resources")]
        [Category("Huong")]
        [Test]
        public void TC_329_Experience_for_resources_in_the_allocated_resources_grid_of_round_instance_form()
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
                .InsertDate("07/06/2023")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);

            var resourceAllocationPage = PageFactoryManager.Get<ResourceAllocationPage>();
            resourceAllocationPage.ClickOnElement(resourceAllocationPage.AllResourceTab);
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceHeaderInput, "James Cook");
            resourceAllocationPage.SendKeys(resourceAllocationPage.ResourceTypeHeaderInput, "Driver");
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            int rowIdx = resourceAllocationPage.DragResourceDriverToDriverCell();
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.WaitForLoadingIconToDisappear();
            resourceAllocationPage.ClickViewRoundInstanceOnDroppedCell(rowIdx)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstanceDetailPage roundInstanceDetailPage = PageFactoryManager.Get<RoundInstanceDetailPage>();
            roundInstanceDetailPage.ClickOnElement(roundInstanceDetailPage.AllocatedResourceTab);
            roundInstanceDetailPage.WaitForLoadingIconToDisappear();
            //Verify whether the experience should be only displayed for the resource class= HUMAN
            roundInstanceDetailPage.VerifyAllocatedRoundInstance("Driver", "James Cook", "★");
            //Verify the number of times that resource has worked on
            //that round should be calculated based on the number of shift instances for that resource for that roundinstance’s round in last 6 months
        }
    }
}
