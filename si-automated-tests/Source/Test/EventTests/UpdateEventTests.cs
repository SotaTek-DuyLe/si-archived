using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.EventTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class UpdateEventTests : BaseTest
    {
        [Category("CreateEvent")]
        [Test(Description = "Event actions, event updates")]
        public void TC_112_Event_actions_event_updates()
        {
            string eventID = "12";
            string eventOption = "Standard - Complaint";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser60.UserName, AutoUser60.Password)
                .IsOnHomePage(AutoUser60);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Events")
                .OpenOption("North Star")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EventsListingPage>()
                .FilterByEventId(eventID)
                //Click row with icon
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .ClickOnServicesTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
               .ClickFirstEventInFirstServiceRow()
               .ClickAnyEventOption(eventOption)
               .SwitchToLastWindow()
               .WaitForLoadingIconToDisappear();
            eventDetailPage
               .WaitForEventDetailDisplayed();
            string nameInDataTab = "Event112" + CommonUtil.GetRandomString(5);
            string noteInDataTab = "EventNote112" + CommonUtil.GetRandomString(5);
            //Enter text some fields
            eventDetailPage
                .ClickDataTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .InputNameInDataTab(nameInDataTab)
                .InputNote(noteInDataTab)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveEventMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string timeNow = CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            //Check [History] tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<string> allAllocatedUnitEventForm = eventDetailPage
                .VerifyHistoryData(timeNow, timeNow, nameInDataTab, noteInDataTab, "New", AutoUser60.DisplayName)
                //Get All value in [Allocated Unit]
                .ExpandDetailToggle()
                .ClickOnAllocatedUnit()
                .GetAllOptionInAllocatedUnitDetailSubTab();
            //Allocate event
            eventDetailPage
                .ClickAllocateEventInEventActionsPanel()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventActionPage eventActionPage = PageFactoryManager.Get<EventActionPage>();
            List<string> allAllocatedUnitEventAction = eventActionPage
                .IsEventActionPage()
                .ClickOnAllocatedUnit()
                .GetAllOptionsInAllocatedUnitDd();
            eventActionPage
                .VerifyAllocatedUnitDisplayTheSameEventForm(allAllocatedUnitEventForm, allAllocatedUnitEventAction);
        }
    }
}
