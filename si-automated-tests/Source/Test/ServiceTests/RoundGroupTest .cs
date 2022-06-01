using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models.Services;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointAddress;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class RoundGroupTest : BaseTest
    {
        [Category("109_Add a Round Group")]
        [Test]
        public void TC_109_Add_Round_Group()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .OpenOption("Round Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToLastWindow();
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDefaultDataOnAddForm()
                .ClickSaveBtn()
                .VerifyToastMessage("Field is required")
                .WaitUntilToastMessageInvisible("Field is required");
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .EnterRoundGroupValue("SKIP 3")
                .ClickSaveBtn()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyServiceButtonsVisible()
                .ClickOnDispatchSiteAndVerifyData()
                .SelectDispatchSite("Kingston Tip")
                .EnterRoundGroupValue("SKIP 3ABC")
                .ClickSaveBtn()
                .VerifyToastMessage("Success")
                .WaitUntilToastMessageInvisible("Success");
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyRoundGroup("SKIP 3ABC");
        }

        [Category("110_Add Round on a Round Group")]
        [Test]
        public void TC_110_Add_Round_on_a_Round_Group()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .OpenOption("Round Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<RoundGroupListPage>()
                .DoubleClickRoundGroup("SKIP2 Daily")
                .SwitchToLastWindow();
            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickRoundTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickAddNewItemOnRoundTab()
                .ClickSaveBtn()
                .VerifyToastMessages(new System.Collections.Generic.List<string>() { "Shift is required" , "Dispatch Site is required" , "Round Type is required" , "Round is required" });

            int newRowIdx = PageFactoryManager.Get<RoundGroupPage>().GetIndexNewRoundRow();
            PageFactoryManager.Get<RoundGroupPage>()
                .EnterRoundValue(newRowIdx, "Test Round")
                .EnterRoundTypeValue(newRowIdx, "Skips")
                .EnterDispatchSiteValue(newRowIdx, "Townmead Tip & Depot (East)")
                .EnterShiftValue(newRowIdx, "PM: 14.00 - 21.30")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            Thread.Sleep(1000);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyRoundColor(newRowIdx, "#000000")
                .DoubleClickRound(newRowIdx)
                .SwitchToLastWindow();

            PageFactoryManager.Get<RoundDetailPage>()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<RoundDetailPage>()
                .VerifyRoundInput("Test Round")
                .VerifyRoundType("Skips")
                .VerifyDispatchSite("Townmead Tip & Depot (East)")
                .VerifyShift("PM: 14.00 - 21.30")
                .ClickAllTabAndVerify()
                .ClickCloseBtn()
                .AcceptAlert()
                .SwitchToLastWindow();

            PageFactoryManager.Get<RoundGroupPage>()
                .IsRoundTab();
        }

        [Category("111_Add Default Resource on a Round Group")]
        [Test]
        public void TC_111_Add_Default_Resource_on_a_Round_Group()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .OpenOption("Round Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<RoundGroupListPage>()
                .DoubleClickRoundGroup("SKIP2 Daily")
                .SwitchToLastWindow();
            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<RoundGroupPage>()
               .ClickAddNewItemOnResourceTab();
            Thread.Sleep(300);
            int newRow = PageFactoryManager.Get<RoundGroupPage>().GetIndexNewResourceRow();
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDropDownTypeIsPresent(newRow)
                .VerifyInputQuantityIsPresent(newRow)
                .VerifyRetireButtonIsPresent(newRow)
                .SelectType(newRow, "Van")
                .EnterQuantity(newRow, "1")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickExpandButton(newRow);
            Thread.Sleep(300);
            string dateNow = DateTime.Now.ToString("dd/MM/yyyy");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickAddResource(newRow)
                .VerifyRowDetailIsVisible(newRow);
            int countOpt = PageFactoryManager.Get<RoundGroupPage>().GetResourceOptionCount(newRow);
            Random rnd = new Random();
            int index = rnd.Next(0, countOpt);
            PageFactoryManager.Get<RoundGroupPage>()
                .SelectResourceType(newRow, index)
                .ClickHasSchedule(newRow)
                .VerifyRightPanelTitle("Round Group Resource Allocation")
                .VerifyPatternStartDateContainString(dateNow)
                .VerifyAllPeriodTimeOptions(new System.Collections.Generic.List<string>() { "Daily", "Weekly", "Monthly", "Yearly" })
                .ClickPeriodTimeButton("Weekly")
                .SelectWeeklyFrequency("Every week")
                .ClickDayButtonOnWeekly("Tue")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyRightPanelIsInVisible()
                .ClickExpandButton(newRow);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyResourceDetailRow(newRow, index, true, $"Every Tuesday commencing {DateTime.Now.ToString("dddd dd MMMM yyyy")}", true, true)
                .ClickEditButton(newRow);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyRightPanelTitle("Round Group Resource Allocation")
                .IsPeriodButtonSelected("Weekly")
                .IsDayButtonOnWeeklySelected("Tue")
                .VerifySelectWeeklyFrequency("Every week")
                .ClickPeriodTimeButton("Daily")
                .SelectDailyFrequency("Every day")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickExpandButton(newRow);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyResourceDetailRow(newRow, index, true, $"Daily commencing {DateTime.Now.ToString("dddd dd MMMM yyyy")}", true, true);

            //Verify that user can sync Round Resources on a Round Group
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickRoundTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .DoubleClickRound("Daily")
                .SwitchToLastWindow();
            PageFactoryManager.Get<RoundDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundDetailPage>()
                .ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundDetailPage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow();

            PageFactoryManager.Get<RoundGroupPage>()
                .ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();
            List<DefaultResourceModel> defaultResourceOnRound = PageFactoryManager.Get<RoundGroupPage>().GetAllDefaultResourceModels();
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickSyncRoundResourceOnResourceTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Successfully saved Round Group");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickRoundTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .DoubleClickRound("Daily")
                .SwitchToLastWindow();
            PageFactoryManager.Get<RoundDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundDetailPage>()
                .ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();
            List<DefaultResourceModel> defaultResourceOnRoundDetailAfterSync = PageFactoryManager.Get<RoundDetailPage>().GetAllDefaultResourceModels();
            PageFactoryManager.Get<RoundDetailPage>()
                .IsDefaultResourceSync(defaultResourceOnRound, defaultResourceOnRoundDetailAfterSync);
        }

        [Category("113_Retire Default Resource on a Round Group")]
        [Test]
        public void TC_113_Retire_Default_Resource_on_a_Round_Group()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .OpenOption("Round Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<RoundGroupListPage>()
                .DoubleClickRoundGroup("SKIP2 Daily")
                .SwitchToLastWindow();
            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickRetireButton("Driver");
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDefaultResourceIsInVisible("Driver")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDefaultResourceIsInVisible("Driver");
        }

        [Category("114_Add Default Resource on Round")]
        [Test]
        public void TC_114_Add_Default_Resource_on_a_Round()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star")
                .ExpandOption("Streets")
                .ExpandOption("Street Cleansing")
                .ExpandOption("Round Groups")
                .ExpandOption("East Zone 1")
                .OpenOption("Monday RIC")
                .SwitchNewIFrame();

            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDefaultResourceRowsIsVisible()
                .ClickAddNewItemOnResourceTab();
            Thread.Sleep(300);
            string dateNow = DateTime.Now.ToString("dd/MM/yyyy");
            DateTime startDate = DateTime.Now.AddDays(7);
            DateTime endDate = DateTime.Now.AddYears(1);
            int newRow = PageFactoryManager.Get<RoundGroupPage>().GetIndexNewResourceRow();
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDropDownTypeIsPresent(newRow)
                .VerifyInputQuantityIsPresent(newRow)
                .VerifyRetireButtonIsPresent(newRow)
                .VerifyStartDateInput(newRow, dateNow)
                .VerifyEndDateInput(newRow, "01/01/2050")
                .SelectType(newRow, "Van")
                .EnterQuantity(newRow, "1")
                .EnterStartDate(newRow, startDate.ToString("dd/MM/yyyy"))
                .EnterEndDate(newRow, endDate.ToString("dd/MM/yyyy"))
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDropDownTypeIsDisable(newRow)
                .VerifyStartDateInputIsDisable(newRow)
                .ClickExpandButton(newRow);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickAddResource(newRow);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .SelectResourceType(newRow, "PK2 NST")
                .ClickHasSchedule(newRow)
                .VerifyRightPanelTitle("Round Resource Allocation")
                .VerifyPatternStartDateContainString(dateNow)
                .VerifyAllPeriodTimeOptions(new System.Collections.Generic.List<string>() { "Daily", "Weekly", "Monthly", "Yearly" })
                .ClickPeriodTimeButton("Weekly")
                .SelectWeeklyFrequency("Every week")
                .ClickDayButtonOnWeekly("Mon")
                .ClickDayButtonOnWeekly("Tue")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickExpandButton(newRow);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyResourceDetailRow(newRow, "PK2 NST", true, $"Every Monday and Tuesday commencing {DateTime.Now.ToString("dddd dd MMMM yyyy")}", startDate.ToString("dd/MM/yyyy"), endDate.ToString("dd/MM/yyyy"), true, true)
                .ClickCalendarTab()
                .DoubleClickRoundGroup(startDate, endDate, new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday })
                .SwitchToChildWindow(2);

            PageFactoryManager.Get<RoundInstancePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstancePage>()
                .ClickAllocatedResourcesTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstancePage>()
                .VerifyAllocateResourceContainType("Van", "PK2 NST");
        }

        [Category("115_Retire Default Resource on a Round ")]
        [Test]
        public void TC_115_Retire_Default_Resource_On_A_Round()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star")
                .ExpandOption("Streets")
                .ExpandOption("Street Cleansing")
                .ExpandOption("Round Groups")
                .ExpandOption("East Zone 1")
                .OpenOption("Tuesday MOR")
                .SwitchNewIFrame();

            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();
            int index = PageFactoryManager.Get<RoundGroupPage>().GetIndexResourceRowByType("Sweeper");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickExpandButton(index);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyResourceDetailRow(index, "Liz Tudor", false, "", "15/12/2021", "01/01/2050", true, false)
                .ClickRetireDefaultResourceButton("Liz Tudor")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            index = PageFactoryManager.Get<RoundGroupPage>().GetIndexResourceRowByType("Sweeper");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickExpandButton(index);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDetailDefaultResourceIsInVisible("Sweeper", "Liz Tudor")
                .ClickRetireButton("Sweeper")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyDefaultResourceIsInVisible("Sweeper");
        }
    }
}
