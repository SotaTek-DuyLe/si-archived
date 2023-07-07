using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Core;
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
        [Category("Chang")]
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
                .ClickMainOption(MainOption.Events)
                .OpenOption(Contract.Municipal)
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
                .InputNoteInDataTab(noteInDataTab)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveEventMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string timeNow = CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            DateTime today = DateTime.Today;
            string dueDate;
            if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                dueDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 10);
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                dueDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 9);
            }
            else
            {
                dueDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 11);
            }
            //Check [History] tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            List<string> allAllocatedUnitEventForm = eventDetailPage
                .VerifyHistoryData(timeNow, dueDate, nameInDataTab, noteInDataTab, "New", AutoUser60.DisplayName)
                //Get All value in [Allocated Unit]
                .ExpandDetailToggle()
                .ClickOnAllocatedUnit()
                .GetAllOptionInAllocatedUnitDetailSubTab();
            List<string> allAssignedUserEventForm = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            eventDetailPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allocatedUnitValue);
            List<string> assignedUserMapped = eventDetailPage
                .ClickOnAssignedUserInDetailSubTab()
                .GetAllOptionInAssignedUserDetailSubTab();
            eventDetailPage
                 .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit("");

            //Allocate event
            eventDetailPage
                .ClickAllocateEventInEventActionsPanel()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();

            //EVENT ACTION
            EventActionPage eventActionPage = PageFactoryManager.Get<EventActionPage>();
            List<string> allAllocatedUnitEventAction = eventActionPage
                .IsEventActionPage()
                .ClickOnAllocatedUnit()
                .GetAllOptionsInAllocatedUnitDd();
            eventActionPage
                .VerifyAllocatedUnitDisplayTheSameEventForm(allAllocatedUnitEventForm, allAllocatedUnitEventAction);
            //Step 12
            List<string> allAllocatedUserEventAction = eventActionPage
                .ClickOnAllocatedUser()
                .GetAllOptionsInAllocatedUserDd();
            eventActionPage
                .VerifyAllocatedUserDisplayTheSameEventForm(allAssignedUserEventForm, allAllocatedUserEventAction);
            

            //Select [Allocated Unit]
            eventActionPage
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit(allocatedUnitValue)
                .ClickOnAllocatedUser();
            allAllocatedUserEventAction = eventActionPage
                .GetAllOptionsInAllocatedUserDd();
            Assert.IsTrue(allAllocatedUserEventAction.SequenceEqual(assignedUserMapped));
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
                //Line 18: Click [Accept] btn 
                .ClickAcceptInEventActionsPanel()
                .VerifyDisplayToastMessage(MessageSuccessConstants.ActionSuccessMessage)
                .WaitForLoadingIconToDisappear();
            //Verify new Status
            eventDetailPage
                .VerifyValueInStatus("Initial Assessment")
                .VerifyValueInNameFieldInDataTab(newEventName);
            //Line 19: Verify value in [History] tab
            eventDetailPage
               .ClickHistoryTab()
               .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyRecordInHistoryTabAfterAccept("Under Investigation.", AutoUser60.DisplayName, newEventName);
            //Click [Add Note] btn
            eventDetailPage
                .ClickAddNoteInEventsActionsPanel()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EventActionPage>()
                .ClickSaveAndCloseBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(3);
            //Verify value in [History] tab
            eventDetailPage
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyRecordInHistoryTabAfterAddNote(AutoUser60.DisplayName)
                //Step 22
                .ClickOnServicesTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
               .ClickFirstEventInFirstServiceRow()
               .ClickAnyEventOption(eventOption)
               .SwitchToLastWindow()
               .WaitForLoadingIconToDisappear();
            eventDetailPage
               .WaitForEventDetailDisplayed();
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveEventMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Verify details value
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInStatus("New")
                .VerifyValueInAllocatedUnit(allocatedUnitValue)
                .VerifyValueInAssignedUser(allAllocatedUserEventAction[0])
                .ClickDataTab()
                .WaitForLoadingIconToDisappear();
            string clientRef = "Client Ref 112" + CommonUtil.GetRandomString(4);
            string emailAddress = "email112" + CommonUtil.GetRandomString(5) + "@email.com";
            eventDetailPage
                .VerifyValueInNameFieldInDataTab(newEventName)
                .VerifyValueInNoteField(noteInDataTab)
                //Step 23: Input clientRef and emailAddress
                .InputClientRef(clientRef)
                .InputEmailAddress(emailAddress)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveEventMessage);
            eventDetailPage
                .VerifyValueInClientRef(clientRef)
                .VerifyValueInEmailFieldInDataTab(emailAddress);
            eventDetailPage
                .VerifyCurrentEventUrl();
            //Step 24: Verify history tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyRecordInHistoryTabAfterUpdateClientRefAndEmail(clientRef, emailAddress, AutoUser60.DisplayName);
            //Step 25: Cancel
            eventDetailPage
                .ClickCancelInEventsActionsPanel()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SaveEventMessage)
                .VerifyDisplayToastMessage(MessageSuccessConstants.ActionSuccessMessage)
                .WaitForLoadingIconToDisappear();
            string endDate = CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            string resolvedDate = CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            eventDetailPage
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyEndDateAndResolvedDate()
                .VerifyValueInStatus("Cancelled");
            //Step 26: Verify history tab
            eventDetailPage
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyRecordInHistoryTabAfterCancel("Cancelled", endDate, resolvedDate, AutoUser60.DisplayName);
            //Step 27: Create new event
            eventDetailPage
                .ClickOnServicesTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
               .ClickFirstEventInFirstServiceRow()
               .ClickAnyEventOption(eventOption)
               .SwitchToLastWindow()
               .WaitForLoadingIconToDisappear();
            eventDetailPage
               .WaitForEventDetailDisplayed();
            eventDetailPage
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveEventMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Verify details value
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInStatus("New")
                .VerifyValueInAllocatedUnit(allocatedUnitValue)
                .VerifyValueInAssignedUser(allAllocatedUserEventAction[0])
                .ClickDataTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyValueInNameFieldInDataTab(newEventName)
                .VerifyValueInNoteField(noteInDataTab)
                .VerifyValueInEmailFieldInDataTab(emailAddress);
            List<string> allActionInEventActions = eventDetailPage
                .GetAllOptionInEventActions();
            //Step 28
            List<string> allActions = eventDetailPage
                .ClickActionBtn()
                .GetAllOptionInActionDd();
            eventDetailPage
                .VerifyActionAreTheSame(allActions, allActionInEventActions);
            //Step 29
            eventDetailPage
                .ClickAnyOptionInActionDd("Allocate Event")
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventActionPage
                .IsEventActionPage()
                //Step 30
                .ClickOnAllocatedUnit()
                .SelectAnyAllocatedUnit("East Waste")
                .ClickSaveAndCloseBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(5)
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .ExpandDetailToggle()
                .VerifyValueInStatus("Initial Assessment")
                .VerifyValueInAllocatedUnit("East Waste")
                //.VerifyValueInAssignedUser("")
                .VerifyValueInAssignedUser("A User")
                //Step 31
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyNewRecordInHistoryTabAfterAllocate("Initial Assessment", AutoUser60.DisplayName);
        }
    }
}
