using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
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
        [Category("Huong")]
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
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .OpenOption("Round Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
               .ClickAddNewItem()
               .SwitchToLastWindow();
            var roundGroupPage = PageFactoryManager.Get<RoundGroupPage>();
            roundGroupPage.ClickOnElement(roundGroupPage.DetailTab);
            roundGroupPage.VerifyDefaultDataOnAddForm()
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
        [Category("Huong")]
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
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
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

            var roundDetailPage = PageFactoryManager.Get<RoundDetailPage>();
            roundDetailPage.WaitForLoadingIconToDisappear();
            roundDetailPage.ClickOnElement(roundDetailPage.DetailTab);
            roundDetailPage.VerifyRoundInput("Test Round")
                .VerifyRoundType("Skips")
                .VerifyDispatchSite("Townmead Tip & Depot (East)")
                .VerifyShift("PM: 14.00 - 21.30")
                .ClickAllTabAndVerify()
                .ClickCloseBtn()
                .SwitchToLastWindow();

            PageFactoryManager.Get<RoundGroupPage>()
                .IsRoundTab();
        }

        [Category("111_Add Default Resource on a Round Group")]
        [Category("Huong")]
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
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .OpenOption("Round Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<RoundGroupListPage>()
                .DoubleClickRoundGroup("SKIP2 Daily")
                .SwitchToChildWindow(2);
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
            var roundGroupPage = PageFactoryManager.Get<RoundGroupPage>();
            roundGroupPage.ClickAddResource(newRow);
            int relTypeIdx = roundGroupPage.GetIndexNewRowDetail(newRow);
            roundGroupPage.VerifyRowDetailIsVisible(newRow, relTypeIdx);
            int countOpt = PageFactoryManager.Get<RoundGroupPage>().GetResourceOptionCount(newRow, relTypeIdx);
            Random rnd = new Random();
            int index = rnd.Next(0, countOpt);
            PageFactoryManager.Get<RoundGroupPage>()
                .SelectResourceType(newRow, relTypeIdx, index)
                .ClickHasSchedule(newRow, relTypeIdx)
                .VerifyRightPanelTitle("Round Group Resource Allocation")
                .VerifyPatternStartDateContainString(dateNow)
                .VerifyAllPeriodTimeOptions(new System.Collections.Generic.List<string>() { "Daily", "Weekly", "Monthly", "Yearly" })
                .ClickPeriodTimeButton("Weekly")
                .SelectWeeklyFrequency("Every week")
                .ClickDayButtonOnWeekly("Tue")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyRightPanelIsInVisible()
                .ClickExpandButton(newRow);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyResourceDetailRow(newRow, relTypeIdx, index, true, $"Every Tuesday commencing {DateTime.Now.ToString("dddd dd MMMM yyyy")}", true, true)
                .ClickEditButton(newRow, relTypeIdx);
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
                .VerifyResourceDetailRow(newRow, relTypeIdx, index, true, $"Daily commencing {DateTime.Now.ToString("dddd dd MMMM yyyy")}", true, true);

            //Verify that user can sync Round Resources on a Round Group
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();
            List<DefaultResourceModel> defaultResourceOnRound = PageFactoryManager.Get<RoundGroupPage>().GetAllDefaultResourceModels();
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickSyncRoundResourceOnResourceTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
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
        [Category("Huong")]
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
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Ancillary")
                .ExpandOption("Skips")
                .OpenOption("Round Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<RoundGroupListPage>()
                .DoubleClickRoundGroup("SKIP2 Daily")
                .SwitchToChildWindow(2);
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
        [Category("Huong")]
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
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
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
                .ClickAddNewItemOnResourceTab()
                .WaitForLoadingIconToDisappear();
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
            RoundGroupPage roundGroupPage = PageFactoryManager.Get<RoundGroupPage>();
            roundGroupPage.ClickAddResource(newRow);
            Thread.Sleep(300);
            int relTypeIdx = roundGroupPage.GetIndexNewRowDetail(newRow);
            PageFactoryManager.Get<RoundGroupPage>()
                .SelectResourceType(newRow, relTypeIdx, "PK2 NST")
                .ClickHasSchedule(newRow, relTypeIdx)
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
                .VerifyResourceDetailRow(newRow, relTypeIdx, "PK2 NST", true, $"Every Monday and Tuesday commencing {DateTime.Now.ToString("dddd dd MMMM yyyy")}", startDate.ToString("dd/MM/yyyy"), endDate.ToString("dd/MM/yyyy"), true, true)
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
        [Category("Huong")]
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
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
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
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            int index = PageFactoryManager.Get<RoundGroupPage>().GetIndexResourceRowByType("Sweeper");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickExpandButton(index);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyResourceDetailRow(index, 0, "Liz Tudor", false, "", "15/12/2021", "01/01/2050", true, false)
                .ClickRetireDefaultResourceButton(index, "Liz Tudor")
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

        [Category("116_Add Schedule on Round")]
        [Category("Huong")]
        [Test]
        public void TC_116_Add_Schedule_On_A_Round()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
                .ExpandOption("Streets")
                .ExpandOption("Street Cleansing")
                .ExpandOption("Round Groups")
                .ExpandOption("East Zone 1")
                .OpenOption("Wednesday BAR")
                .SwitchNewIFrame();

            RoundGroupPage roundGroupPage = PageFactoryManager.Get<RoundGroupPage>();
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.ClickOnElement(roundGroupPage.ScheduleTab);
            roundGroupPage.WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(3000);
            string tomorrow = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            string endDate = DateTime.Now.AddDays(1).AddYears(1).ToString("dd/MM/yyyy");
            roundGroupPage
                .EditPatternEnd("179", tomorrow)
                .ClickSaveBtn()
                .VerifyToastMessage("Success");
            roundGroupPage.ClickOnElement(roundGroupPage.AddNewScheduleBtn);
            roundGroupPage.SwitchToLastWindow();
            RoundSchedulePage roundSchedulePage = PageFactoryManager.Get<RoundSchedulePage>();
            roundSchedulePage.WaitForLoadingIconToDisappear();
            roundSchedulePage.ClickOnElement(roundSchedulePage.DetailTab);
            roundSchedulePage.WaitForLoadingIconToDisappear();
            roundSchedulePage
                .VerifyInputValue(roundSchedulePage.StartDateInput, DateTime.Now.ToString("dd/MM/yyyy"))
                .VerifyInputValue(roundSchedulePage.EndDateInput, "01/01/2050")
                .VerifyElementVisibility(roundSchedulePage.SeasonSelect, true)
                .SendKeys(roundSchedulePage.StartDateInput, tomorrow);
            roundSchedulePage.SendKeys(roundSchedulePage.EndDateInput, endDate);
            roundSchedulePage.ClickOnElement(roundSchedulePage.ScheduleTab);
            roundSchedulePage.ClickPeriodTimeButton("Weekly")
                .SelectTextFromDropDown(roundSchedulePage.weeklyFrequencySelect, "Every fortnight");
            roundSchedulePage.ClickDayButtonOnWeekly("Wed")
                .ClickSaveBtn()
                .VerifyToastMessage("Success");
            string detail = $"Every Wednesday fortnightly commencing {DateTime.Now.AddDays(1).ToString("dddd dd MMMM yyyy")}";
            roundSchedulePage
                .VerifyElementText(roundSchedulePage.RoundScheduleTitle, detail, true, true)
                .VerifyElementText(roundSchedulePage.RoundScheduleStatus, "INACTIVE", toLowerCase: true)
                .CloseCurrentWindow()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            roundGroupPage.ClickOnElement(roundGroupPage.ScheduleTab);
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.VerifyNewSchedule(detail, tomorrow, endDate);
        }

        [Category("117_Retire existing Round schedule")]
        [Category("Huong")]
        [Test]
        public void TC_117_Retire_Existing_Round_Schedule()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Municipal)
                .ExpandOption("Streets")
                .ExpandOption("Street Cleansing")
                .ExpandOption("Round Groups")
                .ExpandOption("East Zone 1")
                .OpenOption("Friday MOR")
                .SwitchNewIFrame();

            RoundGroupPage roundGroupPage = PageFactoryManager.Get<RoundGroupPage>();
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.ClickOnElement(roundGroupPage.ScheduleTab);
            roundGroupPage.WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(5000);
            roundGroupPage.ClickScheduleDetail("181")
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            RoundSchedulePage roundSchedulePage = PageFactoryManager.Get<RoundSchedulePage>();
            roundSchedulePage.ClickOnElement(roundSchedulePage.RetireButton);
            roundSchedulePage.VerifyElementText(roundSchedulePage.RetireConfirmTitle, "Are you sure you want to retire this Round Schedule?")
                .ClickOnElement(roundSchedulePage.OKButton);
            roundSchedulePage.WaitForLoadingIconToDisappear();
            string tomorrow = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            roundSchedulePage.VerifyInputValue(roundSchedulePage.EndDateInput, tomorrow)
                .CloseCurrentWindow()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            roundGroupPage.ClickOnElement(roundGroupPage.ScheduleTab);
            roundGroupPage.WaitForLoadingIconToDisappear()
                .ClickRefreshBtn();
            roundGroupPage.VerifyPatternEnd("181", tomorrow)
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            roundGroupPage.RoundInstancesNotDisplayAfterEnddate(DateTime.Now.AddDays(1));
        }

        [Category("119_Add and Remove Round Sites on a Round")]
        [Category("Huong")]
        [Test]
        public void TC_119_Add_and_Remove_Round_Sites_on_a_Round()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Collections")
                .ExpandOption("Commercial Collections")
                .ExpandOption("Round Groups")
                .ExpandOption("REF1-AM")
                .OpenOption("Monday ")
                .SwitchToFrame(By.XPath("//iframe[@name='main']"));

            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickSiteTab()
                .IsOnSiteTab()
                .ClickRemoveRightSite("Kingston Tip");
            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .CheckRightSiteVisibility("Kingston Tip", false)
                .CheckLeftSiteVisibility("Kingston Tip", true)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickAddLeftSite("Kingston Tip");
            PageFactoryManager.Get<RoundGroupPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundGroupPage>()
                .CheckRightSiteVisibility("Kingston Tip", true)
                .CheckLeftSiteVisibility("Kingston Tip", false)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
        }

        [Category("Round Group")]
        [Category("Huong")]
        [Test]
        public void TC_169_1_Round_and_Round_Group_issues()
        {
            //Verify that user can open Round Group and add Round Gr Resource Schedule on Default Resource
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Collections")
                .ExpandOption("Commercial Collections")
                .ExpandOption("Round Groups")
                .OpenOption("REC2-AM")
                .SwitchNewIFrame();

            var roundGroupPage = PageFactoryManager.Get<RoundGroupPage>();
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();
            int resourceRowIdx = 0;
            roundGroupPage.ClickExpandButton(resourceRowIdx)
                 .ClickAddResource(resourceRowIdx);
            int relTypeIdx = roundGroupPage.GetIndexNewRowDetail(resourceRowIdx);
            roundGroupPage
                 .VerifyRowDetailIsVisible(resourceRowIdx, relTypeIdx)
                 .SelectResourceType(resourceRowIdx, relTypeIdx, 1)
                 .ClickSaveBtn()
                 .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                 .WaitForLoadingIconToDisappear();
            string dateNow = DateTime.Now.ToString("dd/MM/yyyy");
            roundGroupPage
                .ClickExpandButton(resourceRowIdx)
                .ClickHasSchedule(resourceRowIdx, relTypeIdx)
                .VerifyRightPanelTitle("Round Group Resource Allocation")
                .VerifyPatternStartDateContainString(dateNow)
                .ClickPeriodTimeButton("Weekly")
                .SelectWeeklyFrequency("Every week")
                .ClickDayButtonOnWeekly("Mon")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            roundGroupPage
                .VerifyRightPanelIsInVisible()
                .ClickExpandButton(resourceRowIdx);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyResourceDetailRow(resourceRowIdx, relTypeIdx, 1, true, $"Every Monday commencing {DateTime.Now.ToString("dddd dd MMMM yyyy")}", true, true)
                .ClickEditButton(resourceRowIdx, relTypeIdx);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyRightPanelTitle("Round Group Resource Allocation")
                .IsPeriodButtonSelected("Weekly")
                .IsDayButtonOnWeeklySelected("Mon")
                .VerifySelectWeeklyFrequency("Every week")
                .ClickDayButtonOnWeekly("Wed")
                .ClickDayButtonOnWeekly("Fri")
                .EnterQuantity(resourceRowIdx, "2")
                .EnterQuantity(resourceRowIdx, "1")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Success");
            PageFactoryManager.Get<RoundGroupPage>()
                .ClickExpandButton(resourceRowIdx);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
               .VerifyResourceDetailRow(resourceRowIdx, relTypeIdx, 1, true, $"Every Monday, Wednesday and Friday commencing {DateTime.Now.ToString("dddd dd MMMM yyyy")}", true, true);
        }

        [Category("Round Group")]
        [Category("Huong")]
        [Test]
        public void TC_169_2_Round_and_Round_Group_issues()
        {
            //Verify that user can open Round and add Round Res Schedule on Default Resource
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Collections")
                .ExpandOption("Commercial Collections")
                .ExpandOption("Round Groups")
                .ExpandOption("REF3-PM (Narrow)")
                .OpenOption("Tuesday")
                .SwitchNewIFrame();

            var roundGroupPage = PageFactoryManager.Get<RoundGroupPage>();
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.ClickDefaultResourceTab()
                .WaitForLoadingIconToDisappear();
            int driverTypeIdx = 0;
            int relTypeIdx = roundGroupPage.GetIndexNewRowDetail(driverTypeIdx);
            string dateNow = DateTime.Now.ToString("dd/MM/yyyy");
            roundGroupPage
                .ClickExpandButton(driverTypeIdx)
                .ClickHasSchedule(driverTypeIdx, relTypeIdx)
                .VerifyRightPanelTitle("Round Resource Allocation")
                .VerifyPatternStartDateContainString(dateNow)
                .ClickPeriodTimeButton("Weekly")
                .SelectWeeklyFrequency("Every week")
                .ClickDayButtonOnWeekly("Tue")
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            roundGroupPage
                .VerifyRightPanelIsInVisible()
                .ClickExpandButton(driverTypeIdx);
            Thread.Sleep(300);
            PageFactoryManager.Get<RoundGroupPage>()
                .VerifyResourceDetailRow(driverTypeIdx, relTypeIdx, "Hedy Lamarr", true, $"Every Tuesday commencing {DateTime.Now.ToString("dddd dd MMMM yyyy")}", true);
            roundGroupPage.ClickOnElement(roundGroupPage.ScheduleTab);
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.VerifyScheduleDetail("Every Tuesday commencing Monday 10 January 2022")
                .ClickEditSchedule()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            RoundSchedulePage roundSchedulePage = PageFactoryManager.Get<RoundSchedulePage>();
            roundSchedulePage.ClickOnElement(roundSchedulePage.ScheduleTab);
            roundSchedulePage.WaitForLoadingIconToDisappear();
            roundSchedulePage.ClickDayButtonOnWeekly("Fri")
                .ClickSaveAndCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            roundGroupPage.SleepTimeInMiliseconds(5000);
            roundGroupPage.VerifyScheduleDetail("Every Tuesday and Friday commencing Monday 10 January 2022");
        }

        [Category("Round Group")]
        [Category("Huong")]
        [Test]
        public void TC_118_Display_Round_Instance_in_Calendar_on_a_Round()
        {
            //Verify that user can view Round Instance in the Calendar on a Round form
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser37.UserName, AutoUser37.Password)
                .IsOnHomePage(AutoUser37);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Collections")
                .ExpandOption("Commercial Collections")
                .ExpandOption("Round Groups")
                .ExpandOption("REF1-AM")
                .OpenLastOption("Monday ")
                .SwitchNewIFrame();
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddYears(1);
            var roundGroupPage = PageFactoryManager.Get<RoundGroupPage>();
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.ClickCalendarTab()
                .WaitForLoadingIconToDisappear();

            DateTime roundInstanceA = roundGroupPage.DoubleClickRoundGroup(startDate, endDate, new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday }, null);
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstancePage roundInstancePage = PageFactoryManager.Get<RoundInstancePage>();
            roundInstancePage.ClickOnElement(roundInstancePage.DetailsTab);
            roundInstancePage.WaitForLoadingIconToDisappear();
            roundInstancePage.ClickOnElement(roundInstancePage.StatusInput);
            roundInstancePage.SelectByDisplayValueOnUlElement(roundInstancePage.SelectDropdown, "Complete")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            roundInstancePage.VerifyElementText(roundInstancePage.SelectedStatusText, "Complete")
                .ClickCloseBtn()
                .AceptAlertIfPresent()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            roundGroupPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            roundGroupPage.VerifyRoundInstanceState(roundInstanceA, "coreroundstate/3.svg");

            DateTime roundInstanceB = roundGroupPage.DoubleClickRoundGroup(startDate, endDate, new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday }, new List<DateTime>() { roundInstanceA });
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            roundInstancePage.ClickOnElement(roundInstancePage.DetailsTab);
            roundInstancePage.WaitForLoadingIconToDisappear();
            roundInstancePage.ClickOnElement(roundInstancePage.StatusInput);
            roundInstancePage.SelectByDisplayValueOnUlElement(roundInstancePage.SelectDropdown, "Delayed")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            roundInstancePage.VerifyElementText(roundInstancePage.SelectedStatusText, "Delayed")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            roundGroupPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            roundGroupPage.VerifyRoundInstanceState(roundInstanceB, "coreroundstate/5.svg");

            DateTime roundInstanceC = roundGroupPage.DoubleClickRoundGroup(startDate, endDate, new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday }, new List<DateTime>() { roundInstanceA, roundInstanceB });
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            roundInstancePage.ClickOnElement(roundInstancePage.DetailsTab);
            roundInstancePage.WaitForLoadingIconToDisappear();
            roundInstancePage.ClickOnElement(roundInstancePage.StatusInput);
            roundInstancePage.SelectByDisplayValueOnUlElement(roundInstancePage.SelectDropdown, "Not Done")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            roundInstancePage.VerifyElementText(roundInstancePage.SelectedStatusText, "Not Done")
                .ClickCloseBtn()
                .AceptAlertIfPresent()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            roundGroupPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            roundGroupPage.VerifyRoundInstanceState(roundInstanceC, "coreroundstate/4.svg");

            DateTime roundInstanceD = roundGroupPage.DoubleClickRoundGroup(startDate, endDate, new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday }, new List<DateTime>() { roundInstanceA, roundInstanceB, roundInstanceC });
            roundGroupPage.WaitForLoadingIconToDisappear();
            roundGroupPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            roundInstancePage.ClickOnElement(roundInstancePage.DetailsTab);
            roundInstancePage.WaitForLoadingIconToDisappear();
            roundInstancePage.ClickOnElement(roundInstancePage.StatusInput);
            roundInstancePage.SelectByDisplayValueOnUlElement(roundInstancePage.SelectDropdown, "In Progress")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            roundInstancePage.VerifyElementText(roundInstancePage.SelectedStatusText, "In Progress")
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            roundGroupPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            roundGroupPage.VerifyRoundInstanceState(roundInstanceD, "coreroundstate/2.svg");
        }
    }
}
