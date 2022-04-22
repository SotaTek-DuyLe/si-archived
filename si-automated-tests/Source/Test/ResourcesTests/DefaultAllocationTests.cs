using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.Round;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    class DefaultAllocationTests : BaseTest
    {
        [Category("Resources")]
        [Test]
        public void TC_64_Allocation_Deallocation_Default_Resource_Type()
        {
            string roundName = "SKIP2 Daily Daily";
            string currentDate = CommonUtil.GetLocalTimeNow("dd");
            string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd", 3);
            string monthYearInFuture = CommonUtil.GetLocalTimeMinusDay("MMMM yyyy", 3);
            string resourceType = "Driver";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser20.UserName, AutoUser20.Password)
                .IsOnHomePage(AutoUser20);
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
                .SleepTimeInMiliseconds(2000)
                .SwitchToTab("Resource Type");
            //Drag resource type to round
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyFirstResultValue("Type", resourceType)
                .DragAndDropFirstResultToRound(2)
                .VerifyToastMessage("Default resource-type set");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickRound(roundName)
                .ClickViewRoundGroup()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundDetailTab>()
                .IsOnDetailTab()
                .SwitchToTab("Default Resources");
            //Verify end date is current date
            PageFactoryManager.Get<RoundDefaultResourceTab>()
                .IsOnDefaultResourceTab()
                .ClickOnEndDate(2)
                .VerifyEndDateIsDefault()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Move to future date and deallocate resource type from round
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCalendar()
                .InsertDayInFutre(dateInFutre)
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceType)
                .VerifyToastMessage("Default resource-type cleared");
            //Verify End date is updated on round in future
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickRound("SKIP2 Daily Daily")
                .ClickViewRoundGroup()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Default Resources");
            PageFactoryManager.Get<RoundDefaultResourceTab>()
                .IsOnDefaultResourceTab()
                .ClickOnEndDate(2)
                .VerifyEndDateIs(monthYearInFuture, dateInFutre)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Verify End date is updated on round in current date
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCalendar()
                .InsertDayInFutre(currentDate)
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ExpandRoundGroup(2)
                .ClickRound(roundName)
                .ClickViewRoundGroup()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Default Resources");
            PageFactoryManager.Get<RoundDefaultResourceTab>()
                .IsOnDefaultResourceTab()
                .ClickOnEndDate(2)
                .VerifyEndDateIs(monthYearInFuture, dateInFutre)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Deallocate to maintain script
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceType)
                .VerifyToastMessage("Default resource-type cleared");
        }
        [Category("Resources")]
        [Test]
        public void TC_65_Allocation_Deallocation_Default_Resource()
        {
            string roundName = "SKIP2 Daily Daily";
            string currentDate = CommonUtil.GetLocalTimeNow("dd");
            string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd", 3);
            string monthYearInFuture = CommonUtil.GetLocalTimeMinusDay("MMMM yyyy", 3);
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser20.UserName, AutoUser20.Password)
                .IsOnHomePage(AutoUser20);
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
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");
            //Drag resource type to round
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResultToRound(2)
                .VerifyToastMessage("Default Resource Set");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickRound(roundName)
                .ClickViewRoundGroup()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundDetailTab>()
                .IsOnDetailTab()
                .SwitchToTab("Default Resources");
            //Expand Driver and verify end date is current date
            PageFactoryManager.Get<RoundDefaultResourceTab>()
                .ExpandOption(2)
                .ClickOnSubEndDate(1)
                .VerifyEndDateIsDefault()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Move to future date and deallocate resource type from round
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCalendar()
                .InsertDayInFutre(dateInFutre)
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceName)
                .VerifyToastMessage("Default resource cleared");
            ////Verify End date is updated on round in future
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickRound("SKIP2 Daily Daily")
                .ClickViewRoundGroup()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Default Resources");
            PageFactoryManager.Get<RoundDefaultResourceTab>()
                .IsOnDefaultResourceTab()
                .ClickOnEndDate(2)
                .VerifyEndDateIsDefault()
                .ExpandOption(2)
                .ClickOnSubEndDate(1)
                .VerifyEndDateIs(monthYearInFuture, dateInFutre)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Verify End date is updated on round in current date
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCalendar()
                .InsertDayInFutre(currentDate)
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ExpandRoundGroup(2)
                .ClickRound(roundName)
                .ClickViewRoundGroup()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Default Resources");
            PageFactoryManager.Get<RoundDefaultResourceTab>()
                .IsOnDefaultResourceTab()
                .ClickOnEndDate(2)
                .VerifyEndDateIsDefault()
                .ExpandOption(2)
                .ClickOnSubEndDate(1)
                .VerifyEndDateIs(monthYearInFuture, dateInFutre)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Deallocate to maintain script
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceName)
                .VerifyToastMessage("Default resource cleared");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceType)
                .VerifyToastMessage("Default resource-type cleared");
        }
        [Category("Resources")]
        [Test]
        public void TC_66()
        {
            string roundName = "SKIP2 Daily Daily";
            string currentDate = CommonUtil.GetLocalTimeNow("dd");
            string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd", 3);
            string dateInFurtherFutre = CommonUtil.GetLocalTimeMinusDay("dd", 4);
            string monthYearInFuture = CommonUtil.GetLocalTimeMinusDay("MMMM yyyy", 3);
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser20.UserName, AutoUser20.Password)
                .IsOnHomePage(AutoUser20);
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
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");
            //Drag resource type to round - Allocation
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResultToRound(2)
                .VerifyToastMessage("Default Resource Set");
            //Verify End date is updated on round 
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickRound(roundName)
                .ClickViewRoundGroup()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Default Resources");
            PageFactoryManager.Get<RoundDefaultResourceTab>()
                .IsOnDefaultResourceTab()
                .ExpandOption(2)
                .ClickOnSubEndDate(1)
                .VerifyEndDateIsDefault()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();

            //Move to future date and deallocate resource type from round
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCalendar()
                .InsertDayInFutre(dateInFutre)
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceName)
                .VerifyToastMessage("Default resource cleared");
            //Verify End date is updated on round 
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickRound(roundName)
                .ClickViewRoundGroup()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Default Resources");
            PageFactoryManager.Get<RoundDefaultResourceTab>()
                .IsOnDefaultResourceTab()
                .ExpandOption(2)
                .ClickOnSubEndDate(1)
                .VerifyEndDateIs(monthYearInFuture, dateInFutre)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();

            //Move to further future date and allocate resource:
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCalendar()
                .InsertDayInFutre(dateInFurtherFutre)
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
               .FilterResource("Resource", resourceName)
               .VerifyFirstResultValue("Resource", resourceName)
               .DragAndDropFirstResultToRound(2)
               .VerifyToastMessage("Default Resource Set");
            //Verify End date is updated on round in future
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickRound(roundName)
                .ClickViewRoundGroup()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear()
                .SwitchToTab("Default Resources");
            PageFactoryManager.Get<RoundDefaultResourceTab>()
                .IsOnDefaultResourceTab()
                .ExpandOption(2)
                .ClickOnSubEndDate(1)
                .VerifyEndDateIs(monthYearInFuture, dateInFutre)
                .ExpandOption(3)
                .ClickOnSubEndDate(2)
                .VerifyEndDateIsDefault()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Deallocate to maintain script
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCalendar()
                .InsertDayInFutre(currentDate)
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceName)
                .VerifyToastMessage("Default resource cleared");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceType)
                .VerifyToastMessage("Default resource-type cleared");
        }
    }
}
