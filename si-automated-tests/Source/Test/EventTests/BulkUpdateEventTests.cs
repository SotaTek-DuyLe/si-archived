using System;
using System.Collections.Generic;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.EventTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class BulkUpdateEventTests : BaseTest
    {
        [Category("BulkUpdateEvent")]
        [Category("Chang")]
        [Test(Description = "Bulk update event - add note")]
        public void TC_142_Bulk_update_event_add_note()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string firstEventId = "13";
            string secondEventId = "12";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser53.UserName, AutoUser53.Password)
                .IsOnHomePage(AutoUser53);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Events)
                .OpenOption(Contract.RM)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            EventsListingPage eventListingPage = PageFactoryManager.Get<EventsListingPage>();
            eventListingPage
                .FilterByMultipleEventId(firstEventId, secondEventId)
                .ClickCheckboxMultipleEventInList()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventBulkUpdatePage eventBulkUpdatePage = PageFactoryManager.Get<EventBulkUpdatePage>();
            string[] eventIds = { firstEventId, secondEventId };
            string[] eventTypes = { "Complaint", "Complaint" };
            string[] eventServices = { "Bulky Collections", "Clinical Waste" };
            string[] eventAddress = { "6 RALEIGH ROAD, RICHMOND, TW9 2DX", "4A RALEIGH ROAD, RICHMOND, TW9 2DX" };
            List<EventInBulkUpdateEventModel> eventInBulkUpdateEventModels = eventBulkUpdatePage
                .IsEventBulkUpdatePage()
                .GetAllEventInPage();
            eventBulkUpdatePage
                .VerifyRecordInfoInEventBulkUpdatePage(eventInBulkUpdateEventModels, eventIds, eventTypes, eventServices, eventAddress);
            //Step 2: Select [Add notes] - Line 8
            string notesValue = "Notes" + CommonUtil.GetRandomNumber(5);
            eventBulkUpdatePage
                .ClickActionDdAndVerify()
                .SelectAnyAction(CommonConstants.ActionEventBulkUpdate[0])
                .AddNotes(notesValue)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);

            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string updatedTime = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);

            eventBulkUpdatePage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Step 5: Click [Refresh] btn - Line 10
            eventListingPage
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            eventListingPage
                .FilterByEventId(firstEventId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            //Step 6: Check History of Event - Line 11
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            //Get DB
            List<EventDBModel> firstEventModels = finder.GetEvent(int.Parse(firstEventId));
            List<EventDBModel> secondEventModels = finder.GetEvent(int.Parse(secondEventId));
            //First event
            eventDetailPage
                .VerifyHistoryWithDB(firstEventModels[0], "josie", 2, updatedTime, 1099)
                .VerifyRecordInHistoryTabAfterAddNote(AutoUser53.DisplayName)
                .VerifyNotesAfterAddNote(notesValue)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            eventListingPage
                .ClickClearBtn()
                .WaitForLoadingIconToDisappear();
            //Second event
            eventListingPage
                .FilterByEventId(secondEventId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .ClickHistoryTab()
                .WaitForLoadingIconToDisappear();
            eventDetailPage
                .VerifyHistoryWithDB(secondEventModels[0], "A User", 3, updatedTime, 1099)
                .VerifyRecordInHistoryTabAfterAddNote(AutoUser53.DisplayName)
                .VerifyNotesAfterAddNote(notesValue)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);

        }
    }
}
