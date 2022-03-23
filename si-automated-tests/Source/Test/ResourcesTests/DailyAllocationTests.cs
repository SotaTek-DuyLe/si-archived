using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class DailyAllocationTests : BaseTest
    {
        [Category("Resources")]
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
                .Login(AutoUser4.UserName, AutoUser4.Password)
                .IsOnHomePage(AutoUser4);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract("North Star")
                .SelectBusinessUnit("North Star")
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
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
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
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
                .DeallocateResourceByDragAndDrop(resourceName)
                .RefreshGrid()
                .FilterResource("Resource", resourceName)
                .VerifyResourceDeallocated(resourceName)
                .VerifyFirstResultValue("Status", "Available")
            //Deallocate current-date resource
                .InsertDate(currentDate + Keys.Enter)
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
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
        [Test]
        public void TC_44_Create_Resource_And_Daily_Allocation()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string vehicleResourceName = "Van " + CommonUtil.GetRandomNumber(5);
            string currentDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string resourceType = "Driver";
            string vehicleResourceType = "Van";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser4.UserName, AutoUser4.Password)
                .IsOnHomePage(AutoUser4);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract("North Star")
                .SelectBusinessUnit("North Star")
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
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
                .VerifyToastMessage("Successfully saved resource.")
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
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
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
                .VerifyBackgroundColor(resourceName, "red")
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
                .FilterResource("Resource", vehicleResourceName)
                .VerifyFirstResultValue("Status", "Maintenance");

        }

    }
}
