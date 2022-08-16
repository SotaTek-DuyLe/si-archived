using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ApplicationTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class RoundCalendarTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser39.UserName, AutoUser39.Password)
                .IsOnHomePage(AutoUser39);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Applications")
                .OpenOption("Round Calendar")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
        }

        [Category("RoundCalendarTests")]
        [Test(Description = "Verify that Round Calendar displays correctly")]
        public void TC_135_1_Verify_that_Round_Calendar_displays_correctly()
        {
            RoundCalendarPage roundCalendarPage = PageFactoryManager.Get<RoundCalendarPage>();
            roundCalendarPage.SelectTextFromDropDown(roundCalendarPage.SelectContact, Contract.RM);
            roundCalendarPage
                .ClickInputService()
                .SelectServiceNode(Contract.RM)
                .SelectServiceNode("Recycling")
                .SelectServiceNode("Domestic Recycling")
                .SelectTextFromDropDown(roundCalendarPage.SelectShiftGroup, "AM")
                .ClickOnElement(roundCalendarPage.ButtonGo);
            roundCalendarPage.WaitForLoadingIconToDisappear();
            roundCalendarPage.ClickOnElement(roundCalendarPage.ButtonMonth);
            roundCalendarPage
                .ClickMoreButton(true)
                .IsListOfRoundInstanceScheduleDisplayed()
                .ClickMoreButton(false)
                .IsListOfRoundInstanceScheduleDisplayed()
                .ClickRoundInstance(true)
                .VerifyToastMessage("Cannot reschedule rounds more than a week in the past")
                .WaitUntilToastMessageInvisible("Cannot reschedule rounds more than a week in the past");
            roundCalendarPage
                .ClickRoundInstance(false)
                .VerifyToastMessagesIsUnDisplayed();

            RoundInstanceForm roundInstanceForm = PageFactoryManager.Get<RoundInstanceForm>();
            roundCalendarPage
                .DoubleClickRoundInstance(true)
                .VerifyToastMessage("Cannot reschedule rounds more than a week in the past")
                .WaitUntilToastMessageInvisible("Cannot reschedule rounds more than a week in the past")
                .SwitchToChildWindow(2);
            roundInstanceForm
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            roundCalendarPage
                .DoubleClickRoundInstance(false)
                .VerifyToastMessagesIsUnDisplayed()
                .SwitchToChildWindow(2);
            roundCalendarPage.SwitchToChildWindow(2);
            roundInstanceForm
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            roundCalendarPage.ClickOnElement(roundCalendarPage.ButtonWeek);
            roundCalendarPage.IsRoundCalendarInWeekDisplayed();
            roundCalendarPage.ClickOnElement(roundCalendarPage.ButtonDay);
            roundCalendarPage.IsRoundCalendarInDayDisplayed();

            roundCalendarPage.ClickOnElement(roundCalendarPage.ButtonLegend);
            roundCalendarPage.IsCalendarScheduleDisplayed();
            roundCalendarPage.ClickOnElement(roundCalendarPage.ButtonLegend);
            roundCalendarPage.IsCanlendarScheduleUnDisplayed();

            roundCalendarPage.ClickOnElement(roundCalendarPage.ButtonMonth);
            roundCalendarPage
                .ClickRoundInstance(DateTime.Now)
                .VerifyRoundInstanceBackground(DateTime.Now, "rgba(194, 219, 255, 1)")
                .VerifyElementEnable(roundCalendarPage.ButtonSchedule, true);
            roundCalendarPage
                .ClickRoundInstance(DateTime.Now)
                .VerifyRoundInstanceBackground(DateTime.Now, "rgba(255, 255, 255, 1)")
                .VerifyElementEnable(roundCalendarPage.ButtonSchedule, false);

            roundCalendarPage
                .ClickRoundInstance(DateTime.Now)
                .ClickOnElement(roundCalendarPage.ButtonSchedule);

            RescheduleModal rescheduleModal = PageFactoryManager.Get<RescheduleModal>();
            DateTime scheduleDay = DateTime.Now.AddDays(3);
            rescheduleModal
                .IsRescheduleModelDisplayedCorrectly()
                .SendKeys(rescheduleModal.InputRescheduleDate, scheduleDay.ToString("dd/MM/yyyy"));
            rescheduleModal.ClickOnElement(rescheduleModal.ButtonOk);
            rescheduleModal
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Selected Round Instance(s) have been rescheduled")
                .WaitUntilToastMessageInvisible("Selected Round Instance(s) have been rescheduled");
            roundCalendarPage.RoundInstanceHasGreenBackground(scheduleDay);
        }

        [Category("RoundCalendarTests")]
        [Test(Description = "Verify that user can find a Round using 'Round Finder' option")]
        public void TC_135_2_Verify_that_user_can_find_a_Round_using_Round_Finder_option()
        {
            RoundCalendarPage roundCalendarPage = PageFactoryManager.Get<RoundCalendarPage>();
            roundCalendarPage.SelectTextFromDropDown(roundCalendarPage.SelectContact, Contract.RMC);
            roundCalendarPage
                .ClickInputService()
                .SelectServiceNode(Contract.RMC)
                .SelectServiceNode("Collections")
                .SelectServiceNode("Commercial Collections")
                .SelectTextFromDropDown(roundCalendarPage.SelectShiftGroup, "AM")
                .ClickOnElement(roundCalendarPage.ButtonGo);
            roundCalendarPage.WaitForLoadingIconToDisappear();
            roundCalendarPage.ClickOnElement(roundCalendarPage.ButtonMonth);
            roundCalendarPage.WaitForLoadingIconToDisappear();
            roundCalendarPage.ClickOnElement(roundCalendarPage.ButtonRoundFinder);
            DateTime tomorrow = DateTime.Now.AddDays(1);
            roundCalendarPage
                .ClickInputRound()
                .ExpandRoundNode("Commercial Collections")
                .ExpandRoundNode("REC1-AM")
                .SelectRoundNode(tomorrow.DayOfWeek.ToString())
                .ClickButtonFind()
                .VerifyToastMessage("Original date is required")
                .WaitUntilToastMessageInvisible("Original date is required");
            roundCalendarPage
                .SendInputOriginDate(tomorrow.ToString("dd/MM/yyyy"));
            roundCalendarPage.ClickButtonFind();
            roundCalendarPage.WaitForLoadingIconToDisappear();
            RescheduleModal rescheduleModal = PageFactoryManager.Get<RescheduleModal>();
            rescheduleModal.ClickOnElement(rescheduleModal.ButtonReschedule);
            DateTime scheduleDay = DateTime.Now.AddDays(2);
            rescheduleModal
                .SendKeys(rescheduleModal.InputRescheduleDate, scheduleDay.ToString("dd/MM/yyyy"));
            rescheduleModal
                .SendKeysWithoutClear(rescheduleModal.InputRescheduleDate, OpenQA.Selenium.Keys.Enter);
            rescheduleModal.ClickOnElement(rescheduleModal.ButtonOk);
            rescheduleModal
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Selected Round Instance(s) have been rescheduled")
                .WaitUntilToastMessageInvisible("Selected Round Instance(s) have been rescheduled");
            roundCalendarPage.RoundInstanceHasGreenBackground(scheduleDay);
        }
    }
}
