using System.Collections.Generic;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.EventTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class DeleteEventTests : BaseTest
    {
        //BUG
        [Category("DeleteEvent")]
        [Category("Chang")]
        [Test(Description = "Delete event")]
        public void TC_106_Delete_event()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string eventIdWithServiceUnit = "3";

            //DB get data with eventID = 3
            List<EventDBModel> listEvents = finder.GetEvent(int.Parse(eventIdWithServiceUnit));

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser7.UserName, AutoUser7.Password)
                .IsOnHomePage(AutoUser7);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Events)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            EventsListingPage eventListingPage = PageFactoryManager.Get<EventsListingPage>();
            eventListingPage
                .FilterByEventId(eventIdWithServiceUnit)
                //Click row with icon
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
            eventDetailPage
                .WaitForEventDetailDisplayed()
                .VerifyEventName(listEvents[0].eventobjectdesc)
                .VerifyEventTypeWithDB(listEvents[0].echodisplayname)
                //Verify [Detail]
                .ExpandDetailToggle()
                .VerifyValueInSubDetailInformation(listEvents[0])
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();

            eventListingPage
                .ClickDeleteBtn()
                .SwitchToChildWindow(2);

            PageFactoryManager.Get<DeleteEventPage>()
                .IsWarningPopup()
                //No btn
                .ClickNoBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            eventListingPage
                .VerifyWindowClosed(1);
            eventListingPage
                .ClickDeleteBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<DeleteEventPage>()
                .IsWarningPopup()
                //==> small bug
                //Close btn
                .ClickClosePopupBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            eventListingPage
                .VerifyWindowClosed(1);
            eventListingPage
                .ClickDeleteBtn()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<DeleteEventPage>()
                .IsWarningPopup()
                //Yes btn
                .ClickYesBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            eventListingPage
                .VerifyWindowClosed(1);
            eventListingPage
                .ClickClearBtn()
                .WaitForLoadingIconToDisappear();
            eventListingPage
                .FilterByEventId(eventIdWithServiceUnit)
                .VerifyNoRecordDisplayed();
            //DB
            List<EventDBModel> listEventsAfter = finder.GetEvent(int.Parse(eventIdWithServiceUnit));
            Assert.AreEqual(0, listEventsAfter.Count);
        }
    }
}
