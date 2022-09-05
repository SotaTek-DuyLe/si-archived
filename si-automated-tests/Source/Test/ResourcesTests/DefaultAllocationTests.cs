using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.Round;
using System.Collections.Generic;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    class DefaultAllocationTests : BaseTest
    {
        string rscTypeSet = "Default resource-type set";
        string rscTypeClear = "Default resource-type cleared";
        string rscSet = "Default Resource Set";
        string rscClear = "Default resource cleared";
        [Category("Resources")]
        [Category("Dee")]
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
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Default Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.RMC)
                .SelectBusinessUnit(Contract.RMC)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000)
                .SwitchToTab("Resource Type");
            //Drag resource type to round
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyFirstResultValue("Type", resourceType)
                .DragAndDropFirstResultToRoundGroup(2)
                .VerifyAllocatingToast("Default resource-type set");
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
                .VerifyAllocatingToast("Default resource-type cleared");
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
                .VerifyAllocatingToast("Default resource-type cleared");
        }
        [Category("Resources")]
        [Category("Dee")]
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
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Default Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.RMC)
                .SelectBusinessUnit(Contract.RMC)
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
                .DragAndDropFirstResultToRoundGroup(2)
                .VerifyAllocatingToast("Default Resource Set");
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
                .ClickOnLastSubEndDate()
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
                .VerifyAllocatingToast("Default resource cleared");
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
                .ClickOnLastSubEndDate()
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
                .ClickOnLastSubEndDate()
                .VerifyEndDateIs(monthYearInFuture, dateInFutre)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Deallocate to maintain script
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceName)
                .VerifyAllocatingToast("Default resource cleared");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceType)
                .VerifyAllocatingToast("Default resource-type cleared");
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_66_Allocation_Deallocation_Verify_Corresponding_Date()
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
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Default Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.RMC)
                .SelectBusinessUnit(Contract.RMC)
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
                .DragAndDropFirstResultToRoundGroup(2)
                .VerifyAllocatingToast("Default Resource Set");
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
                .ClickOnLastSubEndDate()
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
                .VerifyAllocatingToast("Default resource cleared");
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
                .ClickOnLastSubEndDate()
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
               .DragAndDropFirstResultToRoundGroup(2)
               .VerifyAllocatingToast("Default Resource Set");
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
                .ClickOnSecondLastSubEndDate()
                .VerifyEndDateIs(monthYearInFuture, dateInFutre)
                .ExpandOption(3)
                .ClickOnLastSubEndDate()
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
                .VerifyAllocatingToast("Default resource cleared");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceFromRoundGroup(2, resourceType)
                .VerifyAllocatingToast("Default resource-type cleared");
        }
        [Category("Resources")]
        [Category("Dee")]
        [Test]
        public void TC_137_default_allocation_test()
        {
            var listMessagesResourceType = new List<string>();
            listMessagesResourceType.Add(rscTypeSet);
            listMessagesResourceType.Add(rscTypeClear);
            var listMessagesResource = new List<string>();
            listMessagesResource.Add(rscSet);
            listMessagesResource.Add(rscClear);
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser20.UserName, AutoUser20.Password)
                .IsOnHomePage(AutoUser20);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Default Allocation")
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.RMC)
                .SelectBusinessUnit(Contract.RMC)
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            //ALLOCATING RESOURCE TYPE TO ROUND GROUP
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .SwitchToTab("Resource Types");
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .FilterResource("Type", "Driver");
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .DragAndDropFirstResultToRoundGroup(1)
            //    .VerifyAllocatingToast("Default resource-type set");
            ////ALLOCATING RESOURCE TO ALLOCATED RESOURCE TYPE
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .SwitchToTab("All Resources");
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .FilterResource("Type", "Driver");
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .DragAndDropFirstResultToBlankResourceType("Driver")
            //    .VerifyAllocatingToast("Default Resource Set");
            ////ALLOCATING RESOURCE TYPE TO NEW BOX
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .SwitchToTab("Resource Types");
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .FilterResource("Type", "Loader");
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .DragAndDropFirstResultToNewCell()
            //    .VerifyAllocatingToast("Default resource-type set");
            ////DEALLOCATING RESOURCE TYPE FROM ROUND GROUP
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .DeallocateResourceType("Loader")
            //    .VerifyAllocatingToast("Default resource-type cleared");
            ////DEALLOCATING RESOURCE FROM ROUND GROUP
            //string allocatedResourceName = PageFactoryManager.Get<ResourceAllocationPage>().GetFirstAllocatedResource();
            //PageFactoryManager.Get<ResourceAllocationPage>()
            //    .DeallocateResource(allocatedResourceName)
            //    .VerifyAllocatingToast("Default resource cleared");
            //REALLOCATING RESOURCE FROM ROUND GROUP TO ANOTHER ROUND GROUP
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SwitchToTab("Resource Types");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Type", "Loader");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DragAndDropFirstResultToNewCell()
                .VerifyAllocatingToast("Default resource-type set")
                .RelocateResourceTypeFromRoundGroupToRoundGroup("Loader", 2)
                .VerifyAllocatingToast(listMessagesResourceType);
            //REALLOCATING RESOURCE FROM ROUND GROUP TO ROUND OF DIFFERENT ROUND GROUP
            PageFactoryManager.Get<ResourceAllocationPage>()
                .RelocateResourceTypeFromRoundGroupToRound("Loader", 1)
                .VerifyAllocatingToast(listMessagesResourceType);
            //REALLOCATING RESOURCE FROM ROUND GROUP TO ROUND OF SAME ROUND GROUP
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DeallocateResourceType(1)
                .VerifyAllocatingToast("Default resource-type cleared");
            PageFactoryManager.Get<ResourceAllocationPage>();
                //.FilterResource("Type", "Driver");
            PageFactoryManager.Get<ResourceAllocationPage>()
                .DragAndDropFirstResultToNewCell()
                .VerifyAllocatingToast("Default resource-type set")
                .DeallocateResourceType(6)
                .VerifyAllocatingToast("Default resource-type cleared")
                .RelocateResourceTypeFromRoundGroupToRound("Loader", 5)
                .VerifyAllocatingToast(listMessagesResourceType);
            //REALLOCATING RESOURCE FROM ROUND TO ROUND OF SAME ROUND GROUP
            PageFactoryManager.Get<ResourceAllocationPage>()
                .RelocateResourceTypeFromRoundToRound(1, 2)
                .VerifyAllocatingToast(listMessagesResourceType);
            //REALLOCATING RESOURCE FROM ROUND TO ROUND OF DIFFERENT ROUND GROUP
            PageFactoryManager.Get<ResourceAllocationPage>()
                .RelocateResourceTypeFromRoundToRound(1, 6)
                .VerifyAllocatingToast(listMessagesResourceType);
            //REALLOCATING RESOURCE FROM ROUND TO ROUND GROUP OF SAME ROUND GROUP
            PageFactoryManager.Get<ResourceAllocationPage>()
                .RelocateResourceTypeFromRoundGroupToRoundGroup("Loader", 1)
                .VerifyAllocatingToast(listMessagesResourceType);




            PageFactoryManager.Get<ResourceAllocationPage>()
                .SleepTimeInMiliseconds(10000);

        }
    }
}
