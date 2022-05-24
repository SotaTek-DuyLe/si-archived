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
            string allocatedUnitValue = "East Waste";

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
                .VerifyAllocatedUnitDisplayTheSameEventForm(allAllocatedUnitEventForm, allAllocatedUnitEventAction)
            //Select [Allocated Unit]
                .SelectAnyAllocatedUnit(allocatedUnitValue)
                .ClickOnAllocatedUser();
            List<string> allAllocatedUserEventAction = eventActionPage
                .GetAllOptionsInAllocatedUserDd();
            //Select [Allocated User]
            eventActionPage
                .SelectAnyAllocatedUser(allAllocatedUserEventAction[0])
                .ClickSaveAndCloseBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            //Verify value in [Event detail] page
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInStatus("Initial Assessment")
                .VerifyValueInAllocatedUnit(allocatedUnitValue)
                .VerifyValueInAssignedUser(allAllocatedUserEventAction[0]);
            //Verify value in [History] tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNewRecordInHistoryTab("Initial Assessment", allAllocatedUserEventAction[0], allocatedUnitValue, AutoUser60.DisplayName);
            string newEventName = "NewEvent112" + CommonUtil.GetRandomString(5);
            //Back to [Data] tab and fill some fields
            eventDetailPage
                .ClickDataTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .InputNameInDataTab(newEventName)
                //Click [Accept] btn
                .ClickAcceptInEventActionsPanel()
                .VerifyToastMessage(MessageSuccessConstants.SaveEventMessage)
                .VerifyToastMessage(MessageSuccessConstants.ActionSuccessMessage)
                .WaitForLoadingIconToDisappear();
            //Verify new Status
            eventDetailPage
                .VerifyValueInStatus("Under Investigation")
                .VerifyValueInNameFieldInDataTab(newEventName);
            //Verify value in [History] tab
            eventDetailPage
               .ClickHistoryTab()
               .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyRecordInHistoryTabAfterUpdate(newEventName, AutoUser60.DisplayName)
                .VerifyRecordInHistoryTabAfterAccept("Under Investigation.", AutoUser60.DisplayName);
            //Click [Add Note] btn
            eventDetailPage
                .ClickAddNoteInEventsActionsPanel()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EventActionPage>()
                .ClickSaveAndCloseBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Step 21:
        }
    }
}
